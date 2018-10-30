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
    
    
    [System.ComponentModel.DataAnnotations.Schema.TableAttribute("sys_columns")]
    public partial class SysColumnCatalog
    {
        
        private System.Nullable<int> _object_id;
        
        private string _name;
        
        private System.Nullable<int> _column_id;
        
        private System.Nullable<byte> _system_type_id;
        
        private System.Nullable<int> _user_type_id;
        
        private System.Nullable<short> _max_length;
        
        private System.Nullable<byte> _precision;
        
        private System.Nullable<byte> _scale;
        
        private string _collation_name;
        
        private System.Nullable<bool> _is_nullable;
        
        private System.Nullable<bool> _is_ansi_padded;
        
        private System.Nullable<bool> _is_rowguidcol;
        
        private System.Nullable<bool> _is_identity;
        
        private System.Nullable<bool> _is_computed;
        
        private System.Nullable<bool> _is_filestream;
        
        private System.Nullable<bool> _is_replicated;
        
        private System.Nullable<bool> _is_non_sql_subscribed;
        
        private System.Nullable<bool> _is_merge_published;
        
        private System.Nullable<bool> _is_dts_replicated;
        
        private System.Nullable<bool> _is_xml_document;
        
        private System.Nullable<int> _xml_collection_id;
        
        private System.Nullable<int> _default_object_id;
        
        private System.Nullable<int> _rule_object_id;
        
        private System.Nullable<bool> _is_sparse;
        
        private System.Nullable<bool> _is_column_set;
        
        private System.Nullable<byte> _generated_always_type;
        
        private string _generated_always_type_desc;
        
        private System.Nullable<int> _encryption_type;
        
        private string _encryption_type_desc;
        
        private string _encryption_algorithm_name;
        
        private System.Nullable<int> _column_encryption_key_id;
        
        private string _column_encryption_key_database_name;
        
        private System.Nullable<bool> _is_hidden;
        
        private System.Nullable<bool> _is_masked;
        
        private System.Nullable<int> _graph_type;
        
        private string _graph_type_desc;
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("object_id")]
        [System.ComponentModel.DataAnnotations.KeyAttribute()]
        public System.Nullable<int> ObjectId
        {
            get
            {
                return this._object_id;
            }
            set
            {
                this._object_id = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("name")]
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("column_id")]
        public System.Nullable<int> ColumnId
        {
            get
            {
                return this._column_id;
            }
            set
            {
                this._column_id = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("system_type_id")]
        public System.Nullable<byte> SystemTypeId
        {
            get
            {
                return this._system_type_id;
            }
            set
            {
                this._system_type_id = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("user_type_id")]
        public System.Nullable<int> UserTypeId
        {
            get
            {
                return this._user_type_id;
            }
            set
            {
                this._user_type_id = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("max_length")]
        public System.Nullable<short> MaxLength
        {
            get
            {
                return this._max_length;
            }
            set
            {
                this._max_length = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("precision")]
        public System.Nullable<byte> Precision
        {
            get
            {
                return this._precision;
            }
            set
            {
                this._precision = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("scale")]
        public System.Nullable<byte> Scale
        {
            get
            {
                return this._scale;
            }
            set
            {
                this._scale = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("collation_name")]
        public string CollationName
        {
            get
            {
                return this._collation_name;
            }
            set
            {
                this._collation_name = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_nullable")]
        public System.Nullable<bool> IsNullable
        {
            get
            {
                return this._is_nullable;
            }
            set
            {
                this._is_nullable = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_ansi_padded")]
        public System.Nullable<bool> IsAnsiPadded
        {
            get
            {
                return this._is_ansi_padded;
            }
            set
            {
                this._is_ansi_padded = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_rowguidcol")]
        public System.Nullable<bool> IsRowguidcol
        {
            get
            {
                return this._is_rowguidcol;
            }
            set
            {
                this._is_rowguidcol = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_identity")]
        public System.Nullable<bool> IsIdentity
        {
            get
            {
                return this._is_identity;
            }
            set
            {
                this._is_identity = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_computed")]
        public System.Nullable<bool> IsComputed
        {
            get
            {
                return this._is_computed;
            }
            set
            {
                this._is_computed = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_filestream")]
        public System.Nullable<bool> IsFilestream
        {
            get
            {
                return this._is_filestream;
            }
            set
            {
                this._is_filestream = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_replicated")]
        public System.Nullable<bool> IsReplicated
        {
            get
            {
                return this._is_replicated;
            }
            set
            {
                this._is_replicated = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_non_sql_subscribed")]
        public System.Nullable<bool> IsNonSqlSubscribed
        {
            get
            {
                return this._is_non_sql_subscribed;
            }
            set
            {
                this._is_non_sql_subscribed = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_merge_published")]
        public System.Nullable<bool> IsMergePublished
        {
            get
            {
                return this._is_merge_published;
            }
            set
            {
                this._is_merge_published = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_dts_replicated")]
        public System.Nullable<bool> IsDtsReplicated
        {
            get
            {
                return this._is_dts_replicated;
            }
            set
            {
                this._is_dts_replicated = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_xml_document")]
        public System.Nullable<bool> IsXmlDocument
        {
            get
            {
                return this._is_xml_document;
            }
            set
            {
                this._is_xml_document = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("xml_collection_id")]
        public System.Nullable<int> XmlCollectionId
        {
            get
            {
                return this._xml_collection_id;
            }
            set
            {
                this._xml_collection_id = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("default_object_id")]
        public System.Nullable<int> DefaultObjectId
        {
            get
            {
                return this._default_object_id;
            }
            set
            {
                this._default_object_id = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("rule_object_id")]
        public System.Nullable<int> RuleObjectId
        {
            get
            {
                return this._rule_object_id;
            }
            set
            {
                this._rule_object_id = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_sparse")]
        public System.Nullable<bool> IsSparse
        {
            get
            {
                return this._is_sparse;
            }
            set
            {
                this._is_sparse = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_column_set")]
        public System.Nullable<bool> IsColumnSet
        {
            get
            {
                return this._is_column_set;
            }
            set
            {
                this._is_column_set = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("generated_always_type")]
        public System.Nullable<byte> GeneratedAlwaysType
        {
            get
            {
                return this._generated_always_type;
            }
            set
            {
                this._generated_always_type = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("generated_always_type_desc")]
        public string GeneratedAlwaysTypeDesc
        {
            get
            {
                return this._generated_always_type_desc;
            }
            set
            {
                this._generated_always_type_desc = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("encryption_type")]
        public System.Nullable<int> EncryptionType
        {
            get
            {
                return this._encryption_type;
            }
            set
            {
                this._encryption_type = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("encryption_type_desc")]
        public string EncryptionTypeDesc
        {
            get
            {
                return this._encryption_type_desc;
            }
            set
            {
                this._encryption_type_desc = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("encryption_algorithm_name")]
        public string EncryptionAlgorithmName
        {
            get
            {
                return this._encryption_algorithm_name;
            }
            set
            {
                this._encryption_algorithm_name = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("column_encryption_key_id")]
        public System.Nullable<int> ColumnEncryptionKeyId
        {
            get
            {
                return this._column_encryption_key_id;
            }
            set
            {
                this._column_encryption_key_id = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("column_encryption_key_database_name")]
        public string ColumnEncryptionKeyDatabaseName
        {
            get
            {
                return this._column_encryption_key_database_name;
            }
            set
            {
                this._column_encryption_key_database_name = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_hidden")]
        public System.Nullable<bool> IsHidden
        {
            get
            {
                return this._is_hidden;
            }
            set
            {
                this._is_hidden = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_masked")]
        public System.Nullable<bool> IsMasked
        {
            get
            {
                return this._is_masked;
            }
            set
            {
                this._is_masked = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("graph_type")]
        public System.Nullable<int> GraphType
        {
            get
            {
                return this._graph_type;
            }
            set
            {
                this._graph_type = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("graph_type_desc")]
        public string GraphTypeDesc
        {
            get
            {
                return this._graph_type_desc;
            }
            set
            {
                this._graph_type_desc = value;
            }
        }
    }
}