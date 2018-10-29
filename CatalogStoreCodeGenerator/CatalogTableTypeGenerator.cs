using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
    public class CatalogTableTypeGenerator
    {
        private DataTable _dataTable;

        /// <summary>
        /// Explicit key maps for more interesting keys.
        /// </summary>
        private Dictionary<string, string[]> _customKeyMap = new Dictionary<string, string[]>()
        {
            { "sys.change_tracking_databases", new[] { "database_id" } },
            { "sys.crypt_properties", new[] { "clasa", "major_id" } },
            { "sys.column_encryption_key_values", new[] { "column_encryption_key_id"} },
            { "sys.database_audit_specification_details", new[] { "database_specification_id" } },
            { "sys.database_mirroring", new[] { "database_id" } },
            { "sys.database_permissions", new[] { "class", "major_id", "minor_id" } },
            { "sys.database_role_members", new[] { "role_principal_id", "member_principal_id" } },
            { "sys.database_query_store_options", new[] { "desired_state" } },
            { "sys.destination_data_spaces", new[] { "partition_scheme_id", "destination_id", "data_space_id" } },
            { "sys.foreign_key_columns", new[] { "constraint_object_id" } },
            { "sys.partition_parameters", new[] { "function_id", "parameter_id" } },
            { "sys.partition_range_values", new[] { "function_id", "boundary_id", "parameter_id" } },
            { "sys.service_contract_message_usages", new[] { "service_contract_id", "message_type_id" } },
            { "sys.sql_expression_dependencies", new[] { "referencing_id", "referencing_minor_id", "referenced_id", "referenced_minor_id" } },
            { "sys.allocation_units", new[] { "allocation_unit_id" } },
            { "sys.assembly_references", new[] { "assembly_id", "referenced_assembly_id" } },
            { "sys.availability_databases_cluster", new[] { "group_id", "group_database_id" } },
            { "sys.availability_group_listener_ip_addresses", new[] { "listener_id", "ip_address", "ip_subnet_mask" } },
            { "sys.availability_group_listeners", new[] { "group_id", "listener_id" } },
            { "sys.availability_read_only_routing_lists", new[] { "replica_id", "routing_priority" } },
            { "sys.availability_replicas", new[] { "replica_id", "group_id" } },
            { "sys.column_store_dictionaries", new[] { "hobt_id", "column_id", "dictionary_id" } },
            { "sys.column_store_segments", new[] { "partition_id", "hobt_id", "column_id", "segment_id" } },
            { "sys.conversation_endpoints", new[] { "conversation_handle" } },
            { "sys.conversation_groups", new[] { "conversation_group_id" } },
            { "sys.database_automatic_tuning_mode", new[] { "desired_state" } },
            { "sys.database_filestream_options", new[] { "database_id" } },
            { "sys.database_mirroring_witnesses", new[] { "database_name", "principal_server_name", "mirror_server_name" } },
            { "sys.database_recovery_status", new[] { "database_id" } },
            { "sys.endpoint_webmethods", new[] { "endpoint_id", "namespace" } },
            { "sys.event_notification_event_types", new[] { "type", "type_name" } },
            { "sys.external_library_files", new[] { "external_library_id" } },
            { "sys.external_library_setup_errors", new[] { "db_id", "principal_id", "external_library_id" } },
            { "sys.fulltext_document_types", new[] { "document_type", "class_id" } },
            { "sys.fulltext_index_fragments", new[] { "table_id", "fragment_object_id", "fragment_id" } },
            { "sys.fulltext_semantic_language_statistics_database", new[] { "database_id" } },
            { "sys.fulltext_stopwords", new[] { "stoplist_id" } },
            { "sys.fulltext_system_stopwords", new[] { "stopword", "language_id" } },
            { "sys.key_encryptions", new[] { "key_id" } },
            { "sys.linked_logins", new[] { "server_id" } },
            { "sys.master_key_passwords", new[] { "credential_id" } },
            { "sys.message_type_xml_schema_collection_usages", new[] { "message_type_id", "xml_collection_id" } },
            { "sys.messages", new[] { "message_id", "language_id" } },
            { "sys.openkeys", new[] { "database_id", "key_id" } },
            { "sys.registered_search_properties", new[] { "property_list_id", "property_set_guid", "property_int_id" } },
            { "sys.remote_data_archive_databases", new[] { "remote_database_id" } },
            { "sys.remote_logins", new[] { "server_id" } },
            { "sys.securable_classes", new[] { "class" } },
            { "sys.sensitivity_classifications", new[] { "class", "major_id", "minor_id" } },
            { "sys.server_permissions", new[] { "class", "major_id", "minor_id", "grantee_principal_id" } },
            { "sys.server_principal_credentials", new[] { "principal_id", "credential_id" } },
            { "sys.server_role_members", new[] { "role_principal_id", "member_principal_id" } },
            { "sys.service_contract_usages", new[] { "service_id", "service_contract_id" } },
            { "sys.service_queue_usages", new[] { "service_id", "service_queue_id" } },
            { "sys.spatial_reference_systems", new[] { "spatial_reference_id" } },
            { "sys.syscomments", new[] { "id", "number", "colid" } },
            { "sys.sysconfigures", new[] { "config" } },
            { "sys.sysconstraints", new[] { "constid" } },
            { "sys.syscscontainers", new[] { "blob_container_id" } },
            { "sys.syscurconfigs", new[] { "config" } },
            { "sys.sysfilegroups", new[] { "groupid" } },
            { "sys.sysforeignkeys", new[] { "constid" } },
            { "sys.sysindexkeys", new[] { "id", "indid", "colid", "keyno" } },
            { "sys.sysmembers", new[] { "memberuid", "groupuid" } },
            { "sys.sysoledbusers", new[] { "rmtsrvid" } },
            { "sys.syspermissions", new[] { "id" } },
            { "sys.sysprotects", new[] { "id", "uid", "action" } },
            { "sys.sysreferences", new[] { "constid" } },
            { "sys.sysremotelogins", new[] { "remoteserverid" } },
            { "sys.sysservers", new[] { "srvid" } },
            { "sys.system_components_surface_area_configuration", new[] { "component_name", "database_name", "object_name" } },
            { "sys.trigger_event_types", new[] { "type" } },
            { "sys.trusted_assemblies", new[] { "hash" } },
            { "sys.type_assembly_usages", new[] { "user_type_id", "assembly_id" } },
            { "sys.xml_schema_component_placements", new[] { "xml_component_id", "placement_id" } },
            { "sys.xml_schema_facets", new[] { "xml_component_id", "facet_id" } },
            { "sys.xml_schema_wildcard_namespaces", new[] { "xml_component_id" } },
            { "sys.database_service_objectives", new[] { "database_id" } },
        };

        public CatalogTableTypeGenerator(DataTable dataTable)
        {
            _dataTable = dataTable;
            this.ResolveKeys();
        }

        private void ResolveKeys()
        {
            //
            // TODO: The following is a heuristic.  It has issues, but works for now.  We should have each table explicitely mapped to a key.
            //

            _dataTable.Columns["IsKey"].ReadOnly = false;

            string[] customKeys = null;
            if (_customKeyMap.TryGetValue(_dataTable.TableName, out customKeys))
            {
                foreach (DataRow dataRow in _dataTable.Rows)
                { 
                    if (customKeys.Contains((string)dataRow["ColumnName"]))
                    {
                        dataRow["IsKey"] = true;
                        return;
                    }
                }
            }

            foreach (DataRow dataRow in _dataTable.Rows)
            { 
                if ((string)dataRow["ColumnName"] == "object_id")
                {
                    dataRow["IsKey"] = true;
                    return;
                }
            }

            foreach (DataRow dataRow in _dataTable.Rows)
            { 
                if ((string)dataRow["ColumnName"] == "name")
                {
                    dataRow["IsKey"] = true;
                    return;
                }
            }


            List<string> columnNames = new List<string>();
            foreach (DataRow dataRow in _dataTable.Rows)
            {
                columnNames.Add((string) dataRow["ColumnName"]);
            }

            string message = string.Format("Table {0} does not have any keys mapped. Column list: {1}", _dataTable.TableName, string.Join(",", columnNames));
            Console.WriteLine(message);

//            throw NotImplementedException(message);
        }

        public CodeTypeDeclaration Generate()
        {
            CodeTypeDeclaration typeDecl = new CodeTypeDeclaration(GetTypeName(_dataTable));
            typeDecl.IsClass = true;
            typeDecl.IsPartial = true;

            typeDecl.CustomAttributes.Add(
                new CodeAttributeDeclaration(
                    new CodeTypeReference(typeof(TableAttribute)),
                    new CodeAttributeArgument(new CodePrimitiveExpression(_dataTable.TableName.Replace(".", "_")))));

            foreach (DataRow dataRow in _dataTable.Rows)
            {
                typeDecl.Members.Add(GenerateField(dataRow));
                typeDecl.Members.Add(GenerateProperty(dataRow));
            }

            return typeDecl;
        }

        private static bool IsNullable(DataRow dataRow)
        {
            Type type = (Type)dataRow["DataType"];
            return !(type == typeof(string) || type == typeof(byte[]) || type == typeof(object));  
        }

        private static CodeMemberField GenerateField(DataRow dataRow)
        {
            CodeMemberField fieldDecl = new CodeMemberField();
            if (!IsNullable(dataRow))
            {
                fieldDecl.Attributes = MemberAttributes.Private;
                fieldDecl.Name = GetFieldName(dataRow);
                fieldDecl.Type = GetClrType(dataRow);
            }
            else
            {
                fieldDecl.Attributes = MemberAttributes.Private;
                fieldDecl.Name = GetFieldName(dataRow);
                fieldDecl.Type = new CodeTypeReference(typeof(Nullable<>));
                fieldDecl.Type.TypeArguments.Add(GetClrType(dataRow));
            }

            return fieldDecl;
        }

        private CodeMemberProperty GenerateProperty(DataRow dataRow)
        {
            CodeMemberProperty propDecl = new CodeMemberProperty();
            propDecl.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            propDecl.Name = GetPropertyName(dataRow);

            Type type = (Type)dataRow["DataType"];
            if (type == typeof(object))    // sql_variants are mapped to an object, which EF can't handle
            {
                propDecl.CustomAttributes.Add(
                    new CodeAttributeDeclaration(
                        new CodeTypeReference(typeof(NotMappedAttribute))));
            }
            else
            {
                propDecl.CustomAttributes.Add(
                    new CodeAttributeDeclaration(
                        new CodeTypeReference(typeof(ColumnAttribute)),
                        new CodeAttributeArgument(new CodePrimitiveExpression(dataRow["ColumnName"]))));
            }

            if (dataRow["IsKey"] != DBNull.Value && (bool) dataRow["IsKey"])
            {
                propDecl.CustomAttributes.Add(
                    new CodeAttributeDeclaration(
                        new CodeTypeReference(typeof(KeyAttribute))));
            }

            if (!IsNullable(dataRow))
            {
                propDecl.Type = GetClrType(dataRow);
            }
            else
            {
                propDecl.Type = new CodeTypeReference(typeof(Nullable<>));
                propDecl.Type.TypeArguments.Add(GetClrType(dataRow));
            }

            propDecl.HasGet = true;

            propDecl.GetStatements.Add(
                new CodeMethodReturnStatement(
                    new CodeFieldReferenceExpression(
                        new CodeThisReferenceExpression(),
                        GetFieldName(dataRow))));


            propDecl.HasSet = true;
            propDecl.SetStatements.Add(
                new CodeAssignStatement(
                    new CodeFieldReferenceExpression(
                        new CodeThisReferenceExpression(),
                        GetFieldName(dataRow)),
                    new CodePropertySetValueReferenceExpression()));

            return propDecl;
        }

        public static string GetTypeName(DataTable dataTable)
        {
            return Namer.GetPascalCaseSingular(dataTable.TableName) + "Catalog";
        }

        public static string GetVariableName(DataTable dataTable)
        {
            return Namer.GetCamelCaseSingularPlural(dataTable.TableName);
        }

        public static CodeTypeReference GetClrType(DataRow dataRow)
        {
            return new CodeTypeReference((Type)dataRow["DataType"]);
        }

        public static string GetFieldName(DataRow dataRow)
        {
            return "_" + (string)dataRow["ColumnName"];
        }

        public static string GetVariableName(DataRow dataRow)
        {
            return Namer.GetCamelCaseSingularPlural((string)dataRow["ColumnName"]);
        }

        public static string GetPropertyName(DataRow dataRow)
        {
            return Namer.GetPascalCasePlural((string)dataRow["ColumnName"]);
        }
    }
}
