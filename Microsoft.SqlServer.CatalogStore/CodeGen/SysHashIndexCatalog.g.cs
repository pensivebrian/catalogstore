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
    
    
    [System.ComponentModel.DataAnnotations.Schema.TableAttribute("sys_hash_indexes")]
    public partial class SysHashIndexCatalog
    {
        
        private System.Nullable<int> _object_id;
        
        private string _name;
        
        private System.Nullable<int> _index_id;
        
        private System.Nullable<byte> _type;
        
        private string _type_desc;
        
        private System.Nullable<bool> _is_unique;
        
        private System.Nullable<int> _data_space_id;
        
        private System.Nullable<bool> _ignore_dup_key;
        
        private System.Nullable<bool> _is_primary_key;
        
        private System.Nullable<bool> _is_unique_constraint;
        
        private System.Nullable<byte> _fill_factor;
        
        private System.Nullable<bool> _is_padded;
        
        private System.Nullable<bool> _is_disabled;
        
        private System.Nullable<bool> _is_hypothetical;
        
        private System.Nullable<bool> _is_ignored_in_optimization;
        
        private System.Nullable<bool> _allow_row_locks;
        
        private System.Nullable<bool> _allow_page_locks;
        
        private System.Nullable<bool> _has_filter;
        
        private string _filter_definition;
        
        private System.Nullable<int> _bucket_count;
        
        private System.Nullable<bool> _auto_created;
        
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
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("index_id")]
        public System.Nullable<int> IndexId
        {
            get
            {
                return this._index_id;
            }
            set
            {
                this._index_id = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("type")]
        public System.Nullable<byte> Type
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
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_unique")]
        public System.Nullable<bool> IsUnique
        {
            get
            {
                return this._is_unique;
            }
            set
            {
                this._is_unique = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("data_space_id")]
        public System.Nullable<int> DataSpaceId
        {
            get
            {
                return this._data_space_id;
            }
            set
            {
                this._data_space_id = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("ignore_dup_key")]
        public System.Nullable<bool> IgnoreDupKey
        {
            get
            {
                return this._ignore_dup_key;
            }
            set
            {
                this._ignore_dup_key = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_primary_key")]
        public System.Nullable<bool> IsPrimaryKey
        {
            get
            {
                return this._is_primary_key;
            }
            set
            {
                this._is_primary_key = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_unique_constraint")]
        public System.Nullable<bool> IsUniqueConstraint
        {
            get
            {
                return this._is_unique_constraint;
            }
            set
            {
                this._is_unique_constraint = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("fill_factor")]
        public System.Nullable<byte> FillFactor
        {
            get
            {
                return this._fill_factor;
            }
            set
            {
                this._fill_factor = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_padded")]
        public System.Nullable<bool> IsPadded
        {
            get
            {
                return this._is_padded;
            }
            set
            {
                this._is_padded = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_disabled")]
        public System.Nullable<bool> IsDisabled
        {
            get
            {
                return this._is_disabled;
            }
            set
            {
                this._is_disabled = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_hypothetical")]
        public System.Nullable<bool> IsHypothetical
        {
            get
            {
                return this._is_hypothetical;
            }
            set
            {
                this._is_hypothetical = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_ignored_in_optimization")]
        public System.Nullable<bool> IsIgnoredInOptimization
        {
            get
            {
                return this._is_ignored_in_optimization;
            }
            set
            {
                this._is_ignored_in_optimization = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("allow_row_locks")]
        public System.Nullable<bool> AllowRowLocks
        {
            get
            {
                return this._allow_row_locks;
            }
            set
            {
                this._allow_row_locks = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("allow_page_locks")]
        public System.Nullable<bool> AllowPageLocks
        {
            get
            {
                return this._allow_page_locks;
            }
            set
            {
                this._allow_page_locks = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("has_filter")]
        public System.Nullable<bool> HasFilter
        {
            get
            {
                return this._has_filter;
            }
            set
            {
                this._has_filter = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("filter_definition")]
        public string FilterDefinition
        {
            get
            {
                return this._filter_definition;
            }
            set
            {
                this._filter_definition = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("bucket_count")]
        public System.Nullable<int> BucketCount
        {
            get
            {
                return this._bucket_count;
            }
            set
            {
                this._bucket_count = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("auto_created")]
        public System.Nullable<bool> AutoCreated
        {
            get
            {
                return this._auto_created;
            }
            set
            {
                this._auto_created = value;
            }
        }
    }
}
