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
    
    
    [System.ComponentModel.DataAnnotations.Schema.TableAttribute("sys_change_tracking_databases")]
    public partial class SysChangeTrackingDatabaseCatalog
    {
        
        private System.Nullable<int> _database_id;
        
        private System.Nullable<byte> _is_auto_cleanup_on;
        
        private System.Nullable<int> _retention_period;
        
        private System.Nullable<byte> _retention_period_units;
        
        private string _retention_period_units_desc;
        
        private System.Nullable<long> _max_cleanup_version;
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("database_id")]
        [System.ComponentModel.DataAnnotations.KeyAttribute()]
        public System.Nullable<int> DatabaseId
        {
            get
            {
                return this._database_id;
            }
            set
            {
                this._database_id = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_auto_cleanup_on")]
        public System.Nullable<byte> IsAutoCleanupOn
        {
            get
            {
                return this._is_auto_cleanup_on;
            }
            set
            {
                this._is_auto_cleanup_on = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("retention_period")]
        public System.Nullable<int> RetentionPeriod
        {
            get
            {
                return this._retention_period;
            }
            set
            {
                this._retention_period = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("retention_period_units")]
        public System.Nullable<byte> RetentionPeriodUnits
        {
            get
            {
                return this._retention_period_units;
            }
            set
            {
                this._retention_period_units = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("retention_period_units_desc")]
        public string RetentionPeriodUnitsDesc
        {
            get
            {
                return this._retention_period_units_desc;
            }
            set
            {
                this._retention_period_units_desc = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("max_cleanup_version")]
        public System.Nullable<long> MaxCleanupVersion
        {
            get
            {
                return this._max_cleanup_version;
            }
            set
            {
                this._max_cleanup_version = value;
            }
        }
    }
}