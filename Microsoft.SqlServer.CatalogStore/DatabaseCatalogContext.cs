using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using Microsoft.Data.Sqlite;

namespace Microsoft.SqlServer.CatalogStore
{
    public enum CatalogStore
    {
        InMemory,
        File,
    }

    public partial class DatabaseCatalogContext
    {
        private ServerInfo _serverInfo;

        private const string SelectServerInfoCommand =
            "SELECT SERVERPROPERTY('EngineEdition') as [EngineEdition], " +
            "SERVERPROPERTY('ProductVersion') as [ProductVersion], " +
            "@@MICROSOFTVERSION as [MicrosoftVersion]";

        private const int AzureEngineEdition = 5;
        private const int MangagedInstanceEngineEdition = 8;

        private sealed class ServerInfo
        {
            public int EngineEdition;           // e.g. 3
            public Version Version;             // e.g. 15.0.1000.196

            public bool IsAzure { get { return this.EngineEdition == AzureEngineEdition; } }
            public bool IsManagedInstance { get { return this.EngineEdition == MangagedInstanceEngineEdition; } }
        }

        public void LoadCatalog(string connectionString)
        {
            this.LoadCatalog(connectionString, CatalogStore.InMemory);
        }

        public void LoadCatalog(string connectionString, CatalogStore catalogStore)
        {
            if (catalogStore == CatalogStore.File)
            {
                _connectionString = string.Format("DataSource=file:{0}.dacmodel;Cache=Shared", _connectionId);
            }
            else
            {
                _connectionString = string.Format("DataSource=file:{0}.dacmodel;Mode=Memory;Cache=Shared", _connectionId);
            }

            _sqliteConnection = new SqliteConnection(_connectionString);
            _sqliteConnection.Open();

            using (SqliteCommand command = _sqliteConnection.CreateCommand())
            {
                // Enable write-ahead logging to increase write performance by reducing amount of disk writes,
                // by combining writes at checkpoint, salong with using sequential-only writes to populate the log.
                // Also, WAL allows for relaxed ("normal") "synchronous" mode, see below.
                command.CommandText = "pragma journal_mode=wal";
                command.ExecuteNonQuery();

                // Set "synchronous" mode to "normal" instead of default "full" to reduce the amount of buffer flushing syscalls,
                // significantly reducing both the blocked time and the amount of context switches.
                // When coupled with WAL, this (according to https://sqlite.org/pragma.html#pragma_synchronous and 
                // https://www.sqlite.org/wal.html#performance_considerations) is unlikely to significantly affect durability,
                // while significantly increasing performance, because buffer flushing is done for each checkpoint, instead of each
                // transaction. While some writes can be lost, they are never reordered, and higher layers will recover from that.
                command.CommandText = "pragma synchronous=normal";
                command.ExecuteNonQuery();
            }

            this.InitializeSQLite();        // Takes about .15 seconds.  We could pre-create the file.

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                _serverInfo = GetServerInfo(connection);

                if (_serverInfo.IsManagedInstance)
                {
                    throw new NotSupportedException("Managed Instance is not supported.");
                }

                if (_serverInfo.IsAzure)
                {
                    this.LoadAzure(connection);
                }
                else if (_serverInfo.Version.Major == 15)
                {
                    this.LoadvNext(connection);
                }
                else if (_serverInfo.Version.Major == 14)
                {
                    this.Load2017(connection);
                }
                else if (_serverInfo.Version.Major == 13)
                {
                    this.Load2016(connection);
                }
                else if (_serverInfo.Version.Major == 12)
                {
                    this.Load2014(connection);
                }
                else if (_serverInfo.Version.Major == 11)
                {
                    this.Load2012(connection);
                }
                else if (_serverInfo.Version.Major == 10)
                {
                    this.Load2008(connection);
                }
                else if (_serverInfo.Version.Major == 9)
                {
                    this.Load2005(connection);
                }
                else
                {
                    throw new NotSupportedException(string.Format("Server version {0} is not supported.", _serverInfo.Version));
                }
            }
        }

        private static ServerInfo GetServerInfo(SqlConnection connection)
        {
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = SelectServerInfoCommand;
                using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow | CommandBehavior.SequentialAccess))
                {
                    reader.Read();
                    ServerInfo serverInfo = new ServerInfo();
                    serverInfo.EngineEdition = Convert.ToInt32(reader.GetValue(0));

                    // For SQL Managed Instance, determine the underlying engine version by parsing @@MICROSOFTVERSION
                    if (serverInfo.EngineEdition == MangagedInstanceEngineEdition)
                    {
                        int version = Convert.ToInt32(reader.GetString(2));
                        serverInfo.Version = new Version(string.Format(
                            CultureInfo.InvariantCulture,
                            "{0}.{1}.{2}",
                            ((int)(version / 0x01000000)).ToString(CultureInfo.InvariantCulture),
                            ((int)(version / 0x010000 & 15)).ToString(CultureInfo.InvariantCulture),
                            ((int)version & 255)));
                    }
                    else
                    {
                        serverInfo.Version = new Version(reader.GetString(1));
                    }

                    return serverInfo;
                }
            }

        }
    }
}
