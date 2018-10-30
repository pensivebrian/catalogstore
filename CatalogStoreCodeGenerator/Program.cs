using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogStoreCodeGenerator
{
    class Program
    {
        public static string ConnectionStringAzure = "server=sqltoolsv12tests.database.windows.net;database=keep_broneill_1;User Id=cloudsa;Password={0}";
        public static string ConnectionStringvNext = "server=sqltoolsvnext-5;Integrated Security=true";
        public static string ConnectionString2017 = "server=sqltools2017-3;Integrated Security=true";
        public static string ConnectionString2016 = "server=sqltools2016-3;Integrated Security=true";
        public static string ConnectionString2014 = "server=sqltools2014-3;Integrated Security=true";
        public static string ConnectionString2012 = "server=sqltools2012-3;Integrated Security=true";
        public static string ConnectionString2008 = "server=sqltools2008R2;Integrated Security=true";
        public static string ConnectionString2005 = "server=sqltools2005-3;Integrated Security=true";

        static int Main(string[] args)
        {

            try
            {
                Stopwatch sw = Stopwatch.StartNew();

                // This only needs to be done when the query for the catalog changes.  We cache the results
                // an .xml file, along with a .txt that lists all catalog objects:
                //  
                //  Catalog.2005.xml
                //  Catalog.2005.txt
                //
                // GenerateCatalogDataSetXmlFiles();                   

                GenerateCatalogCode();

                Console.WriteLine("Complete ({0})", sw.Elapsed);
                return 0;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("An unhandled exception has occurred: {0}", e);
                return 1;
            }
            finally
            {
                if (Debugger.IsAttached)
                {
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(intercept: true);
                }
            }
        }

        private static void GenerateCatalogCode()
        {
            CatalogGenerator catalogGenerator = new CatalogGenerator();
            catalogGenerator.Generate(CatalogVersion.Versions.ToList(), @"..\..\..\..\Microsoft.SqlServer.CatalogStore\CodeGen");
        }

        private static void GenerateCatalogDataSetXmlFiles()
        {
            SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder(ConnectionStringAzure);
            Console.WriteLine("Enter Azure SQLDB password for database {0} on server {1}:", csb.InitialCatalog, csb.DataSource);
            string password = ReadPasswordFromConsole();
            ConnectionStringAzure = string.Format(ConnectionStringAzure, password);

            foreach (CatalogVersion catalogVersion in CatalogVersion.Versions)
            {
                Console.WriteLine("Generating {0}...", Path.GetFileName(catalogVersion.XmlFile));
                catalogVersion.GenerateDataSetXmlFile();
            }
        }

        private static string ReadPasswordFromConsole()
        {
            string password = "";
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                // Backspace Should Not Work
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                    {
                        password = password.Substring(0, (password.Length - 1));
                        Console.Write("\b \b");
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                }
            }
            while (true);

            Console.WriteLine();
            return password;
        }

    }
}
