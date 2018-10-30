//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Microsoft.SqlServer.CatalogStore
{
    using System;
    using System.Data.SqlClient;
    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;
    
    
    [System.ComponentModel.DataAnnotations.Schema.TableAttribute("sys_sql_expression_dependencies")]
    public partial class SysSqlExpressionDependencyCatalog
    {
        
        private System.Nullable<int> _referencing_id;
        
        private System.Nullable<int> _referencing_minor_id;
        
        private System.Nullable<byte> _referencing_class;
        
        private string _referencing_class_desc;
        
        private System.Nullable<bool> _is_schema_bound_reference;
        
        private System.Nullable<byte> _referenced_class;
        
        private string _referenced_class_desc;
        
        private string _referenced_server_name;
        
        private string _referenced_database_name;
        
        private string _referenced_schema_name;
        
        private string _referenced_entity_name;
        
        private System.Nullable<int> _referenced_id;
        
        private System.Nullable<int> _referenced_minor_id;
        
        private System.Nullable<bool> _is_caller_dependent;
        
        private System.Nullable<bool> _is_ambiguous;
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("referencing_id")]
        [System.ComponentModel.DataAnnotations.KeyAttribute()]
        public System.Nullable<int> ReferencingId
        {
            get
            {
                return this._referencing_id;
            }
            set
            {
                this._referencing_id = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("referencing_minor_id")]
        public System.Nullable<int> ReferencingMinorId
        {
            get
            {
                return this._referencing_minor_id;
            }
            set
            {
                this._referencing_minor_id = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("referencing_class")]
        public System.Nullable<byte> ReferencingClass
        {
            get
            {
                return this._referencing_class;
            }
            set
            {
                this._referencing_class = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("referencing_class_desc")]
        public string ReferencingClassDesc
        {
            get
            {
                return this._referencing_class_desc;
            }
            set
            {
                this._referencing_class_desc = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_schema_bound_reference")]
        public System.Nullable<bool> IsSchemaBoundReference
        {
            get
            {
                return this._is_schema_bound_reference;
            }
            set
            {
                this._is_schema_bound_reference = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("referenced_class")]
        public System.Nullable<byte> ReferencedClass
        {
            get
            {
                return this._referenced_class;
            }
            set
            {
                this._referenced_class = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("referenced_class_desc")]
        public string ReferencedClassDesc
        {
            get
            {
                return this._referenced_class_desc;
            }
            set
            {
                this._referenced_class_desc = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("referenced_server_name")]
        public string ReferencedServerName
        {
            get
            {
                return this._referenced_server_name;
            }
            set
            {
                this._referenced_server_name = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("referenced_database_name")]
        public string ReferencedDatabaseName
        {
            get
            {
                return this._referenced_database_name;
            }
            set
            {
                this._referenced_database_name = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("referenced_schema_name")]
        public string ReferencedSchemaName
        {
            get
            {
                return this._referenced_schema_name;
            }
            set
            {
                this._referenced_schema_name = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("referenced_entity_name")]
        public string ReferencedEntityName
        {
            get
            {
                return this._referenced_entity_name;
            }
            set
            {
                this._referenced_entity_name = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("referenced_id")]
        public System.Nullable<int> ReferencedId
        {
            get
            {
                return this._referenced_id;
            }
            set
            {
                this._referenced_id = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("referenced_minor_id")]
        public System.Nullable<int> ReferencedMinorId
        {
            get
            {
                return this._referenced_minor_id;
            }
            set
            {
                this._referenced_minor_id = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_caller_dependent")]
        public System.Nullable<bool> IsCallerDependent
        {
            get
            {
                return this._is_caller_dependent;
            }
            set
            {
                this._is_caller_dependent = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_ambiguous")]
        public System.Nullable<bool> IsAmbiguous
        {
            get
            {
                return this._is_ambiguous;
            }
            set
            {
                this._is_ambiguous = value;
            }
        }
    }
}