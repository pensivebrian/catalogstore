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
    
    
    [System.ComponentModel.DataAnnotations.Schema.TableAttribute("sys_stats")]
    public partial class SysStatCatalog
    {
        
        private System.Nullable<int> _object_id;
        
        private string _name;
        
        private System.Nullable<int> _stats_id;
        
        private System.Nullable<bool> _auto_created;
        
        private System.Nullable<bool> _user_created;
        
        private System.Nullable<bool> _no_recompute;
        
        private System.Nullable<bool> _has_filter;
        
        private string _filter_definition;
        
        private System.Nullable<bool> _is_temporary;
        
        private System.Nullable<bool> _is_incremental;
        
        private System.Nullable<bool> _has_persisted_sample;
        
        private System.Nullable<int> _stats_generation_method;
        
        private string _stats_generation_method_desc;
        
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
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("stats_id")]
        public System.Nullable<int> StatsId
        {
            get
            {
                return this._stats_id;
            }
            set
            {
                this._stats_id = value;
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
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("user_created")]
        public System.Nullable<bool> UserCreated
        {
            get
            {
                return this._user_created;
            }
            set
            {
                this._user_created = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("no_recompute")]
        public System.Nullable<bool> NoRecompute
        {
            get
            {
                return this._no_recompute;
            }
            set
            {
                this._no_recompute = value;
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
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_temporary")]
        public System.Nullable<bool> IsTemporary
        {
            get
            {
                return this._is_temporary;
            }
            set
            {
                this._is_temporary = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_incremental")]
        public System.Nullable<bool> IsIncremental
        {
            get
            {
                return this._is_incremental;
            }
            set
            {
                this._is_incremental = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("has_persisted_sample")]
        public System.Nullable<bool> HasPersistedSample
        {
            get
            {
                return this._has_persisted_sample;
            }
            set
            {
                this._has_persisted_sample = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("stats_generation_method")]
        public System.Nullable<int> StatsGenerationMethod
        {
            get
            {
                return this._stats_generation_method;
            }
            set
            {
                this._stats_generation_method = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("stats_generation_method_desc")]
        public string StatsGenerationMethodDesc
        {
            get
            {
                return this._stats_generation_method_desc;
            }
            set
            {
                this._stats_generation_method_desc = value;
            }
        }
    }
}
