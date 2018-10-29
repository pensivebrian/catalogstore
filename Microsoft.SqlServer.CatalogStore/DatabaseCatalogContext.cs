using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace Microsoft.SqlServer.CatalogStore
{
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
            this.InitializeSQLite();
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
