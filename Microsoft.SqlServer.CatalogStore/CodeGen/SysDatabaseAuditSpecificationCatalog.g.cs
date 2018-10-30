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
    
    
    [System.ComponentModel.DataAnnotations.Schema.TableAttribute("sys_database_audit_specifications")]
    public partial class SysDatabaseAuditSpecificationCatalog
    {
        
        private System.Nullable<int> _database_specification_id;
        
        private string _name;
        
        private System.Nullable<System.DateTime> _create_date;
        
        private System.Nullable<System.DateTime> _modify_date;
        
        private System.Nullable<System.Guid> _audit_guid;
        
        private System.Nullable<bool> _is_state_enabled;
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("database_specification_id")]
        public System.Nullable<int> DatabaseSpecificationId
        {
            get
            {
                return this._database_specification_id;
            }
            set
            {
                this._database_specification_id = value;
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
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("audit_guid")]
        public System.Nullable<System.Guid> AuditGuid
        {
            get
            {
                return this._audit_guid;
            }
            set
            {
                this._audit_guid = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_state_enabled")]
        public System.Nullable<bool> IsStateEnabled
        {
            get
            {
                return this._is_state_enabled;
            }
            set
            {
                this._is_state_enabled = value;
            }
        }
    }
}