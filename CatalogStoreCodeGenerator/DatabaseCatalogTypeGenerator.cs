using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace CatalogStoreCodeGenerator
{
    public class DatabaseCatalogTypeGenerator
    {
        private DataSet _mergeDataSet;
        private List<CatalogVersion> _catalogVersions;

        // TODO: We don't map TIMESPAN column in SQL. Need to map this to an Int64 using the TimeSpan.Ticks property.

        private static Dictionary<Type, string> _sqLiteTypeMap = new Dictionary<Type, string>
        {
            { typeof(DateTime), "STRING" },     // using format mask "yyyy-MM-dd HH:mm:ss.FFFFFFFK"
            { typeof(string), "TEXT" },
            { typeof(bool), "NUMERIC" },
            { typeof(byte), "INTEGER" },
            { typeof(int), "INTEGER" },
            { typeof(long), "INTEGER" },
            { typeof(short), "INTEGER" },
            { typeof(float), "REAL" },
            { typeof(double), "REAL" },
            { typeof(decimal), "REAL" },
            { typeof(byte[]), "BLOB" },
            { typeof(Guid), "BLOB" },
            { typeof(object), "BLOB" },
        };

        // sql_variants are mapped to DbType.Object
        // 
        private static Dictionary<Type, DbType> _dbTypeMap = new Dictionary<Type, DbType>
        {
            { typeof(DateTime), DbType.DateTime },
            { typeof(string), DbType.String },
            { typeof(bool), DbType.Boolean },
            { typeof(byte), DbType.Byte },
            { typeof(int), DbType.Int32 },
            { typeof(long), DbType.Int64 },
            { typeof(short), DbType.Int16 },
            { typeof(float), DbType.Single },
            { typeof(double), DbType.Double },
            { typeof(decimal), DbType.Decimal },
            { typeof(byte[]), DbType.Binary },
            { typeof(Guid), DbType.Guid },
            { typeof(object), DbType.Object },
        };

        public DatabaseCatalogTypeGenerator(DataSet mergedDataSet, List<CatalogVersion> catalogVersions)
        {
            _mergeDataSet = mergedDataSet;
            _catalogVersions = catalogVersions;
        }

        public DataSet MergeDataSet { get { return _mergeDataSet; } }

        public CodeTypeDeclaration Generate()
        {
            CodeTypeDeclaration typeDecl = new CodeTypeDeclaration("DatabaseCatalogContext");
            typeDecl.IsClass = true;
            typeDecl.IsPartial = true;
            typeDecl.BaseTypes.Add(typeof(DbContext));
            typeDecl.BaseTypes.Add(typeof(IDisposable));

            CodeMemberField fieldConnetionId = new CodeMemberField(typeof(string), "_connectionId");
            fieldConnetionId.Attributes = MemberAttributes.Private;
            fieldConnetionId.InitExpression = new CodeMethodInvokeExpression(
                    new CodeMethodInvokeExpression(
                        new CodeTypeReferenceExpression(typeof(Guid)),
                        "NewGuid"),
                    "ToString",
                    new CodePrimitiveExpression("N"));
            typeDecl.Members.Add(fieldConnetionId);

            CodeMemberField fieldConnetionStringDecl = new CodeMemberField(typeof(string), "_connectionString");
            fieldConnetionStringDecl.Attributes = MemberAttributes.Private;
            typeDecl.Members.Add(fieldConnetionStringDecl);

            CodeMemberField fieldSqliteConnectionDecl = new CodeMemberField(typeof(SqliteConnection), "_sqliteConnection");
            fieldSqliteConnectionDecl.Attributes = MemberAttributes.Private;
            typeDecl.Members.Add(fieldSqliteConnectionDecl);

            foreach (DataTable dataTable in this.MergeDataSet.Tables)
            {
                typeDecl.Members.Add(GenerateCatalogTableField(dataTable));
            }

            foreach (DataTable dataTable in this.MergeDataSet.Tables)
            {
                typeDecl.Members.Add(GenerateCatalogTableProperty(dataTable));
            }

            typeDecl.Members.Add(this.GenerateOnConfiguringMethod());
            typeDecl.Members.Add(this.GenerateInitializeSQLiteMethod());

            foreach (CatalogVersion catalogVersion in _catalogVersions)
            {
                typeDecl.Members.Add(this.GenerateLoadMethod(catalogVersion));
            }

            typeDecl.Members.Add(new CodeMemberField {
                Name = "SetupSqliteFunctions()",
                Attributes = MemberAttributes.ScopeMask,
                Type = new CodeTypeReference("partial void"),
            });

            typeDecl.Members.Add(this.GenerateDisposeMethod());

            return typeDecl;
        }

        private CodeMemberMethod GenerateOnConfiguringMethod()
        {
            CodeMemberMethod methodDecl = new CodeMemberMethod();
            methodDecl.Name = "OnConfiguring";
            methodDecl.Attributes = MemberAttributes.Family | MemberAttributes.Override;

            methodDecl.Parameters.Add(
                new CodeParameterDeclarationExpression(
                    typeof(DbContextOptionsBuilder),
                    "optionsBuilder"));

            methodDecl.Statements.Add(
                new CodeMethodInvokeExpression(
                    new CodeVariableReferenceExpression("optionsBuilder"),
                    "UseSqlite",
                    new CodeFieldReferenceExpression(
                        new CodeThisReferenceExpression(),
                        "_connectionString")));

            return methodDecl;
        }

        private CodeMemberMethod GenerateInitializeSQLiteMethod()
        {
            CodeMemberMethod methodDecl = new CodeMemberMethod();
            methodDecl.Name = "InitializeSQLite";
            methodDecl.Attributes = MemberAttributes.Private | MemberAttributes.Final;

            methodDecl.Statements.Add(
                new CodeVariableDeclarationStatement(
                    typeof(SqliteCommand),
                    "command",
                    new CodePrimitiveExpression(null)));

            CodeTryCatchFinallyStatement tcfStatement = new CodeTryCatchFinallyStatement();

            tcfStatement.TryStatements.Add(
                new CodeAssignStatement(
                    new CodeVariableReferenceExpression("command"),
                    new CodeMethodInvokeExpression(
                        new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "_sqliteConnection"),
                        "CreateCommand")));

            foreach (DataTable dataTable in this.MergeDataSet.Tables)
            {
                tcfStatement.TryStatements.Add(new CodeSnippetStatement());

                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("create table {0} (", dataTable.TableName.Replace(".", "_"));

                List<string> columns = new List<string>();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    Type dataRowType = (Type) dataRow["DataType"];
                    string columnType = null;
                    if (!_sqLiteTypeMap.TryGetValue(dataRowType, out columnType))
                    {
                        throw new NotSupportedException(string.Format("Data row type {0} is not mapped to a SQLite column type.", dataRowType.FullName));
                    }

                    columns.Add(string.Format("{0} {1}", dataRow["ColumnName"], columnType));
                }

                sb.Append(string.Join(",", columns));
                sb.Append(")");

                tcfStatement.TryStatements.Add(
                    new CodeAssignStatement(
                        new CodePropertyReferenceExpression(new CodeVariableReferenceExpression("command"), "CommandText"),
                        new CodePrimitiveExpression(sb.ToString())));

                tcfStatement.TryStatements.Add(
                    new CodeMethodInvokeExpression(
                        new CodeVariableReferenceExpression("command"),
                        "ExecuteNonQuery"));
            }

            tcfStatement.FinallyStatements.Add(
                new CodeConditionStatement(
                    new CodeBinaryOperatorExpression(
                        new CodeVariableReferenceExpression("command"),
                        CodeBinaryOperatorType.IdentityInequality,
                        new CodePrimitiveExpression(null)),
                    new CodeExpressionStatement(
                        new CodeMethodInvokeExpression(
                            new CodeVariableReferenceExpression("command"),
                            "Dispose"))));

            methodDecl.Statements.Add(tcfStatement);

            return methodDecl;
        }

        private CodeMemberMethod GenerateLoadMethod(CatalogVersion catalogVersion)
        {
            CodeMemberMethod methodDecl = new CodeMemberMethod();
            methodDecl.Name = "Load" + catalogVersion.ReleaseName;
            methodDecl.Attributes = MemberAttributes.Private | MemberAttributes.Final;
            methodDecl.Parameters.Add(new CodeParameterDeclarationExpression(typeof(SqlConnection), "connection"));

            methodDecl.Statements.Add(
                new CodeVariableDeclarationStatement(
                    typeof(SqlCommand),
                    "command",
                    new CodePrimitiveExpression(null)));

            methodDecl.Statements.Add(
                new CodeVariableDeclarationStatement(
                    typeof(SqlDataReader),
                    "reader",
                    new CodePrimitiveExpression(null)));

            CodeTryCatchFinallyStatement tcfStatement = new CodeTryCatchFinallyStatement();

            string sql = string.Join(";", catalogVersion.CatalogDataSet.Tables.OfType<DataTable>().Select(dt => "select * from " + dt.TableName));

            tcfStatement.TryStatements.Add(
                new CodeAssignStatement(
                    new CodeVariableReferenceExpression("command"),
                    new CodeObjectCreateExpression(typeof(SqlCommand), new CodePrimitiveExpression(sql))));

            tcfStatement.TryStatements.Add(
                new CodeAssignStatement(
                    new CodePropertyReferenceExpression(new CodeVariableReferenceExpression("command"), "Connection"),
                    new CodeVariableReferenceExpression("connection")));

            tcfStatement.TryStatements.Add(
                new CodeAssignStatement(
                    new CodeVariableReferenceExpression("reader"),
                    new CodeMethodInvokeExpression(
                        new CodeVariableReferenceExpression("command"),
                        "ExecuteReader")));

            foreach (DataTable dataTable in catalogVersion.CatalogDataSet.Tables)
            {
                CodeConditionStatement hasRowsConditionStatement = new CodeConditionStatement();
                hasRowsConditionStatement.Condition = new CodePropertyReferenceExpression(new CodeVariableReferenceExpression("reader"), "HasRows");

                hasRowsConditionStatement.TrueStatements.Add(
                    new CodeVariableDeclarationStatement(
                        typeof(object[]),
                        "values",
                        new CodeArrayCreateExpression(
                            typeof(object),
                            new CodePropertyReferenceExpression(new CodeVariableReferenceExpression("reader"), "FieldCount"))));

                hasRowsConditionStatement.TrueStatements.Add(
                    new CodeVariableDeclarationStatement(
                        typeof(SqliteTransaction),
                        "sqliteTransaction",
                        new CodeMethodInvokeExpression(
                            new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "_sqliteConnection"),
                            "BeginTransaction")));

                hasRowsConditionStatement.TrueStatements.Add(
                    new CodeVariableDeclarationStatement(
                        typeof(SqliteCommand),
                        "sqliteCommand", 
                        new CodeMethodInvokeExpression(
                            new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "_sqliteConnection"),
                            "CreateCommand")));

                List<string> insertColumns = new List<string>();
                List<string> columns = new List<string>();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    insertColumns.Add((string)dataRow["ColumnName"]);
                    columns.Add("@" + CatalogTableTypeGenerator.GetVariableName(dataRow));
                }

                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("insert into {0} ({1}) values (", dataTable.TableName.Replace(".", "_"), string.Join(",", insertColumns));

                sb.Append(string.Join(", ", columns));
                sb.Append(")");

                hasRowsConditionStatement.TrueStatements.Add(
                    new CodeAssignStatement(
                        new CodePropertyReferenceExpression(
                            new CodeVariableReferenceExpression("sqliteCommand"),
                            "CommandText"),
                        new CodePrimitiveExpression(sb.ToString())));

                hasRowsConditionStatement.TrueStatements.Add(new CodeSnippetStatement());

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    Type dataRowType = (Type) dataRow["DataType"];
                    DbType dbType;
                    if (!_dbTypeMap.TryGetValue(dataRowType, out dbType))
                    {
                        throw new NotSupportedException(string.Format("Data row type {0} is not mapped to a System.Data.DbType.", dataRowType.FullName));
                    }

                    hasRowsConditionStatement.TrueStatements.Add(
                        new CodeVariableDeclarationStatement(
                            typeof(SqliteParameter),
                            CatalogTableTypeGenerator.GetVariableName(dataRow),
                            new CodeObjectCreateExpression(
                                typeof(SqliteParameter),
                                new CodePrimitiveExpression("@" + CatalogTableTypeGenerator.GetVariableName(dataRow)),
                                new CodeFieldReferenceExpression(
                                    new CodeTypeReferenceExpression(typeof(DbType)),
                                    dbType.ToString()))));

                    hasRowsConditionStatement.TrueStatements.Add(
                        new CodeMethodInvokeExpression(
                            new CodePropertyReferenceExpression(
                               new CodeVariableReferenceExpression("sqliteCommand"),
                               "Parameters"),
                            "Add",
                            new CodeVariableReferenceExpression(CatalogTableTypeGenerator.GetVariableName(dataRow))));
                }

                hasRowsConditionStatement.TrueStatements.Add(
                    new CodeMethodInvokeExpression(
                        new CodeVariableReferenceExpression("sqliteCommand"),
                        "Prepare"));

                hasRowsConditionStatement.TrueStatements.Add(new CodeSnippetStatement());

                CodeIterationStatement readLoopStmt = new CodeIterationStatement(
                    new CodeSnippetStatement(),
                    new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("reader"), "Read"),
                    new CodeSnippetStatement());

                readLoopStmt.Statements.Add(
                    new CodeMethodInvokeExpression(
                        new CodeVariableReferenceExpression("reader"),
                        "GetValues",
                        new CodeVariableReferenceExpression("values")));

                readLoopStmt.Statements.Add(new CodeSnippetStatement());

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    readLoopStmt.Statements.Add(
                    new CodeAssignStatement(
                        new CodePropertyReferenceExpression(
                            new CodeVariableReferenceExpression(CatalogTableTypeGenerator.GetVariableName(dataRow)),
                            "Value"),
                        new CodeArrayIndexerExpression(
                            new CodeVariableReferenceExpression("values"),
                            new CodePrimitiveExpression(dataRow["ColumnOrdinal"]))));
                }

                readLoopStmt.Statements.Add(new CodeSnippetStatement());

                readLoopStmt.Statements.Add(
                    new CodeMethodInvokeExpression(
                        new CodeVariableReferenceExpression("sqliteCommand"), "ExecuteNonQuery"));

                hasRowsConditionStatement.TrueStatements.Add(readLoopStmt);

                hasRowsConditionStatement.TrueStatements.Add(
                    new CodeMethodInvokeExpression(
                        new CodeVariableReferenceExpression("sqliteCommand"), "Dispose"));

                hasRowsConditionStatement.TrueStatements.Add(
                    new CodeMethodInvokeExpression(
                        new CodeVariableReferenceExpression("sqliteTransaction"), "Commit"));

                tcfStatement.TryStatements.Add(hasRowsConditionStatement);
                tcfStatement.TryStatements.Add(new CodeSnippetStatement());

                tcfStatement.TryStatements.Add(
                    new CodeMethodInvokeExpression(
                        new CodeVariableReferenceExpression("reader"), "NextResult"));
            }

            tcfStatement.FinallyStatements.Add(
                new CodeConditionStatement(
                    new CodeBinaryOperatorExpression(
                        new CodeVariableReferenceExpression("reader"),
                        CodeBinaryOperatorType.IdentityInequality,
                        new CodePrimitiveExpression(null)),
                    new CodeExpressionStatement(
                        new CodeMethodInvokeExpression(
                            new CodeVariableReferenceExpression("reader"),
                            "Dispose"))));

            tcfStatement.FinallyStatements.Add(
                new CodeConditionStatement(
                    new CodeBinaryOperatorExpression(
                        new CodeVariableReferenceExpression("command"),
                        CodeBinaryOperatorType.IdentityInequality,
                        new CodePrimitiveExpression(null)),
                    new CodeExpressionStatement(
                        new CodeMethodInvokeExpression(
                            new CodeVariableReferenceExpression("command"),
                            "Dispose"))));

            methodDecl.Statements.Add(tcfStatement);

            methodDecl.Statements.Add(
                new CodeMethodInvokeExpression(
                    new CodeThisReferenceExpression(),
                    "SetupSqliteFunctions"));

            return methodDecl;
        }

        private CodeMemberMethod GenerateDisposeMethod()
        {
            CodeMemberMethod methodDecl = new CodeMemberMethod();
            methodDecl.Name = "Dispose";
            methodDecl.Attributes = MemberAttributes.Public | MemberAttributes.Override;

            methodDecl.Statements.Add(
                new CodeMethodInvokeExpression(
                    new CodeBaseReferenceExpression(),
                    "Dispose"));

            methodDecl.Statements.Add(
                new CodeConditionStatement(
                    new CodeBinaryOperatorExpression(
                        new CodeVariableReferenceExpression("_sqliteConnection"),
                        CodeBinaryOperatorType.IdentityInequality,
                        new CodePrimitiveExpression(null)),
                    new CodeExpressionStatement(
                        new CodeMethodInvokeExpression(
                            new CodeVariableReferenceExpression("_sqliteConnection"),
                            "Dispose")),
                    new CodeAssignStatement(
                        new CodeVariableReferenceExpression("_sqliteConnection"), 
                        new CodePrimitiveExpression(null))));

            return methodDecl;
        }

        private static CodeMemberField GenerateCatalogTableField(DataTable dataTable)
        {
            CodeMemberField fieldDecl = new CodeMemberField();
            fieldDecl.Attributes = MemberAttributes.Private;
            fieldDecl.Name = GetCatalogTableFieldName(dataTable);
            fieldDecl.Type = new CodeTypeReference(typeof(DbSet<>));
            fieldDecl.Type.TypeArguments.Add(new CodeTypeReference(CatalogTableTypeGenerator.GetTypeName(dataTable)));

            return fieldDecl;
        }

        private static CodeMemberProperty GenerateCatalogTableProperty(DataTable dataTable)
        {
            CodeMemberProperty propDecl = new CodeMemberProperty();
            propDecl.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            propDecl.Name = GetCatalogTablePropertyName(dataTable);
            propDecl.HasGet = true;
            propDecl.HasSet = true;

            propDecl.Type = new CodeTypeReference(typeof(DbSet<>));
            propDecl.Type.TypeArguments.Add(new CodeTypeReference(CatalogTableTypeGenerator.GetTypeName(dataTable)));

            propDecl.GetStatements.Add(
                new CodeMethodReturnStatement(
                    new CodeFieldReferenceExpression(
                        new CodeThisReferenceExpression(), GetCatalogTableFieldName(dataTable))));

            propDecl.SetStatements.Add(
                new CodeAssignStatement(
                    new CodeFieldReferenceExpression(
                        new CodeThisReferenceExpression(),
                        GetCatalogTableFieldName(dataTable)),
                    new CodePropertySetValueReferenceExpression()));

            return propDecl;
        }

        public static string GetCatalogTableFieldName(DataTable dataTable)
        {
            return "_" + Namer.GetCamelCaseSingularPlural(dataTable.TableName);
        }

        public static string GetCatalogTablePropertyName(DataTable dataTable)
        {
            return Namer.GetPascalCasePlural(dataTable.TableName);
        }
    }
}
