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
    
    
    [System.ComponentModel.DataAnnotations.Schema.TableAttribute("sys_database_query_store_options")]
    public partial class SysDatabaseQueryStoreOptionCatalog
    {
        
        private System.Nullable<short> _desired_state;
        
        private string _desired_state_desc;
        
        private System.Nullable<short> _actual_state;
        
        private string _actual_state_desc;
        
        private System.Nullable<int> _readonly_reason;
        
        private System.Nullable<long> _current_storage_size_mb;
        
        private System.Nullable<long> _flush_interval_seconds;
        
        private System.Nullable<long> _interval_length_minutes;
        
        private System.Nullable<long> _max_storage_size_mb;
        
        private System.Nullable<long> _stale_query_threshold_days;
        
        private System.Nullable<long> _max_plans_per_query;
        
        private System.Nullable<short> _query_capture_mode;
        
        private string _query_capture_mode_desc;
        
        private System.Nullable<short> _size_based_cleanup_mode;
        
        private string _size_based_cleanup_mode_desc;
        
        private System.Nullable<short> _wait_stats_capture_mode;
        
        private string _wait_stats_capture_mode_desc;
        
        private string _actual_state_additional_info;
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("desired_state")]
        [System.ComponentModel.DataAnnotations.KeyAttribute()]
        public System.Nullable<short> DesiredState
        {
            get
            {
                return this._desired_state;
            }
            set
            {
                this._desired_state = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("desired_state_desc")]
        public string DesiredStateDesc
        {
            get
            {
                return this._desired_state_desc;
            }
            set
            {
                this._desired_state_desc = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("actual_state")]
        public System.Nullable<short> ActualState
        {
            get
            {
                return this._actual_state;
            }
            set
            {
                this._actual_state = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("actual_state_desc")]
        public string ActualStateDesc
        {
            get
            {
                return this._actual_state_desc;
            }
            set
            {
                this._actual_state_desc = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("readonly_reason")]
        public System.Nullable<int> ReadonlyReason
        {
            get
            {
                return this._readonly_reason;
            }
            set
            {
                this._readonly_reason = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("current_storage_size_mb")]
        public System.Nullable<long> CurrentStorageSizeMb
        {
            get
            {
                return this._current_storage_size_mb;
            }
            set
            {
                this._current_storage_size_mb = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("flush_interval_seconds")]
        public System.Nullable<long> FlushIntervalSeconds
        {
            get
            {
                return this._flush_interval_seconds;
            }
            set
            {
                this._flush_interval_seconds = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("interval_length_minutes")]
        public System.Nullable<long> IntervalLengthMinutes
        {
            get
            {
                return this._interval_length_minutes;
            }
            set
            {
                this._interval_length_minutes = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("max_storage_size_mb")]
        public System.Nullable<long> MaxStorageSizeMb
        {
            get
            {
                return this._max_storage_size_mb;
            }
            set
            {
                this._max_storage_size_mb = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("stale_query_threshold_days")]
        public System.Nullable<long> StaleQueryThresholdDays
        {
            get
            {
                return this._stale_query_threshold_days;
            }
            set
            {
                this._stale_query_threshold_days = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("max_plans_per_query")]
        public System.Nullable<long> MaxPlansPerQuery
        {
            get
            {
                return this._max_plans_per_query;
            }
            set
            {
                this._max_plans_per_query = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("query_capture_mode")]
        public System.Nullable<short> QueryCaptureMode
        {
            get
            {
                return this._query_capture_mode;
            }
            set
            {
                this._query_capture_mode = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("query_capture_mode_desc")]
        public string QueryCaptureModeDesc
        {
            get
            {
                return this._query_capture_mode_desc;
            }
            set
            {
                this._query_capture_mode_desc = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("size_based_cleanup_mode")]
        public System.Nullable<short> SizeBasedCleanupMode
        {
            get
            {
                return this._size_based_cleanup_mode;
            }
            set
            {
                this._size_based_cleanup_mode = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("size_based_cleanup_mode_desc")]
        public string SizeBasedCleanupModeDesc
        {
            get
            {
                return this._size_based_cleanup_mode_desc;
            }
            set
            {
                this._size_based_cleanup_mode_desc = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("wait_stats_capture_mode")]
        public System.Nullable<short> WaitStatsCaptureMode
        {
            get
            {
                return this._wait_stats_capture_mode;
            }
            set
            {
                this._wait_stats_capture_mode = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("wait_stats_capture_mode_desc")]
        public string WaitStatsCaptureModeDesc
        {
            get
            {
                return this._wait_stats_capture_mode_desc;
            }
            set
            {
                this._wait_stats_capture_mode_desc = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("actual_state_additional_info")]
        public string ActualStateAdditionalInfo
        {
            get
            {
                return this._actual_state_additional_info;
            }
            set
            {
                this._actual_state_additional_info = value;
            }
        }
    }
}