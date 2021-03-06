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
    
    
    [System.ComponentModel.DataAnnotations.Schema.TableAttribute("sys_fulltext_catalogs")]
    public partial class SysFulltextCatalogCatalog
    {
        
        private System.Nullable<int> _fulltext_catalog_id;
        
        private string _name;
        
        private string _path;
        
        private System.Nullable<bool> _is_default;
        
        private System.Nullable<bool> _is_accent_sensitivity_on;
        
        private System.Nullable<int> _data_space_id;
        
        private System.Nullable<int> _file_id;
        
        private System.Nullable<int> _principal_id;
        
        private System.Nullable<bool> _is_importing;
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("fulltext_catalog_id")]
        public System.Nullable<int> FulltextCatalogId
        {
            get
            {
                return this._fulltext_catalog_id;
            }
            set
            {
                this._fulltext_catalog_id = value;
            }
        }
        
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
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("path")]
        public string Path
        {
            get
            {
                return this._path;
            }
            set
            {
                this._path = value;
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
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_accent_sensitivity_on")]
        public System.Nullable<bool> IsAccentSensitivityOn
        {
            get
            {
                return this._is_accent_sensitivity_on;
            }
            set
            {
                this._is_accent_sensitivity_on = value;
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
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("file_id")]
        public System.Nullable<int> FileId
        {
            get
            {
                return this._file_id;
            }
            set
            {
                this._file_id = value;
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
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_importing")]
        public System.Nullable<bool> IsImporting
        {
            get
            {
                return this._is_importing;
            }
            set
            {
                this._is_importing = value;
            }
        }
    }
}
