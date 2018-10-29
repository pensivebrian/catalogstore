using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace CatalogStoreCodeGenerator
{
    public class CatalogVersion
    {
        public DataSet CatalogDataSet;
        public int ServerVersion;
        public string ReleaseName;
        public string XmlFile;
        public string TxtFile;
        public string ConnectionString;

        public void LoadCatalogDataSetFromXml()
        {
            this.CatalogDataSet = new DataSet("Catalog");
            this.CatalogDataSet.ReadXml(this.XmlFile);
            this.IncludeWhitelist();
        }

        private void IncludeWhitelist()
        {
            List<string> whitelist = File.ReadAllLines(@"..\..\..\Catalog.Whitelist.txt").ToList();
            List<DataTable> filteredTables = this.CatalogDataSet.Tables
                .OfType<DataTable>()
                .Where(t => !whitelist.Any(f => t.TableName.StartsWith("sys." + f, StringComparison.InvariantCultureIgnoreCase)))
                .ToList();

            foreach (DataTable tableToRemove in filteredTables)
            {
                this.CatalogDataSet.Tables.Remove(tableToRemove);
            }
        }

        private void ExcludeBlacklist()
        {
            List<string> blacklist = File.ReadAllLines(@"..\..\Catalog.Blacklist.txt").ToList();
            List<DataTable> filteredTables = this.CatalogDataSet.Tables
                .OfType<DataTable>()
                .Where(t => blacklist.Any(f => t.TableName.StartsWith("sys." + f, StringComparison.InvariantCultureIgnoreCase)))
                .ToList();

            foreach (DataTable tableToRemove in filteredTables)
            {
                this.CatalogDataSet.Tables.Remove(tableToRemove);
            }
        }

        public void LoadDataSetCatalogSchemaDatabase()
        {
            this.CatalogDataSet = new DataSet("Catalog");
            List<DataTable> dataTables = LoadDataTables(this.ConnectionString);
            foreach (DataTable dataTable in dataTables)
            {
                this.CatalogDataSet.Tables.Add(dataTable);
            }
        }

        public void GenerateDataSetXmlFile()
        {
            this.LoadDataSetCatalogSchemaDatabase();
            this.CatalogDataSet.WriteXml(this.XmlFile, XmlWriteMode.WriteSchema);

            using (FileStream fs = new FileStream(this.TxtFile, FileMode.Create, FileAccess.Write))
            using (TextWriter tw = new StreamWriter(fs))
            foreach (DataTable dataTable in this.CatalogDataSet.Tables)
            {
                tw.WriteLine(dataTable.TableName);
            }
        }

        private List<DataTable> LoadDataTables(string connectionString)
        {
            List<CatalogObject> catalogObjects = CatalogObject.Load(connectionString);
            List<DataTable> dataTables = new List<DataTable>(catalogObjects.Count);

            using (SqlConnection conn = new SqlConnection(this.ConnectionString))
            {
                conn.Open();
                foreach (CatalogObject catalog in catalogObjects)
                {
                    Console.WriteLine("Loading table {0}...", catalog.Name);

                    try
                    {
                        DataTable dataTable = new DataTable(catalog.Name);
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = "select * from " + catalog.Name;
                            using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SchemaOnly))
                            {
                                dataTable = reader.GetSchemaTable();
                                dataTable.TableName = catalog.Name;
                            }
                        }

                        dataTables.Add(dataTable);
                    }
                    catch (SqlException se)
                    {
                        Console.WriteLine("Error generating type {0}: {1}", catalog.Name, se.Message);
                    }
                }
            }

            return dataTables;
        }

        public static readonly CatalogVersion[] Versions = new[]
        {
            new CatalogVersion
            {
                ServerVersion = 150,
                ReleaseName = "Azure",
                XmlFile = @"..\..\..\Catalog.Azure.xml",
                TxtFile = @"..\..\..\Catalog.Azure.txt",
                ConnectionString = Program.ConnectionStringAzure,
            },
            new CatalogVersion
            {
                ServerVersion = 150,
                ReleaseName = "vNext",
                XmlFile = @"..\..\..\Catalog.vNext.xml",
                TxtFile = @"..\..\..\Catalog.vNext.txt",
                ConnectionString = Program.ConnectionStringvNext,
            },
            new CatalogVersion
            {
                ServerVersion = 140,
                ReleaseName = "2017",
                XmlFile = @"..\..\..\Catalog.2017.xml",
                TxtFile = @"..\..\..\Catalog.2017.txt",
                ConnectionString = Program.ConnectionString2017,
            },
            new CatalogVersion
            {
                ServerVersion = 130,
                ReleaseName = "2016",
                XmlFile = @"..\..\..\Catalog.2016.xml",
                TxtFile = @"..\..\..\Catalog.2016.txt",
                ConnectionString = Program.ConnectionString2016,
            },
            new CatalogVersion
            {
                ServerVersion = 120,
                ReleaseName = "2014",
                XmlFile = @"..\..\..\Catalog.2014.xml",
                TxtFile = @"..\..\..\Catalog.2014.txt",
                ConnectionString = Program.ConnectionString2014,
            },
            new CatalogVersion
            {
                ServerVersion = 110,
                ReleaseName = "2012",
                XmlFile = @"..\..\..\Catalog.2012.xml",
                TxtFile = @"..\..\..\Catalog.2012.txt",
                ConnectionString = Program.ConnectionString2012,
            },
            new CatalogVersion
            {
                ServerVersion = 100,
                ReleaseName = "2008",
                XmlFile = @"..\..\..\Catalog.2008.xml",
                TxtFile = @"..\..\..\Catalog.2008.txt",
                ConnectionString = Program.ConnectionString2008,
            },
            new CatalogVersion
            {
                ServerVersion = 90,
                ReleaseName = "2005",
                XmlFile = @"..\..\..\Catalog.2005.xml",
                TxtFile = @"..\..\..\Catalog.2005.txt",
                ConnectionString = Program.ConnectionString2005,
            },
         };
    }
}
