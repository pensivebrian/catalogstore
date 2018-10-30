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
    
    
    [System.ComponentModel.DataAnnotations.Schema.TableAttribute("sys_external_tables")]
    public partial class SysExternalTableCatalog
    {
        
        private string _name;
        
        private System.Nullable<int> _object_id;
        
        private System.Nullable<int> _principal_id;
        
        private System.Nullable<int> _schema_id;
        
        private System.Nullable<int> _parent_object_id;
        
        private string _type;
        
        private string _type_desc;
        
        private System.Nullable<System.DateTime> _create_date;
        
        private System.Nullable<System.DateTime> _modify_date;
        
        private System.Nullable<bool> _is_ms_shipped;
        
        private System.Nullable<bool> _is_published;
        
        private System.Nullable<bool> _is_schema_published;
        
        private System.Nullable<int> _max_column_id_used;
        
        private System.Nullable<bool> _uses_ansi_nulls;
        
        private System.Nullable<int> _data_source_id;
        
        private System.Nullable<int> _file_format_id;
        
        private string _location;
        
        private string _reject_type;
        
        private System.Nullable<double> _reject_value;
        
        private System.Nullable<double> _reject_sample_value;
        
        private System.Nullable<byte> _distribution_type;
        
        private string _distribution_desc;
        
        private System.Nullable<int> _sharding_col_id;
        
        private string _remote_schema_name;
        
        private string _remote_object_name;
        
        private string _rejected_row_location;
        
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
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("principal_id")]
        public System.Nullable<int> PrincipalId
        {
            get
            {
                return this._principal_id;
            }
            set
            {
                this._principal_id = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("schema_id")]
        public System.Nullable<int> SchemaId
        {
            get
            {
                return this._schema_id;
            }
            set
            {
                this._schema_id = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("parent_object_id")]
        public System.Nullable<int> ParentObjectId
        {
            get
            {
                return this._parent_object_id;
            }
            set
            {
                this._parent_object_id = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("type")]
        public string Type
        {
            get
            {
                return this._type;
            }
            set
            {
                this._type = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("type_desc")]
        public string TypeDesc
        {
            get
            {
                return this._type_desc;
            }
            set
            {
                this._type_desc = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("create_date")]
        public System.Nullable<System.DateTime> CreateDate
        {
            get
            {
                return this._create_date;
            }
            set
            {
                this._create_date = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("modify_date")]
        public System.Nullable<System.DateTime> ModifyDate
        {
            get
            {
                return this._modify_date;
            }
            set
            {
                this._modify_date = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_ms_shipped")]
        public System.Nullable<bool> IsMsShipped
        {
            get
            {
                return this._is_ms_shipped;
            }
            set
            {
                this._is_ms_shipped = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_published")]
        public System.Nullable<bool> IsPublished
        {
            get
            {
                return this._is_published;
            }
            set
            {
                this._is_published = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_schema_published")]
        public System.Nullable<bool> IsSchemaPublished
        {
            get
            {
                return this._is_schema_published;
            }
            set
            {
                this._is_schema_published = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("max_column_id_used")]
        public System.Nullable<int> MaxColumnIdUsed
        {
            get
            {
                return this._max_column_id_used;
            }
            set
            {
                this._max_column_id_used = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("uses_ansi_nulls")]
        public System.Nullable<bool> UsesAnsiNulls
        {
            get
            {
                return this._uses_ansi_nulls;
            }
            set
            {
                this._uses_ansi_nulls = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("data_source_id")]
        public System.Nullable<int> DataSourceId
        {
            get
            {
                return this._data_source_id;
            }
            set
            {
                this._data_source_id = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("file_format_id")]
        public System.Nullable<int> FileFormatId
        {
            get
            {
                return this._file_format_id;
            }
            set
            {
                this._file_format_id = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("location")]
        public string Location
        {
            get
            {
                return this._location;
            }
            set
            {
                this._location = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("reject_type")]
        public string RejectType
        {
            get
            {
                return this._reject_type;
            }
            set
            {
                this._reject_type = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("reject_value")]
        public System.Nullable<double> RejectValue
        {
            get
            {
                return this._reject_value;
            }
            set
            {
                this._reject_value = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("reject_sample_value")]
        public System.Nullable<double> RejectSampleValue
        {
            get
            {
                return this._reject_sample_value;
            }
            set
            {
                this._reject_sample_value = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("distribution_type")]
        public System.Nullable<byte> DistributionType
        {
            get
            {
                return this._distribution_type;
            }
            set
            {
                this._distribution_type = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("distribution_desc")]
        public string DistributionDesc
        {
            get
            {
                return this._distribution_desc;
            }
            set
            {
                this._distribution_desc = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("sharding_col_id")]
        public System.Nullable<int> ShardingColId
        {
            get
            {
                return this._sharding_col_id;
            }
            set
            {
                this._sharding_col_id = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("remote_schema_name")]
        public string RemoteSchemaName
        {
            get
            {
                return this._remote_schema_name;
            }
            set
            {
                this._remote_schema_name = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("remote_object_name")]
        public string RemoteObjectName
        {
            get
            {
                return this._remote_object_name;
            }
            set
            {
                this._remote_object_name = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("rejected_row_location")]
        public string RejectedRowLocation
        {
            get
            {
                return this._rejected_row_location;
            }
            set
            {
                this._rejected_row_location = value;
            }
        }
    }
}
