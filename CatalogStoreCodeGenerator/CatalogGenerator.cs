using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogStoreCodeGenerator
{
    public class CatalogGenerator
    {  
        public CodeNamespace Generate(List<CatalogVersion> catalogVersions)
        {
            foreach (CatalogVersion catalogVersion in catalogVersions)
            {
                catalogVersion.LoadCatalogDataSetFromXml();
            }

            DataSet mergedDataSet = this.MergeDataSet(catalogVersions);

            CodeNamespace namespaceDecl = new CodeNamespace("Microsoft.SqlServer.CatalogStore");

            namespaceDecl.Imports.Add(new CodeNamespaceImport("System"));
            namespaceDecl.Imports.Add(new CodeNamespaceImport("System.Data.SqlClient"));
            namespaceDecl.Imports.Add(new CodeNamespaceImport("Microsoft.Data.Sqlite"));
            namespaceDecl.Imports.Add(new CodeNamespaceImport("Microsoft.EntityFrameworkCore"));

            DatabaseCatalogTypeGenerator databaseCatalogTypeGenerator = new DatabaseCatalogTypeGenerator(mergedDataSet, catalogVersions);
            namespaceDecl.Types.Add(databaseCatalogTypeGenerator.Generate());

            foreach (DataTable dataTable in databaseCatalogTypeGenerator.MergeDataSet.Tables)
            {
                Console.WriteLine("Generating type {0}...", dataTable.TableName);
                CatalogTableTypeGenerator catalogTypeGenerator = new CatalogTableTypeGenerator(dataTable);
                CodeTypeDeclaration catalogTypeDecl = catalogTypeGenerator.Generate();
                namespaceDecl.Types.Add(catalogTypeDecl);
            }

            return namespaceDecl;
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
