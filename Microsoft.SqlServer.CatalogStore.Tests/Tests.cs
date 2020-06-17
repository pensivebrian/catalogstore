using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.SqlServer.CatalogStore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.SqlServer.CatalogStore.Tests
{
    [TestClass]
    public class Tests
    {
        public static string NavConnectionString { get { return "server=bro-sql2017;database=NAV;Integrated Security=true"; } }

        public static string Nav2ConnectionString { get { return "server=bro-sql2017;database=NAV2;Integrated Security=true"; } }

        public static string Nav5ConnectionString { get { return "server=bro-sql2017;database=NAV5;Integrated Security=true"; } }

        public static string Nav10ConnectionString { get { return "server=bro-sql2017;database=NAV10;Integrated Security=true"; } }

        public static string Nav20ConnectionString { get { return "server=bro-sql2017;database=NAV20;Integrated Security=true"; } }

        public string AzureAdventureWorksBasicConnectionString { get { return Environment.GetEnvironmentVariable("AZURE_BASIC_CONNECTION_STRING"); } }

        public string AzureAdventureWorksS3ConnectionString { get { return Environment.GetEnvironmentVariable("AZURE_S3_CONNECTION_STRING"); } }

        public string AzureAdventureWorksP1ConnectionString { get { return Environment.GetEnvironmentVariable("AZURE_P1_CONNECTION_STRING"); } }

        public string AdventureWorksConnectionString { get { return "server=bro-sql2017;database=AdventureWorks;Integrated Security=true"; } }

        public string WideWorldImportersConnectionString { get { return "server=bro-sql2017;database=AdventureWorks;Integrated Security=true"; } }

        public string Dynamics365ConnectionString { get { return "server=bro-sql2017;database=dyn365;Integrated Security=true"; } }

        [TestMethod]
        public void TestDynamics365CatalogStore()
        {
            RunCatalogStoreTest(Dynamics365ConnectionString);
        }

        [TestMethod]
        public void TestDynamics365CatalogStoreFile()
        {
            RunCatalogStoreTest(Dynamics365ConnectionString, CatalogStore.File);
        }


        [TestMethod]
        public void TestAdventureWorksCatalogStore()
        {
            RunCatalogStoreTest(AdventureWorksConnectionString);
        }

        [TestMethod]
        public void TestAdventureWorksCatalogStoreFile()
        {
            RunCatalogStoreTest(AdventureWorksConnectionString, CatalogStore.File);
        }

        [TestMethod]
        public void TestAdventureWorksDacFx()
        {
            RunDacFxTest(AdventureWorksConnectionString);
        }

        [TestMethod]
        public void TestNavCatalogStore()
        {
            RunCatalogStoreTest(NavConnectionString);
        }

        [TestMethod]
        public void TestNavDacFx()
        {
            RunDacFxTest(NavConnectionString);
        }

        [TestMethod]
        public void TestNav2CatalogStore()
        {
            RunCatalogStoreTest(Nav2ConnectionString);
        }

        [TestMethod]
        public void TestNav2DacFx()
        {
            RunDacFxTest(Nav2ConnectionString);
        }

        [TestMethod]
        public void TestNav5CatalogStore()
        {
            RunCatalogStoreTest(Nav5ConnectionString);
        }

        [TestMethod]
        public void TestNav5DacFx()
        {
            RunDacFxTest(Nav5ConnectionString);
        }

        [TestMethod]
        public void TestNav10CatalogStore()
        {
            RunCatalogStoreTest(Nav10ConnectionString);
        }

        [TestMethod]
        public void TestNav10DacFx()
        {
            RunDacFxTest(Nav10ConnectionString);
        }

        [TestMethod]
        public void TestNav20CatalogStore()
        {
            RunCatalogStoreTest(Nav20ConnectionString);
        }

        [TestMethod]
        public void TestNav20DacFx()
        {
            RunDacFxTest(Nav20ConnectionString);
        }

        [TestMethod]
        public void TestWideWorldImportersCatalogStore()
        {
            RunCatalogStoreTest(WideWorldImportersConnectionString);
        }

        [TestMethod]
        public void TestWideWorldImportersDacFx()
        {
            RunDacFxTest(WideWorldImportersConnectionString);
        }

        [TestMethod]
        public void TestAzureAdventureWorksBasicCatalogStore()
        {
            RunCatalogStoreTest(this.AzureAdventureWorksBasicConnectionString);
        }

        [TestMethod]
        public void TestAzureAdventureWorksBasicDacFx()
        {
            RunDacFxTest(this.AzureAdventureWorksBasicConnectionString, azure: true);
        }

        [TestMethod]
        public void TestAzureAdventureWorksS3CatalogStore()
        {
            RunCatalogStoreTest(this.AzureAdventureWorksS3ConnectionString);
        }

        [TestMethod]
        public void TestAzureAdventureWorksS3DacFx()
        {
            RunDacFxTest(this.AzureAdventureWorksS3ConnectionString, azure: true);
        }

        [TestMethod]
        public void TestAzureAdventureWorksP1CatalogStore()
        {
            RunCatalogStoreTest(this.AzureAdventureWorksP1ConnectionString);
        }

        [TestMethod]
        public void TestAzureAdventureWorksP1DacFx()
        {
            RunDacFxTest(this.AzureAdventureWorksP1ConnectionString, azure: true);
        }

        public void RunCatalogStoreTest(string connectionString, CatalogStore catalogStore = CatalogStore.InMemory)
        {
            try
            {
                DatabaseCatalogContext databaseCatalog = new DatabaseCatalogContext();
                Stopwatch stopwatch = Stopwatch.StartNew();
                databaseCatalog.LoadCatalog(connectionString, catalogStore);
                stopwatch.Stop();
                Console.WriteLine("Loading: {0}", stopwatch.Elapsed.TotalSeconds);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void RunDacFxTest(string connectionString, bool azure = false)
        {
            try
            {
                DacFxReverseEngineer dacFxRE = new DacFxReverseEngineer();
                Stopwatch stopwatch = Stopwatch.StartNew();
                dacFxRE.Load(connectionString, azure);
                stopwatch.Stop();
                Console.WriteLine("DacFx Load: {0}", stopwatch.Elapsed.TotalSeconds);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
