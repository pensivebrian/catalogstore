using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.TransactSql.ScriptDom;

namespace ReverseEngineeringTableDump
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                IEnumerable<string> tables = GetTableReferences(@"..\..\..\DacFxReverseEngineeringQueryAzure.sql", SqlEngineType.SqlAzure);
                tables = tables.Union(GetTableReferences(@"..\..\..\DacFxReverseEngineeringQuery2016.sql", SqlEngineType.Standalone)).OrderBy(t => t);
                File.WriteAllLines(@"..\..\..\..\CatalogStoreCodeGenerator\Catalog.Whitelist.txt", tables);
                Console.WriteLine("Complete!");
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception occurred: {0}", e);
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
        private static List<string> GetTableReferences(string file, SqlEngineType engineType)
        {
            using (TextReader reader = File.OpenText(file))
            {
                TSql140Parser parser = new TSql140Parser(initialQuotedIdentifiers: false, engineType: engineType);
                IList<ParseError> errors = null;
                StatementList statements = parser.ParseStatementList(reader, out errors);

                if (errors.Count > 0)
                {
                    throw new Exception(string.Format("File {0} had {1} errors", file, errors.Count));
                }

                TableVisitor visitor = new TableVisitor();
                statements.Accept(visitor);
                List<string> tables = visitor.Tables.Distinct().OrderBy(t => t).ToList();
                return tables;
            }
        }
    }

    public class TableVisitor : TSqlConcreteFragmentVisitor
    {
        private List<string> _tables = new List<string>();
        public override void ExplicitVisit(NamedTableReference node)
        {
            _tables.Add(node.SchemaObject.BaseIdentifier.Value);
            base.ExplicitVisit(node);
        }

        public List<string> Tables { get { return _tables; } }
    }
}