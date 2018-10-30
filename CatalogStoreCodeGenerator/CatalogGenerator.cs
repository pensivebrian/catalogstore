using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogStoreCodeGenerator
{
    public class CatalogGenerator
    {  
        public void Generate(List<CatalogVersion> catalogVersions, string outputFolder)
        {
            foreach (CatalogVersion catalogVersion in catalogVersions)
            {
                Console.WriteLine("Loading catalog xml file {0}...", catalogVersion.XmlFile);
                catalogVersion.LoadCatalogDataSetFromXml();
            }

            Console.WriteLine("Merging catalog xml files...");
            DataSet mergedDataSet = this.MergeDataSet(catalogVersions);

            List<CodeTypeDeclaration> typeDecls = new List<CodeTypeDeclaration>();

            Console.WriteLine("Generating type {0}...", "DatabaseCatalogContext");
            DatabaseCatalogTypeGenerator databaseCatalogTypeGenerator = new DatabaseCatalogTypeGenerator(mergedDataSet, catalogVersions);
            typeDecls.Add(databaseCatalogTypeGenerator.Generate());

            foreach (DataTable dataTable in databaseCatalogTypeGenerator.MergeDataSet.Tables)
            {
                Console.WriteLine("Generating type {0}...", dataTable.TableName);
                CatalogTableTypeGenerator catalogTypeGenerator = new CatalogTableTypeGenerator(dataTable);
                CodeTypeDeclaration catalogTypeDecl = catalogTypeGenerator.Generate();
                typeDecls.Add(catalogTypeDecl);
            }

            Console.WriteLine("Writing code files...");
            foreach (CodeTypeDeclaration typeDecl in typeDecls)
            {
                CodeNamespace namespaceDecl = new CodeNamespace("Microsoft.SqlServer.CatalogStore");
                namespaceDecl.Imports.Add(new CodeNamespaceImport("System"));
                namespaceDecl.Imports.Add(new CodeNamespaceImport("System.Data.SqlClient"));
                namespaceDecl.Imports.Add(new CodeNamespaceImport("Microsoft.Data.Sqlite"));
                namespaceDecl.Imports.Add(new CodeNamespaceImport("Microsoft.EntityFrameworkCore"));
                namespaceDecl.Types.Add(typeDecl);

                WriteToFile(namespaceDecl, outputFolder + Path.DirectorySeparatorChar + typeDecl.Name + ".g.cs");
            }
        }

        private static void WriteToFile(CodeNamespace codeNamespace, string filePath)
        {
            CodeCompileUnit compileUnit = new CodeCompileUnit();
            compileUnit.Namespaces.Add(codeNamespace);

            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CodeGeneratorOptions options = new CodeGeneratorOptions
            {
                BracingStyle = "C",
            };

            using (StreamWriter sourceWriter = new StreamWriter(filePath))
            {
                provider.GenerateCodeFromCompileUnit(compileUnit, sourceWriter, options);
            }
        }

        private DataSet MergeDataSet(List<CatalogVersion> catalogVersions)
        {
            DataSet mergedDataSet = catalogVersions.First(cv => cv.ReleaseName == "vNext").CatalogDataSet;
            mergedDataSet = mergedDataSet.Copy();

            foreach (DataSet dataSet in catalogVersions.Where(cv => cv.ReleaseName != "vNext").Select(cv => cv.CatalogDataSet))
            {
                foreach (DataTable dataTable in dataSet.Tables)
                {
                    if (!mergedDataSet.Tables.Contains(dataTable.TableName))
                    {
                        DataTable cloneTable = dataTable.Copy();
                        mergedDataSet.Tables.Add(cloneTable);
                    }
                    else
                    {
                        DataTable mergedTable = mergedDataSet.Tables[dataTable.TableName];
                        foreach (DataColumn dataColumn in dataTable.Columns)
                        {
                            if (!mergedTable.Columns.Contains(dataColumn.ColumnName))
                            {
                                DataColumn cloneColumn = new DataColumn
                                {
                                    ColumnName = dataColumn.ColumnName,
                                    AllowDBNull = dataColumn.AllowDBNull,
                                    AutoIncrement = dataColumn.AutoIncrement,
                                    AutoIncrementSeed = dataColumn.AutoIncrementSeed,
                                    AutoIncrementStep = dataColumn.AutoIncrementStep,
                                    Caption = dataColumn.Caption,
                                    ColumnMapping = dataColumn.ColumnMapping,
                                    DataType = dataColumn.DataType,
                                    DateTimeMode = dataColumn.DateTimeMode,
                                    DefaultValue = dataColumn.DefaultValue,
                                    Expression = dataColumn.Expression,
                                    MaxLength = dataColumn.MaxLength,
                                    Namespace = dataColumn.Namespace,
                                    Prefix = dataColumn.Prefix,
                                    ReadOnly = dataColumn.ReadOnly,
                                    Unique = dataColumn.Unique,
                                };

                                mergedTable.Columns.Add(cloneColumn);
                            }
                        }

                    }

                }
            }
          
            return mergedDataSet;
        }
    }
}
