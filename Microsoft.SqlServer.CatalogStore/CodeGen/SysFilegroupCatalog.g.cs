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
    
    
    [System.ComponentModel.DataAnnotations.Schema.TableAttribute("sys_filegroups")]
    public partial class SysFilegroupCatalog
    {
        
        private string _name;
        
        private System.Nullable<int> _data_space_id;
        
        private string _type;
        
        private string _type_desc;
        
        private System.Nullable<bool> _is_default;
        
        private System.Nullable<bool> _is_system;
        
        private System.Nullable<System.Guid> _filegroup_guid;
        
        private System.Nullable<int> _log_filegroup_id;
        
        private System.Nullable<bool> _is_read_only;
        
        private System.Nullable<bool> _is_autogrow_all_files;
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("name")]
        [System.ComponentModel.DataAnnotations.KeyAttribute()]
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
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_default")]
        public System.Nullable<bool> IsDefault
        {
            get
            {
                return this._is_default;
            }
            set
            {
                this._is_default = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_system")]
        public System.Nullable<bool> IsSystem
        {
            get
            {
                return this._is_system;
            }
            set
            {
                this._is_system = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("filegroup_guid")]
        public System.Nullable<System.Guid> FilegroupGuid
        {
            get
            {
                return this._filegroup_guid;
            }
            set
            {
                this._filegroup_guid = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("log_filegroup_id")]
        public System.Nullable<int> LogFilegroupId
        {
            get
            {
                return this._log_filegroup_id;
            }
            set
            {
                this._log_filegroup_id = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_read_only")]
        public System.Nullable<bool> IsReadOnly
        {
            get
            {
                return this._is_read_only;
            }
            set
            {
                this._is_read_only = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_autogrow_all_files")]
        public System.Nullable<bool> IsAutogrowAllFiles
        {
            get
            {
                return this._is_autogrow_all_files;
            }
            set
            {
                this._is_autogrow_all_files = value;
            }
        }
    }
}
