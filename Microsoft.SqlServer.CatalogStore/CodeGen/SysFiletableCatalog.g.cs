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
    
    
    [System.ComponentModel.DataAnnotations.Schema.TableAttribute("sys_filetables")]
    public partial class SysFiletableCatalog
    {
        
        private System.Nullable<int> _object_id;
        
        private System.Nullable<bool> _is_enabled;
        
        private string _directory_name;
        
        private System.Nullable<int> _filename_collation_id;
        
        private string _filename_collation_name;
        
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
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_enabled")]
        public System.Nullable<bool> IsEnabled
        {
            get
            {
                return this._is_enabled;
            }
            set
            {
                this._is_enabled = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("directory_name")]
        public string DirectoryName
        {
            get
            {
                return this._directory_name;
            }
            set
            {
                this._directory_name = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("filename_collation_id")]
        public System.Nullable<int> FilenameCollationId
        {
            get
            {
                return this._filename_collation_id;
            }
            set
            {
                this._filename_collation_id = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("filename_collation_name")]
        public string FilenameCollationName
        {
            get
            {
                return this._filename_collation_name;
            }
            set
            {
                this._filename_collation_name = value;
            }
        }
    }
}
