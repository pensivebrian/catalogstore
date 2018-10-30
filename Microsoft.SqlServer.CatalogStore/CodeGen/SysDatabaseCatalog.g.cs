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
    
    
    [System.ComponentModel.DataAnnotations.Schema.TableAttribute("sys_databases")]
    public partial class SysDatabaseCatalog
    {
        
        private string _name;
        
        private System.Nullable<int> _database_id;
        
        private System.Nullable<int> _source_database_id;
        
        private byte[] _owner_sid;
        
        private System.Nullable<System.DateTime> _create_date;
        
        private System.Nullable<byte> _compatibility_level;
        
        private string _collation_name;
        
        private System.Nullable<byte> _user_access;
        
        private string _user_access_desc;
        
        private System.Nullable<bool> _is_read_only;
        
        private System.Nullable<bool> _is_auto_close_on;
        
        private System.Nullable<bool> _is_auto_shrink_on;
        
        private System.Nullable<byte> _state;
        
        private string _state_desc;
        
        private System.Nullable<bool> _is_in_standby;
        
        private System.Nullable<bool> _is_cleanly_shutdown;
        
        private System.Nullable<bool> _is_supplemental_logging_enabled;
        
        private System.Nullable<byte> _snapshot_isolation_state;
        
        private string _snapshot_isolation_state_desc;
        
        private System.Nullable<bool> _is_read_committed_snapshot_on;
        
        private System.Nullable<byte> _recovery_model;
        
        private string _recovery_model_desc;
        
        private System.Nullable<byte> _page_verify_option;
        
        private string _page_verify_option_desc;
        
        private System.Nullable<bool> _is_auto_create_stats_on;
        
        private System.Nullable<bool> _is_auto_create_stats_incremental_on;
        
        private System.Nullable<bool> _is_auto_update_stats_on;
        
        private System.Nullable<bool> _is_auto_update_stats_async_on;
        
        private System.Nullable<bool> _is_ansi_null_default_on;
        
        private System.Nullable<bool> _is_ansi_nulls_on;
        
        private System.Nullable<bool> _is_ansi_padding_on;
        
        private System.Nullable<bool> _is_ansi_warnings_on;
        
        private System.Nullable<bool> _is_arithabort_on;
        
        private System.Nullable<bool> _is_concat_null_yields_null_on;
        
        private System.Nullable<bool> _is_numeric_roundabort_on;
        
        private System.Nullable<bool> _is_quoted_identifier_on;
        
        private System.Nullable<bool> _is_recursive_triggers_on;
        
        private System.Nullable<bool> _is_cursor_close_on_commit_on;
        
        private System.Nullable<bool> _is_local_cursor_default;
        
        private System.Nullable<bool> _is_fulltext_enabled;
        
        private System.Nullable<bool> _is_trustworthy_on;
        
        private System.Nullable<bool> _is_db_chaining_on;
        
        private System.Nullable<bool> _is_parameterization_forced;
        
        private System.Nullable<bool> _is_master_key_encrypted_by_server;
        
        private System.Nullable<bool> _is_query_store_on;
        
        private System.Nullable<bool> _is_published;
        
        private System.Nullable<bool> _is_subscribed;
        
        private System.Nullable<bool> _is_merge_published;
        
        private System.Nullable<bool> _is_distributor;
        
        private System.Nullable<bool> _is_sync_with_backup;
        
        private System.Nullable<System.Guid> _service_broker_guid;
        
        private System.Nullable<bool> _is_broker_enabled;
        
        private System.Nullable<byte> _log_reuse_wait;
        
        private string _log_reuse_wait_desc;
        
        private System.Nullable<bool> _is_date_correlation_on;
        
        private System.Nullable<bool> _is_cdc_enabled;
        
        private System.Nullable<bool> _is_encrypted;
        
        private System.Nullable<bool> _is_honor_broker_priority_on;
        
        private System.Nullable<System.Guid> _replica_id;
        
        private System.Nullable<System.Guid> _group_database_id;
        
        private System.Nullable<int> _resource_pool_id;
        
        private System.Nullable<short> _default_language_lcid;
        
        private string _default_language_name;
        
        private System.Nullable<int> _default_fulltext_language_lcid;
        
        private string _default_fulltext_language_name;
        
        private System.Nullable<bool> _is_nested_triggers_on;
        
        private System.Nullable<bool> _is_transform_noise_words_on;
        
        private System.Nullable<short> _two_digit_year_cutoff;
        
        private System.Nullable<byte> _containment;
        
        private string _containment_desc;
        
        private System.Nullable<int> _target_recovery_time_in_seconds;
        
        private System.Nullable<int> _delayed_durability;
        
        private string _delayed_durability_desc;
        
        private System.Nullable<bool> _is_memory_optimized_elevate_to_snapshot_on;
        
        private System.Nullable<bool> _is_federation_member;
        
        private System.Nullable<bool> _is_remote_data_archive_enabled;
        
        private System.Nullable<bool> _is_mixed_page_allocation_on;
        
        private System.Nullable<bool> _is_temporal_history_retention_enabled;
        
        private System.Nullable<int> _catalog_collation_type;
        
        private string _catalog_collation_type_desc;
        
        private string _physical_database_name;
        
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
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("database_id")]
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
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("source_database_id")]
        public System.Nullable<int> SourceDatabaseId
        {
            get
            {
                return this._source_database_id;
            }
            set
            {
                this._source_database_id = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("owner_sid")]
        public byte[] OwnerSid
        {
            get
            {
                return this._owner_sid;
            }
            set
            {
                this._owner_sid = value;
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
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("compatibility_level")]
        public System.Nullable<byte> CompatibilityLevel
        {
            get
            {
                return this._compatibility_level;
            }
            set
            {
                this._compatibility_level = value;
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
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("user_access")]
        public System.Nullable<byte> UserAccess
        {
            get
            {
                return this._user_access;
            }
            set
            {
                this._user_access = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("user_access_desc")]
        public string UserAccessDesc
        {
            get
            {
                return this._user_access_desc;
            }
            set
            {
                this._user_access_desc = value;
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
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_auto_close_on")]
        public System.Nullable<bool> IsAutoCloseOn
        {
            get
            {
                return this._is_auto_close_on;
            }
            set
            {
                this._is_auto_close_on = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_auto_shrink_on")]
        public System.Nullable<bool> IsAutoShrinkOn
        {
            get
            {
                return this._is_auto_shrink_on;
            }
            set
            {
                this._is_auto_shrink_on = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("state")]
        public System.Nullable<byte> State
        {
            get
            {
                return this._state;
            }
            set
            {
                this._state = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("state_desc")]
        public string StateDesc
        {
            get
            {
                return this._state_desc;
            }
            set
            {
                this._state_desc = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_in_standby")]
        public System.Nullable<bool> IsInStandby
        {
            get
            {
                return this._is_in_standby;
            }
            set
            {
                this._is_in_standby = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_cleanly_shutdown")]
        public System.Nullable<bool> IsCleanlyShutdown
        {
            get
            {
                return this._is_cleanly_shutdown;
            }
            set
            {
                this._is_cleanly_shutdown = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_supplemental_logging_enabled")]
        public System.Nullable<bool> IsSupplementalLoggingEnabled
        {
            get
            {
                return this._is_supplemental_logging_enabled;
            }
            set
            {
                this._is_supplemental_logging_enabled = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("snapshot_isolation_state")]
        public System.Nullable<byte> SnapshotIsolationState
        {
            get
            {
                return this._snapshot_isolation_state;
            }
            set
            {
                this._snapshot_isolation_state = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("snapshot_isolation_state_desc")]
        public string SnapshotIsolationStateDesc
        {
            get
            {
                return this._snapshot_isolation_state_desc;
            }
            set
            {
                this._snapshot_isolation_state_desc = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_read_committed_snapshot_on")]
        public System.Nullable<bool> IsReadCommittedSnapshotOn
        {
            get
            {
                return this._is_read_committed_snapshot_on;
            }
            set
            {
                this._is_read_committed_snapshot_on = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("recovery_model")]
        public System.Nullable<byte> RecoveryModel
        {
            get
            {
                return this._recovery_model;
            }
            set
            {
                this._recovery_model = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("recovery_model_desc")]
        public string RecoveryModelDesc
        {
            get
            {
                return this._recovery_model_desc;
            }
            set
            {
                this._recovery_model_desc = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("page_verify_option")]
        public System.Nullable<byte> PageVerifyOption
        {
            get
            {
                return this._page_verify_option;
            }
            set
            {
                this._page_verify_option = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("page_verify_option_desc")]
        public string PageVerifyOptionDesc
        {
            get
            {
                return this._page_verify_option_desc;
            }
            set
            {
                this._page_verify_option_desc = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_auto_create_stats_on")]
        public System.Nullable<bool> IsAutoCreateStatsOn
        {
            get
            {
                return this._is_auto_create_stats_on;
            }
            set
            {
                this._is_auto_create_stats_on = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_auto_create_stats_incremental_on")]
        public System.Nullable<bool> IsAutoCreateStatsIncrementalOn
        {
            get
            {
                return this._is_auto_create_stats_incremental_on;
            }
            set
            {
                this._is_auto_create_stats_incremental_on = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_auto_update_stats_on")]
        public System.Nullable<bool> IsAutoUpdateStatsOn
        {
            get
            {
                return this._is_auto_update_stats_on;
            }
            set
            {
                this._is_auto_update_stats_on = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_auto_update_stats_async_on")]
        public System.Nullable<bool> IsAutoUpdateStatsAsyncOn
        {
            get
            {
                return this._is_auto_update_stats_async_on;
            }
            set
            {
                this._is_auto_update_stats_async_on = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_ansi_null_default_on")]
        public System.Nullable<bool> IsAnsiNullDefaultOn
        {
            get
            {
                return this._is_ansi_null_default_on;
            }
            set
            {
                this._is_ansi_null_default_on = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_ansi_nulls_on")]
        public System.Nullable<bool> IsAnsiNullsOn
        {
            get
            {
                return this._is_ansi_nulls_on;
            }
            set
            {
                this._is_ansi_nulls_on = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_ansi_padding_on")]
        public System.Nullable<bool> IsAnsiPaddingOn
        {
            get
            {
                return this._is_ansi_padding_on;
            }
            set
            {
                this._is_ansi_padding_on = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_ansi_warnings_on")]
        public System.Nullable<bool> IsAnsiWarningsOn
        {
            get
            {
                return this._is_ansi_warnings_on;
            }
            set
            {
                this._is_ansi_warnings_on = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_arithabort_on")]
        public System.Nullable<bool> IsArithabortOn
        {
            get
            {
                return this._is_arithabort_on;
            }
            set
            {
                this._is_arithabort_on = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_concat_null_yields_null_on")]
        public System.Nullable<bool> IsConcatNullYieldsNullOn
        {
            get
            {
                return this._is_concat_null_yields_null_on;
            }
            set
            {
                this._is_concat_null_yields_null_on = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_numeric_roundabort_on")]
        public System.Nullable<bool> IsNumericRoundabortOn
        {
            get
            {
                return this._is_numeric_roundabort_on;
            }
            set
            {
                this._is_numeric_roundabort_on = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_quoted_identifier_on")]
        public System.Nullable<bool> IsQuotedIdentifierOn
        {
            get
            {
                return this._is_quoted_identifier_on;
            }
            set
            {
                this._is_quoted_identifier_on = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_recursive_triggers_on")]
        public System.Nullable<bool> IsRecursiveTriggersOn
        {
            get
            {
                return this._is_recursive_triggers_on;
            }
            set
            {
                this._is_recursive_triggers_on = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_cursor_close_on_commit_on")]
        public System.Nullable<bool> IsCursorCloseOnCommitOn
        {
            get
            {
                return this._is_cursor_close_on_commit_on;
            }
            set
            {
                this._is_cursor_close_on_commit_on = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_local_cursor_default")]
        public System.Nullable<bool> IsLocalCursorDefault
        {
            get
            {
                return this._is_local_cursor_default;
            }
            set
            {
                this._is_local_cursor_default = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_fulltext_enabled")]
        public System.Nullable<bool> IsFulltextEnabled
        {
            get
            {
                return this._is_fulltext_enabled;
            }
            set
            {
                this._is_fulltext_enabled = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_trustworthy_on")]
        public System.Nullable<bool> IsTrustworthyOn
        {
            get
            {
                return this._is_trustworthy_on;
            }
            set
            {
                this._is_trustworthy_on = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_db_chaining_on")]
        public System.Nullable<bool> IsDbChainingOn
        {
            get
            {
                return this._is_db_chaining_on;
            }
            set
            {
                this._is_db_chaining_on = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_parameterization_forced")]
        public System.Nullable<bool> IsParameterizationForced
        {
            get
            {
                return this._is_parameterization_forced;
            }
            set
            {
                this._is_parameterization_forced = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_master_key_encrypted_by_server")]
        public System.Nullable<bool> IsMasterKeyEncryptedByServer
        {
            get
            {
                return this._is_master_key_encrypted_by_server;
            }
            set
            {
                this._is_master_key_encrypted_by_server = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_query_store_on")]
        public System.Nullable<bool> IsQueryStoreOn
        {
            get
            {
                return this._is_query_store_on;
            }
            set
            {
                this._is_query_store_on = value;
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
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_subscribed")]
        public System.Nullable<bool> IsSubscribed
        {
            get
            {
                return this._is_subscribed;
            }
            set
            {
                this._is_subscribed = value;
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
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_distributor")]
        public System.Nullable<bool> IsDistributor
        {
            get
            {
                return this._is_distributor;
            }
            set
            {
                this._is_distributor = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_sync_with_backup")]
        public System.Nullable<bool> IsSyncWithBackup
        {
            get
            {
                return this._is_sync_with_backup;
            }
            set
            {
                this._is_sync_with_backup = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("service_broker_guid")]
        public System.Nullable<System.Guid> ServiceBrokerGuid
        {
            get
            {
                return this._service_broker_guid;
            }
            set
            {
                this._service_broker_guid = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_broker_enabled")]
        public System.Nullable<bool> IsBrokerEnabled
        {
            get
            {
                return this._is_broker_enabled;
            }
            set
            {
                this._is_broker_enabled = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("log_reuse_wait")]
        public System.Nullable<byte> LogReuseWait
        {
            get
            {
                return this._log_reuse_wait;
            }
            set
            {
                this._log_reuse_wait = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("log_reuse_wait_desc")]
        public string LogReuseWaitDesc
        {
            get
            {
                return this._log_reuse_wait_desc;
            }
            set
            {
                this._log_reuse_wait_desc = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_date_correlation_on")]
        public System.Nullable<bool> IsDateCorrelationOn
        {
            get
            {
                return this._is_date_correlation_on;
            }
            set
            {
                this._is_date_correlation_on = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_cdc_enabled")]
        public System.Nullable<bool> IsCdcEnabled
        {
            get
            {
                return this._is_cdc_enabled;
            }
            set
            {
                this._is_cdc_enabled = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_encrypted")]
        public System.Nullable<bool> IsEncrypted
        {
            get
            {
                return this._is_encrypted;
            }
            set
            {
                this._is_encrypted = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_honor_broker_priority_on")]
        public System.Nullable<bool> IsHonorBrokerPriorityOn
        {
            get
            {
                return this._is_honor_broker_priority_on;
            }
            set
            {
                this._is_honor_broker_priority_on = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("replica_id")]
        public System.Nullable<System.Guid> ReplicaId
        {
            get
            {
                return this._replica_id;
            }
            set
            {
                this._replica_id = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("group_database_id")]
        public System.Nullable<System.Guid> GroupDatabaseId
        {
            get
            {
                return this._group_database_id;
            }
            set
            {
                this._group_database_id = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("resource_pool_id")]
        public System.Nullable<int> ResourcePoolId
        {
            get
            {
                return this._resource_pool_id;
            }
            set
            {
                this._resource_pool_id = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("default_language_lcid")]
        public System.Nullable<short> DefaultLanguageLcid
        {
            get
            {
                return this._default_language_lcid;
            }
            set
            {
                this._default_language_lcid = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("default_language_name")]
        public string DefaultLanguageName
        {
            get
            {
                return this._default_language_name;
            }
            set
            {
                this._default_language_name = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("default_fulltext_language_lcid")]
        public System.Nullable<int> DefaultFulltextLanguageLcid
        {
            get
            {
                return this._default_fulltext_language_lcid;
            }
            set
            {
                this._default_fulltext_language_lcid = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("default_fulltext_language_name")]
        public string DefaultFulltextLanguageName
        {
            get
            {
                return this._default_fulltext_language_name;
            }
            set
            {
                this._default_fulltext_language_name = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_nested_triggers_on")]
        public System.Nullable<bool> IsNestedTriggersOn
        {
            get
            {
                return this._is_nested_triggers_on;
            }
            set
            {
                this._is_nested_triggers_on = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_transform_noise_words_on")]
        public System.Nullable<bool> IsTransformNoiseWordsOn
        {
            get
            {
                return this._is_transform_noise_words_on;
            }
            set
            {
                this._is_transform_noise_words_on = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("two_digit_year_cutoff")]
        public System.Nullable<short> TwoDigitYearCutoff
        {
            get
            {
                return this._two_digit_year_cutoff;
            }
            set
            {
                this._two_digit_year_cutoff = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("containment")]
        public System.Nullable<byte> Containment
        {
            get
            {
                return this._containment;
            }
            set
            {
                this._containment = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("containment_desc")]
        public string ContainmentDesc
        {
            get
            {
                return this._containment_desc;
            }
            set
            {
                this._containment_desc = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("target_recovery_time_in_seconds")]
        public System.Nullable<int> TargetRecoveryTimeInSeconds
        {
            get
            {
                return this._target_recovery_time_in_seconds;
            }
            set
            {
                this._target_recovery_time_in_seconds = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("delayed_durability")]
        public System.Nullable<int> DelayedDurability
        {
            get
            {
                return this._delayed_durability;
            }
            set
            {
                this._delayed_durability = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("delayed_durability_desc")]
        public string DelayedDurabilityDesc
        {
            get
            {
                return this._delayed_durability_desc;
            }
            set
            {
                this._delayed_durability_desc = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_memory_optimized_elevate_to_snapshot_on")]
        public System.Nullable<bool> IsMemoryOptimizedElevateToSnapshotOn
        {
            get
            {
                return this._is_memory_optimized_elevate_to_snapshot_on;
            }
            set
            {
                this._is_memory_optimized_elevate_to_snapshot_on = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_federation_member")]
        public System.Nullable<bool> IsFederationMember
        {
            get
            {
                return this._is_federation_member;
            }
            set
            {
                this._is_federation_member = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_remote_data_archive_enabled")]
        public System.Nullable<bool> IsRemoteDataArchiveEnabled
        {
            get
            {
                return this._is_remote_data_archive_enabled;
            }
            set
            {
                this._is_remote_data_archive_enabled = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_mixed_page_allocation_on")]
        public System.Nullable<bool> IsMixedPageAllocationOn
        {
            get
            {
                return this._is_mixed_page_allocation_on;
            }
            set
            {
                this._is_mixed_page_allocation_on = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_temporal_history_retention_enabled")]
        public System.Nullable<bool> IsTemporalHistoryRetentionEnabled
        {
            get
            {
                return this._is_temporal_history_retention_enabled;
            }
            set
            {
                this._is_temporal_history_retention_enabled = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("catalog_collation_type")]
        public System.Nullable<int> CatalogCollationType
        {
            get
            {
                return this._catalog_collation_type;
            }
            set
            {
                this._catalog_collation_type = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("catalog_collation_type_desc")]
        public string CatalogCollationTypeDesc
        {
            get
            {
                return this._catalog_collation_type_desc;
            }
            set
            {
                this._catalog_collation_type_desc = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("physical_database_name")]
        public string PhysicalDatabaseName
        {
            get
            {
                return this._physical_database_name;
            }
            set
            {
                this._physical_database_name = value;
            }
        }
    }
}
