using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.SqlServer.CatalogStore.Tests
{
    public class DacFxReverseEngineer
    {
        public void Load(string connectionString, bool azure = false)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandTimeout = 60 * 30;
                command.CommandText = azure ? _azureQuery : _query;
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    do
                    {
                        while (reader.Read())
                        {
                            int columnsCount = reader.FieldCount;
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                if (!reader.IsDBNull(i))
                                {
                                    reader.GetValue(i);
                                }
                            }
                        }
                    }
                    while (reader.NextResult());
                }
            }
        }

        private static readonly string _azureQuery = @"
DECLARE @SystemTableTypeDiscriminators TABLE(TypeCode char(2) PRIMARY KEY);
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql130DatabaseCredentialPopulator' as [PopulatorName];
SELECT * FROM (
SELECT	
        [dbc].[credential_id]       AS [CredentialId],
        [dbc].[name]                AS [CredentialName],
        [dbc].[credential_identity] AS [Identity]
FROM	
        [sys].[database_scoped_credentials] [dbc] WITH (NOLOCK) ) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.SqlAzureV12DatabaseOptionsPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [db].[is_ansi_null_default_on]          AS [IsAnsiNullDefaultOn],
        [db].[is_ansi_nulls_on]                 AS [IsAnsiNullsOn],
        [db].[is_ansi_padding_on]               AS [IsAnsiPaddingOn],
        [db].[is_ansi_warnings_on]              AS [IsAnsiWarningsOn],
        [db].[is_arithabort_on]                 AS [IsArithAbortOn],
        [db].[is_auto_close_on]                 AS [IsAutoCloseOn],
        [db].[is_auto_create_stats_on]          AS [IsAutoCreateStatisticsOn],
        [db].[is_auto_shrink_on]                AS [IsAutoShrinkOn],
        [db].[is_auto_update_stats_on]          AS [IsAutoUpdateStatisticsOn],
        [db].[is_cursor_close_on_commit_on]     AS [IsCursorCloseOnCommitOn],
        [db].[is_fulltext_enabled]              AS [IsFullTextEnabled],
        [db].[is_in_standby]                    AS [IsInStandBy],
        [db].[is_local_cursor_default]          AS [IsLocalCursorsDefault],
        [db].[is_merge_published]               AS [IsMergePublished],
        [db].[is_concat_null_yields_null_on]    AS [IsConcatNullYieldsNullOn],
        [db].[is_numeric_roundabort_on]         AS [IsNumericRoundAbortOn],
        [db].[is_published]                     AS [IsPublished],
        [db].[is_quoted_identifier_on]          AS [IsQuotedIdentifierOn],
        [db].[is_recursive_triggers_on]         AS [IsRecursiveTriggersOn],
        [db].[is_subscribed]                    AS [IsSubscribed],
        CONVERT(bit, DATABASEPROPERTYEX([db].[name], N'IsTornPageDetectionEnabled'))    AS [IsTornPageProtectionOn],
        [db].[collation_name]                   AS [Collation],
        [db].[recovery_model_desc]              AS [RecoveryMode],
        DATABASEPROPERTYEX([db].[name], N'SQLSortOrder')   AS [SQLSortOrder],
        [db].[state_desc]                       AS [Status],
        DATABASEPROPERTYEX([db].[name], N'Updateability')  AS [Updateability],
        [db].[user_access_desc]                    AS [UserAccess],
        DATABASEPROPERTYEX([db].[name], N'Version')        AS [Version],
        [db].[is_db_chaining_on]                AS [IsDbChainingOn],
        [db].[is_trustworthy_on]                AS [IsTrustWorthyOn],
        [db].[is_auto_update_stats_async_on]    AS [IsAutoUpdateStatisticesAsyncOn],
        [db].[page_verify_option]               AS [PageVerifyOption],
        [db].[delayed_durability]               AS [DelayedDurabilityMode],
        CAST(0 as BIT)                          AS [MirrongFailOverLsn],
        CAST(0 as BIT)                          AS [MirroringState],
        CAST(0 as BIT)                          AS [MirroringPartnerServer],
        CAST(0 as BIT)                          AS [MirroringSafetyLevel],
        CAST(0 as BIT)                          AS [MirroringRedoQueueSize],
        CAST(0 as BIT)                          AS [MirroringRedoQueueType],
        CAST(0 as BIT)                          AS [MirroringPartnerServerTimeout],
        CAST(0 as BIT)                          AS [MirroringWitnessServer],
        CAST(0 as BIT)                          AS [MirroringWitnessState],
        [db].[is_supplemental_logging_enabled]  AS [IsSupplementalLoggingOn],
        [db].[is_broker_enabled]                AS [IsServiceBrokerEnabled],
        [db].[is_honor_broker_priority_on]      AS [IsBrokerPriorityHonored],
        [db].[is_date_correlation_on]           AS [IsDateCorrelationOptimizationOn],
        [db].[is_parameterization_forced]       AS [IsParameterizationForced],
        [db].[snapshot_isolation_state]         AS [SnapshotIsolationState],
        [db].[is_read_committed_snapshot_on]    AS [IsReadCommittedSnapshot],
        [db].[is_memory_optimized_elevate_to_snapshot_on]    AS [IsMemoryOptimizedElevatedToSnapshot],
        [db].[is_auto_create_stats_incremental_on]    AS [IsAutoCreateStatisticsIncrementalOn],
        [db].[compatibility_level]              AS [CompatibilityLevel],
        [db].[is_encrypted]                     AS [IsEncrypted],
        CAST(CASE
             WHEN [dbc].[database_id] IS NULL THEN 0
             ELSE 1
             END AS BIT)                        AS [IsChangeTrackingOn],
        CAST([dbc].[is_auto_cleanup_on] AS BIT) AS [IsChangeTrackingAutoCleanupOn],
        [dbc].[retention_period]                AS [ChangeTrackingRetentionPeriod],
        [dbc].[retention_period_units]          AS [ChangeTrackingRetentionPeriodUnits],
        CAST(CASE
             WHEN [db].[name] IN (N'master', N'tempdb', N'model', N'msdb') THEN 0
             ELSE 1
             END AS BIT)                        AS [IsVardecimalStorageFormatOn],
        [db].[containment]                      AS [Containment],
        [db].[catalog_collation_type]           AS [CatalogCollation],
        [db].[default_language_lcid]            AS [DefaultLanguageId],
        [db].[default_fulltext_language_lcid]   AS [DefaultFulltextLanguageId],
        [db].[is_nested_triggers_on]            AS [IsNestedTriggersOn],
        [db].[is_transform_noise_words_on]      AS [IsTransformNoiseWordsOn],
        [db].[two_digit_year_cutoff]            AS [TwoDigitYearCutoff],
        CASE WHEN [fg].[is_default] = 1 THEN [fg].[name] ELSE NULL END AS [DefaultFileGroupName],
        CASE WHEN [fg].[is_default] = 1 THEN [fg].[data_space_id] ELSE -1 END AS [DefaultFileGroupId],
        CASE WHEN [fsfg].[is_default] = 1 THEN [fsfg].[name] ELSE NULL END AS [DefaultFileStreamFileGroupName],
        [db].[target_recovery_time_in_seconds] AS [TargetRecoveryTimeInSeconds],
        CAST(0 as TINYINT)                      AS [NonTransactedAccess],
        ''                                      AS [FileStreamDirectoryName],
        [qds].[desired_state]              AS [QueryStoreDesiredState],
        [qds].[actual_state]               AS [QueryStoreActualState],
        [qds].[current_storage_size_mb]         AS [QueryStoreCurrentStorageSize],
        [qds].[flush_interval_seconds]          AS [QueryStoreFlushInterval],
        [qds].[interval_length_minutes]         AS [QueryStoreStatsInterval],
        [qds].[max_storage_size_mb]             AS [QueryStoreMaxStorageSize],
        [qds].[stale_query_threshold_days]      AS [QueryStoreStaleQueryThreshold],
        [qds].[max_plans_per_query]             AS [QueryStoreMaxPlansPerQuery],
        [qds].[query_capture_mode]         AS [QueryStoreQueryCaptureMode],
        [dbscm].[value]                         AS [MaxDop],
        [dbscm].[value_for_secondary]           AS [MaxDopForSecondary],
        [dbscl].[value]                         AS [LegacyCardinalityEstimation],
        [dbscl].[value_for_secondary]           AS [LegacyCardinalityEstimationForSecondary],
        [dbscp].[value]                         AS [ParameterSniffing],
        [dbscp].[value_for_secondary]           AS [ParameterSniffingForSecondary],
        [dbscq].[value]                         AS [QueryOptimizerHotfixes],
        [dbscq].[value_for_secondary]           AS [QueryOptimizerHotfixesForSecondary],
        [db].[is_temporal_history_retention_enabled] AS [IsTemporalRetentionOn]
FROM [sys].[databases] [db] WITH (NOLOCK)
LEFT JOIN [sys].[change_tracking_databases] [dbc] WITH (NOLOCK) ON [dbc].[database_id] = [db].[database_id]
LEFT JOIN [sys].[filegroups] [fg] WITH(NOLOCK) ON [fg].[is_default] = 1 AND [fg].[type] = N'FG'
LEFT JOIN [sys].[filegroups] [fsfg] WITH(NOLOCK) ON [fsfg].[is_default] = 1 AND [fsfg].[type] = N'FD'
LEFT JOIN [sys].[database_query_store_options] [qds] WITH (NOLOCK) ON 1 = 1
LEFT JOIN [sys].[database_scoped_configurations] AS [dbscm] WITH (NOLOCK) ON [dbscm].[name] = N'MAXDOP'
LEFT JOIN [sys].[database_scoped_configurations] AS [dbscl] WITH (NOLOCK) ON [dbscl].[name] = N'LEGACY_CARDINALITY_ESTIMATION'
LEFT JOIN [sys].[database_scoped_configurations] AS [dbscp] WITH (NOLOCK) ON [dbscp].[name] = N'PARAMETER_SNIFFING'
LEFT JOIN [sys].[database_scoped_configurations] AS [dbscq] WITH (NOLOCK) ON [dbscq].[name] = N'QUERY_OPTIMIZER_HOTFIXES'
WHERE [db].[name] = DB_NAME()) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.SqlAzureV12GenericDatabaseScopedConfigurationOptionPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [dsc].[configuration_id]             AS [GenericDatabaseOptionId],
        [dsc].[name]                         AS [GenericDatabaseOptionName],
        [dsc].[value]                        AS [GenericDatabaseOptionValue],
        [dsc].[value_for_secondary]          AS [GenericDatabaseOptionSecondaryValue],
        [dsc].[is_value_default]             AS [IsValueDefault]
FROM [sys].[database_scoped_configurations] [dsc] WITH (NOLOCK)
WHERE [dsc].[name] != 'MAXDOP' AND
      [dsc].[name] != 'LEGACY_CARDINALITY_ESTIMATION' AND
      [dsc].[name] != 'PARAMETER_SNIFFING' AND
      [dsc].[name] != 'QUERY_OPTIMIZER_HOTFIXES') AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.SqlAzureV12UserPopulator' as [PopulatorName];
SELECT * FROM (SELECT  [dp].[principal_id]          AS [ObjectId],
        [dp].[name]                  AS [Name],
        [dp].[type]                  AS [Type],
        NULL                         AS [LoginName],
        NULL                         AS [CertificateId],
        NULL                         AS [CertificateName],
        NULL                         AS [AsymmetricKeyId],
        NULL                         AS [AsymmetricKeyName],
        [dp].[default_schema_name]   AS [DefaultSchemaName],       
        [dp].[authentication_type]   AS [AuthenticationType],
        [dp].[default_language_name] AS [DefaultLanguage],
        [dp].[sid]                   AS [Sid]         
FROM    [sys].[database_principals] AS [dp] WITH (NOLOCK)
WHERE   CHARINDEX([dp].[type], N'USGCKEX') > 0
AND     [dp].[name] != N'cdc') AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90RolePopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [dp].[principal_id]                   AS [ObjectId],
        [dp].[name]                           AS [Name],
        [dp].[owning_principal_id]            AS [OwnerId],
        USER_NAME([dp].[owning_principal_id]) AS [OwnerName]
FROM    [sys].[database_principals] AS [dp] WITH (NOLOCK)
WHERE   [dp].[type] = N'R'
AND     USER_NAME([dp].[owning_principal_id]) != N'cdc'
) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90ApplicationRolePopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [dp].[principal_id]        AS [ObjectId],
        [s].[schema_id]            AS [SchemaId],
        [dp].[name]                AS [Name],
        [dp].[default_schema_name] AS [DefaultSchemaName]
FROM    [sys].[database_principals] AS [dp] WITH (NOLOCK)
        LEFT JOIN [sys].[schemas] [s] WITH (NOLOCK) ON [s].[name] = [dp].[default_schema_name]
WHERE   [dp].[type] = N'A'
) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.SqlRoleMembershipPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [dp1].[principal_id]          AS [MemberId],
        [dp1].[name]                  AS [MemberName],
        [dp2].[principal_id]          AS [RoleId],
        [dp2].[name]                  AS [RoleName]
FROM
        [sys].[database_role_members] [drm] WITH (NOLOCK)
        INNER JOIN [sys].[database_principals] [dp1] WITH (NOLOCK) ON [dp1].[principal_id] = [drm].[member_principal_id]
        INNER JOIN [sys].[database_principals] [dp2] WITH (NOLOCK) ON [dp2].[principal_id] = [drm].[role_principal_id]
WHERE USER_NAME([drm].[member_principal_id]) != N'cdc'
) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql120FilegroupPopulator' as [PopulatorName];
SELECT * FROM (SELECT  
        [data_space_id]     AS [FilegroupId],
        [name]              AS [Name],
        [is_default]        AS [IsDefault],
        [is_read_only]      AS [IsReadOnly],
        [type]              AS [Type]
FROM    
        [sys].[filegroups] WITH (NOLOCK)
WHERE   
        ([is_system] IS NULL OR [is_system] = 0) AND [type] <> N'FX') AS [_results] ORDER BY FilegroupId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90FullTextCatalogPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [sfc].[fulltext_catalog_id] AS [ObjectId],
        [sfc].[name] AS [Name],
        [sfc].[principal_id] AS [OwnerId],
        [sdp].[name] AS [Owner],
        [sfc].[path] AS [Path],
        [sfc].[is_default] AS [IsDefault],
        [sfc].[is_accent_sensitivity_on] AS [IsAccentSensitivityOn],
        [sfc].[data_space_id] AS [FilegroupId],
        [sds].[name] AS [FilegroupName]
FROM    [sys].[fulltext_catalogs] [sfc] WITH (NOLOCK)
LEFT    JOIN [sys].[database_principals] [sdp] WITH (NOLOCK) ON [sdp].[principal_id] = [sfc].[principal_id]
LEFT    JOIN [sys].[data_spaces] [sds] WITH (NOLOCK) ON [sds].[data_space_id] = [sfc].[data_space_id]
) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90AssemblyPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [sdp].[principal_id]   AS [OwnerId],
        [sdp].[name]           AS [OwnerName], 
        [sas].[name]           AS [Name], 
        [sas].[assembly_id]    AS [ObjectId], 
        [sas].[is_visible]     AS [IsVisible], 
        [sas].[permission_set] AS [PermissionSet], 
        [sas].[clr_name]       AS [AssemblyFullName],
        [saf].[content]       AS [AssemblyBits]
FROM 
        [sys].[assemblies] AS [sas] WITH (NOLOCK)
LEFT    JOIN [sys].[database_principals] [sdp] WITH (NOLOCK) ON [sdp].[principal_id] = [sas].[principal_id]
LEFT	JOIN [sys].[assembly_files] AS [saf] WITH (NOLOCK) ON [saf].[assembly_id] = [sas].[assembly_id]
WHERE	[saf].[file_id] = 1
    AND [sas].[is_user_defined] = 1
) AS [_results] ORDER BY ObjectId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90CertificatePopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [sct].[certificate_id]          AS [ObjectId],
        [sct].[name]                    AS [Name],
        [sdp].[name]                    AS [Owner],
        [sdp].[principal_id]            AS [OwnerId],
        [sct].[pvt_key_encryption_type] AS [KeyEncryptionType],
        [sct].[cert_serial_number]      AS [SerialNumber],
        [sct].[issuer_name]             AS [Issuer],
        [sct].[subject]                 AS [Subject],
        [sct].[start_date]              AS [StartDate],
        [sct].[expiry_date]             AS [ExpiryDate],
        [sct].[thumbprint]              AS [ThumbPrint],
        CONVERT(bit, [sct].[is_active_for_begin_dialog]) AS [IsActiveForBeginDialog]
FROM
        [sys].[certificates] [sct] WITH (NOLOCK)
LEFT    JOIN [sys].[database_principals] [sdp] WITH (NOLOCK) ON [sdp].[principal_id] = [sct].[principal_id]
WHERE
        [sct].[name] NOT LIKE '##%##'
) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90SymmetricKeyPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [ssk].[name] AS [KeyName],
        [ssk].[symmetric_key_id] AS [KeyId],
        [sdp].[name] AS [Owner],
        [sdp].[principal_id] AS [OwnerId],
        [ssk].[key_length] AS [KeyLength],
        [ssk].[key_algorithm] AS [KeyAlgorithm],
        [ske].[crypt_type] AS [EncryptionType]
FROM    [sys].[symmetric_keys] [ssk] WITH (NOLOCK)
LEFT    JOIN [sys].[database_principals] [sdp] WITH (NOLOCK) ON [sdp].[principal_id] = [ssk].[principal_id]
LEFT    JOIN [sys].[key_encryptions] [ske] WITH (NOLOCK) ON [ske].[key_id] = [ssk].[symmetric_key_id] AND [ske].[crypt_type] IN ('ESKP','ESP2','ESP3')) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90SymmetricKeyEncryptionMechanismPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [encrypt].[key_id] AS [KeyId],
        [encrypt].[crypt_type] AS [EncryptionType],
        [ensk].[symmetric_key_id] AS [EncryptionKeyId],
        [ensk].[name] AS [EncryptionKeyName],
        [cert].[certificate_id] AS [EncryptionCertId],
        [cert].[name] AS [EncryptionCertName],
        [enask].[asymmetric_key_id] AS [EncryptionAKeyId],
        [enask].[name] AS [EncryptionAKeyName],
        [key].[name] AS [SymmetricKeyName]
FROM    [sys].[key_encryptions] [encrypt] WITH (NOLOCK)
INNER   JOIN [sys].[symmetric_keys] [key] WITH (NOLOCK) ON [key].[symmetric_key_id] = [encrypt].[key_id]
LEFT    JOIN [sys].[symmetric_keys] [ensk] WITH (NOLOCK) ON [ensk].[key_guid] = [encrypt].[thumbprint]
LEFT    JOIN [sys].[certificates] [cert] WITH (NOLOCK) ON [cert].[thumbprint] = [encrypt].[thumbprint]
LEFT    JOIN [sys].[asymmetric_keys] [enask] WITH (NOLOCK) ON [enask].[thumbprint] = [encrypt].[thumbprint]
) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90SchemaPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [ssc].[schema_id]    AS [SchemaId],
        [ssc].[name]         AS [Name], 
        [ssc].[principal_id] AS [AuthorizerId],
        [sdp].[name]         AS [AuthorizerName] 
FROM    [sys].[schemas] [ssc] WITH (NOLOCK)
LEFT    JOIN [sys].[database_principals] [sdp] WITH (NOLOCK) ON [sdp].[principal_id] = [ssc].[principal_id]
WHERE   [ssc].[name] != N'cdc'
) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90XmlSchemaCollectionPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [xsc].[xml_collection_id]      AS [ObjectId],
        [xsc].[schema_id]              AS [SchemaId],
        SCHEMA_NAME([xsc].[schema_id]) AS [SchemaName],
        [xsc].[name]                   AS [Name],
        XML_SCHEMA_NAMESPACE(SCHEMA_NAME([xsc].[schema_id]), [xsc].[name]) AS [Document]
FROM    [sys].[xml_schema_collections] [xsc] WITH (NOLOCK)
WHERE   xsc.name <> N'sys'
) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90UserDefinedDataTypePopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [st].[user_type_id]             AS [TypeId],
        [st].[name]                     AS [Name],
        [st].[schema_id]                AS [OwnerId],
        SCHEMA_NAME([st].[schema_id])   AS [Owner],
        [st].[is_nullable]              AS [AllowNulls],
        [bt].[user_type_id]             AS [BaseTypeId],
        [bt].[name]                     AS [BaseType],
        CASE WHEN [st].[max_length] >= 0 AND [bt].[name] IN (N'nchar', N'nvarchar') 
             THEN [st].[max_length] / 2 
             ELSE [st].[max_length] 
        END                             AS [Length],
        [st].[precision]                AS [Precision],
        [st].[scale]                    AS [Scale],
        [st].[collation_name]           AS [CollationName]
FROM    [sys].[types] [st] WITH (NOLOCK)
LEFT    JOIN [sys].[database_principals] [dp] WITH (NOLOCK) ON [dp].[principal_id]   = [st].[principal_id]
LEFT    JOIN [sys].[types]               [bt] WITH (NOLOCK) ON [st].[system_type_id] = [bt].[system_type_id] AND [bt].[system_type_id] = [bt].[user_type_id]
WHERE   [st].[is_user_defined] = 1 AND [st].[is_assembly_type] = 0 AND [st].[is_table_type] = 0
) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90UserDefinedTypePopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [st].[user_type_id]             AS [TypeId],
        [st].[name]                     AS [Name],
        [st].[schema_id]                AS [OwnerId],
        SCHEMA_NAME([st].[schema_id])   AS [Owner],
        [at].[assembly_id]              AS [AssemblyId],
        [as].[name]                     AS [Assembly],
        [at].[assembly_class]           AS [AssemblyClass]
FROM    [sys].[types] [st] WITH (NOLOCK)
LEFT    JOIN [sys].[database_principals] [dp] WITH (NOLOCK) ON [dp].[principal_id]   = [st].[principal_id]
LEFT    JOIN [sys].[types]               [bt] WITH (NOLOCK) ON [st].[system_type_id] = [bt].[system_type_id] AND [bt].[system_type_id] = [bt].[user_type_id]
LEFT    JOIN [sys].[objects]             [ds] WITH (NOLOCK) ON [ds].[object_id]      = [st].[default_object_id]
LEFT    JOIN [sys].[assembly_types]      [at] WITH (NOLOCK) ON [at].[user_type_id]   = [st].[user_type_id]
LEFT    JOIN [sys].[assemblies]          [as] WITH (NOLOCK) ON [as].[assembly_id]    = [at].[assembly_id]
WHERE   [st].[is_user_defined] = 1 AND [st].[is_assembly_type] = 1
) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql120TableTypePopulator' as [PopulatorName];
SELECT * FROM (
SELECT  
        [tt].[name]                   AS [Name],
        [tt].[schema_id]              AS [OwnerId],
        SCHEMA_NAME([tt].[schema_id]) AS [OwnerName],
        [tt].[user_type_id]           AS [TableTypeId],
        [o].[create_date]             AS [CreatedDate],
        [tt].[is_memory_optimized]    AS [IsMemoryOptimized]
FROM    
        [sys].[table_types] [tt] WITH (NOLOCK)
        INNER JOIN [sys].[objects] as [o] WITH (NOLOCK) ON [tt].[type_table_object_id] = [o].[object_id]
WHERE   
        [tt].[is_user_defined] = 1
) AS [_results] ORDER BY TableTypeId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql100TableColumnTypePopulator' as [PopulatorName];
SELECT * FROM (
SELECT  
        [t].[name] AS [ColumnSourceName], 
        [t].[user_type_id] AS [TableId],
        SCHEMA_NAME([t].[schema_id]) AS [SchemaName], 
        [c].[name] AS [ColumnName], 
        [c].[user_type_id] AS [TypeId],
        [types].[name] AS [TypeName],
        [basetypes].[name] AS [BaseTypeName],
        CONVERT(bit, ISNULL([types].[is_user_defined], 0)) AS [IsUserDefinedType],
        SCHEMA_NAME([types].[schema_id]) AS [TypeSchemaName],
        [c].[column_id] AS [ColumnId], 
        [c].[precision] AS [Precision],
        [c].[scale] AS [Scale],
        CASE WHEN [c].[max_length] >= 0 AND [types].[name] IN (N'nchar', N'nvarchar') THEN ([c].[max_length] / 2) ELSE [c].[max_length] END AS [Length],
        CONVERT(bit, [c].[is_identity]) AS [IsIdentity],
        CONVERT(bit, [c].[is_computed]) AS [IsComputed],
        CAST(ISNULL([ic].[seed_value], 0) AS DECIMAL(38)) AS [IdentitySeed],
        CAST(ISNULL([ic].[increment_value], 0) AS DECIMAL(38)) AS [IdentityIncrement],
        CONVERT(bit, [c].[is_nullable]) AS [IsNullable],
        [cc].[definition] AS [ComputedText],
        [c].[is_rowguidcol] AS [IsRowGuidColumn],
        [c].[collation_name] AS [Collation],
        [c].[is_xml_document] AS [IsXmlDocument],
        [c].[xml_collection_id] AS [XmlCollectionId],
        [xscs].[name] AS [XmlCollection],
        SCHEMA_NAME([xscs].[schema_id]) AS [XmlCollectionSchemaName],
        CONVERT(bit, ISNULL([cc].[is_persisted], 0)) AS [IsPersisted],
        CAST(0 AS BIT) AS [IsPrimaryKey]
FROM    
        [sys].[columns] [c] WITH (NOLOCK)
        INNER JOIN [sys].[table_types] [t] WITH (NOLOCK) ON [c].[object_id] = [t].[type_table_object_id]
        LEFT JOIN [sys].[types] [basetypes] WITH (NOLOCK) ON [c].[system_type_id] = [basetypes].[system_type_id] AND [basetypes].[system_type_id] = [basetypes].[user_type_id]
        LEFT JOIN [sys].[types] [types] WITH (NOLOCK) ON [c].[user_type_id] = [types].[user_type_id]
        LEFT JOIN [sys].[identity_columns] [ic] WITH (NOLOCK) ON [ic].[object_id] = [c].[object_id] AND [ic].[column_id] = [c].[column_id]
        LEFT JOIN [sys].[computed_columns] [cc] WITH (NOLOCK) ON [cc].[object_id] = [c].[object_id] AND [cc].[column_id] = [c].[column_id]
        LEFT JOIN [sys].[xml_schema_collections] [xscs] WITH (NOLOCK) ON [xscs].[xml_collection_id] = [c].[xml_collection_id]        
WHERE   
        [t].[is_user_defined] = 1
) AS [_results] ORDER BY TableId,ColumnId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql130TableTypeUniqueKeyPopulator' as [PopulatorName];
SELECT * FROM (
SELECT DISTINCT
            SCHEMA_NAME([tt].[schema_id]) AS [SchemaName]
           ,[tt].[user_type_id]        AS [ColumnSourceId]
           ,[tt].[name]             AS [ColumnSourceName]
           ,[i].[index_id]         AS [ConstraintId]
           ,CONVERT(bit, CASE [i].[type] WHEN 1 THEN 1 ELSE 0 END) 
                                   AS [IsClustered]
           ,[i].[ignore_dup_key]   AS [DoIgnoreDuplicateKey]   
           ,[i].[name]             AS [Name]
           ,CONVERT(BIT, CASE WHEN [hi].[object_id] IS NULL THEN 0 ELSE 1 END) AS [IsHash]
           ,[hi].[bucket_count] AS [BucketCount]
FROM 
            [sys].[indexes] AS [i] WITH (NOLOCK)
            INNER JOIN [sys].[objects] AS [o]  WITH (NOLOCK) ON [i].[object_id] = [o].[object_id]
            INNER JOIN [sys].[table_types] AS [tt] WITH (NOLOCK) ON [o].[object_id] = [tt].[type_table_object_id]
            LEFT OUTER JOIN [sys].[hash_indexes] AS [hi] WITH (NOLOCK) ON [hi].[object_id] = [i].[object_id] AND [hi].[index_id] = [i].[index_id]
WHERE 
            [o].[type] = N'TT'
            AND OBJECTPROPERTY([o].[object_id], N'IsSystemTable') = 0 
            AND [i].[name] IS NOT NULL
            AND [i].[is_hypothetical] = 0
            AND [i].[is_primary_key] = 0
            AND [i].[is_unique_constraint] = 1
            AND	[o].[object_id] NOT IN (SELECT [major_id] FROM [sys].[extended_properties] WITH (NOLOCK) WHERE [minor_id] = 0 AND [class] = 1 AND [name] = N'microsoft_database_tools_support')  
) AS [_results] ORDER BY ColumnSourceId,ConstraintId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql100TableTypeConstraintColumnPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  
    SCHEMA_NAME([tt].[schema_id])  AS [SchemaName]
   ,[tt].[user_type_id]           AS [ColumnSourceId]
   ,[tt].[name]               AS [ColumnSourceName]
   ,[i].[index_id]           AS [IndexId]
   ,[i].[name]                AS [IndexName]
   ,[c].[column_id]           AS [ColumnId]
   ,[c].[name]                AS [ColumnName]
   ,[ic].[is_included_column] AS [IsIncludedColumn]
   ,[ic].[key_ordinal]        AS [KeyOrdinal]
   ,[ic].[is_descending_key]  AS [IsDescending]
FROM 
    [sys].[columns] AS [c] WITH (NOLOCK)
    LEFT JOIN [sys].[index_columns] AS [ic] WITH (NOLOCK) ON [c].[object_id] = [ic].[object_id] AND [c].[column_id] = [ic].[column_id]
    LEFT JOIN [sys].[indexes]       AS [i] WITH (NOLOCK) ON [i].[object_id] = [ic].[object_id] AND [i].[index_id] = [ic].[index_id]
    LEFT JOIN [sys].[objects]       AS [o] WITH (NOLOCK) ON [o].[object_id] = [i].[object_id]
    LEFT JOIN [sys].[table_types]  AS [tt] WITH (NOLOCK) ON [o].[object_id] = [tt].[type_table_object_id]
WHERE 
    [o].[type] = N'TT' 
    AND OBJECTPROPERTY([o].[object_id], N'IsSystemTable') = 0 
    AND [i].[name] IS NOT NULL
    AND [i].[is_hypothetical] = 0
    AND [i].[is_primary_key] = 0
    AND [i].[is_unique_constraint] = 1
    AND	[o].[object_id] NOT IN (SELECT [major_id] FROM [sys].[extended_properties] WITH (NOLOCK) WHERE [minor_id] = 0 AND [class] = 1 AND [name] = N'microsoft_database_tools_support')
) AS [_results] ORDER BY ColumnSourceId,IndexId,KeyOrdinal ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql120TableTypePrimaryKeyPopulator' as [PopulatorName];
SELECT * FROM (
SELECT DISTINCT
    SCHEMA_NAME([tt].[schema_id]) AS [SchemaName]
   ,[tt].[user_type_id]        AS [ColumnSourceId]
   ,[tt].[name]             AS [ColumnSourceName]
   ,[i].[index_id]         AS [ConstraintId]
   ,CONVERT(bit, CASE [i].[type] WHEN 1 THEN 1 ELSE 0 END) 
                           AS [IsClustered]
   ,[i].[ignore_dup_key]   AS [DoIgnoreDuplicateKey]   
   ,[i].[name]             AS [Name]
   ,CONVERT(BIT, CASE WHEN [hi].[object_id] IS NULL THEN 0 ELSE 1 END) AS [IsHash]
   ,[hi].[bucket_count] AS [BucketCount]
FROM 
    [sys].[indexes] AS [i] WITH (NOLOCK)
    INNER JOIN [sys].[objects] AS [o]  WITH (NOLOCK) ON [i].[object_id] = [o].[object_id]
    INNER JOIN [sys].[table_types] AS [tt] WITH (NOLOCK) ON [o].[object_id] = [tt].[type_table_object_id]
    LEFT OUTER JOIN [sys].[hash_indexes] AS [hi] WITH (NOLOCK) ON [hi].[object_id] = [i].[object_id] AND [hi].[index_id] = [i].[index_id]
WHERE 
    [o].[type] = N'TT'
    AND OBJECTPROPERTY([o].[object_id], N'IsSystemTable') = 0 
    AND [i].[name] IS NOT NULL
    AND [i].[is_hypothetical] = 0
    AND [i].[is_primary_key] = 1
    AND	[o].[object_id] NOT IN (SELECT [major_id] FROM [sys].[extended_properties] WITH (NOLOCK) WHERE [minor_id] = 0 AND [class] = 1 AND [name] = N'microsoft_database_tools_support')  
) AS [_results] ORDER BY ColumnSourceId,ConstraintId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql100TableTypeConstraintColumnPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  
    SCHEMA_NAME([tt].[schema_id])  AS [SchemaName]
   ,[tt].[user_type_id]           AS [ColumnSourceId]
   ,[tt].[name]               AS [ColumnSourceName]
   ,[i].[index_id]           AS [IndexId]
   ,[i].[name]                AS [IndexName]
   ,[c].[column_id]           AS [ColumnId]
   ,[c].[name]                AS [ColumnName]
   ,[ic].[is_included_column] AS [IsIncludedColumn]
   ,[ic].[key_ordinal]        AS [KeyOrdinal]
   ,[ic].[is_descending_key]  AS [IsDescending]
FROM 
    [sys].[columns] AS [c] WITH (NOLOCK)
    LEFT JOIN [sys].[index_columns] AS [ic] WITH (NOLOCK) ON [c].[object_id] = [ic].[object_id] AND [c].[column_id] = [ic].[column_id]
    LEFT JOIN [sys].[indexes]       AS [i] WITH (NOLOCK) ON [i].[object_id] = [ic].[object_id] AND [i].[index_id] = [ic].[index_id]
    LEFT JOIN [sys].[objects]       AS [o] WITH (NOLOCK) ON [o].[object_id] = [i].[object_id]
    LEFT JOIN [sys].[table_types]  AS [tt] WITH (NOLOCK) ON [o].[object_id] = [tt].[type_table_object_id]
WHERE 
    [o].[type] = N'TT' 
    AND OBJECTPROPERTY([o].[object_id], N'IsSystemTable') = 0 
    AND [i].[name] IS NOT NULL
    AND [i].[is_hypothetical] = 0
    AND [i].[is_primary_key] = 1
    AND	[o].[object_id] NOT IN (SELECT [major_id] FROM [sys].[extended_properties] WITH (NOLOCK) WHERE [minor_id] = 0 AND [class] = 1 AND [name] = N'microsoft_database_tools_support')
) AS [_results] ORDER BY ColumnSourceId,IndexId,KeyOrdinal ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql130TableTypeIndexPopulator' as [PopulatorName];
SELECT * FROM (SELECT DISTINCT
    SCHEMA_NAME([tt].[schema_id]) AS [SchemaName]
   ,[tt].[user_type_id]        AS [ColumnSourceId]
   ,[tt].[name]             AS [ColumnSourceName]
   ,'TT'  AS [ColumnSourceType]
   ,[i].[index_id]         AS [IndexId]
   ,[i].[name]             AS [IndexName]
   ,[f].[type]             AS [DataspaceType]
   ,[f].[data_space_id]    AS [DataspaceId]
   ,[f].[name]             AS [DataspaceName]
   ,NULL  AS [FileStreamId]
   ,[ds].[name]            AS [FileStreamName]
   ,[ds].[type]            AS [FileStreamType]   
   ,[i].[fill_factor]      AS [FillFactor]    
   ,CONVERT(bit, CASE [i].[type] WHEN 1 THEN 1 ELSE 0 END) 
                           AS [IsClustered]
   ,[i].[is_unique]        AS [IsUnique]
   ,[i].[is_padded]        AS [IsPadded]
   ,[i].[ignore_dup_key]   AS [DoIgnoreDuplicateKey]
   ,[t].[no_recompute]     AS [NoRecomputeStatistics]
   ,[i].[allow_row_locks]  AS [DoAllowRowLocks]
   ,[i].[allow_page_locks] AS [DoAllowPageLocks]
   ,[i].[is_disabled]      AS [IsDisabled]
   ,[i].[filter_definition]
                           AS [Predicate]
   ,CONVERT(bit, 1)        AS [EqualsParentDataSpace]
   ,[i].[type]             AS [IndexType]
   ,CONVERT(BIT, CASE WHEN [hi].[object_id] IS NULL THEN 0 ELSE 1 END) AS [IsHash]
   ,[hi].[bucket_count] AS [BucketCount]
FROM 
    [sys].[indexes] AS [i] WITH (NOLOCK)
    INNER JOIN [sys].[objects]           AS [o]  WITH (NOLOCK) ON [i].[object_id] = [o].[object_id]
    INNER JOIN [sys].[table_types] AS [tt] WITH (NOLOCK) ON [o].[object_id] = [tt].[type_table_object_id]
    LEFT  JOIN [sys].[data_spaces]       AS [f]  WITH (NOLOCK) ON [i].[data_space_id] = [f].[data_space_id]
    LEFT  JOIN [sys].[stats]             AS [t]  WITH (NOLOCK) ON [t].[object_id] = [i].[object_id] AND [t].[name] = [i].[name]
    LEFT  JOIN [sys].[tables]            AS [ta] WITH (NOLOCK) ON [ta].[object_id] = [i].[object_id]
    LEFT  JOIN [sys].[data_spaces]       AS [ds] WITH (NOLOCK) ON [ds].[data_space_id] = [ta].[filestream_data_space_id]
    LEFT OUTER JOIN [sys].[hash_indexes] AS [hi] WITH (NOLOCK) ON [hi].[object_id] = [i].[object_id] AND [hi].[index_id] = [i].[index_id]
WHERE [i].[is_primary_key] = 0
    AND [i].[is_unique_constraint] = 0
    AND [i].[is_hypothetical] = 0
    AND [i].[name] IS NOT NULL) AS [_results] ORDER BY ColumnSourceId,IndexId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql120TableTypeIndexColumnPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  
    SCHEMA_NAME([tt].[schema_id]) AS [SchemaName]
   ,[tt].[user_type_id]        AS [ColumnSourceId]
   ,[tt].[name]             AS [ColumnSourceName]
   ,'TT'  AS [ColumnSourceType]
   ,[i].[index_id]           AS [IndexId]
   ,[i].[name]               AS [IndexName]
   ,[c].[column_id]          AS [ColumnId]
   ,[c].[name]               AS [ColumnName]
   ,[ic].[is_descending_key] AS [IsDescending]
   ,[ic].[is_included_column] AS [IsIncludedColumn]
   ,[ic].[partition_ordinal]  AS [PartitionOrdinal]
   ,[ic].[key_ordinal]        AS [KeyOrdinal]
   ,[i].[type]                AS [IndexType]
FROM 
    [sys].[columns] AS [c] WITH (NOLOCK)
    INNER JOIN [sys].[table_types] AS [tt] WITH (NOLOCK) ON [c].[object_id] = [tt].[type_table_object_id]
    LEFT JOIN [sys].[index_columns] AS [ic] WITH (NOLOCK) ON [c].[object_id] = [ic].[object_id] AND [c].[column_id] = [ic].[column_id]
    LEFT JOIN [sys].[indexes]       AS [i] WITH (NOLOCK) ON [i].[object_id] = [ic].[object_id] AND [i].[index_id] = [ic].[index_id]
    LEFT JOIN [sys].[objects]   AS [o] WITH (NOLOCK) ON [o].[object_id] = [i].[object_id]
    LEFT JOIN [sys].[tables] as [t] WITH (NOLOCK) ON [t].[object_id] = [c].[object_id]
WHERE [i].[is_primary_key] = 0
    AND [i].[is_unique_constraint] = 0
    AND [i].[name] IS NOT NULL
    AND [i].[is_hypothetical] = 0) AS [_results] ORDER BY ColumnSourceId,IndexId,KeyOrdinal ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql100TableTypeCheckConstraintPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        SCHEMA_NAME([t].[schema_id])        AS [SchemaName]
       ,[t].[user_type_id]                  AS [TableTypeId]
       ,[t].[name]                          AS [TableTypeName]
       ,[cc].[definition]                   AS [Script]
       ,[cc].[object_id]                    AS [ConstraintId]
       ,[cc].[name]                         AS [Name]
FROM	
        [sys].[check_constraints] AS [cc] WITH (NOLOCK)	
        INNER JOIN [sys].[table_types] AS [t] with (NOLOCK) on [t].[type_table_object_id] = [cc].[parent_object_id]
) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql100TableTypeDefaultConstraintPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [d].[object_id]                     AS [ConstraintId]
       ,SCHEMA_NAME([t].[schema_id])        AS [SchemaName]
       ,[t].[user_type_id]                  AS [TableTypeId]
       ,[t].[name]                          AS [TableTypeName]
       ,[c].[column_id]                     AS [ColumnId]
       ,[c].[name]                          AS [ColumnName]
       ,[d].[definition]                    AS [Script]
       ,[d].[name]                          AS [Name]
FROM
        [sys].[default_constraints]    AS [d] WITH (NOLOCK)
        INNER JOIN [sys].[columns] AS [c] WITH (NOLOCK) ON [c].[object_id] = [d].[parent_object_id] AND [c].[column_id] = [d].[parent_column_id]		
        INNER JOIN [sys].[table_types] AS [t] with (NOLOCK) on [t].[type_table_object_id] = [d].[parent_object_id]
WHERE
        OBJECTPROPERTY([d].[parent_object_id], N'IsSystemTable') = 0 
) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90PartitionFunctionPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [pf].[function_id]             AS [ObjectId],
        [pf].[name]                    AS [Name],
        [pf].[fanout]                  AS [FanOut],
        [pf].[boundary_value_on_right] AS [BoundaryValueOnRight],
        [p].[system_type_id]           AS [TypeId],
        [types].[name]                 AS [TypeName],
        CASE WHEN   [p].[max_length] >= 0
            AND     [types].[name] IN (N'nchar', N'nvarchar')
            THEN [p].[max_length] / 2
            ELSE [p].[max_length]
            END                        AS [Length],
        [p].[precision]                AS [Precision],
        [p].[scale]                    AS [Scale]
FROM
        [sys].[partition_functions] [pf] WITH (NOLOCK)
LEFT    JOIN [sys].[partition_parameters] [p] WITH (NOLOCK) ON [p].[function_id] = [pf].[function_id]
LEFT    JOIN [sys].[types] [types] WITH (NOLOCK) ON [p].[system_type_id] = [types].[system_type_id] AND [types].[system_type_id] = [types].[user_type_id]
WHERE   [pf].[is_system] = 0
) AS [_results] ORDER BY ObjectId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90PartitionRangeValuePopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [pr].[function_id] AS [PartitionFunctionId],
        [pr].[value]       AS [Value],
        SQL_VARIANT_PROPERTY([pr].[value], N'BaseType') AS [BaseType],
        [pf].[name]        AS [Name],
        [pr].[boundary_id] AS [BoundaryId]
FROM    [sys].[partition_range_values] [pr] WITH (NOLOCK)
    INNER JOIN [sys].[partition_functions] [pf] WITH (NOLOCK) ON [pr].[function_id] = [pf].[function_id]
WHERE   [pf].[is_system] = 0) AS [_results] ORDER BY PartitionFunctionId,BoundaryId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90PartitionSchemePopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [s].[data_space_id] AS [ObjectId],
        [s].[name]          AS [Name],
        [s].[function_id]   AS [FunctionId],
        [f].[name]          AS [FunctionName]
FROM
        [sys].[partition_schemes] [s] WITH (NOLOCK)
LEFT    JOIN [sys].[partition_functions] [f] WITH (NOLOCK) ON [f].[function_id] = [s].[function_id]
WHERE   [s].[is_system] IS NULL OR [s].[is_system] = 0) AS [_results] ORDER BY ObjectId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90PartitionSchemeFilegroupPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [s].[data_space_id] AS [ObjectId],
        [s].[name]          AS [Name],
        [dd].[data_space_id] AS [FileGroupId],
        [d].[name]           AS [FileGroup],
        [dd].[destination_id] AS [DestinationId]
FROM
        [sys].[partition_schemes] [s] WITH (NOLOCK)
INNER   JOIN [sys].[partition_functions] [f] WITH (NOLOCK) ON [f].[function_id] = [s].[function_id]
INNER   JOIN [sys].[destination_data_spaces] [dd] WITH (NOLOCK) ON [dd].[partition_scheme_id] = [s].[data_space_id]
INNER   JOIN [sys].[data_spaces] [d] WITH (NOLOCK) ON [d].[data_space_id] = [dd].[data_space_id]
WHERE   ([s].[is_system] IS NULL OR [s].[is_system] = 0)
        AND ([d].[is_system] IS NULL OR [d].[is_system] = 0)) AS [_results] ORDER BY ObjectId,DestinationId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql130FunctionPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [s].[schema_id]        AS [OwnerId]
       ,SCHEMA_NAME([s].[schema_id]) AS [FunctionOwner]
       ,[s].[object_id]        AS [FunctionId]
       ,[s].[name]             AS [FunctionName]
       ,[s].[type]             AS [FunctionType]
       ,CONVERT(bit, ISNULL([sm].[uses_ansi_nulls], 0)) 
                               AS [IsAnsiNulls]
       ,CONVERT(bit, ISNULL([sm].[uses_quoted_identifier], 0)) 
                               AS [IsQuotedIdentifier]
       ,[sm].[definition]      AS [Script]
       ,CASE WHEN [s].[is_published] <> 0 OR [s].[is_schema_published] <> 0 THEN 1 ELSE 0 END AS [ReplInfo]
       ,[s].[create_date]            AS [CreatedDate]
       ,[sm].[is_schema_bound] AS [IsSchemaBound]
       ,[sm].[uses_native_compilation] AS [UsesNativeCompilation]
       ,[sm].[execute_as_principal_id] AS [ExecuteAsId]
       ,[p].[name] AS [ExecuteAsName]
       ,CAST([sm].[null_on_null_input] AS BIT) AS [NullOnNullInput]
       ,NULL AS [HasAmbiguousReference]
FROM   
        [sys].[objects] AS [s] WITH (NOLOCK)
        LEFT JOIN [sys].[sql_modules]         AS [sm] WITH (NOLOCK) ON [sm].[object_id] = [s].[object_id]
        LEFT JOIN [sys].[database_principals] [p] WITH (NOLOCK) ON [p].[principal_id] = [sm].[execute_as_principal_id]   
        
WHERE   
        [s].[type] IN (N'IF', N'FN', N'TF', N'FT', N'FS') 
        AND ([s].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [s].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )) AND OBJECTPROPERTY([s].[object_id], N'IsEncrypted') = 0
        AND SCHEMA_NAME([s].[schema_id]) <> N'cdc'
) AS [_results] WHERE ( 1 = (select TOP 1 1 FROM @SystemTableTypeDiscriminators as [t] where [t].[TypeCode] COLLATE DATABASE_DEFAULT = [_results].[FunctionType]));
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql100EncryptedAndClrFunctionPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [s].[schema_id]        AS [OwnerId]
       ,SCHEMA_NAME([s].[schema_id])
                               AS [FunctionOwner]
       ,[s].[object_id]        AS [FunctionId]
       ,[s].[name]             AS [FunctionName]
       ,[s].[type]             AS [FunctionType]
       ,CONVERT(bit, ISNULL([sm].[uses_ansi_nulls], 0))
                               AS [IsAnsiNulls]
       ,CONVERT(bit, ISNULL([sm].[uses_quoted_identifier], 0))
                               AS [IsQuotedIdentifier]
       ,CONVERT(bit, ISNULL([sm].[is_schema_bound], 0))
                               AS [IsSchemaBound]
       ,NULL                   AS [AssemblyId]
       ,NULL                   AS [Assembly]
       ,NULL                   AS [AssemblyClass]
       ,NULL                 AS [AssemblyMethod]
       ,[sm].[execute_as_principal_id] AS [ExecuteAsId]
       ,[p].[name] AS [ExecuteAsName]
       ,CASE WHEN [s].[is_published] <> 0 OR [s].[is_schema_published] <> 0 THEN 1 ELSE 0 END AS [ReplInfo]
       ,[s].[create_date]  AS [CreatedDate]
      ,CAST([sm].[null_on_null_input] AS BIT) AS [NullOnNullInput]
FROM
        [sys].[objects] AS [s] WITH (NOLOCK)
        LEFT JOIN [sys].[sql_modules]    AS [sm] WITH (NOLOCK) ON [sm].[object_id] = [s].[object_id]
        LEFT JOIN [sys].[database_principals] [p] WITH (NOLOCK) ON [p].[principal_id] = [sm].[execute_as_principal_id]
WHERE
        [s].[type] IN (N'IF', N'FN', N'TF') AND ([s].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [s].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )) AND OBJECTPROPERTY([s].[object_id], N'IsEncrypted') = 1

UNION

SELECT
        [s].[schema_id]        AS [OwnerId]
       ,SCHEMA_NAME([s].[schema_id])
                               AS [FunctionOwner]
       ,[s].[object_id]        AS [FunctionId]
       ,[s].[name]             AS [FunctionName]
       ,[s].[type]             AS [FunctionType]
       ,NULL                   AS [IsAnsiNulls]
       ,NULL                   AS [IsQuotedIdentifier]
       ,NULL                   AS [IsSchemaBound]
       ,[as].[assembly_id]            AS [AssemblyId]
       ,[as].[name]                   AS [Assembly]
       ,[am].[assembly_class]         AS [AssemblyClass]
       ,[am].[assembly_method]        AS [AssemblyMethod]
       ,[am].[execute_as_principal_id] AS [ExecuteAsId]
       ,[p].[name] AS [ExecuteAsName]
       ,CASE WHEN [s].[is_published] <> 0 OR [s].[is_schema_published] <> 0 THEN 1 ELSE 0 END AS [ReplInfo]
       ,[s].[create_date]  AS [CreatedDate]
       ,CAST(0 as BIT) AS [NullOnNullInput]
FROM
        [sys].[objects] AS [s] WITH (NOLOCK)
        LEFT JOIN [sys].[assembly_modules]    AS [am] WITH (NOLOCK) ON [am].[object_id] = [s].[object_id]
        LEFT JOIN [sys].[assemblies]          AS [as] WITH (NOLOCK) ON [as].[assembly_id] = [am].[assembly_id]
        LEFT JOIN [sys].[database_principals] [p] WITH (NOLOCK) ON [p].[principal_id] = [am].[execute_as_principal_id]

WHERE
        [s].[type] IN (N'FT', N'FS') AND ([s].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [s].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))
) AS [_results] WHERE ( 1 = (select TOP 1 1 FROM @SystemTableTypeDiscriminators as [t] where [t].[TypeCode] COLLATE DATABASE_DEFAULT = [_results].[FunctionType])) ORDER BY FunctionId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql100FunctionParameterPopulator' as [PopulatorName];
SELECT * FROM (
SELECT    [o].[object_id]              AS [FunctionId]
          ,SCHEMA_NAME([o].[schema_id]) AS [FunctionOwner]
          ,[o].[name]                   AS [FunctionName]
          ,[o].[type]                   AS [FunctionType]
          ,[p].[parameter_id]           AS [ParameterId]
          ,[p].[name]                   AS [ParameterName]
          ,[p].[parameter_id]           AS [ParameterOrder]
          ,[b].[name]                   AS [BaseTypeName]
          ,[p].[user_type_id]           AS [TypeId]
          ,[s].[name]                   AS [TypeSchemaName]
          ,[t].[name]                   AS [TypeName]
          ,CONVERT(bit, ISNULL([t].[is_user_defined], 0)) AS [IsUserDefinedType]
          ,[p].[precision]              AS [Precision]
          ,[p].[scale]                  AS [Scale]
          ,NULL                         AS [OrderColumnId]
          ,NULL                         AS [IsDescending]            
          ,CONVERT(bit, 1)              AS [IsParameter]
          ,CASE WHEN [p].[max_length] >= 0 AND [t].[name] IN (N'nchar', N'nvarchar')
                THEN [p].[max_length]/2 
                ELSE [p].[max_length] 
           END                          AS [Length]
          ,[p].[has_default_value]      AS [HasDefaultValue]
          ,[p].[default_value]          AS [DefaultValue]
          ,NULL                         AS [IsIdentity]
          ,NULL                         AS [IdentitySeed]
          ,NULL                         AS [IdentityIncrement]
          ,[p].[is_output]              AS [IsOutput]
          ,CONVERT(bit, [p].[is_readonly])
                                        AS [IsReadOnly]
          ,NULL                         AS [IsComputed]
          ,NULL                         AS [IsRowGuidColumn]
          ,[p].[is_cursor_ref]          AS [IsCursor]
          ,NULL                         AS [Collation]
          ,NULL                         AS [AllowNulls]
          ,[p].[is_xml_document]        AS [IsXmlDocument]
          ,[p].[xml_collection_id]        AS [XmlCollectionId]
          ,[xscs].[name]                AS [XmlCollection]
          ,SCHEMA_NAME([xscs].[schema_id]) AS [XmlCollectionSchemaName]
FROM       
           [sys].[parameters] [p] WITH (NOLOCK)
           INNER JOIN [sys].[objects] [o] WITH (NOLOCK) ON [p].[object_id] = [o].[object_id]
           LEFT JOIN [sys].[types] [b] WITH (NOLOCK) ON [p].[system_type_id] = [b].[system_type_id] AND [b].[system_type_id] = [b].[user_type_id]
           LEFT JOIN [sys].[types] [t] WITH (NOLOCK) ON [p].[user_type_id] = [t].[user_type_id]
           LEFT JOIN [sys].[schemas] [s] WITH (NOLOCK) ON [s].[schema_id] = [t].[schema_id]
           LEFT JOIN [sys].[xml_schema_collections] [xscs] WITH (NOLOCK) ON [xscs].[xml_collection_id] = [p].[xml_collection_id]
WHERE      
           [o].[type] IN (N'TF', N'FT', N'IF', N'FN', N'FS')
           AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )) AND ((OBJECTPROPERTY([o].[object_id], N'IsEncrypted') = 1) OR [o].[type] IN (N'FS', N'FT'))
) AS [_results] WHERE ( 1 = (select TOP 1 1 FROM @SystemTableTypeDiscriminators as [t] where [t].[TypeCode] COLLATE DATABASE_DEFAULT = [_results].[FunctionType])) ORDER BY FunctionId,ParameterOrder ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql100FunctionParameterPopulator' as [PopulatorName];
SELECT * FROM (
SELECT    [o].[object_id]              AS [FunctionId]
          ,SCHEMA_NAME([o].[schema_id]) AS [FunctionOwner]
          ,[o].[name]                   AS [FunctionName]
          ,[o].[type]                   AS [FunctionType]
          ,[c].[column_id]              AS [ParameterId]
          ,[c].[name]                   AS [ParameterName]
          ,[c].[column_id]              AS [ParameterOrder]
          ,[b].[name]                   AS [BaseTypeName]
          ,[c].[user_type_id]           AS [TypeId]
          ,[s].[name]                   AS [TypeSchemaName]
          ,[t].[name]                   AS [TypeName]
          ,CONVERT(bit, ISNULL([t].[is_user_defined], 0)) AS [IsUserDefinedType]
          ,[c].[precision]              AS [Precision]
          ,[c].[scale]                  AS [Scale]
          ,[oc].[order_column_id]       AS [OrderColumnId]
          ,[oc].[is_descending]         AS [IsDescending]          
          ,CONVERT(bit, 0)              AS [IsParameter]
          ,CASE WHEN [c].[max_length] >= 0 AND [t].[name] IN (N'nchar', N'nvarchar')
                THEN [c].[max_length]/2 
                ELSE [c].[max_length] 
           END                          AS [Length]
          ,CAST(0 AS BIT)               AS [HasDefaultValue]
          ,NULL                         AS [DefaultValue]
          ,[c].[is_identity]            AS [IsIdentity]
          ,CAST(ISNULL([i].[seed_value], 0) AS DECIMAL(38)) 
                                        AS [IdentitySeed]
          ,CAST(ISNULL([i].[increment_value], 0) AS DECIMAL(38))
                                        AS [IdentityIncrement]
          ,NULL                         AS [IsOutput]
          ,NULL                         AS [IsReadOnly]
          ,[c].[is_computed]            AS [IsComputed]
          ,[c].[is_rowguidcol]          AS [IsRowGuidColumn]
          ,NULL                         AS [IsCursor]
          ,[c].[collation_name]         AS [Collation]
          ,CONVERT(bit, [c].[is_nullable]) 
                                        AS [AllowNulls]
          ,[c].[is_xml_document] AS [IsXmlDocument]
          ,[c].[xml_collection_id] AS [XmlCollectionId]
          ,[xscs].[name] AS [XmlCollection]
          ,SCHEMA_NAME([xscs].[schema_id]) AS [XmlCollectionSchemaName]

FROM      [sys].[columns] [c]
          INNER JOIN [sys].[objects] [o] WITH (NOLOCK) ON [o].[object_id] = [c].[object_id]
          LEFT  JOIN [sys].[types] [b] WITH (NOLOCK) ON [c].[system_type_id] = [b].[system_type_id] AND [b].[system_type_id] = [b].[user_type_id]
          LEFT JOIN [sys].[types] [t] WITH (NOLOCK) ON [c].[user_type_id] = [t].[user_type_id]
          LEFT JOIN [sys].[schemas] [s] WITH (NOLOCK) ON [s].[schema_id] = [t].[schema_id]
          LEFT JOIN [sys].[identity_columns] [i] WITH (NOLOCK) ON [i].[object_id] = [c].[object_id] AND [i].[column_id] = [c].[column_id]
          LEFT JOIN [sys].[xml_schema_collections] [xscs] WITH (NOLOCK) ON [xscs].[xml_collection_id] = [c].[xml_collection_id]
          LEFT JOIN [sys].[function_order_columns] [oc] WITH (NOLOCK) ON [oc].[object_id] = [o].[object_id] AND [oc].[column_id] = [c].[column_id]
WHERE      
          [o].[type] IN (N'TF', N'FT', N'IF', N'FN', N'FS')
          AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )) AND ((OBJECTPROPERTY([o].[object_id], N'IsEncrypted') = 1) OR [o].[type] IN (N'FS', N'FT'))) AS [_results] WHERE ( 1 = (select TOP 1 1 FROM @SystemTableTypeDiscriminators as [t] where [t].[TypeCode] COLLATE DATABASE_DEFAULT = [_results].[FunctionType])) ORDER BY FunctionId,ParameterOrder ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90AggregatePopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [o].[schema_id]              AS [SchemaId],
        SCHEMA_NAME([o].[schema_id]) AS [SchemaName],
        [o].[object_id]              AS [AggregateId],
        [o].[name]                   AS [AggregateName],
        [a].[assembly_id]            AS [AssemblyId],
        [a].[name]                   AS [AssemblyName],
        [am].[assembly_class]        AS [AssemblyClass],
        [o].[create_date]            AS [CreatedDate],
        [sm].[execute_as_principal_id] AS [ExecuteAsId],
        [p].[name] AS [ExecuteAsName]
FROM    [sys].[objects] [o] WITH (NOLOCK)
LEFT    JOIN [sys].[sql_modules] [sm] WITH (NOLOCK) ON [sm].[object_id] = [o].[object_id]
LEFT    JOIN [sys].[assembly_modules] [am] WITH (NOLOCK) ON [am].[object_id] = [o].[object_id]
LEFT    JOIN [sys].[assemblies] [a] WITH (NOLOCK) ON [a].[assembly_id] = [am].[assembly_id]
LEFT    JOIN [sys].[database_principals] [p] WITH (NOLOCK) ON [p].[principal_id] = [sm].[execute_as_principal_id]
WHERE   [o].[type] = N'AF' AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))
) AS [_results] ORDER BY AggregateId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90AggregateParametersPopulator' as [PopulatorName];
SELECT * FROM (
SELECT     [o].[object_id]              AS [FunctionId]
          ,SCHEMA_NAME([o].[schema_id]) AS [FunctionOwner]
          ,[o].[name]                   AS [FunctionName]
          ,[p].[parameter_id]           AS [ParameterId]
          ,[p].[name]                   AS [ParameterName]
          ,[p].[parameter_id]           AS [ParameterOrder]
          ,[b].[name]                   AS [BaseTypeName]
          ,[p].[user_type_id]           AS [TypeId]
          ,SCHEMA_NAME([t].[schema_id])      AS [TypeSchemaName]
          ,[t].[name]                   AS [TypeName]
          ,[p].[precision]              AS [Precision]
          ,[p].[scale]                  AS [Scale]
          ,[p].[is_xml_document]        AS [IsXmlDocument]
          ,[p].[xml_collection_id]      AS [XmlCollectionId]
          ,[xscs].[name]                AS [XmlCollection]
          ,SCHEMA_NAME([xscs].[schema_id]) AS [XmlCollectionSchemaName]
          ,CASE WHEN [p].[max_length] >= 0 AND [t].[name] IN (N'nchar', N'nvarchar')
                THEN [p].[max_length]/2
                ELSE [p].[max_length]
           END                          AS [Length]
FROM       [sys].[parameters] [p] WITH (NOLOCK)
INNER JOIN [sys].[objects] [o] WITH (NOLOCK) ON [p].[object_id] = [o].[object_id]
LEFT  JOIN [sys].[types]   [b] WITH (NOLOCK) ON [p].[system_type_id] = [b].[system_type_id] AND [b].[system_type_id] = [b].[user_type_id]
LEFT  JOIN [sys].[types]   [t] WITH (NOLOCK) ON [p].[user_type_id] = [t].[user_type_id]
LEFT  JOIN [sys].[xml_schema_collections] [xscs] WITH (NOLOCK) ON [xscs].[xml_collection_id] = [p].[xml_collection_id]
WHERE      [o].[type] = N'AF' AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) AS [_results] ORDER BY FunctionId,ParameterOrder ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql120ProcedurePopulator' as [PopulatorName];
SELECT * FROM (SELECT
        [sp].[schema_id]              AS [SchemaId]
       ,SCHEMA_NAME([sp].[schema_id]) AS [ProcedureOwner]
       ,[sp].[object_id]              AS [ProcedureId]
       ,[sp].[name]                   AS [ProcedureName]
       ,1                             AS [ProcedureNumber]
       ,CONVERT(bit, ISNULL([sm].[uses_quoted_identifier], 0))
                                      AS [IsQuotedIdentifier]
       ,CONVERT(bit, ISNULL([sm].[uses_ansi_nulls], 0))
                                      AS [IsAnsiNulls]
       ,[sm].[is_recompiled]          AS [IsRecompiled]
       ,CAST(CASE sp.type WHEN N'RF' THEN 1 ELSE 0 END AS bit) AS [IsForReplication]
       ,[sm].[definition]             AS [Script]
       ,1                             AS [SequenceNumber]
       ,0                             AS [SubsequenceNumber]
       ,CASE WHEN [sp].[is_published] <> 0 OR [sp].[is_schema_published] <> 0 THEN 1 ELSE 0 END AS [ReplInfo]
       ,[sp].[create_date]            AS [CreatedDate]
       ,[sm].[execute_as_principal_id] AS [ExecuteAsId]
       ,[p].[name] AS [ExecuteAsName]
       ,[sm].[is_schema_bound] AS [IsSchemaBound]
       ,[sm].[uses_native_compilation] AS [UsesNativeCompilation]
       ,NULL AS [HasAmbiguousReference]
FROM
        [sys].[objects]                   AS [sp] WITH (NOLOCK)
        LEFT JOIN [sys].[sql_modules]     AS [sm] WITH (NOLOCK) ON [sm].[object_id] = [sp].[object_id]
        LEFT JOIN [sys].[database_principals] [p] WITH (NOLOCK) ON [p].[principal_id] = [sm].[execute_as_principal_id]
        
WHERE [sp].[type] in (N'P',N'RF') AND ([sp].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [sp].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )) AND (OBJECTPROPERTY([sp].[object_id], N'IsEncrypted') = 0 OR OBJECTPROPERTY([sp].[object_id], N'IsEncrypted') IS NULL)
) AS [_results] ORDER BY ProcedureId,ProcedureNumber,SequenceNumber DESC;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.SqlProcedurePopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [sp].[schema_id]              AS [SchemaId]
       ,SCHEMA_NAME([sp].[schema_id]) AS [ProcedureOwner]
       ,[sp].[object_id]              AS [ProcedureId]
       ,[sp].[name]                   AS [ProcedureName]
       , [np].[procedure_number]      AS [ProcedureNumber]
       ,CONVERT(bit, ISNULL([sm].[uses_quoted_identifier], 0))
                                      AS [IsQuotedIdentifier]
       ,CONVERT(bit, ISNULL([sm].[uses_ansi_nulls], 0))
                                      AS [IsAnsiNulls]
       ,[sm].[is_recompiled]          AS [IsRecompiled]
       ,CAST(CASE sp.type WHEN N'RF' THEN 1 ELSE 0 END AS bit) AS [IsForReplication]
       ,[np].[definition]             AS [Script]
       ,1                             AS [SequenceNumber]
       ,0                             AS [SubsequenceNumber]
       ,CASE WHEN [sp].[is_published] <> 0 OR [sp].[is_schema_published] <> 0 THEN 1 ELSE 0 END AS [ReplInfo]
       ,[sp].[create_date]            AS [CreatedDate]
       ,[sm].[execute_as_principal_id] AS [ExecuteAsId]
       ,[p].[name] AS [ExecuteAsName]
       ,NULL AS [HasAmbiguousReference]
FROM
[sys].[procedures] AS [sp] WITH (NOLOCK)
INNER JOIN [sys].[numbered_procedures] AS [np] WITH (NOLOCK) ON [np].[object_id] = [sp].[object_id]
LEFT JOIN [sys].[sql_modules]     AS [sm] WITH (NOLOCK) ON [sm].[object_id] = [sp].[object_id]
LEFT JOIN [sys].[database_principals] [p] WITH (NOLOCK) ON [p].[principal_id] = [sm].[execute_as_principal_id]

WHERE ([sp].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [sp].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )) AND OBJECTPROPERTY([sp].[object_id], N'IsEncrypted') = 0
) AS [_results] ORDER BY ProcedureId,ProcedureNumber,SequenceNumber DESC;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90EncryptedAndClrProcedurePopulator' as [PopulatorName];
SELECT * FROM (

SELECT
        [sp].[schema_id]              AS [SchemaId]
       ,SCHEMA_NAME([sp].[schema_id]) AS [ProcedureOwner]
       ,[sp].[object_id]              AS [ProcedureId]
       ,[sp].[name]                   AS [ProcedureName]
       ,CAST(1 AS smallint)           AS [ProcedureNumber]
       ,NULL
                                      AS [IsQuotedIdentifier]
       ,NULL
                                      AS [IsAnsiNulls]
       ,CONVERT(bit, ISNULL(OBJECTPROPERTY([sp].[object_id], N'IsReplProc'), 0))
                                      AS [IsForReplication]
       ,NULL
                                      AS [IsSchemaBound]
       ,NULL                          AS [IsRecompiled]
       ,[sp].[type]                   AS [ProcedureType]
       ,[as].[assembly_id]            AS [AssemblyId]
       ,[as].[name]                   AS [Assembly]
       ,[am].[assembly_class]         AS [AssemblyClass]
       ,[am].[assembly_method]        AS [AssemblyMethod]
       ,[am].[null_on_null_input]      AS [NullOnNullInput]
       ,[am].[execute_as_principal_id] AS [ExecuteAsId]
       ,[p2].[name] AS [ExecuteAsName]
       ,CASE WHEN [so].[is_published] <> 0 OR [so].[is_schema_published] <> 0 THEN 1 ELSE 0 END AS [ReplInfo]
       ,[sp].[create_date]            AS [CreatedDate]
FROM
        [sys].[objects]                    AS [sp] WITH (NOLOCK)
        LEFT JOIN [sys].[assembly_modules]    AS [am] WITH (NOLOCK) ON [am].[object_id] = [sp].[object_id]
        LEFT JOIN [sys].[assemblies]          AS [as] WITH (NOLOCK) ON [as].[assembly_id] = [am].[assembly_id]
        LEFT JOIN [sys].[database_principals] AS [p1] WITH (NOLOCK) ON [p1].[principal_id] = [sp].[principal_id]
        LEFT JOIN [sys].[database_principals] AS [p2] WITH (NOLOCK) ON [p2].[principal_id] = [am].[execute_as_principal_id]
        LEFT JOIN [sys].[objects]             AS [so] WITH (NOLOCK) ON [so].[object_id] = [sp].[object_id]
WHERE  ([sp].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [sp].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )) AND [sp].[type] = N'PC'
) AS [_results] ORDER BY ProcedureId,ProcedureNumber ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql130ProcedureParameterPopulator' as [PopulatorName];
SELECT * FROM (
SELECT 
     [p].[object_id]            AS [ProcedureId]
    ,SCHEMA_NAME([p].[schema_id]) 
                                AS [ProcedureOwner]
    ,[p].[name]                 AS [ProcedureName]
    ,1					        AS [ProcedureNumber]
    ,[c].[parameter_id]	        AS [ParameterId]
    ,[c].[name]                 AS [ParameterName]
    ,[c].[parameter_id]         AS [ParameterOrder]
    ,[c].[user_type_id]         AS [TypeId]
    ,SCHEMA_NAME([t].[schema_id])  
                                AS [TypeSchemaName]
    ,(CASE WHEN (CONVERT(bit, COLUMNPROPERTY([p].[object_id], [c].[name], N'IsCursorType')) = 1)
          THEN N'cursor'
          ELSE [t].[name]
     END)                       AS [TypeName]
    ,CASE WHEN [c].[max_length] >= 0 AND [t].[name] IN (N'nchar', N'nvarchar')
          THEN ([c].[max_length] / 2)
          ELSE [c].[max_length] 
     END                        AS [Length]
    ,[c].[precision]            AS [Precision]
    ,[c].[scale]                AS [Scale]
    ,[c].[has_default_value]    AS [HasDefaultValue]
    ,[c].[default_value]        AS [DefaultValue]
    ,[c].[is_output]	        AS [IsOutput]
    ,CAST([c].[is_readonly] AS bit)
                                AS [IsReadOnly]
    ,CAST([c].[is_nullable] AS bit)
                                AS [IsNullable]
    ,[c].[is_xml_document] 
                                AS [IsXmlDocument]
    ,[c].[xml_collection_id] 
                                AS [XmlCollectionId]
    ,[xscs].[name]		        AS [XmlCollection]
    ,SCHEMA_NAME([xscs].[schema_id]) 
                                AS [XmlCollectionSchemaName]
FROM 
    [sys].[objects] AS [p] WITH (NOLOCK)	
    INNER JOIN [sys].[parameters] AS [c] WITH (NOLOCK) ON [c].[object_id] = [p].[object_id]    
    LEFT  JOIN [sys].[types] AS [t] WITH (NOLOCK) ON [t].[user_type_id] = [c].[user_type_id]
    LEFT  JOIN [sys].[xml_schema_collections] [xscs] WITH (NOLOCK) ON [xscs].[xml_collection_id] = [c].[xml_collection_id]
WHERE	
    [p].[type] = N'PC'
    AND [p].[name] NOT LIKE N'#%%'
    AND (( OBJECTPROPERTY([p].[object_id], N'IsEncrypted') = 1 OR OBJECTPROPERTY([p].[object_id], N'IsEncrypted') IS NULL) OR [p].[type] = N'PC')
    AND ([p].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [p].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) AS [_results] ORDER BY ProcedureId,ProcedureNumber,ParameterOrder ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90EncryptedAndClrProcedurePopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [sp].[schema_id]              AS [SchemaId]
       ,SCHEMA_NAME([sp].[schema_id]) AS [ProcedureOwner]
       ,[sp].[object_id]              AS [ProcedureId]
       ,[sp].[name]                   AS [ProcedureName]
       ,1                             AS [ProcedureNumber]
       ,CONVERT(bit, ISNULL([sm].[uses_quoted_identifier], 0))
                                      AS [IsQuotedIdentifier]
       ,CONVERT(bit, ISNULL([sm].[uses_ansi_nulls], 0))
                                      AS [IsAnsiNulls]
       ,CONVERT(bit, ISNULL(OBJECTPROPERTY([sp].[object_id], N'IsReplProc'), 0))
                                      AS [IsForReplication]
       ,CONVERT(bit, ISNULL([sm].[is_schema_bound], 0))
                                      AS [IsSchemaBound]
       ,[sm].[is_recompiled]          AS [IsRecompiled]
       ,[sp].[type]                   AS [ProcedureType]
       ,NULL                          AS [AssemblyId]
       ,NULL                          AS [Assembly]
       ,NULL                          AS [AssemblyClass]
       ,NULL                          AS [AssemblyMethod]
       ,[sm].[null_on_null_input]      AS [NullOnNullInput]
       ,[sm].[execute_as_principal_id] AS [ExecuteAsId]
       ,[p2].[name] AS [ExecuteAsName]
       ,CASE WHEN [sp].[is_published] <> 0 OR [sp].[is_schema_published] <> 0 THEN 1 ELSE 0 END AS [ReplInfo]
       ,[sp].[create_date]            AS [CreatedDate]
FROM
        [sys].[objects]                   AS [sp] WITH (NOLOCK)
        LEFT JOIN [sys].[sql_modules]     AS [sm] WITH (NOLOCK) ON [sm].[object_id] = [sp].[object_id]
        LEFT JOIN [sys].[database_principals] AS [p1] WITH (NOLOCK) ON [p1].[principal_id] = [sp].[principal_id]
        LEFT JOIN [sys].[database_principals] AS [p2] WITH (NOLOCK) ON [p2].[principal_id] = [sm].[execute_as_principal_id]

WHERE [sp].[type] in (N'P',N'RF') AND ([sp].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [sp].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )) AND (OBJECTPROPERTY([sp].[object_id], N'IsEncrypted') = 1 OR OBJECTPROPERTY([sp].[object_id], N'IsEncrypted') IS NULL)

) AS [_results] ORDER BY ProcedureId,ProcedureNumber ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql130ProcedureParameterPopulator' as [PopulatorName];
SELECT * FROM (
SELECT 
     [p].[object_id]            AS [ProcedureId]
    ,SCHEMA_NAME([p].[schema_id]) 
                                AS [ProcedureOwner]
    ,[p].[name]                 AS [ProcedureName]
    ,1					        AS [ProcedureNumber]
    ,[c].[parameter_id]	        AS [ParameterId]
    ,[c].[name]                 AS [ParameterName]
    ,[c].[parameter_id]         AS [ParameterOrder]
    ,[c].[user_type_id]         AS [TypeId]
    ,SCHEMA_NAME([t].[schema_id])  
                                AS [TypeSchemaName]
    ,(CASE WHEN (CONVERT(bit, COLUMNPROPERTY([p].[object_id], [c].[name], N'IsCursorType')) = 1)
          THEN N'cursor'
          ELSE [t].[name]
     END)                       AS [TypeName]
    ,CASE WHEN [c].[max_length] >= 0 AND [t].[name] IN (N'nchar', N'nvarchar')
          THEN ([c].[max_length] / 2)
          ELSE [c].[max_length] 
     END                        AS [Length]
    ,[c].[precision]            AS [Precision]
    ,[c].[scale]                AS [Scale]
    ,[c].[has_default_value]    AS [HasDefaultValue]
    ,[c].[default_value]        AS [DefaultValue]
    ,[c].[is_output]	        AS [IsOutput]
    ,CAST([c].[is_readonly] AS bit)
                                AS [IsReadOnly]
    ,CAST([c].[is_nullable] AS bit)
                                AS [IsNullable]
    ,[c].[is_xml_document] 
                                AS [IsXmlDocument]
    ,[c].[xml_collection_id] 
                                AS [XmlCollectionId]
    ,[xscs].[name]		        AS [XmlCollection]
    ,SCHEMA_NAME([xscs].[schema_id]) 
                                AS [XmlCollectionSchemaName]
FROM 
    [sys].[objects] AS [p] WITH (NOLOCK)	
    INNER JOIN [sys].[parameters] AS [c] WITH (NOLOCK) ON [c].[object_id] = [p].[object_id]    
    LEFT  JOIN [sys].[types] AS [t] WITH (NOLOCK) ON [t].[user_type_id] = [c].[user_type_id]
    LEFT  JOIN [sys].[xml_schema_collections] [xscs] WITH (NOLOCK) ON [xscs].[xml_collection_id] = [c].[xml_collection_id]
WHERE	
    [p].[type] IN (N'P',N'RF')
    AND [p].[name] NOT LIKE N'#%%'
    AND (( OBJECTPROPERTY([p].[object_id], N'IsEncrypted') = 1 OR OBJECTPROPERTY([p].[object_id], N'IsEncrypted') IS NULL) OR [p].[type] = N'PC')
    AND ([p].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [p].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) AS [_results] ORDER BY ProcedureId,ProcedureNumber,ParameterOrder ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90EncryptedAndClrProcedurePopulator' as [PopulatorName];
SELECT * FROM (

SELECT
        [sp].[schema_id]              AS [SchemaId]
       ,SCHEMA_NAME([sp].[schema_id]) AS [ProcedureOwner]
       ,[sp].[object_id]              AS [ProcedureId]
       ,[sp].[name]                   AS [ProcedureName]
       ,[np].[procedure_number]       AS [ProcedureNumber]
       ,CONVERT(bit, ISNULL([sm].[uses_quoted_identifier], 0))
                                      AS [IsQuotedIdentifier]
       ,CONVERT(bit, ISNULL([sm].[uses_ansi_nulls], 0))
                                      AS [IsAnsiNulls]
       ,CONVERT(bit, ISNULL(OBJECTPROPERTY([sp].[object_id], N'IsReplProc'), 0))
                                      AS [IsForReplication]
       ,CONVERT(bit, ISNULL([sm].[is_schema_bound], 0))
                                      AS [IsSchemaBound]
       ,[sm].[is_recompiled]          AS [IsRecompiled]
       ,[sp].[type]                   AS [ProcedureType]
       ,NULL                          AS [AssemblyId]
       ,NULL                          AS [Assembly]
       ,NULL                          AS [AssemblyClass]
       ,NULL                          AS [AssemblyMethod]
       ,[sm].[null_on_null_input]      AS [NullOnNullInput]
       ,[sm].[execute_as_principal_id] AS [ExecuteAsId]
       ,[p2].[name] AS [ExecuteAsName]
       ,CASE WHEN [sp].[is_published] <> 0 OR [sp].[is_schema_published] <> 0 THEN 1 ELSE 0 END AS [ReplInfo]
       ,[sp].[create_date]            AS [CreatedDate]
FROM
        [sys].[procedures]                     AS [sp] WITH (NOLOCK)
        INNER JOIN [sys].[numbered_procedures] AS [np] WITH (NOLOCK) ON [np].[object_id] = [sp].[object_id]
        LEFT JOIN [sys].[sql_modules]          AS [sm] WITH (NOLOCK) ON [sm].[object_id] = [sp].[object_id]
        LEFT JOIN [sys].[database_principals]  AS [p1] WITH (NOLOCK) ON [p1].[principal_id] = [sp].[principal_id]
        LEFT JOIN [sys].[database_principals]  AS [p2] WITH (NOLOCK) ON [p2].[principal_id] = [sm].[execute_as_principal_id]

WHERE  ([sp].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [sp].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )) AND (OBJECTPROPERTY([sp].[object_id], N'IsEncrypted') = 1 OR OBJECTPROPERTY([sp].[object_id], N'IsEncrypted') IS NULL)
) AS [_results] ORDER BY ProcedureId,ProcedureNumber ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql100ProcedureParameterPopulator' as [PopulatorName];
SELECT * FROM (
SELECT 
     [p].[object_id]            AS [ProcedureId]
    ,SCHEMA_NAME([p].[schema_id]) 
                                AS [ProcedureOwner]
    ,[p].[name]                 AS [ProcedureName]
    ,[np].[procedure_number] 
                                AS [ProcedureNumber]
    ,[c].[parameter_id]	        AS [ParameterId]
    ,[c].[name]                 AS [ParameterName]
    ,[c].[parameter_id]         AS [ParameterOrder]
    ,[c].[user_type_id]         AS [TypeId]
    ,SCHEMA_NAME([t].[schema_id])  
                                AS [TypeSchemaName]
    ,(CASE WHEN (CONVERT(bit, COLUMNPROPERTY([p].[object_id], [c].[name], N'IsCursorType')) = 1)
          THEN N'cursor'
          ELSE [t].[name]
     END)                       AS [TypeName]
    ,CASE WHEN [c].[max_length] >= 0 AND [t].[name] IN (N'nchar', N'nvarchar')
          THEN ([c].[max_length] / 2)
          ELSE [c].[max_length] 
     END                        AS [Length]
    ,[c].[precision]            AS [Precision]
    ,[c].[scale]                AS [Scale]
    ,CAST(0 AS BIT)             AS [HasDefaultValue]
    ,NULL                       AS [DefaultValue]
    ,[c].[is_output]	        AS [IsOutput] 
    ,CONVERT(bit, CASE ([c].[system_type_id]) WHEN 243 THEN 1 ELSE 0 END)
                                AS [IsReadOnly]    
    ,CAST(0 AS bit) 	        AS [IsXmlDocument]
    ,0		                    AS [XmlCollectionId]
    ,NULL		                AS [XmlCollection]
    ,NULL				        AS [XmlCollectionSchemaName]
FROM 
    [sys].[numbered_procedures] AS [np] WITH (NOLOCK)
    INNER JOIN [sys].[procedures] AS [p] WITH (NOLOCK) ON [p].[object_id] = [np].[object_id]
    INNER JOIN [sys].[numbered_procedure_parameters] AS [c] WITH (NOLOCK) ON [c].[object_id] = [p].[object_id] 
               AND [c].[procedure_number] = [np].[procedure_number]   
    LEFT  JOIN [sys].[types] AS [t] WITH (NOLOCK) ON [t].[user_type_id] = [c].[user_type_id]    
WHERE	
    [p].[name] NOT LIKE N'#%%'
    AND OBJECTPROPERTY([p].[object_id], N'IsEncrypted') = 1
    AND ([p].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [p].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) AS [_results] ORDER BY ProcedureId,ProcedureNumber,ParameterOrder ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.SqlAzureV12TablePopulator' as [PopulatorName];
SELECT * FROM (
SELECT  
        [t].[schema_id]              AS [SchemaId],
        SCHEMA_NAME([t].[schema_id]) AS [SchemaName],
        [t].[name]                   AS [ColumnSourceName], 
        [t].[object_id]              AS [TableId],
        [t].[type]                   AS [Type],
        [ds].[type]                  AS [DataspaceType],
        [ds].[data_space_id]         AS [DataspaceId],
        [ds].[name]                  AS [DataspaceName],
        [si].[index_id]              AS [IndexId],
        [si].[type]                  AS [IndexType],
        CASE WHEN exists(SELECT 1 FROM [sys].[columns] AS [c] WITH (NOLOCK) WHERE [c].[object_id] = [st].[object_id] AND [st].[is_memory_optimized] = 0 AND  ([c].[system_type_id] IN (34, 35, 99, 241) OR ( [c].[system_type_id] in (165, 167,231,240) AND [c].[max_length] = -1))) THEN
            [dsx].[data_space_id] 
        ELSE
            NULL
        END AS [TextFilegroupId],
        CASE WHEN [dsx].[name] = 'XTP' THEN NULL ELSE [dsx].[name] END AS [TextFilegroupName],
        CASE WHEN exists(SELECT 1 FROM [sys].[columns] AS [c] WITH (NOLOCK) WHERE [c].[object_id] = [st].[object_id] AND  [c].[is_filestream] = 1) THEN
            [dsf].[data_space_id] 
        ELSE
            NULL
        END AS [FileStreamId],
        [dsf].[name]                  AS [FileStreamName],
        [dsf].[type]                  AS [FileStreamType],
        [st].[uses_ansi_nulls]		  AS [IsAnsiNulls],
        CAST(OBJECTPROPERTY([st].[object_id],N'IsQuotedIdentOn') AS bit) AS [IsQuotedIdentifier],
        CAST([st].[lock_on_bulk_load] AS bit) 
                                      AS [IsLockedOnBulkLoad],
        [st].[text_in_row_limit]	  AS [TextInRowLimit],
        CAST([st].[large_value_types_out_of_row] AS bit)
                                      AS [LargeValuesOutOfRow],
        CAST(OBJECTPROPERTY([st].[object_id], N'TableHasVarDecimalStorageFormat') AS bit)
                                      AS [HasVarDecimalStorageFormat],
        [st].[is_tracked_by_cdc]      AS [IsTrackedByCDC],
        CAST(CASE
             WHEN [ctt].[object_id] IS NULL THEN 0
             ELSE 1
             END AS BIT)              AS [IsChangeTrackingOn],
        [ctt].[is_track_columns_updated_on] AS [IsTrackColumnsUpdatedOn],
        [st].[lock_escalation]        AS [LockEscalation],     
        CASE WHEN [st].[is_replicated] <> 0 OR [st].[is_merge_published] <> 0 OR [st].[is_schema_published] <> 0 OR [st].[is_published] <> 0 THEN 1 ELSE 0 END AS [ReplInfo]        ,
        [t].[create_date] AS [CreateDate],
        CAST([st].[is_memory_optimized] AS BIT) AS [IsMemoryOptimized],
        [st].[durability] AS [Durability],
        [st].[temporal_type] AS [TemporalType],
        [historyTable].[name] AS [HistoryTableName],
        SCHEMA_NAME([historyTable].[schema_id]) AS [HistoryTableSchema],
        [st].[history_retention_period] AS [HistoryRetentionPeriod],
        [st].[history_retention_period_unit] AS [HistoryRetentionUnit],
        [st].[is_node] AS IsNode,
        [st].[is_edge] AS IsEdge
FROM    
        [sys].[objects] [t] WITH (NOLOCK)
        LEFT    JOIN [sys].[tables] [st] WITH (NOLOCK) ON [t].[object_id] = [st].[object_id]
        LEFT    JOIN (SELECT * FROM [sys].[indexes] WITH (NOLOCK) WHERE ISNULL([index_id],0) < 2) [si] ON [si].[object_id] = [st].[object_id]
        LEFT    JOIN [sys].[data_spaces] [ds] WITH (NOLOCK) ON [ds].[data_space_id] = [si].[data_space_id]
        LEFT    JOIN [sys].[data_spaces] [dsx] WITH (NOLOCK) ON [dsx].[data_space_id] = [st].[lob_data_space_id]
        LEFT    JOIN [sys].[data_spaces] [dsf] WITH (NOLOCK) ON [dsf].[data_space_id] = [st].[filestream_data_space_id]
        LEFT    JOIN [sys].[change_tracking_tables] [ctt] WITH (NOLOCK) ON [ctt].[object_id] = [st].[object_id]
        LEFT    JOIN [sys].[periods] [periods] WITH (NOLOCK) ON [periods].[object_id] = [st].[object_id]
        LEFT    JOIN [sys].[tables] [historyTable] WITH (NOLOCK) ON [st].[history_table_id] = [historyTable].[object_id]
WHERE  [t].[type] = N'U' AND ISNULL([st].[is_filetable],0) = 0 AND ISNULL([st].[is_external],0) = 0 AND ([t].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [t].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) AS [_results] WHERE ( 1 = (select TOP 1 1 FROM @SystemTableTypeDiscriminators as [t] where [t].[TypeCode] COLLATE DATABASE_DEFAULT = [_results].[Type])) ORDER BY TableId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.SqlAzureV12TableColumnPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [t].[name] AS [ColumnSourceName], 
        [t].[object_id] AS [TableId],
        SCHEMA_NAME([t].[schema_id]) AS [SchemaName], 
        [c].[name] AS [ColumnName], 
        [c].[user_type_id] AS [TypeId],
        CONVERT(bit, ISNULL([types].[is_user_defined], 0)) AS [IsUserDefinedType],
        [types].[name] AS [TypeName],
        [basetypes].[name] AS [BaseTypeName],
        SCHEMA_NAME([types].[schema_id]) AS [TypeSchemaName],
        [c].[column_id] AS [ColumnId], 
        [c].[precision] AS [Precision],
        [c].[scale] AS [Scale],
        CASE WHEN [c].[max_length] >= 0 AND [types].[name] IN (N'nchar', N'nvarchar') THEN ([c].[max_length] / 2) ELSE [c].[max_length] END AS [Length],
        CONVERT(bit, [c].[is_identity]) AS [IsIdentity],
        CONVERT(bit, [c].[is_computed]) AS [IsComputed],
        CONVERT(bit, ISNULL([ic].[is_not_for_replication], 0)) AS [IsNotForReplication],
        CAST(ISNULL([ic].[seed_value], 0) AS DECIMAL(38)) AS [IdentitySeed],
        CAST(ISNULL([ic].[increment_value], 0) AS DECIMAL(38)) AS [IdentityIncrement],
        CONVERT(bit, [c].[is_nullable]) AS [IsNullable],
        [cc].[definition] AS [ComputedText],
        [c].[is_rowguidcol] AS [IsRowGuidColumn],
        [c].[collation_name] AS [Collation],
        [c].[is_xml_document] AS [IsXmlDocument],
        [c].[xml_collection_id] AS [XmlCollectionId],
        [xscs].[name] AS [XmlCollection],
        SCHEMA_NAME([xscs].[schema_id]) AS [XmlCollectionSchemaName],
        [c].[is_sparse]                 AS [IsSparse],
        [c].[is_column_set]             AS [IsColumnSet],
        [c].[is_filestream]             AS [IsFilestream],
        [c].[generated_always_type]     AS [GeneratedAlwaysType],
        [mc].[masking_function]         AS [MaskingFunction],
        [c].[is_hidden]                 AS [IsHidden],
        CASE [ta].[is_filetable] WHEN 1 THEN N'UF' ELSE N'U ' END AS [Type],
        CONVERT(bit, ISNULL([cc].[is_persisted], 0)) AS [IsPersisted],
        CAST(ISNULL([indexCol].[IsPartitionColumn],0) AS BIT) AS [IsPartitionColumn],
        ISNULL([c].[encryption_type], 0) AS [EncryptionType],
        [c].[encryption_algorithm_name] AS [EncryptionAlgorithmName],
        [c].[column_encryption_key_id] AS [ColumnEncryptionKeyId],
        [cek].[name] AS [ColumnEncryptionKeyName],
        CAST(0 AS BIT) AS [IsPrimaryKey],
        CAST(0 AS BIT) AS [IsForeignKey],
        [c].[graph_type] AS [GraphType],
        [ta].[is_node] AS [IsNode]
FROM    [sys].[columns] [c] WITH (NOLOCK)
INNER   JOIN [sys].[objects] [t] WITH (NOLOCK) ON [c].[object_id] = [t].[object_id]
LEFT	JOIN [sys].[tables] [ta] WITH(NOLOCK) ON [ta].[object_id] = [t].[object_id]
LEFT    JOIN [sys].[types] [basetypes] WITH (NOLOCK) ON [c].[system_type_id] = [basetypes].[system_type_id] AND [basetypes].[system_type_id] = [basetypes].[user_type_id]
LEFT    JOIN [sys].[types] [types] WITH (NOLOCK) ON [c].[user_type_id] = [types].[user_type_id]
LEFT    JOIN [sys].[identity_columns] [ic] WITH (NOLOCK) ON [ic].[object_id] = [c].[object_id] AND [ic].[column_id] = [c].[column_id]
LEFT    JOIN [sys].[computed_columns] [cc] WITH (NOLOCK) ON [cc].[object_id] = [c].[object_id] AND [cc].[column_id] = [c].[column_id]
LEFT    JOIN [sys].[masked_columns] [mc] WITH (NOLOCK) ON [mc].[object_id] = [c].[object_id] AND [mc].[column_id] = [c].[column_id]
LEFT    JOIN [sys].[xml_schema_collections] [xscs] WITH (NOLOCK) ON [xscs].[xml_collection_id] = [c].[xml_collection_id]
LEFT    JOIN [sys].[column_encryption_keys] [cek] WITH (NOLOCK) ON [cek].[column_encryption_key_id] = [c].[column_encryption_key_id]
LEFT    JOIN (
    SELECT 1 AS [IsPartitionColumn], [indexCol].[column_id], [ix].[object_id] FROM [sys].[index_columns] [indexCol] WITH (NOLOCK) 
    INNER JOIN [sys].[indexes] [ix] WITH (NOLOCK) ON [indexCol].[index_id] = [ix].[index_id] AND [ix].[is_hypothetical] = 0 AND [ix].[type] IN (0,1,5) and [ix].[object_id] = [indexCol].[object_id]
    WHERE [indexCol].[partition_ordinal] > 0) AS [indexCol] ON [c].[object_id] = [indexCol].[object_id] AND [c].[column_id] = [indexCol].[column_id]
WHERE   [t].[type] = N'U' AND ISNULL([ta].[is_filetable],0) = 0 AND ISNULL([ta].[is_external],0) = 0
AND ([t].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [t].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) AS [_results] WHERE ( 1 = (select TOP 1 1 FROM @SystemTableTypeDiscriminators as [t] where [t].[TypeCode] COLLATE DATABASE_DEFAULT = [_results].[Type])) ORDER BY TableId,ColumnId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110FileTablePopulator' as [PopulatorName];
SELECT * FROM (
SELECT  
        [t].[schema_id]               AS [SchemaId],
        SCHEMA_NAME([t].[schema_id])  AS [SchemaName],
        [t].[name]                    AS [ColumnSourceName],
        [t].[object_id]               AS [TableId],
        CASE [st].[is_filetable] WHEN 1 THEN N'UF' ELSE N'U ' END                           AS [Type],
        [ds].[type]                   AS [DataspaceType],
        [ds].[data_space_id]          AS [DataspaceId],
        [ds].[name]                   AS [DataspaceName],
        [si].[index_id]               AS [IndexId],
        [si].[type]                   AS [IndexType],
        CASE WHEN exists(SELECT 1 FROM [sys].[columns] AS [c] WITH (NOLOCK) WHERE [c].[object_id] = [t].[object_id] AND  ([c].[system_type_id] IN (34, 35, 99, 241) OR ( [c].[system_type_id] in (165, 167,231,240) AND [c].[max_length] = -1))) THEN
            [dsx].[data_space_id] 
        ELSE
            NULL
        END AS [TextFilegroupId],
        [dsx].[name]                  AS [TextFilegroupName],
        CASE WHEN exists(SELECT 1 FROM [sys].[columns] AS [c] WITH (NOLOCK) WHERE [c].[object_id] = [st].[object_id] AND  [c].[is_filestream] = 1) THEN
            [dsf].[data_space_id] 
        ELSE
            NULL
        END AS [FileStreamId],
        [dsf].[name]                  AS [FileStreamName],
        [dsf].[type]                  AS [FileStreamType],
        [st].[uses_ansi_nulls]        AS [IsAnsiNulls],
        CAST(OBJECTPROPERTY([t].[object_id],N'IsQuotedIdentOn') AS bit) AS [IsQuotedIdentifier],
        CAST([st].[lock_on_bulk_load] AS bit) 
                                      AS [IsLockedOnBulkLoad],
        [st].[text_in_row_limit]      AS [TextInRowLimit],
        CAST([st].[large_value_types_out_of_row] AS bit)
                                      AS [LargeValuesOutOfRow],
        CAST(OBJECTPROPERTY([t].[object_id], N'TableHasVarDecimalStorageFormat') AS bit)
                                      AS [HasVarDecimalStorageFormat],
        [st].[is_tracked_by_cdc]      AS [IsTrackedByCDC],
        CAST(CASE
             WHEN [ctt].[object_id] IS NULL THEN 0
             ELSE 1
             END AS BIT)              AS [IsChangeTrackingOn],
        [ctt].[is_track_columns_updated_on] AS [IsTrackColumnsUpdatedOn],
        [st].[lock_escalation]        AS [LockEscalation],
        CASE WHEN [st].[is_replicated] <> 0 OR [st].[is_merge_published] <> 0 OR [st].[is_schema_published] <> 0 OR [st].[is_published] <> 0 THEN 1 ELSE 0 END AS [ReplInfo],
        [sft].[is_enabled]            AS [FileTableEnabled],
        [sft].[directory_name]        AS [FileTableDirectoryName],
        [sft].[filename_collation_id] AS [FileTableCollationId],
        [sft].[filename_collation_name] AS [FileTableCollation],
        [st].[create_date] AS [CreateDate]
FROM
        [sys].[objects] [t] WITH (NOLOCK)
        LEFT    JOIN [sys].[tables] [st] WITH (NOLOCK) ON [t].[object_id] = [st].[object_id]
        LEFT    JOIN [sys].[indexes] [si] WITH (NOLOCK) ON [si].[object_id] = [st].[object_id]
        LEFT    JOIN [sys].[data_spaces] [ds] WITH (NOLOCK) ON [ds].[data_space_id] = [si].[data_space_id]
        LEFT    JOIN [sys].[data_spaces] [dsx] WITH (NOLOCK) ON [dsx].[data_space_id] = [st].[lob_data_space_id]
        LEFT    JOIN [sys].[data_spaces] [dsf] WITH (NOLOCK) ON [dsf].[data_space_id] = [st].[filestream_data_space_id]
        LEFT    JOIN [sys].[change_tracking_tables] [ctt] WITH (NOLOCK) ON [ctt].[object_id] = [st].[object_id]
        LEFT    JOIN [sys].[filetables] [sft] WITH (NOLOCK) ON [sft].[object_id] = [st].[object_id]
WHERE   [t].[type] = N'U' AND ISNULL([st].[is_filetable],0) = 1 AND ISNULL([si].[index_id],0) < 2
AND ([t].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [t].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) AS [_results] WHERE ( 1 = (select TOP 1 1 FROM @SystemTableTypeDiscriminators as [t] where [t].[TypeCode] COLLATE DATABASE_DEFAULT = [_results].[Type])) ORDER BY TableId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110FileTableColumnPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [t].[name] AS [ColumnSourceName], 
        [t].[object_id] AS [TableId],
        SCHEMA_NAME([t].[schema_id]) AS [SchemaName], 
        [c].[name] AS [ColumnName], 
        [c].[user_type_id] AS [TypeId],
        CONVERT(bit, ISNULL([types].[is_user_defined], 0)) AS [IsUserDefinedType],
        [types].[name] AS [TypeName],
        [basetypes].[name] AS [BaseTypeName],
        SCHEMA_NAME([types].[schema_id]) AS [TypeSchemaName],
        [c].[column_id] AS [ColumnId], 
        [c].[precision] AS [Precision],
        [c].[scale] AS [Scale],
        CASE WHEN [c].[max_length] >= 0 AND [types].[name] IN (N'nchar', N'nvarchar') THEN ([c].[max_length] / 2) ELSE [c].[max_length] END AS [Length],
        CONVERT(bit, [c].[is_identity]) AS [IsIdentity],
        CONVERT(bit, [c].[is_computed]) AS [IsComputed],
        CONVERT(bit, ISNULL([ic].[is_not_for_replication], 0)) AS [IsNotForReplication],
        CAST(ISNULL([ic].[seed_value], 0) AS DECIMAL(38)) AS [IdentitySeed],
        CAST(ISNULL([ic].[increment_value], 0) AS DECIMAL(38)) AS [IdentityIncrement],
        CONVERT(bit, [c].[is_nullable]) AS [IsNullable],
        [cc].[definition] AS [ComputedText],
        [c].[is_rowguidcol] AS [IsRowGuidColumn],
        [c].[collation_name] AS [Collation],
        [c].[is_xml_document] AS [IsXmlDocument],
        [c].[xml_collection_id] AS [XmlCollectionId],
        [xscs].[name] AS [XmlCollection],
        SCHEMA_NAME([xscs].[schema_id]) AS [XmlCollectionSchemaName],
        [c].[is_sparse]                 AS [IsSparse],
        [c].[is_column_set]              AS [IsColumnSet],
        [c].[is_filestream]             AS [IsFilestream],
        CASE [ta].[is_filetable] WHEN 1 THEN N'UF' ELSE N'U ' END AS [Type],
        CONVERT(bit, ISNULL([cc].[is_persisted], 0)) AS [IsPersisted],
        CAST(ISNULL([indexCol].[IsPartitionColumn],0) AS BIT) AS [IsPartitionColumn],
        CAST(0 AS BIT) AS [IsPrimaryKey],
        CAST(0 AS BIT) AS [IsForeignKey]
FROM    [sys].[columns] [c] WITH (NOLOCK)
INNER   JOIN [sys].[objects] [t] WITH (NOLOCK) ON [c].[object_id] = [t].[object_id]
LEFT	JOIN [sys].[tables] [ta] WITH(NOLOCK) ON [ta].[object_id] = [t].[object_id]
LEFT    JOIN [sys].[types] [basetypes] WITH (NOLOCK) ON [c].[system_type_id] = [basetypes].[system_type_id] AND [basetypes].[system_type_id] = [basetypes].[user_type_id]
LEFT    JOIN [sys].[types] [types] WITH (NOLOCK) ON [c].[user_type_id] = [types].[user_type_id]
LEFT    JOIN [sys].[identity_columns] [ic] WITH (NOLOCK) ON [ic].[object_id] = [c].[object_id] AND [ic].[column_id] = [c].[column_id]
LEFT    JOIN [sys].[computed_columns] [cc] WITH (NOLOCK) ON [cc].[object_id] = [c].[object_id] AND [cc].[column_id] = [c].[column_id]
LEFT    JOIN [sys].[xml_schema_collections] [xscs] WITH (NOLOCK) ON [xscs].[xml_collection_id] = [c].[xml_collection_id]
LEFT    JOIN (
    SELECT 1 AS [IsPartitionColumn], [indexCol].[column_id], [ix].[object_id] FROM [sys].[index_columns] [indexCol] WITH (NOLOCK) 
    INNER JOIN [sys].[indexes] [ix] WITH (NOLOCK) ON [indexCol].[index_id] = [ix].[index_id] AND [ix].[is_hypothetical] = 0 AND [ix].[type] IN (0,1) and [ix].[object_id] = [indexCol].[object_id]
    WHERE [indexCol].[partition_ordinal] > 0) AS [indexCol] ON [c].[object_id] = [indexCol].[object_id] AND [c].[column_id] = [indexCol].[column_id]
WHERE   [t].[type] = N'U' AND ISNULL([ta].[is_filetable],0) = 1
AND ([t].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [t].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) AS [_results] WHERE ( 1 = (select TOP 1 1 FROM @SystemTableTypeDiscriminators as [t] where [t].[TypeCode] COLLATE DATABASE_DEFAULT = [_results].[Type])) ORDER BY TableId,ColumnId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql120PrimaryKeyPopulator' as [PopulatorName];
SELECT * FROM (
SELECT DISTINCT
    SCHEMA_NAME([o].[schema_id]) AS [SchemaName]
   ,[i].[object_id]        AS [ColumnSourceId]
   ,[o].[name]             AS [ColumnSourceName]
   ,CASE [ta].[is_filetable] WHEN 1 THEN N'UF' ELSE [o].[type] END                    AS [ColumnSourceType]
   ,[kc].[object_id]       AS [ConstraintId]
   ,[i].[name]             AS [ConstraintName]
   ,CAST(CASE 
    WHEN [kc].[is_system_named] = 1 AND [ft].[object_id] IS NOT NULL THEN 1
    WHEN [kc].[is_system_named] = 1 AND EXISTS(SELECT TOP 1 1 FROM sys.extended_properties WITH (NOLOCK) WHERE [class] = 1 AND major_id = [kc].[object_id]) THEN 1
    ELSE 0 END AS BIT) AS [PromoteName]
   ,CAST(CASE [kc].[is_system_named]
               WHEN 1 THEN 1
               ELSE CASE WHEN [ftc].[object_id] IS NULL THEN 0 ELSE NULL END
             END AS BIT) AS [IsSystemNamed]
   ,[f].[type]             AS [DataspaceType]
   ,[i].[data_space_id]    AS [DataspaceId]
   ,[f].[name]             AS [DataspaceName]
   ,[fs].[data_space_id]   AS [FileStreamId]
   ,[fs].[name]            AS [FileStreamName]
   ,[fs].[type]            AS [FileStreamType]
   ,[i].[fill_factor]      AS [FillFactor]    
   ,CONVERT(bit, CASE [i].[type] WHEN 1 THEN 1 ELSE 0 END) 
                           AS [IsClustered]
   ,[i].[is_unique]        AS [IsUnique]
   ,[i].[is_padded]        AS [IsPadded]
   ,[i].[ignore_dup_key]   AS [DoIgnoreDuplicateKey]
   ,[t].[no_recompute]     AS [NoRecomputeStatistics]
   ,[i].[allow_row_locks]  AS [DoAllowRowLocks]
   ,[i].[allow_page_locks] AS [DoAllowPageLocks]
   ,NULL                   AS [PartitionSchemeName]
   ,NULL                   AS [PartitionColumnName]
   ,[i].[is_disabled]      AS [IsDisabled]
   ,CONVERT(bit, CASE WHEN [ti].[data_space_id] <> [i].[data_space_id] THEN 0 ELSE 1 END)
                           AS [EqualsParentDataSpace]
   ,CAST(CASE WHEN [ftc].[object_id] IS NULL THEN 0 ELSE 1 END AS BIT) AS [IsFileTableSystemConstraint]
   ,CONVERT(BIT, CASE WHEN [hi].[object_id] IS NULL THEN 0 ELSE 1 END) AS [IsHash]
   ,[hi].[bucket_count] AS [BucketCount]
   ,CAST(CASE [ta].[is_memory_optimized] WHEN 1 THEN 1 ELSE 0 END AS BIT) AS [IsFromMemoryOptimizedTable]
FROM 
    [sys].[indexes] AS [i] WITH (NOLOCK)
    INNER JOIN [sys].[objects]           AS [o]  WITH (NOLOCK) ON [i].[object_id] = [o].[object_id]
    INNER JOIN [sys].[key_constraints]   AS [kc] WITH (NOLOCK) ON [i].[object_id] = [kc].[parent_object_id] AND [i].[index_id] = [kc].[unique_index_id]
    LEFT  JOIN [sys].[data_spaces]       AS [f]  WITH (NOLOCK) ON [i].[data_space_id] = [f].[data_space_id]
    LEFT  JOIN [sys].[stats]             AS [t]  WITH (NOLOCK) ON [t].[object_id] = [i].[object_id] AND [t].[name] = [i].[name]
    LEFT  JOIN [sys].[tables]            AS [ta] WITH (NOLOCK) ON [ta].[object_id] = [i].[object_id]
    LEFT  JOIN [sys].[data_spaces]       AS [fs] WITH (NOLOCK) ON [fs].[data_space_id] = [ta].[filestream_data_space_id]
    LEFT  JOIN (SELECT * FROM [sys].[indexes] WITH (NOLOCK) WHERE [index_id] < 2) AS [ti] ON [o].[object_id] = [ti].[object_id]
    LEFT JOIN [sys].[filetable_system_defined_objects] AS [ftc] WITH (NOLOCK) ON [ftc].[parent_object_id] = [ta].[object_id] AND [ftc].[object_id] = [kc].[object_id]
    LEFT OUTER JOIN [sys].[fulltext_indexes] AS [ft] WITH (NOLOCK) ON [i].[object_id] = [ft].[object_id] AND [i].[index_id] = [ft].[unique_index_id]
    LEFT OUTER JOIN [sys].[hash_indexes] AS [hi] WITH (NOLOCK) ON [hi].[object_id] = [i].[object_id] AND [hi].[index_id] = [i].[index_id]
WHERE 
    ([o].[type]  = N'U' OR [o].[type] = N'TF') AND [i].[is_primary_key] = 1 
    AND [i].[name] IS NOT NULL
    AND [i].[is_hypothetical] = 0
    AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) AS [_results] WHERE ([ColumnSourceType] <> N'TF' OR OBJECTPROPERTYEX([ColumnSourceId],N'IsEncrypted') = 1)
 ORDER BY ColumnSourceId,ConstraintId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql100ConstraintColumnSpecificationPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  
    SCHEMA_NAME([o].[schema_id])  AS [SchemaName]
   ,[o].[name]               AS [ColumnSourceName]
   ,[i].[object_id]          AS [ColumnSourceId]
   ,CASE [t].[is_filetable] WHEN 1 THEN N'UF' ELSE [o].[type] END               AS [ColumnSourceType]
   ,[kc].[object_id]         AS [IndexId]
   ,[i].[name]               AS [IndexName]
   ,[c].[column_id]          AS [ColumnId]
   ,[c].[name]               AS [ColumnName]
   ,[ic].[is_descending_key] AS [IsDescending]
   ,[ic].[is_included_column] AS [IsIncludedColumn]
   ,[ic].[partition_ordinal]  AS [PartitionOrdinal]
   ,[ic].[key_ordinal]        AS [KeyOrdinal]
   ,[kc].[name]               AS [ConstraintName]
FROM 
    [sys].[columns] AS [c] WITH (NOLOCK)
    LEFT JOIN [sys].[index_columns] AS [ic] WITH (NOLOCK) ON [c].[object_id] = [ic].[object_id] AND [c].[column_id] = [ic].[column_id]
    LEFT JOIN [sys].[indexes]       AS [i] WITH (NOLOCK) ON [i].[object_id] = [ic].[object_id] AND [i].[index_id] = [ic].[index_id]
    LEFT JOIN [sys].[objects]   AS [o] WITH (NOLOCK) ON [o].[object_id] = [i].[object_id]
    LEFT JOIN [sys].[key_constraints] AS [kc] WITH (NOLOCK) ON [i].[object_id] = [kc].[parent_object_id] AND [i].[index_id] = [kc].[unique_index_id]
    LEFT JOIN [sys].[tables] AS [t] WITH(NOLOCK) ON [t].[object_id] = [c].[object_id]
WHERE 
    ([o].[type] = N'U' OR [o].[type] = N'TF') 
    AND [i].[is_primary_key] = 1
    AND [i].[name] IS NOT NULL
    AND [i].[is_hypothetical] = 0
    AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))
) AS [_results] WHERE ([ColumnSourceType] <> N'TF' OR OBJECTPROPERTYEX([ColumnSourceId],N'IsEncrypted') = 1)
 ORDER BY ColumnSourceId,IndexId,KeyOrdinal ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql130UniqueConstraintPopulator' as [PopulatorName];
SELECT * FROM (
SELECT DISTINCT
            SCHEMA_NAME([o].[schema_id]) AS [SchemaName]
           ,[i].[object_id]        AS [ColumnSourceId]
           ,[o].[name]             AS [ColumnSourceName]
           ,[o].[type]             AS [ColumnSourceType]
           ,[kc].[object_id]       AS [ConstraintId]
           ,[i].[name]             AS [ConstraintName]
           ,CAST(CASE 
            WHEN [kc].[is_system_named] = 1 AND [ft].[object_id] IS NOT NULL THEN 1
            WHEN [kc].[is_system_named] = 1 AND EXISTS(SELECT TOP 1 1 FROM sys.extended_properties WITH (NOLOCK) WHERE [class] = 1 AND major_id = [kc].[object_id]) THEN 1
            ELSE 0 END AS BIT) AS [PromoteName]
            ,CAST(CASE [kc].[is_system_named]
                       WHEN 1 THEN 1
                       ELSE CASE WHEN [ftc].[object_id] IS NULL THEN 0 ELSE NULL END
                     END AS BIT) AS [IsSystemNamed]
           ,[f].[type]             AS [DataspaceType]
           ,[i].[data_space_id]    AS [DataspaceId]
           ,[f].[name]             AS [DataspaceName]
           ,[fs].[data_space_id]   AS [FileStreamId]
           ,[fs].[name]            AS [FileStreamName]
           ,[fs].[type]            AS [FileStreamType]
           ,[i].[fill_factor]      AS [FillFactor]    
           ,CONVERT(bit, CASE [i].[type] WHEN 1 THEN 1 ELSE 0 END) 
                                   AS [IsClustered]
           ,[i].[is_unique]        AS [IsUnique]
           ,[i].[is_padded]        AS [IsPadded]
           ,[i].[ignore_dup_key]   AS [DoIgnoreDuplicateKey]
           ,[t].[no_recompute]     AS [NoRecomputeStatistics]
           ,[i].[allow_row_locks]  AS [DoAllowRowLocks]
           ,[i].[allow_page_locks] AS [DoAllowPageLocks]
           ,CONVERT(bit, CASE WHEN [ti].[data_space_id] <> [i].[data_space_id] THEN 0 ELSE 1 END)
                           AS [EqualsParentDataSpace]
           ,CAST(CASE WHEN [ftc].[object_id] IS NULL THEN 0 ELSE 1 END AS BIT) AS [IsFileTableSystemConstraint]
           ,CAST(ISNULL([ic].[column_id], 0) AS BIT) AS [IsStreamIdFileTableSystemConstraint]
            ,CONVERT(BIT, CASE WHEN [hi].[object_id] IS NULL THEN 0 ELSE 1 END) AS [IsHash]
           ,[hi].[bucket_count] AS [BucketCount]
           ,CAST(CASE [ta].[is_memory_optimized] WHEN 1 THEN 1 ELSE 0 END AS BIT) AS [IsFromMemoryOptimizedTable]
FROM 
            [sys].[indexes] AS [i] WITH (NOLOCK)
            INNER JOIN [sys].[objects]           AS [o]  WITH (NOLOCK) ON [i].[object_id] = [o].[object_id]
            INNER JOIN [sys].[key_constraints]   AS [kc] WITH (NOLOCK) ON [i].[object_id] = [kc].[parent_object_id] AND [i].[index_id] = [kc].[unique_index_id]
            LEFT JOIN [sys].[data_spaces]       AS [f]  WITH (NOLOCK) ON [i].[data_space_id] = [f].[data_space_id]
            LEFT JOIN [sys].[stats]             AS [t]  WITH (NOLOCK) ON [t].[object_id] = [i].[object_id] AND [t].[name] = [i].[name]
            LEFT JOIN [sys].[tables]            AS [ta] WITH (NOLOCK) ON [ta].[object_id] = [i].[object_id]
            LEFT JOIN [sys].[data_spaces]       AS [fs] WITH (NOLOCK) ON [fs].[data_space_id] = [ta].[filestream_data_space_id]
            LEFT JOIN [sys].[indexes]          AS [ti] WITH (NOLOCK) ON [o].[object_id] = [ti].[object_id] AND [ti].[index_id] < 2
            LEFT JOIN [sys].[filetable_system_defined_objects] AS [ftc] WITH (NOLOCK) ON [ftc].[parent_object_id] = [ta].[object_id] AND [ftc].[object_id] = [kc].[object_id]
            LEFT JOIN [sys].[index_columns] [ic] WITH (NOLOCK) ON [ic].column_id = 1 AND [ic].[object_id] = [i].[object_id] AND [ic].[index_id] = [i].[index_id] AND [ic].[object_id] = [ftc].[parent_object_id]
            LEFT JOIN [sys].[fulltext_indexes] AS [ft] WITH (NOLOCK) ON [i].[object_id] = [ft].[object_id] AND [i].[index_id] = [ft].[unique_index_id]
            LEFT JOIN [sys].[hash_indexes] AS [hi] WITH (NOLOCK) ON [hi].[object_id] = [i].[object_id] AND [hi].[index_id] = [i].[index_id]
WHERE 
            ([o].[type]  = N'U' OR [o].[type] = N'TF') 
            AND [i].[is_unique_constraint] = 1
            AND [i].[name] IS NOT NULL
            AND [i].[is_hypothetical] = 0
            AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) AS [_results] WHERE ([ColumnSourceType] <> N'TF' OR OBJECTPROPERTYEX([ColumnSourceId],N'IsEncrypted') = 1)
 ORDER BY ColumnSourceId,ConstraintId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90ConstraintColumnSpecificationPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  
    SCHEMA_NAME([o].[schema_id])  AS [SchemaName]
   ,[i].[object_id]          AS [ColumnSourceId]
   ,[o].[name]               AS [ColumnSourceName]
   ,[o].[type]               AS [ColumnSourceType]
   ,[i].[name]               AS [ConstraintName]
   ,[kc].[object_id]         AS [IndexId]
   ,[i].[name]               AS [IndexName]
   ,[c].[column_id]          AS [ColumnId]
   ,[c].[name]               AS [ColumnName]
   ,[ic].[is_descending_key] AS [IsDescending]
   ,[ic].[is_included_column] AS [IsIncludedColumn]
   ,[ic].[partition_ordinal]  AS [PartitionOrdinal]
   ,[ic].[key_ordinal]        AS [KeyOrdinal]
FROM 
    [sys].[columns] AS [c] WITH (NOLOCK)    
    LEFT JOIN [sys].[index_columns] AS [ic] WITH (NOLOCK) ON [c].[object_id] = [ic].[object_id] AND [c].[column_id] = [ic].[column_id]
    LEFT JOIN [sys].[indexes]       AS [i] WITH (NOLOCK) ON [i].[object_id] = [ic].[object_id] AND [i].[index_id] = [ic].[index_id]
    LEFT JOIN [sys].[objects]   AS [o] WITH (NOLOCK) ON [o].[object_id] = [i].[object_id]
    LEFT JOIN [sys].[key_constraints] AS [kc] WITH (NOLOCK) ON [i].[object_id] = [kc].[parent_object_id] AND [i].[index_id] = [kc].[unique_index_id]
    LEFT JOIN [sys].[tables] AS [t] WITH (NOLOCK) ON [t].[object_id] = [c].[object_id]
WHERE 
    ([o].[type] = N'U' OR [o].[type] = N'TF') 
    AND [i].[is_unique_constraint] = 1    
    AND [i].[name] IS NOT NULL
    AND [i].[is_hypothetical] = 0
    AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) AS [_results] WHERE ([ColumnSourceType] <> N'TF' OR OBJECTPROPERTYEX([ColumnSourceId],N'IsEncrypted') = 1)
 ORDER BY ColumnSourceId,IndexId,KeyOrdinal ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110ForeignKeyPopulator' as [PopulatorName];
SELECT * FROM (
SELECT DISTINCT
        SCHEMA_NAME([fs].[schema_id])  AS [SchemaName]
       ,[fs].[name]                    AS [ColumnSourceName]
       ,CASE [fs].[is_filetable] WHEN 1 THEN N'UF' ELSE [fs].[type] END AS [ColumnSourceType]
       ,[sfk].[name]                   AS [ConstraintName]
       ,CAST(CASE 
            WHEN [sfk].[is_system_named] = 1 AND [sfk].[is_disabled] = 1 THEN 1
            WHEN [sfk].[is_system_named] = 1 AND EXISTS(SELECT TOP 1 1 FROM sys.extended_properties WITH (NOLOCK) WHERE [class] = 1 AND major_id = [sfk].[object_id]) THEN 1
            ELSE 0 END AS BIT) AS [PromoteName]
        ,CAST(CASE [sfk].[is_system_named]
               WHEN 1 THEN 1
               ELSE CASE WHEN [ftc].[object_id] IS NULL THEN 0 ELSE NULL END
             END AS BIT) AS [IsSystemNamed]
       ,[sfk].[parent_object_id]       AS [ColumnSourceId]
       ,[sfk].[object_id]              AS [ConstraintId]
       ,[sfk].[referenced_object_id]   AS [ReferencedColumnSourceId]
       ,SCHEMA_NAME([rs].[schema_id])  AS [ReferencedSchemaName]
       ,[rs].[name]                    AS [ReferencedColumnSourceName]
       ,[sfk].[is_disabled]            AS [IsDisabled]
       ,[sfk].[is_not_for_replication] AS [IsNotForReplication]
       ,[sfk].[is_not_trusted]         AS [IsNotTrusted]
       ,[sfk].[update_referential_action] AS [UpdateAction]
       ,[sfk].[delete_referential_action] AS [DeleteAction]
       ,CAST(CASE WHEN [ftc].[object_id] IS NULL THEN 0 ELSE 1 END AS BIT) AS [IsFileTableSystemConstraint]
       ,[si].[name]                       AS [ReferencedIndexName]
FROM    
        [sys].[foreign_keys] AS [sfk] WITH (NOLOCK)
        INNER JOIN [sys].[tables] [fs] WITH (NOLOCK) ON [sfk].[parent_object_id] = [fs].[object_id]
        INNER JOIN [sys].[tables] [rs] WITH (NOLOCK) ON [sfk].[referenced_object_id] = [rs].[object_id] 
        INNER JOIN [sys].[indexes] [si] WITH (NOLOCK) ON [sfk].[referenced_object_id] = [si].[object_id] AND [sfk].[key_index_id] = [si].[index_id]
        LEFT JOIN [sys].[filetable_system_defined_objects] AS [ftc] WITH (NOLOCK) ON [ftc].[parent_object_id] = [fs].[object_id] AND [ftc].[object_id] = [sfk].[object_id]
WHERE   [ftc].[object_id] IS NULL AND ([fs].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [fs].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) AS [_results] ORDER BY ColumnSourceId,ConstraintId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.SqlForeignKeyColumnPopulator' as [PopulatorName];
SELECT * FROM (
SELECT 
        SCHEMA_NAME([fs].[schema_id])  AS [SchemaName]
       ,[sfk].[name]                   AS [ConstraintName]
       ,[fs].[name]                    AS [ColumnSourceName]
       ,CASE [fs].[is_filetable] WHEN 1 THEN N'UF' ELSE [fs].[type] END AS [ColumnSourceType]
       ,[sfk].[parent_object_id]       AS [ColumnSourceId]
       ,[sfk].[object_id]              AS [ConstraintId]
       ,[sfkc].[parent_column_id]      AS [ColumnId]
       ,COL_NAME([sfk].[parent_object_id],[sfkc].[parent_column_id]) AS [ColumnName]
       ,[sfkc].[referenced_object_id]  AS [ReferencedColumnSourceId]
       ,[sfkc].[referenced_column_id]  AS [ReferencedColumnId]  
       ,SCHEMA_NAME([rs].[schema_id])  AS [ReferencedSchemaName]
       ,[rs].[name]       AS [ReferencedColumnSourceName] 
       ,COL_NAME([sfk].[referenced_object_id],[sfkc].[referenced_column_id]) AS [ReferencedColumnName]    
       ,[sfkc].[constraint_column_id] AS  [KeyOrdinal]
FROM    [sys].[foreign_keys] AS [sfk] WITH (NOLOCK)
INNER   JOIN [sys].[foreign_key_columns] AS [sfkc] WITH (NOLOCK) ON [sfk].[object_id] = [sfkc].[constraint_object_id]
INNER   JOIN [sys].[tables] [fs] WITH (NOLOCK) ON [sfk].[parent_object_id] = [fs].[object_id]
INNER   JOIN [sys].[tables] [rs] WITH (NOLOCK) ON [sfk].[referenced_object_id] = [rs].[object_id] 
LEFT    JOIN [sys].[filetable_system_defined_objects] AS [ftc] WITH (NOLOCK) ON [ftc].[parent_object_id] = [fs].[object_id] AND [ftc].[object_id] = [sfk].[object_id]
WHERE   [ftc].[object_id] IS NULL AND ([fs].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [fs].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) AS [_results] ORDER BY ColumnSourceId,ConstraintId,KeyOrdinal ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.SqlDefaultConstraintPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [d].[object_id]                     AS [ConstraintId]
       ,SCHEMA_NAME([d].[schema_id])        AS [SchemaName]
       ,[d].[name]                          AS [ConstraintName]
       ,CAST(0 AS BIT) AS [PromoteName]
       ,[d].[is_system_named]               AS [IsSystemNamed]
       ,[d].[parent_object_id]              AS [TableId]
       ,[o].[name]                          AS [TableName]
       ,[o].[type]                          AS [ColumnSourceType]
       ,[c].[column_id]                     AS [ColumnId]
       ,[c].[name]                          AS [ColumnName]
       ,[d].[definition]                    AS [Script]
FROM
        [sys].[default_constraints]    AS [d] WITH (NOLOCK)
        INNER JOIN [sys].[columns] AS [c] WITH (NOLOCK) ON [c].[object_id] = [d].[parent_object_id] AND [c].[column_id] = [d].[parent_column_id]
        INNER JOIN [sys].[objects] AS [o] WITH (NOLOCK) ON [o].[object_id] = [d].[parent_object_id]
WHERE OBJECTPROPERTY([d].[parent_object_id], N'IsSystemTable') = 0 AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))
) AS [_results] WHERE ([ColumnSourceType] <> N'TF' OR OBJECTPROPERTYEX([TableId],N'IsEncrypted') = 1)
 ORDER BY TableId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110CheckConstraintPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        SCHEMA_NAME([cc].[schema_id])        AS [SchemaName]
       ,[cc].[name]                          AS [ConstraintName]
       ,CAST(CASE 
        WHEN [cc].[is_system_named] = 1 AND [cc].[is_disabled]  = 1 THEN 1
        WHEN [cc].[is_system_named] = 1 AND EXISTS(SELECT TOP 1 1 FROM sys.extended_properties WITH (NOLOCK) WHERE [class] = 1 AND major_id = [cc].[object_id]) THEN 1
        ELSE 0 END AS BIT) AS [PromoteName]
       ,CAST(CASE [cc].[is_system_named]
               WHEN 1 THEN 1
               ELSE CASE WHEN [ftc].[object_id] IS NULL THEN 0 ELSE NULL END
             END AS BIT) AS [IsSystemNamed]
       ,[cc].[parent_object_id]              AS [TableId]
       ,[o].[name]                           AS [TableName]
       ,CASE [ta].[is_filetable] WHEN 1 THEN N'UF' ELSE [o].[type] END                                  AS [ColumnSourceType]
       ,[cc].[definition]                    AS [Script]
       ,[cc].[is_not_for_replication]        AS [IsNotForReplication]
       ,[cc].[is_not_trusted]                AS [IsNotTrusted]
       ,[cc].[is_disabled]                   AS [IsDisabled]
       ,[cc].[object_id]                     AS [ConstraintId]
       ,CAST(CASE WHEN [ftc].[object_id] IS NULL THEN 0 ELSE 1 END AS BIT) AS [IsFileTableSystemConstraint]
FROM
        [sys].[check_constraints] AS [cc] WITH (NOLOCK)
        INNER JOIN [sys].[objects] [o] WITH (NOLOCK) ON [o].[object_id] = [cc].[parent_object_id]
        LEFT JOIN [sys].[tables] AS [ta] WITH (NOLOCK) ON [ta].[object_id] = [o].[object_id]
        LEFT JOIN [sys].[filetable_system_defined_objects] AS [ftc] WITH (NOLOCK) ON [ftc].[parent_object_id] = [ta].[object_id] AND [ftc].[object_id] = [cc].[object_id]
WHERE   [ftc].[object_id] IS NULL AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))
) AS [_results] WHERE ([ColumnSourceType] <> N'TF' OR OBJECTPROPERTYEX([TableId],N'IsEncrypted') = 1)
 ORDER BY TableId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90ViewPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [sv].[schema_id]              AS [SchemaId]
       ,SCHEMA_NAME([sv].[schema_id]) AS [SchemaName]
       ,[sv].[object_id]              AS [ViewId]
       ,[sv].[name]                   AS [ColumnSourceName]
       ,CONVERT(bit, OBJECTPROPERTY([sv].[object_id], N'ExecIsQuotedIdentOn'))
                                      AS [IsQuotedIdentifier]
       ,CONVERT(bit, OBJECTPROPERTY([sv].[object_id], N'ExecIsAnsiNullsOn'))
                                      AS [IsAnsiNulls]
       ,[sm].[definition]             AS [Script]
       ,CASE WHEN [so].[is_published] <> 0 OR [so].[is_schema_published] <> 0 THEN 1 ELSE 0 END AS [ReplInfo]
       ,[sv].[create_date] AS [CreatedDate]
       ,[sv].[with_check_option]      AS [WithCheckOption]
       ,[sm].[is_schema_bound]        AS [IsSchemaBound]
       ,[sv].[has_opaque_metadata]    AS [HasOpaqueMetadata]
       ,NULL AS [HasAmbiguousReference]
FROM
        [sys].[all_views] [sv] WITH (NOLOCK)
        LEFT JOIN [sys].[sql_modules] [sm] WITH (NOLOCK) ON [sm].[object_id] = [sv].[object_id]
        LEFT JOIN [sys].[objects] [so] WITH (NOLOCK) ON [so].[object_id] = [sv].[object_id]
        
WHERE ([sv].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [sv].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )) AND OBJECTPROPERTY([sv].[object_id], N'IsEncrypted') = 0
) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.SqlEncryptedViewPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [sv].[schema_id]              AS [SchemaId]
       ,SCHEMA_NAME([sv].[schema_id]) AS [SchemaName]
       ,[sv].[object_id]              AS [ViewId]
       ,[sv].[name]                   AS [ColumnSourceName]
       ,CONVERT(bit, OBJECTPROPERTY([sv].[object_id], N'ExecIsQuotedIdentOn'))
                                      AS [IsQuotedIdentifier]
       ,CONVERT(bit, OBJECTPROPERTY([sv].[object_id], N'ExecIsAnsiNullsOn'))
                                      AS [IsAnsiNulls]
       ,CONVERT(bit, ISNULL(OBJECTPROPERTY([sv].[object_id], N'IsSchemaBound'), 0))
                                      AS [IsSchemaBound]
       ,CASE WHEN [so].[is_published] <> 0 OR [so].[is_schema_published] <> 0 THEN 1 ELSE 0 END AS [ReplInfo]
       ,[sv].[create_date]            AS [CreatedDate]
       ,[sv].[with_check_option]      AS [WithCheckOption]
       ,[sv].[has_opaque_metadata]    AS [HasOpaqueMetadata]
FROM
        [sys].[all_views] [sv] WITH (NOLOCK)
        LEFT JOIN [sys].[objects] [so] WITH (NOLOCK) ON [so].[object_id] = [sv].[object_id]
WHERE ([sv].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [sv].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )) AND OBJECTPROPERTY([sv].[object_id], N'IsEncrypted') = 1) AS [_results] ORDER BY ViewId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.SqlViewColumnPopulator' as [PopulatorName];
SELECT * FROM (

SELECT
        SCHEMA_NAME([v].[schema_id]) AS [SchemaName],
        [v].[name]      AS [ColumnSourceName],
        [clmns].[name]  AS [ColumnName],
        [v].[object_id]        AS [ViewId],
        [clmns].[column_id] AS [ColumnId],
        CAST(0  AS bit) AS [IsForeignKey],
        USER_NAME([usrt].[schema_id])     AS [TypeSchemaName],
        [usrt].[name]               AS [TypeName],
        ISNULL([baset].[name], N'') AS [BaseTypeName],
        [clmns].[collation_name]         AS [Collation],
        [clmns].[is_xml_document] AS [IsXmlDocument],
        [clmns].[xml_collection_id] AS [XmlCollectionId],
        [xscs].[name] AS [XmlCollection],
        SCHEMA_NAME([xscs].[schema_id]) AS [XmlCollectionSchemaName],
        CONVERT(bit, COLUMNPROPERTY([v].[object_id], [clmns].[name], N'IsIdNotForRepl')) AS [IsNotForReplication],
        CONVERT(bit, COLUMNPROPERTY([v].[object_id], [clmns].[name], N'IsRowGuidCol')) AS [IsRowGuidColumn],
        CAST(CASE WHEN  ([clmns].[max_length] >= 0 AND [baset].[name] IN (N'nchar', N'nvarchar')) THEN [clmns].[max_length]/2 ELSE clmns.[max_length] END AS int) AS [Length],
        [clmns].[user_type_id] AS [TypeId],
        CAST(0 AS bit) AS [InPrimaryKey],
        CAST([clmns].[precision] AS int)          AS [Precision],
        CAST([clmns].[scale] AS int)         AS [Scale],
        CAST([clmns].[is_nullable] AS bit)     AS [AllowNulls],
        CAST([clmns].[is_computed] AS bit)     AS [IsComputed]
FROM
        [sys].[all_views] AS [v] WITH (NOLOCK)
        INNER JOIN [sys].[columns] AS [clmns] WITH (NOLOCK) ON [clmns].[object_id] = [v].[object_id]
        LEFT OUTER JOIN [sys].[types] AS [usrt] WITH (NOLOCK) ON [usrt].[user_type_id] = [clmns].[user_type_id]
        LEFT OUTER JOIN [sys].[types] AS [baset] WITH (NOLOCK) ON [baset].[system_type_id] = [clmns].[system_type_id] and [baset].[user_type_id] = [baset].[system_type_id]
        LEFT JOIN [sys].[xml_schema_collections] [xscs] WITH (NOLOCK) ON [xscs].[xml_collection_id] = [clmns].[xml_collection_id]
WHERE ([v].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [v].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )) AND OBJECTPROPERTY([v].[object_id], N'IsEncrypted') =  1
) AS [_results] ORDER BY ViewId,ColumnId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql140IndexPopulator' as [PopulatorName];
SELECT * FROM (
SELECT * FROM (
SELECT DISTINCT
    SCHEMA_NAME([o].[schema_id]) AS [SchemaName]
   ,[i].[object_id]        AS [ColumnSourceId]
   ,[o].[name]             AS [ColumnSourceName]
   ,[o].[type]             AS [ColumnSourceType]
   ,[i].[index_id]         AS [IndexId]
   ,[i].[name]             AS [IndexName]
   ,[f].[type]             AS [DataspaceType]
   ,[f].[data_space_id]    AS [DataspaceId]
   ,[f].[name]             AS [DataspaceName]
   ,CASE WHEN exists(SELECT 1 FROM [sys].[columns] AS [c] WITH (NOLOCK) WHERE [c].[object_id] = [o].[object_id] AND  [c].[is_filestream] = 1) THEN
            [ds].[data_space_id]
        ELSE
            NULL
        END  AS [FileStreamId]
   ,[ds].[name]            AS [FileStreamName]
   ,[ds].[type]            AS [FileStreamType]   
   ,[i].[fill_factor]      AS [FillFactor]    
   ,CONVERT(bit, CASE [i].[type] WHEN 1 THEN 1 WHEN 5 THEN 1 ELSE 0 END) 
                           AS [IsClustered]
   ,[i].[is_unique]        AS [IsUnique]
   ,[i].[is_padded]        AS [IsPadded]
   ,[i].[ignore_dup_key]   AS [DoIgnoreDuplicateKey]
   ,[t].[no_recompute]     AS [NoRecomputeStatistics]
   ,[t].[is_incremental]   AS [DoIncrementalStatistics]
   ,[i].[allow_row_locks]  AS [DoAllowRowLocks]
   ,[i].[allow_page_locks] AS [DoAllowPageLocks]
   ,[i].[is_disabled]      AS [IsDisabled]
   ,[i].[filter_definition]
                           AS [Predicate]
   ,[i].[compression_delay] AS [CompressionDelay]
   ,CONVERT(bit, CASE WHEN [ti].[data_space_id] <> [i].[data_space_id] THEN 0 ELSE 1 END)
                           AS [EqualsParentDataSpace]

   ,[i].[type]             AS [IndexType]
  ,CONVERT(BIT, CASE WHEN [hi].[object_id] IS NULL THEN 0 ELSE 1 END) AS [IsHash]
   ,[hi].[bucket_count] AS [BucketCount]
FROM 
    [sys].[indexes] AS [i] WITH (NOLOCK)
    INNER JOIN [sys].[objects]           AS [o]  WITH (NOLOCK) ON [i].[object_id] = [o].[object_id]
    LEFT  JOIN [sys].[data_spaces]       AS [f]  WITH (NOLOCK) ON [i].[data_space_id] = [f].[data_space_id]
    LEFT  JOIN [sys].[stats]             AS [t]  WITH (NOLOCK) ON [t].[object_id] = [i].[object_id] AND [t].[name] = [i].[name]
    LEFT  JOIN [sys].[tables]            AS [ta] WITH (NOLOCK) ON [ta].[object_id] = [i].[object_id]
    LEFT  JOIN [sys].[data_spaces]       AS [ds] WITH (NOLOCK) ON [ds].[data_space_id] = [ta].[filestream_data_space_id]
    LEFT  JOIN (SELECT * FROM [sys].[indexes] WITH (NOLOCK) WHERE [index_id] < 2) AS [ti] ON [o].[object_id] = [ti].[object_id]
    LEFT OUTER JOIN [sys].[hash_indexes] AS [hi] WITH (NOLOCK) ON [hi].[object_id] = [i].[object_id] AND [hi].[index_id] = [i].[index_id]
WHERE 
    ([o].[type] = N'U' OR [o].[type] = N'V')
    AND [i].[is_primary_key] = 0
    AND [i].[is_unique_constraint] = 0
    AND [i].[is_hypothetical] = 0
    AND [i].[name] IS NOT NULL
    AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) indexBase
WHERE [IndexType] NOT IN (3, 4, 5, 6)
) AS [_results] ORDER BY ColumnSourceId,IndexId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql140IndexedColumnSpecificationPopulator' as [PopulatorName];
SELECT * FROM (
SELECT * FROM (
SELECT  
    SCHEMA_NAME([o].[schema_id])  AS [SchemaName]
   ,[i].[object_id]          AS [ColumnSourceId]
   ,[o].[name]               AS [ColumnSourceName]
   ,[o].[type]               AS [ColumnSourceType]
   ,[i].[index_id]           AS [IndexId]
   ,[i].[name]               AS [IndexName]
   ,[c].[column_id]          AS [ColumnId]
   ,[c].[name]               AS [ColumnName]
   ,[ic].[is_descending_key] AS [IsDescending]
   ,[ic].[is_included_column] AS [IsIncludedColumn]
   ,[ic].[partition_ordinal]  AS [PartitionOrdinal]
   ,[ic].[key_ordinal]        AS [KeyOrdinal]
   ,[i].[type]                AS [IndexType]
   ,[c].[graph_type]        AS [GraphType]
   ,[t].[is_node]           AS [IsNode]
FROM 
    [sys].[columns] AS [c] WITH (NOLOCK)
    LEFT JOIN [sys].[index_columns] AS [ic] WITH (NOLOCK) ON [c].[object_id] = [ic].[object_id] AND [c].[column_id] = [ic].[column_id]
    LEFT JOIN [sys].[indexes]       AS [i] WITH (NOLOCK) ON [i].[object_id] = [ic].[object_id] AND [i].[index_id] = [ic].[index_id]
    LEFT JOIN [sys].[objects]   AS [o] WITH (NOLOCK) ON [o].[object_id] = [i].[object_id]
    LEFT JOIN [sys].[tables] as [t] WITH (NOLOCK) ON [t].[object_id] = [c].[object_id]
WHERE 
    ([o].[type] = N'U' OR [o].[type] = N'V')
    AND [i].[is_primary_key] = 0
    AND [i].[is_unique_constraint] = 0
    AND [i].[name] IS NOT NULL
    AND [i].[is_hypothetical] = 0
    AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) columns
WHERE [IndexType] NOT IN (3, 4, 6)
) AS [_results] ORDER BY ColumnSourceId,IndexId,KeyOrdinal ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql120StatisticPopulator' as [PopulatorName];
SELECT * FROM (
        SELECT     SCHEMA_NAME([o].[schema_id]) AS [SchemaName]
          ,[s].[object_id]              AS [ColumnSourceId]
          ,[o].[name]					AS [ColumnSourceName]
          ,CASE [ta].[is_filetable] WHEN 1 THEN N'UF' ELSE [o].[type] END  AS [ColumnSourceType]
          ,[s].[stats_id]				AS [StatisticsId]
          ,[s].[name]					AS [StatisticsName]
          ,[s].[no_recompute]			AS [NoRecompute]
          ,[s].[is_incremental]         AS [IsIncremental]
          ,[s].[has_filter]             AS [HasFilter]
          ,[s].[filter_definition]      AS [FilterDefinition]
          ,CAST([s].[user_created] AS BIT) AS [IsUserCreated]
FROM  	   [sys].[stats]   AS [s] WITH (NOLOCK) 
INNER JOIN [sys].[objects] AS [o] WITH (NOLOCK) ON [o].[object_id] = [s].[object_id] 
LEFT JOIN [sys].[tables] AS [ta] WITH (NOLOCK) ON [ta].[object_id] = [o].[object_id]
WHERE      [o].[type] IN (N'U', N'V')
           AND [s].[name] IS NOT NULL
           AND (CONVERT(bit, INDEXPROPERTY([s].[object_id], [s].[name], N'IsStatistics')) = 1)
           AND [s].[user_created] = 1
           AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) AS [_results] ORDER BY ColumnSourceId,StatisticsId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.SqlStatisticColumnPopulator' as [PopulatorName];
SELECT * FROM (
SELECT     SCHEMA_NAME([o].[schema_id]) AS [SchemaName]
          ,[s].[object_id]              AS [ColumnSourceId]
          ,[o].[name]                   AS [ColumnSourceName]
          ,[s].[stats_id]               AS [StatisticsId]
          ,[s].[name]                   AS [StatisticsName]
          ,[c].[column_id]              AS [ColumnId] 
          ,[sc].[name]                  AS [ColumnName]
          ,CASE [ta].[is_filetable] WHEN 1 THEN N'UF' ELSE [o].[type] END  AS [ColumnSourceType]
          ,[c].[stats_column_id]        AS [StatsColumnId]
FROM       [sys].[stats_columns] AS [c] WITH (NOLOCK)   
INNER JOIN [sys].[stats] AS [s] WITH (NOLOCK) ON [s].[object_id] = [c].[object_id] AND [s].[stats_id] = [c].[stats_id]
INNER JOIN [sys].[objects] AS [o] WITH (NOLOCK) ON [o].[object_id] = [c].[object_id] 
INNER JOIN [sys].[columns] AS [sc] WITH (NOLOCK) ON [sc].[object_id] = [c].[object_id] AND [sc].[column_id] = [c].[column_id]
LEFT JOIN [sys].[tables] AS [ta] WITH (NOLOCK) ON [ta].[object_id] = [o].[object_id]
WHERE      [o].[type] IN (N'U', N'V')
           AND [s].[name] IS NOT NULL 
           AND (CONVERT(bit, INDEXPROPERTY([s].[object_id], [s].[name], N'IsStatistics')) = 1)
           AND [s].[user_created] = 1
           AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) AS [_results] ORDER BY ColumnSourceId,StatisticsId,StatsColumnId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql100FullTextStopListPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [fs].[stoplist_id]        AS [StopListId],
        [fs].[name]               AS [StopListName],
        [fs].[principal_id]       AS [AuthorizerId],
        [dp].[name]               AS [AuthorizerName]
FROM 
        [sys].[fulltext_stoplists] [fs] WITH (NOLOCK)
        INNER JOIN [sys].[database_principals] [dp] WITH (NOLOCK) ON [dp].[principal_id] = [fs].[principal_id]
) AS [_results] ORDER BY StopListId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110SearchPropertyListPopulator' as [PopulatorName];
SELECT * FROM (SELECT 
 [spl].[name]              AS [Name],
 [spl].[property_list_id] AS [PropertyListId],
 [spl].[principal_id]     AS [AuthorizerId],
 [p].[name]              AS [Authorizer]
 FROM [sys].[registered_search_property_lists] AS [spl] WITH (NOLOCK)
 LEFT JOIN [sys].[database_principals] AS [p] WITH(NOLOCK) ON [spl].[principal_id] = [p].[principal_id]) AS [_results] ORDER BY PropertyListId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110SearchPropertyPopulator' as [PopulatorName];
SELECT * FROM (SELECT 
 [sp].[property_list_id] AS [PropertyListId],
 [sp].[property_int_id] AS [PropertyIntId],
 [sp].[property_name]   AS [Name],
 [sp].[property_description]     AS [Description],
 [sp].[property_int_id]   AS [Identifier],
 [sp].[property_set_guid] AS [PropertySetGuid],
 [spl].[name]             AS [PropertyListName]
 FROM 
[sys].[registered_search_properties] AS [sp] WITH (NOLOCK)
INNER JOIN [sys].[registered_search_property_lists] AS [spl] WITH(NOLOCK) ON [spl].[property_list_id] = [sp].[property_list_id]) AS [_results] ORDER BY PropertyListId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110FullTextIndexPopulator' as [PopulatorName];
SELECT * FROM (SELECT SCHEMA_NAME([o].[schema_id]) AS [SchemaName]
          ,[o].[object_id]              AS [ColumnSourceId]
          ,[o].[name]                   AS [ColumnSourceName]
          ,[o].[type]                   AS [ColumnSourceType]
          ,[i].[index_id]               AS [KeyId]
          ,[i].[name]                   AS [KeyName]
          ,[ft].[fulltext_catalog_id]   AS [FullTextCatalogId]
          ,[f].[name]                   AS [FullTextCatalogName]
          ,CONVERT(bit, CASE 
    WHEN [i].[is_primary_key] <> 0 THEN 1
    WHEN [i].[is_unique_constraint] <> 0 THEN 1
    WHEN [i].[is_unique] <> 0 THEN 1
    ELSE 0 END)
                                        AS [IsKeyConstraint] 
          ,[ft].[change_tracking_state] AS [ChangeTrackingState]
          ,[ft].[is_enabled]            AS [IsEnabled]
          ,[ft].[stoplist_id]           AS [StopListId]
          ,[fs].[name]                  AS [StopListName]
          ,[ds].[type]                  AS [DataspaceType]
          ,[ds].[data_space_id]         AS [DataspaceId]
          ,[ds].[name]                  AS [DataspaceName]
          ,CASE WHEN [o].[is_published] <> 0 OR [o].[is_schema_published] <> 0 THEN 1 ELSE 0 END AS [ReplInfo]
          ,[ft].[property_list_id]     AS [PropertyListId]
          ,[spl].[name]                AS [PropertyListName]
FROM
           [sys].[fulltext_indexes] AS [ft] WITH (NOLOCK)
           INNER JOIN [sys].[objects] AS [o] WITH (NOLOCK) ON [o].[object_id] = [ft].[object_id] 
           INNER JOIN [sys].[indexes] AS [i] WITH (NOLOCK) ON [i].[object_id] = [ft].[object_id] AND [i].[index_id] = [ft].[unique_index_id]
           INNER JOIN [sys].[fulltext_catalogs] AS [f] WITH (NOLOCK) ON [f].[fulltext_catalog_id] = [ft].[fulltext_catalog_id]
           INNER JOIN [sys].[data_spaces] AS [ds] WITH (NOLOCK) ON [ds].[data_space_id] = [ft].[data_space_id]
           LEFT JOIN [sys].[fulltext_stoplists] AS [fs] WITH (NOLOCK) ON [fs].[stoplist_id] = [ft].[stoplist_id]
           LEFT JOIN [sys].[registered_search_property_lists] AS [spl] WITH (NOLOCK) ON [ft].[property_list_id] = [spl].[property_list_id]
WHERE      [i].[is_hypothetical] = 0
           AND OBJECTPROPERTY([o].[object_id], N'IsSystemTable') = 0 
           AND [i].[name] IS NOT NULL
           AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) AS [_results] ORDER BY ColumnSourceId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110FullTextIndexColumnSpecifierPopulator' as [PopulatorName];
SELECT * FROM (
SELECT     SCHEMA_NAME([o].[schema_id]) AS [SchemaName]
          ,[c].[object_id]              AS [ColumnSourceId]
          ,[o].[name]                   AS [ColumnSourceName]
          ,[ft].[unique_index_id]       AS [KeyId]
          ,[ic].[column_id]             AS [ColumnId]
          ,[c].[name]                   AS [ColumnName]
          ,[ic].[type_column_id]        AS [TypeColumnId]
          ,[c2].[name]                  AS [TypeColumnName]
          ,[ic].[language_id]           AS [LCID]
          ,[o].[type]                   AS [ColumnSourceType]      
          ,CAST([ic].[statistical_semantics] AS BIT) AS [IsStatisticalSemantics]    
FROM       [sys].[columns] AS [c] WITH (NOLOCK)
INNER JOIN [sys].[fulltext_index_columns] AS [ic] WITH (NOLOCK) ON [ic].[object_id] = [c].[object_id] AND [ic].[column_id] = [c].[column_id]
INNER JOIN [sys].[objects] AS [o] WITH (NOLOCK) ON [o].[object_id] = [c].[object_id] 
INNER JOIN [sys].[fulltext_indexes] AS [ft] WITH (NOLOCK) ON [ft].[object_id] = [o].[object_id]
INNER JOIN [sys].[fulltext_catalogs] AS [f] WITH (NOLOCK) ON [f].[fulltext_catalog_id] = [ft].[fulltext_catalog_id]
LEFT JOIN  [sys].[columns] AS [c2] WITH (NOLOCK) ON [ic].[object_id] = [c2].[object_id] AND [ic].[type_column_id] = [c2].[column_id]
WHERE      OBJECTPROPERTY([o].[object_id], N'IsSystemTable') = 0 AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))
) AS [_results] ORDER BY ColumnSourceId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql140ColumnStoreIndexPopulator' as [PopulatorName];
SELECT * FROM (
SELECT * FROM (
SELECT DISTINCT
    SCHEMA_NAME([o].[schema_id]) AS [SchemaName]
   ,[i].[object_id]        AS [ColumnSourceId]
   ,[o].[name]             AS [ColumnSourceName]
   ,[o].[type]             AS [ColumnSourceType]
   ,[i].[index_id]         AS [IndexId]
   ,[i].[name]             AS [IndexName]
   ,[f].[type]             AS [DataspaceType]
   ,[f].[data_space_id]    AS [DataspaceId]
   ,[f].[name]             AS [DataspaceName]
   ,CASE WHEN exists(SELECT 1 FROM [sys].[columns] AS [c] WITH (NOLOCK) WHERE [c].[object_id] = [o].[object_id] AND  [c].[is_filestream] = 1) THEN
            [ds].[data_space_id]
        ELSE
            NULL
        END  AS [FileStreamId]
   ,[ds].[name]            AS [FileStreamName]
   ,[ds].[type]            AS [FileStreamType]   
   ,[i].[fill_factor]      AS [FillFactor]    
   ,CONVERT(bit, CASE [i].[type] WHEN 1 THEN 1 WHEN 5 THEN 1 ELSE 0 END) 
                           AS [IsClustered]
   ,[i].[is_unique]        AS [IsUnique]
   ,[i].[is_padded]        AS [IsPadded]
   ,[i].[ignore_dup_key]   AS [DoIgnoreDuplicateKey]
   ,[t].[no_recompute]     AS [NoRecomputeStatistics]
   ,[t].[is_incremental]   AS [DoIncrementalStatistics]
   ,[i].[allow_row_locks]  AS [DoAllowRowLocks]
   ,[i].[allow_page_locks] AS [DoAllowPageLocks]
   ,[i].[is_disabled]      AS [IsDisabled]
   ,[i].[filter_definition]
                           AS [Predicate]
   ,[i].[compression_delay] AS [CompressionDelay]
   ,CONVERT(bit, CASE WHEN [ti].[data_space_id] <> [i].[data_space_id] THEN 0 ELSE 1 END)
                           AS [EqualsParentDataSpace]

   ,[i].[type]             AS [IndexType]
  
FROM 
    [sys].[indexes] AS [i] WITH (NOLOCK)
    INNER JOIN [sys].[objects]           AS [o]  WITH (NOLOCK) ON [i].[object_id] = [o].[object_id]
    LEFT  JOIN [sys].[data_spaces]       AS [f]  WITH (NOLOCK) ON [i].[data_space_id] = [f].[data_space_id]
    LEFT  JOIN [sys].[stats]             AS [t]  WITH (NOLOCK) ON [t].[object_id] = [i].[object_id] AND [t].[name] = [i].[name]
    LEFT  JOIN [sys].[tables]            AS [ta] WITH (NOLOCK) ON [ta].[object_id] = [i].[object_id]
    LEFT  JOIN [sys].[data_spaces]       AS [ds] WITH (NOLOCK) ON [ds].[data_space_id] = [ta].[filestream_data_space_id]
    LEFT  JOIN (SELECT * FROM [sys].[indexes] WITH (NOLOCK) WHERE [index_id] < 2) AS [ti] ON [o].[object_id] = [ti].[object_id]
    
WHERE 
    ([o].[type] = N'U' OR [o].[type] = N'V')
    AND [i].[is_primary_key] = 0
    AND [i].[is_unique_constraint] = 0
    AND [i].[is_hypothetical] = 0
    AND [i].[name] IS NOT NULL
    AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) indexBase
WHERE [IndexType] IN (5, 6)
) AS [_results] ORDER BY ColumnSourceId,IndexId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql140ColumnStoreIndexColumnSpecificationPopulator' as [PopulatorName];
SELECT * FROM (
SELECT * FROM (
SELECT  
    SCHEMA_NAME([o].[schema_id])  AS [SchemaName]
   ,[i].[object_id]          AS [ColumnSourceId]
   ,[o].[name]               AS [ColumnSourceName]
   ,[o].[type]               AS [ColumnSourceType]
   ,[i].[index_id]           AS [IndexId]
   ,[i].[name]               AS [IndexName]
   ,[c].[column_id]          AS [ColumnId]
   ,[c].[name]               AS [ColumnName]
   ,[ic].[is_descending_key] AS [IsDescending]
   ,[ic].[is_included_column] AS [IsIncludedColumn]
   ,[ic].[partition_ordinal]  AS [PartitionOrdinal]
   ,[ic].[key_ordinal]        AS [KeyOrdinal]
   ,[i].[type]                AS [IndexType]
   ,[c].[graph_type]        AS [GraphType]
   ,[t].[is_node]           AS [IsNode]
FROM 
    [sys].[columns] AS [c] WITH (NOLOCK)
    LEFT JOIN [sys].[index_columns] AS [ic] WITH (NOLOCK) ON [c].[object_id] = [ic].[object_id] AND [c].[column_id] = [ic].[column_id]
    LEFT JOIN [sys].[indexes]       AS [i] WITH (NOLOCK) ON [i].[object_id] = [ic].[object_id] AND [i].[index_id] = [ic].[index_id]
    LEFT JOIN [sys].[objects]   AS [o] WITH (NOLOCK) ON [o].[object_id] = [i].[object_id]
    LEFT JOIN [sys].[tables] as [t] WITH (NOLOCK) ON [t].[object_id] = [c].[object_id]
WHERE 
    ([o].[type] = N'U' OR [o].[type] = N'V')
    AND [i].[is_primary_key] = 0
    AND [i].[is_unique_constraint] = 0
    AND [i].[name] IS NOT NULL
    AND [i].[is_hypothetical] = 0
    AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) columns
WHERE [IndexType] IN (5, 6)
) AS [_results] ORDER BY ColumnSourceId,IndexId,KeyOrdinal ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110SpatialIndexPopulator' as [PopulatorName];
SELECT * FROM (
SELECT 
        SCHEMA_NAME([o].[schema_id])    AS [SchemaName],
        [si].[object_id]                AS [ColumnSourceId],
        [o].[name]                      AS [ColumnSourceName],
        [o].[type]                      AS [ColumnSourceType],
        [ic].[column_id]                AS [ColumnId],
        [c].[name]                      AS [ColumnName],
        [si].[index_id]                 AS [IndexId],
        [si].[name]                     AS [IndexName],
        [ds].[type]                     AS [DataspaceType],
        [ds].[data_space_id]            AS [DataspaceId],
        [ds].[name]                     AS [DataspaceName],
        [si].[fill_factor]              AS [FillFactor],
        [si].[is_padded]                AS [IsPadded],
        [si].[is_disabled]              AS [IsDisabled],
        [si].[allow_page_locks]         AS [DoAllowPageLocks],
        [si].[allow_row_locks]          AS [DoAllowRowLocks],
        [sit].[cells_per_object]        AS [CellsPerObject],
        [sit].[bounding_box_xmin]       AS [XMin],
        [sit].[bounding_box_xmax]       AS [XMax],
        [sit].[bounding_box_ymin]       AS [YMin],
        [sit].[bounding_box_ymax]       AS [YMax],
        [sit].[level_1_grid]            AS [Level1Grid],
        [sit].[level_2_grid]            AS [Level2Grid],
        [sit].[level_3_grid]            AS [Level3Grid],
        [sit].[level_4_grid]            AS [Level4Grid],
        [sit].[tessellation_scheme]     AS [TessellationScheme],
        [s].[no_recompute]              AS [NoRecomputeStatistics],
        [p].[data_compression]          AS [DataCompressionId],
        CONVERT(bit, CASE WHEN [ti].[data_space_id] = [ds].[data_space_id] THEN 1 ELSE 0 END)
                                        AS [EqualsParentDataSpace]
FROM
        [sys].[spatial_indexes]          AS [si] WITH (NOLOCK)
        INNER JOIN [sys].[objects]       AS [o] WITH (NOLOCK) ON [si].[object_id] = [o].[object_id]
        INNER JOIN [sys].[spatial_index_tessellations] [sit] WITH (NOLOCK) ON [si].[object_id] = [sit].[object_id] AND [si].[index_id] = [sit].[index_id]
        INNER JOIN [sys].[data_spaces]   AS [ds] WITH (NOLOCK) ON [ds].[data_space_id] = [si].[data_space_id] 
        INNER JOIN [sys].[index_columns] AS [ic] WITH (NOLOCK) ON [si].[object_id] = [ic].[object_id] AND [si].[index_id] = [ic].[index_id]
        INNER JOIN [sys].[columns]       AS [c] WITH (NOLOCK) ON [si].[object_id] = [c].[object_id] AND [ic].[column_id] = [c].[column_id]
        INNER JOIN [sys].[objects]       AS [o2] WITH (NOLOCK) ON [o2].[parent_object_id] = [si].[object_id]
        INNER JOIN [sys].[stats]         AS [s] WITH (NOLOCK) ON [o2].[object_id] = [s].[object_id] AND [s].[name] = [si].[name]
        INNER JOIN [sys].[partitions]    AS [p] WITH (NOLOCK) ON [p].[object_id] = [o2].[object_id] AND [p].[partition_number] = 1
        LEFT  JOIN [sys].[indexes]       AS [ti] WITH (NOLOCK) ON [o].[object_id] = [ti].[object_id]
        LEFT JOIN [sys].[tables]         AS [t] WITH (NOLOCK) ON [t].[object_id] = [si].[object_id]
WHERE [si].[is_hypothetical] = 0
        AND [ti].[index_id] < 2
        AND OBJECTPROPERTY([o].[object_id], N'IsSystemTable') = 0
        AND ([t].[is_filetable] = 0 OR [t].[is_filetable] IS NULL)
        AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))
) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90XmlIndexPopulator' as [PopulatorName];
SELECT * FROM (
SELECT DISTINCT
    SCHEMA_NAME([o].[schema_id]) AS [SchemaName]
   ,[i].[object_id]        AS [ColumnSourceId]
   ,[o].[name]             AS [ColumnSourceName]
   ,[o].[type]             AS [ColumnSourceType]
   ,[i].[index_id]         AS [IndexId]
   ,[i].[name]             AS [IndexName]
   ,CONVERT(bit, CASE WHEN [i].[xml_index_type] = 0 THEN 1 ELSE 0 END) 
                               AS [IsPrimaryXmlIndex]
   ,[i].[using_xml_index_id]   AS [PrimaryXmlIndexId]
   ,[xi].[name]                AS [PrimaryXmlIndexName]
   ,[ic].[column_id]           AS [XmlIndexColumnId]
   ,[c].[name]                 AS [XmlIndexColumnName]
   ,[i].[secondary_type]       AS [SecondaryType]
   ,[i].[fill_factor]      AS [FillFactor]    
   ,[i].[is_padded]        AS [IsPadded]
   ,[t].[no_recompute]     AS [NoRecomputeStatistics]
   ,[i].[allow_row_locks]  AS [DoAllowRowLocks]
   ,[i].[allow_page_locks] AS [DoAllowPageLocks]
   ,[i].[is_disabled]      AS [IsDisabled]
FROM 
    [sys].[xml_indexes] AS [i] WITH (NOLOCK)
    INNER JOIN [sys].[objects]           AS [o]  WITH (NOLOCK) ON [i].[object_id] = [o].[object_id]
    INNER JOIN [sys].[index_columns]     AS [ic] WITH (NOLOCK) ON [i].[object_id] = [ic].[object_id] AND [i].[index_id] = [ic].[index_id]
    INNER JOIN [sys].[columns]           AS [c]  WITH (NOLOCK) ON [ic].[object_id] = [c].[object_id] AND [ic].[column_id] = [c].[column_id]
    INNER JOIN [sys].[objects]           AS [o2] WITH (NOLOCK) ON [o2].[parent_object_id] = [i].[object_id]
    INNER JOIN [sys].[stats]             AS [t]  WITH (NOLOCK) ON [t].[object_id] = [o2].[object_id] AND [t].[name] = [i].[name]    
    LEFT  JOIN [sys].[xml_indexes] AS [xi] WITH (NOLOCK) ON [xi].[object_id] = [i].[object_id] AND [xi].[index_id] = [i].[using_xml_index_id] 
    LEFT  JOIN [sys].[tables]        AS [ta] WITH (NOLOCK) ON [o].[object_id] = [ta].[object_id]   
WHERE 
    ([o].[type] = N'U' AND [ta].[is_filetable] = 0 OR [o].[type] = N'V')
    AND [i].[name] IS NOT NULL
    AND [i].[is_hypothetical] = 0
    AND ( [i].[xml_index_type] = 0 OR [i].[xml_index_type] = 1 )
    AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) AS [_results] ORDER BY ColumnSourceId,IndexId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110SelectiveXmlIndexPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
    SCHEMA_NAME([o].[schema_id]) AS [SchemaName]
    ,[i].[object_id]        AS [ColumnSourceId]
    ,[o].[name]             AS [ColumnSourceName]
    ,[o].[type]             AS [ColumnSourceType]
    ,[i].[index_id]         AS [IndexId]
    ,[i].[name]             AS [IndexName]
    ,CONVERT(bit, CASE WHEN [i].[xml_index_type] = 2 THEN 1 ELSE 0 END) 
                                AS [IsPrimaryXmlIndex]
    ,[i].[using_xml_index_id]   AS [PrimaryXmlIndexId]
    ,[i].[path_id]              AS [PrimaryXmlIndexPromotedPathId]
    ,[sxip].name                AS [PrimaryXmlIndexPromotedPathName]
    ,[xi].[name]                AS [PrimaryXmlIndexName]
    ,[ic].[column_id]           AS [XmlIndexColumnId]
    ,[c].[name]                 AS [XmlIndexColumnName]
    ,[i].[fill_factor]      AS [FillFactor]
    ,[i].[is_padded]        AS [IsPadded]
    ,[i].[allow_row_locks]  AS [DoAllowRowLocks]
    ,[i].[allow_page_locks] AS [DoAllowPageLocks]
    ,[i].[ignore_dup_key]   AS [DoIgnoreDuplicateKey]
    ,[t].[no_recompute]     AS [NoRecomputeStatistics]
    ,[i].[is_disabled]      AS [IsDisabled]
    ,[i].[xml_index_type]   AS [XmlIndexType]
FROM 
    [sys].[xml_indexes] AS [i] WITH (NOLOCK)
    INNER JOIN [sys].[objects]           AS [o]  WITH (NOLOCK) ON [i].[object_id] = [o].[object_id]
    INNER JOIN [sys].[index_columns]     AS [ic] WITH (NOLOCK) ON [i].[object_id] = [ic].[object_id] AND [i].[index_id] = [ic].[index_id]
    INNER JOIN [sys].[columns]           AS [c]  WITH (NOLOCK) ON [ic].[object_id] = [c].[object_id] AND [ic].[column_id] = [c].[column_id]
    INNER JOIN [sys].[objects]           AS [o2] WITH (NOLOCK) ON [o2].[parent_object_id] = [i].[object_id]
    INNER JOIN [sys].[stats]       AS [t]  WITH (NOLOCK) ON [t].[object_id] = [o2].[object_id] AND [t].[name] = [i].[name]
    LEFT  JOIN [sys].[xml_indexes] AS [xi] WITH (NOLOCK) ON [xi].[object_id] = [i].[object_id] AND [xi].[index_id] = [i].[using_xml_index_id] 
    LEFT  JOIN [sys].[tables]      AS [ta] WITH (NOLOCK) ON [o].[object_id] = [ta].[object_id]    
    LEFT  JOIN [sys].[selective_xml_index_paths] AS [sxip] WITH(NOLOCK) ON [sxip].[object_id] = [i].[object_id] AND [sxip].[index_id] = [i].[using_xml_index_id] AND [sxip].[path_id] = [i].[path_id]
WHERE 
    ([o].[type] = N'U' AND [ta].[is_filetable] = 0 OR [o].[type] = N'V')
    AND [i].[name] IS NOT NULL
    AND [i].[is_hypothetical] = 0
    AND ( [i].[xml_index_type] = 2 OR [i].[xml_index_type] = 3 )
    AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) AS [_results] ORDER BY XmlIndexType,ColumnSourceId,IndexId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110SelectiveXmlIndexNamespacePopulator' as [PopulatorName];
SELECT * FROM (
SELECT 
    SCHEMA_NAME([o].[schema_id]) AS [SchemaName]
    ,[i].[object_id]        AS [ColumnSourceId]
    ,[o].[name]             AS [ColumnSourceName]
    ,[o].[type]             AS [ColumnSourceType]
    ,[i].[index_id]         AS [IndexId]
    ,[i].[name]             AS [IndexName]
    ,CAST([sxin].[is_default_uri] AS BIT) AS [IsDefaultUri]
    ,[sxin].[uri] AS [Uri]
    ,[sxin].[prefix] AS [Prefix]
    ,[i].[xml_index_type]   AS [XmlIndexType]
FROM [sys].[selective_xml_index_namespaces] AS[sxin] WITH (NOLOCK) 
    INNER JOIN [sys].[xml_indexes] AS [i] WITH (NOLOCK) ON [sxin].[index_id] = [i].[index_id] AND [sxin].[object_id] = [i].[object_id]
    INNER JOIN [sys].[objects] AS [o]  WITH (NOLOCK) ON [i].[object_id] = [o].[object_id]
    LEFT JOIN [sys].[tables] as [ta] WITH (NOLOCK) ON [ta].[object_id] = [i].[object_id]
WHERE  [i].[xml_index_type]  IN (2,3) AND 
    ([o].[type] = N'U' AND [ta].[is_filetable] = 0 OR [o].[type] = N'V')  AND
    [i].[name] IS NOT NULL AND 
    [i].[is_hypothetical] = 0
    AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) AS [_results] ORDER BY XmlIndexType,ColumnSourceId,IndexId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110PromotedNodePathsPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
    SCHEMA_NAME([o].[schema_id]) AS [SchemaName]
    ,[i].[object_id]        AS [ColumnSourceId]
    ,[o].[name]             AS [ColumnSourceName]
    ,[o].[type]             AS [ColumnSourceType]
    ,[i].[index_id]         AS [IndexId]
    ,[i].[name]             AS [IndexName]
    ,[ip].[path_id]         AS [PromotedXmlPathId]
    ,[ip].[path]            AS [PromotedPath]
    ,[ip].[name]            AS [PathName]
    ,[ip].[path_type]       AS [PathType]
    ,[ip].[xml_component_id]                AS [XQueryTypeId]
    ,[ip].[xquery_type_description]         AS [XQueryType]    
    ,CAST(ISNULL([ip].[is_xquery_type_inferred], 0) AS BIT)  AS [IsXQueryTypeInferred]
    ,[ip].[xquery_max_length]               AS [XQueryMaxLength]
    ,CAST(ISNULL([ip].[is_xquery_max_length_inferred],0) AS BIT)  AS [IsXQueryMaxLengthInferred]
    ,[ip].[is_node]         AS [IsNode]
    ,[ip].[is_singleton]    AS [IsSingleton]
    ,SCHEMA_NAME([types].[schema_id]) AS [TypeSchemaName]
    ,[ip].[system_type_id]  AS [BaseTypeId]
    ,[basetypes].[name]     AS [BaseTypeName]
    ,[ip].[user_type_id]    AS [TypeId]
    ,[types].[name]         AS [TypeName]
    ,CASE WHEN [ip].[max_length] >= 0 AND [types].[name] IN (N'nchar', N'nvarchar') THEN ([ip].[max_length] / 2) ELSE [ip].[max_length] END AS [Length]
    ,[ip].[precision]       AS [Precision]
    ,[ip].[scale]           AS [Scale]
    ,[ip].[collation_name]  AS [Collation]
    ,[i].[xml_index_type]   AS [XmlIndexType]
FROM 
    [sys].[selective_xml_index_paths] AS [ip] WITH (NOLOCK)
    INNER JOIN [sys].[xml_indexes] AS [i] WITH (NOLOCK) ON [ip].[index_id] = [i].[index_id] AND [ip].[object_id] = [i].[object_id]
    INNER JOIN [sys].[objects]           AS [o]  WITH (NOLOCK) ON [i].[object_id] = [o].[object_id]    
    LEFT  JOIN [sys].[xml_indexes]  AS [xi] WITH (NOLOCK) ON [xi].[object_id] = [i].[object_id] AND [xi].[index_id] = [i].[using_xml_index_id] 
    LEFT  JOIN [sys].[tables]       AS [ta] WITH (NOLOCK) ON [o].[object_id] = [ta].[object_id]   
    LEFT  JOIN [sys].[types]        AS [basetypes] WITH (NOLOCK) ON [ip].[system_type_id] = [basetypes].[system_type_id] AND [basetypes].[system_type_id] = [basetypes].[user_type_id]
    LEFT  JOIN [sys].[types]        AS [types] WITH (NOLOCK) ON [ip].[user_type_id] = [types].[user_type_id]
WHERE 
    ([o].[type] = N'U' AND [ta].[is_filetable] = 0 OR [o].[type] = N'V')
    AND [i].[name] IS NOT NULL
    AND [i].[is_hypothetical] = 0
    AND ( [i].[xml_index_type] = 2 OR [i].[xml_index_type] = 3 )
    AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) AS [_results] ORDER BY XmlIndexType,ColumnSourceId,IndexId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90EncryptedAndClrDmlTriggerPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
           [s].[name]      AS [SchemaName],
           [t].[parent_id] AS [ParentId],
           [o].[name]      AS [ParentName],
           CASE [ta].[is_filetable] WHEN 1 THEN N'UF' ELSE [o].[type] END      AS [ObjectType],
           [t].[name]      AS [TriggerName],
           [t].[type]      AS [TriggerType],
           [t].[object_id] AS [ObjectId],
           CONVERT(bit, [sm].[uses_ansi_nulls]) AS [IsAnsiNulls],
           CONVERT(bit, [sm].[uses_quoted_identifier]) AS [IsQuotedIdentifier],
           CONVERT(bit, [t].[is_not_for_replication]) AS [IsNotForReplication],
           CONVERT(bit, [t].[is_instead_of_trigger])  AS [IsInsteadOfTrigger],
           CONVERT(bit, [t].[is_disabled])            AS [IsDisabled],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsUpdateTrigger')) AS [IsUpdateTrigger],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsDeleteTrigger')) AS [IsDeleteTrigger],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsInsertTrigger')) AS [IsInsertTrigger],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsAfterTrigger'))  AS [IsAfterTrigger],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsFirstDeleteTrigger')) AS [IsFirstDeleteTrigger],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsFirstInsertTrigger')) AS [IsFirstInsertTrigger],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsFirstUpdateTrigger')) AS [IsFirstUpdateTrigger],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsLastDeleteTrigger'))  AS [IsLastDeleteTrigger],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsLastInsertTrigger'))  AS [IsLastInsertTrigger],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsLastUpdateTrigger'))  AS [IsLastUpdateTrigger],
           NULL                           AS [AssemblyId],
           NULL                           AS [Assembly],
           NULL                           AS [AssemblyClass],
           NULL                           AS [AssemblyMethod],
           [sm].[execute_as_principal_id] AS [ExecuteAsId],
           [p].[name] AS [ExecuteAsName]

FROM
           [sys].[triggers]       AS [t] WITH (NOLOCK)
           INNER JOIN [sys].[objects]        AS [o] WITH (NOLOCK) ON [o].[object_id] = [t].[parent_id]
           LEFT JOIN [sys].[schemas]        AS [s] WITH (NOLOCK) ON [o].[schema_id] = [s].[schema_id]
           LEFT JOIN [sys].[sql_modules]    AS [sm] WITH (NOLOCK) ON [t].[object_id] = [sm].[object_id]
           LEFT JOIN [sys].[database_principals] [p] WITH (NOLOCK) ON [p].[principal_id] = [sm].[execute_as_principal_id]
           LEFT JOIN [sys].[tables] [ta] WITH (NOLOCK) ON [ta].[object_id] = [o].[object_id]
WHERE
           [t].[parent_class] = 1
           AND OBJECTPROPERTY([t].[object_id], N'IsEncrypted') = 1
           AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))

UNION

SELECT
           [s].[name]      AS [SchemaName],
           [t].[parent_id] AS [ParentId],
           [o].[name]      AS [ParentName],
           [o].[type]      AS [ObjectType],
           [t].[name]      AS [TriggerName],
           [t].[type]      AS [TriggerType],
           [t].[object_id] AS [ObjectId],
           NULL            AS [IsAnsiNulls],
           NULL            AS [IsQuotedIdentifier],
           CONVERT(bit, [t].[is_not_for_replication]) AS [IsNotForReplication],
           CONVERT(bit, [t].[is_instead_of_trigger])  AS [IsInsteadOfTrigger],
           CONVERT(bit, [t].[is_disabled])            AS [IsDisabled],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsUpdateTrigger')) AS [IsUpdateTrigger],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsDeleteTrigger')) AS [IsDeleteTrigger],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsInsertTrigger')) AS [IsInsertTrigger],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsAfterTrigger'))  AS [IsAfterTrigger],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsFirstDeleteTrigger')) AS [IsFirstDeleteTrigger],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsFirstInsertTrigger')) AS [IsFirstInsertTrigger],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsFirstUpdateTrigger')) AS [IsFirstUpdateTrigger],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsLastDeleteTrigger'))  AS [IsLastDeleteTrigger],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsLastInsertTrigger'))  AS [IsLastInsertTrigger],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsLastUpdateTrigger'))  AS [IsLastUpdateTrigger],
           [as].[assembly_id]            AS [AssemblyId],
           [as].[name]                   AS [Assembly],
           [am].[assembly_class]         AS [AssemblyClass],
           [am].[assembly_method]        AS [AssemblyMethod],
           [am].[execute_as_principal_id] AS [ExecuteAsId],
           [p].[name] AS [ExecuteAsName]
FROM
           [sys].[triggers]       AS [t] WITH (NOLOCK)
           INNER JOIN [sys].[objects]        AS [o] WITH (NOLOCK) ON [o].[object_id] = [t].[parent_id]
           LEFT JOIN [sys].[schemas]        AS [s] WITH (NOLOCK) ON [o].[schema_id] = [s].[schema_id]
           LEFT JOIN [sys].[assembly_modules]    AS [am] WITH (NOLOCK) ON [am].[object_id] = [t].[object_id]
           LEFT JOIN [sys].[assemblies]          AS [as] WITH (NOLOCK) ON [as].[assembly_id] = [am].[assembly_id]
           LEFT JOIN [sys].[database_principals] [p] WITH (NOLOCK) ON [p].[principal_id] = [am].[execute_as_principal_id]
WHERE
           [t].[parent_class] = 1
           AND [t].[type] = N'TA'
           AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))
) AS [_results] ORDER BY ParentId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql130DmlTriggerPopulator' as [PopulatorName];
SELECT * FROM (
SELECT DISTINCT
           [s].[name]      AS [SchemaName],
           [t].[parent_id] AS [ParentId],
           [o].[name]      AS [ParentName],
           CASE [ta].[is_filetable] WHEN 1 THEN N'UF' ELSE [o].[type] END      AS [ColumnSourceType],
           [t].[name]      AS [TriggerName],
           [t].[object_id] AS [ObjectId],
           [m].[uses_native_compilation] AS [UsesNativeCompilation],
           [m].[is_schema_bound] AS [IsSchemaBound],
           CONVERT(bit, [t].[is_disabled])            AS [IsDisabled],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'IsEncrypted')) AS [IsEncrypted],
           CONVERT(bit, [t].[is_not_for_replication]) AS [IsNotForReplication],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsUpdateTrigger')) AS [IsUpdateTrigger],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsDeleteTrigger')) AS [IsDeleteTrigger],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsInsertTrigger')) AS [IsInsertTrigger],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsAfterTrigger'))  AS [IsAfterTrigger],
           CONVERT(bit, [t].[is_instead_of_trigger])  AS [IsInsteadOfTrigger],
           CASE WHEN OBJECTPROPERTY([t].[object_id], N'ExecIsFirstDeleteTrigger') = 1 THEN 0 WHEN OBJECTPROPERTY([t].[object_id], N'ExecIsLastDeleteTrigger') = 1 THEN 2 ELSE 1 END AS [DeleteOrder],
           CASE WHEN OBJECTPROPERTY([t].[object_id], N'ExecIsFirstInsertTrigger') = 1 THEN 0 WHEN OBJECTPROPERTY([t].[object_id], N'ExecIsLastInsertTrigger') = 1 THEN 2 ELSE 1 END AS [InsertOrder],
           CASE WHEN OBJECTPROPERTY([t].[object_id], N'ExecIsFirstUpdateTrigger') = 1 THEN 0 WHEN OBJECTPROPERTY([t].[object_id], N'ExecIsLastUpdateTrigger') = 1 THEN 2 ELSE 1 END AS [UpdateOrder],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsQuotedIdentOn')) AS [IsQuotedIdentifier],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsAnsiNullsOn'))   AS [IsAnsiNulls],
           [m].[definition] AS [Script],
           [m].[execute_as_principal_id] AS [ExecuteAsId],
           [p].[name] AS [ExecuteAsName],
           NULL AS [HasAmbiguousReference]
FROM
           [sys].[triggers]       AS [t] WITH (NOLOCK)
           INNER JOIN [sys].[objects]        AS [o] WITH (NOLOCK) ON [o].[object_id] = [t].[parent_id]
           LEFT JOIN [sys].[schemas]        AS [s] WITH (NOLOCK) ON [o].[schema_id] = [s].[schema_id]
           LEFT JOIN [sys].[sql_modules]    AS [m] WITH (NOLOCK) ON [t].[object_id] = [m].[object_id]
           LEFT JOIN [sys].[database_principals] [p] WITH (NOLOCK) ON [p].[principal_id] = [m].[execute_as_principal_id]
           
           LEFT JOIN [sys].[tables] [ta] WITH(NOLOCK) ON [ta].[object_id] = [o].[object_id]
WHERE
           [t].[parent_class] = 1
           AND OBJECTPROPERTY([t].[object_id], N'IsEncrypted') = 0
           AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))
) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90EncryptedAndClrDdlTriggerPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [t].[object_id]         AS [ObjectId],
        [t].[name]              AS [Name],
        [t].[type]              AS [TriggerType],
        [t].[parent_class]      AS [TriggerScope],
        [t].[is_instead_of_trigger] AS [IsInsteadOfTrigger],
        [t].[is_disabled]           AS [IsDisabled],
        NULL                        AS [AssemblyId],
        NULL                        AS [Assembly],
        NULL                        AS [AssemblyClass],
        NULL                        AS [AssemblyMethod],
        CONVERT(bit, ISNULL([sm].[uses_ansi_nulls], 0)) AS [IsAnsiNulls],
        CONVERT(bit, ISNULL([sm].[uses_quoted_identifier], 0)) AS [IsQuotedIdentifier],
        [sm].[execute_as_principal_id] AS [ExecuteAsId],
        [p].[name] AS [ExecuteAsName]
FROM
        [sys].[triggers] [t] WITH (NOLOCK)
        LEFT JOIN [sys].[sql_modules] [sm] WITH (NOLOCK) ON [sm].[object_id] = [t].[object_id]
        LEFT JOIN [sys].[database_principals] [p] WITH (NOLOCK) ON [p].[principal_id] = [sm].[execute_as_principal_id]
WHERE
        [t].[parent_class] = 0
        AND [sm].[definition] IS NULL
        AND [t].[type] = N'TR'
        AND ([t].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [t].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))
UNION

SELECT
        [t].[object_id]         AS [ObjectId],
        [t].[name]              AS [Name],
        [t].[type]              AS [TriggerType],
        [t].[parent_class]      AS [TriggerScope],
        [t].[is_instead_of_trigger] AS [IsInsteadOfTrigger],
        [t].[is_disabled]           AS [IsDisabled],
        [a].[assembly_id]       AS [AssemblyId],
        [a].[name]              AS [Assembly],
        [am].[assembly_class]   AS [AssemblyClass],
        [am].[assembly_method]  AS [AssemblyMethod],
        NULL                    AS [IsAnsiNulls],
        NULL                    AS [IsQuotedIdentifier],
        [am].[execute_as_principal_id] AS [ExecuteAsId],
        [p].[name] AS [ExecuteAsName]
FROM
        [sys].[triggers] [t] WITH (NOLOCK)
        LEFT JOIN [sys].[assembly_modules] [am] WITH (NOLOCK) ON [am].[object_id] = [t].[object_id]
        LEFT JOIN [sys].[assemblies] [a] WITH (NOLOCK) ON [a].[assembly_id] = [am].[assembly_id]
        LEFT JOIN [sys].[database_principals] [p] WITH (NOLOCK) ON [p].[principal_id] = [am].[execute_as_principal_id]
WHERE
        [t].[parent_class] = 0
        AND [t].[type] = N'TA'
        AND ([t].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [t].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))
) AS [_results] ORDER BY ObjectId DESC;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90EncryptedDdlTriggerEventPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [e].[object_id] AS [TriggerId],
        [e].[type_desc] AS [EventType],
        [e].[is_first]  AS [IsFirst],
        [e].[is_last]   AS [IsLast],
        [t].[name]      AS [Name],
        [e].[type]      AS [EventTypeId],
        [e].[event_group_type] AS [EventGroupType]
FROM
        [sys].[trigger_events] [e] WITH (NOLOCK)
        INNER JOIN [sys].[triggers] [t] WITH (NOLOCK) ON [t].[object_id] = [e].[object_id]
        LEFT OUTER JOIN [sys].[sql_modules] [m] WITH (NOLOCK) ON [t].[object_id] = [m].[object_id]
WHERE
        [t].[parent_class] = 0
        AND ([m].[definition] IS NULL OR [t].[type] <> N'TR')
        AND ([t].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [t].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))
) AS [_results] ORDER BY TriggerId DESC;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90DdlTriggerPopulator' as [PopulatorName];
SELECT * FROM (
SELECT DISTINCT
        [t].[object_id]         AS [ObjectId],
        [t].[name]              AS [Name],
        [t].[parent_class]      AS [TriggerScope],
        CONVERT(bit, [t].[is_disabled])            AS [IsDisabled],
        CONVERT(bit, ISNULL([sm].[uses_ansi_nulls], 0)) AS [IsAnsiNulls],
        CONVERT(bit, ISNULL([sm].[uses_quoted_identifier], 0)) AS [IsQuotedIdentifier],
        [sm].[definition] AS [Script],
        [sm].[execute_as_principal_id] AS [ExecuteAsId],
        [p].[name] AS [ExecuteAsName]
FROM
        [sys].[triggers] [t] WITH (NOLOCK)
        LEFT JOIN [sys].[sql_modules] [sm] WITH (NOLOCK) ON [sm].[object_id] = [t].[object_id]
        LEFT JOIN [sys].[database_principals] [p] WITH (NOLOCK) ON [p].[principal_id] = [sm].[execute_as_principal_id]
WHERE
        [t].[parent_class] = 0
        AND [t].[type] = N'TR' AND [sm].[definition] IS NOT NULL
        AND ([t].[is_ms_shipped] = 0)
) AS [_results] ORDER BY ObjectId DESC;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90DdlTriggerEventPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [e].[object_id] AS [TriggerId],
        [e].[type_desc] AS [EventType],
        [e].[is_first]  AS [IsFirst],
        [e].[is_last]   AS [IsLast],
        [t].[name]      AS [Name],
        [e].[type]      AS [EventTypeId],
        [e].[event_group_type] AS [EventGroupType]
FROM
        [sys].[trigger_events] [e] WITH (NOLOCK)
        INNER JOIN [sys].[triggers] [t] WITH (NOLOCK) ON [t].[object_id] = [e].[object_id]
        LEFT OUTER JOIN [sys].[sql_modules] [m] WITH (NOLOCK) ON [t].[object_id] = [m].[object_id]
WHERE
        [t].[parent_class] = 0
        AND [m].[definition] IS NOT NULL
        AND [t].[type] = N'TR'
        AND ([t].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [t].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))
) AS [_results] ORDER BY TriggerId DESC;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90SynonymPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [ssy].[object_id]    AS [ObjectId],
        [ssy].[name] AS [Name],
        [ssy].[schema_id] AS [SchemaId],
        SCHEMA_NAME([ssy].[schema_id]) AS [SchemaName],
        [ssy].[base_object_name] AS [ReferenceObjectName]
FROM    [sys].[synonyms] [ssy] WITH (NOLOCK)
WHERE   ([ssy].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [ssy].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))
) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.SqlDefaultPopulator' as [PopulatorName];
SELECT * FROM (
SELECT       [so].[object_id]           AS [LegacyId],
          SCHEMA_NAME([so].[schema_id]) AS [LegacySchemaName],
          [so].[schema_id]              AS [LegacySchemaId],
          [so].[name]                   AS [LegacyName],
          [sm].[definition]             AS [Script]
FROM      [sys].[objects]     AS [so] WITH (NOLOCK)
LEFT JOIN [sys].[sql_modules] AS [sm] WITH (NOLOCK) ON [sm].[object_id] = [so].[object_id]
LEFT JOIN [sys].[tables] AS [t]  WITH (NOLOCK) ON [t].[object_id] = [so].[object_id]
WHERE     OBJECTPROPERTY([so].[object_id], N'IsDefault') <> 0
          AND ([so].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [so].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))
) AS [_results] ORDER BY LegacyId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.SqlLegacyDataConstriantColumnBindingPopulator' as [PopulatorName];
SELECT * FROM (
SELECT     SCHEMA_NAME([do].[object_id]) AS [LegacySchemaName]
          ,[do].[name] AS [LegacyName]
          ,[do].[object_id] AS [LegacyId]
          ,SCHEMA_NAME([co].[schema_id]) AS [SchemaName]
          ,[co].[name] AS [ColumnSourceName]
          ,[sc].[name] AS [ColumnName]
          ,[sc].[object_id] AS [ColumnSourceId]
          ,[sc].[column_id] AS [ColumnId]
FROM       [sys].[columns] AS [sc] WITH (NOLOCK)
INNER JOIN [sys].[objects] AS [co] WITH (NOLOCK) ON [sc].[object_id] = [co].[object_id]
INNER JOIN [sys].[objects] AS [do] WITH (NOLOCK) ON [sc].[default_object_id] = [do].[object_id]
LEFT JOIN  [sys].[tables] AS [t] WITH (NOLOCK) ON [t].[object_id] = [sc].[object_id]
WHERE      OBJECTPROPERTY([do].[object_id], N'IsDefault') <> 0
           AND [sc].[is_computed] = 0 AND ([do].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [do].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))
) AS [_results] ORDER BY LegacyId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.SqlLegacyDataConstraintUddtBindingPopulator' as [PopulatorName];
SELECT * FROM (
SELECT     [sc].[name]        AS [UddtSchemaName]
          ,[st].[name]        AS [UddtName]
          ,[st].[user_type_id]   AS [UddtId]
          ,[lo].[object_id]   AS [LegacyId]
          ,SCHEMA_NAME([lo].[object_id]) AS [LegacySchemaName]
          ,[lo].[name]        AS [LegacyName]
FROM       [sys].[types]   AS [st] WITH (NOLOCK)
INNER JOIN [sys].[objects] AS [lo] WITH (NOLOCK) ON [lo].[object_id] = [st].[default_object_id]
LEFT  JOIN [sys].[schemas] AS [sc] WITH (NOLOCK) ON [sc].[schema_id] = [st].[schema_id]
LEFT  JOIN [sys].[tables] AS [t] WITH (NOLOCK) ON [t].[object_id] = [lo].[object_id]
WHERE      [st].[user_type_id] > 256
           AND OBJECTPROPERTY([lo].[object_id], N'IsDefault') <> 0
           AND ([lo].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [lo].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))
) AS [_results] ORDER BY LegacyId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.SqlRulePopulator' as [PopulatorName];
SELECT * FROM (
SELECT    [so].[object_id]              AS [LegacyId],
          SCHEMA_NAME([so].[schema_id]) AS [LegacySchemaName],
          [so].[schema_id]              AS [LegacySchemaId],
          [so].[name]                   AS [LegacyName],
          [sm].[definition]             AS [Script]
FROM      [sys].[objects]     AS [so] WITH (NOLOCK)
LEFT JOIN [sys].[sql_modules] AS [sm] WITH (NOLOCK) ON [sm].[object_id] = [so].[object_id]
WHERE     OBJECTPROPERTY([so].[object_id], N'IsRule') <> 0
          AND ([so].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [so].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))
) AS [_results] ORDER BY LegacyId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.SqlLegacyDataConstriantColumnBindingPopulator' as [PopulatorName];
SELECT * FROM (
SELECT     SCHEMA_NAME([do].[object_id]) AS [LegacySchemaName]
          ,[do].[name] AS [LegacyName]
          ,[do].[object_id] AS [LegacyId]
          ,SCHEMA_NAME([co].[schema_id]) AS [SchemaName]
          ,[co].[name] AS [ColumnSourceName]
          ,[sc].[name] AS [ColumnName]
          ,[sc].[object_id] AS [ColumnSourceId]
          ,[sc].[column_id] AS [ColumnId]
FROM       [sys].[columns] AS [sc] WITH (NOLOCK)
INNER JOIN [sys].[objects] AS [co] WITH (NOLOCK) ON [sc].[object_id] = [co].[object_id]
INNER JOIN [sys].[objects] AS [do] WITH (NOLOCK) ON [sc].[rule_object_id] = [do].[object_id]
WHERE      OBJECTPROPERTY([do].[object_id], N'IsRule') <> 0
           AND [sc].[is_computed] = 0 AND ([do].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [do].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))
) AS [_results] ORDER BY LegacyId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.SqlLegacyDataConstraintUddtBindingPopulator' as [PopulatorName];
SELECT * FROM (
SELECT     [sc].[name]        AS [UddtSchemaName]
          ,[st].[name]        AS [UddtName]
          ,[st].[user_type_id]   AS [UddtId]
          ,[lo].[object_id]   AS [LegacyId]
          ,SCHEMA_NAME([lo].[object_id]) AS [LegacySchemaName]
          ,[lo].[name]        AS [LegacyName]
FROM       [sys].[types]   AS [st] WITH (NOLOCK)
INNER JOIN [sys].[objects] AS [lo] WITH (NOLOCK) ON [lo].[object_id] = [st].[rule_object_id]
LEFT  JOIN [sys].[schemas] AS [sc] WITH (NOLOCK) ON [sc].[schema_id] = [st].[schema_id]
WHERE      [st].[user_type_id] > 256
           AND OBJECTPROPERTY([lo].[object_id], N'IsRule') <> 0
           AND ([lo].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [lo].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))
) AS [_results] ORDER BY LegacyId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90MessageTypePopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [s].[message_type_id]         AS [ObjectId],
        [s].[name]                    AS [Name],
        [s].[principal_id]            AS [OwnerId],
        USER_NAME([s].[principal_id]) AS [Owner],
        [s].[validation]              AS [ValidationType],
        [s].[xml_collection_id]       AS [XmlCollectionId],
        [x].[name]                    AS [XmlCollectionName],
        SCHEMA_NAME([x].[schema_id])  AS [XmlCollectionSchemaName]
FROM    [sys].[service_message_types] [s] WITH (NOLOCK)
LEFT    JOIN [sys].[xml_schema_collections] [x] WITH (NOLOCK) ON [x].[xml_collection_id] = [s].[xml_collection_id]
) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql120QueuePopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [ssq].[object_id]               AS [ObjectId],
        [ssq].[schema_id]               AS [SchemaId],
        SCHEMA_NAME([ssq].[schema_id])  AS [SchemaName],
        [ssq].[name]                    AS [Name], 
        [ssq].[max_readers]             AS [MaxReaders], 
        [ssq].[activation_procedure]    AS [ActivationProcedure], 
        [sdp2].[name]                   AS [ExecuteAsName], 
        [ssq].[execute_as_principal_id] AS [ExecuteAsId],
        [ssq].[is_activation_enabled]   AS [IsActivationEnabled], 
        [ssq].[is_receive_enabled]      AS [IsReceiveEnabled], 
        [ssq].[is_enqueue_enabled]      AS [IsEnqueueEnabled], 
        [ssq].[is_retention_enabled]    AS [IsRetentionEnabled],
        [ssq].[is_poison_message_handling_enabled] AS [IsPoisonMessageHandlingEnabled],
        [sfg].[type]                    AS [DataspaceType],
        [sin].[data_space_id]           AS [DataspaceId],
        [sfg].[name]                    AS [DataspaceName],
        [ssq].[create_date]             AS [CreatedDate]
FROM    
        [sys].[service_queues] [ssq] WITH (NOLOCK)
LEFT    JOIN [sys].[database_principals] [sdp2] WITH (NOLOCK) ON [sdp2].[principal_id] = [ssq].[execute_as_principal_id]
INNER   JOIN [sys].[internal_tables] [sit] WITH (NOLOCK) ON [ssq].[object_id] = [sit].[parent_object_id]
INNER   JOIN [sys].[indexes] [sin] WITH (NOLOCK) ON [sin].[object_id] = [sit].[object_id] AND [sin].[index_id] < 2
INNER   JOIN [sys].[filegroups] [sfg] WITH (NOLOCK) ON [sfg].[data_space_id] = [sin].[data_space_id]
WHERE   ([ssq].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [ssq].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90ContractPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [ssc].[service_contract_id] AS [ContractId],
        [ssc].[name] AS [ContractName],
        [ssc].[principal_id] AS [OwnerId],
        USER_NAME([ssc].[principal_id]) AS [Owner]
FROM    [sys].[service_contracts] [ssc] WITH (NOLOCK)
) AS [_results] ORDER BY ContractId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90ContractMessageTypeUsagePopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [scmu].[service_contract_id] AS [ContractId],
        [scmu].[message_type_id] AS [MessageTypeId],
        [ssc].[name] AS [ContractName],
        [mt].[name] AS [MessageTypeName],
        [scmu].[is_sent_by_initiator] AS [IsSentByInitiator],
        [scmu].[is_sent_by_target] AS [IsSentByTarget]
FROM    [sys].[service_contract_message_usages] [scmu] WITH (NOLOCK)
LEFT    JOIN [sys].[service_contracts] [ssc] WITH (NOLOCK) ON [ssc].[service_contract_id] = [scmu].[service_contract_id]
LEFT    JOIN [sys].[service_message_types] [mt] WITH (NOLOCK) ON [mt].[message_type_id] = [scmu].[message_type_id]
WHERE [ssc].[name] IS NOT NULL
) AS [_results] ORDER BY ContractId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90ServicePopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [svc].[service_id]             AS [ServiceId],
        [svc].[name]                   AS [ServiceName],
        [svc].[principal_id]           AS [OwnerId],
        USER_NAME([svc].[principal_id]) AS [Owner],
        [svc].[service_queue_id]       AS [QueueId],
        [ssq].[name]                   AS [QueueName],
        SCHEMA_NAME([ssq].[schema_id]) AS [QueueSchema]
FROM
        [sys].[services] [svc] WITH (NOLOCK)
LEFT    JOIN [sys].[service_queues] [ssq] WITH (NOLOCK) ON [svc].[service_queue_id] = [ssq].[object_id]
) AS [_results] ORDER BY ServiceId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90ServiceContractUsagePopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [scu].[service_id]             AS [ServiceId],
        [svc].[name]                   AS [ServiceName],
        [sc].[service_contract_id]     AS [ContractId],
        [sc].[name]                    AS [ContractName]
FROM
        [sys].[service_contract_usages] [scu] WITH (NOLOCK)
LEFT    JOIN [sys].[services] [svc] WITH (NOLOCK) ON [svc].[service_id] = [scu].[service_id]
LEFT    JOIN [sys].[service_contracts] [sc] WITH (NOLOCK) ON [sc].[service_contract_id] = [scu].[service_contract_id]
WHERE [sc].[name] IS NOT NULL
) AS [_results] ORDER BY ServiceId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90EventNotificationPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [en].[object_id]    AS [EventNotificationId],
        [en].[name]         AS [Name],
        [en].[parent_class] AS [ScopeId],
        [en].[parent_id]    AS [OnObjectId],
        [o].[name]          AS [OnObjectName],
        SCHEMA_NAME([o].[schema_id]) AS [OnObjectSchema],
        [o].[type]          AS [OnObjectType],
        CONVERT(bit, CASE WHEN [en].[creator_sid] IS NOT NULL THEN 1 ELSE 0 END) AS [FanIn],
        [en].[service_name]      AS [ServiceName],
        [en].[broker_instance]   AS [BrokerInstance],
        CAST(CASE WHEN CONVERT(nvarchar(128), [db].[service_broker_guid]) = [en].[broker_instance] THEN 1 ELSE 0 END AS bit)
        AS [IsCurrentDatabase]
FROM
        [sys].[event_notifications] [en] WITH (NOLOCK)
LEFT    JOIN [sys].[databases] [db] WITH (NOLOCK) ON DB_NAME() = [db].[name]
LEFT    JOIN [sys].[objects] [o] WITH (NOLOCK) ON [o].[object_id] = [en].[parent_id] AND [en].[parent_class] = 1
) AS [_results] ORDER BY EventNotificationId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90EventNotificationEventTypePopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [e].[object_id] AS [EventNotificationId],
        [e].[type_desc] AS [EventType],
        [en].[name]     AS [Name],
        [o].[name]      AS [OnObjectName],
        SCHEMA_NAME([o].[schema_id]) AS [OnObjectSchema]
FROM
        [sys].[events] [e] WITH (NOLOCK)
        INNER JOIN [sys].[event_notifications] [en] WITH (NOLOCK) ON [e].[object_id] = [en].[object_id]
        LEFT JOIN [sys].[objects] [o] WITH (NOLOCK) ON [o].[object_id] = [en].[parent_id] AND [en].[parent_class] = 1
WHERE    [e].[is_trigger_event] = 0 AND [e].[event_group_type] IS NULL
) AS [_results] ORDER BY EventNotificationId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql100EventNotificationEventGroupPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [e].[object_id] AS [EventNotificationId],
        [e].[event_group_type] as [EventGroupType],
        [en].[name]     AS [Name],
        [o].[name]      AS [OnObjectName],
        SCHEMA_NAME([o].[schema_id]) AS [OnObjectSchema]
FROM
        [sys].[events] [e] WITH (NOLOCK)
        INNER JOIN [sys].[event_notifications] [en] WITH (NOLOCK) ON [e].[object_id] = [en].[object_id] AND [e].[is_trigger_event] = 0 AND [e].[event_group_type] IS NOT NULL
        LEFT JOIN [sys].[objects] [o] WITH (NOLOCK) ON [o].[object_id] = [en].[parent_id] AND [en].[parent_class] = 1
GROUP BY  [e].[object_id], [e].[event_group_type], [en].[name], [o].[name], [o].[schema_id]
) AS [_results] ORDER BY EventNotificationId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90RemoteServiceBindingPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [rs].[remote_service_binding_id] AS [ObjectId],
        [rs].[name]                AS [Name],
        [rs].[remote_service_name] AS [RemoteServiceName],
        [rs].[principal_id]        AS [OwnerId],
        USER_NAME([rs].[principal_id]) AS [Owner],
        [rs].[remote_principal_id] AS [RemoteUserId],
        USER_NAME([rs].[remote_principal_id]) AS [RemoteUser],
        [rs].[is_anonymous_on]     AS [IsAnonymous]
FROM
        [sys].[remote_service_bindings] [rs] WITH (NOLOCK)
) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql100BrokerPriorityPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
    [p].[name]			AS [Name],
    [p].[service_contract_id]	AS [ContractId],
    [c].[name]			AS [ContractName],
    [p].[priority]			AS [Priority],
    [s].[service_id]		AS [LocalServiceId],
    [s].[name]			AS [LocalServiceName],
    [p].[remote_service_name]	AS [RemoteServiceName]
    
FROM
    [sys].[conversation_priorities] [p] WITH (NOLOCK)
    LEFT JOIN [sys].[service_contracts] [c] WITH (NOLOCK) ON [c].[service_contract_id] = [p].[service_contract_id]
    LEFT JOIN [sys].[services] [s] WITH (NOLOCK) ON [p].[local_service_id] = [s].[service_id]
) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90SignaturePopulator' as [PopulatorName];


SELECT [c].[class]                      AS [ObjectClass]
      ,[c].[major_id]                   AS [ObjectId]
      ,[c].[thumbprint]                 AS [Thumbprint]
      ,[c].[crypt_type]                 AS [CryptographyType]
      ,[c].[crypt_property]             AS [SignedBlob]
      ,NULL                             AS [ObjectType]
      ,[a].[name]                       AS [ObjectName]
      ,[k].[asymmetric_key_id]          AS [KeyId]
      ,[k].[name]                       AS [KeyName]
      ,[k].[pvt_key_encryption_type]    AS [PrivateKeyEncryptionType]
      ,NULL                             AS [SchemaName]
FROM [sys].[crypt_properties] [c] WITH (NOLOCK)
LEFT JOIN [sys].[assemblies] [a] WITH (NOLOCK) ON ([a].[assembly_id] = [c].[major_id])
LEFT JOIN [sys].[asymmetric_keys] [k] WITH (NOLOCK) ON ([c].[thumbprint] = [k].[thumbprint])
WHERE (([c].[crypt_type] = N'CPVA' OR [c].[crypt_type] = N'SPVA') AND [c].[class] = 5)

UNION

SELECT [c].[class]                      AS [ObjectClass]
      ,[c].[major_id]                   AS [ObjectId]
      ,[c].[thumbprint]                 AS [Thumbprint]
      ,[c].[crypt_type]                 AS [CryptographyType]
      ,[c].[crypt_property]             AS [SignedBlob]
      ,NULL                             AS [ObjectType]
      ,[a].[name]                       AS [ObjectName]
      ,[k].[certificate_id]             AS [KeyId]
      ,[k].[name]                       AS [KeyName]
      ,[k].[pvt_key_encryption_type]    AS [PrivateKeyEncryptionType]
      ,NULL                             AS [SchemaName]
FROM [sys].[crypt_properties] [c] WITH (NOLOCK)
LEFT JOIN [sys].[assemblies] [a] WITH (NOLOCK) ON ([a].[assembly_id] = [c].[major_id])
LEFT JOIN [sys].[certificates] [k] WITH (NOLOCK) ON ([c].[thumbprint] = [k].[thumbprint])
WHERE (([c].[crypt_type] = N'CPVC' OR [c].[crypt_type] = N'SPVC') AND [c].[class] = 5)
;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90SignaturePopulator' as [PopulatorName];

SELECT [c].[class]                      AS [ObjectClass]
      ,[c].[major_id]                   AS [ObjectId]
      ,[c].[thumbprint]                 AS [Thumbprint]
      ,[c].[crypt_type]                 AS [CryptographyType]
      ,[c].[crypt_property]             AS [SignedBlob]
      ,[o].[type]                       AS [ObjectType]
      ,[o].[name]                       AS [ObjectName]
      ,[k].[asymmetric_key_id]          AS [KeyId]
      ,[k].[name]                       AS [KeyName]
      ,[k].[pvt_key_encryption_type]    AS [PrivateKeyEncryptionType]
      ,[s].[name]                       AS [SchemaName]
FROM [sys].[crypt_properties] [c] WITH (NOLOCK)
LEFT JOIN [sys].[objects] [o] WITH (NOLOCK) ON ([o].[object_id] = [c].[major_id])
LEFT JOIN [sys].[asymmetric_keys] [k] WITH (NOLOCK) ON ([c].[thumbprint] = [k].[thumbprint])
LEFT JOIN [sys].[schemas] [s] WITH (NOLOCK) ON ([s].[schema_id] = [o].[schema_id])
WHERE (([c].[crypt_type] = N'CPVA' OR [c].[crypt_type] = N'SPVA') AND [c].[class] = 1 AND [o].[is_ms_shipped] = 0)

UNION

SELECT [c].[class]                      AS [ObjectClass]
      ,[c].[major_id]                   AS [ObjectId]
      ,[c].[thumbprint]                 AS [Thumbprint]
      ,[c].[crypt_type]                 AS [CryptographyType]
      ,[c].[crypt_property]             AS [SignedBlob]
      ,[o].[type]                       AS [ObjectType]
      ,[o].[name]                       AS [ObjectName]
      ,[k].[certificate_id]             AS [KeyId]
      ,[k].[name]                       AS [KeyName]
      ,[k].[pvt_key_encryption_type]    AS [PrivateKeyEncryptionType]
      ,[s].[name]                       AS [SchemaName]
FROM [sys].[crypt_properties] [c] WITH (NOLOCK)
LEFT JOIN [sys].[objects] [o] WITH (NOLOCK) ON ([o].[object_id] = [c].[major_id])
LEFT JOIN [sys].[certificates] [k] WITH (NOLOCK) ON ([c].[thumbprint] = [k].[thumbprint])
LEFT JOIN [sys].[schemas] [s] WITH (NOLOCK) ON ([s].[schema_id] = [o].[schema_id])
WHERE (([c].[crypt_type] = N'CPVC' OR [c].[crypt_type] = N'SPVC') AND [c].[class] = 1) AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ));
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql120SpecifiesDataCompressionOptionsPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        CASE 
            WHEN [i].[is_primary_key] = 1 THEN [pk].[object_id]
            WHEN [i].[is_unique_constraint] = 1 THEN [uq].[object_id]
            ELSE [o].[object_id]
        END  AS [ObjectId],
        CASE 
            WHEN [i].[is_primary_key] = 0 AND [i].[is_unique_constraint] = 0 THEN [i].[index_id]
            ELSE 0
        END  AS [ChildObjectId],
        [i].[type]                      AS [IndexType],
        [i].[is_primary_key]            AS [IsPrimaryKey],
        [i].[is_unique_constraint]      AS [IsUniqueConstraint],
        ISNULL([t].[is_filetable],0)    AS [IsFileTable],
        [p].[partition_number]          AS [PartitionNumber],
        [p].[data_compression]          AS [DataCompression],
        CONVERT(bit, CASE WHEN [i].[type] in (5, 6) THEN 1 ELSE 0 END) AS [IsColumnStoreIndex],
        CASE 
            WHEN [i].[is_primary_key] = 1 THEN [pk].[name]
            WHEN [i].[is_unique_constraint] = 1 THEN [uq].[name]
            ELSE [o].[name]
        END  AS [Name],        
       CASE 
            WHEN [i].[is_primary_key] = 1 THEN SCHEMA_NAME([pk].[schema_id])
            WHEN [i].[is_unique_constraint] = 1 THEN SCHEMA_NAME([uq].[schema_id])
            ELSE SCHEMA_NAME([o].[schema_id])
        END  AS [SchemaName],
        CASE 
            WHEN [i].[type] <> 0 THEN[o].[object_id]
            ELSE null
        END  AS [ParentObjectId],
        CASE 
            WHEN [i].[type] <> 0 THEN [o].[schema_id]
            ELSE null
        END  AS [ParentSchemaId],
        CASE 
            WHEN [i].[type] <> 0 THEN [o].[is_ms_shipped]
            ELSE 0
        END  AS [ParentIsMsShipped]
FROM
        [sys].[partitions] [p] WITH (NOLOCK)
        INNER JOIN [sys].[indexes] [i] WITH (NOLOCK) ON [p].[object_id] = [i].[object_id] AND [p].[index_id] = [i].[index_id]
        INNER JOIN [sys].[objects] [o] WITH (NOLOCK) ON [i].[object_id] = [o].[object_id]
        LEFT  JOIN [sys].[tables] AS [t] WITH (NOLOCK) ON [t].[object_id] = [o].[object_id]
        LEFT JOIN [sys].[key_constraints] as [pk] WITH(NOLOCK) ON [pk].[parent_object_id] = [i].[object_id] and [i].[is_primary_key] = 1 and [pk].[type]=N'PK' and [i].[index_id] = [pk].[unique_index_id]
        LEFT JOIN [sys].[key_constraints] as [uq] WITH(NOLOCK) ON [uq].[parent_object_id] = [i].[object_id] and [i].[is_unique_constraint] = 1 and [uq].[type]=N'UQ' and [i].[index_id] = [uq].[unique_index_id]
WHERE [i].[is_hypothetical] = 0
        AND 
        ( 
            ( 
                ([i].[type] = 0 OR [i].[type] = 1 OR [i].[type] = 2) 
                 AND ([p].[data_compression] = 1 OR [p].[data_compression] = 2)
            )
            OR 
            (
                ([i].[type] = 5 OR [i].[type] = 6)
                AND ([p].[data_compression] = 4)
            )
        )
        AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))
) AS [_results] ORDER BY ObjectId,ChildObjectId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110SequencePopulator' as [PopulatorName];
SELECT * FROM (SELECT 
[s].[schema_id] AS [SchemaId],
SCHEMA_NAME([s].[schema_id]) AS [SchemaName],
[s].[object_id] AS [ObjectId],
[s].[name] AS [Name],
[s].[user_type_id] AS [TypeId],
[s].[precision] AS [Precision],
0 AS [Scale],
CONVERT(bit, ISNULL([types].[is_user_defined], 0)) AS [IsUserDefinedType],
[types].[name] AS [TypeName],
[basetypes].[name] AS [BaseTypeName],
SCHEMA_NAME([types].[schema_id]) AS [TypeSchemaName],
CASE WHEN [basetypes].[max_length] >= 0 AND [basetypes].[name] IN (N'nchar', N'nvarchar') THEN ([basetypes].[max_length] / 2) ELSE [basetypes].[max_length] END AS [Length],
[s].[start_value] AS [StartValue],
[s].[increment] AS [Increment],
[s].[minimum_value] AS [MinimumValue],
[s].[maximum_value] AS [MaximumValue],
[s].[is_cycling] AS [IsCycling],
[s].[is_cached] AS [IsCached],
[s].[cache_size] AS [CacheSize],
[s].[current_value] AS [CurrentValue],
[s].[last_used_value] AS [LastUsedValue],
[s].[is_exhausted] AS [IsExhausted]
FROM [sys].[sequences] AS [s] WITH (NOLOCK)
INNER JOIN [sys].[types] [basetypes] WITH (NOLOCK) ON [s].[system_type_id] = [basetypes].[system_type_id] AND [basetypes].[system_type_id] = [basetypes].[user_type_id]
LEFT JOIN [sys].[types] [types] WITH (NOLOCK) ON [s].[user_type_id] = [types].[user_type_id]) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql130SecurityPolicyPopulator' as [PopulatorName];
SELECT * FROM (SELECT    

        [sp].[name]                         AS [Name],
        [sp].[object_id]                    AS [Id],
        [sp].[schema_id]                    AS [SchemaId],
        SCHEMA_NAME([sp].[schema_id])       AS [SchemaName], 
        [sp].[is_enabled]                   AS [Enabled],
        [sp].[is_not_for_replication]       AS [NotForReplication],
        [sp].[is_schema_bound]              AS [IsSchemaBound]
        FROM [sys].[security_policies] [sp] WITH (NOLOCK)
        WHERE [is_ms_shipped] <> 1) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql130SecurityPredicatePopulator' as [PopulatorName];
SELECT 
        
        [pr].[object_id]                AS [PolicyId],
        [pl].[name]                     AS [PolicyName],
        SCHEMA_NAME([pl].[schema_id])   AS [PolicySchemaName],
        [pr].[security_predicate_id]    AS [PredicateId],
        [pr].[target_object_id]         AS [TargetObjectId],
        SCHEMA_NAME([tbl].[schema_id])  AS [TableTargetSchemaName],
        [tbl].[name]                    AS [TableTargetObjectName],
        SCHEMA_NAME([v].[schema_id])    AS [ViewTargetSchemaName],
        [v].[name]                      AS [ViewTargetObjectName],
        [pr].[predicate_definition]     AS [Script],
        [pr].[predicate_type]           AS [PredicateType],
        [pr].[operation]                AS [Operation]
        FROM [sys].[security_predicates] AS [pr] WITH (NOLOCK)
        INNER JOIN [sys].[security_policies] AS [pl] WITH(NOLOCK) ON [pr].[object_id] = [pl].[object_id] AND [pl].[is_ms_shipped] <> 1
        LEFT JOIN [sys].[tables] AS [tbl] WITH(NOLOCK) ON [pr].[target_object_id] = [tbl].[object_id]
        LEFT JOIN [sys].[views] AS [v] WITH(NOLOCK) ON [pr].[target_object_id] = [v].[object_id];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.SqlColumnEncryptionKeyPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [cek].[column_encryption_key_id] as [ColumnEncryptionKeyId],
        [cek].[name] as [ColumnEncryptionName]
FROM 
        [sys].[column_encryption_keys] [cek] WITH (NOLOCK)
) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.SqlColumnEncryptionKeyValuePopulator' as [PopulatorName];

SELECT
        [cekval].[column_encryption_key_id] as [ColumnEncryptionKeyId],
        [cekval].[column_master_key_definition_id] as [ColumnMasterKeyId],
        [cek].[name] as [ColumnEncryptionName],
        [cmk].[name] as [ColumnMasterKeyName],
        CONVERT(VARCHAR(8000), [cekval].[encrypted_value], 1) as [EncryptedValue],
        [cekval].[encryption_algorithm_name] as [EncryptionAlgorithm]
FROM 
        [sys].[column_encryption_key_values] [cekval] WITH (NOLOCK)
        INNER JOIN [sys].[column_encryption_keys] [cek] WITH (NOLOCK) ON [cekval].[column_encryption_key_id] = [cek].[column_encryption_key_id]
        INNER JOIN [sys].[column_master_key_definitions] [cmk] WITH (NOLOCK) ON [cekval].[column_master_key_definition_id] = [cmk].[column_master_key_definition_id]
;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.SqlColumnMasterKeyPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [cmk].[column_master_key_definition_id] as [ColumnMasterKeyId],
        [cmk].[name] as [ColumnMasterKeyName],
        [cmk].[key_store_provider_name] as [KeyStoreProvider],
        [cmk].[key_path] as [KeyPath]
FROM 
        [sys].[column_master_key_definitions] [cmk] WITH (NOLOCK)
) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.SqlAzureV12ExternalDataSourcePopulator' as [PopulatorName];
SELECT * FROM (
SELECT
	[eds].[name]                                    AS [Name],
	[eds].[data_source_id]                          AS [DataSourceId],
	[eds].[type]                                    AS [DataSourceType],
	[eds].[location]                                AS [Location],	
    [eds].[database_name]                           AS [DatabaseName],
    [eds].[shard_map_name]                          AS [ShardMapName],
	[c].[name]                                      AS [Credential]
FROM
	[sys].[external_data_sources]	AS [eds] WITH (NOLOCK)
	LEFT  JOIN [sys].[database_scoped_credentials] AS [c]  WITH (NOLOCK) ON [c].[credential_id] = [eds].[credential_id]) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.SqlAzureV12ExternalTablePopulator' as [PopulatorName];
SELECT * FROM (
SELECT  
        [t].[schema_id]                             AS [SchemaId],
        SCHEMA_NAME([t].[schema_id])                AS [SchemaName],
        [t].[name]                                  AS [ColumnSourceName], 
        [t].[object_id]                             AS [TableId],
        [t].[type]                                  AS [Type],
        [ds].[type]                                 AS [DataspaceType],
        [ds].[data_space_id]                        AS [DataspaceId],
        [ds].[name]                                 AS [DataspaceName],
        [si].[index_id]                             AS [IndexId],
        [si].[type]                                 AS [IndexType],
        [st].[uses_ansi_nulls]                      AS [IsAnsiNulls],
        ISNULL(CAST(OBJECTPROPERTY([st].[object_id],N'IsQuotedIdentOn') AS bit),0)
                                                    AS [IsQuotedIdentifier],
        ISNULL(CAST([st].[lock_on_bulk_load] AS bit),0)
                                                    AS [IsLockedOnBulkLoad],
        ISNULL([st].[text_in_row_limit],0)          AS [TextInRowLimit],
        CAST([st].[large_value_types_out_of_row] AS bit)
                                                    AS [LargeValuesOutOfRow],
        CAST(OBJECTPROPERTY([st].[object_id], N'TableHasVarDecimalStorageFormat') AS bit)
                                                    AS [HasVarDecimalStorageFormat],
        [st].[is_tracked_by_cdc]                    AS [IsTrackedByCDC],
        [st].[lock_escalation]                      AS [LockEscalation],     
        [t].[create_date]                           AS [CreateDate],
        [eds].[name]                                AS [DataSourceName],
        [et].[distribution_desc]                    AS [DistributionPolicyDesc],
        [et].[remote_schema_name]                   AS [ExternalSchemaName],
        [et].[remote_object_name]                   AS [ExternalObjectName],
        [c].[name]                                  AS [ShardingColumnName]
FROM    
        [sys].[objects] [t] WITH (NOLOCK)
        LEFT    JOIN [sys].[tables] [st] WITH (NOLOCK) ON [t].[object_id] = [st].[object_id]
        LEFT    JOIN (SELECT * FROM [sys].[indexes] WITH (NOLOCK) WHERE ISNULL([index_id],0) < 2) [si] ON [si].[object_id] = [st].[object_id]
        LEFT    JOIN [sys].[data_spaces] [ds] WITH (NOLOCK) ON [ds].[data_space_id] = [si].[data_space_id]
        LEFT    JOIN [sys].[external_tables] [et] WITH (NOLOCK) ON [et].[object_id] = [st].[object_id]
        LEFT    JOIN [sys].[external_data_sources] [eds]  WITH (NOLOCK) ON [eds].[data_source_id] = [et].[data_source_id]
        LEFT    JOIN [sys].[columns] [c] WITH (NOLOCK) ON [c].[object_id] = [t].[object_id] AND [c].[column_id] = [et].[sharding_col_id]
WHERE  [t].[type] = N'U' AND ISNULL([st].[is_filetable],0) = 0 AND ISNULL([st].[is_external],0) = 1 AND ([t].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [t].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) AS [_results] WHERE ( 1 = (select TOP 1 1 FROM @SystemTableTypeDiscriminators as [t] where [t].[TypeCode] COLLATE DATABASE_DEFAULT = [_results].[Type])) ORDER BY TableId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql130ExternalTableColumnPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [t].[name]                                              AS [ColumnSourceName], 
        [t].[object_id]                                         AS [TableId],
        SCHEMA_NAME([t].[schema_id])                            AS [SchemaName], 
        [c].[name]                                              AS [ColumnName], 
        [c].[user_type_id]                                      AS [TypeId],
        CONVERT(bit, ISNULL([types].[is_user_defined], 0))      AS [IsUserDefinedType],
        [types].[name]                                          AS [TypeName],
        [basetypes].[name]                                      AS [BaseTypeName],
        SCHEMA_NAME([types].[schema_id])                        AS [TypeSchemaName],
        [c].[column_id]                                         AS [ColumnId], 
        [c].[precision]                                         AS [Precision],
        [c].[scale]                                             AS [Scale],
        CASE 
            WHEN [c].[max_length] >= 0 AND [types].[name] IN (N'nchar', N'nvarchar') THEN ([c].[max_length] / 2) 
            ELSE [c].[max_length] 
        END                                                     AS [Length],
        CONVERT(bit, [c].[is_identity])                         AS [IsIdentity],
        CONVERT(bit, [c].[is_computed])                         AS [IsComputed],
        CONVERT(bit, ISNULL([ic].[is_not_for_replication], 0))  AS [IsNotForReplication],
        CAST(ISNULL([ic].[seed_value], 0) AS DECIMAL(38))       AS [IdentitySeed],
        CAST(ISNULL([ic].[increment_value], 0) AS DECIMAL(38))  AS [IdentityIncrement],
        CONVERT(bit, [c].[is_nullable])                         AS [IsNullable],
        [cc].[definition]                                       AS [ComputedText],
        [c].[is_rowguidcol]                                     AS [IsRowGuidColumn],
        [c].[collation_name]                                    AS [Collation],
        [c].[is_xml_document]                                   AS [IsXmlDocument],
        [c].[xml_collection_id]                                 AS [XmlCollectionId],
        [xscs].[name]                                           AS [XmlCollection],
        SCHEMA_NAME([xscs].[schema_id])                         AS [XmlCollectionSchemaName],
        [c].[is_sparse]                                         AS [IsSparse],
        [c].[is_column_set]                                     AS [IsColumnSet],
        [c].[is_filestream]                                     AS [IsFilestream],
        CASE [ta].[is_filetable] 
            WHEN 1 THEN N'UF' 
            ELSE N'U ' 
        END                                                     AS [Type],
        CONVERT(bit, ISNULL([cc].[is_persisted], 0))            AS [IsPersisted],
        CAST(ISNULL([indexCol].[IsPartitionColumn],0) AS BIT)   AS [IsPartitionColumn],
        CAST(0 AS BIT)                                                     AS [IsPrimaryKey],
        CAST(0 AS BIT)                                                     AS [IsForeignKey]
FROM    [sys].[columns] [c] WITH (NOLOCK)
        INNER   JOIN [sys].[objects] [t] WITH (NOLOCK) ON [c].[object_id] = [t].[object_id]
        LEFT	JOIN [sys].[tables] [ta] WITH(NOLOCK) ON [ta].[object_id] = [t].[object_id]
        LEFT    JOIN [sys].[types] [basetypes] WITH (NOLOCK) ON [c].[system_type_id] = [basetypes].[system_type_id] AND [basetypes].[system_type_id] = [basetypes].[user_type_id]
        LEFT    JOIN [sys].[types] [types] WITH (NOLOCK) ON [c].[user_type_id] = [types].[user_type_id]
        LEFT    JOIN [sys].[identity_columns] [ic] WITH (NOLOCK) ON [ic].[object_id] = [c].[object_id] AND [ic].[column_id] = [c].[column_id]
        LEFT    JOIN [sys].[computed_columns] [cc] WITH (NOLOCK) ON [cc].[object_id] = [c].[object_id] AND [cc].[column_id] = [c].[column_id]
        LEFT    JOIN [sys].[xml_schema_collections] [xscs] WITH (NOLOCK) ON [xscs].[xml_collection_id] = [c].[xml_collection_id]
        LEFT    JOIN (
            SELECT 1 AS [IsPartitionColumn], [indexCol].[column_id], [ix].[object_id] FROM [sys].[index_columns] [indexCol] WITH (NOLOCK) 
                INNER JOIN [sys].[indexes] [ix] WITH (NOLOCK) ON [indexCol].[index_id] = [ix].[index_id] AND [ix].[is_hypothetical] = 0 AND [ix].[type] IN (0,1,5) and [ix].[object_id] = [indexCol].[object_id]
            WHERE [indexCol].[partition_ordinal] > 0) AS [indexCol] ON [c].[object_id] = [indexCol].[object_id] AND [c].[column_id] = [indexCol].[column_id]
WHERE   [t].[type] = N'U' AND ISNULL([ta].[is_filetable],0) = 0 AND ISNULL([ta].[is_external],0) = 1 AND ([t].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [t].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) AS [_results] WHERE ( 1 = (select TOP 1 1 FROM @SystemTableTypeDiscriminators as [t] where [t].[TypeCode] COLLATE DATABASE_DEFAULT = [_results].[Type])) ORDER BY TableId,ColumnId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110ExtendedPropertyPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [sp].[class] AS [TypeId],
        [sp].[major_id] AS [MajorId],
        [sp].[minor_id] AS [MinorId],
        [sp].[name] AS [PropertyName],
        [sp].[value] AS [PropertyValue],
        N'DT' AS [ObjectType],
        SQL_VARIANT_PROPERTY([sp].[value], N'BaseType') AS [BaseType],
        [t].[name] AS [Level0Name],
        NULL AS [Level1Name],
        NULL AS [Level2Name],
        CAST(0 AS BIT) AS [IsPartitionScheme],
        CAST(0 AS BIT) AS [IsDefault],
        CAST(NULL AS TINYINT) AS [IndexType]
FROM
        [sys].[extended_properties] [sp] WITH (NOLOCK)
        INNER JOIN [sys].[triggers] [t] WITH (NOLOCK) ON [t].[object_id] = [sp].[major_id]
        LEFT JOIN [sys].[all_objects] [parent] WITH (NOLOCK) ON [parent].[object_id] = [t].[parent_id]
WHERE
        [sp].[class] = 1
        AND [t].[parent_class] = 0
        AND [sp].[name] <> N'microsoft_database_tools_support'
        AND ([t].[parent_id] = 0 AND ([t].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [t].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )) OR [t].[parent_id] <> 0 AND ([parent].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [parent].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )))
) AS [_results] ORDER BY Level0Name ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110ExtendedPropertyPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [sp].[class] AS [TypeId],
        [sp].[major_id] AS [MajorId],
        [sp].[minor_id] AS [MinorId],
        [sp].[name] AS [PropertyName],
        [sp].[value] AS [PropertyValue],
        N'EN' AS [ObjectType],
        SQL_VARIANT_PROPERTY([sp].[value], N'BaseType') AS [BaseType],
        CASE [en].[parent_class]
            WHEN 1 THEN SCHEMA_NAME([q].[schema_id])
            ELSE [en].[name]
        END AS [Level0Name],
        CASE [en].[parent_class]
            WHEN 1 THEN [q].[name]
            ELSE NULL
        END AS [Level1Name],
        CASE [en].[parent_class]
            WHEN 1 THEN [en].[name]
            ELSE NULL
        END AS [Level2Name],
        CAST(0 AS BIT) AS [IsPartitionScheme],
        CAST(0 AS BIT) AS [IsDefault],
        CAST(NULL AS TINYINT) AS [IndexType]
FROM
        [sys].[extended_properties] [sp] WITH (NOLOCK)
        INNER JOIN [sys].[event_notifications] [en] WITH (NOLOCK) ON [en].[object_id] = [sp].[major_id]
        LEFT JOIN [sys].[service_queues] [q] WITH (NOLOCK) ON [en].[parent_id] = [q].[object_id] AND [en].[parent_class] = 1
WHERE
        [sp].[class] = 1
        AND [sp].[name] <> N'microsoft_database_tools_support') AS [_results] ORDER BY Level0Name,Level1Name,Level2Name ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110ExtendedPropertyPopulator' as [PopulatorName];
SELECT * FROM (
SELECT 
        [sp].[class] AS [TypeId],
        [sp].[major_id] AS [MajorId],
        [sp].[minor_id] AS [MinorId],
        [sp].[name] AS [PropertyName], 
        [sp].[value] AS [PropertyValue],
        [so].[type] AS [ObjectType],
        SQL_VARIANT_PROPERTY([sp].[value], N'BaseType') AS [BaseType],
        CASE [sp].[class]
            WHEN  0 THEN NULL
            WHEN  3 THEN SCHEMA_NAME([sp].[major_id])
            WHEN  4 THEN USER_NAME([sp].[major_id])
            WHEN  5 THEN (SELECT TOP 1 [sa2].[name]   FROM [sys].[assemblies] [sa2] WITH (NOLOCK) WHERE [sa2].[assembly_id] = [sp].[major_id])
            WHEN  6 THEN SCHEMA_NAME((SELECT TOP 1 [st].[schema_id] FROM [sys].[types] [st] WITH (NOLOCK) WHERE [st].[user_type_id] = [sp].[major_id]))
            WHEN  8 THEN SCHEMA_NAME([tt].[schema_id])
            WHEN 10 THEN SCHEMA_NAME((SELECT TOP 1 [sxsc2].[schema_id] FROM [sys].[xml_schema_collections] [sxsc2] WITH (NOLOCK) WHERE [sxsc2].[xml_collection_id] = [sp].[major_id]))
            WHEN 15 THEN (SELECT TOP 1 [smt2].[name]  FROM [sys].[service_message_types] [smt2] WITH (NOLOCK) WHERE [smt2].[message_type_id] = [sp].[major_id]) COLLATE database_default
            WHEN 16 THEN (SELECT TOP 1 [ssc2].[name]  FROM [sys].[service_contracts] [ssc2] WITH (NOLOCK) WHERE [ssc2].[service_contract_id] = [sp].[major_id])
            WHEN 17 THEN (SELECT TOP 1 [ss2].[name]   FROM [sys].[services] [ss2] WITH (NOLOCK) WHERE [ss2].[service_id] = [sp].[major_id])
            WHEN 18 THEN (SELECT TOP 1 [srs2].[name]  FROM [sys].[remote_service_bindings] [srs2] WITH (NOLOCK) WHERE [srs2].[remote_service_binding_id] = [sp].[major_id])
            WHEN 19 THEN (SELECT TOP 1 [sr2].[name]   FROM [sys].[routes] [sr2] WITH (NOLOCK) WHERE [sr2].[route_id] = [sp].[major_id])
            WHEN 20 THEN (SELECT TOP 1 [sd2].[name]  FROM [sys].[data_spaces] [sd2] WITH (NOLOCK) WHERE [sd2].[data_space_id] = [sp].[major_id])
            WHEN 21 THEN (SELECT TOP 1 [spf2].[name]  FROM [sys].[partition_functions] [spf2] WITH (NOLOCK) WHERE [spf2].[function_id] = [sp].[major_id])
            WHEN 22 THEN (SELECT TOP 1 [sdf2].[name]  FROM [sys].[database_files] [sdf2] WITH (NOLOCK) WHERE [sdf2].[file_id] = [sp].[major_id])
            ELSE SCHEMA_NAME([so].[schema_id])
        END AS [Level0Name],
        CASE [sp].[class] 
            WHEN 3 THEN NULL
            WHEN 4 THEN NULL
            WHEN 6  THEN (SELECT TOP 1 [st2].[name] FROM [sys].[types] [st2] WITH (NOLOCK) WHERE [st2].[user_type_id] = [sp].[major_id])
            WHEN 8  THEN [tt].[name]
            WHEN 10 THEN (SELECT TOP 1 [sx2].[name] FROM [sys].[xml_schema_collections] [sx2] WITH (NOLOCK) WHERE [sx2].[xml_collection_id] = [sp].[major_id])
            WHEN 22 THEN NULL
            ELSE [so].[name]
        END AS [Level1Name],
        CASE [sp].[class]			
            WHEN 2 THEN (SELECT TOP 1 [spa].[name] FROM [sys].[parameters] [spa] WITH (NOLOCK) WHERE [spa].[object_id] = [sp].[major_id] AND [spa].[parameter_id] = [sp].[minor_id])
            WHEN 7 THEN (SELECT TOP 1 [si].[name] FROM [sys].[indexes] [si] WITH (NOLOCK) WHERE [si].[object_id] = [sp].[major_id] AND [si].[index_id] = [sp].[minor_id])
            WHEN 8 THEN COL_NAME([tt].[type_table_object_id] ,[sp].[minor_id])
            ELSE NULL
        END AS [Level2Name],
        CASE 
            WHEN [sp].[class] = 20 AND EXISTS((SELECT TOP 1 [sd2].[name]  FROM [sys].[data_spaces] [sd2] WITH (NOLOCK) WHERE [sd2].[data_space_id] = [sp].[major_id] AND [sd2].[type] = N'PS')) THEN
                CAST(1 AS BIT)
            ELSE CAST(0 AS BIT)
        END AS IsPartitionScheme,
        CAST(0 AS BIT) AS [IsDefault],
        [i].[type] AS [IndexType],
        CASE [sp].[class]
            WHEN 6 THEN [tt].[type_table_object_id]
            WHEN 8 THEN [tt].[type_table_object_id]
            WHEN 1 THEN [parent].[object_id]
            ELSE NULL
        END AS [ParentId],
         CASE [sp].[class]
            WHEN 6 THEN [tt].[schema_id]
            WHEN 8 THEN [tt].[schema_id]
            WHEN 1 THEN [parent].[schema_id]
            ELSE NULL
        END AS [ParentSchemaId],
        [sxi].[xml_index_type] AS [XMLIndexType],
        ISNULL([parent].[is_ms_shipped], 0) AS [IsMsShipped]
FROM    [sys].[extended_properties] [sp] WITH (NOLOCK) 
        LEFT JOIN [sys].[all_objects] [so] WITH (NOLOCK) ON [so].[object_id] = [sp].[major_id] AND [sp].[class] IN (1, 2, 7)
        LEFT JOIN [sys].[all_objects] [parent] WITH(NOLOCK) ON [so].[parent_object_id] = [parent].[object_id]
        LEFT JOIN [sys].[indexes] [i] WITH (NOLOCk) ON [sp].[class] = 7 AND [so].[object_id] = [i].[object_id]  AND [sp].[minor_id] = [i].[index_id]
        LEFT JOIN [sys].[table_types] [tt] WITH (NOLOCK) ON [sp].[class] IN (6,8) AND [tt].[user_type_id] = [sp].[major_id]
        LEFT JOIN [sys].[xml_indexes] [sxi] WITH (NOLOCk) ON [sp].[class] = 7 AND [so].[object_id] = [sxi].[object_id]  AND [sp].[minor_id] = [sxi].[index_id]
WHERE 
        [sp].[class] <> 1
        AND (OBJECTPROPERTY([so].[parent_object_id], N'TableIsFake') = 0 OR OBJECTPROPERTY([so].[parent_object_id], N'TableIsFake')  IS NULL)
        AND [sp].[class] <> 27
        AND ([so].[type] <> N'PG' OR [so].[type] IS NULL)
        AND [sp].[name] <> N'microsoft_database_tools_support'
        AND ([i].[is_primary_key] IS NULL OR [i].[is_primary_key] = 0)
        AND ([sp].[class] <> 7 OR [i].[object_id] IS NOT NULL)
        AND ([sp].[class] <> 7 OR ([so].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [so].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )))
) AS [_results] WHERE [TypeId] = 0
 ORDER BY Level0Name,Level1Name ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110ExtendedPropertyPopulator' as [PopulatorName];
SELECT * FROM (
SELECT 
        [sp].[class] AS [TypeId],
        [sp].[major_id] AS [MajorId],
        [sp].[minor_id] AS [MinorId],
        [sp].[name] AS [PropertyName], 
        [sp].[value] AS [PropertyValue],
        [so].[type] AS [ObjectType],
        SQL_VARIANT_PROPERTY([sp].[value], N'BaseType') AS [BaseType],
        CASE [sp].[class]
            WHEN  0 THEN NULL
            WHEN  3 THEN SCHEMA_NAME([sp].[major_id])
            WHEN  4 THEN USER_NAME([sp].[major_id])
            WHEN  5 THEN (SELECT TOP 1 [sa2].[name]   FROM [sys].[assemblies] [sa2] WITH (NOLOCK) WHERE [sa2].[assembly_id] = [sp].[major_id])
            WHEN  6 THEN SCHEMA_NAME((SELECT TOP 1 [st].[schema_id] FROM [sys].[types] [st] WITH (NOLOCK) WHERE [st].[user_type_id] = [sp].[major_id]))
            WHEN  8 THEN SCHEMA_NAME([tt].[schema_id])
            WHEN 10 THEN SCHEMA_NAME((SELECT TOP 1 [sxsc2].[schema_id] FROM [sys].[xml_schema_collections] [sxsc2] WITH (NOLOCK) WHERE [sxsc2].[xml_collection_id] = [sp].[major_id]))
            WHEN 15 THEN (SELECT TOP 1 [smt2].[name]  FROM [sys].[service_message_types] [smt2] WITH (NOLOCK) WHERE [smt2].[message_type_id] = [sp].[major_id]) COLLATE database_default
            WHEN 16 THEN (SELECT TOP 1 [ssc2].[name]  FROM [sys].[service_contracts] [ssc2] WITH (NOLOCK) WHERE [ssc2].[service_contract_id] = [sp].[major_id])
            WHEN 17 THEN (SELECT TOP 1 [ss2].[name]   FROM [sys].[services] [ss2] WITH (NOLOCK) WHERE [ss2].[service_id] = [sp].[major_id])
            WHEN 18 THEN (SELECT TOP 1 [srs2].[name]  FROM [sys].[remote_service_bindings] [srs2] WITH (NOLOCK) WHERE [srs2].[remote_service_binding_id] = [sp].[major_id])
            WHEN 19 THEN (SELECT TOP 1 [sr2].[name]   FROM [sys].[routes] [sr2] WITH (NOLOCK) WHERE [sr2].[route_id] = [sp].[major_id])
            WHEN 20 THEN (SELECT TOP 1 [sd2].[name]  FROM [sys].[data_spaces] [sd2] WITH (NOLOCK) WHERE [sd2].[data_space_id] = [sp].[major_id])
            WHEN 21 THEN (SELECT TOP 1 [spf2].[name]  FROM [sys].[partition_functions] [spf2] WITH (NOLOCK) WHERE [spf2].[function_id] = [sp].[major_id])
            WHEN 22 THEN (SELECT TOP 1 [sdf2].[name]  FROM [sys].[database_files] [sdf2] WITH (NOLOCK) WHERE [sdf2].[file_id] = [sp].[major_id])
            ELSE SCHEMA_NAME([so].[schema_id])
        END AS [Level0Name],
        CASE [sp].[class] 
            WHEN 3 THEN NULL
            WHEN 4 THEN NULL
            WHEN 6  THEN (SELECT TOP 1 [st2].[name] FROM [sys].[types] [st2] WITH (NOLOCK) WHERE [st2].[user_type_id] = [sp].[major_id])
            WHEN 8  THEN [tt].[name]
            WHEN 10 THEN (SELECT TOP 1 [sx2].[name] FROM [sys].[xml_schema_collections] [sx2] WITH (NOLOCK) WHERE [sx2].[xml_collection_id] = [sp].[major_id])
            WHEN 22 THEN NULL
            ELSE [so].[name]
        END AS [Level1Name],
        CASE [sp].[class]			
            WHEN 2 THEN (SELECT TOP 1 [spa].[name] FROM [sys].[parameters] [spa] WITH (NOLOCK) WHERE [spa].[object_id] = [sp].[major_id] AND [spa].[parameter_id] = [sp].[minor_id])
            WHEN 7 THEN (SELECT TOP 1 [si].[name] FROM [sys].[indexes] [si] WITH (NOLOCK) WHERE [si].[object_id] = [sp].[major_id] AND [si].[index_id] = [sp].[minor_id])
            WHEN 8 THEN COL_NAME([tt].[type_table_object_id] ,[sp].[minor_id])
            ELSE NULL
        END AS [Level2Name],
        CASE 
            WHEN [sp].[class] = 20 AND EXISTS((SELECT TOP 1 [sd2].[name]  FROM [sys].[data_spaces] [sd2] WITH (NOLOCK) WHERE [sd2].[data_space_id] = [sp].[major_id] AND [sd2].[type] = N'PS')) THEN
                CAST(1 AS BIT)
            ELSE CAST(0 AS BIT)
        END AS IsPartitionScheme,
        CAST(0 AS BIT) AS [IsDefault],
        [i].[type] AS [IndexType],
        CASE [sp].[class]
            WHEN 6 THEN [tt].[type_table_object_id]
            WHEN 8 THEN [tt].[type_table_object_id]
            WHEN 1 THEN [parent].[object_id]
            ELSE NULL
        END AS [ParentId],
         CASE [sp].[class]
            WHEN 6 THEN [tt].[schema_id]
            WHEN 8 THEN [tt].[schema_id]
            WHEN 1 THEN [parent].[schema_id]
            ELSE NULL
        END AS [ParentSchemaId],
        [sxi].[xml_index_type] AS [XMLIndexType],
        ISNULL([parent].[is_ms_shipped], 0) AS [IsMsShipped]
FROM    [sys].[extended_properties] [sp] WITH (NOLOCK) 
        LEFT JOIN [sys].[all_objects] [so] WITH (NOLOCK) ON [so].[object_id] = [sp].[major_id] AND [sp].[class] IN (1, 2, 7)
        LEFT JOIN [sys].[all_objects] [parent] WITH(NOLOCK) ON [so].[parent_object_id] = [parent].[object_id]
        LEFT JOIN [sys].[indexes] [i] WITH (NOLOCk) ON [sp].[class] = 7 AND [so].[object_id] = [i].[object_id]  AND [sp].[minor_id] = [i].[index_id]
        LEFT JOIN [sys].[table_types] [tt] WITH (NOLOCK) ON [sp].[class] IN (6,8) AND [tt].[user_type_id] = [sp].[major_id]
        LEFT JOIN [sys].[xml_indexes] [sxi] WITH (NOLOCk) ON [sp].[class] = 7 AND [so].[object_id] = [sxi].[object_id]  AND [sp].[minor_id] = [sxi].[index_id]
WHERE 
        [sp].[class] <> 1
        AND (OBJECTPROPERTY([so].[parent_object_id], N'TableIsFake') = 0 OR OBJECTPROPERTY([so].[parent_object_id], N'TableIsFake')  IS NULL)
        AND [sp].[class] <> 27
        AND ([so].[type] <> N'PG' OR [so].[type] IS NULL)
        AND [sp].[name] <> N'microsoft_database_tools_support'
        AND ([i].[is_primary_key] IS NULL OR [i].[is_primary_key] = 0)
        AND ([sp].[class] <> 7 OR [i].[object_id] IS NOT NULL)
        AND ([sp].[class] <> 7 OR ([so].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [so].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )))
) AS [_results] WHERE [TypeId] = 2
 ORDER BY Level0Name,Level1Name ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110ExtendedPropertyPopulator' as [PopulatorName];
SELECT * FROM (
SELECT 
        [sp].[class] AS [TypeId],
        [sp].[major_id] AS [MajorId],
        [sp].[minor_id] AS [MinorId],
        [sp].[name] AS [PropertyName], 
        [sp].[value] AS [PropertyValue],
        [so].[type] AS [ObjectType],
        SQL_VARIANT_PROPERTY([sp].[value], N'BaseType') AS [BaseType],
        CASE [sp].[class]
            WHEN  0 THEN NULL
            WHEN  3 THEN SCHEMA_NAME([sp].[major_id])
            WHEN  4 THEN USER_NAME([sp].[major_id])
            WHEN  5 THEN (SELECT TOP 1 [sa2].[name]   FROM [sys].[assemblies] [sa2] WITH (NOLOCK) WHERE [sa2].[assembly_id] = [sp].[major_id])
            WHEN  6 THEN SCHEMA_NAME((SELECT TOP 1 [st].[schema_id] FROM [sys].[types] [st] WITH (NOLOCK) WHERE [st].[user_type_id] = [sp].[major_id]))
            WHEN  8 THEN SCHEMA_NAME([tt].[schema_id])
            WHEN 10 THEN SCHEMA_NAME((SELECT TOP 1 [sxsc2].[schema_id] FROM [sys].[xml_schema_collections] [sxsc2] WITH (NOLOCK) WHERE [sxsc2].[xml_collection_id] = [sp].[major_id]))
            WHEN 15 THEN (SELECT TOP 1 [smt2].[name]  FROM [sys].[service_message_types] [smt2] WITH (NOLOCK) WHERE [smt2].[message_type_id] = [sp].[major_id]) COLLATE database_default
            WHEN 16 THEN (SELECT TOP 1 [ssc2].[name]  FROM [sys].[service_contracts] [ssc2] WITH (NOLOCK) WHERE [ssc2].[service_contract_id] = [sp].[major_id])
            WHEN 17 THEN (SELECT TOP 1 [ss2].[name]   FROM [sys].[services] [ss2] WITH (NOLOCK) WHERE [ss2].[service_id] = [sp].[major_id])
            WHEN 18 THEN (SELECT TOP 1 [srs2].[name]  FROM [sys].[remote_service_bindings] [srs2] WITH (NOLOCK) WHERE [srs2].[remote_service_binding_id] = [sp].[major_id])
            WHEN 19 THEN (SELECT TOP 1 [sr2].[name]   FROM [sys].[routes] [sr2] WITH (NOLOCK) WHERE [sr2].[route_id] = [sp].[major_id])
            WHEN 20 THEN (SELECT TOP 1 [sd2].[name]  FROM [sys].[data_spaces] [sd2] WITH (NOLOCK) WHERE [sd2].[data_space_id] = [sp].[major_id])
            WHEN 21 THEN (SELECT TOP 1 [spf2].[name]  FROM [sys].[partition_functions] [spf2] WITH (NOLOCK) WHERE [spf2].[function_id] = [sp].[major_id])
            WHEN 22 THEN (SELECT TOP 1 [sdf2].[name]  FROM [sys].[database_files] [sdf2] WITH (NOLOCK) WHERE [sdf2].[file_id] = [sp].[major_id])
            ELSE SCHEMA_NAME([so].[schema_id])
        END AS [Level0Name],
        CASE [sp].[class] 
            WHEN 3 THEN NULL
            WHEN 4 THEN NULL
            WHEN 6  THEN (SELECT TOP 1 [st2].[name] FROM [sys].[types] [st2] WITH (NOLOCK) WHERE [st2].[user_type_id] = [sp].[major_id])
            WHEN 8  THEN [tt].[name]
            WHEN 10 THEN (SELECT TOP 1 [sx2].[name] FROM [sys].[xml_schema_collections] [sx2] WITH (NOLOCK) WHERE [sx2].[xml_collection_id] = [sp].[major_id])
            WHEN 22 THEN NULL
            ELSE [so].[name]
        END AS [Level1Name],
        CASE [sp].[class]			
            WHEN 2 THEN (SELECT TOP 1 [spa].[name] FROM [sys].[parameters] [spa] WITH (NOLOCK) WHERE [spa].[object_id] = [sp].[major_id] AND [spa].[parameter_id] = [sp].[minor_id])
            WHEN 7 THEN (SELECT TOP 1 [si].[name] FROM [sys].[indexes] [si] WITH (NOLOCK) WHERE [si].[object_id] = [sp].[major_id] AND [si].[index_id] = [sp].[minor_id])
            WHEN 8 THEN COL_NAME([tt].[type_table_object_id] ,[sp].[minor_id])
            ELSE NULL
        END AS [Level2Name],
        CASE 
            WHEN [sp].[class] = 20 AND EXISTS((SELECT TOP 1 [sd2].[name]  FROM [sys].[data_spaces] [sd2] WITH (NOLOCK) WHERE [sd2].[data_space_id] = [sp].[major_id] AND [sd2].[type] = N'PS')) THEN
                CAST(1 AS BIT)
            ELSE CAST(0 AS BIT)
        END AS IsPartitionScheme,
        CAST(0 AS BIT) AS [IsDefault],
        [i].[type] AS [IndexType],
        CASE [sp].[class]
            WHEN 6 THEN [tt].[type_table_object_id]
            WHEN 8 THEN [tt].[type_table_object_id]
            WHEN 1 THEN [parent].[object_id]
            ELSE NULL
        END AS [ParentId],
         CASE [sp].[class]
            WHEN 6 THEN [tt].[schema_id]
            WHEN 8 THEN [tt].[schema_id]
            WHEN 1 THEN [parent].[schema_id]
            ELSE NULL
        END AS [ParentSchemaId],
        [sxi].[xml_index_type] AS [XMLIndexType],
        ISNULL([parent].[is_ms_shipped], 0) AS [IsMsShipped]
FROM    [sys].[extended_properties] [sp] WITH (NOLOCK) 
        LEFT JOIN [sys].[all_objects] [so] WITH (NOLOCK) ON [so].[object_id] = [sp].[major_id] AND [sp].[class] IN (1, 2, 7)
        LEFT JOIN [sys].[all_objects] [parent] WITH(NOLOCK) ON [so].[parent_object_id] = [parent].[object_id]
        LEFT JOIN [sys].[indexes] [i] WITH (NOLOCk) ON [sp].[class] = 7 AND [so].[object_id] = [i].[object_id]  AND [sp].[minor_id] = [i].[index_id]
        LEFT JOIN [sys].[table_types] [tt] WITH (NOLOCK) ON [sp].[class] IN (6,8) AND [tt].[user_type_id] = [sp].[major_id]
        LEFT JOIN [sys].[xml_indexes] [sxi] WITH (NOLOCk) ON [sp].[class] = 7 AND [so].[object_id] = [sxi].[object_id]  AND [sp].[minor_id] = [sxi].[index_id]
WHERE 
        [sp].[class] <> 1
        AND (OBJECTPROPERTY([so].[parent_object_id], N'TableIsFake') = 0 OR OBJECTPROPERTY([so].[parent_object_id], N'TableIsFake')  IS NULL)
        AND [sp].[class] <> 27
        AND ([so].[type] <> N'PG' OR [so].[type] IS NULL)
        AND [sp].[name] <> N'microsoft_database_tools_support'
        AND ([i].[is_primary_key] IS NULL OR [i].[is_primary_key] = 0)
        AND ([sp].[class] <> 7 OR [i].[object_id] IS NOT NULL)
        AND ([sp].[class] <> 7 OR ([so].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [so].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )))
) AS [_results] WHERE [TypeId] = 3 AND [Level2Name] IS NULL AND [Level1Name] IS NULL
 ORDER BY Level0Name,Level1Name ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110ExtendedPropertyPopulator' as [PopulatorName];
SELECT * FROM (
SELECT 
        [sp].[class] AS [TypeId],
        [sp].[major_id] AS [MajorId],
        [sp].[minor_id] AS [MinorId],
        [sp].[name] AS [PropertyName], 
        [sp].[value] AS [PropertyValue],
        [so].[type] AS [ObjectType],
        SQL_VARIANT_PROPERTY([sp].[value], N'BaseType') AS [BaseType],
        CASE [sp].[class]
            WHEN  0 THEN NULL
            WHEN  3 THEN SCHEMA_NAME([sp].[major_id])
            WHEN  4 THEN USER_NAME([sp].[major_id])
            WHEN  5 THEN (SELECT TOP 1 [sa2].[name]   FROM [sys].[assemblies] [sa2] WITH (NOLOCK) WHERE [sa2].[assembly_id] = [sp].[major_id])
            WHEN  6 THEN SCHEMA_NAME((SELECT TOP 1 [st].[schema_id] FROM [sys].[types] [st] WITH (NOLOCK) WHERE [st].[user_type_id] = [sp].[major_id]))
            WHEN  8 THEN SCHEMA_NAME([tt].[schema_id])
            WHEN 10 THEN SCHEMA_NAME((SELECT TOP 1 [sxsc2].[schema_id] FROM [sys].[xml_schema_collections] [sxsc2] WITH (NOLOCK) WHERE [sxsc2].[xml_collection_id] = [sp].[major_id]))
            WHEN 15 THEN (SELECT TOP 1 [smt2].[name]  FROM [sys].[service_message_types] [smt2] WITH (NOLOCK) WHERE [smt2].[message_type_id] = [sp].[major_id]) COLLATE database_default
            WHEN 16 THEN (SELECT TOP 1 [ssc2].[name]  FROM [sys].[service_contracts] [ssc2] WITH (NOLOCK) WHERE [ssc2].[service_contract_id] = [sp].[major_id])
            WHEN 17 THEN (SELECT TOP 1 [ss2].[name]   FROM [sys].[services] [ss2] WITH (NOLOCK) WHERE [ss2].[service_id] = [sp].[major_id])
            WHEN 18 THEN (SELECT TOP 1 [srs2].[name]  FROM [sys].[remote_service_bindings] [srs2] WITH (NOLOCK) WHERE [srs2].[remote_service_binding_id] = [sp].[major_id])
            WHEN 19 THEN (SELECT TOP 1 [sr2].[name]   FROM [sys].[routes] [sr2] WITH (NOLOCK) WHERE [sr2].[route_id] = [sp].[major_id])
            WHEN 20 THEN (SELECT TOP 1 [sd2].[name]  FROM [sys].[data_spaces] [sd2] WITH (NOLOCK) WHERE [sd2].[data_space_id] = [sp].[major_id])
            WHEN 21 THEN (SELECT TOP 1 [spf2].[name]  FROM [sys].[partition_functions] [spf2] WITH (NOLOCK) WHERE [spf2].[function_id] = [sp].[major_id])
            WHEN 22 THEN (SELECT TOP 1 [sdf2].[name]  FROM [sys].[database_files] [sdf2] WITH (NOLOCK) WHERE [sdf2].[file_id] = [sp].[major_id])
            ELSE SCHEMA_NAME([so].[schema_id])
        END AS [Level0Name],
        CASE [sp].[class] 
            WHEN 3 THEN NULL
            WHEN 4 THEN NULL
            WHEN 6  THEN (SELECT TOP 1 [st2].[name] FROM [sys].[types] [st2] WITH (NOLOCK) WHERE [st2].[user_type_id] = [sp].[major_id])
            WHEN 8  THEN [tt].[name]
            WHEN 10 THEN (SELECT TOP 1 [sx2].[name] FROM [sys].[xml_schema_collections] [sx2] WITH (NOLOCK) WHERE [sx2].[xml_collection_id] = [sp].[major_id])
            WHEN 22 THEN NULL
            ELSE [so].[name]
        END AS [Level1Name],
        CASE [sp].[class]			
            WHEN 2 THEN (SELECT TOP 1 [spa].[name] FROM [sys].[parameters] [spa] WITH (NOLOCK) WHERE [spa].[object_id] = [sp].[major_id] AND [spa].[parameter_id] = [sp].[minor_id])
            WHEN 7 THEN (SELECT TOP 1 [si].[name] FROM [sys].[indexes] [si] WITH (NOLOCK) WHERE [si].[object_id] = [sp].[major_id] AND [si].[index_id] = [sp].[minor_id])
            WHEN 8 THEN COL_NAME([tt].[type_table_object_id] ,[sp].[minor_id])
            ELSE NULL
        END AS [Level2Name],
        CASE 
            WHEN [sp].[class] = 20 AND EXISTS((SELECT TOP 1 [sd2].[name]  FROM [sys].[data_spaces] [sd2] WITH (NOLOCK) WHERE [sd2].[data_space_id] = [sp].[major_id] AND [sd2].[type] = N'PS')) THEN
                CAST(1 AS BIT)
            ELSE CAST(0 AS BIT)
        END AS IsPartitionScheme,
        CAST(0 AS BIT) AS [IsDefault],
        [i].[type] AS [IndexType],
        CASE [sp].[class]
            WHEN 6 THEN [tt].[type_table_object_id]
            WHEN 8 THEN [tt].[type_table_object_id]
            WHEN 1 THEN [parent].[object_id]
            ELSE NULL
        END AS [ParentId],
         CASE [sp].[class]
            WHEN 6 THEN [tt].[schema_id]
            WHEN 8 THEN [tt].[schema_id]
            WHEN 1 THEN [parent].[schema_id]
            ELSE NULL
        END AS [ParentSchemaId],
        [sxi].[xml_index_type] AS [XMLIndexType],
        ISNULL([parent].[is_ms_shipped], 0) AS [IsMsShipped]
FROM    [sys].[extended_properties] [sp] WITH (NOLOCK) 
        LEFT JOIN [sys].[all_objects] [so] WITH (NOLOCK) ON [so].[object_id] = [sp].[major_id] AND [sp].[class] IN (1, 2, 7)
        LEFT JOIN [sys].[all_objects] [parent] WITH(NOLOCK) ON [so].[parent_object_id] = [parent].[object_id]
        LEFT JOIN [sys].[indexes] [i] WITH (NOLOCk) ON [sp].[class] = 7 AND [so].[object_id] = [i].[object_id]  AND [sp].[minor_id] = [i].[index_id]
        LEFT JOIN [sys].[table_types] [tt] WITH (NOLOCK) ON [sp].[class] IN (6,8) AND [tt].[user_type_id] = [sp].[major_id]
        LEFT JOIN [sys].[xml_indexes] [sxi] WITH (NOLOCk) ON [sp].[class] = 7 AND [so].[object_id] = [sxi].[object_id]  AND [sp].[minor_id] = [sxi].[index_id]
WHERE 
        [sp].[class] <> 1
        AND (OBJECTPROPERTY([so].[parent_object_id], N'TableIsFake') = 0 OR OBJECTPROPERTY([so].[parent_object_id], N'TableIsFake')  IS NULL)
        AND [sp].[class] <> 27
        AND ([so].[type] <> N'PG' OR [so].[type] IS NULL)
        AND [sp].[name] <> N'microsoft_database_tools_support'
        AND ([i].[is_primary_key] IS NULL OR [i].[is_primary_key] = 0)
        AND ([sp].[class] <> 7 OR [i].[object_id] IS NOT NULL)
        AND ([sp].[class] <> 7 OR ([so].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [so].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )))
) AS [_results] WHERE [TypeId] = 4 AND [Level2Name] IS NULL AND [Level1Name] IS NULL
 ORDER BY Level0Name,Level1Name ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110ExtendedPropertyPopulator' as [PopulatorName];
SELECT * FROM (
SELECT 
        [sp].[class] AS [TypeId],
        [sp].[major_id] AS [MajorId],
        [sp].[minor_id] AS [MinorId],
        [sp].[name] AS [PropertyName], 
        [sp].[value] AS [PropertyValue],
        [so].[type] AS [ObjectType],
        SQL_VARIANT_PROPERTY([sp].[value], N'BaseType') AS [BaseType],
        CASE [sp].[class]
            WHEN  0 THEN NULL
            WHEN  3 THEN SCHEMA_NAME([sp].[major_id])
            WHEN  4 THEN USER_NAME([sp].[major_id])
            WHEN  5 THEN (SELECT TOP 1 [sa2].[name]   FROM [sys].[assemblies] [sa2] WITH (NOLOCK) WHERE [sa2].[assembly_id] = [sp].[major_id])
            WHEN  6 THEN SCHEMA_NAME((SELECT TOP 1 [st].[schema_id] FROM [sys].[types] [st] WITH (NOLOCK) WHERE [st].[user_type_id] = [sp].[major_id]))
            WHEN  8 THEN SCHEMA_NAME([tt].[schema_id])
            WHEN 10 THEN SCHEMA_NAME((SELECT TOP 1 [sxsc2].[schema_id] FROM [sys].[xml_schema_collections] [sxsc2] WITH (NOLOCK) WHERE [sxsc2].[xml_collection_id] = [sp].[major_id]))
            WHEN 15 THEN (SELECT TOP 1 [smt2].[name]  FROM [sys].[service_message_types] [smt2] WITH (NOLOCK) WHERE [smt2].[message_type_id] = [sp].[major_id]) COLLATE database_default
            WHEN 16 THEN (SELECT TOP 1 [ssc2].[name]  FROM [sys].[service_contracts] [ssc2] WITH (NOLOCK) WHERE [ssc2].[service_contract_id] = [sp].[major_id])
            WHEN 17 THEN (SELECT TOP 1 [ss2].[name]   FROM [sys].[services] [ss2] WITH (NOLOCK) WHERE [ss2].[service_id] = [sp].[major_id])
            WHEN 18 THEN (SELECT TOP 1 [srs2].[name]  FROM [sys].[remote_service_bindings] [srs2] WITH (NOLOCK) WHERE [srs2].[remote_service_binding_id] = [sp].[major_id])
            WHEN 19 THEN (SELECT TOP 1 [sr2].[name]   FROM [sys].[routes] [sr2] WITH (NOLOCK) WHERE [sr2].[route_id] = [sp].[major_id])
            WHEN 20 THEN (SELECT TOP 1 [sd2].[name]  FROM [sys].[data_spaces] [sd2] WITH (NOLOCK) WHERE [sd2].[data_space_id] = [sp].[major_id])
            WHEN 21 THEN (SELECT TOP 1 [spf2].[name]  FROM [sys].[partition_functions] [spf2] WITH (NOLOCK) WHERE [spf2].[function_id] = [sp].[major_id])
            WHEN 22 THEN (SELECT TOP 1 [sdf2].[name]  FROM [sys].[database_files] [sdf2] WITH (NOLOCK) WHERE [sdf2].[file_id] = [sp].[major_id])
            ELSE SCHEMA_NAME([so].[schema_id])
        END AS [Level0Name],
        CASE [sp].[class] 
            WHEN 3 THEN NULL
            WHEN 4 THEN NULL
            WHEN 6  THEN (SELECT TOP 1 [st2].[name] FROM [sys].[types] [st2] WITH (NOLOCK) WHERE [st2].[user_type_id] = [sp].[major_id])
            WHEN 8  THEN [tt].[name]
            WHEN 10 THEN (SELECT TOP 1 [sx2].[name] FROM [sys].[xml_schema_collections] [sx2] WITH (NOLOCK) WHERE [sx2].[xml_collection_id] = [sp].[major_id])
            WHEN 22 THEN NULL
            ELSE [so].[name]
        END AS [Level1Name],
        CASE [sp].[class]			
            WHEN 2 THEN (SELECT TOP 1 [spa].[name] FROM [sys].[parameters] [spa] WITH (NOLOCK) WHERE [spa].[object_id] = [sp].[major_id] AND [spa].[parameter_id] = [sp].[minor_id])
            WHEN 7 THEN (SELECT TOP 1 [si].[name] FROM [sys].[indexes] [si] WITH (NOLOCK) WHERE [si].[object_id] = [sp].[major_id] AND [si].[index_id] = [sp].[minor_id])
            WHEN 8 THEN COL_NAME([tt].[type_table_object_id] ,[sp].[minor_id])
            ELSE NULL
        END AS [Level2Name],
        CASE 
            WHEN [sp].[class] = 20 AND EXISTS((SELECT TOP 1 [sd2].[name]  FROM [sys].[data_spaces] [sd2] WITH (NOLOCK) WHERE [sd2].[data_space_id] = [sp].[major_id] AND [sd2].[type] = N'PS')) THEN
                CAST(1 AS BIT)
            ELSE CAST(0 AS BIT)
        END AS IsPartitionScheme,
        CAST(0 AS BIT) AS [IsDefault],
        [i].[type] AS [IndexType],
        CASE [sp].[class]
            WHEN 6 THEN [tt].[type_table_object_id]
            WHEN 8 THEN [tt].[type_table_object_id]
            WHEN 1 THEN [parent].[object_id]
            ELSE NULL
        END AS [ParentId],
         CASE [sp].[class]
            WHEN 6 THEN [tt].[schema_id]
            WHEN 8 THEN [tt].[schema_id]
            WHEN 1 THEN [parent].[schema_id]
            ELSE NULL
        END AS [ParentSchemaId],
        [sxi].[xml_index_type] AS [XMLIndexType],
        ISNULL([parent].[is_ms_shipped], 0) AS [IsMsShipped]
FROM    [sys].[extended_properties] [sp] WITH (NOLOCK) 
        LEFT JOIN [sys].[all_objects] [so] WITH (NOLOCK) ON [so].[object_id] = [sp].[major_id] AND [sp].[class] IN (1, 2, 7)
        LEFT JOIN [sys].[all_objects] [parent] WITH(NOLOCK) ON [so].[parent_object_id] = [parent].[object_id]
        LEFT JOIN [sys].[indexes] [i] WITH (NOLOCk) ON [sp].[class] = 7 AND [so].[object_id] = [i].[object_id]  AND [sp].[minor_id] = [i].[index_id]
        LEFT JOIN [sys].[table_types] [tt] WITH (NOLOCK) ON [sp].[class] IN (6,8) AND [tt].[user_type_id] = [sp].[major_id]
        LEFT JOIN [sys].[xml_indexes] [sxi] WITH (NOLOCk) ON [sp].[class] = 7 AND [so].[object_id] = [sxi].[object_id]  AND [sp].[minor_id] = [sxi].[index_id]
WHERE 
        [sp].[class] <> 1
        AND (OBJECTPROPERTY([so].[parent_object_id], N'TableIsFake') = 0 OR OBJECTPROPERTY([so].[parent_object_id], N'TableIsFake')  IS NULL)
        AND [sp].[class] <> 27
        AND ([so].[type] <> N'PG' OR [so].[type] IS NULL)
        AND [sp].[name] <> N'microsoft_database_tools_support'
        AND ([i].[is_primary_key] IS NULL OR [i].[is_primary_key] = 0)
        AND ([sp].[class] <> 7 OR [i].[object_id] IS NOT NULL)
        AND ([sp].[class] <> 7 OR ([so].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [so].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )))
) AS [_results] WHERE [TypeId] = 5 AND [Level2Name] IS NULL AND [Level1Name] IS NULL
 ORDER BY Level0Name,Level1Name ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110ExtendedPropertyPopulator' as [PopulatorName];
SELECT * FROM (
SELECT 
        [sp].[class] AS [TypeId],
        [sp].[major_id] AS [MajorId],
        [sp].[minor_id] AS [MinorId],
        [sp].[name] AS [PropertyName], 
        [sp].[value] AS [PropertyValue],
        [so].[type] AS [ObjectType],
        SQL_VARIANT_PROPERTY([sp].[value], N'BaseType') AS [BaseType],
        CASE [sp].[class]
            WHEN  0 THEN NULL
            WHEN  3 THEN SCHEMA_NAME([sp].[major_id])
            WHEN  4 THEN USER_NAME([sp].[major_id])
            WHEN  5 THEN (SELECT TOP 1 [sa2].[name]   FROM [sys].[assemblies] [sa2] WITH (NOLOCK) WHERE [sa2].[assembly_id] = [sp].[major_id])
            WHEN  6 THEN SCHEMA_NAME((SELECT TOP 1 [st].[schema_id] FROM [sys].[types] [st] WITH (NOLOCK) WHERE [st].[user_type_id] = [sp].[major_id]))
            WHEN  8 THEN SCHEMA_NAME([tt].[schema_id])
            WHEN 10 THEN SCHEMA_NAME((SELECT TOP 1 [sxsc2].[schema_id] FROM [sys].[xml_schema_collections] [sxsc2] WITH (NOLOCK) WHERE [sxsc2].[xml_collection_id] = [sp].[major_id]))
            WHEN 15 THEN (SELECT TOP 1 [smt2].[name]  FROM [sys].[service_message_types] [smt2] WITH (NOLOCK) WHERE [smt2].[message_type_id] = [sp].[major_id]) COLLATE database_default
            WHEN 16 THEN (SELECT TOP 1 [ssc2].[name]  FROM [sys].[service_contracts] [ssc2] WITH (NOLOCK) WHERE [ssc2].[service_contract_id] = [sp].[major_id])
            WHEN 17 THEN (SELECT TOP 1 [ss2].[name]   FROM [sys].[services] [ss2] WITH (NOLOCK) WHERE [ss2].[service_id] = [sp].[major_id])
            WHEN 18 THEN (SELECT TOP 1 [srs2].[name]  FROM [sys].[remote_service_bindings] [srs2] WITH (NOLOCK) WHERE [srs2].[remote_service_binding_id] = [sp].[major_id])
            WHEN 19 THEN (SELECT TOP 1 [sr2].[name]   FROM [sys].[routes] [sr2] WITH (NOLOCK) WHERE [sr2].[route_id] = [sp].[major_id])
            WHEN 20 THEN (SELECT TOP 1 [sd2].[name]  FROM [sys].[data_spaces] [sd2] WITH (NOLOCK) WHERE [sd2].[data_space_id] = [sp].[major_id])
            WHEN 21 THEN (SELECT TOP 1 [spf2].[name]  FROM [sys].[partition_functions] [spf2] WITH (NOLOCK) WHERE [spf2].[function_id] = [sp].[major_id])
            WHEN 22 THEN (SELECT TOP 1 [sdf2].[name]  FROM [sys].[database_files] [sdf2] WITH (NOLOCK) WHERE [sdf2].[file_id] = [sp].[major_id])
            ELSE SCHEMA_NAME([so].[schema_id])
        END AS [Level0Name],
        CASE [sp].[class] 
            WHEN 3 THEN NULL
            WHEN 4 THEN NULL
            WHEN 6  THEN (SELECT TOP 1 [st2].[name] FROM [sys].[types] [st2] WITH (NOLOCK) WHERE [st2].[user_type_id] = [sp].[major_id])
            WHEN 8  THEN [tt].[name]
            WHEN 10 THEN (SELECT TOP 1 [sx2].[name] FROM [sys].[xml_schema_collections] [sx2] WITH (NOLOCK) WHERE [sx2].[xml_collection_id] = [sp].[major_id])
            WHEN 22 THEN NULL
            ELSE [so].[name]
        END AS [Level1Name],
        CASE [sp].[class]			
            WHEN 2 THEN (SELECT TOP 1 [spa].[name] FROM [sys].[parameters] [spa] WITH (NOLOCK) WHERE [spa].[object_id] = [sp].[major_id] AND [spa].[parameter_id] = [sp].[minor_id])
            WHEN 7 THEN (SELECT TOP 1 [si].[name] FROM [sys].[indexes] [si] WITH (NOLOCK) WHERE [si].[object_id] = [sp].[major_id] AND [si].[index_id] = [sp].[minor_id])
            WHEN 8 THEN COL_NAME([tt].[type_table_object_id] ,[sp].[minor_id])
            ELSE NULL
        END AS [Level2Name],
        CASE 
            WHEN [sp].[class] = 20 AND EXISTS((SELECT TOP 1 [sd2].[name]  FROM [sys].[data_spaces] [sd2] WITH (NOLOCK) WHERE [sd2].[data_space_id] = [sp].[major_id] AND [sd2].[type] = N'PS')) THEN
                CAST(1 AS BIT)
            ELSE CAST(0 AS BIT)
        END AS IsPartitionScheme,
        CAST(0 AS BIT) AS [IsDefault],
        [i].[type] AS [IndexType],
        CASE [sp].[class]
            WHEN 6 THEN [tt].[type_table_object_id]
            WHEN 8 THEN [tt].[type_table_object_id]
            WHEN 1 THEN [parent].[object_id]
            ELSE NULL
        END AS [ParentId],
         CASE [sp].[class]
            WHEN 6 THEN [tt].[schema_id]
            WHEN 8 THEN [tt].[schema_id]
            WHEN 1 THEN [parent].[schema_id]
            ELSE NULL
        END AS [ParentSchemaId],
        [sxi].[xml_index_type] AS [XMLIndexType],
        ISNULL([parent].[is_ms_shipped], 0) AS [IsMsShipped]
FROM    [sys].[extended_properties] [sp] WITH (NOLOCK) 
        LEFT JOIN [sys].[all_objects] [so] WITH (NOLOCK) ON [so].[object_id] = [sp].[major_id] AND [sp].[class] IN (1, 2, 7)
        LEFT JOIN [sys].[all_objects] [parent] WITH(NOLOCK) ON [so].[parent_object_id] = [parent].[object_id]
        LEFT JOIN [sys].[indexes] [i] WITH (NOLOCk) ON [sp].[class] = 7 AND [so].[object_id] = [i].[object_id]  AND [sp].[minor_id] = [i].[index_id]
        LEFT JOIN [sys].[table_types] [tt] WITH (NOLOCK) ON [sp].[class] IN (6,8) AND [tt].[user_type_id] = [sp].[major_id]
        LEFT JOIN [sys].[xml_indexes] [sxi] WITH (NOLOCk) ON [sp].[class] = 7 AND [so].[object_id] = [sxi].[object_id]  AND [sp].[minor_id] = [sxi].[index_id]
WHERE 
        [sp].[class] <> 1
        AND (OBJECTPROPERTY([so].[parent_object_id], N'TableIsFake') = 0 OR OBJECTPROPERTY([so].[parent_object_id], N'TableIsFake')  IS NULL)
        AND [sp].[class] <> 27
        AND ([so].[type] <> N'PG' OR [so].[type] IS NULL)
        AND [sp].[name] <> N'microsoft_database_tools_support'
        AND ([i].[is_primary_key] IS NULL OR [i].[is_primary_key] = 0)
        AND ([sp].[class] <> 7 OR [i].[object_id] IS NOT NULL)
        AND ([sp].[class] <> 7 OR ([so].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [so].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )))
) AS [_results] WHERE [TypeId] = 6 AND [Level2Name] IS NULL
 ORDER BY Level0Name,Level1Name ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110ExtendedPropertyPopulator' as [PopulatorName];
SELECT * FROM (
SELECT 
        [sp].[class] AS [TypeId],
        [sp].[major_id] AS [MajorId],
        [sp].[minor_id] AS [MinorId],
        [sp].[name] AS [PropertyName], 
        [sp].[value] AS [PropertyValue],
        [so].[type] AS [ObjectType],
        SQL_VARIANT_PROPERTY([sp].[value], N'BaseType') AS [BaseType],
        CASE [sp].[class]
            WHEN  0 THEN NULL
            WHEN  3 THEN SCHEMA_NAME([sp].[major_id])
            WHEN  4 THEN USER_NAME([sp].[major_id])
            WHEN  5 THEN (SELECT TOP 1 [sa2].[name]   FROM [sys].[assemblies] [sa2] WITH (NOLOCK) WHERE [sa2].[assembly_id] = [sp].[major_id])
            WHEN  6 THEN SCHEMA_NAME((SELECT TOP 1 [st].[schema_id] FROM [sys].[types] [st] WITH (NOLOCK) WHERE [st].[user_type_id] = [sp].[major_id]))
            WHEN  8 THEN SCHEMA_NAME([tt].[schema_id])
            WHEN 10 THEN SCHEMA_NAME((SELECT TOP 1 [sxsc2].[schema_id] FROM [sys].[xml_schema_collections] [sxsc2] WITH (NOLOCK) WHERE [sxsc2].[xml_collection_id] = [sp].[major_id]))
            WHEN 15 THEN (SELECT TOP 1 [smt2].[name]  FROM [sys].[service_message_types] [smt2] WITH (NOLOCK) WHERE [smt2].[message_type_id] = [sp].[major_id]) COLLATE database_default
            WHEN 16 THEN (SELECT TOP 1 [ssc2].[name]  FROM [sys].[service_contracts] [ssc2] WITH (NOLOCK) WHERE [ssc2].[service_contract_id] = [sp].[major_id])
            WHEN 17 THEN (SELECT TOP 1 [ss2].[name]   FROM [sys].[services] [ss2] WITH (NOLOCK) WHERE [ss2].[service_id] = [sp].[major_id])
            WHEN 18 THEN (SELECT TOP 1 [srs2].[name]  FROM [sys].[remote_service_bindings] [srs2] WITH (NOLOCK) WHERE [srs2].[remote_service_binding_id] = [sp].[major_id])
            WHEN 19 THEN (SELECT TOP 1 [sr2].[name]   FROM [sys].[routes] [sr2] WITH (NOLOCK) WHERE [sr2].[route_id] = [sp].[major_id])
            WHEN 20 THEN (SELECT TOP 1 [sd2].[name]  FROM [sys].[data_spaces] [sd2] WITH (NOLOCK) WHERE [sd2].[data_space_id] = [sp].[major_id])
            WHEN 21 THEN (SELECT TOP 1 [spf2].[name]  FROM [sys].[partition_functions] [spf2] WITH (NOLOCK) WHERE [spf2].[function_id] = [sp].[major_id])
            WHEN 22 THEN (SELECT TOP 1 [sdf2].[name]  FROM [sys].[database_files] [sdf2] WITH (NOLOCK) WHERE [sdf2].[file_id] = [sp].[major_id])
            ELSE SCHEMA_NAME([so].[schema_id])
        END AS [Level0Name],
        CASE [sp].[class] 
            WHEN 3 THEN NULL
            WHEN 4 THEN NULL
            WHEN 6  THEN (SELECT TOP 1 [st2].[name] FROM [sys].[types] [st2] WITH (NOLOCK) WHERE [st2].[user_type_id] = [sp].[major_id])
            WHEN 8  THEN [tt].[name]
            WHEN 10 THEN (SELECT TOP 1 [sx2].[name] FROM [sys].[xml_schema_collections] [sx2] WITH (NOLOCK) WHERE [sx2].[xml_collection_id] = [sp].[major_id])
            WHEN 22 THEN NULL
            ELSE [so].[name]
        END AS [Level1Name],
        CASE [sp].[class]			
            WHEN 2 THEN (SELECT TOP 1 [spa].[name] FROM [sys].[parameters] [spa] WITH (NOLOCK) WHERE [spa].[object_id] = [sp].[major_id] AND [spa].[parameter_id] = [sp].[minor_id])
            WHEN 7 THEN (SELECT TOP 1 [si].[name] FROM [sys].[indexes] [si] WITH (NOLOCK) WHERE [si].[object_id] = [sp].[major_id] AND [si].[index_id] = [sp].[minor_id])
            WHEN 8 THEN COL_NAME([tt].[type_table_object_id] ,[sp].[minor_id])
            ELSE NULL
        END AS [Level2Name],
        CASE 
            WHEN [sp].[class] = 20 AND EXISTS((SELECT TOP 1 [sd2].[name]  FROM [sys].[data_spaces] [sd2] WITH (NOLOCK) WHERE [sd2].[data_space_id] = [sp].[major_id] AND [sd2].[type] = N'PS')) THEN
                CAST(1 AS BIT)
            ELSE CAST(0 AS BIT)
        END AS IsPartitionScheme,
        CAST(0 AS BIT) AS [IsDefault],
        [i].[type] AS [IndexType],
        CASE [sp].[class]
            WHEN 6 THEN [tt].[type_table_object_id]
            WHEN 8 THEN [tt].[type_table_object_id]
            WHEN 1 THEN [parent].[object_id]
            ELSE NULL
        END AS [ParentId],
         CASE [sp].[class]
            WHEN 6 THEN [tt].[schema_id]
            WHEN 8 THEN [tt].[schema_id]
            WHEN 1 THEN [parent].[schema_id]
            ELSE NULL
        END AS [ParentSchemaId],
        [sxi].[xml_index_type] AS [XMLIndexType],
        ISNULL([parent].[is_ms_shipped], 0) AS [IsMsShipped]
FROM    [sys].[extended_properties] [sp] WITH (NOLOCK) 
        LEFT JOIN [sys].[all_objects] [so] WITH (NOLOCK) ON [so].[object_id] = [sp].[major_id] AND [sp].[class] IN (1, 2, 7)
        LEFT JOIN [sys].[all_objects] [parent] WITH(NOLOCK) ON [so].[parent_object_id] = [parent].[object_id]
        LEFT JOIN [sys].[indexes] [i] WITH (NOLOCk) ON [sp].[class] = 7 AND [so].[object_id] = [i].[object_id]  AND [sp].[minor_id] = [i].[index_id]
        LEFT JOIN [sys].[table_types] [tt] WITH (NOLOCK) ON [sp].[class] IN (6,8) AND [tt].[user_type_id] = [sp].[major_id]
        LEFT JOIN [sys].[xml_indexes] [sxi] WITH (NOLOCk) ON [sp].[class] = 7 AND [so].[object_id] = [sxi].[object_id]  AND [sp].[minor_id] = [sxi].[index_id]
WHERE 
        [sp].[class] <> 1
        AND (OBJECTPROPERTY([so].[parent_object_id], N'TableIsFake') = 0 OR OBJECTPROPERTY([so].[parent_object_id], N'TableIsFake')  IS NULL)
        AND [sp].[class] <> 27
        AND ([so].[type] <> N'PG' OR [so].[type] IS NULL)
        AND [sp].[name] <> N'microsoft_database_tools_support'
        AND ([i].[is_primary_key] IS NULL OR [i].[is_primary_key] = 0)
        AND ([sp].[class] <> 7 OR [i].[object_id] IS NOT NULL)
        AND ([sp].[class] <> 7 OR ([so].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [so].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )))
) AS [_results] WHERE [TypeId] = 7
 ORDER BY Level0Name,Level1Name ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110ExtendedPropertyPopulator' as [PopulatorName];
SELECT * FROM (
SELECT 
        [sp].[class] AS [TypeId],
        [sp].[major_id] AS [MajorId],
        [sp].[minor_id] AS [MinorId],
        [sp].[name] AS [PropertyName], 
        [sp].[value] AS [PropertyValue],
        [so].[type] AS [ObjectType],
        SQL_VARIANT_PROPERTY([sp].[value], N'BaseType') AS [BaseType],
        CASE [sp].[class]
            WHEN  0 THEN NULL
            WHEN  3 THEN SCHEMA_NAME([sp].[major_id])
            WHEN  4 THEN USER_NAME([sp].[major_id])
            WHEN  5 THEN (SELECT TOP 1 [sa2].[name]   FROM [sys].[assemblies] [sa2] WITH (NOLOCK) WHERE [sa2].[assembly_id] = [sp].[major_id])
            WHEN  6 THEN SCHEMA_NAME((SELECT TOP 1 [st].[schema_id] FROM [sys].[types] [st] WITH (NOLOCK) WHERE [st].[user_type_id] = [sp].[major_id]))
            WHEN  8 THEN SCHEMA_NAME([tt].[schema_id])
            WHEN 10 THEN SCHEMA_NAME((SELECT TOP 1 [sxsc2].[schema_id] FROM [sys].[xml_schema_collections] [sxsc2] WITH (NOLOCK) WHERE [sxsc2].[xml_collection_id] = [sp].[major_id]))
            WHEN 15 THEN (SELECT TOP 1 [smt2].[name]  FROM [sys].[service_message_types] [smt2] WITH (NOLOCK) WHERE [smt2].[message_type_id] = [sp].[major_id]) COLLATE database_default
            WHEN 16 THEN (SELECT TOP 1 [ssc2].[name]  FROM [sys].[service_contracts] [ssc2] WITH (NOLOCK) WHERE [ssc2].[service_contract_id] = [sp].[major_id])
            WHEN 17 THEN (SELECT TOP 1 [ss2].[name]   FROM [sys].[services] [ss2] WITH (NOLOCK) WHERE [ss2].[service_id] = [sp].[major_id])
            WHEN 18 THEN (SELECT TOP 1 [srs2].[name]  FROM [sys].[remote_service_bindings] [srs2] WITH (NOLOCK) WHERE [srs2].[remote_service_binding_id] = [sp].[major_id])
            WHEN 19 THEN (SELECT TOP 1 [sr2].[name]   FROM [sys].[routes] [sr2] WITH (NOLOCK) WHERE [sr2].[route_id] = [sp].[major_id])
            WHEN 20 THEN (SELECT TOP 1 [sd2].[name]  FROM [sys].[data_spaces] [sd2] WITH (NOLOCK) WHERE [sd2].[data_space_id] = [sp].[major_id])
            WHEN 21 THEN (SELECT TOP 1 [spf2].[name]  FROM [sys].[partition_functions] [spf2] WITH (NOLOCK) WHERE [spf2].[function_id] = [sp].[major_id])
            WHEN 22 THEN (SELECT TOP 1 [sdf2].[name]  FROM [sys].[database_files] [sdf2] WITH (NOLOCK) WHERE [sdf2].[file_id] = [sp].[major_id])
            ELSE SCHEMA_NAME([so].[schema_id])
        END AS [Level0Name],
        CASE [sp].[class] 
            WHEN 3 THEN NULL
            WHEN 4 THEN NULL
            WHEN 6  THEN (SELECT TOP 1 [st2].[name] FROM [sys].[types] [st2] WITH (NOLOCK) WHERE [st2].[user_type_id] = [sp].[major_id])
            WHEN 8  THEN [tt].[name]
            WHEN 10 THEN (SELECT TOP 1 [sx2].[name] FROM [sys].[xml_schema_collections] [sx2] WITH (NOLOCK) WHERE [sx2].[xml_collection_id] = [sp].[major_id])
            WHEN 22 THEN NULL
            ELSE [so].[name]
        END AS [Level1Name],
        CASE [sp].[class]			
            WHEN 2 THEN (SELECT TOP 1 [spa].[name] FROM [sys].[parameters] [spa] WITH (NOLOCK) WHERE [spa].[object_id] = [sp].[major_id] AND [spa].[parameter_id] = [sp].[minor_id])
            WHEN 7 THEN (SELECT TOP 1 [si].[name] FROM [sys].[indexes] [si] WITH (NOLOCK) WHERE [si].[object_id] = [sp].[major_id] AND [si].[index_id] = [sp].[minor_id])
            WHEN 8 THEN COL_NAME([tt].[type_table_object_id] ,[sp].[minor_id])
            ELSE NULL
        END AS [Level2Name],
        CASE 
            WHEN [sp].[class] = 20 AND EXISTS((SELECT TOP 1 [sd2].[name]  FROM [sys].[data_spaces] [sd2] WITH (NOLOCK) WHERE [sd2].[data_space_id] = [sp].[major_id] AND [sd2].[type] = N'PS')) THEN
                CAST(1 AS BIT)
            ELSE CAST(0 AS BIT)
        END AS IsPartitionScheme,
        CAST(0 AS BIT) AS [IsDefault],
        [i].[type] AS [IndexType],
        CASE [sp].[class]
            WHEN 6 THEN [tt].[type_table_object_id]
            WHEN 8 THEN [tt].[type_table_object_id]
            WHEN 1 THEN [parent].[object_id]
            ELSE NULL
        END AS [ParentId],
         CASE [sp].[class]
            WHEN 6 THEN [tt].[schema_id]
            WHEN 8 THEN [tt].[schema_id]
            WHEN 1 THEN [parent].[schema_id]
            ELSE NULL
        END AS [ParentSchemaId],
        [sxi].[xml_index_type] AS [XMLIndexType],
        ISNULL([parent].[is_ms_shipped], 0) AS [IsMsShipped]
FROM    [sys].[extended_properties] [sp] WITH (NOLOCK) 
        LEFT JOIN [sys].[all_objects] [so] WITH (NOLOCK) ON [so].[object_id] = [sp].[major_id] AND [sp].[class] IN (1, 2, 7)
        LEFT JOIN [sys].[all_objects] [parent] WITH(NOLOCK) ON [so].[parent_object_id] = [parent].[object_id]
        LEFT JOIN [sys].[indexes] [i] WITH (NOLOCk) ON [sp].[class] = 7 AND [so].[object_id] = [i].[object_id]  AND [sp].[minor_id] = [i].[index_id]
        LEFT JOIN [sys].[table_types] [tt] WITH (NOLOCK) ON [sp].[class] IN (6,8) AND [tt].[user_type_id] = [sp].[major_id]
        LEFT JOIN [sys].[xml_indexes] [sxi] WITH (NOLOCk) ON [sp].[class] = 7 AND [so].[object_id] = [sxi].[object_id]  AND [sp].[minor_id] = [sxi].[index_id]
WHERE 
        [sp].[class] <> 1
        AND (OBJECTPROPERTY([so].[parent_object_id], N'TableIsFake') = 0 OR OBJECTPROPERTY([so].[parent_object_id], N'TableIsFake')  IS NULL)
        AND [sp].[class] <> 27
        AND ([so].[type] <> N'PG' OR [so].[type] IS NULL)
        AND [sp].[name] <> N'microsoft_database_tools_support'
        AND ([i].[is_primary_key] IS NULL OR [i].[is_primary_key] = 0)
        AND ([sp].[class] <> 7 OR [i].[object_id] IS NOT NULL)
        AND ([sp].[class] <> 7 OR ([so].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [so].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )))
) AS [_results] WHERE [TypeId] = 8
 ORDER BY Level0Name,Level1Name ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110ExtendedPropertyPopulator' as [PopulatorName];
SELECT * FROM (
SELECT 
        [sp].[class] AS [TypeId],
        [sp].[major_id] AS [MajorId],
        [sp].[minor_id] AS [MinorId],
        [sp].[name] AS [PropertyName], 
        [sp].[value] AS [PropertyValue],
        [so].[type] AS [ObjectType],
        SQL_VARIANT_PROPERTY([sp].[value], N'BaseType') AS [BaseType],
        CASE [sp].[class]
            WHEN  0 THEN NULL
            WHEN  3 THEN SCHEMA_NAME([sp].[major_id])
            WHEN  4 THEN USER_NAME([sp].[major_id])
            WHEN  5 THEN (SELECT TOP 1 [sa2].[name]   FROM [sys].[assemblies] [sa2] WITH (NOLOCK) WHERE [sa2].[assembly_id] = [sp].[major_id])
            WHEN  6 THEN SCHEMA_NAME((SELECT TOP 1 [st].[schema_id] FROM [sys].[types] [st] WITH (NOLOCK) WHERE [st].[user_type_id] = [sp].[major_id]))
            WHEN  8 THEN SCHEMA_NAME([tt].[schema_id])
            WHEN 10 THEN SCHEMA_NAME((SELECT TOP 1 [sxsc2].[schema_id] FROM [sys].[xml_schema_collections] [sxsc2] WITH (NOLOCK) WHERE [sxsc2].[xml_collection_id] = [sp].[major_id]))
            WHEN 15 THEN (SELECT TOP 1 [smt2].[name]  FROM [sys].[service_message_types] [smt2] WITH (NOLOCK) WHERE [smt2].[message_type_id] = [sp].[major_id]) COLLATE database_default
            WHEN 16 THEN (SELECT TOP 1 [ssc2].[name]  FROM [sys].[service_contracts] [ssc2] WITH (NOLOCK) WHERE [ssc2].[service_contract_id] = [sp].[major_id])
            WHEN 17 THEN (SELECT TOP 1 [ss2].[name]   FROM [sys].[services] [ss2] WITH (NOLOCK) WHERE [ss2].[service_id] = [sp].[major_id])
            WHEN 18 THEN (SELECT TOP 1 [srs2].[name]  FROM [sys].[remote_service_bindings] [srs2] WITH (NOLOCK) WHERE [srs2].[remote_service_binding_id] = [sp].[major_id])
            WHEN 19 THEN (SELECT TOP 1 [sr2].[name]   FROM [sys].[routes] [sr2] WITH (NOLOCK) WHERE [sr2].[route_id] = [sp].[major_id])
            WHEN 20 THEN (SELECT TOP 1 [sd2].[name]  FROM [sys].[data_spaces] [sd2] WITH (NOLOCK) WHERE [sd2].[data_space_id] = [sp].[major_id])
            WHEN 21 THEN (SELECT TOP 1 [spf2].[name]  FROM [sys].[partition_functions] [spf2] WITH (NOLOCK) WHERE [spf2].[function_id] = [sp].[major_id])
            WHEN 22 THEN (SELECT TOP 1 [sdf2].[name]  FROM [sys].[database_files] [sdf2] WITH (NOLOCK) WHERE [sdf2].[file_id] = [sp].[major_id])
            ELSE SCHEMA_NAME([so].[schema_id])
        END AS [Level0Name],
        CASE [sp].[class] 
            WHEN 3 THEN NULL
            WHEN 4 THEN NULL
            WHEN 6  THEN (SELECT TOP 1 [st2].[name] FROM [sys].[types] [st2] WITH (NOLOCK) WHERE [st2].[user_type_id] = [sp].[major_id])
            WHEN 8  THEN [tt].[name]
            WHEN 10 THEN (SELECT TOP 1 [sx2].[name] FROM [sys].[xml_schema_collections] [sx2] WITH (NOLOCK) WHERE [sx2].[xml_collection_id] = [sp].[major_id])
            WHEN 22 THEN NULL
            ELSE [so].[name]
        END AS [Level1Name],
        CASE [sp].[class]			
            WHEN 2 THEN (SELECT TOP 1 [spa].[name] FROM [sys].[parameters] [spa] WITH (NOLOCK) WHERE [spa].[object_id] = [sp].[major_id] AND [spa].[parameter_id] = [sp].[minor_id])
            WHEN 7 THEN (SELECT TOP 1 [si].[name] FROM [sys].[indexes] [si] WITH (NOLOCK) WHERE [si].[object_id] = [sp].[major_id] AND [si].[index_id] = [sp].[minor_id])
            WHEN 8 THEN COL_NAME([tt].[type_table_object_id] ,[sp].[minor_id])
            ELSE NULL
        END AS [Level2Name],
        CASE 
            WHEN [sp].[class] = 20 AND EXISTS((SELECT TOP 1 [sd2].[name]  FROM [sys].[data_spaces] [sd2] WITH (NOLOCK) WHERE [sd2].[data_space_id] = [sp].[major_id] AND [sd2].[type] = N'PS')) THEN
                CAST(1 AS BIT)
            ELSE CAST(0 AS BIT)
        END AS IsPartitionScheme,
        CAST(0 AS BIT) AS [IsDefault],
        [i].[type] AS [IndexType],
        CASE [sp].[class]
            WHEN 6 THEN [tt].[type_table_object_id]
            WHEN 8 THEN [tt].[type_table_object_id]
            WHEN 1 THEN [parent].[object_id]
            ELSE NULL
        END AS [ParentId],
         CASE [sp].[class]
            WHEN 6 THEN [tt].[schema_id]
            WHEN 8 THEN [tt].[schema_id]
            WHEN 1 THEN [parent].[schema_id]
            ELSE NULL
        END AS [ParentSchemaId],
        [sxi].[xml_index_type] AS [XMLIndexType],
        ISNULL([parent].[is_ms_shipped], 0) AS [IsMsShipped]
FROM    [sys].[extended_properties] [sp] WITH (NOLOCK) 
        LEFT JOIN [sys].[all_objects] [so] WITH (NOLOCK) ON [so].[object_id] = [sp].[major_id] AND [sp].[class] IN (1, 2, 7)
        LEFT JOIN [sys].[all_objects] [parent] WITH(NOLOCK) ON [so].[parent_object_id] = [parent].[object_id]
        LEFT JOIN [sys].[indexes] [i] WITH (NOLOCk) ON [sp].[class] = 7 AND [so].[object_id] = [i].[object_id]  AND [sp].[minor_id] = [i].[index_id]
        LEFT JOIN [sys].[table_types] [tt] WITH (NOLOCK) ON [sp].[class] IN (6,8) AND [tt].[user_type_id] = [sp].[major_id]
        LEFT JOIN [sys].[xml_indexes] [sxi] WITH (NOLOCk) ON [sp].[class] = 7 AND [so].[object_id] = [sxi].[object_id]  AND [sp].[minor_id] = [sxi].[index_id]
WHERE 
        [sp].[class] <> 1
        AND (OBJECTPROPERTY([so].[parent_object_id], N'TableIsFake') = 0 OR OBJECTPROPERTY([so].[parent_object_id], N'TableIsFake')  IS NULL)
        AND [sp].[class] <> 27
        AND ([so].[type] <> N'PG' OR [so].[type] IS NULL)
        AND [sp].[name] <> N'microsoft_database_tools_support'
        AND ([i].[is_primary_key] IS NULL OR [i].[is_primary_key] = 0)
        AND ([sp].[class] <> 7 OR [i].[object_id] IS NOT NULL)
        AND ([sp].[class] <> 7 OR ([so].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [so].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )))
) AS [_results] WHERE [TypeId] = 10 AND [Level2Name] IS NULL
 ORDER BY Level0Name,Level1Name ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110ExtendedPropertyPopulator' as [PopulatorName];
SELECT * FROM (
SELECT 
        [sp].[class] AS [TypeId],
        [sp].[major_id] AS [MajorId],
        [sp].[minor_id] AS [MinorId],
        [sp].[name] AS [PropertyName], 
        [sp].[value] AS [PropertyValue],
        [so].[type] AS [ObjectType],
        SQL_VARIANT_PROPERTY([sp].[value], N'BaseType') AS [BaseType],
        CASE [sp].[class]
            WHEN  0 THEN NULL
            WHEN  3 THEN SCHEMA_NAME([sp].[major_id])
            WHEN  4 THEN USER_NAME([sp].[major_id])
            WHEN  5 THEN (SELECT TOP 1 [sa2].[name]   FROM [sys].[assemblies] [sa2] WITH (NOLOCK) WHERE [sa2].[assembly_id] = [sp].[major_id])
            WHEN  6 THEN SCHEMA_NAME((SELECT TOP 1 [st].[schema_id] FROM [sys].[types] [st] WITH (NOLOCK) WHERE [st].[user_type_id] = [sp].[major_id]))
            WHEN  8 THEN SCHEMA_NAME([tt].[schema_id])
            WHEN 10 THEN SCHEMA_NAME((SELECT TOP 1 [sxsc2].[schema_id] FROM [sys].[xml_schema_collections] [sxsc2] WITH (NOLOCK) WHERE [sxsc2].[xml_collection_id] = [sp].[major_id]))
            WHEN 15 THEN (SELECT TOP 1 [smt2].[name]  FROM [sys].[service_message_types] [smt2] WITH (NOLOCK) WHERE [smt2].[message_type_id] = [sp].[major_id]) COLLATE database_default
            WHEN 16 THEN (SELECT TOP 1 [ssc2].[name]  FROM [sys].[service_contracts] [ssc2] WITH (NOLOCK) WHERE [ssc2].[service_contract_id] = [sp].[major_id])
            WHEN 17 THEN (SELECT TOP 1 [ss2].[name]   FROM [sys].[services] [ss2] WITH (NOLOCK) WHERE [ss2].[service_id] = [sp].[major_id])
            WHEN 18 THEN (SELECT TOP 1 [srs2].[name]  FROM [sys].[remote_service_bindings] [srs2] WITH (NOLOCK) WHERE [srs2].[remote_service_binding_id] = [sp].[major_id])
            WHEN 19 THEN (SELECT TOP 1 [sr2].[name]   FROM [sys].[routes] [sr2] WITH (NOLOCK) WHERE [sr2].[route_id] = [sp].[major_id])
            WHEN 20 THEN (SELECT TOP 1 [sd2].[name]  FROM [sys].[data_spaces] [sd2] WITH (NOLOCK) WHERE [sd2].[data_space_id] = [sp].[major_id])
            WHEN 21 THEN (SELECT TOP 1 [spf2].[name]  FROM [sys].[partition_functions] [spf2] WITH (NOLOCK) WHERE [spf2].[function_id] = [sp].[major_id])
            WHEN 22 THEN (SELECT TOP 1 [sdf2].[name]  FROM [sys].[database_files] [sdf2] WITH (NOLOCK) WHERE [sdf2].[file_id] = [sp].[major_id])
            ELSE SCHEMA_NAME([so].[schema_id])
        END AS [Level0Name],
        CASE [sp].[class] 
            WHEN 3 THEN NULL
            WHEN 4 THEN NULL
            WHEN 6  THEN (SELECT TOP 1 [st2].[name] FROM [sys].[types] [st2] WITH (NOLOCK) WHERE [st2].[user_type_id] = [sp].[major_id])
            WHEN 8  THEN [tt].[name]
            WHEN 10 THEN (SELECT TOP 1 [sx2].[name] FROM [sys].[xml_schema_collections] [sx2] WITH (NOLOCK) WHERE [sx2].[xml_collection_id] = [sp].[major_id])
            WHEN 22 THEN NULL
            ELSE [so].[name]
        END AS [Level1Name],
        CASE [sp].[class]			
            WHEN 2 THEN (SELECT TOP 1 [spa].[name] FROM [sys].[parameters] [spa] WITH (NOLOCK) WHERE [spa].[object_id] = [sp].[major_id] AND [spa].[parameter_id] = [sp].[minor_id])
            WHEN 7 THEN (SELECT TOP 1 [si].[name] FROM [sys].[indexes] [si] WITH (NOLOCK) WHERE [si].[object_id] = [sp].[major_id] AND [si].[index_id] = [sp].[minor_id])
            WHEN 8 THEN COL_NAME([tt].[type_table_object_id] ,[sp].[minor_id])
            ELSE NULL
        END AS [Level2Name],
        CASE 
            WHEN [sp].[class] = 20 AND EXISTS((SELECT TOP 1 [sd2].[name]  FROM [sys].[data_spaces] [sd2] WITH (NOLOCK) WHERE [sd2].[data_space_id] = [sp].[major_id] AND [sd2].[type] = N'PS')) THEN
                CAST(1 AS BIT)
            ELSE CAST(0 AS BIT)
        END AS IsPartitionScheme,
        CAST(0 AS BIT) AS [IsDefault],
        [i].[type] AS [IndexType],
        CASE [sp].[class]
            WHEN 6 THEN [tt].[type_table_object_id]
            WHEN 8 THEN [tt].[type_table_object_id]
            WHEN 1 THEN [parent].[object_id]
            ELSE NULL
        END AS [ParentId],
         CASE [sp].[class]
            WHEN 6 THEN [tt].[schema_id]
            WHEN 8 THEN [tt].[schema_id]
            WHEN 1 THEN [parent].[schema_id]
            ELSE NULL
        END AS [ParentSchemaId],
        [sxi].[xml_index_type] AS [XMLIndexType],
        ISNULL([parent].[is_ms_shipped], 0) AS [IsMsShipped]
FROM    [sys].[extended_properties] [sp] WITH (NOLOCK) 
        LEFT JOIN [sys].[all_objects] [so] WITH (NOLOCK) ON [so].[object_id] = [sp].[major_id] AND [sp].[class] IN (1, 2, 7)
        LEFT JOIN [sys].[all_objects] [parent] WITH(NOLOCK) ON [so].[parent_object_id] = [parent].[object_id]
        LEFT JOIN [sys].[indexes] [i] WITH (NOLOCk) ON [sp].[class] = 7 AND [so].[object_id] = [i].[object_id]  AND [sp].[minor_id] = [i].[index_id]
        LEFT JOIN [sys].[table_types] [tt] WITH (NOLOCK) ON [sp].[class] IN (6,8) AND [tt].[user_type_id] = [sp].[major_id]
        LEFT JOIN [sys].[xml_indexes] [sxi] WITH (NOLOCk) ON [sp].[class] = 7 AND [so].[object_id] = [sxi].[object_id]  AND [sp].[minor_id] = [sxi].[index_id]
WHERE 
        [sp].[class] <> 1
        AND (OBJECTPROPERTY([so].[parent_object_id], N'TableIsFake') = 0 OR OBJECTPROPERTY([so].[parent_object_id], N'TableIsFake')  IS NULL)
        AND [sp].[class] <> 27
        AND ([so].[type] <> N'PG' OR [so].[type] IS NULL)
        AND [sp].[name] <> N'microsoft_database_tools_support'
        AND ([i].[is_primary_key] IS NULL OR [i].[is_primary_key] = 0)
        AND ([sp].[class] <> 7 OR [i].[object_id] IS NOT NULL)
        AND ([sp].[class] <> 7 OR ([so].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [so].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )))
) AS [_results] WHERE [TypeId] = 15 AND [Level2Name] IS NULL AND [Level1Name] IS NULL
 ORDER BY Level0Name,Level1Name ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110ExtendedPropertyPopulator' as [PopulatorName];
SELECT * FROM (
SELECT 
        [sp].[class] AS [TypeId],
        [sp].[major_id] AS [MajorId],
        [sp].[minor_id] AS [MinorId],
        [sp].[name] AS [PropertyName], 
        [sp].[value] AS [PropertyValue],
        [so].[type] AS [ObjectType],
        SQL_VARIANT_PROPERTY([sp].[value], N'BaseType') AS [BaseType],
        CASE [sp].[class]
            WHEN  0 THEN NULL
            WHEN  3 THEN SCHEMA_NAME([sp].[major_id])
            WHEN  4 THEN USER_NAME([sp].[major_id])
            WHEN  5 THEN (SELECT TOP 1 [sa2].[name]   FROM [sys].[assemblies] [sa2] WITH (NOLOCK) WHERE [sa2].[assembly_id] = [sp].[major_id])
            WHEN  6 THEN SCHEMA_NAME((SELECT TOP 1 [st].[schema_id] FROM [sys].[types] [st] WITH (NOLOCK) WHERE [st].[user_type_id] = [sp].[major_id]))
            WHEN  8 THEN SCHEMA_NAME([tt].[schema_id])
            WHEN 10 THEN SCHEMA_NAME((SELECT TOP 1 [sxsc2].[schema_id] FROM [sys].[xml_schema_collections] [sxsc2] WITH (NOLOCK) WHERE [sxsc2].[xml_collection_id] = [sp].[major_id]))
            WHEN 15 THEN (SELECT TOP 1 [smt2].[name]  FROM [sys].[service_message_types] [smt2] WITH (NOLOCK) WHERE [smt2].[message_type_id] = [sp].[major_id]) COLLATE database_default
            WHEN 16 THEN (SELECT TOP 1 [ssc2].[name]  FROM [sys].[service_contracts] [ssc2] WITH (NOLOCK) WHERE [ssc2].[service_contract_id] = [sp].[major_id])
            WHEN 17 THEN (SELECT TOP 1 [ss2].[name]   FROM [sys].[services] [ss2] WITH (NOLOCK) WHERE [ss2].[service_id] = [sp].[major_id])
            WHEN 18 THEN (SELECT TOP 1 [srs2].[name]  FROM [sys].[remote_service_bindings] [srs2] WITH (NOLOCK) WHERE [srs2].[remote_service_binding_id] = [sp].[major_id])
            WHEN 19 THEN (SELECT TOP 1 [sr2].[name]   FROM [sys].[routes] [sr2] WITH (NOLOCK) WHERE [sr2].[route_id] = [sp].[major_id])
            WHEN 20 THEN (SELECT TOP 1 [sd2].[name]  FROM [sys].[data_spaces] [sd2] WITH (NOLOCK) WHERE [sd2].[data_space_id] = [sp].[major_id])
            WHEN 21 THEN (SELECT TOP 1 [spf2].[name]  FROM [sys].[partition_functions] [spf2] WITH (NOLOCK) WHERE [spf2].[function_id] = [sp].[major_id])
            WHEN 22 THEN (SELECT TOP 1 [sdf2].[name]  FROM [sys].[database_files] [sdf2] WITH (NOLOCK) WHERE [sdf2].[file_id] = [sp].[major_id])
            ELSE SCHEMA_NAME([so].[schema_id])
        END AS [Level0Name],
        CASE [sp].[class] 
            WHEN 3 THEN NULL
            WHEN 4 THEN NULL
            WHEN 6  THEN (SELECT TOP 1 [st2].[name] FROM [sys].[types] [st2] WITH (NOLOCK) WHERE [st2].[user_type_id] = [sp].[major_id])
            WHEN 8  THEN [tt].[name]
            WHEN 10 THEN (SELECT TOP 1 [sx2].[name] FROM [sys].[xml_schema_collections] [sx2] WITH (NOLOCK) WHERE [sx2].[xml_collection_id] = [sp].[major_id])
            WHEN 22 THEN NULL
            ELSE [so].[name]
        END AS [Level1Name],
        CASE [sp].[class]			
            WHEN 2 THEN (SELECT TOP 1 [spa].[name] FROM [sys].[parameters] [spa] WITH (NOLOCK) WHERE [spa].[object_id] = [sp].[major_id] AND [spa].[parameter_id] = [sp].[minor_id])
            WHEN 7 THEN (SELECT TOP 1 [si].[name] FROM [sys].[indexes] [si] WITH (NOLOCK) WHERE [si].[object_id] = [sp].[major_id] AND [si].[index_id] = [sp].[minor_id])
            WHEN 8 THEN COL_NAME([tt].[type_table_object_id] ,[sp].[minor_id])
            ELSE NULL
        END AS [Level2Name],
        CASE 
            WHEN [sp].[class] = 20 AND EXISTS((SELECT TOP 1 [sd2].[name]  FROM [sys].[data_spaces] [sd2] WITH (NOLOCK) WHERE [sd2].[data_space_id] = [sp].[major_id] AND [sd2].[type] = N'PS')) THEN
                CAST(1 AS BIT)
            ELSE CAST(0 AS BIT)
        END AS IsPartitionScheme,
        CAST(0 AS BIT) AS [IsDefault],
        [i].[type] AS [IndexType],
        CASE [sp].[class]
            WHEN 6 THEN [tt].[type_table_object_id]
            WHEN 8 THEN [tt].[type_table_object_id]
            WHEN 1 THEN [parent].[object_id]
            ELSE NULL
        END AS [ParentId],
         CASE [sp].[class]
            WHEN 6 THEN [tt].[schema_id]
            WHEN 8 THEN [tt].[schema_id]
            WHEN 1 THEN [parent].[schema_id]
            ELSE NULL
        END AS [ParentSchemaId],
        [sxi].[xml_index_type] AS [XMLIndexType],
        ISNULL([parent].[is_ms_shipped], 0) AS [IsMsShipped]
FROM    [sys].[extended_properties] [sp] WITH (NOLOCK) 
        LEFT JOIN [sys].[all_objects] [so] WITH (NOLOCK) ON [so].[object_id] = [sp].[major_id] AND [sp].[class] IN (1, 2, 7)
        LEFT JOIN [sys].[all_objects] [parent] WITH(NOLOCK) ON [so].[parent_object_id] = [parent].[object_id]
        LEFT JOIN [sys].[indexes] [i] WITH (NOLOCk) ON [sp].[class] = 7 AND [so].[object_id] = [i].[object_id]  AND [sp].[minor_id] = [i].[index_id]
        LEFT JOIN [sys].[table_types] [tt] WITH (NOLOCK) ON [sp].[class] IN (6,8) AND [tt].[user_type_id] = [sp].[major_id]
        LEFT JOIN [sys].[xml_indexes] [sxi] WITH (NOLOCk) ON [sp].[class] = 7 AND [so].[object_id] = [sxi].[object_id]  AND [sp].[minor_id] = [sxi].[index_id]
WHERE 
        [sp].[class] <> 1
        AND (OBJECTPROPERTY([so].[parent_object_id], N'TableIsFake') = 0 OR OBJECTPROPERTY([so].[parent_object_id], N'TableIsFake')  IS NULL)
        AND [sp].[class] <> 27
        AND ([so].[type] <> N'PG' OR [so].[type] IS NULL)
        AND [sp].[name] <> N'microsoft_database_tools_support'
        AND ([i].[is_primary_key] IS NULL OR [i].[is_primary_key] = 0)
        AND ([sp].[class] <> 7 OR [i].[object_id] IS NOT NULL)
        AND ([sp].[class] <> 7 OR ([so].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [so].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )))
) AS [_results] WHERE [TypeId] = 16 AND [Level2Name] IS NULL AND [Level1Name] IS NULL
 ORDER BY Level0Name,Level1Name ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110ExtendedPropertyPopulator' as [PopulatorName];
SELECT * FROM (
SELECT 
        [sp].[class] AS [TypeId],
        [sp].[major_id] AS [MajorId],
        [sp].[minor_id] AS [MinorId],
        [sp].[name] AS [PropertyName], 
        [sp].[value] AS [PropertyValue],
        [so].[type] AS [ObjectType],
        SQL_VARIANT_PROPERTY([sp].[value], N'BaseType') AS [BaseType],
        CASE [sp].[class]
            WHEN  0 THEN NULL
            WHEN  3 THEN SCHEMA_NAME([sp].[major_id])
            WHEN  4 THEN USER_NAME([sp].[major_id])
            WHEN  5 THEN (SELECT TOP 1 [sa2].[name]   FROM [sys].[assemblies] [sa2] WITH (NOLOCK) WHERE [sa2].[assembly_id] = [sp].[major_id])
            WHEN  6 THEN SCHEMA_NAME((SELECT TOP 1 [st].[schema_id] FROM [sys].[types] [st] WITH (NOLOCK) WHERE [st].[user_type_id] = [sp].[major_id]))
            WHEN  8 THEN SCHEMA_NAME([tt].[schema_id])
            WHEN 10 THEN SCHEMA_NAME((SELECT TOP 1 [sxsc2].[schema_id] FROM [sys].[xml_schema_collections] [sxsc2] WITH (NOLOCK) WHERE [sxsc2].[xml_collection_id] = [sp].[major_id]))
            WHEN 15 THEN (SELECT TOP 1 [smt2].[name]  FROM [sys].[service_message_types] [smt2] WITH (NOLOCK) WHERE [smt2].[message_type_id] = [sp].[major_id]) COLLATE database_default
            WHEN 16 THEN (SELECT TOP 1 [ssc2].[name]  FROM [sys].[service_contracts] [ssc2] WITH (NOLOCK) WHERE [ssc2].[service_contract_id] = [sp].[major_id])
            WHEN 17 THEN (SELECT TOP 1 [ss2].[name]   FROM [sys].[services] [ss2] WITH (NOLOCK) WHERE [ss2].[service_id] = [sp].[major_id])
            WHEN 18 THEN (SELECT TOP 1 [srs2].[name]  FROM [sys].[remote_service_bindings] [srs2] WITH (NOLOCK) WHERE [srs2].[remote_service_binding_id] = [sp].[major_id])
            WHEN 19 THEN (SELECT TOP 1 [sr2].[name]   FROM [sys].[routes] [sr2] WITH (NOLOCK) WHERE [sr2].[route_id] = [sp].[major_id])
            WHEN 20 THEN (SELECT TOP 1 [sd2].[name]  FROM [sys].[data_spaces] [sd2] WITH (NOLOCK) WHERE [sd2].[data_space_id] = [sp].[major_id])
            WHEN 21 THEN (SELECT TOP 1 [spf2].[name]  FROM [sys].[partition_functions] [spf2] WITH (NOLOCK) WHERE [spf2].[function_id] = [sp].[major_id])
            WHEN 22 THEN (SELECT TOP 1 [sdf2].[name]  FROM [sys].[database_files] [sdf2] WITH (NOLOCK) WHERE [sdf2].[file_id] = [sp].[major_id])
            ELSE SCHEMA_NAME([so].[schema_id])
        END AS [Level0Name],
        CASE [sp].[class] 
            WHEN 3 THEN NULL
            WHEN 4 THEN NULL
            WHEN 6  THEN (SELECT TOP 1 [st2].[name] FROM [sys].[types] [st2] WITH (NOLOCK) WHERE [st2].[user_type_id] = [sp].[major_id])
            WHEN 8  THEN [tt].[name]
            WHEN 10 THEN (SELECT TOP 1 [sx2].[name] FROM [sys].[xml_schema_collections] [sx2] WITH (NOLOCK) WHERE [sx2].[xml_collection_id] = [sp].[major_id])
            WHEN 22 THEN NULL
            ELSE [so].[name]
        END AS [Level1Name],
        CASE [sp].[class]			
            WHEN 2 THEN (SELECT TOP 1 [spa].[name] FROM [sys].[parameters] [spa] WITH (NOLOCK) WHERE [spa].[object_id] = [sp].[major_id] AND [spa].[parameter_id] = [sp].[minor_id])
            WHEN 7 THEN (SELECT TOP 1 [si].[name] FROM [sys].[indexes] [si] WITH (NOLOCK) WHERE [si].[object_id] = [sp].[major_id] AND [si].[index_id] = [sp].[minor_id])
            WHEN 8 THEN COL_NAME([tt].[type_table_object_id] ,[sp].[minor_id])
            ELSE NULL
        END AS [Level2Name],
        CASE 
            WHEN [sp].[class] = 20 AND EXISTS((SELECT TOP 1 [sd2].[name]  FROM [sys].[data_spaces] [sd2] WITH (NOLOCK) WHERE [sd2].[data_space_id] = [sp].[major_id] AND [sd2].[type] = N'PS')) THEN
                CAST(1 AS BIT)
            ELSE CAST(0 AS BIT)
        END AS IsPartitionScheme,
        CAST(0 AS BIT) AS [IsDefault],
        [i].[type] AS [IndexType],
        CASE [sp].[class]
            WHEN 6 THEN [tt].[type_table_object_id]
            WHEN 8 THEN [tt].[type_table_object_id]
            WHEN 1 THEN [parent].[object_id]
            ELSE NULL
        END AS [ParentId],
         CASE [sp].[class]
            WHEN 6 THEN [tt].[schema_id]
            WHEN 8 THEN [tt].[schema_id]
            WHEN 1 THEN [parent].[schema_id]
            ELSE NULL
        END AS [ParentSchemaId],
        [sxi].[xml_index_type] AS [XMLIndexType],
        ISNULL([parent].[is_ms_shipped], 0) AS [IsMsShipped]
FROM    [sys].[extended_properties] [sp] WITH (NOLOCK) 
        LEFT JOIN [sys].[all_objects] [so] WITH (NOLOCK) ON [so].[object_id] = [sp].[major_id] AND [sp].[class] IN (1, 2, 7)
        LEFT JOIN [sys].[all_objects] [parent] WITH(NOLOCK) ON [so].[parent_object_id] = [parent].[object_id]
        LEFT JOIN [sys].[indexes] [i] WITH (NOLOCk) ON [sp].[class] = 7 AND [so].[object_id] = [i].[object_id]  AND [sp].[minor_id] = [i].[index_id]
        LEFT JOIN [sys].[table_types] [tt] WITH (NOLOCK) ON [sp].[class] IN (6,8) AND [tt].[user_type_id] = [sp].[major_id]
        LEFT JOIN [sys].[xml_indexes] [sxi] WITH (NOLOCk) ON [sp].[class] = 7 AND [so].[object_id] = [sxi].[object_id]  AND [sp].[minor_id] = [sxi].[index_id]
WHERE 
        [sp].[class] <> 1
        AND (OBJECTPROPERTY([so].[parent_object_id], N'TableIsFake') = 0 OR OBJECTPROPERTY([so].[parent_object_id], N'TableIsFake')  IS NULL)
        AND [sp].[class] <> 27
        AND ([so].[type] <> N'PG' OR [so].[type] IS NULL)
        AND [sp].[name] <> N'microsoft_database_tools_support'
        AND ([i].[is_primary_key] IS NULL OR [i].[is_primary_key] = 0)
        AND ([sp].[class] <> 7 OR [i].[object_id] IS NOT NULL)
        AND ([sp].[class] <> 7 OR ([so].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [so].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )))
) AS [_results] WHERE [TypeId] = 17 AND [Level2Name] IS NULL AND [Level1Name] IS NULL
 ORDER BY Level0Name,Level1Name ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110ExtendedPropertyPopulator' as [PopulatorName];
SELECT * FROM (
SELECT 
        [sp].[class] AS [TypeId],
        [sp].[major_id] AS [MajorId],
        [sp].[minor_id] AS [MinorId],
        [sp].[name] AS [PropertyName], 
        [sp].[value] AS [PropertyValue],
        [so].[type] AS [ObjectType],
        SQL_VARIANT_PROPERTY([sp].[value], N'BaseType') AS [BaseType],
        CASE [sp].[class]
            WHEN  0 THEN NULL
            WHEN  3 THEN SCHEMA_NAME([sp].[major_id])
            WHEN  4 THEN USER_NAME([sp].[major_id])
            WHEN  5 THEN (SELECT TOP 1 [sa2].[name]   FROM [sys].[assemblies] [sa2] WITH (NOLOCK) WHERE [sa2].[assembly_id] = [sp].[major_id])
            WHEN  6 THEN SCHEMA_NAME((SELECT TOP 1 [st].[schema_id] FROM [sys].[types] [st] WITH (NOLOCK) WHERE [st].[user_type_id] = [sp].[major_id]))
            WHEN  8 THEN SCHEMA_NAME([tt].[schema_id])
            WHEN 10 THEN SCHEMA_NAME((SELECT TOP 1 [sxsc2].[schema_id] FROM [sys].[xml_schema_collections] [sxsc2] WITH (NOLOCK) WHERE [sxsc2].[xml_collection_id] = [sp].[major_id]))
            WHEN 15 THEN (SELECT TOP 1 [smt2].[name]  FROM [sys].[service_message_types] [smt2] WITH (NOLOCK) WHERE [smt2].[message_type_id] = [sp].[major_id]) COLLATE database_default
            WHEN 16 THEN (SELECT TOP 1 [ssc2].[name]  FROM [sys].[service_contracts] [ssc2] WITH (NOLOCK) WHERE [ssc2].[service_contract_id] = [sp].[major_id])
            WHEN 17 THEN (SELECT TOP 1 [ss2].[name]   FROM [sys].[services] [ss2] WITH (NOLOCK) WHERE [ss2].[service_id] = [sp].[major_id])
            WHEN 18 THEN (SELECT TOP 1 [srs2].[name]  FROM [sys].[remote_service_bindings] [srs2] WITH (NOLOCK) WHERE [srs2].[remote_service_binding_id] = [sp].[major_id])
            WHEN 19 THEN (SELECT TOP 1 [sr2].[name]   FROM [sys].[routes] [sr2] WITH (NOLOCK) WHERE [sr2].[route_id] = [sp].[major_id])
            WHEN 20 THEN (SELECT TOP 1 [sd2].[name]  FROM [sys].[data_spaces] [sd2] WITH (NOLOCK) WHERE [sd2].[data_space_id] = [sp].[major_id])
            WHEN 21 THEN (SELECT TOP 1 [spf2].[name]  FROM [sys].[partition_functions] [spf2] WITH (NOLOCK) WHERE [spf2].[function_id] = [sp].[major_id])
            WHEN 22 THEN (SELECT TOP 1 [sdf2].[name]  FROM [sys].[database_files] [sdf2] WITH (NOLOCK) WHERE [sdf2].[file_id] = [sp].[major_id])
            ELSE SCHEMA_NAME([so].[schema_id])
        END AS [Level0Name],
        CASE [sp].[class] 
            WHEN 3 THEN NULL
            WHEN 4 THEN NULL
            WHEN 6  THEN (SELECT TOP 1 [st2].[name] FROM [sys].[types] [st2] WITH (NOLOCK) WHERE [st2].[user_type_id] = [sp].[major_id])
            WHEN 8  THEN [tt].[name]
            WHEN 10 THEN (SELECT TOP 1 [sx2].[name] FROM [sys].[xml_schema_collections] [sx2] WITH (NOLOCK) WHERE [sx2].[xml_collection_id] = [sp].[major_id])
            WHEN 22 THEN NULL
            ELSE [so].[name]
        END AS [Level1Name],
        CASE [sp].[class]			
            WHEN 2 THEN (SELECT TOP 1 [spa].[name] FROM [sys].[parameters] [spa] WITH (NOLOCK) WHERE [spa].[object_id] = [sp].[major_id] AND [spa].[parameter_id] = [sp].[minor_id])
            WHEN 7 THEN (SELECT TOP 1 [si].[name] FROM [sys].[indexes] [si] WITH (NOLOCK) WHERE [si].[object_id] = [sp].[major_id] AND [si].[index_id] = [sp].[minor_id])
            WHEN 8 THEN COL_NAME([tt].[type_table_object_id] ,[sp].[minor_id])
            ELSE NULL
        END AS [Level2Name],
        CASE 
            WHEN [sp].[class] = 20 AND EXISTS((SELECT TOP 1 [sd2].[name]  FROM [sys].[data_spaces] [sd2] WITH (NOLOCK) WHERE [sd2].[data_space_id] = [sp].[major_id] AND [sd2].[type] = N'PS')) THEN
                CAST(1 AS BIT)
            ELSE CAST(0 AS BIT)
        END AS IsPartitionScheme,
        CAST(0 AS BIT) AS [IsDefault],
        [i].[type] AS [IndexType],
        CASE [sp].[class]
            WHEN 6 THEN [tt].[type_table_object_id]
            WHEN 8 THEN [tt].[type_table_object_id]
            WHEN 1 THEN [parent].[object_id]
            ELSE NULL
        END AS [ParentId],
         CASE [sp].[class]
            WHEN 6 THEN [tt].[schema_id]
            WHEN 8 THEN [tt].[schema_id]
            WHEN 1 THEN [parent].[schema_id]
            ELSE NULL
        END AS [ParentSchemaId],
        [sxi].[xml_index_type] AS [XMLIndexType],
        ISNULL([parent].[is_ms_shipped], 0) AS [IsMsShipped]
FROM    [sys].[extended_properties] [sp] WITH (NOLOCK) 
        LEFT JOIN [sys].[all_objects] [so] WITH (NOLOCK) ON [so].[object_id] = [sp].[major_id] AND [sp].[class] IN (1, 2, 7)
        LEFT JOIN [sys].[all_objects] [parent] WITH(NOLOCK) ON [so].[parent_object_id] = [parent].[object_id]
        LEFT JOIN [sys].[indexes] [i] WITH (NOLOCk) ON [sp].[class] = 7 AND [so].[object_id] = [i].[object_id]  AND [sp].[minor_id] = [i].[index_id]
        LEFT JOIN [sys].[table_types] [tt] WITH (NOLOCK) ON [sp].[class] IN (6,8) AND [tt].[user_type_id] = [sp].[major_id]
        LEFT JOIN [sys].[xml_indexes] [sxi] WITH (NOLOCk) ON [sp].[class] = 7 AND [so].[object_id] = [sxi].[object_id]  AND [sp].[minor_id] = [sxi].[index_id]
WHERE 
        [sp].[class] <> 1
        AND (OBJECTPROPERTY([so].[parent_object_id], N'TableIsFake') = 0 OR OBJECTPROPERTY([so].[parent_object_id], N'TableIsFake')  IS NULL)
        AND [sp].[class] <> 27
        AND ([so].[type] <> N'PG' OR [so].[type] IS NULL)
        AND [sp].[name] <> N'microsoft_database_tools_support'
        AND ([i].[is_primary_key] IS NULL OR [i].[is_primary_key] = 0)
        AND ([sp].[class] <> 7 OR [i].[object_id] IS NOT NULL)
        AND ([sp].[class] <> 7 OR ([so].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [so].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )))
) AS [_results] WHERE [TypeId] = 18 AND [Level2Name] IS NULL AND [Level1Name] IS NULL
 ORDER BY Level0Name,Level1Name ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110ExtendedPropertyPopulator' as [PopulatorName];
SELECT * FROM (
SELECT 
        [sp].[class] AS [TypeId],
        [sp].[major_id] AS [MajorId],
        [sp].[minor_id] AS [MinorId],
        [sp].[name] AS [PropertyName], 
        [sp].[value] AS [PropertyValue],
        [so].[type] AS [ObjectType],
        SQL_VARIANT_PROPERTY([sp].[value], N'BaseType') AS [BaseType],
        CASE [sp].[class]
            WHEN  0 THEN NULL
            WHEN  3 THEN SCHEMA_NAME([sp].[major_id])
            WHEN  4 THEN USER_NAME([sp].[major_id])
            WHEN  5 THEN (SELECT TOP 1 [sa2].[name]   FROM [sys].[assemblies] [sa2] WITH (NOLOCK) WHERE [sa2].[assembly_id] = [sp].[major_id])
            WHEN  6 THEN SCHEMA_NAME((SELECT TOP 1 [st].[schema_id] FROM [sys].[types] [st] WITH (NOLOCK) WHERE [st].[user_type_id] = [sp].[major_id]))
            WHEN  8 THEN SCHEMA_NAME([tt].[schema_id])
            WHEN 10 THEN SCHEMA_NAME((SELECT TOP 1 [sxsc2].[schema_id] FROM [sys].[xml_schema_collections] [sxsc2] WITH (NOLOCK) WHERE [sxsc2].[xml_collection_id] = [sp].[major_id]))
            WHEN 15 THEN (SELECT TOP 1 [smt2].[name]  FROM [sys].[service_message_types] [smt2] WITH (NOLOCK) WHERE [smt2].[message_type_id] = [sp].[major_id]) COLLATE database_default
            WHEN 16 THEN (SELECT TOP 1 [ssc2].[name]  FROM [sys].[service_contracts] [ssc2] WITH (NOLOCK) WHERE [ssc2].[service_contract_id] = [sp].[major_id])
            WHEN 17 THEN (SELECT TOP 1 [ss2].[name]   FROM [sys].[services] [ss2] WITH (NOLOCK) WHERE [ss2].[service_id] = [sp].[major_id])
            WHEN 18 THEN (SELECT TOP 1 [srs2].[name]  FROM [sys].[remote_service_bindings] [srs2] WITH (NOLOCK) WHERE [srs2].[remote_service_binding_id] = [sp].[major_id])
            WHEN 19 THEN (SELECT TOP 1 [sr2].[name]   FROM [sys].[routes] [sr2] WITH (NOLOCK) WHERE [sr2].[route_id] = [sp].[major_id])
            WHEN 20 THEN (SELECT TOP 1 [sd2].[name]  FROM [sys].[data_spaces] [sd2] WITH (NOLOCK) WHERE [sd2].[data_space_id] = [sp].[major_id])
            WHEN 21 THEN (SELECT TOP 1 [spf2].[name]  FROM [sys].[partition_functions] [spf2] WITH (NOLOCK) WHERE [spf2].[function_id] = [sp].[major_id])
            WHEN 22 THEN (SELECT TOP 1 [sdf2].[name]  FROM [sys].[database_files] [sdf2] WITH (NOLOCK) WHERE [sdf2].[file_id] = [sp].[major_id])
            ELSE SCHEMA_NAME([so].[schema_id])
        END AS [Level0Name],
        CASE [sp].[class] 
            WHEN 3 THEN NULL
            WHEN 4 THEN NULL
            WHEN 6  THEN (SELECT TOP 1 [st2].[name] FROM [sys].[types] [st2] WITH (NOLOCK) WHERE [st2].[user_type_id] = [sp].[major_id])
            WHEN 8  THEN [tt].[name]
            WHEN 10 THEN (SELECT TOP 1 [sx2].[name] FROM [sys].[xml_schema_collections] [sx2] WITH (NOLOCK) WHERE [sx2].[xml_collection_id] = [sp].[major_id])
            WHEN 22 THEN NULL
            ELSE [so].[name]
        END AS [Level1Name],
        CASE [sp].[class]			
            WHEN 2 THEN (SELECT TOP 1 [spa].[name] FROM [sys].[parameters] [spa] WITH (NOLOCK) WHERE [spa].[object_id] = [sp].[major_id] AND [spa].[parameter_id] = [sp].[minor_id])
            WHEN 7 THEN (SELECT TOP 1 [si].[name] FROM [sys].[indexes] [si] WITH (NOLOCK) WHERE [si].[object_id] = [sp].[major_id] AND [si].[index_id] = [sp].[minor_id])
            WHEN 8 THEN COL_NAME([tt].[type_table_object_id] ,[sp].[minor_id])
            ELSE NULL
        END AS [Level2Name],
        CASE 
            WHEN [sp].[class] = 20 AND EXISTS((SELECT TOP 1 [sd2].[name]  FROM [sys].[data_spaces] [sd2] WITH (NOLOCK) WHERE [sd2].[data_space_id] = [sp].[major_id] AND [sd2].[type] = N'PS')) THEN
                CAST(1 AS BIT)
            ELSE CAST(0 AS BIT)
        END AS IsPartitionScheme,
        CAST(0 AS BIT) AS [IsDefault],
        [i].[type] AS [IndexType],
        CASE [sp].[class]
            WHEN 6 THEN [tt].[type_table_object_id]
            WHEN 8 THEN [tt].[type_table_object_id]
            WHEN 1 THEN [parent].[object_id]
            ELSE NULL
        END AS [ParentId],
         CASE [sp].[class]
            WHEN 6 THEN [tt].[schema_id]
            WHEN 8 THEN [tt].[schema_id]
            WHEN 1 THEN [parent].[schema_id]
            ELSE NULL
        END AS [ParentSchemaId],
        [sxi].[xml_index_type] AS [XMLIndexType],
        ISNULL([parent].[is_ms_shipped], 0) AS [IsMsShipped]
FROM    [sys].[extended_properties] [sp] WITH (NOLOCK) 
        LEFT JOIN [sys].[all_objects] [so] WITH (NOLOCK) ON [so].[object_id] = [sp].[major_id] AND [sp].[class] IN (1, 2, 7)
        LEFT JOIN [sys].[all_objects] [parent] WITH(NOLOCK) ON [so].[parent_object_id] = [parent].[object_id]
        LEFT JOIN [sys].[indexes] [i] WITH (NOLOCk) ON [sp].[class] = 7 AND [so].[object_id] = [i].[object_id]  AND [sp].[minor_id] = [i].[index_id]
        LEFT JOIN [sys].[table_types] [tt] WITH (NOLOCK) ON [sp].[class] IN (6,8) AND [tt].[user_type_id] = [sp].[major_id]
        LEFT JOIN [sys].[xml_indexes] [sxi] WITH (NOLOCk) ON [sp].[class] = 7 AND [so].[object_id] = [sxi].[object_id]  AND [sp].[minor_id] = [sxi].[index_id]
WHERE 
        [sp].[class] <> 1
        AND (OBJECTPROPERTY([so].[parent_object_id], N'TableIsFake') = 0 OR OBJECTPROPERTY([so].[parent_object_id], N'TableIsFake')  IS NULL)
        AND [sp].[class] <> 27
        AND ([so].[type] <> N'PG' OR [so].[type] IS NULL)
        AND [sp].[name] <> N'microsoft_database_tools_support'
        AND ([i].[is_primary_key] IS NULL OR [i].[is_primary_key] = 0)
        AND ([sp].[class] <> 7 OR [i].[object_id] IS NOT NULL)
        AND ([sp].[class] <> 7 OR ([so].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [so].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )))
) AS [_results] WHERE [TypeId] = 20 AND [Level2Name] IS NULL AND [Level1Name] IS NULL
 ORDER BY Level0Name,Level1Name ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110ExtendedPropertyPopulator' as [PopulatorName];
SELECT * FROM (
SELECT 
        [sp].[class] AS [TypeId],
        [sp].[major_id] AS [MajorId],
        [sp].[minor_id] AS [MinorId],
        [sp].[name] AS [PropertyName], 
        [sp].[value] AS [PropertyValue],
        [so].[type] AS [ObjectType],
        SQL_VARIANT_PROPERTY([sp].[value], N'BaseType') AS [BaseType],
        CASE [sp].[class]
            WHEN  0 THEN NULL
            WHEN  3 THEN SCHEMA_NAME([sp].[major_id])
            WHEN  4 THEN USER_NAME([sp].[major_id])
            WHEN  5 THEN (SELECT TOP 1 [sa2].[name]   FROM [sys].[assemblies] [sa2] WITH (NOLOCK) WHERE [sa2].[assembly_id] = [sp].[major_id])
            WHEN  6 THEN SCHEMA_NAME((SELECT TOP 1 [st].[schema_id] FROM [sys].[types] [st] WITH (NOLOCK) WHERE [st].[user_type_id] = [sp].[major_id]))
            WHEN  8 THEN SCHEMA_NAME([tt].[schema_id])
            WHEN 10 THEN SCHEMA_NAME((SELECT TOP 1 [sxsc2].[schema_id] FROM [sys].[xml_schema_collections] [sxsc2] WITH (NOLOCK) WHERE [sxsc2].[xml_collection_id] = [sp].[major_id]))
            WHEN 15 THEN (SELECT TOP 1 [smt2].[name]  FROM [sys].[service_message_types] [smt2] WITH (NOLOCK) WHERE [smt2].[message_type_id] = [sp].[major_id]) COLLATE database_default
            WHEN 16 THEN (SELECT TOP 1 [ssc2].[name]  FROM [sys].[service_contracts] [ssc2] WITH (NOLOCK) WHERE [ssc2].[service_contract_id] = [sp].[major_id])
            WHEN 17 THEN (SELECT TOP 1 [ss2].[name]   FROM [sys].[services] [ss2] WITH (NOLOCK) WHERE [ss2].[service_id] = [sp].[major_id])
            WHEN 18 THEN (SELECT TOP 1 [srs2].[name]  FROM [sys].[remote_service_bindings] [srs2] WITH (NOLOCK) WHERE [srs2].[remote_service_binding_id] = [sp].[major_id])
            WHEN 19 THEN (SELECT TOP 1 [sr2].[name]   FROM [sys].[routes] [sr2] WITH (NOLOCK) WHERE [sr2].[route_id] = [sp].[major_id])
            WHEN 20 THEN (SELECT TOP 1 [sd2].[name]  FROM [sys].[data_spaces] [sd2] WITH (NOLOCK) WHERE [sd2].[data_space_id] = [sp].[major_id])
            WHEN 21 THEN (SELECT TOP 1 [spf2].[name]  FROM [sys].[partition_functions] [spf2] WITH (NOLOCK) WHERE [spf2].[function_id] = [sp].[major_id])
            WHEN 22 THEN (SELECT TOP 1 [sdf2].[name]  FROM [sys].[database_files] [sdf2] WITH (NOLOCK) WHERE [sdf2].[file_id] = [sp].[major_id])
            ELSE SCHEMA_NAME([so].[schema_id])
        END AS [Level0Name],
        CASE [sp].[class] 
            WHEN 3 THEN NULL
            WHEN 4 THEN NULL
            WHEN 6  THEN (SELECT TOP 1 [st2].[name] FROM [sys].[types] [st2] WITH (NOLOCK) WHERE [st2].[user_type_id] = [sp].[major_id])
            WHEN 8  THEN [tt].[name]
            WHEN 10 THEN (SELECT TOP 1 [sx2].[name] FROM [sys].[xml_schema_collections] [sx2] WITH (NOLOCK) WHERE [sx2].[xml_collection_id] = [sp].[major_id])
            WHEN 22 THEN NULL
            ELSE [so].[name]
        END AS [Level1Name],
        CASE [sp].[class]			
            WHEN 2 THEN (SELECT TOP 1 [spa].[name] FROM [sys].[parameters] [spa] WITH (NOLOCK) WHERE [spa].[object_id] = [sp].[major_id] AND [spa].[parameter_id] = [sp].[minor_id])
            WHEN 7 THEN (SELECT TOP 1 [si].[name] FROM [sys].[indexes] [si] WITH (NOLOCK) WHERE [si].[object_id] = [sp].[major_id] AND [si].[index_id] = [sp].[minor_id])
            WHEN 8 THEN COL_NAME([tt].[type_table_object_id] ,[sp].[minor_id])
            ELSE NULL
        END AS [Level2Name],
        CASE 
            WHEN [sp].[class] = 20 AND EXISTS((SELECT TOP 1 [sd2].[name]  FROM [sys].[data_spaces] [sd2] WITH (NOLOCK) WHERE [sd2].[data_space_id] = [sp].[major_id] AND [sd2].[type] = N'PS')) THEN
                CAST(1 AS BIT)
            ELSE CAST(0 AS BIT)
        END AS IsPartitionScheme,
        CAST(0 AS BIT) AS [IsDefault],
        [i].[type] AS [IndexType],
        CASE [sp].[class]
            WHEN 6 THEN [tt].[type_table_object_id]
            WHEN 8 THEN [tt].[type_table_object_id]
            WHEN 1 THEN [parent].[object_id]
            ELSE NULL
        END AS [ParentId],
         CASE [sp].[class]
            WHEN 6 THEN [tt].[schema_id]
            WHEN 8 THEN [tt].[schema_id]
            WHEN 1 THEN [parent].[schema_id]
            ELSE NULL
        END AS [ParentSchemaId],
        [sxi].[xml_index_type] AS [XMLIndexType],
        ISNULL([parent].[is_ms_shipped], 0) AS [IsMsShipped]
FROM    [sys].[extended_properties] [sp] WITH (NOLOCK) 
        LEFT JOIN [sys].[all_objects] [so] WITH (NOLOCK) ON [so].[object_id] = [sp].[major_id] AND [sp].[class] IN (1, 2, 7)
        LEFT JOIN [sys].[all_objects] [parent] WITH(NOLOCK) ON [so].[parent_object_id] = [parent].[object_id]
        LEFT JOIN [sys].[indexes] [i] WITH (NOLOCk) ON [sp].[class] = 7 AND [so].[object_id] = [i].[object_id]  AND [sp].[minor_id] = [i].[index_id]
        LEFT JOIN [sys].[table_types] [tt] WITH (NOLOCK) ON [sp].[class] IN (6,8) AND [tt].[user_type_id] = [sp].[major_id]
        LEFT JOIN [sys].[xml_indexes] [sxi] WITH (NOLOCk) ON [sp].[class] = 7 AND [so].[object_id] = [sxi].[object_id]  AND [sp].[minor_id] = [sxi].[index_id]
WHERE 
        [sp].[class] <> 1
        AND (OBJECTPROPERTY([so].[parent_object_id], N'TableIsFake') = 0 OR OBJECTPROPERTY([so].[parent_object_id], N'TableIsFake')  IS NULL)
        AND [sp].[class] <> 27
        AND ([so].[type] <> N'PG' OR [so].[type] IS NULL)
        AND [sp].[name] <> N'microsoft_database_tools_support'
        AND ([i].[is_primary_key] IS NULL OR [i].[is_primary_key] = 0)
        AND ([sp].[class] <> 7 OR [i].[object_id] IS NOT NULL)
        AND ([sp].[class] <> 7 OR ([so].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [so].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )))
) AS [_results] WHERE [TypeId] = 21 AND [Level2Name] IS NULL AND [Level1Name] IS NULL
 ORDER BY Level0Name,Level1Name ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110ExtendedPropertyPopulator' as [PopulatorName];
SELECT * FROM (
SELECT 
        [sp].[class] AS [TypeId],
        [sp].[major_id] AS [MajorId],
        [sp].[minor_id] AS [MinorId],
        [sp].[name] AS [PropertyName], 
        [sp].[value] AS [PropertyValue],
        [so].[type] AS [ObjectType],
        SQL_VARIANT_PROPERTY([sp].[value], N'BaseType') AS [BaseType],
        CASE [sp].[class]
            WHEN  0 THEN NULL
            WHEN  3 THEN SCHEMA_NAME([sp].[major_id])
            WHEN  4 THEN USER_NAME([sp].[major_id])
            WHEN  5 THEN (SELECT TOP 1 [sa2].[name]   FROM [sys].[assemblies] [sa2] WITH (NOLOCK) WHERE [sa2].[assembly_id] = [sp].[major_id])
            WHEN  6 THEN SCHEMA_NAME((SELECT TOP 1 [st].[schema_id] FROM [sys].[types] [st] WITH (NOLOCK) WHERE [st].[user_type_id] = [sp].[major_id]))
            WHEN  8 THEN SCHEMA_NAME([tt].[schema_id])
            WHEN 10 THEN SCHEMA_NAME((SELECT TOP 1 [sxsc2].[schema_id] FROM [sys].[xml_schema_collections] [sxsc2] WITH (NOLOCK) WHERE [sxsc2].[xml_collection_id] = [sp].[major_id]))
            WHEN 15 THEN (SELECT TOP 1 [smt2].[name]  FROM [sys].[service_message_types] [smt2] WITH (NOLOCK) WHERE [smt2].[message_type_id] = [sp].[major_id]) COLLATE database_default
            WHEN 16 THEN (SELECT TOP 1 [ssc2].[name]  FROM [sys].[service_contracts] [ssc2] WITH (NOLOCK) WHERE [ssc2].[service_contract_id] = [sp].[major_id])
            WHEN 17 THEN (SELECT TOP 1 [ss2].[name]   FROM [sys].[services] [ss2] WITH (NOLOCK) WHERE [ss2].[service_id] = [sp].[major_id])
            WHEN 18 THEN (SELECT TOP 1 [srs2].[name]  FROM [sys].[remote_service_bindings] [srs2] WITH (NOLOCK) WHERE [srs2].[remote_service_binding_id] = [sp].[major_id])
            WHEN 19 THEN (SELECT TOP 1 [sr2].[name]   FROM [sys].[routes] [sr2] WITH (NOLOCK) WHERE [sr2].[route_id] = [sp].[major_id])
            WHEN 20 THEN (SELECT TOP 1 [sd2].[name]  FROM [sys].[data_spaces] [sd2] WITH (NOLOCK) WHERE [sd2].[data_space_id] = [sp].[major_id])
            WHEN 21 THEN (SELECT TOP 1 [spf2].[name]  FROM [sys].[partition_functions] [spf2] WITH (NOLOCK) WHERE [spf2].[function_id] = [sp].[major_id])
            WHEN 22 THEN (SELECT TOP 1 [sdf2].[name]  FROM [sys].[database_files] [sdf2] WITH (NOLOCK) WHERE [sdf2].[file_id] = [sp].[major_id])
            ELSE SCHEMA_NAME([so].[schema_id])
        END AS [Level0Name],
        CASE [sp].[class] 
            WHEN 3 THEN NULL
            WHEN 4 THEN NULL
            WHEN 6  THEN (SELECT TOP 1 [st2].[name] FROM [sys].[types] [st2] WITH (NOLOCK) WHERE [st2].[user_type_id] = [sp].[major_id])
            WHEN 8  THEN [tt].[name]
            WHEN 10 THEN (SELECT TOP 1 [sx2].[name] FROM [sys].[xml_schema_collections] [sx2] WITH (NOLOCK) WHERE [sx2].[xml_collection_id] = [sp].[major_id])
            WHEN 22 THEN NULL
            ELSE [so].[name]
        END AS [Level1Name],
        CASE [sp].[class]			
            WHEN 2 THEN (SELECT TOP 1 [spa].[name] FROM [sys].[parameters] [spa] WITH (NOLOCK) WHERE [spa].[object_id] = [sp].[major_id] AND [spa].[parameter_id] = [sp].[minor_id])
            WHEN 7 THEN (SELECT TOP 1 [si].[name] FROM [sys].[indexes] [si] WITH (NOLOCK) WHERE [si].[object_id] = [sp].[major_id] AND [si].[index_id] = [sp].[minor_id])
            WHEN 8 THEN COL_NAME([tt].[type_table_object_id] ,[sp].[minor_id])
            ELSE NULL
        END AS [Level2Name],
        CASE 
            WHEN [sp].[class] = 20 AND EXISTS((SELECT TOP 1 [sd2].[name]  FROM [sys].[data_spaces] [sd2] WITH (NOLOCK) WHERE [sd2].[data_space_id] = [sp].[major_id] AND [sd2].[type] = N'PS')) THEN
                CAST(1 AS BIT)
            ELSE CAST(0 AS BIT)
        END AS IsPartitionScheme,
        CAST(0 AS BIT) AS [IsDefault],
        [i].[type] AS [IndexType],
        CASE [sp].[class]
            WHEN 6 THEN [tt].[type_table_object_id]
            WHEN 8 THEN [tt].[type_table_object_id]
            WHEN 1 THEN [parent].[object_id]
            ELSE NULL
        END AS [ParentId],
         CASE [sp].[class]
            WHEN 6 THEN [tt].[schema_id]
            WHEN 8 THEN [tt].[schema_id]
            WHEN 1 THEN [parent].[schema_id]
            ELSE NULL
        END AS [ParentSchemaId],
        [sxi].[xml_index_type] AS [XMLIndexType],
        ISNULL([parent].[is_ms_shipped], 0) AS [IsMsShipped]
FROM    [sys].[extended_properties] [sp] WITH (NOLOCK) 
        LEFT JOIN [sys].[all_objects] [so] WITH (NOLOCK) ON [so].[object_id] = [sp].[major_id] AND [sp].[class] IN (1, 2, 7)
        LEFT JOIN [sys].[all_objects] [parent] WITH(NOLOCK) ON [so].[parent_object_id] = [parent].[object_id]
        LEFT JOIN [sys].[indexes] [i] WITH (NOLOCk) ON [sp].[class] = 7 AND [so].[object_id] = [i].[object_id]  AND [sp].[minor_id] = [i].[index_id]
        LEFT JOIN [sys].[table_types] [tt] WITH (NOLOCK) ON [sp].[class] IN (6,8) AND [tt].[user_type_id] = [sp].[major_id]
        LEFT JOIN [sys].[xml_indexes] [sxi] WITH (NOLOCk) ON [sp].[class] = 7 AND [so].[object_id] = [sxi].[object_id]  AND [sp].[minor_id] = [sxi].[index_id]
WHERE 
        [sp].[class] <> 1
        AND (OBJECTPROPERTY([so].[parent_object_id], N'TableIsFake') = 0 OR OBJECTPROPERTY([so].[parent_object_id], N'TableIsFake')  IS NULL)
        AND [sp].[class] <> 27
        AND ([so].[type] <> N'PG' OR [so].[type] IS NULL)
        AND [sp].[name] <> N'microsoft_database_tools_support'
        AND ([i].[is_primary_key] IS NULL OR [i].[is_primary_key] = 0)
        AND ([sp].[class] <> 7 OR [i].[object_id] IS NOT NULL)
        AND ([sp].[class] <> 7 OR ([so].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [so].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )))
) AS [_results] WHERE [TypeId] = 23 AND [Level2Name] IS NULL AND [Level1Name] IS NULL
 ORDER BY Level0Name,Level1Name ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110ExtendedPropertyPopulator' as [PopulatorName];
SELECT * FROM (
SELECT 
        [sp].[class] AS [TypeId],
        [sp].[major_id] AS [MajorId],
        [sp].[minor_id] AS [MinorId],
        [sp].[name] AS [PropertyName], 
        [sp].[value] AS [PropertyValue],
        [so].[type] AS [ObjectType],
        SQL_VARIANT_PROPERTY([sp].[value], N'BaseType') AS [BaseType],
        CASE [sp].[class]
            WHEN  0 THEN NULL
            WHEN  3 THEN SCHEMA_NAME([sp].[major_id])
            WHEN  4 THEN USER_NAME([sp].[major_id])
            WHEN  5 THEN (SELECT TOP 1 [sa2].[name]   FROM [sys].[assemblies] [sa2] WITH (NOLOCK) WHERE [sa2].[assembly_id] = [sp].[major_id])
            WHEN  6 THEN SCHEMA_NAME((SELECT TOP 1 [st].[schema_id] FROM [sys].[types] [st] WITH (NOLOCK) WHERE [st].[user_type_id] = [sp].[major_id]))
            WHEN  8 THEN SCHEMA_NAME([tt].[schema_id])
            WHEN 10 THEN SCHEMA_NAME((SELECT TOP 1 [sxsc2].[schema_id] FROM [sys].[xml_schema_collections] [sxsc2] WITH (NOLOCK) WHERE [sxsc2].[xml_collection_id] = [sp].[major_id]))
            WHEN 15 THEN (SELECT TOP 1 [smt2].[name]  FROM [sys].[service_message_types] [smt2] WITH (NOLOCK) WHERE [smt2].[message_type_id] = [sp].[major_id]) COLLATE database_default
            WHEN 16 THEN (SELECT TOP 1 [ssc2].[name]  FROM [sys].[service_contracts] [ssc2] WITH (NOLOCK) WHERE [ssc2].[service_contract_id] = [sp].[major_id])
            WHEN 17 THEN (SELECT TOP 1 [ss2].[name]   FROM [sys].[services] [ss2] WITH (NOLOCK) WHERE [ss2].[service_id] = [sp].[major_id])
            WHEN 18 THEN (SELECT TOP 1 [srs2].[name]  FROM [sys].[remote_service_bindings] [srs2] WITH (NOLOCK) WHERE [srs2].[remote_service_binding_id] = [sp].[major_id])
            WHEN 19 THEN (SELECT TOP 1 [sr2].[name]   FROM [sys].[routes] [sr2] WITH (NOLOCK) WHERE [sr2].[route_id] = [sp].[major_id])
            WHEN 20 THEN (SELECT TOP 1 [sd2].[name]  FROM [sys].[data_spaces] [sd2] WITH (NOLOCK) WHERE [sd2].[data_space_id] = [sp].[major_id])
            WHEN 21 THEN (SELECT TOP 1 [spf2].[name]  FROM [sys].[partition_functions] [spf2] WITH (NOLOCK) WHERE [spf2].[function_id] = [sp].[major_id])
            WHEN 22 THEN (SELECT TOP 1 [sdf2].[name]  FROM [sys].[database_files] [sdf2] WITH (NOLOCK) WHERE [sdf2].[file_id] = [sp].[major_id])
            ELSE SCHEMA_NAME([so].[schema_id])
        END AS [Level0Name],
        CASE [sp].[class] 
            WHEN 3 THEN NULL
            WHEN 4 THEN NULL
            WHEN 6  THEN (SELECT TOP 1 [st2].[name] FROM [sys].[types] [st2] WITH (NOLOCK) WHERE [st2].[user_type_id] = [sp].[major_id])
            WHEN 8  THEN [tt].[name]
            WHEN 10 THEN (SELECT TOP 1 [sx2].[name] FROM [sys].[xml_schema_collections] [sx2] WITH (NOLOCK) WHERE [sx2].[xml_collection_id] = [sp].[major_id])
            WHEN 22 THEN NULL
            ELSE [so].[name]
        END AS [Level1Name],
        CASE [sp].[class]			
            WHEN 2 THEN (SELECT TOP 1 [spa].[name] FROM [sys].[parameters] [spa] WITH (NOLOCK) WHERE [spa].[object_id] = [sp].[major_id] AND [spa].[parameter_id] = [sp].[minor_id])
            WHEN 7 THEN (SELECT TOP 1 [si].[name] FROM [sys].[indexes] [si] WITH (NOLOCK) WHERE [si].[object_id] = [sp].[major_id] AND [si].[index_id] = [sp].[minor_id])
            WHEN 8 THEN COL_NAME([tt].[type_table_object_id] ,[sp].[minor_id])
            ELSE NULL
        END AS [Level2Name],
        CASE 
            WHEN [sp].[class] = 20 AND EXISTS((SELECT TOP 1 [sd2].[name]  FROM [sys].[data_spaces] [sd2] WITH (NOLOCK) WHERE [sd2].[data_space_id] = [sp].[major_id] AND [sd2].[type] = N'PS')) THEN
                CAST(1 AS BIT)
            ELSE CAST(0 AS BIT)
        END AS IsPartitionScheme,
        CAST(0 AS BIT) AS [IsDefault],
        [i].[type] AS [IndexType],
        CASE [sp].[class]
            WHEN 6 THEN [tt].[type_table_object_id]
            WHEN 8 THEN [tt].[type_table_object_id]
            WHEN 1 THEN [parent].[object_id]
            ELSE NULL
        END AS [ParentId],
         CASE [sp].[class]
            WHEN 6 THEN [tt].[schema_id]
            WHEN 8 THEN [tt].[schema_id]
            WHEN 1 THEN [parent].[schema_id]
            ELSE NULL
        END AS [ParentSchemaId],
        [sxi].[xml_index_type] AS [XMLIndexType],
        ISNULL([parent].[is_ms_shipped], 0) AS [IsMsShipped]
FROM    [sys].[extended_properties] [sp] WITH (NOLOCK) 
        LEFT JOIN [sys].[all_objects] [so] WITH (NOLOCK) ON [so].[object_id] = [sp].[major_id] AND [sp].[class] IN (1, 2, 7)
        LEFT JOIN [sys].[all_objects] [parent] WITH(NOLOCK) ON [so].[parent_object_id] = [parent].[object_id]
        LEFT JOIN [sys].[indexes] [i] WITH (NOLOCk) ON [sp].[class] = 7 AND [so].[object_id] = [i].[object_id]  AND [sp].[minor_id] = [i].[index_id]
        LEFT JOIN [sys].[table_types] [tt] WITH (NOLOCK) ON [sp].[class] IN (6,8) AND [tt].[user_type_id] = [sp].[major_id]
        LEFT JOIN [sys].[xml_indexes] [sxi] WITH (NOLOCk) ON [sp].[class] = 7 AND [so].[object_id] = [sxi].[object_id]  AND [sp].[minor_id] = [sxi].[index_id]
WHERE 
        [sp].[class] <> 1
        AND (OBJECTPROPERTY([so].[parent_object_id], N'TableIsFake') = 0 OR OBJECTPROPERTY([so].[parent_object_id], N'TableIsFake')  IS NULL)
        AND [sp].[class] <> 27
        AND ([so].[type] <> N'PG' OR [so].[type] IS NULL)
        AND [sp].[name] <> N'microsoft_database_tools_support'
        AND ([i].[is_primary_key] IS NULL OR [i].[is_primary_key] = 0)
        AND ([sp].[class] <> 7 OR [i].[object_id] IS NOT NULL)
        AND ([sp].[class] <> 7 OR ([so].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [so].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )))
) AS [_results] WHERE [TypeId] = 24 AND [Level2Name] IS NULL AND [Level1Name] IS NULL
 ORDER BY Level0Name,Level1Name ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110ExtendedPropertyPopulator' as [PopulatorName];
SELECT * FROM (
SELECT 
        [sp].[class] AS [TypeId],
        [sp].[major_id] AS [MajorId],
        [sp].[minor_id] AS [MinorId],
        [sp].[name] AS [PropertyName], 
        [sp].[value] AS [PropertyValue],
        [so].[type] AS [ObjectType],
        SQL_VARIANT_PROPERTY([sp].[value], N'BaseType') AS [BaseType],
        CASE [sp].[class]
            WHEN  0 THEN NULL
            WHEN  3 THEN SCHEMA_NAME([sp].[major_id])
            WHEN  4 THEN USER_NAME([sp].[major_id])
            WHEN  5 THEN (SELECT TOP 1 [sa2].[name]   FROM [sys].[assemblies] [sa2] WITH (NOLOCK) WHERE [sa2].[assembly_id] = [sp].[major_id])
            WHEN  6 THEN SCHEMA_NAME((SELECT TOP 1 [st].[schema_id] FROM [sys].[types] [st] WITH (NOLOCK) WHERE [st].[user_type_id] = [sp].[major_id]))
            WHEN  8 THEN SCHEMA_NAME([tt].[schema_id])
            WHEN 10 THEN SCHEMA_NAME((SELECT TOP 1 [sxsc2].[schema_id] FROM [sys].[xml_schema_collections] [sxsc2] WITH (NOLOCK) WHERE [sxsc2].[xml_collection_id] = [sp].[major_id]))
            WHEN 15 THEN (SELECT TOP 1 [smt2].[name]  FROM [sys].[service_message_types] [smt2] WITH (NOLOCK) WHERE [smt2].[message_type_id] = [sp].[major_id]) COLLATE database_default
            WHEN 16 THEN (SELECT TOP 1 [ssc2].[name]  FROM [sys].[service_contracts] [ssc2] WITH (NOLOCK) WHERE [ssc2].[service_contract_id] = [sp].[major_id])
            WHEN 17 THEN (SELECT TOP 1 [ss2].[name]   FROM [sys].[services] [ss2] WITH (NOLOCK) WHERE [ss2].[service_id] = [sp].[major_id])
            WHEN 18 THEN (SELECT TOP 1 [srs2].[name]  FROM [sys].[remote_service_bindings] [srs2] WITH (NOLOCK) WHERE [srs2].[remote_service_binding_id] = [sp].[major_id])
            WHEN 19 THEN (SELECT TOP 1 [sr2].[name]   FROM [sys].[routes] [sr2] WITH (NOLOCK) WHERE [sr2].[route_id] = [sp].[major_id])
            WHEN 20 THEN (SELECT TOP 1 [sd2].[name]  FROM [sys].[data_spaces] [sd2] WITH (NOLOCK) WHERE [sd2].[data_space_id] = [sp].[major_id])
            WHEN 21 THEN (SELECT TOP 1 [spf2].[name]  FROM [sys].[partition_functions] [spf2] WITH (NOLOCK) WHERE [spf2].[function_id] = [sp].[major_id])
            WHEN 22 THEN (SELECT TOP 1 [sdf2].[name]  FROM [sys].[database_files] [sdf2] WITH (NOLOCK) WHERE [sdf2].[file_id] = [sp].[major_id])
            ELSE SCHEMA_NAME([so].[schema_id])
        END AS [Level0Name],
        CASE [sp].[class] 
            WHEN 3 THEN NULL
            WHEN 4 THEN NULL
            WHEN 6  THEN (SELECT TOP 1 [st2].[name] FROM [sys].[types] [st2] WITH (NOLOCK) WHERE [st2].[user_type_id] = [sp].[major_id])
            WHEN 8  THEN [tt].[name]
            WHEN 10 THEN (SELECT TOP 1 [sx2].[name] FROM [sys].[xml_schema_collections] [sx2] WITH (NOLOCK) WHERE [sx2].[xml_collection_id] = [sp].[major_id])
            WHEN 22 THEN NULL
            ELSE [so].[name]
        END AS [Level1Name],
        CASE [sp].[class]			
            WHEN 2 THEN (SELECT TOP 1 [spa].[name] FROM [sys].[parameters] [spa] WITH (NOLOCK) WHERE [spa].[object_id] = [sp].[major_id] AND [spa].[parameter_id] = [sp].[minor_id])
            WHEN 7 THEN (SELECT TOP 1 [si].[name] FROM [sys].[indexes] [si] WITH (NOLOCK) WHERE [si].[object_id] = [sp].[major_id] AND [si].[index_id] = [sp].[minor_id])
            WHEN 8 THEN COL_NAME([tt].[type_table_object_id] ,[sp].[minor_id])
            ELSE NULL
        END AS [Level2Name],
        CASE 
            WHEN [sp].[class] = 20 AND EXISTS((SELECT TOP 1 [sd2].[name]  FROM [sys].[data_spaces] [sd2] WITH (NOLOCK) WHERE [sd2].[data_space_id] = [sp].[major_id] AND [sd2].[type] = N'PS')) THEN
                CAST(1 AS BIT)
            ELSE CAST(0 AS BIT)
        END AS IsPartitionScheme,
        CAST(0 AS BIT) AS [IsDefault],
        [i].[type] AS [IndexType],
        CASE [sp].[class]
            WHEN 6 THEN [tt].[type_table_object_id]
            WHEN 8 THEN [tt].[type_table_object_id]
            WHEN 1 THEN [parent].[object_id]
            ELSE NULL
        END AS [ParentId],
         CASE [sp].[class]
            WHEN 6 THEN [tt].[schema_id]
            WHEN 8 THEN [tt].[schema_id]
            WHEN 1 THEN [parent].[schema_id]
            ELSE NULL
        END AS [ParentSchemaId],
        [sxi].[xml_index_type] AS [XMLIndexType],
        ISNULL([parent].[is_ms_shipped], 0) AS [IsMsShipped]
FROM    [sys].[extended_properties] [sp] WITH (NOLOCK) 
        LEFT JOIN [sys].[all_objects] [so] WITH (NOLOCK) ON [so].[object_id] = [sp].[major_id] AND [sp].[class] IN (1, 2, 7)
        LEFT JOIN [sys].[all_objects] [parent] WITH(NOLOCK) ON [so].[parent_object_id] = [parent].[object_id]
        LEFT JOIN [sys].[indexes] [i] WITH (NOLOCk) ON [sp].[class] = 7 AND [so].[object_id] = [i].[object_id]  AND [sp].[minor_id] = [i].[index_id]
        LEFT JOIN [sys].[table_types] [tt] WITH (NOLOCK) ON [sp].[class] IN (6,8) AND [tt].[user_type_id] = [sp].[major_id]
        LEFT JOIN [sys].[xml_indexes] [sxi] WITH (NOLOCk) ON [sp].[class] = 7 AND [so].[object_id] = [sxi].[object_id]  AND [sp].[minor_id] = [sxi].[index_id]
WHERE 
        [sp].[class] <> 1
        AND (OBJECTPROPERTY([so].[parent_object_id], N'TableIsFake') = 0 OR OBJECTPROPERTY([so].[parent_object_id], N'TableIsFake')  IS NULL)
        AND [sp].[class] <> 27
        AND ([so].[type] <> N'PG' OR [so].[type] IS NULL)
        AND [sp].[name] <> N'microsoft_database_tools_support'
        AND ([i].[is_primary_key] IS NULL OR [i].[is_primary_key] = 0)
        AND ([sp].[class] <> 7 OR [i].[object_id] IS NOT NULL)
        AND ([sp].[class] <> 7 OR ([so].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [so].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )))
) AS [_results] WHERE [TypeId] = 25 AND [Level2Name] IS NULL AND [Level1Name] IS NULL
 ORDER BY Level0Name,Level1Name ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110ExtendedPropertyPopulator' as [PopulatorName];
SELECT * FROM (
SELECT 
        [sp].[class] AS [TypeId],
        [sp].[major_id] AS [MajorId],
        [sp].[minor_id] AS [MinorId],
        [sp].[name] AS [PropertyName], 
        [sp].[value] AS [PropertyValue],
        [so].[type] AS [ObjectType],
        SQL_VARIANT_PROPERTY([sp].[value], N'BaseType') AS [BaseType],
        CASE [sp].[class]
            WHEN  0 THEN NULL
            WHEN  3 THEN SCHEMA_NAME([sp].[major_id])
            WHEN  4 THEN USER_NAME([sp].[major_id])
            WHEN  5 THEN (SELECT TOP 1 [sa2].[name]   FROM [sys].[assemblies] [sa2] WITH (NOLOCK) WHERE [sa2].[assembly_id] = [sp].[major_id])
            WHEN  6 THEN SCHEMA_NAME((SELECT TOP 1 [st].[schema_id] FROM [sys].[types] [st] WITH (NOLOCK) WHERE [st].[user_type_id] = [sp].[major_id]))
            WHEN  8 THEN SCHEMA_NAME([tt].[schema_id])
            WHEN 10 THEN SCHEMA_NAME((SELECT TOP 1 [sxsc2].[schema_id] FROM [sys].[xml_schema_collections] [sxsc2] WITH (NOLOCK) WHERE [sxsc2].[xml_collection_id] = [sp].[major_id]))
            WHEN 15 THEN (SELECT TOP 1 [smt2].[name]  FROM [sys].[service_message_types] [smt2] WITH (NOLOCK) WHERE [smt2].[message_type_id] = [sp].[major_id]) COLLATE database_default
            WHEN 16 THEN (SELECT TOP 1 [ssc2].[name]  FROM [sys].[service_contracts] [ssc2] WITH (NOLOCK) WHERE [ssc2].[service_contract_id] = [sp].[major_id])
            WHEN 17 THEN (SELECT TOP 1 [ss2].[name]   FROM [sys].[services] [ss2] WITH (NOLOCK) WHERE [ss2].[service_id] = [sp].[major_id])
            WHEN 18 THEN (SELECT TOP 1 [srs2].[name]  FROM [sys].[remote_service_bindings] [srs2] WITH (NOLOCK) WHERE [srs2].[remote_service_binding_id] = [sp].[major_id])
            WHEN 19 THEN (SELECT TOP 1 [sr2].[name]   FROM [sys].[routes] [sr2] WITH (NOLOCK) WHERE [sr2].[route_id] = [sp].[major_id])
            WHEN 20 THEN (SELECT TOP 1 [sd2].[name]  FROM [sys].[data_spaces] [sd2] WITH (NOLOCK) WHERE [sd2].[data_space_id] = [sp].[major_id])
            WHEN 21 THEN (SELECT TOP 1 [spf2].[name]  FROM [sys].[partition_functions] [spf2] WITH (NOLOCK) WHERE [spf2].[function_id] = [sp].[major_id])
            WHEN 22 THEN (SELECT TOP 1 [sdf2].[name]  FROM [sys].[database_files] [sdf2] WITH (NOLOCK) WHERE [sdf2].[file_id] = [sp].[major_id])
            ELSE SCHEMA_NAME([so].[schema_id])
        END AS [Level0Name],
        CASE [sp].[class] 
            WHEN 3 THEN NULL
            WHEN 4 THEN NULL
            WHEN 6  THEN (SELECT TOP 1 [st2].[name] FROM [sys].[types] [st2] WITH (NOLOCK) WHERE [st2].[user_type_id] = [sp].[major_id])
            WHEN 8  THEN [tt].[name]
            WHEN 10 THEN (SELECT TOP 1 [sx2].[name] FROM [sys].[xml_schema_collections] [sx2] WITH (NOLOCK) WHERE [sx2].[xml_collection_id] = [sp].[major_id])
            WHEN 22 THEN NULL
            ELSE [so].[name]
        END AS [Level1Name],
        CASE [sp].[class]			
            WHEN 2 THEN (SELECT TOP 1 [spa].[name] FROM [sys].[parameters] [spa] WITH (NOLOCK) WHERE [spa].[object_id] = [sp].[major_id] AND [spa].[parameter_id] = [sp].[minor_id])
            WHEN 7 THEN (SELECT TOP 1 [si].[name] FROM [sys].[indexes] [si] WITH (NOLOCK) WHERE [si].[object_id] = [sp].[major_id] AND [si].[index_id] = [sp].[minor_id])
            WHEN 8 THEN COL_NAME([tt].[type_table_object_id] ,[sp].[minor_id])
            ELSE NULL
        END AS [Level2Name],
        CASE 
            WHEN [sp].[class] = 20 AND EXISTS((SELECT TOP 1 [sd2].[name]  FROM [sys].[data_spaces] [sd2] WITH (NOLOCK) WHERE [sd2].[data_space_id] = [sp].[major_id] AND [sd2].[type] = N'PS')) THEN
                CAST(1 AS BIT)
            ELSE CAST(0 AS BIT)
        END AS IsPartitionScheme,
        CAST(0 AS BIT) AS [IsDefault],
        [i].[type] AS [IndexType],
        CASE [sp].[class]
            WHEN 6 THEN [tt].[type_table_object_id]
            WHEN 8 THEN [tt].[type_table_object_id]
            WHEN 1 THEN [parent].[object_id]
            ELSE NULL
        END AS [ParentId],
         CASE [sp].[class]
            WHEN 6 THEN [tt].[schema_id]
            WHEN 8 THEN [tt].[schema_id]
            WHEN 1 THEN [parent].[schema_id]
            ELSE NULL
        END AS [ParentSchemaId],
        [sxi].[xml_index_type] AS [XMLIndexType],
        ISNULL([parent].[is_ms_shipped], 0) AS [IsMsShipped]
FROM    [sys].[extended_properties] [sp] WITH (NOLOCK) 
        LEFT JOIN [sys].[all_objects] [so] WITH (NOLOCK) ON [so].[object_id] = [sp].[major_id] AND [sp].[class] IN (1, 2, 7)
        LEFT JOIN [sys].[all_objects] [parent] WITH(NOLOCK) ON [so].[parent_object_id] = [parent].[object_id]
        LEFT JOIN [sys].[indexes] [i] WITH (NOLOCk) ON [sp].[class] = 7 AND [so].[object_id] = [i].[object_id]  AND [sp].[minor_id] = [i].[index_id]
        LEFT JOIN [sys].[table_types] [tt] WITH (NOLOCK) ON [sp].[class] IN (6,8) AND [tt].[user_type_id] = [sp].[major_id]
        LEFT JOIN [sys].[xml_indexes] [sxi] WITH (NOLOCk) ON [sp].[class] = 7 AND [so].[object_id] = [sxi].[object_id]  AND [sp].[minor_id] = [sxi].[index_id]
WHERE 
        [sp].[class] <> 1
        AND (OBJECTPROPERTY([so].[parent_object_id], N'TableIsFake') = 0 OR OBJECTPROPERTY([so].[parent_object_id], N'TableIsFake')  IS NULL)
        AND [sp].[class] <> 27
        AND ([so].[type] <> N'PG' OR [so].[type] IS NULL)
        AND [sp].[name] <> N'microsoft_database_tools_support'
        AND ([i].[is_primary_key] IS NULL OR [i].[is_primary_key] = 0)
        AND ([sp].[class] <> 7 OR [i].[object_id] IS NOT NULL)
        AND ([sp].[class] <> 7 OR ([so].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [so].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )))
) AS [_results] WHERE [TypeId] = 26 AND [Level2Name] IS NULL AND [Level1Name] IS NULL
 ORDER BY Level0Name,Level1Name ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110ExtendedPropertyPopulator' as [PopulatorName];
SELECT * FROM (
SELECT 
        [sp].[class] AS [TypeId],
        [sp].[major_id] AS [MajorId],
        [sp].[minor_id] AS [MinorId],
        [sp].[name] AS [PropertyName], 
        [sp].[value] AS [PropertyValue],
        [so].[type] AS [ObjectType],
        SQL_VARIANT_PROPERTY([sp].[value], N'BaseType') AS [BaseType],
        CASE [sp].[class]
            WHEN  0 THEN NULL
            WHEN  3 THEN SCHEMA_NAME([sp].[major_id])
            WHEN  4 THEN USER_NAME([sp].[major_id])
            WHEN  5 THEN (SELECT TOP 1 [sa2].[name]   FROM [sys].[assemblies] [sa2] WITH (NOLOCK) WHERE [sa2].[assembly_id] = [sp].[major_id])
            WHEN  6 THEN SCHEMA_NAME((SELECT TOP 1 [st].[schema_id] FROM [sys].[types] [st] WITH (NOLOCK) WHERE [st].[user_type_id] = [sp].[major_id]))
            WHEN  8 THEN SCHEMA_NAME([tt].[schema_id])
            WHEN 10 THEN SCHEMA_NAME((SELECT TOP 1 [sxsc2].[schema_id] FROM [sys].[xml_schema_collections] [sxsc2] WITH (NOLOCK) WHERE [sxsc2].[xml_collection_id] = [sp].[major_id]))
            WHEN 15 THEN (SELECT TOP 1 [smt2].[name]  FROM [sys].[service_message_types] [smt2] WITH (NOLOCK) WHERE [smt2].[message_type_id] = [sp].[major_id]) COLLATE database_default
            WHEN 16 THEN (SELECT TOP 1 [ssc2].[name]  FROM [sys].[service_contracts] [ssc2] WITH (NOLOCK) WHERE [ssc2].[service_contract_id] = [sp].[major_id])
            WHEN 17 THEN (SELECT TOP 1 [ss2].[name]   FROM [sys].[services] [ss2] WITH (NOLOCK) WHERE [ss2].[service_id] = [sp].[major_id])
            WHEN 18 THEN (SELECT TOP 1 [srs2].[name]  FROM [sys].[remote_service_bindings] [srs2] WITH (NOLOCK) WHERE [srs2].[remote_service_binding_id] = [sp].[major_id])
            WHEN 19 THEN (SELECT TOP 1 [sr2].[name]   FROM [sys].[routes] [sr2] WITH (NOLOCK) WHERE [sr2].[route_id] = [sp].[major_id])
            WHEN 20 THEN (SELECT TOP 1 [sd2].[name]  FROM [sys].[data_spaces] [sd2] WITH (NOLOCK) WHERE [sd2].[data_space_id] = [sp].[major_id])
            WHEN 21 THEN (SELECT TOP 1 [spf2].[name]  FROM [sys].[partition_functions] [spf2] WITH (NOLOCK) WHERE [spf2].[function_id] = [sp].[major_id])
            WHEN 22 THEN (SELECT TOP 1 [sdf2].[name]  FROM [sys].[database_files] [sdf2] WITH (NOLOCK) WHERE [sdf2].[file_id] = [sp].[major_id])
            ELSE SCHEMA_NAME([so].[schema_id])
        END AS [Level0Name],
        CASE [sp].[class] 
            WHEN 3 THEN NULL
            WHEN 4 THEN NULL
            WHEN 6  THEN (SELECT TOP 1 [st2].[name] FROM [sys].[types] [st2] WITH (NOLOCK) WHERE [st2].[user_type_id] = [sp].[major_id])
            WHEN 8  THEN [tt].[name]
            WHEN 10 THEN (SELECT TOP 1 [sx2].[name] FROM [sys].[xml_schema_collections] [sx2] WITH (NOLOCK) WHERE [sx2].[xml_collection_id] = [sp].[major_id])
            WHEN 22 THEN NULL
            ELSE [so].[name]
        END AS [Level1Name],
        CASE [sp].[class]			
            WHEN 2 THEN (SELECT TOP 1 [spa].[name] FROM [sys].[parameters] [spa] WITH (NOLOCK) WHERE [spa].[object_id] = [sp].[major_id] AND [spa].[parameter_id] = [sp].[minor_id])
            WHEN 7 THEN (SELECT TOP 1 [si].[name] FROM [sys].[indexes] [si] WITH (NOLOCK) WHERE [si].[object_id] = [sp].[major_id] AND [si].[index_id] = [sp].[minor_id])
            WHEN 8 THEN COL_NAME([tt].[type_table_object_id] ,[sp].[minor_id])
            ELSE NULL
        END AS [Level2Name],
        CASE 
            WHEN [sp].[class] = 20 AND EXISTS((SELECT TOP 1 [sd2].[name]  FROM [sys].[data_spaces] [sd2] WITH (NOLOCK) WHERE [sd2].[data_space_id] = [sp].[major_id] AND [sd2].[type] = N'PS')) THEN
                CAST(1 AS BIT)
            ELSE CAST(0 AS BIT)
        END AS IsPartitionScheme,
        CAST(0 AS BIT) AS [IsDefault],
        [i].[type] AS [IndexType],
        CASE [sp].[class]
            WHEN 6 THEN [tt].[type_table_object_id]
            WHEN 8 THEN [tt].[type_table_object_id]
            WHEN 1 THEN [parent].[object_id]
            ELSE NULL
        END AS [ParentId],
         CASE [sp].[class]
            WHEN 6 THEN [tt].[schema_id]
            WHEN 8 THEN [tt].[schema_id]
            WHEN 1 THEN [parent].[schema_id]
            ELSE NULL
        END AS [ParentSchemaId],
        [sxi].[xml_index_type] AS [XMLIndexType],
        ISNULL([parent].[is_ms_shipped], 0) AS [IsMsShipped]
FROM    [sys].[extended_properties] [sp] WITH (NOLOCK) 
        LEFT JOIN [sys].[all_objects] [so] WITH (NOLOCK) ON [so].[object_id] = [sp].[major_id] AND [sp].[class] IN (1, 2, 7)
        LEFT JOIN [sys].[all_objects] [parent] WITH(NOLOCK) ON [so].[parent_object_id] = [parent].[object_id]
        LEFT JOIN [sys].[indexes] [i] WITH (NOLOCk) ON [sp].[class] = 7 AND [so].[object_id] = [i].[object_id]  AND [sp].[minor_id] = [i].[index_id]
        LEFT JOIN [sys].[table_types] [tt] WITH (NOLOCK) ON [sp].[class] IN (6,8) AND [tt].[user_type_id] = [sp].[major_id]
        LEFT JOIN [sys].[xml_indexes] [sxi] WITH (NOLOCk) ON [sp].[class] = 7 AND [so].[object_id] = [sxi].[object_id]  AND [sp].[minor_id] = [sxi].[index_id]
WHERE 
        [sp].[class] <> 1
        AND (OBJECTPROPERTY([so].[parent_object_id], N'TableIsFake') = 0 OR OBJECTPROPERTY([so].[parent_object_id], N'TableIsFake')  IS NULL)
        AND [sp].[class] <> 27
        AND ([so].[type] <> N'PG' OR [so].[type] IS NULL)
        AND [sp].[name] <> N'microsoft_database_tools_support'
        AND ([i].[is_primary_key] IS NULL OR [i].[is_primary_key] = 0)
        AND ([sp].[class] <> 7 OR [i].[object_id] IS NOT NULL)
        AND ([sp].[class] <> 7 OR ([so].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [so].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )))
) AS [_results] WHERE [TypeId] = 29 AND [Level2Name] IS NULL AND [Level1Name] IS NULL
 ORDER BY Level0Name,Level1Name ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110ExtendedPropertyPopulator' as [PopulatorName];
SELECT * FROM (
SELECT 
        [sp].[class] AS [TypeId],
        [sp].[major_id] AS [MajorId],
        [sp].[minor_id] AS [MinorId],
        [sp].[name] AS [PropertyName], 
        [sp].[value] AS [PropertyValue],
        [so].[type] AS [ObjectType],
        SQL_VARIANT_PROPERTY([sp].[value], N'BaseType') AS [BaseType],
        CASE [sp].[class]
            WHEN  0 THEN NULL
            WHEN  3 THEN SCHEMA_NAME([sp].[major_id])
            WHEN  4 THEN USER_NAME([sp].[major_id])
            WHEN  5 THEN (SELECT TOP 1 [sa2].[name]   FROM [sys].[assemblies] [sa2] WITH (NOLOCK) WHERE [sa2].[assembly_id] = [sp].[major_id])
            WHEN  6 THEN SCHEMA_NAME((SELECT TOP 1 [st].[schema_id] FROM [sys].[types] [st] WITH (NOLOCK) WHERE [st].[user_type_id] = [sp].[major_id]))
            WHEN  8 THEN SCHEMA_NAME([tt].[schema_id])
            WHEN 10 THEN SCHEMA_NAME((SELECT TOP 1 [sxsc2].[schema_id] FROM [sys].[xml_schema_collections] [sxsc2] WITH (NOLOCK) WHERE [sxsc2].[xml_collection_id] = [sp].[major_id]))
            WHEN 15 THEN (SELECT TOP 1 [smt2].[name]  FROM [sys].[service_message_types] [smt2] WITH (NOLOCK) WHERE [smt2].[message_type_id] = [sp].[major_id]) COLLATE database_default
            WHEN 16 THEN (SELECT TOP 1 [ssc2].[name]  FROM [sys].[service_contracts] [ssc2] WITH (NOLOCK) WHERE [ssc2].[service_contract_id] = [sp].[major_id])
            WHEN 17 THEN (SELECT TOP 1 [ss2].[name]   FROM [sys].[services] [ss2] WITH (NOLOCK) WHERE [ss2].[service_id] = [sp].[major_id])
            WHEN 18 THEN (SELECT TOP 1 [srs2].[name]  FROM [sys].[remote_service_bindings] [srs2] WITH (NOLOCK) WHERE [srs2].[remote_service_binding_id] = [sp].[major_id])
            WHEN 19 THEN (SELECT TOP 1 [sr2].[name]   FROM [sys].[routes] [sr2] WITH (NOLOCK) WHERE [sr2].[route_id] = [sp].[major_id])
            WHEN 20 THEN (SELECT TOP 1 [sd2].[name]  FROM [sys].[data_spaces] [sd2] WITH (NOLOCK) WHERE [sd2].[data_space_id] = [sp].[major_id])
            WHEN 21 THEN (SELECT TOP 1 [spf2].[name]  FROM [sys].[partition_functions] [spf2] WITH (NOLOCK) WHERE [spf2].[function_id] = [sp].[major_id])
            WHEN 22 THEN (SELECT TOP 1 [sdf2].[name]  FROM [sys].[database_files] [sdf2] WITH (NOLOCK) WHERE [sdf2].[file_id] = [sp].[major_id])
            ELSE SCHEMA_NAME([so].[schema_id])
        END AS [Level0Name],
        CASE [sp].[class] 
            WHEN 3 THEN NULL
            WHEN 4 THEN NULL
            WHEN 6  THEN (SELECT TOP 1 [st2].[name] FROM [sys].[types] [st2] WITH (NOLOCK) WHERE [st2].[user_type_id] = [sp].[major_id])
            WHEN 8  THEN [tt].[name]
            WHEN 10 THEN (SELECT TOP 1 [sx2].[name] FROM [sys].[xml_schema_collections] [sx2] WITH (NOLOCK) WHERE [sx2].[xml_collection_id] = [sp].[major_id])
            WHEN 22 THEN NULL
            ELSE [so].[name]
        END AS [Level1Name],
        CASE [sp].[class]			
            WHEN 2 THEN (SELECT TOP 1 [spa].[name] FROM [sys].[parameters] [spa] WITH (NOLOCK) WHERE [spa].[object_id] = [sp].[major_id] AND [spa].[parameter_id] = [sp].[minor_id])
            WHEN 7 THEN (SELECT TOP 1 [si].[name] FROM [sys].[indexes] [si] WITH (NOLOCK) WHERE [si].[object_id] = [sp].[major_id] AND [si].[index_id] = [sp].[minor_id])
            WHEN 8 THEN COL_NAME([tt].[type_table_object_id] ,[sp].[minor_id])
            ELSE NULL
        END AS [Level2Name],
        CASE 
            WHEN [sp].[class] = 20 AND EXISTS((SELECT TOP 1 [sd2].[name]  FROM [sys].[data_spaces] [sd2] WITH (NOLOCK) WHERE [sd2].[data_space_id] = [sp].[major_id] AND [sd2].[type] = N'PS')) THEN
                CAST(1 AS BIT)
            ELSE CAST(0 AS BIT)
        END AS IsPartitionScheme,
        CAST(0 AS BIT) AS [IsDefault],
        [i].[type] AS [IndexType],
        CASE [sp].[class]
            WHEN 6 THEN [tt].[type_table_object_id]
            WHEN 8 THEN [tt].[type_table_object_id]
            WHEN 1 THEN [parent].[object_id]
            ELSE NULL
        END AS [ParentId],
         CASE [sp].[class]
            WHEN 6 THEN [tt].[schema_id]
            WHEN 8 THEN [tt].[schema_id]
            WHEN 1 THEN [parent].[schema_id]
            ELSE NULL
        END AS [ParentSchemaId],
        [sxi].[xml_index_type] AS [XMLIndexType],
        ISNULL([parent].[is_ms_shipped], 0) AS [IsMsShipped]
FROM    [sys].[extended_properties] [sp] WITH (NOLOCK) 
        LEFT JOIN [sys].[all_objects] [so] WITH (NOLOCK) ON [so].[object_id] = [sp].[major_id] AND [sp].[class] IN (1, 2, 7)
        LEFT JOIN [sys].[all_objects] [parent] WITH(NOLOCK) ON [so].[parent_object_id] = [parent].[object_id]
        LEFT JOIN [sys].[indexes] [i] WITH (NOLOCk) ON [sp].[class] = 7 AND [so].[object_id] = [i].[object_id]  AND [sp].[minor_id] = [i].[index_id]
        LEFT JOIN [sys].[table_types] [tt] WITH (NOLOCK) ON [sp].[class] IN (6,8) AND [tt].[user_type_id] = [sp].[major_id]
        LEFT JOIN [sys].[xml_indexes] [sxi] WITH (NOLOCk) ON [sp].[class] = 7 AND [so].[object_id] = [sxi].[object_id]  AND [sp].[minor_id] = [sxi].[index_id]
WHERE 
        [sp].[class] <> 1
        AND (OBJECTPROPERTY([so].[parent_object_id], N'TableIsFake') = 0 OR OBJECTPROPERTY([so].[parent_object_id], N'TableIsFake')  IS NULL)
        AND [sp].[class] <> 27
        AND ([so].[type] <> N'PG' OR [so].[type] IS NULL)
        AND [sp].[name] <> N'microsoft_database_tools_support'
        AND ([i].[is_primary_key] IS NULL OR [i].[is_primary_key] = 0)
        AND ([sp].[class] <> 7 OR [i].[object_id] IS NOT NULL)
        AND ([sp].[class] <> 7 OR ([so].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [so].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )))
) AS [_results] WHERE [TypeId] = 31 AND [Level2Name] IS NULL AND [Level1Name] IS NULL
 ORDER BY Level0Name,Level1Name ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110ExtendedPropertyPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [sp].[class] AS [TypeId],
        [sp].[major_id] AS [MajorId],
        [sp].[minor_id] AS [MinorId],
        [sp].[name] AS [PropertyName],
        [sp].[value] AS [PropertyValue],
        CASE [t].[is_filetable] WHEN 1 THEN N'UF' ELSE [so].[type] END AS [ObjectType],
        SQL_VARIANT_PROPERTY([sp].[value], N'BaseType') AS [BaseType],
        SCHEMA_NAME([so].[schema_id]) AS [Level0Name],
        [so].[name] AS [Level1Name],
        [sc].[name] AS [Level2Name],
        CAST(0 AS BIT) AS [IsPartitionScheme],
        CAST(OBJECTPROPERTY([so].[object_id], N'IsDefault') AS BIT) AS [IsDefault],
        CAST(NULL AS TINYINT) AS [IndexType],
        [parent].[object_id] AS [ParentId],
        [parent].[schema_id] AS [ParentSchemaId],
        ISNULL([parent].[is_ms_shipped], 0) AS [IsMsShipped]
FROM
        [sys].[extended_properties] [sp] WITH (NOLOCK)
        INNER JOIN [sys].[all_objects] [so] WITH (NOLOCK) ON [so].[object_id] = [sp].[major_id]
        LEFT JOIN [sys].[all_objects] [parent] WITH (NOLOCK) ON [so].[parent_object_id] = [parent].[object_id]
        LEFT JOIN [sys].[tables] [t] WITH (NOLOCK) ON [so].[object_id] = [t].[object_id]
        LEFT JOIN [sys].[all_columns] [sc] WITH (NOLOCK) ON [sc].[object_id] = [sp].[major_id] AND [sc].[column_id] = [sp].[minor_id]
        LEFT JOIN [sys].[filetable_system_defined_objects] AS [ftc] WITH (NOLOCK) ON [ftc].[object_id] = [so].[object_id]

WHERE
        [sp].[class] = 1 AND  SCHEMA_NAME([so].[schema_id]) IS NOT NULL 
        AND ([so].[type] <> N'PG' OR [so].[type] IS NULL)
        AND [sp].[name] <> N'microsoft_database_tools_support'
        AND ([ftc].[object_id] IS NULL OR [so].[type] IN (N'UQ',N'PK'))
        AND ([so].[parent_object_id] = 0 AND ([so].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [so].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )) OR [so].[parent_object_id] <> 0 AND ([parent].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [parent].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )))

    AND (OBJECTPROPERTY([so].[parent_object_id], N'TableIsFake') = 0 OR OBJECTPROPERTY([so].[parent_object_id], N'TableIsFake')  IS NULL)) AS [_results] WHERE ( 1 = (select TOP 1 1 FROM @SystemTableTypeDiscriminators as [t] where [t].[TypeCode] COLLATE DATABASE_DEFAULT = [_results].[ObjectType])) AND [TypeId] = 1
 ORDER BY Level0Name,Level1Name ;";


        private static readonly string _query = @"
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql130DatabaseCredentialPopulator' as [PopulatorName];
SELECT * FROM (
SELECT	
        [dbc].[credential_id]       AS [CredentialId],
        [dbc].[name]                AS [CredentialName],
        [dbc].[credential_identity] AS [Identity]
FROM	
        [sys].[database_scoped_credentials] [dbc] WITH (NOLOCK) ) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql130DatabaseOptionsPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [db].[is_ansi_null_default_on]          AS [IsAnsiNullDefaultOn],
        [db].[is_ansi_nulls_on]                 AS [IsAnsiNullsOn],
        [db].[is_ansi_padding_on]               AS [IsAnsiPaddingOn],
        [db].[is_ansi_warnings_on]              AS [IsAnsiWarningsOn],
        [db].[is_arithabort_on]                 AS [IsArithAbortOn],
        [db].[is_auto_close_on]                 AS [IsAutoCloseOn],
        [db].[is_auto_create_stats_on]          AS [IsAutoCreateStatisticsOn],
        [db].[is_auto_shrink_on]                AS [IsAutoShrinkOn],
        [db].[is_auto_update_stats_on]          AS [IsAutoUpdateStatisticsOn],
        [db].[is_cursor_close_on_commit_on]     AS [IsCursorCloseOnCommitOn],
        [db].[is_fulltext_enabled]              AS [IsFullTextEnabled],
        [db].[is_in_standby]                    AS [IsInStandBy],
        [db].[is_local_cursor_default]          AS [IsLocalCursorsDefault],
        [db].[is_merge_published]               AS [IsMergePublished],
        [db].[is_concat_null_yields_null_on]    AS [IsConcatNullYieldsNullOn],
        [db].[is_numeric_roundabort_on]         AS [IsNumericRoundAbortOn],
        [db].[is_published]                     AS [IsPublished],
        [db].[is_quoted_identifier_on]          AS [IsQuotedIdentifierOn],
        [db].[is_recursive_triggers_on]         AS [IsRecursiveTriggersOn],
        [db].[is_subscribed]                    AS [IsSubscribed],
        CONVERT(bit, DATABASEPROPERTYEX([db].[name], N'IsTornPageDetectionEnabled'))    AS [IsTornPageProtectionOn],
        [db].[collation_name]                   AS [Collation],
        [db].[recovery_model_desc]              AS [RecoveryMode],
        DATABASEPROPERTYEX([db].[name], N'SQLSortOrder')   AS [SQLSortOrder],
        [db].[state_desc]                       AS [Status],
        DATABASEPROPERTYEX([db].[name], N'Updateability')  AS [Updateability],
        [db].[user_access_desc]                    AS [UserAccess],
        DATABASEPROPERTYEX([db].[name], N'Version')        AS [Version],
        [db].[is_db_chaining_on]                AS [IsDbChainingOn],
        [db].[is_trustworthy_on]                AS [IsTrustWorthyOn],
        [db].[is_auto_update_stats_async_on]    AS [IsAutoUpdateStatisticesAsyncOn],
        [db].[page_verify_option]               AS [PageVerifyOption],
        [db].[delayed_durability]               AS [DelayedDurabilityMode],
        [dbm].[mirroring_failover_lsn]          AS [MirrongFailOverLsn],
        [dbm].[mirroring_state]                 AS [MirroringState],
        [dbm].[mirroring_partner_name]          AS [MirroringPartnerServer],
        [dbm].[mirroring_safety_level]          AS [MirroringSafetyLevel],
        [dbm].[mirroring_redo_queue]            AS [MirroringRedoQueueSize],
        [dbm].[mirroring_redo_queue_type]       AS [MirroringRedoQueueType],
        [dbm].[mirroring_connection_timeout]    AS [MirroringPartnerServerTimeout],
        [dbm].[mirroring_witness_name]          AS [MirroringWitnessServer],
        [dbm].[mirroring_witness_state]         AS [MirroringWitnessState],
        [db].[is_supplemental_logging_enabled]  AS [IsSupplementalLoggingOn],
        [db].[is_broker_enabled]                AS [IsServiceBrokerEnabled],
        [db].[is_honor_broker_priority_on]      AS [IsBrokerPriorityHonored],
        [db].[is_date_correlation_on]           AS [IsDateCorrelationOptimizationOn],
        [db].[is_parameterization_forced]       AS [IsParameterizationForced],
        [db].[snapshot_isolation_state]         AS [SnapshotIsolationState],
        [db].[is_read_committed_snapshot_on]    AS [IsReadCommittedSnapshot],
        [db].[is_memory_optimized_elevate_to_snapshot_on]    AS [IsMemoryOptimizedElevatedToSnapshot],
        [db].[is_auto_create_stats_incremental_on]    AS [IsAutoCreateStatisticsIncrementalOn],
        [db].[compatibility_level]              AS [CompatibilityLevel],
        [db].[is_encrypted]                     AS [IsEncrypted],
        CAST(CASE
             WHEN [dbc].[database_id] IS NULL THEN 0
             ELSE 1
             END AS BIT)                        AS [IsChangeTrackingOn],
        CAST([dbc].[is_auto_cleanup_on] AS BIT) AS [IsChangeTrackingAutoCleanupOn],
        [dbc].[retention_period]                AS [ChangeTrackingRetentionPeriod],
        [dbc].[retention_period_units]          AS [ChangeTrackingRetentionPeriodUnits],
        CAST(CASE
             WHEN [db].[name] IN (N'master', N'tempdb', N'model', N'msdb') THEN 0
             ELSE 1
             END AS BIT)                        AS [IsVardecimalStorageFormatOn],
        [db].[containment]                      AS [Containment],
        [db].[default_language_lcid]            AS [DefaultLanguageId],
        [db].[default_fulltext_language_lcid]   AS [DefaultFulltextLanguageId],
        [db].[is_nested_triggers_on]            AS [IsNestedTriggersOn],
        [db].[is_transform_noise_words_on]      AS [IsTransformNoiseWordsOn],
        [db].[two_digit_year_cutoff]            AS [TwoDigitYearCutoff],
        CASE WHEN [fg].[is_default] = 1 THEN [fg].[name] ELSE NULL END AS [DefaultFileGroupName],
        CASE WHEN [fg].[is_default] = 1 THEN [fg].[data_space_id] ELSE -1 END AS [DefaultFileGroupId],
        CASE WHEN [fsfg].[is_default] = 1 THEN [fsfg].[name] ELSE NULL END AS [DefaultFileStreamFileGroupName],
        [db].[target_recovery_time_in_seconds] AS [TargetRecoveryTimeInSeconds],
        [fsop].[non_transacted_access] AS [NonTransactedAccess],
        [fsop].[directory_name] AS [FileStreamDirectoryName],
        [qds].[desired_state]				    AS [QueryStoreDesiredState],
        [qds].[actual_state]				    AS [QueryStoreActualState],
        [qds].[current_storage_size_mb]			AS [QueryStoreCurrentStorageSize],
        [qds].[flush_interval_seconds]			AS [QueryStoreFlushInterval],
        [qds].[interval_length_minutes]			AS [QueryStoreStatsInterval],
        [qds].[max_storage_size_mb]				AS [QueryStoreMaxStorageSize],
        [qds].[stale_query_threshold_days]		AS [QueryStoreStaleQueryThreshold],
        [qds].[max_plans_per_query]				AS [QueryStoreMaxPlansPerQuery],
        [qds].[query_capture_mode]			    AS [QueryStoreQueryCaptureMode],
        [dbscm].[value]                         AS [MaxDop],
        [dbscm].[value_for_secondary]           AS [MaxDopForSecondary],
        [dbscl].[value]                         AS [LegacyCardinalityEstimation],
        [dbscl].[value_for_secondary]           AS [LegacyCardinalityEstimationForSecondary],
        [dbscp].[value]                         AS [ParameterSniffing],
        [dbscp].[value_for_secondary]           AS [ParameterSniffingForSecondary],
        [dbscq].[value]                         AS [QueryOptimizerHotfixes],
        [dbscq].[value_for_secondary]           AS [QueryOptimizerHotfixesForSecondary],
        [db].[is_remote_data_archive_enabled]   AS [IsRemoteDataOn]
FROM [sys].[databases] [db] WITH (NOLOCK)
LEFT JOIN [sys].[database_mirroring] [dbm] WITH (NOLOCK) ON [dbm].[database_id] = [db].[database_id]
LEFT JOIN [sys].[change_tracking_databases] [dbc] WITH (NOLOCK) ON [dbc].[database_id] = [db].[database_id]
LEFT JOIN [sys].[filegroups] [fg] WITH (NOLOCK) ON [fg].[is_default] = 1 AND [fg].[type] = N'FG'
LEFT JOIN [sys].[filegroups] [fsfg] WITH (NOLOCK) ON [fsfg].[is_default] = 1 AND [fsfg].[type] = N'FD'
LEFT JOIN [sys].[database_filestream_options] AS [fsop] WITH (NOLOCK) ON [db].[database_id] = [fsop].[database_id]
LEFT JOIN [sys].[database_query_store_options] [qds] WITH (NOLOCK) ON 1 = 1
LEFT JOIN [sys].[database_scoped_configurations] AS [dbscm] WITH (NOLOCK) ON [dbscm].[name] = N'MAXDOP'
LEFT JOIN [sys].[database_scoped_configurations] AS [dbscl] WITH (NOLOCK) ON [dbscl].[name] = N'LEGACY_CARDINALITY_ESTIMATION'
LEFT JOIN [sys].[database_scoped_configurations] AS [dbscp] WITH (NOLOCK) ON [dbscp].[name] = N'PARAMETER_SNIFFING'
LEFT JOIN [sys].[database_scoped_configurations] AS [dbscq] WITH (NOLOCK) ON [dbscq].[name] = N'QUERY_OPTIMIZER_HOTFIXES'
WHERE [db].[name] = DB_NAME()) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110UserPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [dp].[principal_id]          AS [ObjectId],
        [dp].[name]                  AS [Name],
        [dp].[type]                  AS [Type],
        SUSER_SNAME([dp].[sid])      AS [LoginName],
        [cert].[certificate_id]      AS [CertificateId],
        [cert].[name]                AS [CertificateName],
        [ak].[asymmetric_key_id]     AS [AsymmetricKeyId],
        [ak].[name]                  AS [AsymmetricKeyName],
        [dp].[default_schema_name]   AS [DefaultSchemaName],
        [dp].[authentication_type]   AS [AuthenticationType],
        [dp].[default_language_name] AS [DefaultLanguage]
FROM    [sys].[database_principals] AS [dp] WITH (NOLOCK)
LEFT    JOIN [sys].[certificates] AS [cert] WITH (NOLOCK) ON [cert].[sid] = [dp].[sid]
LEFT    JOIN [sys].[asymmetric_keys] AS [ak] WITH (NOLOCK) ON [ak].[sid] = [dp].[sid]
WHERE   CHARINDEX([dp].[type], N'USGCK') > 0
AND     [dp].[name] != N'cdc') AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90RolePopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [dp].[principal_id]                   AS [ObjectId],
        [dp].[name]                           AS [Name],
        [dp].[owning_principal_id]            AS [OwnerId],
        USER_NAME([dp].[owning_principal_id]) AS [OwnerName]
FROM    [sys].[database_principals] AS [dp] WITH (NOLOCK)
WHERE   [dp].[type] = N'R'
AND     USER_NAME([dp].[owning_principal_id]) != N'cdc'
) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90ApplicationRolePopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [dp].[principal_id]        AS [ObjectId],
        [s].[schema_id]            AS [SchemaId],
        [dp].[name]                AS [Name],
        [dp].[default_schema_name] AS [DefaultSchemaName]
FROM    [sys].[database_principals] AS [dp] WITH (NOLOCK)
        LEFT JOIN [sys].[schemas] [s] WITH (NOLOCK) ON [s].[name] = [dp].[default_schema_name]
WHERE   [dp].[type] = N'A'
) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.SqlRoleMembershipPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [dp1].[principal_id]          AS [MemberId],
        [dp1].[name]                  AS [MemberName],
        [dp2].[principal_id]          AS [RoleId],
        [dp2].[name]                  AS [RoleName]
FROM
        [sys].[database_role_members] [drm] WITH (NOLOCK)
        INNER JOIN [sys].[database_principals] [dp1] WITH (NOLOCK) ON [dp1].[principal_id] = [drm].[member_principal_id]
        INNER JOIN [sys].[database_principals] [dp2] WITH (NOLOCK) ON [dp2].[principal_id] = [drm].[role_principal_id]
WHERE USER_NAME([drm].[member_principal_id]) != N'cdc'
) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql120FilegroupPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  
        [data_space_id]     AS [FilegroupId],
        [name]              AS [Name],
        [is_default]        AS [IsDefault],
        [is_read_only]      AS [IsReadOnly],
        [type]              AS [Type]
FROM    
        [sys].[filegroups] WITH (NOLOCK)
) AS [_results] ORDER BY FilegroupId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90FilePopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [sdf].[name]          AS [Name],
        [sdf].[physical_name] AS [FileName],
        [sdf].[data_space_id] AS [FilegroupId],
        [sfg].[name]          AS [FilegroupName],
        [sdf].[size]          AS [Size],
        [sdf].[max_size]      AS [MaxSize],
        [sdf].[growth]        AS [FileGrowth],
        CONVERT(bit, CASE [sdf].[max_size] WHEN -1 THEN 1 ELSE 0 END) AS [IsUnlimited],
        CONVERT(bit, CASE [sdf].[type] WHEN 1 THEN 1 ELSE 0 END)      AS [IsLogFile],
        [sdf].[is_percent_growth]                                     AS [IsPercentGrowth],
        [sdf].[state]                                                 AS [State]
FROM    [sys].[database_files]    AS [sdf] WITH (NOLOCK)
LEFT    OUTER JOIN sys.filegroups AS [sfg] WITH (NOLOCK) ON [sdf].[data_space_id] = [sfg].[data_space_id]
WHERE   [sdf].[type] <>  4
) AS [_results] ORDER BY FilegroupId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90FullTextCatalogPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [sfc].[fulltext_catalog_id] AS [ObjectId],
        [sfc].[name] AS [Name],
        [sfc].[principal_id] AS [OwnerId],
        [sdp].[name] AS [Owner],
        [sfc].[path] AS [Path],
        [sfc].[is_default] AS [IsDefault],
        [sfc].[is_accent_sensitivity_on] AS [IsAccentSensitivityOn],
        [sfc].[data_space_id] AS [FilegroupId],
        [sds].[name] AS [FilegroupName]
FROM    [sys].[fulltext_catalogs] [sfc] WITH (NOLOCK)
LEFT    JOIN [sys].[database_principals] [sdp] WITH (NOLOCK) ON [sdp].[principal_id] = [sfc].[principal_id]
LEFT    JOIN [sys].[data_spaces] [sds] WITH (NOLOCK) ON [sds].[data_space_id] = [sfc].[data_space_id]
) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90AssemblyPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [sdp].[principal_id]   AS [OwnerId],
        [sdp].[name]           AS [OwnerName], 
        [sas].[name]           AS [Name], 
        [sas].[assembly_id]    AS [ObjectId], 
        [sas].[is_visible]     AS [IsVisible], 
        [sas].[permission_set] AS [PermissionSet], 
        [sas].[clr_name]       AS [AssemblyFullName],
        [saf].[content]       AS [AssemblyBits]
FROM 
        [sys].[assemblies] AS [sas] WITH (NOLOCK)
LEFT    JOIN [sys].[database_principals] [sdp] WITH (NOLOCK) ON [sdp].[principal_id] = [sas].[principal_id]
LEFT	JOIN [sys].[assembly_files] AS [saf] WITH (NOLOCK) ON [saf].[assembly_id] = [sas].[assembly_id]
WHERE	[saf].[file_id] = 1
    AND [sas].[is_user_defined] = 1
) AS [_results] ORDER BY ObjectId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90CertificatePopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [sct].[certificate_id]          AS [ObjectId],
        [sct].[name]                    AS [Name],
        [sdp].[name]                    AS [Owner],
        [sdp].[principal_id]            AS [OwnerId],
        [sct].[pvt_key_encryption_type] AS [KeyEncryptionType],
        [sct].[cert_serial_number]      AS [SerialNumber],
        [sct].[issuer_name]             AS [Issuer],
        [sct].[subject]                 AS [Subject],
        [sct].[start_date]              AS [StartDate],
        [sct].[expiry_date]             AS [ExpiryDate],
        [sct].[thumbprint]              AS [ThumbPrint],
        CONVERT(bit, [sct].[is_active_for_begin_dialog]) AS [IsActiveForBeginDialog]
FROM
        [sys].[certificates] [sct] WITH (NOLOCK)
LEFT    JOIN [sys].[database_principals] [sdp] WITH (NOLOCK) ON [sdp].[principal_id] = [sct].[principal_id]
WHERE
        [sct].[name] NOT LIKE '##%##'
) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql100AsymmetricKeyPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [sak].[name]					AS [KeyName],
        [sak].[asymmetric_key_id]		AS [KeyId],
        [sdp].[name]					AS [Owner],
        [sdp].[principal_id]			AS [OwnerId],
        [sak].[thumbprint]				AS [Thumbprint],
        [sak].[algorithm]				AS [EncryptionAlgorithm],
        [sak].[pvt_key_encryption_type] AS [EncryptionType],
        [sak].[key_length]				AS [KeyLength],
        CAST(CASE WHEN [sak].[provider_type] = N'CRYPTOGRAPHIC PROVIDER' THEN 1
                  ELSE 0
             END
        AS BIT)							AS [UseCryptoProvider],
        [cpr].[provider_id]				AS [CryptoProviderId],
        [cpr].[name]					AS [CryptoProviderName]
FROM    [sys].[asymmetric_keys] [sak] WITH (NOLOCK)
LEFT    JOIN [sys].[database_principals] [sdp] WITH (NOLOCK) ON [sdp].[principal_id] = [sak].[principal_id]
LEFT	JOIN [sys].[cryptographic_providers] [cpr] WITH (NOLOCK) ON [cpr].[guid] = [sak].[cryptographic_provider_guid]
) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql100SymmetricKeyPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [ssk].[name] AS [KeyName],
        [ssk].[symmetric_key_id] AS [KeyId],
        [sdp].[name] AS [Owner],
        [sdp].[principal_id] AS [OwnerId],
        [ssk].[key_thumbprint] AS [ThumbPrint],
        [ssk].[key_length] AS [KeyLength],
        [ssk].[key_algorithm] AS [KeyAlgorithm],
		(CASE WHEN EXISTS (SELECT 1 FROM [sys].[key_encryptions] [ske] WITH (NOLOCK) WHERE 
		    [ske].[key_id] = [ssk].[symmetric_key_id] AND [ske].[crypt_type] = 'ESKP') THEN 'ESKP' ELSE NULL END) AS [EncryptionType],
        CAST(CASE WHEN [ssk].[provider_type] = N'CRYPTOGRAPHIC PROVIDER' THEN 1
                  ELSE 0
             END
        AS BIT)							AS [UseCryptoProvider],
        [cpr].[provider_id]				AS [CryptoProviderId],
        [cpr].[name]					AS [CryptoProviderName]
FROM    [sys].[symmetric_keys] [ssk] WITH (NOLOCK)
LEFT    JOIN [sys].[database_principals] [sdp] WITH (NOLOCK) ON [sdp].[principal_id] = [ssk].[principal_id]
LEFT	JOIN [sys].[cryptographic_providers] [cpr] WITH (NOLOCK) ON [cpr].[guid] = [ssk].[cryptographic_provider_guid] ) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90SymmetricKeyEncryptionMechanismPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [encrypt].[key_id] AS [KeyId],
        [encrypt].[crypt_type] AS [EncryptionType],
        [ensk].[symmetric_key_id] AS [EncryptionKeyId],
        [ensk].[name] AS [EncryptionKeyName],
        [cert].[certificate_id] AS [EncryptionCertId],
        [cert].[name] AS [EncryptionCertName],
        [enask].[asymmetric_key_id] AS [EncryptionAKeyId],
        [enask].[name] AS [EncryptionAKeyName],
        [key].[name] AS [SymmetricKeyName]
FROM    [sys].[key_encryptions] [encrypt] WITH (NOLOCK)
INNER   JOIN [sys].[symmetric_keys] [key] WITH (NOLOCK) ON [key].[symmetric_key_id] = [encrypt].[key_id]
LEFT    JOIN [sys].[symmetric_keys] [ensk] WITH (NOLOCK) ON [ensk].[key_guid] = [encrypt].[thumbprint]
LEFT    JOIN [sys].[certificates] [cert] WITH (NOLOCK) ON [cert].[thumbprint] = [encrypt].[thumbprint]
LEFT    JOIN [sys].[asymmetric_keys] [enask] WITH (NOLOCK) ON [enask].[thumbprint] = [encrypt].[thumbprint]
) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql100DatabaseEncryptionKeyPopulator' as [PopulatorName];

SELECT
        [dek].[key_algorithm]            AS [Algorithm],
        [dek].[key_length]               AS [KeyLength],
        [c].[certificate_id]             AS [CertificateId],
        [c].[name]                       AS [CertificateName],
        [ak].[asymmetric_key_id]         AS [AsymmetricKeyId],
        [ak].[name]                      AS [AsymmetricKeyName]
FROM
        [sys].[dm_database_encryption_keys] [dek] WITH (NOLOCK)
        LEFT JOIN [master].[sys].[certificates] [c] WITH (NOLOCK) ON [dek].[encryptor_thumbprint] = [c].[thumbprint]
        LEFT JOIN [master].[sys].[asymmetric_keys] [ak] WITH (NOLOCK) ON [dek].[encryptor_thumbprint] = [ak].[thumbprint]
WHERE
        DB_ID() = [dek].[database_id]
;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90SchemaPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [ssc].[schema_id]    AS [SchemaId],
        [ssc].[name]         AS [Name], 
        [ssc].[principal_id] AS [AuthorizerId],
        [sdp].[name]         AS [AuthorizerName] 
FROM    [sys].[schemas] [ssc] WITH (NOLOCK)
LEFT    JOIN [sys].[database_principals] [sdp] WITH (NOLOCK) ON [sdp].[principal_id] = [ssc].[principal_id]
WHERE   [ssc].[name] != N'cdc'
) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90XmlSchemaCollectionPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [xsc].[xml_collection_id]      AS [ObjectId],
        [xsc].[schema_id]              AS [SchemaId],
        SCHEMA_NAME([xsc].[schema_id]) AS [SchemaName],
        [xsc].[name]                   AS [Name],
        XML_SCHEMA_NAMESPACE(SCHEMA_NAME([xsc].[schema_id]), [xsc].[name]) AS [Document]
FROM    [sys].[xml_schema_collections] [xsc] WITH (NOLOCK)
WHERE   xsc.name <> N'sys'
) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90UserDefinedDataTypePopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [st].[user_type_id]             AS [TypeId],
        [st].[name]                     AS [Name],
        [st].[schema_id]                AS [OwnerId],
        SCHEMA_NAME([st].[schema_id])   AS [Owner],
        [st].[is_nullable]              AS [AllowNulls],
        [bt].[user_type_id]             AS [BaseTypeId],
        [bt].[name]                     AS [BaseType],
        CASE WHEN [st].[max_length] >= 0 AND [bt].[name] IN (N'nchar', N'nvarchar') 
             THEN [st].[max_length] / 2 
             ELSE [st].[max_length] 
        END                             AS [Length],
        [st].[precision]                AS [Precision],
        [st].[scale]                    AS [Scale],
        [st].[collation_name]           AS [CollationName]
FROM    [sys].[types] [st] WITH (NOLOCK)
LEFT    JOIN [sys].[database_principals] [dp] WITH (NOLOCK) ON [dp].[principal_id]   = [st].[principal_id]
LEFT    JOIN [sys].[types]               [bt] WITH (NOLOCK) ON [st].[system_type_id] = [bt].[system_type_id] AND [bt].[system_type_id] = [bt].[user_type_id]
WHERE   [st].[is_user_defined] = 1 AND [st].[is_assembly_type] = 0 AND [st].[is_table_type] = 0
) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90UserDefinedTypePopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [st].[user_type_id]             AS [TypeId],
        [st].[name]                     AS [Name],
        [st].[schema_id]                AS [OwnerId],
        SCHEMA_NAME([st].[schema_id])   AS [Owner],
        [at].[assembly_id]              AS [AssemblyId],
        [as].[name]                     AS [Assembly],
        [at].[assembly_class]           AS [AssemblyClass]
FROM    [sys].[types] [st] WITH (NOLOCK)
LEFT    JOIN [sys].[database_principals] [dp] WITH (NOLOCK) ON [dp].[principal_id]   = [st].[principal_id]
LEFT    JOIN [sys].[types]               [bt] WITH (NOLOCK) ON [st].[system_type_id] = [bt].[system_type_id] AND [bt].[system_type_id] = [bt].[user_type_id]
LEFT    JOIN [sys].[objects]             [ds] WITH (NOLOCK) ON [ds].[object_id]      = [st].[default_object_id]
LEFT    JOIN [sys].[assembly_types]      [at] WITH (NOLOCK) ON [at].[user_type_id]   = [st].[user_type_id]
LEFT    JOIN [sys].[assemblies]          [as] WITH (NOLOCK) ON [as].[assembly_id]    = [at].[assembly_id]
WHERE   [st].[is_user_defined] = 1 AND [st].[is_assembly_type] = 1
) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql120TableTypePopulator' as [PopulatorName];
SELECT * FROM (
SELECT  
        [tt].[name]                   AS [Name],
        [tt].[schema_id]              AS [OwnerId],
        SCHEMA_NAME([tt].[schema_id]) AS [OwnerName],
        [tt].[user_type_id]           AS [TableTypeId],
        [o].[create_date]             AS [CreatedDate],
        [tt].[is_memory_optimized]    AS [IsMemoryOptimized]
FROM    
        [sys].[table_types] [tt] WITH (NOLOCK)
        INNER JOIN [sys].[objects] as [o] WITH (NOLOCK) ON [tt].[type_table_object_id] = [o].[object_id]
WHERE   
        [tt].[is_user_defined] = 1
) AS [_results] ORDER BY TableTypeId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql100TableColumnTypePopulator' as [PopulatorName];
SELECT * FROM (
SELECT  
        [t].[name] AS [ColumnSourceName], 
        [t].[user_type_id] AS [TableId],
        SCHEMA_NAME([t].[schema_id]) AS [SchemaName], 
        [c].[name] AS [ColumnName], 
        [c].[user_type_id] AS [TypeId],
        [types].[name] AS [TypeName],
        [basetypes].[name] AS [BaseTypeName],
        CONVERT(bit, ISNULL([types].[is_user_defined], 0)) AS [IsUserDefinedType],
        SCHEMA_NAME([types].[schema_id]) AS [TypeSchemaName],
        [c].[column_id] AS [ColumnId], 
        [c].[precision] AS [Precision],
        [c].[scale] AS [Scale],
        CASE WHEN [c].[max_length] >= 0 AND [types].[name] IN (N'nchar', N'nvarchar') THEN ([c].[max_length] / 2) ELSE [c].[max_length] END AS [Length],
        CONVERT(bit, [c].[is_identity]) AS [IsIdentity],
        CONVERT(bit, [c].[is_computed]) AS [IsComputed],
        CAST(ISNULL([ic].[seed_value], 0) AS DECIMAL(38)) AS [IdentitySeed],
        CAST(ISNULL([ic].[increment_value], 0) AS DECIMAL(38)) AS [IdentityIncrement],
        CONVERT(bit, [c].[is_nullable]) AS [IsNullable],
        [cc].[definition] AS [ComputedText],
        [c].[is_rowguidcol] AS [IsRowGuidColumn],
        [c].[collation_name] AS [Collation],
        [c].[is_xml_document] AS [IsXmlDocument],
        [c].[xml_collection_id] AS [XmlCollectionId],
        [xscs].[name] AS [XmlCollection],
        SCHEMA_NAME([xscs].[schema_id]) AS [XmlCollectionSchemaName],
        CONVERT(bit, ISNULL([cc].[is_persisted], 0)) AS [IsPersisted],
        CAST(0 AS BIT) AS [IsPrimaryKey]
FROM    
        [sys].[columns] [c] WITH (NOLOCK)
        INNER JOIN [sys].[table_types] [t] WITH (NOLOCK) ON [c].[object_id] = [t].[type_table_object_id]
        LEFT JOIN [sys].[types] [basetypes] WITH (NOLOCK) ON [c].[system_type_id] = [basetypes].[system_type_id] AND [basetypes].[system_type_id] = [basetypes].[user_type_id]
        LEFT JOIN [sys].[types] [types] WITH (NOLOCK) ON [c].[user_type_id] = [types].[user_type_id]
        LEFT JOIN [sys].[identity_columns] [ic] WITH (NOLOCK) ON [ic].[object_id] = [c].[object_id] AND [ic].[column_id] = [c].[column_id]
        LEFT JOIN [sys].[computed_columns] [cc] WITH (NOLOCK) ON [cc].[object_id] = [c].[object_id] AND [cc].[column_id] = [c].[column_id]
        LEFT JOIN [sys].[xml_schema_collections] [xscs] WITH (NOLOCK) ON [xscs].[xml_collection_id] = [c].[xml_collection_id]        
WHERE   
        [t].[is_user_defined] = 1
) AS [_results] ORDER BY TableId,ColumnId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql130TableTypeUniqueKeyPopulator' as [PopulatorName];
SELECT * FROM (
SELECT DISTINCT
            SCHEMA_NAME([tt].[schema_id]) AS [SchemaName]
           ,[tt].[user_type_id]        AS [ColumnSourceId]
           ,[tt].[name]             AS [ColumnSourceName]
           ,[i].[index_id]         AS [ConstraintId]
           ,CONVERT(bit, CASE [i].[type] WHEN 1 THEN 1 ELSE 0 END) 
                                   AS [IsClustered]
           ,[i].[ignore_dup_key]   AS [DoIgnoreDuplicateKey]   
           ,[i].[name]             AS [Name]
           ,CONVERT(BIT, CASE WHEN [hi].[object_id] IS NULL THEN 0 ELSE 1 END) AS [IsHash]
           ,[hi].[bucket_count] AS [BucketCount]
FROM 
            [sys].[indexes] AS [i] WITH (NOLOCK)
            INNER JOIN [sys].[objects] AS [o]  WITH (NOLOCK) ON [i].[object_id] = [o].[object_id]
            INNER JOIN [sys].[table_types] AS [tt] WITH (NOLOCK) ON [o].[object_id] = [tt].[type_table_object_id]
            LEFT OUTER JOIN [sys].[hash_indexes] AS [hi] WITH (NOLOCK) ON [hi].[object_id] = [i].[object_id] AND [hi].[index_id] = [i].[index_id]
WHERE 
            [o].[type] = N'TT'
            AND OBJECTPROPERTY([o].[object_id], N'IsSystemTable') = 0 
            AND [i].[name] IS NOT NULL
            AND [i].[is_hypothetical] = 0
            AND [i].[is_primary_key] = 0
            AND [i].[is_unique_constraint] = 1
            AND	[o].[object_id] NOT IN (SELECT [major_id] FROM [sys].[extended_properties] WITH (NOLOCK) WHERE [minor_id] = 0 AND [class] = 1 AND [name] = N'microsoft_database_tools_support')  
) AS [_results] ORDER BY ColumnSourceId,ConstraintId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql100TableTypeConstraintColumnPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  
    SCHEMA_NAME([tt].[schema_id])  AS [SchemaName]
   ,[tt].[user_type_id]           AS [ColumnSourceId]
   ,[tt].[name]               AS [ColumnSourceName]
   ,[i].[index_id]           AS [IndexId]
   ,[i].[name]                AS [IndexName]
   ,[c].[column_id]           AS [ColumnId]
   ,[c].[name]                AS [ColumnName]
   ,[ic].[is_included_column] AS [IsIncludedColumn]
   ,[ic].[key_ordinal]        AS [KeyOrdinal]
   ,[ic].[is_descending_key]  AS [IsDescending]
FROM 
    [sys].[columns] AS [c] WITH (NOLOCK)
    LEFT JOIN [sys].[index_columns] AS [ic] WITH (NOLOCK) ON [c].[object_id] = [ic].[object_id] AND [c].[column_id] = [ic].[column_id]
    LEFT JOIN [sys].[indexes]       AS [i] WITH (NOLOCK) ON [i].[object_id] = [ic].[object_id] AND [i].[index_id] = [ic].[index_id]
    LEFT JOIN [sys].[objects]       AS [o] WITH (NOLOCK) ON [o].[object_id] = [i].[object_id]
    LEFT JOIN [sys].[table_types]  AS [tt] WITH (NOLOCK) ON [o].[object_id] = [tt].[type_table_object_id]
WHERE 
    [o].[type] = N'TT' 
    AND OBJECTPROPERTY([o].[object_id], N'IsSystemTable') = 0 
    AND [i].[name] IS NOT NULL
    AND [i].[is_hypothetical] = 0
    AND [i].[is_primary_key] = 0
    AND [i].[is_unique_constraint] = 1
    AND	[o].[object_id] NOT IN (SELECT [major_id] FROM [sys].[extended_properties] WITH (NOLOCK) WHERE [minor_id] = 0 AND [class] = 1 AND [name] = N'microsoft_database_tools_support')
) AS [_results] ORDER BY ColumnSourceId,IndexId,KeyOrdinal ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql120TableTypePrimaryKeyPopulator' as [PopulatorName];
SELECT * FROM (
SELECT DISTINCT
    SCHEMA_NAME([tt].[schema_id]) AS [SchemaName]
   ,[tt].[user_type_id]        AS [ColumnSourceId]
   ,[tt].[name]             AS [ColumnSourceName]
   ,[i].[index_id]         AS [ConstraintId]
   ,CONVERT(bit, CASE [i].[type] WHEN 1 THEN 1 ELSE 0 END) 
                           AS [IsClustered]
   ,[i].[ignore_dup_key]   AS [DoIgnoreDuplicateKey]   
   ,[i].[name]             AS [Name]
   ,CONVERT(BIT, CASE WHEN [hi].[object_id] IS NULL THEN 0 ELSE 1 END) AS [IsHash]
   ,[hi].[bucket_count] AS [BucketCount]
FROM 
    [sys].[indexes] AS [i] WITH (NOLOCK)
    INNER JOIN [sys].[objects] AS [o]  WITH (NOLOCK) ON [i].[object_id] = [o].[object_id]
    INNER JOIN [sys].[table_types] AS [tt] WITH (NOLOCK) ON [o].[object_id] = [tt].[type_table_object_id]
    LEFT OUTER JOIN [sys].[hash_indexes] AS [hi] WITH (NOLOCK) ON [hi].[object_id] = [i].[object_id] AND [hi].[index_id] = [i].[index_id]
WHERE 
    [o].[type] = N'TT'
    AND OBJECTPROPERTY([o].[object_id], N'IsSystemTable') = 0 
    AND [i].[name] IS NOT NULL
    AND [i].[is_hypothetical] = 0
    AND [i].[is_primary_key] = 1
    AND	[o].[object_id] NOT IN (SELECT [major_id] FROM [sys].[extended_properties] WITH (NOLOCK) WHERE [minor_id] = 0 AND [class] = 1 AND [name] = N'microsoft_database_tools_support')  
) AS [_results] ORDER BY ColumnSourceId,ConstraintId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql100TableTypeConstraintColumnPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  
    SCHEMA_NAME([tt].[schema_id])  AS [SchemaName]
   ,[tt].[user_type_id]           AS [ColumnSourceId]
   ,[tt].[name]               AS [ColumnSourceName]
   ,[i].[index_id]           AS [IndexId]
   ,[i].[name]                AS [IndexName]
   ,[c].[column_id]           AS [ColumnId]
   ,[c].[name]                AS [ColumnName]
   ,[ic].[is_included_column] AS [IsIncludedColumn]
   ,[ic].[key_ordinal]        AS [KeyOrdinal]
   ,[ic].[is_descending_key]  AS [IsDescending]
FROM 
    [sys].[columns] AS [c] WITH (NOLOCK)
    LEFT JOIN [sys].[index_columns] AS [ic] WITH (NOLOCK) ON [c].[object_id] = [ic].[object_id] AND [c].[column_id] = [ic].[column_id]
    LEFT JOIN [sys].[indexes]       AS [i] WITH (NOLOCK) ON [i].[object_id] = [ic].[object_id] AND [i].[index_id] = [ic].[index_id]
    LEFT JOIN [sys].[objects]       AS [o] WITH (NOLOCK) ON [o].[object_id] = [i].[object_id]
    LEFT JOIN [sys].[table_types]  AS [tt] WITH (NOLOCK) ON [o].[object_id] = [tt].[type_table_object_id]
WHERE 
    [o].[type] = N'TT' 
    AND OBJECTPROPERTY([o].[object_id], N'IsSystemTable') = 0 
    AND [i].[name] IS NOT NULL
    AND [i].[is_hypothetical] = 0
    AND [i].[is_primary_key] = 1
    AND	[o].[object_id] NOT IN (SELECT [major_id] FROM [sys].[extended_properties] WITH (NOLOCK) WHERE [minor_id] = 0 AND [class] = 1 AND [name] = N'microsoft_database_tools_support')
) AS [_results] ORDER BY ColumnSourceId,IndexId,KeyOrdinal ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql130TableTypeIndexPopulator' as [PopulatorName];
SELECT * FROM (SELECT DISTINCT
    SCHEMA_NAME([tt].[schema_id]) AS [SchemaName]
   ,[tt].[user_type_id]        AS [ColumnSourceId]
   ,[tt].[name]             AS [ColumnSourceName]
   ,'TT'  AS [ColumnSourceType]
   ,[i].[index_id]         AS [IndexId]
   ,[i].[name]             AS [IndexName]
   ,[f].[type]             AS [DataspaceType]
   ,[f].[data_space_id]    AS [DataspaceId]
   ,[f].[name]             AS [DataspaceName]
   ,NULL  AS [FileStreamId]
   ,[ds].[name]            AS [FileStreamName]
   ,[ds].[type]            AS [FileStreamType]   
   ,[i].[fill_factor]      AS [FillFactor]    
   ,CONVERT(bit, CASE [i].[type] WHEN 1 THEN 1 ELSE 0 END) 
                           AS [IsClustered]
   ,[i].[is_unique]        AS [IsUnique]
   ,[i].[is_padded]        AS [IsPadded]
   ,[i].[ignore_dup_key]   AS [DoIgnoreDuplicateKey]
   ,[t].[no_recompute]     AS [NoRecomputeStatistics]
   ,[i].[allow_row_locks]  AS [DoAllowRowLocks]
   ,[i].[allow_page_locks] AS [DoAllowPageLocks]
   ,[i].[is_disabled]      AS [IsDisabled]
   ,[i].[filter_definition]
                           AS [Predicate]
   ,CONVERT(bit, 1)        AS [EqualsParentDataSpace]
   ,[i].[type]             AS [IndexType]
   ,CONVERT(BIT, CASE WHEN [hi].[object_id] IS NULL THEN 0 ELSE 1 END) AS [IsHash]
   ,[hi].[bucket_count] AS [BucketCount]
FROM 
    [sys].[indexes] AS [i] WITH (NOLOCK)
    INNER JOIN [sys].[objects]           AS [o]  WITH (NOLOCK) ON [i].[object_id] = [o].[object_id]
    INNER JOIN [sys].[table_types] AS [tt] WITH (NOLOCK) ON [o].[object_id] = [tt].[type_table_object_id]
    LEFT  JOIN [sys].[data_spaces]       AS [f]  WITH (NOLOCK) ON [i].[data_space_id] = [f].[data_space_id]
    LEFT  JOIN [sys].[stats]             AS [t]  WITH (NOLOCK) ON [t].[object_id] = [i].[object_id] AND [t].[name] = [i].[name]
    LEFT  JOIN [sys].[tables]            AS [ta] WITH (NOLOCK) ON [ta].[object_id] = [i].[object_id]
    LEFT  JOIN [sys].[data_spaces]       AS [ds] WITH (NOLOCK) ON [ds].[data_space_id] = [ta].[filestream_data_space_id]
    LEFT OUTER JOIN [sys].[hash_indexes] AS [hi] WITH (NOLOCK) ON [hi].[object_id] = [i].[object_id] AND [hi].[index_id] = [i].[index_id]
WHERE [i].[is_primary_key] = 0
    AND [i].[is_unique_constraint] = 0
    AND [i].[is_hypothetical] = 0
    AND [i].[name] IS NOT NULL) AS [_results] ORDER BY ColumnSourceId,IndexId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql120TableTypeIndexColumnPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  
    SCHEMA_NAME([tt].[schema_id]) AS [SchemaName]
   ,[tt].[user_type_id]        AS [ColumnSourceId]
   ,[tt].[name]             AS [ColumnSourceName]
   ,'TT'  AS [ColumnSourceType]
   ,[i].[index_id]           AS [IndexId]
   ,[i].[name]               AS [IndexName]
   ,[c].[column_id]          AS [ColumnId]
   ,[c].[name]               AS [ColumnName]
   ,[ic].[is_descending_key] AS [IsDescending]
   ,[ic].[is_included_column] AS [IsIncludedColumn]
   ,[ic].[partition_ordinal]  AS [PartitionOrdinal]
   ,[ic].[key_ordinal]        AS [KeyOrdinal]
   ,[i].[type]                AS [IndexType]
FROM 
    [sys].[columns] AS [c] WITH (NOLOCK)
    INNER JOIN [sys].[table_types] AS [tt] WITH (NOLOCK) ON [c].[object_id] = [tt].[type_table_object_id]
    LEFT JOIN [sys].[index_columns] AS [ic] WITH (NOLOCK) ON [c].[object_id] = [ic].[object_id] AND [c].[column_id] = [ic].[column_id]
    LEFT JOIN [sys].[indexes]       AS [i] WITH (NOLOCK) ON [i].[object_id] = [ic].[object_id] AND [i].[index_id] = [ic].[index_id]
    LEFT JOIN [sys].[objects]   AS [o] WITH (NOLOCK) ON [o].[object_id] = [i].[object_id]
    LEFT JOIN [sys].[tables] as [t] WITH (NOLOCK) ON [t].[object_id] = [c].[object_id]
WHERE [i].[is_primary_key] = 0
    AND [i].[is_unique_constraint] = 0
    AND [i].[name] IS NOT NULL
    AND [i].[is_hypothetical] = 0) AS [_results] ORDER BY ColumnSourceId,IndexId,KeyOrdinal ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql100TableTypeCheckConstraintPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        SCHEMA_NAME([t].[schema_id])        AS [SchemaName]
       ,[t].[user_type_id]                  AS [TableTypeId]
       ,[t].[name]                          AS [TableTypeName]
       ,[cc].[definition]                   AS [Script]
       ,[cc].[object_id]                    AS [ConstraintId]
       ,[cc].[name]                         AS [Name]
FROM	
        [sys].[check_constraints] AS [cc] WITH (NOLOCK)	
        INNER JOIN [sys].[table_types] AS [t] with (NOLOCK) on [t].[type_table_object_id] = [cc].[parent_object_id]
) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql100TableTypeDefaultConstraintPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [d].[object_id]                     AS [ConstraintId]
       ,SCHEMA_NAME([t].[schema_id])        AS [SchemaName]
       ,[t].[user_type_id]                  AS [TableTypeId]
       ,[t].[name]                          AS [TableTypeName]
       ,[c].[column_id]                     AS [ColumnId]
       ,[c].[name]                          AS [ColumnName]
       ,[d].[definition]                    AS [Script]
       ,[d].[name]                          AS [Name]
FROM
        [sys].[default_constraints]    AS [d] WITH (NOLOCK)
        INNER JOIN [sys].[columns] AS [c] WITH (NOLOCK) ON [c].[object_id] = [d].[parent_object_id] AND [c].[column_id] = [d].[parent_column_id]		
        INNER JOIN [sys].[table_types] AS [t] with (NOLOCK) on [t].[type_table_object_id] = [d].[parent_object_id]
WHERE
        OBJECTPROPERTY([d].[parent_object_id], N'IsSystemTable') = 0 
) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90PartitionFunctionPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [pf].[function_id]             AS [ObjectId],
        [pf].[name]                    AS [Name],
        [pf].[fanout]                  AS [FanOut],
        [pf].[boundary_value_on_right] AS [BoundaryValueOnRight],
        [p].[system_type_id]           AS [TypeId],
        [types].[name]                 AS [TypeName],
        CASE WHEN   [p].[max_length] >= 0
            AND     [types].[name] IN (N'nchar', N'nvarchar')
            THEN [p].[max_length] / 2
            ELSE [p].[max_length]
            END                        AS [Length],
        [p].[precision]                AS [Precision],
        [p].[scale]                    AS [Scale]
FROM
        [sys].[partition_functions] [pf] WITH (NOLOCK)
LEFT    JOIN [sys].[partition_parameters] [p] WITH (NOLOCK) ON [p].[function_id] = [pf].[function_id]
LEFT    JOIN [sys].[types] [types] WITH (NOLOCK) ON [p].[system_type_id] = [types].[system_type_id] AND [types].[system_type_id] = [types].[user_type_id]
WHERE   [pf].[is_system] = 0
) AS [_results] ORDER BY ObjectId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90PartitionRangeValuePopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [pr].[function_id] AS [PartitionFunctionId],
        [pr].[value]       AS [Value],
        SQL_VARIANT_PROPERTY([pr].[value], N'BaseType') AS [BaseType],
        [pf].[name]        AS [Name],
        [pr].[boundary_id] AS [BoundaryId]
FROM    [sys].[partition_range_values] [pr] WITH (NOLOCK)
    INNER JOIN [sys].[partition_functions] [pf] WITH (NOLOCK) ON [pr].[function_id] = [pf].[function_id]
WHERE   [pf].[is_system] = 0) AS [_results] ORDER BY PartitionFunctionId,BoundaryId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90PartitionSchemePopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [s].[data_space_id] AS [ObjectId],
        [s].[name]          AS [Name],
        [s].[function_id]   AS [FunctionId],
        [f].[name]          AS [FunctionName]
FROM
        [sys].[partition_schemes] [s] WITH (NOLOCK)
LEFT    JOIN [sys].[partition_functions] [f] WITH (NOLOCK) ON [f].[function_id] = [s].[function_id]
WHERE   [s].[is_system] IS NULL OR [s].[is_system] = 0) AS [_results] ORDER BY ObjectId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90PartitionSchemeFilegroupPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [s].[data_space_id] AS [ObjectId],
        [s].[name]          AS [Name],
        [dd].[data_space_id] AS [FileGroupId],
        [d].[name]           AS [FileGroup],
        [dd].[destination_id] AS [DestinationId]
FROM
        [sys].[partition_schemes] [s] WITH (NOLOCK)
INNER   JOIN [sys].[partition_functions] [f] WITH (NOLOCK) ON [f].[function_id] = [s].[function_id]
INNER   JOIN [sys].[destination_data_spaces] [dd] WITH (NOLOCK) ON [dd].[partition_scheme_id] = [s].[data_space_id]
INNER   JOIN [sys].[data_spaces] [d] WITH (NOLOCK) ON [d].[data_space_id] = [dd].[data_space_id]
WHERE   ([s].[is_system] IS NULL OR [s].[is_system] = 0)
        AND ([d].[is_system] IS NULL OR [d].[is_system] = 0)) AS [_results] ORDER BY ObjectId,DestinationId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql130FunctionPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [s].[schema_id]        AS [OwnerId]
       ,SCHEMA_NAME([s].[schema_id]) AS [FunctionOwner]
       ,[s].[object_id]        AS [FunctionId]
       ,[s].[name]             AS [FunctionName]
       ,[s].[type]             AS [FunctionType]
       ,CONVERT(bit, ISNULL([sm].[uses_ansi_nulls], 0)) 
                               AS [IsAnsiNulls]
       ,CONVERT(bit, ISNULL([sm].[uses_quoted_identifier], 0)) 
                               AS [IsQuotedIdentifier]
       ,[sm].[definition]      AS [Script]
       ,CASE WHEN [s].[is_published] <> 0 OR [s].[is_schema_published] <> 0 THEN 1 ELSE 0 END AS [ReplInfo]
       ,[s].[create_date]            AS [CreatedDate]
       ,[sm].[is_schema_bound] AS [IsSchemaBound]
       ,[sm].[uses_native_compilation] AS [UsesNativeCompilation]
       ,[sm].[execute_as_principal_id] AS [ExecuteAsId]
       ,[p].[name] AS [ExecuteAsName]
       ,CAST([sm].[null_on_null_input] AS BIT) AS [NullOnNullInput]
       ,[ambiguous].[IsAmbiguous] AS [HasAmbiguousReference]
FROM   
        [sys].[objects] AS [s] WITH (NOLOCK)
        LEFT JOIN [sys].[sql_modules]         AS [sm] WITH (NOLOCK) ON [sm].[object_id] = [s].[object_id]
        LEFT JOIN [sys].[database_principals] [p] WITH (NOLOCK) ON [p].[principal_id] = [sm].[execute_as_principal_id]   
        OUTER APPLY (SELECT TOP 1 1 AS [IsAmbiguous] FROM [sys].[sql_expression_dependencies] AS [exp] WITH (NOLOCK)
            WHERE [exp].[referencing_id] = [s].[object_id] AND [exp].[referencing_class] = 1 AND ([exp].[is_ambiguous] = 1 OR ([exp].[is_caller_dependent] = 1 AND 1 <> (SELECT COUNT(1) FROM [sys].[objects] AS [o] WITH (NOLOCK) WHERE [o].[name] = [exp].[referenced_entity_name])) )) AS [ambiguous] 
WHERE   
        [s].[type] IN (N'IF', N'FN', N'TF', N'FT', N'FS') 
        AND ([s].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [s].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )) AND OBJECTPROPERTY([s].[object_id], N'IsEncrypted') = 0
        AND SCHEMA_NAME([s].[schema_id]) <> N'cdc'
) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql130FunctionParameterPopulator' as [PopulatorName];
SELECT * FROM (
SELECT    [o].[object_id]              AS [FunctionId]
          ,SCHEMA_NAME([o].[schema_id]) AS [FunctionOwner]
          ,[o].[name]                   AS [FunctionName]
          ,[o].[type]                   AS [FunctionType]
          ,[p].[parameter_id]           AS [ParameterId]
          ,[p].[name]                   AS [ParameterName]
          ,[p].[parameter_id]           AS [ParameterOrder]
          ,[b].[name]                   AS [BaseTypeName]
          ,[p].[user_type_id]           AS [TypeId]
          ,[s].[name]                   AS [TypeSchemaName]
          ,[t].[name]                   AS [TypeName]
          ,CONVERT(bit, ISNULL([t].[is_user_defined], 0)) AS [IsUserDefinedType]
          ,[p].[precision]              AS [Precision]
          ,[p].[scale]                  AS [Scale]
          ,NULL                         AS [OrderColumnId]
          ,NULL                         AS [IsDescending]            
          ,CONVERT(bit, 1)              AS [IsParameter]
          ,CASE WHEN [p].[max_length] >= 0 AND [t].[name] IN (N'nchar', N'nvarchar')
                THEN [p].[max_length]/2 
                ELSE [p].[max_length] 
           END                          AS [Length]
          ,[p].[has_default_value]      AS [HasDefaultValue]
          ,[p].[default_value]          AS [DefaultValue]
          ,NULL                         AS [IsIdentity]
          ,NULL                         AS [IdentitySeed]
          ,NULL                         AS [IdentityIncrement]
          ,[p].[is_output]              AS [IsOutput]
          ,CONVERT(bit, [p].[is_readonly])
                                        AS [IsReadOnly]
          ,[p].[is_nullable]            AS [IsNullable]
          ,NULL                         AS [IsComputed]
          ,NULL                         AS [IsRowGuidColumn]
          ,[p].[is_cursor_ref]          AS [IsCursor]
          ,NULL                         AS [Collation]
          ,NULL                         AS [AllowNulls]
          ,[p].[is_xml_document]        AS [IsXmlDocument]
          ,[p].[xml_collection_id]      AS [XmlCollectionId]
          ,[xscs].[name]                AS [XmlCollection]
          ,SCHEMA_NAME([xscs].[schema_id]) AS [XmlCollectionSchemaName]
FROM       
           [sys].[parameters] [p] WITH (NOLOCK)
           INNER JOIN [sys].[objects] [o] WITH (NOLOCK) ON [p].[object_id] = [o].[object_id]
           LEFT JOIN [sys].[types] [b] WITH (NOLOCK) ON [p].[system_type_id] = [b].[system_type_id] AND [b].[system_type_id] = [b].[user_type_id]
           LEFT JOIN [sys].[types] [t] WITH (NOLOCK) ON [p].[user_type_id] = [t].[user_type_id]
           LEFT JOIN [sys].[schemas] [s] WITH (NOLOCK) ON [s].[schema_id] = [t].[schema_id]
           LEFT JOIN [sys].[xml_schema_collections] [xscs] WITH (NOLOCK) ON [xscs].[xml_collection_id] = [p].[xml_collection_id]
WHERE      
           [o].[type] IN (N'TF', N'IF', N'FN')
           AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )) AND OBJECTPROPERTY([o].[object_id], N'IsEncrypted') = 0
) AS [_results] ORDER BY FunctionId,ParameterOrder ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql130FunctionParameterPopulator' as [PopulatorName];
SELECT * FROM (
SELECT    [o].[object_id]              AS [FunctionId]
          ,SCHEMA_NAME([o].[schema_id]) AS [FunctionOwner]
          ,[o].[name]                   AS [FunctionName]
          ,[o].[type]                   AS [FunctionType]
          ,[c].[column_id]              AS [ParameterId]
          ,[c].[name]                   AS [ParameterName]
          ,[c].[column_id]              AS [ParameterOrder]
          ,[b].[name]                   AS [BaseTypeName]
          ,[c].[user_type_id]           AS [TypeId]
          ,[s].[name]                   AS [TypeSchemaName]
          ,[t].[name]                   AS [TypeName]
          ,CONVERT(bit, ISNULL([t].[is_user_defined], 0)) AS [IsUserDefinedType]
          ,[c].[precision]              AS [Precision]
          ,[c].[scale]                  AS [Scale]
          ,[oc].[order_column_id]       AS [OrderColumnId]
          ,[oc].[is_descending]         AS [IsDescending]
          ,CONVERT(bit, 0)              AS [IsParameter]
          ,CASE WHEN [c].[max_length] >= 0 AND [t].[name] IN (N'nchar', N'nvarchar')
                THEN [c].[max_length]/2 
                ELSE [c].[max_length] 
           END                          AS [Length]
          ,CAST(0 AS BIT)               AS [HasDefaultValue]
          ,NULL                         AS [DefaultValue]
          ,[c].[is_identity]            AS [IsIdentity]
          ,CAST(ISNULL([i].[seed_value], 0) AS DECIMAL(38)) 
                                        AS [IdentitySeed]
          ,CAST(ISNULL([i].[increment_value], 0) AS DECIMAL(38))
                                        AS [IdentityIncrement]
          ,NULL                         AS [IsOutput]
          ,NULL                         AS [IsReadOnly]
          ,[c].[is_computed]            AS [IsComputed]
          ,[c].[is_rowguidcol]          AS [IsRowGuidColumn]
          ,NULL                         AS [IsCursor]
          ,[c].[collation_name]         AS [Collation]
          ,CONVERT(bit, [c].[is_nullable]) 
                                        AS [AllowNulls]
          ,[c].[is_xml_document] AS [IsXmlDocument]
          ,[c].[xml_collection_id] AS [XmlCollectionId]
          ,[xscs].[name] AS [XmlCollection]
          ,SCHEMA_NAME([xscs].[schema_id]) AS [XmlCollectionSchemaName]

FROM      [sys].[columns] [c] WITH (NOLOCK)
          INNER JOIN [sys].[objects] [o] WITH (NOLOCK) ON [o].[object_id] = [c].[object_id]
          LEFT  JOIN [sys].[types] [b] WITH (NOLOCK) ON [c].[system_type_id] = [b].[system_type_id] AND [b].[system_type_id] = [b].[user_type_id]
          LEFT JOIN [sys].[types] [t] WITH (NOLOCK) ON [c].[user_type_id] = [t].[user_type_id]
          LEFT JOIN [sys].[schemas] [s] WITH (NOLOCK) ON [s].[schema_id] = [t].[schema_id]
          LEFT JOIN [sys].[identity_columns] [i] WITH (NOLOCK) ON [i].[object_id] = [c].[object_id] AND [i].[column_id] = [c].[column_id]
          LEFT JOIN [sys].[xml_schema_collections] [xscs] WITH (NOLOCK) ON [xscs].[xml_collection_id] = [c].[xml_collection_id]
          LEFT JOIN [sys].[function_order_columns] [oc] WITH (NOLOCK) ON [oc].[object_id] = [o].[object_id] AND [oc].[column_id] = [c].[column_id]
WHERE      
          [o].[type] IN (N'TF', N'IF', N'FN')
          AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )) AND OBJECTPROPERTY([o].[object_id], N'IsEncrypted') = 0) AS [_results] ORDER BY FunctionId,ParameterOrder ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql100EncryptedAndClrFunctionPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [s].[schema_id]        AS [OwnerId]
       ,SCHEMA_NAME([s].[schema_id])
                               AS [FunctionOwner]
       ,[s].[object_id]        AS [FunctionId]
       ,[s].[name]             AS [FunctionName]
       ,[s].[type]             AS [FunctionType]
       ,CONVERT(bit, ISNULL([sm].[uses_ansi_nulls], 0))
                               AS [IsAnsiNulls]
       ,CONVERT(bit, ISNULL([sm].[uses_quoted_identifier], 0))
                               AS [IsQuotedIdentifier]
       ,CONVERT(bit, ISNULL([sm].[is_schema_bound], 0))
                               AS [IsSchemaBound]
       ,NULL                   AS [AssemblyId]
       ,NULL                   AS [Assembly]
       ,NULL                   AS [AssemblyClass]
       ,NULL                 AS [AssemblyMethod]
       ,[sm].[execute_as_principal_id] AS [ExecuteAsId]
       ,[p].[name] AS [ExecuteAsName]
       ,CASE WHEN [s].[is_published] <> 0 OR [s].[is_schema_published] <> 0 THEN 1 ELSE 0 END AS [ReplInfo]
       ,[s].[create_date]  AS [CreatedDate]
      ,CAST([sm].[null_on_null_input] AS BIT) AS [NullOnNullInput]
FROM
        [sys].[objects] AS [s] WITH (NOLOCK)
        LEFT JOIN [sys].[sql_modules]    AS [sm] WITH (NOLOCK) ON [sm].[object_id] = [s].[object_id]
        LEFT JOIN [sys].[database_principals] [p] WITH (NOLOCK) ON [p].[principal_id] = [sm].[execute_as_principal_id]
WHERE
        [s].[type] IN (N'IF', N'FN', N'TF') AND ([s].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [s].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )) AND OBJECTPROPERTY([s].[object_id], N'IsEncrypted') = 1

UNION

SELECT
        [s].[schema_id]        AS [OwnerId]
       ,SCHEMA_NAME([s].[schema_id])
                               AS [FunctionOwner]
       ,[s].[object_id]        AS [FunctionId]
       ,[s].[name]             AS [FunctionName]
       ,[s].[type]             AS [FunctionType]
       ,NULL                   AS [IsAnsiNulls]
       ,NULL                   AS [IsQuotedIdentifier]
       ,NULL                   AS [IsSchemaBound]
       ,[as].[assembly_id]            AS [AssemblyId]
       ,[as].[name]                   AS [Assembly]
       ,[am].[assembly_class]         AS [AssemblyClass]
       ,[am].[assembly_method]        AS [AssemblyMethod]
       ,[am].[execute_as_principal_id] AS [ExecuteAsId]
       ,[p].[name] AS [ExecuteAsName]
       ,CASE WHEN [s].[is_published] <> 0 OR [s].[is_schema_published] <> 0 THEN 1 ELSE 0 END AS [ReplInfo]
       ,[s].[create_date]  AS [CreatedDate]
       ,CAST(0 as BIT) AS [NullOnNullInput]
FROM
        [sys].[objects] AS [s] WITH (NOLOCK)
        LEFT JOIN [sys].[assembly_modules]    AS [am] WITH (NOLOCK) ON [am].[object_id] = [s].[object_id]
        LEFT JOIN [sys].[assemblies]          AS [as] WITH (NOLOCK) ON [as].[assembly_id] = [am].[assembly_id]
        LEFT JOIN [sys].[database_principals] [p] WITH (NOLOCK) ON [p].[principal_id] = [am].[execute_as_principal_id]

WHERE
        [s].[type] IN (N'FT', N'FS') AND ([s].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [s].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))
) AS [_results] ORDER BY FunctionId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql100FunctionParameterPopulator' as [PopulatorName];
SELECT * FROM (
SELECT    [o].[object_id]              AS [FunctionId]
          ,SCHEMA_NAME([o].[schema_id]) AS [FunctionOwner]
          ,[o].[name]                   AS [FunctionName]
          ,[o].[type]                   AS [FunctionType]
          ,[p].[parameter_id]           AS [ParameterId]
          ,[p].[name]                   AS [ParameterName]
          ,[p].[parameter_id]           AS [ParameterOrder]
          ,[b].[name]                   AS [BaseTypeName]
          ,[p].[user_type_id]           AS [TypeId]
          ,[s].[name]                   AS [TypeSchemaName]
          ,[t].[name]                   AS [TypeName]
          ,CONVERT(bit, ISNULL([t].[is_user_defined], 0)) AS [IsUserDefinedType]
          ,[p].[precision]              AS [Precision]
          ,[p].[scale]                  AS [Scale]
          ,NULL                         AS [OrderColumnId]
          ,NULL                         AS [IsDescending]            
          ,CONVERT(bit, 1)              AS [IsParameter]
          ,CASE WHEN [p].[max_length] >= 0 AND [t].[name] IN (N'nchar', N'nvarchar')
                THEN [p].[max_length]/2 
                ELSE [p].[max_length] 
           END                          AS [Length]
          ,[p].[has_default_value]      AS [HasDefaultValue]
          ,[p].[default_value]          AS [DefaultValue]
          ,NULL                         AS [IsIdentity]
          ,NULL                         AS [IdentitySeed]
          ,NULL                         AS [IdentityIncrement]
          ,[p].[is_output]              AS [IsOutput]
          ,CONVERT(bit, [p].[is_readonly])
                                        AS [IsReadOnly]
          ,NULL                         AS [IsComputed]
          ,NULL                         AS [IsRowGuidColumn]
          ,[p].[is_cursor_ref]          AS [IsCursor]
          ,NULL                         AS [Collation]
          ,NULL                         AS [AllowNulls]
          ,[p].[is_xml_document]        AS [IsXmlDocument]
          ,[p].[xml_collection_id]        AS [XmlCollectionId]
          ,[xscs].[name]                AS [XmlCollection]
          ,SCHEMA_NAME([xscs].[schema_id]) AS [XmlCollectionSchemaName]
FROM       
           [sys].[parameters] [p] WITH (NOLOCK)
           INNER JOIN [sys].[objects] [o] WITH (NOLOCK) ON [p].[object_id] = [o].[object_id]
           LEFT JOIN [sys].[types] [b] WITH (NOLOCK) ON [p].[system_type_id] = [b].[system_type_id] AND [b].[system_type_id] = [b].[user_type_id]
           LEFT JOIN [sys].[types] [t] WITH (NOLOCK) ON [p].[user_type_id] = [t].[user_type_id]
           LEFT JOIN [sys].[schemas] [s] WITH (NOLOCK) ON [s].[schema_id] = [t].[schema_id]
           LEFT JOIN [sys].[xml_schema_collections] [xscs] WITH (NOLOCK) ON [xscs].[xml_collection_id] = [p].[xml_collection_id]
WHERE      
           [o].[type] IN (N'TF', N'FT', N'IF', N'FN', N'FS')
           AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )) AND ((OBJECTPROPERTY([o].[object_id], N'IsEncrypted') = 1) OR [o].[type] IN (N'FS', N'FT'))
) AS [_results] ORDER BY FunctionId,ParameterOrder ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql100FunctionParameterPopulator' as [PopulatorName];
SELECT * FROM (
SELECT    [o].[object_id]              AS [FunctionId]
          ,SCHEMA_NAME([o].[schema_id]) AS [FunctionOwner]
          ,[o].[name]                   AS [FunctionName]
          ,[o].[type]                   AS [FunctionType]
          ,[c].[column_id]              AS [ParameterId]
          ,[c].[name]                   AS [ParameterName]
          ,[c].[column_id]              AS [ParameterOrder]
          ,[b].[name]                   AS [BaseTypeName]
          ,[c].[user_type_id]           AS [TypeId]
          ,[s].[name]                   AS [TypeSchemaName]
          ,[t].[name]                   AS [TypeName]
          ,CONVERT(bit, ISNULL([t].[is_user_defined], 0)) AS [IsUserDefinedType]
          ,[c].[precision]              AS [Precision]
          ,[c].[scale]                  AS [Scale]
          ,[oc].[order_column_id]       AS [OrderColumnId]
          ,[oc].[is_descending]         AS [IsDescending]          
          ,CONVERT(bit, 0)              AS [IsParameter]
          ,CASE WHEN [c].[max_length] >= 0 AND [t].[name] IN (N'nchar', N'nvarchar')
                THEN [c].[max_length]/2 
                ELSE [c].[max_length] 
           END                          AS [Length]
          ,CAST(0 AS BIT)               AS [HasDefaultValue]
          ,NULL                         AS [DefaultValue]
          ,[c].[is_identity]            AS [IsIdentity]
          ,CAST(ISNULL([i].[seed_value], 0) AS DECIMAL(38)) 
                                        AS [IdentitySeed]
          ,CAST(ISNULL([i].[increment_value], 0) AS DECIMAL(38))
                                        AS [IdentityIncrement]
          ,NULL                         AS [IsOutput]
          ,NULL                         AS [IsReadOnly]
          ,[c].[is_computed]            AS [IsComputed]
          ,[c].[is_rowguidcol]          AS [IsRowGuidColumn]
          ,NULL                         AS [IsCursor]
          ,[c].[collation_name]         AS [Collation]
          ,CONVERT(bit, [c].[is_nullable]) 
                                        AS [AllowNulls]
          ,[c].[is_xml_document] AS [IsXmlDocument]
          ,[c].[xml_collection_id] AS [XmlCollectionId]
          ,[xscs].[name] AS [XmlCollection]
          ,SCHEMA_NAME([xscs].[schema_id]) AS [XmlCollectionSchemaName]

FROM      [sys].[columns] [c]
          INNER JOIN [sys].[objects] [o] WITH (NOLOCK) ON [o].[object_id] = [c].[object_id]
          LEFT  JOIN [sys].[types] [b] WITH (NOLOCK) ON [c].[system_type_id] = [b].[system_type_id] AND [b].[system_type_id] = [b].[user_type_id]
          LEFT JOIN [sys].[types] [t] WITH (NOLOCK) ON [c].[user_type_id] = [t].[user_type_id]
          LEFT JOIN [sys].[schemas] [s] WITH (NOLOCK) ON [s].[schema_id] = [t].[schema_id]
          LEFT JOIN [sys].[identity_columns] [i] WITH (NOLOCK) ON [i].[object_id] = [c].[object_id] AND [i].[column_id] = [c].[column_id]
          LEFT JOIN [sys].[xml_schema_collections] [xscs] WITH (NOLOCK) ON [xscs].[xml_collection_id] = [c].[xml_collection_id]
          LEFT JOIN [sys].[function_order_columns] [oc] WITH (NOLOCK) ON [oc].[object_id] = [o].[object_id] AND [oc].[column_id] = [c].[column_id]
WHERE      
          [o].[type] IN (N'TF', N'FT', N'IF', N'FN', N'FS')
          AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )) AND ((OBJECTPROPERTY([o].[object_id], N'IsEncrypted') = 1) OR [o].[type] IN (N'FS', N'FT'))) AS [_results] ORDER BY FunctionId,ParameterOrder ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90AggregatePopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [o].[schema_id]              AS [SchemaId],
        SCHEMA_NAME([o].[schema_id]) AS [SchemaName],
        [o].[object_id]              AS [AggregateId],
        [o].[name]                   AS [AggregateName],
        [a].[assembly_id]            AS [AssemblyId],
        [a].[name]                   AS [AssemblyName],
        [am].[assembly_class]        AS [AssemblyClass],
        [o].[create_date]            AS [CreatedDate],
        [sm].[execute_as_principal_id] AS [ExecuteAsId],
        [p].[name] AS [ExecuteAsName]
FROM    [sys].[objects] [o] WITH (NOLOCK)
LEFT    JOIN [sys].[sql_modules] [sm] WITH (NOLOCK) ON [sm].[object_id] = [o].[object_id]
LEFT    JOIN [sys].[assembly_modules] [am] WITH (NOLOCK) ON [am].[object_id] = [o].[object_id]
LEFT    JOIN [sys].[assemblies] [a] WITH (NOLOCK) ON [a].[assembly_id] = [am].[assembly_id]
LEFT    JOIN [sys].[database_principals] [p] WITH (NOLOCK) ON [p].[principal_id] = [sm].[execute_as_principal_id]
WHERE   [o].[type] = N'AF' AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))
) AS [_results] ORDER BY AggregateId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90AggregateParametersPopulator' as [PopulatorName];
SELECT * FROM (
SELECT     [o].[object_id]              AS [FunctionId]
          ,SCHEMA_NAME([o].[schema_id]) AS [FunctionOwner]
          ,[o].[name]                   AS [FunctionName]
          ,[p].[parameter_id]           AS [ParameterId]
          ,[p].[name]                   AS [ParameterName]
          ,[p].[parameter_id]           AS [ParameterOrder]
          ,[b].[name]                   AS [BaseTypeName]
          ,[p].[user_type_id]           AS [TypeId]
          ,SCHEMA_NAME([t].[schema_id])      AS [TypeSchemaName]
          ,[t].[name]                   AS [TypeName]
          ,[p].[precision]              AS [Precision]
          ,[p].[scale]                  AS [Scale]
          ,[p].[is_xml_document]        AS [IsXmlDocument]
          ,[p].[xml_collection_id]      AS [XmlCollectionId]
          ,[xscs].[name]                AS [XmlCollection]
          ,SCHEMA_NAME([xscs].[schema_id]) AS [XmlCollectionSchemaName]
          ,CASE WHEN [p].[max_length] >= 0 AND [t].[name] IN (N'nchar', N'nvarchar')
                THEN [p].[max_length]/2
                ELSE [p].[max_length]
           END                          AS [Length]
FROM       [sys].[parameters] [p] WITH (NOLOCK)
INNER JOIN [sys].[objects] [o] WITH (NOLOCK) ON [p].[object_id] = [o].[object_id]
LEFT  JOIN [sys].[types]   [b] WITH (NOLOCK) ON [p].[system_type_id] = [b].[system_type_id] AND [b].[system_type_id] = [b].[user_type_id]
LEFT  JOIN [sys].[types]   [t] WITH (NOLOCK) ON [p].[user_type_id] = [t].[user_type_id]
LEFT  JOIN [sys].[xml_schema_collections] [xscs] WITH (NOLOCK) ON [xscs].[xml_collection_id] = [p].[xml_collection_id]
WHERE      [o].[type] = N'AF' AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) AS [_results] ORDER BY FunctionId,ParameterOrder ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql120ProcedurePopulator' as [PopulatorName];
SELECT * FROM (SELECT
        [sp].[schema_id]              AS [SchemaId]
       ,SCHEMA_NAME([sp].[schema_id]) AS [ProcedureOwner]
       ,[sp].[object_id]              AS [ProcedureId]
       ,[sp].[name]                   AS [ProcedureName]
       ,1                             AS [ProcedureNumber]
       ,CONVERT(bit, ISNULL([sm].[uses_quoted_identifier], 0))
                                      AS [IsQuotedIdentifier]
       ,CONVERT(bit, ISNULL([sm].[uses_ansi_nulls], 0))
                                      AS [IsAnsiNulls]
       ,[sm].[is_recompiled]          AS [IsRecompiled]
       ,CAST(CASE sp.type WHEN N'RF' THEN 1 ELSE 0 END AS bit) AS [IsForReplication]
       ,[sm].[definition]             AS [Script]
       ,1                             AS [SequenceNumber]
       ,0                             AS [SubsequenceNumber]
       ,CASE WHEN [sp].[is_published] <> 0 OR [sp].[is_schema_published] <> 0 THEN 1 ELSE 0 END AS [ReplInfo]
       ,[sp].[create_date]            AS [CreatedDate]
       ,[sm].[execute_as_principal_id] AS [ExecuteAsId]
       ,[p].[name] AS [ExecuteAsName]
       ,[sm].[is_schema_bound] AS [IsSchemaBound]
       ,[sm].[uses_native_compilation] AS [UsesNativeCompilation]
       ,[ambiguous].[IsAmbiguous] AS [HasAmbiguousReference]
FROM
        [sys].[objects]                   AS [sp] WITH (NOLOCK)
        LEFT JOIN [sys].[sql_modules]     AS [sm] WITH (NOLOCK) ON [sm].[object_id] = [sp].[object_id]
        LEFT JOIN [sys].[database_principals] [p] WITH (NOLOCK) ON [p].[principal_id] = [sm].[execute_as_principal_id]
        OUTER APPLY (SELECT TOP 1 1 AS [IsAmbiguous] FROM [sys].[sql_expression_dependencies] AS [exp] WITH (NOLOCK)
            WHERE [exp].[referencing_id] = [sp].[object_id] AND [exp].[referencing_class] = 1 AND ([exp].[is_ambiguous] = 1 OR ([exp].[is_caller_dependent] = 1 AND 1 <> (SELECT COUNT(1) FROM [sys].[objects] AS [o] WITH (NOLOCK) WHERE [o].[name] = [exp].[referenced_entity_name])) )) AS [ambiguous] 
WHERE [sp].[type] in (N'P',N'RF') AND ([sp].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [sp].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )) AND (OBJECTPROPERTY([sp].[object_id], N'IsEncrypted') = 0 OR OBJECTPROPERTY([sp].[object_id], N'IsEncrypted') IS NULL)
) AS [_results] ORDER BY ProcedureId,ProcedureNumber,SequenceNumber DESC;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql100ProcedureParameterPopulator' as [PopulatorName];
SELECT * FROM (
SELECT 
     [p].[object_id]            AS [ProcedureId]
    ,SCHEMA_NAME([p].[schema_id]) 
                                AS [ProcedureOwner]
    ,[p].[name]                 AS [ProcedureName]
    ,1                          AS [ProcedureNumber]
    ,[c].[parameter_id]         AS [ParameterId]
    ,[c].[name]                 AS [ParameterName]
    ,[c].[parameter_id]         AS [ParameterOrder]
    ,[c].[user_type_id]         AS [TypeId]
    ,SCHEMA_NAME([t].[schema_id])  
                                AS [TypeSchemaName]
    ,(CASE WHEN (CONVERT(bit, COLUMNPROPERTY([p].[object_id], [c].[name], N'IsCursorType')) = 1)
          THEN N'cursor'
          ELSE [t].[name]
     END)                       AS [TypeName]
    ,CASE WHEN [c].[max_length] >= 0 AND [t].[name] IN (N'nchar', N'nvarchar')
          THEN ([c].[max_length] / 2)
          ELSE [c].[max_length] 
     END                        AS [Length]
    ,[c].[precision]            AS [Precision]
    ,[c].[scale]                AS [Scale]
    ,[c].[has_default_value]    AS [HasDefaultValue]
    ,[c].[default_value]        AS [DefaultValue]
    ,[c].[is_output]            AS [IsOutput]
    ,CAST([c].[is_readonly] AS bit)
                                AS [IsReadOnly]
    ,[c].[is_xml_document] 
                                AS [IsXmlDocument]
    ,[c].[xml_collection_id] 
                                AS [XmlCollectionId]
    ,[xscs].[name]              AS [XmlCollection]
    ,SCHEMA_NAME([xscs].[schema_id]) 
                                AS [XmlCollectionSchemaName]
FROM 
    [sys].[objects] AS [p] WITH (NOLOCK)    
    INNER JOIN [sys].[parameters] AS [c] WITH (NOLOCK) ON [c].[object_id] = [p].[object_id]    
    LEFT  JOIN [sys].[types] AS [t] WITH (NOLOCK) ON [t].[user_type_id] = [c].[user_type_id]
    LEFT  JOIN [sys].[xml_schema_collections] [xscs] WITH (NOLOCK) ON [xscs].[xml_collection_id] = [c].[xml_collection_id]
WHERE    
    [p].[type] IN (N'P',N'RF') 
    AND [p].[name] NOT LIKE N'#%%'    
    AND OBJECTPROPERTY([p].[object_id], N'IsEncrypted') = 0
    AND ([p].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [p].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) AS [_results] ORDER BY ProcedureId,ProcedureNumber,ParameterOrder ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.SqlProcedurePopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [sp].[schema_id]              AS [SchemaId]
       ,SCHEMA_NAME([sp].[schema_id]) AS [ProcedureOwner]
       ,[sp].[object_id]              AS [ProcedureId]
       ,[sp].[name]                   AS [ProcedureName]
       , [np].[procedure_number]      AS [ProcedureNumber]
       ,CONVERT(bit, ISNULL([sm].[uses_quoted_identifier], 0))
                                      AS [IsQuotedIdentifier]
       ,CONVERT(bit, ISNULL([sm].[uses_ansi_nulls], 0))
                                      AS [IsAnsiNulls]
       ,[sm].[is_recompiled]          AS [IsRecompiled]
       ,CAST(CASE sp.type WHEN N'RF' THEN 1 ELSE 0 END AS bit) AS [IsForReplication]
       ,[np].[definition]             AS [Script]
       ,1                             AS [SequenceNumber]
       ,0                             AS [SubsequenceNumber]
       ,CASE WHEN [sp].[is_published] <> 0 OR [sp].[is_schema_published] <> 0 THEN 1 ELSE 0 END AS [ReplInfo]
       ,[sp].[create_date]            AS [CreatedDate]
       ,[sm].[execute_as_principal_id] AS [ExecuteAsId]
       ,[p].[name] AS [ExecuteAsName]
       ,[ambiguous].[IsAmbiguous] AS [HasAmbiguousReference]
FROM
[sys].[procedures] AS [sp] WITH (NOLOCK)
INNER JOIN [sys].[numbered_procedures] AS [np] WITH (NOLOCK) ON [np].[object_id] = [sp].[object_id]
LEFT JOIN [sys].[sql_modules]     AS [sm] WITH (NOLOCK) ON [sm].[object_id] = [sp].[object_id]
LEFT JOIN [sys].[database_principals] [p] WITH (NOLOCK) ON [p].[principal_id] = [sm].[execute_as_principal_id]
OUTER APPLY (SELECT TOP 1 1 AS [IsAmbiguous] FROM [sys].[sql_expression_dependencies] AS [exp] WITH (NOLOCK)
            WHERE [exp].[referencing_id] = [sp].[object_id] AND [exp].[referencing_class] = 1 AND ([exp].[is_ambiguous] = 1 OR ([exp].[is_caller_dependent] = 1 AND 1 <> (SELECT COUNT(1) FROM [sys].[objects] AS [o] WITH (NOLOCK) WHERE [o].[name] = [exp].[referenced_entity_name])) )) AS [ambiguous] 
WHERE ([sp].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [sp].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )) AND OBJECTPROPERTY([sp].[object_id], N'IsEncrypted') = 0
) AS [_results] ORDER BY ProcedureId,ProcedureNumber,SequenceNumber DESC;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql100ProcedureParameterPopulator' as [PopulatorName];
SELECT * FROM (
SELECT 
     [p].[object_id]            AS [ProcedureId]
    ,SCHEMA_NAME([p].[schema_id]) 
                                AS [ProcedureOwner]
    ,[p].[name]                 AS [ProcedureName]
    ,[np].[procedure_number] 
                                AS [ProcedureNumber]
    ,[c].[parameter_id]            AS [ParameterId]
    ,[c].[name]                 AS [ParameterName]
    ,[c].[parameter_id]         AS [ParameterOrder]
    ,[c].[user_type_id]         AS [TypeId]
    ,SCHEMA_NAME([t].[schema_id])  
                                AS [TypeSchemaName]
    ,(CASE WHEN (CONVERT(bit, COLUMNPROPERTY([p].[object_id], [c].[name], N'IsCursorType')) = 1)
          THEN N'cursor'
          ELSE [t].[name]
     END)                       AS [TypeName]
    ,CASE WHEN [c].[max_length] >= 0 AND [t].[name] IN (N'nchar', N'nvarchar')
          THEN ([c].[max_length] / 2)
          ELSE [c].[max_length] 
     END                        AS [Length]
    ,[c].[precision]            AS [Precision]
    ,[c].[scale]                AS [Scale]
    ,CAST(0 AS BIT)             AS [HasDefaultValue]
    ,NULL                       AS [DefaultValue]
    ,[c].[is_output]            AS [IsOutput] 
    ,CONVERT(bit, CASE ([c].[system_type_id]) WHEN 243 THEN 1 ELSE 0 END)
                                AS [IsReadOnly]    
    ,CAST(0 AS bit)             AS [IsXmlDocument]
    ,0                            AS [XmlCollectionId]
    ,NULL                        AS [XmlCollection]
    ,NULL                        AS [XmlCollectionSchemaName]
FROM 
    [sys].[numbered_procedures] AS [np] WITH (NOLOCK)
    INNER JOIN [sys].[procedures] AS [p] WITH (NOLOCK) ON [p].[object_id] = [np].[object_id]
    INNER JOIN [sys].[numbered_procedure_parameters] AS [c] WITH (NOLOCK) ON [c].[object_id] = [p].[object_id] 
               AND [c].[procedure_number] = [np].[procedure_number]   
    LEFT  JOIN [sys].[types] AS [t] WITH (NOLOCK) ON [t].[user_type_id] = [c].[user_type_id]    
WHERE    
    [p].[name] NOT LIKE N'#%%'
    AND OBJECTPROPERTY([p].[object_id], N'IsEncrypted') = 0
    AND ([p].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [p].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) AS [_results] ORDER BY ProcedureId,ProcedureNumber,ParameterOrder ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90EncryptedAndClrProcedurePopulator' as [PopulatorName];
SELECT * FROM (

SELECT
        [sp].[schema_id]              AS [SchemaId]
       ,SCHEMA_NAME([sp].[schema_id]) AS [ProcedureOwner]
       ,[sp].[object_id]              AS [ProcedureId]
       ,[sp].[name]                   AS [ProcedureName]
       ,CAST(1 AS smallint)           AS [ProcedureNumber]
       ,NULL
                                      AS [IsQuotedIdentifier]
       ,NULL
                                      AS [IsAnsiNulls]
       ,CONVERT(bit, ISNULL(OBJECTPROPERTY([sp].[object_id], N'IsReplProc'), 0))
                                      AS [IsForReplication]
       ,NULL
                                      AS [IsSchemaBound]
       ,NULL                          AS [IsRecompiled]
       ,[sp].[type]                   AS [ProcedureType]
       ,[as].[assembly_id]            AS [AssemblyId]
       ,[as].[name]                   AS [Assembly]
       ,[am].[assembly_class]         AS [AssemblyClass]
       ,[am].[assembly_method]        AS [AssemblyMethod]
       ,[am].[null_on_null_input]      AS [NullOnNullInput]
       ,[am].[execute_as_principal_id] AS [ExecuteAsId]
       ,[p2].[name] AS [ExecuteAsName]
       ,CASE WHEN [so].[is_published] <> 0 OR [so].[is_schema_published] <> 0 THEN 1 ELSE 0 END AS [ReplInfo]
       ,[sp].[create_date]            AS [CreatedDate]
FROM
        [sys].[objects]                    AS [sp] WITH (NOLOCK)
        LEFT JOIN [sys].[assembly_modules]    AS [am] WITH (NOLOCK) ON [am].[object_id] = [sp].[object_id]
        LEFT JOIN [sys].[assemblies]          AS [as] WITH (NOLOCK) ON [as].[assembly_id] = [am].[assembly_id]
        LEFT JOIN [sys].[database_principals] AS [p1] WITH (NOLOCK) ON [p1].[principal_id] = [sp].[principal_id]
        LEFT JOIN [sys].[database_principals] AS [p2] WITH (NOLOCK) ON [p2].[principal_id] = [am].[execute_as_principal_id]
        LEFT JOIN [sys].[objects]             AS [so] WITH (NOLOCK) ON [so].[object_id] = [sp].[object_id]
WHERE  ([sp].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [sp].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )) AND [sp].[type] = N'PC'
) AS [_results] ORDER BY ProcedureId,ProcedureNumber ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql100ProcedureParameterPopulator' as [PopulatorName];
SELECT * FROM (
SELECT 
     [p].[object_id]            AS [ProcedureId]
    ,SCHEMA_NAME([p].[schema_id]) 
                                AS [ProcedureOwner]
    ,[p].[name]                 AS [ProcedureName]
    ,1					        AS [ProcedureNumber]
    ,[c].[parameter_id]	        AS [ParameterId]
    ,[c].[name]                 AS [ParameterName]
    ,[c].[parameter_id]         AS [ParameterOrder]
    ,[c].[user_type_id]         AS [TypeId]
    ,SCHEMA_NAME([t].[schema_id])  
                                AS [TypeSchemaName]
    ,(CASE WHEN (CONVERT(bit, COLUMNPROPERTY([p].[object_id], [c].[name], N'IsCursorType')) = 1)
          THEN N'cursor'
          ELSE [t].[name]
     END)                       AS [TypeName]
    ,CASE WHEN [c].[max_length] >= 0 AND [t].[name] IN (N'nchar', N'nvarchar')
          THEN ([c].[max_length] / 2)
          ELSE [c].[max_length] 
     END                        AS [Length]
    ,[c].[precision]            AS [Precision]
    ,[c].[scale]                AS [Scale]
    ,[c].[has_default_value]    AS [HasDefaultValue]
    ,[c].[default_value]        AS [DefaultValue]
    ,[c].[is_output]	        AS [IsOutput]
    ,CAST([c].[is_readonly] AS bit)
                                AS [IsReadOnly]
    ,[c].[is_xml_document] 
                                AS [IsXmlDocument]
    ,[c].[xml_collection_id] 
                                AS [XmlCollectionId]
    ,[xscs].[name]		        AS [XmlCollection]
    ,SCHEMA_NAME([xscs].[schema_id]) 
                                AS [XmlCollectionSchemaName]
FROM 
    [sys].[objects] AS [p] WITH (NOLOCK)	
    INNER JOIN [sys].[parameters] AS [c] WITH (NOLOCK) ON [c].[object_id] = [p].[object_id]    
    LEFT  JOIN [sys].[types] AS [t] WITH (NOLOCK) ON [t].[user_type_id] = [c].[user_type_id]
    LEFT  JOIN [sys].[xml_schema_collections] [xscs] WITH (NOLOCK) ON [xscs].[xml_collection_id] = [c].[xml_collection_id]
WHERE	
    [p].[type] = N'PC'
    AND [p].[name] NOT LIKE N'#%%'
    AND (( OBJECTPROPERTY([p].[object_id], N'IsEncrypted') = 1 OR OBJECTPROPERTY([p].[object_id], N'IsEncrypted') IS NULL) OR [p].[type] = N'PC')
    AND ([p].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [p].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) AS [_results] ORDER BY ProcedureId,ProcedureNumber,ParameterOrder ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90EncryptedAndClrProcedurePopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [sp].[schema_id]              AS [SchemaId]
       ,SCHEMA_NAME([sp].[schema_id]) AS [ProcedureOwner]
       ,[sp].[object_id]              AS [ProcedureId]
       ,[sp].[name]                   AS [ProcedureName]
       ,1                             AS [ProcedureNumber]
       ,CONVERT(bit, ISNULL([sm].[uses_quoted_identifier], 0))
                                      AS [IsQuotedIdentifier]
       ,CONVERT(bit, ISNULL([sm].[uses_ansi_nulls], 0))
                                      AS [IsAnsiNulls]
       ,CONVERT(bit, ISNULL(OBJECTPROPERTY([sp].[object_id], N'IsReplProc'), 0))
                                      AS [IsForReplication]
       ,CONVERT(bit, ISNULL([sm].[is_schema_bound], 0))
                                      AS [IsSchemaBound]
       ,[sm].[is_recompiled]          AS [IsRecompiled]
       ,[sp].[type]                   AS [ProcedureType]
       ,NULL                          AS [AssemblyId]
       ,NULL                          AS [Assembly]
       ,NULL                          AS [AssemblyClass]
       ,NULL                          AS [AssemblyMethod]
       ,[sm].[null_on_null_input]      AS [NullOnNullInput]
       ,[sm].[execute_as_principal_id] AS [ExecuteAsId]
       ,[p2].[name] AS [ExecuteAsName]
       ,CASE WHEN [sp].[is_published] <> 0 OR [sp].[is_schema_published] <> 0 THEN 1 ELSE 0 END AS [ReplInfo]
       ,[sp].[create_date]            AS [CreatedDate]
FROM
        [sys].[objects]                   AS [sp] WITH (NOLOCK)
        LEFT JOIN [sys].[sql_modules]     AS [sm] WITH (NOLOCK) ON [sm].[object_id] = [sp].[object_id]
        LEFT JOIN [sys].[database_principals] AS [p1] WITH (NOLOCK) ON [p1].[principal_id] = [sp].[principal_id]
        LEFT JOIN [sys].[database_principals] AS [p2] WITH (NOLOCK) ON [p2].[principal_id] = [sm].[execute_as_principal_id]

WHERE [sp].[type] in (N'P',N'RF') AND ([sp].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [sp].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )) AND (OBJECTPROPERTY([sp].[object_id], N'IsEncrypted') = 1 OR OBJECTPROPERTY([sp].[object_id], N'IsEncrypted') IS NULL)

) AS [_results] ORDER BY ProcedureId,ProcedureNumber ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql100ProcedureParameterPopulator' as [PopulatorName];
SELECT * FROM (
SELECT 
     [p].[object_id]            AS [ProcedureId]
    ,SCHEMA_NAME([p].[schema_id]) 
                                AS [ProcedureOwner]
    ,[p].[name]                 AS [ProcedureName]
    ,1					        AS [ProcedureNumber]
    ,[c].[parameter_id]	        AS [ParameterId]
    ,[c].[name]                 AS [ParameterName]
    ,[c].[parameter_id]         AS [ParameterOrder]
    ,[c].[user_type_id]         AS [TypeId]
    ,SCHEMA_NAME([t].[schema_id])  
                                AS [TypeSchemaName]
    ,(CASE WHEN (CONVERT(bit, COLUMNPROPERTY([p].[object_id], [c].[name], N'IsCursorType')) = 1)
          THEN N'cursor'
          ELSE [t].[name]
     END)                       AS [TypeName]
    ,CASE WHEN [c].[max_length] >= 0 AND [t].[name] IN (N'nchar', N'nvarchar')
          THEN ([c].[max_length] / 2)
          ELSE [c].[max_length] 
     END                        AS [Length]
    ,[c].[precision]            AS [Precision]
    ,[c].[scale]                AS [Scale]
    ,[c].[has_default_value]    AS [HasDefaultValue]
    ,[c].[default_value]        AS [DefaultValue]
    ,[c].[is_output]	        AS [IsOutput]
    ,CAST([c].[is_readonly] AS bit)
                                AS [IsReadOnly]
    ,[c].[is_xml_document] 
                                AS [IsXmlDocument]
    ,[c].[xml_collection_id] 
                                AS [XmlCollectionId]
    ,[xscs].[name]		        AS [XmlCollection]
    ,SCHEMA_NAME([xscs].[schema_id]) 
                                AS [XmlCollectionSchemaName]
FROM 
    [sys].[objects] AS [p] WITH (NOLOCK)	
    INNER JOIN [sys].[parameters] AS [c] WITH (NOLOCK) ON [c].[object_id] = [p].[object_id]    
    LEFT  JOIN [sys].[types] AS [t] WITH (NOLOCK) ON [t].[user_type_id] = [c].[user_type_id]
    LEFT  JOIN [sys].[xml_schema_collections] [xscs] WITH (NOLOCK) ON [xscs].[xml_collection_id] = [c].[xml_collection_id]
WHERE	
    [p].[type] IN (N'P',N'RF')
    AND [p].[name] NOT LIKE N'#%%'
    AND (( OBJECTPROPERTY([p].[object_id], N'IsEncrypted') = 1 OR OBJECTPROPERTY([p].[object_id], N'IsEncrypted') IS NULL) OR [p].[type] = N'PC')
    AND ([p].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [p].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) AS [_results] ORDER BY ProcedureId,ProcedureNumber,ParameterOrder ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90EncryptedAndClrProcedurePopulator' as [PopulatorName];
SELECT * FROM (

SELECT
        [sp].[schema_id]              AS [SchemaId]
       ,SCHEMA_NAME([sp].[schema_id]) AS [ProcedureOwner]
       ,[sp].[object_id]              AS [ProcedureId]
       ,[sp].[name]                   AS [ProcedureName]
       ,[np].[procedure_number]       AS [ProcedureNumber]
       ,CONVERT(bit, ISNULL([sm].[uses_quoted_identifier], 0))
                                      AS [IsQuotedIdentifier]
       ,CONVERT(bit, ISNULL([sm].[uses_ansi_nulls], 0))
                                      AS [IsAnsiNulls]
       ,CONVERT(bit, ISNULL(OBJECTPROPERTY([sp].[object_id], N'IsReplProc'), 0))
                                      AS [IsForReplication]
       ,CONVERT(bit, ISNULL([sm].[is_schema_bound], 0))
                                      AS [IsSchemaBound]
       ,[sm].[is_recompiled]          AS [IsRecompiled]
       ,[sp].[type]                   AS [ProcedureType]
       ,NULL                          AS [AssemblyId]
       ,NULL                          AS [Assembly]
       ,NULL                          AS [AssemblyClass]
       ,NULL                          AS [AssemblyMethod]
       ,[sm].[null_on_null_input]      AS [NullOnNullInput]
       ,[sm].[execute_as_principal_id] AS [ExecuteAsId]
       ,[p2].[name] AS [ExecuteAsName]
       ,CASE WHEN [sp].[is_published] <> 0 OR [sp].[is_schema_published] <> 0 THEN 1 ELSE 0 END AS [ReplInfo]
       ,[sp].[create_date]            AS [CreatedDate]
FROM
        [sys].[procedures]                     AS [sp] WITH (NOLOCK)
        INNER JOIN [sys].[numbered_procedures] AS [np] WITH (NOLOCK) ON [np].[object_id] = [sp].[object_id]
        LEFT JOIN [sys].[sql_modules]          AS [sm] WITH (NOLOCK) ON [sm].[object_id] = [sp].[object_id]
        LEFT JOIN [sys].[database_principals]  AS [p1] WITH (NOLOCK) ON [p1].[principal_id] = [sp].[principal_id]
        LEFT JOIN [sys].[database_principals]  AS [p2] WITH (NOLOCK) ON [p2].[principal_id] = [sm].[execute_as_principal_id]

WHERE  ([sp].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [sp].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )) AND (OBJECTPROPERTY([sp].[object_id], N'IsEncrypted') = 1 OR OBJECTPROPERTY([sp].[object_id], N'IsEncrypted') IS NULL)
) AS [_results] ORDER BY ProcedureId,ProcedureNumber ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql100ProcedureParameterPopulator' as [PopulatorName];
SELECT * FROM (
SELECT 
     [p].[object_id]            AS [ProcedureId]
    ,SCHEMA_NAME([p].[schema_id]) 
                                AS [ProcedureOwner]
    ,[p].[name]                 AS [ProcedureName]
    ,[np].[procedure_number] 
                                AS [ProcedureNumber]
    ,[c].[parameter_id]	        AS [ParameterId]
    ,[c].[name]                 AS [ParameterName]
    ,[c].[parameter_id]         AS [ParameterOrder]
    ,[c].[user_type_id]         AS [TypeId]
    ,SCHEMA_NAME([t].[schema_id])  
                                AS [TypeSchemaName]
    ,(CASE WHEN (CONVERT(bit, COLUMNPROPERTY([p].[object_id], [c].[name], N'IsCursorType')) = 1)
          THEN N'cursor'
          ELSE [t].[name]
     END)                       AS [TypeName]
    ,CASE WHEN [c].[max_length] >= 0 AND [t].[name] IN (N'nchar', N'nvarchar')
          THEN ([c].[max_length] / 2)
          ELSE [c].[max_length] 
     END                        AS [Length]
    ,[c].[precision]            AS [Precision]
    ,[c].[scale]                AS [Scale]
    ,CAST(0 AS BIT)             AS [HasDefaultValue]
    ,NULL                       AS [DefaultValue]
    ,[c].[is_output]	        AS [IsOutput] 
    ,CONVERT(bit, CASE ([c].[system_type_id]) WHEN 243 THEN 1 ELSE 0 END)
                                AS [IsReadOnly]    
    ,CAST(0 AS bit) 	        AS [IsXmlDocument]
    ,0		                    AS [XmlCollectionId]
    ,NULL		                AS [XmlCollection]
    ,NULL				        AS [XmlCollectionSchemaName]
FROM 
    [sys].[numbered_procedures] AS [np] WITH (NOLOCK)
    INNER JOIN [sys].[procedures] AS [p] WITH (NOLOCK) ON [p].[object_id] = [np].[object_id]
    INNER JOIN [sys].[numbered_procedure_parameters] AS [c] WITH (NOLOCK) ON [c].[object_id] = [p].[object_id] 
               AND [c].[procedure_number] = [np].[procedure_number]   
    LEFT  JOIN [sys].[types] AS [t] WITH (NOLOCK) ON [t].[user_type_id] = [c].[user_type_id]    
WHERE	
    [p].[name] NOT LIKE N'#%%'
    AND OBJECTPROPERTY([p].[object_id], N'IsEncrypted') = 1
    AND ([p].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [p].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) AS [_results] ORDER BY ProcedureId,ProcedureNumber,ParameterOrder ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql130TablePopulator' as [PopulatorName];
SELECT * FROM (
SELECT  
        [t].[schema_id]              AS [SchemaId],
        SCHEMA_NAME([t].[schema_id]) AS [SchemaName],
        [t].[name]                   AS [ColumnSourceName], 
        [t].[object_id]              AS [TableId],
        [t].[type]                   AS [Type],
        [ds].[type]                  AS [DataspaceType],
        [ds].[data_space_id]         AS [DataspaceId],
        [ds].[name]                  AS [DataspaceName],
        [si].[index_id]              AS [IndexId],
        [si].[type]                  AS [IndexType],
        CASE WHEN exists(SELECT 1 FROM [sys].[columns] AS [c] WITH (NOLOCK) WHERE [c].[object_id] = [st].[object_id] AND  ([c].[system_type_id] IN (34, 35, 99, 241) OR ( [c].[system_type_id] in (165, 167,231,240) AND [c].[max_length] = -1))) THEN
            [dsx].[data_space_id] 
        ELSE
            NULL
        END AS [TextFilegroupId],
        [dsx].[name]				  AS [TextFilegroupName],
        CASE WHEN exists(SELECT 1 FROM [sys].[columns] AS [c] WITH (NOLOCK) WHERE [c].[object_id] = [st].[object_id] AND  [c].[is_filestream] = 1) THEN
            [dsf].[data_space_id] 
        ELSE
            NULL
        END AS [FileStreamId],
        [dsf].[name]                  AS [FileStreamName],
        [dsf].[type]                  AS [FileStreamType],
        [st].[uses_ansi_nulls]		  AS [IsAnsiNulls],
        CAST(OBJECTPROPERTY([st].[object_id],N'IsQuotedIdentOn') AS bit) AS [IsQuotedIdentifier],
        CAST([st].[lock_on_bulk_load] AS bit) 
                                      AS [IsLockedOnBulkLoad],
        [st].[text_in_row_limit]	  AS [TextInRowLimit],
        CAST([st].[large_value_types_out_of_row] AS bit)
                                      AS [LargeValuesOutOfRow],
        CAST(OBJECTPROPERTY([st].[object_id], N'TableHasVarDecimalStorageFormat') AS bit)
                                      AS [HasVarDecimalStorageFormat],
        [st].[is_tracked_by_cdc]      AS [IsTrackedByCDC],
        CAST(CASE
             WHEN [ctt].[object_id] IS NULL THEN 0
             ELSE 1
             END AS BIT)              AS [IsChangeTrackingOn],
        [ctt].[is_track_columns_updated_on] AS [IsTrackColumnsUpdatedOn],
        [st].[lock_escalation]        AS [LockEscalation],     
        CASE WHEN [st].[is_replicated] <> 0 OR [st].[is_merge_published] <> 0 OR [st].[is_schema_published] <> 0 OR [st].[is_published] <> 0 THEN 1 ELSE 0 END AS [ReplInfo]        ,
        [t].[create_date] AS [CreateDate],
        CAST([st].[is_memory_optimized] AS BIT) AS [IsMemoryOptimized],
        [st].[durability] AS [Durability],
        [st].[temporal_type] AS [TemporalType],
        [historyTable].[name] AS [HistoryTableName],
        SCHEMA_NAME([historyTable].[schema_id]) AS [HistoryTableSchema],
        [st].[is_remote_data_archive_enabled] AS  [IsRemoteDataEnabled]
FROM    
        [sys].[objects] [t] WITH (NOLOCK)
        LEFT    JOIN [sys].[tables] [st] WITH (NOLOCK) ON [t].[object_id] = [st].[object_id]
        LEFT    JOIN (SELECT * FROM [sys].[indexes] WITH (NOLOCK) WHERE ISNULL([index_id],0) < 2) [si] ON [si].[object_id] = [st].[object_id]
        LEFT    JOIN [sys].[data_spaces] [ds] WITH (NOLOCK) ON [ds].[data_space_id] = [si].[data_space_id]
        LEFT    JOIN [sys].[data_spaces] [dsx] WITH (NOLOCK) ON [dsx].[data_space_id] = [st].[lob_data_space_id]
        LEFT    JOIN [sys].[data_spaces] [dsf] WITH (NOLOCK) ON [dsf].[data_space_id] = [st].[filestream_data_space_id]
        LEFT    JOIN [sys].[change_tracking_tables] [ctt] WITH (NOLOCK) ON [ctt].[object_id] = [st].[object_id]
        LEFT    JOIN [sys].[periods] [periods] WITH (NOLOCK) ON [periods].[object_id] = [st].[object_id]
        LEFT    JOIN [sys].[tables] [historyTable] WITH (NOLOCK) ON [st].[history_table_id] = [historyTable].[object_id]
WHERE  [t].[type] = N'U' AND ISNULL([st].[is_filetable],0) = 0 AND ISNULL([st].[is_external],0) = 0 AND ([t].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [t].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) AS [_results] ORDER BY TableId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql130TableColumnPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [t].[name] AS [ColumnSourceName], 
        [t].[object_id] AS [TableId],
        SCHEMA_NAME([t].[schema_id]) AS [SchemaName], 
        [c].[name] AS [ColumnName], 
        [c].[user_type_id] AS [TypeId],
        CONVERT(bit, ISNULL([types].[is_user_defined], 0)) AS [IsUserDefinedType],
        [types].[name] AS [TypeName],
        [basetypes].[name] AS [BaseTypeName],
        SCHEMA_NAME([types].[schema_id]) AS [TypeSchemaName],
        [c].[column_id] AS [ColumnId], 
        [c].[precision] AS [Precision],
        [c].[scale] AS [Scale],
        CASE WHEN [c].[max_length] >= 0 AND [types].[name] IN (N'nchar', N'nvarchar') THEN ([c].[max_length] / 2) ELSE [c].[max_length] END AS [Length],
        CONVERT(bit, [c].[is_identity]) AS [IsIdentity],
        CONVERT(bit, [c].[is_computed]) AS [IsComputed],
        CONVERT(bit, ISNULL([ic].[is_not_for_replication], 0)) AS [IsNotForReplication],
        CAST(ISNULL([ic].[seed_value], 0) AS DECIMAL(38)) AS [IdentitySeed],
        CAST(ISNULL([ic].[increment_value], 0) AS DECIMAL(38)) AS [IdentityIncrement],
        CONVERT(bit, [c].[is_nullable]) AS [IsNullable],
        [cc].[definition] AS [ComputedText],
        [c].[is_rowguidcol] AS [IsRowGuidColumn],
        [c].[collation_name] AS [Collation],
        [c].[is_xml_document] AS [IsXmlDocument],
        [c].[xml_collection_id] AS [XmlCollectionId],
        [xscs].[name] AS [XmlCollection],
        SCHEMA_NAME([xscs].[schema_id]) AS [XmlCollectionSchemaName],
        [c].[is_sparse]                 AS [IsSparse],
        [c].[is_column_set]             AS [IsColumnSet],
        [c].[is_filestream]             AS [IsFilestream],
        [c].[generated_always_type]     AS [GeneratedAlwaysType],
        [mc].[masking_function]         AS [MaskingFunction],
        [c].[is_hidden]                 AS [IsHidden],
        CASE [ta].[is_filetable] WHEN 1 THEN N'UF' ELSE N'U ' END AS [Type],
        CONVERT(bit, ISNULL([cc].[is_persisted], 0)) AS [IsPersisted],
        CAST(ISNULL([indexCol].[IsPartitionColumn],0) AS BIT) AS [IsPartitionColumn],
        ISNULL([c].[encryption_type], 0) AS [EncryptionType],
        [c].[encryption_algorithm_name] AS [EncryptionAlgorithmName],
        [c].[column_encryption_key_id] AS [ColumnEncryptionKeyId],
        [cek].[name] AS [ColumnEncryptionKeyName],
        CAST(0 AS BIT) AS [IsPrimaryKey],
        CAST(0 AS BIT) AS [IsForeignKey]
FROM    [sys].[columns] [c] WITH (NOLOCK)
INNER   JOIN [sys].[objects] [t] WITH (NOLOCK) ON [c].[object_id] = [t].[object_id]
LEFT	JOIN [sys].[tables] [ta] WITH(NOLOCK) ON [ta].[object_id] = [t].[object_id]
LEFT    JOIN [sys].[types] [basetypes] WITH (NOLOCK) ON [c].[system_type_id] = [basetypes].[system_type_id] AND [basetypes].[system_type_id] = [basetypes].[user_type_id]
LEFT    JOIN [sys].[types] [types] WITH (NOLOCK) ON [c].[user_type_id] = [types].[user_type_id]
LEFT    JOIN [sys].[identity_columns] [ic] WITH (NOLOCK) ON [ic].[object_id] = [c].[object_id] AND [ic].[column_id] = [c].[column_id]
LEFT    JOIN [sys].[computed_columns] [cc] WITH (NOLOCK) ON [cc].[object_id] = [c].[object_id] AND [cc].[column_id] = [c].[column_id]
LEFT    JOIN [sys].[masked_columns] [mc] WITH (NOLOCK) ON [mc].[object_id] = [c].[object_id] AND [mc].[column_id] = [c].[column_id]
LEFT    JOIN [sys].[xml_schema_collections] [xscs] WITH (NOLOCK) ON [xscs].[xml_collection_id] = [c].[xml_collection_id]
LEFT    JOIN [sys].[column_encryption_keys] [cek] WITH (NOLOCK) ON [cek].[column_encryption_key_id] = [c].[column_encryption_key_id]
LEFT    JOIN (
    SELECT 1 AS [IsPartitionColumn], [indexCol].[column_id], [ix].[object_id] FROM [sys].[index_columns] [indexCol] WITH (NOLOCK) 
    INNER JOIN [sys].[indexes] [ix] WITH (NOLOCK) ON [indexCol].[index_id] = [ix].[index_id] AND [ix].[is_hypothetical] = 0 AND [ix].[type] IN (0,1,5) and [ix].[object_id] = [indexCol].[object_id]
    WHERE [indexCol].[partition_ordinal] > 0) AS [indexCol] ON [c].[object_id] = [indexCol].[object_id] AND [c].[column_id] = [indexCol].[column_id]
WHERE   [t].[type] = N'U' AND ISNULL([ta].[is_filetable],0) = 0 AND ISNULL([ta].[is_external],0) = 0
AND ([t].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [t].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) AS [_results] ORDER BY TableId,ColumnId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110FileTablePopulator' as [PopulatorName];
SELECT * FROM (
SELECT  
        [t].[schema_id]               AS [SchemaId],
        SCHEMA_NAME([t].[schema_id])  AS [SchemaName],
        [t].[name]                    AS [ColumnSourceName],
        [t].[object_id]               AS [TableId],
        CASE [st].[is_filetable] WHEN 1 THEN N'UF' ELSE N'U ' END                           AS [Type],
        [ds].[type]                   AS [DataspaceType],
        [ds].[data_space_id]          AS [DataspaceId],
        [ds].[name]                   AS [DataspaceName],
        [si].[index_id]               AS [IndexId],
        [si].[type]                   AS [IndexType],
        CASE WHEN exists(SELECT 1 FROM [sys].[columns] AS [c] WITH (NOLOCK) WHERE [c].[object_id] = [t].[object_id] AND  ([c].[system_type_id] IN (34, 35, 99, 241) OR ( [c].[system_type_id] in (165, 167,231,240) AND [c].[max_length] = -1))) THEN
            [dsx].[data_space_id] 
        ELSE
            NULL
        END AS [TextFilegroupId],
        [dsx].[name]                  AS [TextFilegroupName],
        CASE WHEN exists(SELECT 1 FROM [sys].[columns] AS [c] WITH (NOLOCK) WHERE [c].[object_id] = [st].[object_id] AND  [c].[is_filestream] = 1) THEN
            [dsf].[data_space_id] 
        ELSE
            NULL
        END AS [FileStreamId],
        [dsf].[name]                  AS [FileStreamName],
        [dsf].[type]                  AS [FileStreamType],
        [st].[uses_ansi_nulls]        AS [IsAnsiNulls],
        CAST(OBJECTPROPERTY([t].[object_id],N'IsQuotedIdentOn') AS bit) AS [IsQuotedIdentifier],
        CAST([st].[lock_on_bulk_load] AS bit) 
                                      AS [IsLockedOnBulkLoad],
        [st].[text_in_row_limit]      AS [TextInRowLimit],
        CAST([st].[large_value_types_out_of_row] AS bit)
                                      AS [LargeValuesOutOfRow],
        CAST(OBJECTPROPERTY([t].[object_id], N'TableHasVarDecimalStorageFormat') AS bit)
                                      AS [HasVarDecimalStorageFormat],
        [st].[is_tracked_by_cdc]      AS [IsTrackedByCDC],
        CAST(CASE
             WHEN [ctt].[object_id] IS NULL THEN 0
             ELSE 1
             END AS BIT)              AS [IsChangeTrackingOn],
        [ctt].[is_track_columns_updated_on] AS [IsTrackColumnsUpdatedOn],
        [st].[lock_escalation]        AS [LockEscalation],
        CASE WHEN [st].[is_replicated] <> 0 OR [st].[is_merge_published] <> 0 OR [st].[is_schema_published] <> 0 OR [st].[is_published] <> 0 THEN 1 ELSE 0 END AS [ReplInfo],
        [sft].[is_enabled]            AS [FileTableEnabled],
        [sft].[directory_name]        AS [FileTableDirectoryName],
        [sft].[filename_collation_id] AS [FileTableCollationId],
        [sft].[filename_collation_name] AS [FileTableCollation],
        [st].[create_date] AS [CreateDate]
FROM
        [sys].[objects] [t] WITH (NOLOCK)
        LEFT    JOIN [sys].[tables] [st] WITH (NOLOCK) ON [t].[object_id] = [st].[object_id]
        LEFT    JOIN [sys].[indexes] [si] WITH (NOLOCK) ON [si].[object_id] = [st].[object_id]
        LEFT    JOIN [sys].[data_spaces] [ds] WITH (NOLOCK) ON [ds].[data_space_id] = [si].[data_space_id]
        LEFT    JOIN [sys].[data_spaces] [dsx] WITH (NOLOCK) ON [dsx].[data_space_id] = [st].[lob_data_space_id]
        LEFT    JOIN [sys].[data_spaces] [dsf] WITH (NOLOCK) ON [dsf].[data_space_id] = [st].[filestream_data_space_id]
        LEFT    JOIN [sys].[change_tracking_tables] [ctt] WITH (NOLOCK) ON [ctt].[object_id] = [st].[object_id]
        LEFT    JOIN [sys].[filetables] [sft] WITH (NOLOCK) ON [sft].[object_id] = [st].[object_id]
WHERE   [t].[type] = N'U' AND ISNULL([st].[is_filetable],0) = 1 AND ISNULL([si].[index_id],0) < 2
AND ([t].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [t].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) AS [_results] ORDER BY TableId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110FileTableColumnPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [t].[name] AS [ColumnSourceName], 
        [t].[object_id] AS [TableId],
        SCHEMA_NAME([t].[schema_id]) AS [SchemaName], 
        [c].[name] AS [ColumnName], 
        [c].[user_type_id] AS [TypeId],
        CONVERT(bit, ISNULL([types].[is_user_defined], 0)) AS [IsUserDefinedType],
        [types].[name] AS [TypeName],
        [basetypes].[name] AS [BaseTypeName],
        SCHEMA_NAME([types].[schema_id]) AS [TypeSchemaName],
        [c].[column_id] AS [ColumnId], 
        [c].[precision] AS [Precision],
        [c].[scale] AS [Scale],
        CASE WHEN [c].[max_length] >= 0 AND [types].[name] IN (N'nchar', N'nvarchar') THEN ([c].[max_length] / 2) ELSE [c].[max_length] END AS [Length],
        CONVERT(bit, [c].[is_identity]) AS [IsIdentity],
        CONVERT(bit, [c].[is_computed]) AS [IsComputed],
        CONVERT(bit, ISNULL([ic].[is_not_for_replication], 0)) AS [IsNotForReplication],
        CAST(ISNULL([ic].[seed_value], 0) AS DECIMAL(38)) AS [IdentitySeed],
        CAST(ISNULL([ic].[increment_value], 0) AS DECIMAL(38)) AS [IdentityIncrement],
        CONVERT(bit, [c].[is_nullable]) AS [IsNullable],
        [cc].[definition] AS [ComputedText],
        [c].[is_rowguidcol] AS [IsRowGuidColumn],
        [c].[collation_name] AS [Collation],
        [c].[is_xml_document] AS [IsXmlDocument],
        [c].[xml_collection_id] AS [XmlCollectionId],
        [xscs].[name] AS [XmlCollection],
        SCHEMA_NAME([xscs].[schema_id]) AS [XmlCollectionSchemaName],
        [c].[is_sparse]                 AS [IsSparse],
        [c].[is_column_set]              AS [IsColumnSet],
        [c].[is_filestream]             AS [IsFilestream],
        CASE [ta].[is_filetable] WHEN 1 THEN N'UF' ELSE N'U ' END AS [Type],
        CONVERT(bit, ISNULL([cc].[is_persisted], 0)) AS [IsPersisted],
        CAST(ISNULL([indexCol].[IsPartitionColumn],0) AS BIT) AS [IsPartitionColumn],
        CAST(0 AS BIT) AS [IsPrimaryKey],
        CAST(0 AS BIT) AS [IsForeignKey]
FROM    [sys].[columns] [c] WITH (NOLOCK)
INNER   JOIN [sys].[objects] [t] WITH (NOLOCK) ON [c].[object_id] = [t].[object_id]
LEFT	JOIN [sys].[tables] [ta] WITH(NOLOCK) ON [ta].[object_id] = [t].[object_id]
LEFT    JOIN [sys].[types] [basetypes] WITH (NOLOCK) ON [c].[system_type_id] = [basetypes].[system_type_id] AND [basetypes].[system_type_id] = [basetypes].[user_type_id]
LEFT    JOIN [sys].[types] [types] WITH (NOLOCK) ON [c].[user_type_id] = [types].[user_type_id]
LEFT    JOIN [sys].[identity_columns] [ic] WITH (NOLOCK) ON [ic].[object_id] = [c].[object_id] AND [ic].[column_id] = [c].[column_id]
LEFT    JOIN [sys].[computed_columns] [cc] WITH (NOLOCK) ON [cc].[object_id] = [c].[object_id] AND [cc].[column_id] = [c].[column_id]
LEFT    JOIN [sys].[xml_schema_collections] [xscs] WITH (NOLOCK) ON [xscs].[xml_collection_id] = [c].[xml_collection_id]
LEFT    JOIN (
    SELECT 1 AS [IsPartitionColumn], [indexCol].[column_id], [ix].[object_id] FROM [sys].[index_columns] [indexCol] WITH (NOLOCK) 
    INNER JOIN [sys].[indexes] [ix] WITH (NOLOCK) ON [indexCol].[index_id] = [ix].[index_id] AND [ix].[is_hypothetical] = 0 AND [ix].[type] IN (0,1) and [ix].[object_id] = [indexCol].[object_id]
    WHERE [indexCol].[partition_ordinal] > 0) AS [indexCol] ON [c].[object_id] = [indexCol].[object_id] AND [c].[column_id] = [indexCol].[column_id]
WHERE   [t].[type] = N'U' AND ISNULL([ta].[is_filetable],0) = 1
AND ([t].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [t].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) AS [_results] ORDER BY TableId,ColumnId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql120PrimaryKeyPopulator' as [PopulatorName];
SELECT * FROM (
SELECT DISTINCT
    SCHEMA_NAME([o].[schema_id]) AS [SchemaName]
   ,[i].[object_id]        AS [ColumnSourceId]
   ,[o].[name]             AS [ColumnSourceName]
   ,CASE [ta].[is_filetable] WHEN 1 THEN N'UF' ELSE [o].[type] END                    AS [ColumnSourceType]
   ,[kc].[object_id]       AS [ConstraintId]
   ,[i].[name]             AS [ConstraintName]
   ,CAST(CASE 
    WHEN [kc].[is_system_named] = 1 AND [ft].[object_id] IS NOT NULL THEN 1
    WHEN [kc].[is_system_named] = 1 AND EXISTS(SELECT TOP 1 1 FROM sys.extended_properties WITH (NOLOCK) WHERE [class] = 1 AND major_id = [kc].[object_id]) THEN 1
    ELSE 0 END AS BIT) AS [PromoteName]
   ,CAST(CASE [kc].[is_system_named]
               WHEN 1 THEN 1
               ELSE CASE WHEN [ftc].[object_id] IS NULL THEN 0 ELSE NULL END
             END AS BIT) AS [IsSystemNamed]
   ,[f].[type]             AS [DataspaceType]
   ,[i].[data_space_id]    AS [DataspaceId]
   ,[f].[name]             AS [DataspaceName]
   ,[fs].[data_space_id]   AS [FileStreamId]
   ,[fs].[name]            AS [FileStreamName]
   ,[fs].[type]            AS [FileStreamType]
   ,[i].[fill_factor]      AS [FillFactor]    
   ,CONVERT(bit, CASE [i].[type] WHEN 1 THEN 1 ELSE 0 END) 
                           AS [IsClustered]
   ,[i].[is_unique]        AS [IsUnique]
   ,[i].[is_padded]        AS [IsPadded]
   ,[i].[ignore_dup_key]   AS [DoIgnoreDuplicateKey]
   ,[t].[no_recompute]     AS [NoRecomputeStatistics]
   ,[i].[allow_row_locks]  AS [DoAllowRowLocks]
   ,[i].[allow_page_locks] AS [DoAllowPageLocks]
   ,NULL                   AS [PartitionSchemeName]
   ,NULL                   AS [PartitionColumnName]
   ,[i].[is_disabled]      AS [IsDisabled]
   ,CONVERT(bit, CASE WHEN [ti].[data_space_id] <> [i].[data_space_id] THEN 0 ELSE 1 END)
                           AS [EqualsParentDataSpace]
   ,CAST(CASE WHEN [ftc].[object_id] IS NULL THEN 0 ELSE 1 END AS BIT) AS [IsFileTableSystemConstraint]
   ,CONVERT(BIT, CASE WHEN [hi].[object_id] IS NULL THEN 0 ELSE 1 END) AS [IsHash]
   ,[hi].[bucket_count] AS [BucketCount]
   ,CAST(CASE [ta].[is_memory_optimized] WHEN 1 THEN 1 ELSE 0 END AS BIT) AS [IsFromMemoryOptimizedTable]
FROM 
    [sys].[indexes] AS [i] WITH (NOLOCK)
    INNER JOIN [sys].[objects]           AS [o]  WITH (NOLOCK) ON [i].[object_id] = [o].[object_id]
    INNER JOIN [sys].[key_constraints]   AS [kc] WITH (NOLOCK) ON [i].[object_id] = [kc].[parent_object_id] AND [i].[index_id] = [kc].[unique_index_id]
    LEFT  JOIN [sys].[data_spaces]       AS [f]  WITH (NOLOCK) ON [i].[data_space_id] = [f].[data_space_id]
    LEFT  JOIN [sys].[stats]             AS [t]  WITH (NOLOCK) ON [t].[object_id] = [i].[object_id] AND [t].[name] = [i].[name]
    LEFT  JOIN [sys].[tables]            AS [ta] WITH (NOLOCK) ON [ta].[object_id] = [i].[object_id]
    LEFT  JOIN [sys].[data_spaces]       AS [fs] WITH (NOLOCK) ON [fs].[data_space_id] = [ta].[filestream_data_space_id]
    LEFT  JOIN (SELECT * FROM [sys].[indexes] WITH (NOLOCK) WHERE [index_id] < 2) AS [ti] ON [o].[object_id] = [ti].[object_id]
    LEFT JOIN [sys].[filetable_system_defined_objects] AS [ftc] WITH (NOLOCK) ON [ftc].[parent_object_id] = [ta].[object_id] AND [ftc].[object_id] = [kc].[object_id]
    LEFT OUTER JOIN [sys].[fulltext_indexes] AS [ft] WITH (NOLOCK) ON [i].[object_id] = [ft].[object_id] AND [i].[index_id] = [ft].[unique_index_id]
    LEFT OUTER JOIN [sys].[hash_indexes] AS [hi] WITH (NOLOCK) ON [hi].[object_id] = [i].[object_id] AND [hi].[index_id] = [i].[index_id]
WHERE 
    ([o].[type]  = N'U' OR [o].[type] = N'TF') AND [i].[is_primary_key] = 1 
    AND [i].[name] IS NOT NULL
    AND [i].[is_hypothetical] = 0
    AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) AS [_results] ORDER BY ColumnSourceId,ConstraintId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql100ConstraintColumnSpecificationPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  
    SCHEMA_NAME([o].[schema_id])  AS [SchemaName]
   ,[o].[name]               AS [ColumnSourceName]
   ,[i].[object_id]          AS [ColumnSourceId]
   ,CASE [t].[is_filetable] WHEN 1 THEN N'UF' ELSE [o].[type] END               AS [ColumnSourceType]
   ,[kc].[object_id]         AS [IndexId]
   ,[i].[name]               AS [IndexName]
   ,[c].[column_id]          AS [ColumnId]
   ,[c].[name]               AS [ColumnName]
   ,[ic].[is_descending_key] AS [IsDescending]
   ,[ic].[is_included_column] AS [IsIncludedColumn]
   ,[ic].[partition_ordinal]  AS [PartitionOrdinal]
   ,[ic].[key_ordinal]        AS [KeyOrdinal]
   ,[kc].[name]               AS [ConstraintName]
FROM 
    [sys].[columns] AS [c] WITH (NOLOCK)
    LEFT JOIN [sys].[index_columns] AS [ic] WITH (NOLOCK) ON [c].[object_id] = [ic].[object_id] AND [c].[column_id] = [ic].[column_id]
    LEFT JOIN [sys].[indexes]       AS [i] WITH (NOLOCK) ON [i].[object_id] = [ic].[object_id] AND [i].[index_id] = [ic].[index_id]
    LEFT JOIN [sys].[objects]   AS [o] WITH (NOLOCK) ON [o].[object_id] = [i].[object_id]
    LEFT JOIN [sys].[key_constraints] AS [kc] WITH (NOLOCK) ON [i].[object_id] = [kc].[parent_object_id] AND [i].[index_id] = [kc].[unique_index_id]
    LEFT JOIN [sys].[tables] AS [t] WITH(NOLOCK) ON [t].[object_id] = [c].[object_id]
WHERE 
    ([o].[type] = N'U' OR [o].[type] = N'TF') 
    AND [i].[is_primary_key] = 1
    AND [i].[name] IS NOT NULL
    AND [i].[is_hypothetical] = 0
    AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))
) AS [_results] ORDER BY ColumnSourceId,IndexId,KeyOrdinal ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql130UniqueConstraintPopulator' as [PopulatorName];
SELECT * FROM (
SELECT DISTINCT
            SCHEMA_NAME([o].[schema_id]) AS [SchemaName]
           ,[i].[object_id]        AS [ColumnSourceId]
           ,[o].[name]             AS [ColumnSourceName]
           ,[o].[type]             AS [ColumnSourceType]
           ,[kc].[object_id]       AS [ConstraintId]
           ,[i].[name]             AS [ConstraintName]
           ,CAST(CASE 
            WHEN [kc].[is_system_named] = 1 AND [ft].[object_id] IS NOT NULL THEN 1
            WHEN [kc].[is_system_named] = 1 AND EXISTS(SELECT TOP 1 1 FROM sys.extended_properties WITH (NOLOCK) WHERE [class] = 1 AND major_id = [kc].[object_id]) THEN 1
            ELSE 0 END AS BIT) AS [PromoteName]
            ,CAST(CASE [kc].[is_system_named]
                       WHEN 1 THEN 1
                       ELSE CASE WHEN [ftc].[object_id] IS NULL THEN 0 ELSE NULL END
                     END AS BIT) AS [IsSystemNamed]
           ,[f].[type]             AS [DataspaceType]
           ,[i].[data_space_id]    AS [DataspaceId]
           ,[f].[name]             AS [DataspaceName]
           ,[fs].[data_space_id]   AS [FileStreamId]
           ,[fs].[name]            AS [FileStreamName]
           ,[fs].[type]            AS [FileStreamType]
           ,[i].[fill_factor]      AS [FillFactor]    
           ,CONVERT(bit, CASE [i].[type] WHEN 1 THEN 1 ELSE 0 END) 
                                   AS [IsClustered]
           ,[i].[is_unique]        AS [IsUnique]
           ,[i].[is_padded]        AS [IsPadded]
           ,[i].[ignore_dup_key]   AS [DoIgnoreDuplicateKey]
           ,[t].[no_recompute]     AS [NoRecomputeStatistics]
           ,[i].[allow_row_locks]  AS [DoAllowRowLocks]
           ,[i].[allow_page_locks] AS [DoAllowPageLocks]
           ,CONVERT(bit, CASE WHEN [ti].[data_space_id] <> [i].[data_space_id] THEN 0 ELSE 1 END)
                           AS [EqualsParentDataSpace]
           ,CAST(CASE WHEN [ftc].[object_id] IS NULL THEN 0 ELSE 1 END AS BIT) AS [IsFileTableSystemConstraint]
           ,CAST(ISNULL([ic].[column_id], 0) AS BIT) AS [IsStreamIdFileTableSystemConstraint]
            ,CONVERT(BIT, CASE WHEN [hi].[object_id] IS NULL THEN 0 ELSE 1 END) AS [IsHash]
           ,[hi].[bucket_count] AS [BucketCount]
           ,CAST(CASE [ta].[is_memory_optimized] WHEN 1 THEN 1 ELSE 0 END AS BIT) AS [IsFromMemoryOptimizedTable]
FROM 
            [sys].[indexes] AS [i] WITH (NOLOCK)
            INNER JOIN [sys].[objects]           AS [o]  WITH (NOLOCK) ON [i].[object_id] = [o].[object_id]
            INNER JOIN [sys].[key_constraints]   AS [kc] WITH (NOLOCK) ON [i].[object_id] = [kc].[parent_object_id] AND [i].[index_id] = [kc].[unique_index_id]
            LEFT JOIN [sys].[data_spaces]       AS [f]  WITH (NOLOCK) ON [i].[data_space_id] = [f].[data_space_id]
            LEFT JOIN [sys].[stats]             AS [t]  WITH (NOLOCK) ON [t].[object_id] = [i].[object_id] AND [t].[name] = [i].[name]
            LEFT JOIN [sys].[tables]            AS [ta] WITH (NOLOCK) ON [ta].[object_id] = [i].[object_id]
            LEFT JOIN [sys].[data_spaces]       AS [fs] WITH (NOLOCK) ON [fs].[data_space_id] = [ta].[filestream_data_space_id]
            LEFT JOIN [sys].[indexes]          AS [ti] WITH (NOLOCK) ON [o].[object_id] = [ti].[object_id] AND [ti].[index_id] < 2
            LEFT JOIN [sys].[filetable_system_defined_objects] AS [ftc] WITH (NOLOCK) ON [ftc].[parent_object_id] = [ta].[object_id] AND [ftc].[object_id] = [kc].[object_id]
            LEFT JOIN [sys].[index_columns] [ic] WITH (NOLOCK) ON [ic].column_id = 1 AND [ic].[object_id] = [i].[object_id] AND [ic].[index_id] = [i].[index_id] AND [ic].[object_id] = [ftc].[parent_object_id]
            LEFT JOIN [sys].[fulltext_indexes] AS [ft] WITH (NOLOCK) ON [i].[object_id] = [ft].[object_id] AND [i].[index_id] = [ft].[unique_index_id]
            LEFT JOIN [sys].[hash_indexes] AS [hi] WITH (NOLOCK) ON [hi].[object_id] = [i].[object_id] AND [hi].[index_id] = [i].[index_id]
WHERE 
            ([o].[type]  = N'U' OR [o].[type] = N'TF') 
            AND [i].[is_unique_constraint] = 1
            AND [i].[name] IS NOT NULL
            AND [i].[is_hypothetical] = 0
            AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) AS [_results] ORDER BY ColumnSourceId,ConstraintId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90ConstraintColumnSpecificationPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  
    SCHEMA_NAME([o].[schema_id])  AS [SchemaName]
   ,[i].[object_id]          AS [ColumnSourceId]
   ,[o].[name]               AS [ColumnSourceName]
   ,[o].[type]               AS [ColumnSourceType]
   ,[i].[name]               AS [ConstraintName]
   ,[kc].[object_id]         AS [IndexId]
   ,[i].[name]               AS [IndexName]
   ,[c].[column_id]          AS [ColumnId]
   ,[c].[name]               AS [ColumnName]
   ,[ic].[is_descending_key] AS [IsDescending]
   ,[ic].[is_included_column] AS [IsIncludedColumn]
   ,[ic].[partition_ordinal]  AS [PartitionOrdinal]
   ,[ic].[key_ordinal]        AS [KeyOrdinal]
FROM 
    [sys].[columns] AS [c] WITH (NOLOCK)    
    LEFT JOIN [sys].[index_columns] AS [ic] WITH (NOLOCK) ON [c].[object_id] = [ic].[object_id] AND [c].[column_id] = [ic].[column_id]
    LEFT JOIN [sys].[indexes]       AS [i] WITH (NOLOCK) ON [i].[object_id] = [ic].[object_id] AND [i].[index_id] = [ic].[index_id]
    LEFT JOIN [sys].[objects]   AS [o] WITH (NOLOCK) ON [o].[object_id] = [i].[object_id]
    LEFT JOIN [sys].[key_constraints] AS [kc] WITH (NOLOCK) ON [i].[object_id] = [kc].[parent_object_id] AND [i].[index_id] = [kc].[unique_index_id]
    LEFT JOIN [sys].[tables] AS [t] WITH (NOLOCK) ON [t].[object_id] = [c].[object_id]
WHERE 
    ([o].[type] = N'U' OR [o].[type] = N'TF') 
    AND [i].[is_unique_constraint] = 1    
    AND [i].[name] IS NOT NULL
    AND [i].[is_hypothetical] = 0
    AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) AS [_results] ORDER BY ColumnSourceId,IndexId,KeyOrdinal ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110ForeignKeyPopulator' as [PopulatorName];
SELECT * FROM (
SELECT DISTINCT
        SCHEMA_NAME([fs].[schema_id])  AS [SchemaName]
       ,[fs].[name]                    AS [ColumnSourceName]
       ,CASE [fs].[is_filetable] WHEN 1 THEN N'UF' ELSE [fs].[type] END AS [ColumnSourceType]
       ,[sfk].[name]                   AS [ConstraintName]
       ,CAST(CASE 
            WHEN [sfk].[is_system_named] = 1 AND [sfk].[is_disabled] = 1 THEN 1
            WHEN [sfk].[is_system_named] = 1 AND EXISTS(SELECT TOP 1 1 FROM sys.extended_properties WITH (NOLOCK) WHERE [class] = 1 AND major_id = [sfk].[object_id]) THEN 1
            ELSE 0 END AS BIT) AS [PromoteName]
        ,CAST(CASE [sfk].[is_system_named]
               WHEN 1 THEN 1
               ELSE CASE WHEN [ftc].[object_id] IS NULL THEN 0 ELSE NULL END
             END AS BIT) AS [IsSystemNamed]
       ,[sfk].[parent_object_id]       AS [ColumnSourceId]
       ,[sfk].[object_id]              AS [ConstraintId]
       ,[sfk].[referenced_object_id]   AS [ReferencedColumnSourceId]
       ,SCHEMA_NAME([rs].[schema_id])  AS [ReferencedSchemaName]
       ,[rs].[name]                    AS [ReferencedColumnSourceName]
       ,[sfk].[is_disabled]            AS [IsDisabled]
       ,[sfk].[is_not_for_replication] AS [IsNotForReplication]
       ,[sfk].[is_not_trusted]         AS [IsNotTrusted]
       ,[sfk].[update_referential_action] AS [UpdateAction]
       ,[sfk].[delete_referential_action] AS [DeleteAction]
       ,CAST(CASE WHEN [ftc].[object_id] IS NULL THEN 0 ELSE 1 END AS BIT) AS [IsFileTableSystemConstraint]
       ,[si].[name]                       AS [ReferencedIndexName]
FROM    
        [sys].[foreign_keys] AS [sfk] WITH (NOLOCK)
        INNER JOIN [sys].[tables] [fs] WITH (NOLOCK) ON [sfk].[parent_object_id] = [fs].[object_id]
        INNER JOIN [sys].[tables] [rs] WITH (NOLOCK) ON [sfk].[referenced_object_id] = [rs].[object_id] 
        INNER JOIN [sys].[indexes] [si] WITH (NOLOCK) ON [sfk].[referenced_object_id] = [si].[object_id] AND [sfk].[key_index_id] = [si].[index_id]
        LEFT JOIN [sys].[filetable_system_defined_objects] AS [ftc] WITH (NOLOCK) ON [ftc].[parent_object_id] = [fs].[object_id] AND [ftc].[object_id] = [sfk].[object_id]
WHERE   [ftc].[object_id] IS NULL AND ([fs].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [fs].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) AS [_results] ORDER BY ColumnSourceId,ConstraintId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.SqlForeignKeyColumnPopulator' as [PopulatorName];
SELECT * FROM (
SELECT 
        SCHEMA_NAME([fs].[schema_id])  AS [SchemaName]
       ,[sfk].[name]                   AS [ConstraintName]
       ,[fs].[name]                    AS [ColumnSourceName]
       ,CASE [fs].[is_filetable] WHEN 1 THEN N'UF' ELSE [fs].[type] END AS [ColumnSourceType]
       ,[sfk].[parent_object_id]       AS [ColumnSourceId]
       ,[sfk].[object_id]              AS [ConstraintId]
       ,[sfkc].[parent_column_id]      AS [ColumnId]
       ,COL_NAME([sfk].[parent_object_id],[sfkc].[parent_column_id]) AS [ColumnName]
       ,[sfkc].[referenced_object_id]  AS [ReferencedColumnSourceId]
       ,[sfkc].[referenced_column_id]  AS [ReferencedColumnId]  
       ,SCHEMA_NAME([rs].[schema_id])  AS [ReferencedSchemaName]
       ,[rs].[name]       AS [ReferencedColumnSourceName] 
       ,COL_NAME([sfk].[referenced_object_id],[sfkc].[referenced_column_id]) AS [ReferencedColumnName]    
       ,[sfkc].[constraint_column_id] AS  [KeyOrdinal]
FROM    [sys].[foreign_keys] AS [sfk] WITH (NOLOCK)
INNER   JOIN [sys].[foreign_key_columns] AS [sfkc] WITH (NOLOCK) ON [sfk].[object_id] = [sfkc].[constraint_object_id]
INNER   JOIN [sys].[tables] [fs] WITH (NOLOCK) ON [sfk].[parent_object_id] = [fs].[object_id]
INNER   JOIN [sys].[tables] [rs] WITH (NOLOCK) ON [sfk].[referenced_object_id] = [rs].[object_id] 
LEFT    JOIN [sys].[filetable_system_defined_objects] AS [ftc] WITH (NOLOCK) ON [ftc].[parent_object_id] = [fs].[object_id] AND [ftc].[object_id] = [sfk].[object_id]
WHERE   [ftc].[object_id] IS NULL AND ([fs].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [fs].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) AS [_results] ORDER BY ColumnSourceId,ConstraintId,KeyOrdinal ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110DefaultConstraintPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [d].[object_id]                     AS [ConstraintId]
       ,SCHEMA_NAME([d].[schema_id])        AS [SchemaName]
       ,[d].[name]                          AS [ConstraintName]
       ,CAST(CASE 
        WHEN [d].[is_system_named] = 1 AND EXISTS(SELECT TOP 1 1 FROM sys.extended_properties WITH (NOLOCK) WHERE [class] = 1 AND major_id = [d].[object_id]) THEN 1
        ELSE 0 END AS BIT) AS [PromoteName]
      ,CAST(CASE [d].[is_system_named]
               WHEN 1 THEN 1
               ELSE CASE WHEN [ftc].[object_id] IS NULL THEN 0 ELSE NULL END
             END AS BIT) AS [IsSystemNamed]
       ,[d].[parent_object_id]              AS [TableId]
       ,[o].[name]                          AS [TableName]
       ,CASE [ta].[is_filetable] WHEN 1 THEN N'UF' ELSE [o].[type] END                                 AS [ColumnSourceType]
       ,[c].[column_id]                     AS [ColumnId]
       ,[c].[name]                          AS [ColumnName]
       ,[d].[definition]                    AS [Script]
       ,CAST(CASE WHEN [ftc].[object_id] IS NULL THEN 0 ELSE 1 END AS BIT) AS [IsFileTableSystemConstraint]
       ,CAST(CASE [ta].[is_memory_optimized] WHEN 1 THEN 1 ELSE 0 END AS BIT) AS [IsFromMemoryOptimizedTable]
FROM
        [sys].[default_constraints]    AS [d] WITH (NOLOCK)
        INNER JOIN [sys].[columns] AS [c] WITH (NOLOCK) ON [c].[object_id] = [d].[parent_object_id] AND [c].[column_id] = [d].[parent_column_id]
        INNER JOIN [sys].[objects]     AS [o] WITH (NOLOCK) ON [o].[object_id] = [d].[parent_object_id]
        LEFT JOIN [sys].[tables] as [ta] WITH (NOLOCK) ON [ta].[object_id] = [o].[object_id]
        LEFT JOIN [sys].[filetable_system_defined_objects] AS [ftc] WITH (NOLOCK) ON [ftc].[parent_object_id] = [ta].[object_id] AND [ftc].[object_id] = [d].[object_id]
WHERE
        [ftc].[object_id] IS NULL
        AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))
        AND OBJECTPROPERTY([d].[parent_object_id], N'IsMSShipped') = 0
        AND OBJECTPROPERTY([d].[parent_object_id], N'IsSystemTable') = 0
) AS [_results] ORDER BY TableId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110CheckConstraintPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        SCHEMA_NAME([cc].[schema_id])        AS [SchemaName]
       ,[cc].[name]                          AS [ConstraintName]
       ,CAST(CASE 
        WHEN [cc].[is_system_named] = 1 AND [cc].[is_disabled]  = 1 THEN 1
        WHEN [cc].[is_system_named] = 1 AND EXISTS(SELECT TOP 1 1 FROM sys.extended_properties WITH (NOLOCK) WHERE [class] = 1 AND major_id = [cc].[object_id]) THEN 1
        ELSE 0 END AS BIT) AS [PromoteName]
       ,CAST(CASE [cc].[is_system_named]
               WHEN 1 THEN 1
               ELSE CASE WHEN [ftc].[object_id] IS NULL THEN 0 ELSE NULL END
             END AS BIT) AS [IsSystemNamed]
       ,[cc].[parent_object_id]              AS [TableId]
       ,[o].[name]                           AS [TableName]
       ,CASE [ta].[is_filetable] WHEN 1 THEN N'UF' ELSE [o].[type] END                                  AS [ColumnSourceType]
       ,[cc].[definition]                    AS [Script]
       ,[cc].[is_not_for_replication]        AS [IsNotForReplication]
       ,[cc].[is_not_trusted]                AS [IsNotTrusted]
       ,[cc].[is_disabled]                   AS [IsDisabled]
       ,[cc].[object_id]                     AS [ConstraintId]
       ,CAST(CASE WHEN [ftc].[object_id] IS NULL THEN 0 ELSE 1 END AS BIT) AS [IsFileTableSystemConstraint]
FROM
        [sys].[check_constraints] AS [cc] WITH (NOLOCK)
        INNER JOIN [sys].[objects] [o] WITH (NOLOCK) ON [o].[object_id] = [cc].[parent_object_id]
        LEFT JOIN [sys].[tables] AS [ta] WITH (NOLOCK) ON [ta].[object_id] = [o].[object_id]
        LEFT JOIN [sys].[filetable_system_defined_objects] AS [ftc] WITH (NOLOCK) ON [ftc].[parent_object_id] = [ta].[object_id] AND [ftc].[object_id] = [cc].[object_id]
WHERE   [ftc].[object_id] IS NULL AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))
) AS [_results] ORDER BY TableId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90ViewPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [sv].[schema_id]              AS [SchemaId]
       ,SCHEMA_NAME([sv].[schema_id]) AS [SchemaName]
       ,[sv].[object_id]              AS [ViewId]
       ,[sv].[name]                   AS [ColumnSourceName]
       ,CONVERT(bit, OBJECTPROPERTY([sv].[object_id], N'ExecIsQuotedIdentOn'))
                                      AS [IsQuotedIdentifier]
       ,CONVERT(bit, OBJECTPROPERTY([sv].[object_id], N'ExecIsAnsiNullsOn'))
                                      AS [IsAnsiNulls]
       ,[sm].[definition]             AS [Script]
       ,CASE WHEN [so].[is_published] <> 0 OR [so].[is_schema_published] <> 0 THEN 1 ELSE 0 END AS [ReplInfo]
       ,[sv].[create_date] AS [CreatedDate]
       ,[sv].[with_check_option]      AS [WithCheckOption]
       ,[sm].[is_schema_bound]        AS [IsSchemaBound]
       ,[sv].[has_opaque_metadata]    AS [HasOpaqueMetadata]
       ,NULL AS [HasAmbiguousReference]
FROM
        [sys].[all_views] [sv] WITH (NOLOCK)
        LEFT JOIN [sys].[sql_modules] [sm] WITH (NOLOCK) ON [sm].[object_id] = [sv].[object_id]
        LEFT JOIN [sys].[objects] [so] WITH (NOLOCK) ON [so].[object_id] = [sv].[object_id]
        
WHERE ([sv].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [sv].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )) AND OBJECTPROPERTY([sv].[object_id], N'IsEncrypted') = 0
) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.SqlViewColumnPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        SCHEMA_NAME([v].[schema_id]) AS [SchemaName],
        [v].[name]      AS [ColumnSourceName],
        [clmns].[name]  AS [ColumnName],
        [v].[object_id]        AS [ViewId],
        [clmns].[column_id] AS [ColumnId],
        CAST(0  AS bit) AS [IsForeignKey],
        USER_NAME([usrt].[schema_id])     AS [TypeSchemaName],
        [usrt].[name]               AS [TypeName],
        ISNULL([baset].[name], N'') AS [BaseTypeName],
        [clmns].[collation_name]         AS [Collation],
        [clmns].[is_xml_document] AS [IsXmlDocument],
        [clmns].[xml_collection_id] AS [XmlCollectionId],
        [xscs].[name] AS [XmlCollection],
        SCHEMA_NAME([xscs].[schema_id]) AS [XmlCollectionSchemaName],
        CONVERT(bit, COLUMNPROPERTY([v].[object_id], [clmns].[name], N'IsIdNotForRepl')) AS [IsNotForReplication],
        CONVERT(bit, COLUMNPROPERTY([v].[object_id], [clmns].[name], N'IsRowGuidCol')) AS [IsRowGuidColumn],
        CASE WHEN [clmns].[max_length] >= 0 AND [baset].[name] IN (N'nchar', N'nvarchar') THEN ([clmns].[max_length] / 2) ELSE [clmns].[max_length] END AS [Length],
        CAST(0 AS bit) AS [InPrimaryKey],
        [clmns].[user_type_id] AS [TypeId],
        CAST([clmns].[precision] AS int)          AS [Precision],
        CAST([clmns].[scale] AS int)         AS [Scale],
        CAST([clmns].[is_nullable] AS bit)     AS [AllowNulls],
        CAST([clmns].[is_computed] AS bit)     AS [IsComputed]
FROM
        [sys].[all_views] AS [v] WITH (NOLOCK)
        INNER JOIN [sys].[columns] AS [clmns] WITH (NOLOCK) ON [clmns].[object_id] = [v].[object_id]
        LEFT OUTER JOIN [sys].[types] AS [usrt] WITH (NOLOCK) ON [usrt].[user_type_id] = [clmns].[user_type_id]
        LEFT OUTER JOIN [sys].[types] AS [baset] WITH (NOLOCK) ON [baset].[system_type_id] = [clmns].[system_type_id] and [baset].[user_type_id] = [baset].[system_type_id]
        LEFT JOIN [sys].[xml_schema_collections] [xscs] WITH (NOLOCK) ON [xscs].[xml_collection_id] = [clmns].[xml_collection_id]
WHERE ([v].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [v].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )) AND OBJECTPROPERTY([v].[object_id], N'IsEncrypted') = 0
) AS [_results] ORDER BY ViewId,ColumnId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.SqlEncryptedViewPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [sv].[schema_id]              AS [SchemaId]
       ,SCHEMA_NAME([sv].[schema_id]) AS [SchemaName]
       ,[sv].[object_id]              AS [ViewId]
       ,[sv].[name]                   AS [ColumnSourceName]
       ,CONVERT(bit, OBJECTPROPERTY([sv].[object_id], N'ExecIsQuotedIdentOn'))
                                      AS [IsQuotedIdentifier]
       ,CONVERT(bit, OBJECTPROPERTY([sv].[object_id], N'ExecIsAnsiNullsOn'))
                                      AS [IsAnsiNulls]
       ,CONVERT(bit, ISNULL(OBJECTPROPERTY([sv].[object_id], N'IsSchemaBound'), 0))
                                      AS [IsSchemaBound]
       ,CASE WHEN [so].[is_published] <> 0 OR [so].[is_schema_published] <> 0 THEN 1 ELSE 0 END AS [ReplInfo]
       ,[sv].[create_date]            AS [CreatedDate]
       ,[sv].[with_check_option]      AS [WithCheckOption]
       ,[sv].[has_opaque_metadata]    AS [HasOpaqueMetadata]
FROM
        [sys].[all_views] [sv] WITH (NOLOCK)
        LEFT JOIN [sys].[objects] [so] WITH (NOLOCK) ON [so].[object_id] = [sv].[object_id]
WHERE ([sv].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [sv].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )) AND OBJECTPROPERTY([sv].[object_id], N'IsEncrypted') = 1) AS [_results] ORDER BY ViewId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.SqlViewColumnPopulator' as [PopulatorName];
SELECT * FROM (

SELECT
        SCHEMA_NAME([v].[schema_id]) AS [SchemaName],
        [v].[name]      AS [ColumnSourceName],
        [clmns].[name]  AS [ColumnName],
        [v].[object_id]        AS [ViewId],
        [clmns].[column_id] AS [ColumnId],
        CAST(0  AS bit) AS [IsForeignKey],
        USER_NAME([usrt].[schema_id])     AS [TypeSchemaName],
        [usrt].[name]               AS [TypeName],
        ISNULL([baset].[name], N'') AS [BaseTypeName],
        [clmns].[collation_name]         AS [Collation],
        [clmns].[is_xml_document] AS [IsXmlDocument],
        [clmns].[xml_collection_id] AS [XmlCollectionId],
        [xscs].[name] AS [XmlCollection],
        SCHEMA_NAME([xscs].[schema_id]) AS [XmlCollectionSchemaName],
        CONVERT(bit, COLUMNPROPERTY([v].[object_id], [clmns].[name], N'IsIdNotForRepl')) AS [IsNotForReplication],
        CONVERT(bit, COLUMNPROPERTY([v].[object_id], [clmns].[name], N'IsRowGuidCol')) AS [IsRowGuidColumn],
        CAST(CASE WHEN  ([clmns].[max_length] >= 0 AND [baset].[name] IN (N'nchar', N'nvarchar')) THEN [clmns].[max_length]/2 ELSE clmns.[max_length] END AS int) AS [Length],
        [clmns].[user_type_id] AS [TypeId],
        CAST(0 AS bit) AS [InPrimaryKey],
        CAST([clmns].[precision] AS int)          AS [Precision],
        CAST([clmns].[scale] AS int)         AS [Scale],
        CAST([clmns].[is_nullable] AS bit)     AS [AllowNulls],
        CAST([clmns].[is_computed] AS bit)     AS [IsComputed]
FROM
        [sys].[all_views] AS [v] WITH (NOLOCK)
        INNER JOIN [sys].[columns] AS [clmns] WITH (NOLOCK) ON [clmns].[object_id] = [v].[object_id]
        LEFT OUTER JOIN [sys].[types] AS [usrt] WITH (NOLOCK) ON [usrt].[user_type_id] = [clmns].[user_type_id]
        LEFT OUTER JOIN [sys].[types] AS [baset] WITH (NOLOCK) ON [baset].[system_type_id] = [clmns].[system_type_id] and [baset].[user_type_id] = [baset].[system_type_id]
        LEFT JOIN [sys].[xml_schema_collections] [xscs] WITH (NOLOCK) ON [xscs].[xml_collection_id] = [clmns].[xml_collection_id]
WHERE ([v].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [v].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )) AND OBJECTPROPERTY([v].[object_id], N'IsEncrypted') =  1
) AS [_results] ORDER BY ViewId,ColumnId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql130IndexPopulator' as [PopulatorName];
SELECT * FROM (
SELECT * FROM (
SELECT DISTINCT
    SCHEMA_NAME([o].[schema_id]) AS [SchemaName]
   ,[i].[object_id]        AS [ColumnSourceId]
   ,[o].[name]             AS [ColumnSourceName]
   ,[o].[type]             AS [ColumnSourceType]
   ,[i].[index_id]         AS [IndexId]
   ,[i].[name]             AS [IndexName]
   ,[f].[type]             AS [DataspaceType]
   ,[f].[data_space_id]    AS [DataspaceId]
   ,[f].[name]             AS [DataspaceName]
   ,CASE WHEN exists(SELECT 1 FROM [sys].[columns] AS [c] WITH (NOLOCK) WHERE [c].[object_id] = [o].[object_id] AND  [c].[is_filestream] = 1) THEN
            [ds].[data_space_id]
        ELSE
            NULL
        END  AS [FileStreamId]
   ,[ds].[name]            AS [FileStreamName]
   ,[ds].[type]            AS [FileStreamType]   
   ,[i].[fill_factor]      AS [FillFactor]    
   ,CONVERT(bit, CASE [i].[type] WHEN 1 THEN 1 WHEN 5 THEN 1 ELSE 0 END) 
                           AS [IsClustered]
   ,[i].[is_unique]        AS [IsUnique]
   ,[i].[is_padded]        AS [IsPadded]
   ,[i].[ignore_dup_key]   AS [DoIgnoreDuplicateKey]
   ,[t].[no_recompute]     AS [NoRecomputeStatistics]
   ,[t].[is_incremental]   AS [DoIncrementalStatistics]
   ,[i].[allow_row_locks]  AS [DoAllowRowLocks]
   ,[i].[allow_page_locks] AS [DoAllowPageLocks]
   ,[i].[is_disabled]      AS [IsDisabled]
   ,[i].[filter_definition]
                           AS [Predicate]
   ,[i].[compression_delay] AS [CompressionDelay]
   ,CONVERT(bit, CASE WHEN [ti].[data_space_id] <> [i].[data_space_id] THEN 0 ELSE 1 END)
                           AS [EqualsParentDataSpace]

   ,[i].[type]             AS [IndexType]
  ,CONVERT(BIT, CASE WHEN [hi].[object_id] IS NULL THEN 0 ELSE 1 END) AS [IsHash]
   ,[hi].[bucket_count] AS [BucketCount]
FROM 
    [sys].[indexes] AS [i] WITH (NOLOCK)
    INNER JOIN [sys].[objects]           AS [o]  WITH (NOLOCK) ON [i].[object_id] = [o].[object_id]
    LEFT  JOIN [sys].[data_spaces]       AS [f]  WITH (NOLOCK) ON [i].[data_space_id] = [f].[data_space_id]
    LEFT  JOIN [sys].[stats]             AS [t]  WITH (NOLOCK) ON [t].[object_id] = [i].[object_id] AND [t].[name] = [i].[name]
    LEFT  JOIN [sys].[tables]            AS [ta] WITH (NOLOCK) ON [ta].[object_id] = [i].[object_id]
    LEFT  JOIN [sys].[data_spaces]       AS [ds] WITH (NOLOCK) ON [ds].[data_space_id] = [ta].[filestream_data_space_id]
    LEFT  JOIN (SELECT * FROM [sys].[indexes] WITH (NOLOCK) WHERE [index_id] < 2) AS [ti] ON [o].[object_id] = [ti].[object_id]
    LEFT OUTER JOIN [sys].[hash_indexes] AS [hi] WITH (NOLOCK) ON [hi].[object_id] = [i].[object_id] AND [hi].[index_id] = [i].[index_id]
WHERE 
    ([o].[type] = N'U' OR [o].[type] = N'V')
    AND [i].[is_primary_key] = 0
    AND [i].[is_unique_constraint] = 0
    AND [i].[is_hypothetical] = 0
    AND [i].[name] IS NOT NULL
    AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) indexBase
WHERE [IndexType] NOT IN (3, 4, 5, 6)
) AS [_results] ORDER BY ColumnSourceId,IndexId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90IndexedColumnSpecificationPopulator' as [PopulatorName];
SELECT * FROM (
SELECT * FROM (
SELECT  
    SCHEMA_NAME([o].[schema_id])  AS [SchemaName]
   ,[i].[object_id]          AS [ColumnSourceId]
   ,[o].[name]               AS [ColumnSourceName]
   ,[o].[type]               AS [ColumnSourceType]
   ,[i].[index_id]           AS [IndexId]
   ,[i].[name]               AS [IndexName]
   ,[c].[column_id]          AS [ColumnId]
   ,[c].[name]               AS [ColumnName]
   ,[ic].[is_descending_key] AS [IsDescending]
   ,[ic].[is_included_column] AS [IsIncludedColumn]
   ,[ic].[partition_ordinal]  AS [PartitionOrdinal]
   ,[ic].[key_ordinal]        AS [KeyOrdinal]
   ,[i].[type]                AS [IndexType]
FROM 
    [sys].[columns] AS [c] WITH (NOLOCK)
    LEFT JOIN [sys].[index_columns] AS [ic] WITH (NOLOCK) ON [c].[object_id] = [ic].[object_id] AND [c].[column_id] = [ic].[column_id]
    LEFT JOIN [sys].[indexes]       AS [i] WITH (NOLOCK) ON [i].[object_id] = [ic].[object_id] AND [i].[index_id] = [ic].[index_id]
    LEFT JOIN [sys].[objects]   AS [o] WITH (NOLOCK) ON [o].[object_id] = [i].[object_id]
    LEFT JOIN [sys].[tables] as [t] WITH (NOLOCK) ON [t].[object_id] = [c].[object_id]
WHERE 
    ([o].[type] = N'U' OR [o].[type] = N'V')
    AND [i].[is_primary_key] = 0
    AND [i].[is_unique_constraint] = 0
    AND [i].[name] IS NOT NULL
    AND [i].[is_hypothetical] = 0
    AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) columns
WHERE [IndexType] NOT IN (3, 4, 6)
) AS [_results] ORDER BY ColumnSourceId,IndexId,KeyOrdinal ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql120StatisticPopulator' as [PopulatorName];
SELECT * FROM (
        SELECT     SCHEMA_NAME([o].[schema_id]) AS [SchemaName]
          ,[s].[object_id]              AS [ColumnSourceId]
          ,[o].[name]					AS [ColumnSourceName]
          ,CASE [ta].[is_filetable] WHEN 1 THEN N'UF' ELSE [o].[type] END  AS [ColumnSourceType]
          ,[s].[stats_id]				AS [StatisticsId]
          ,[s].[name]					AS [StatisticsName]
          ,[s].[no_recompute]			AS [NoRecompute]
          ,[s].[is_incremental]         AS [IsIncremental]
          ,[s].[has_filter]             AS [HasFilter]
          ,[s].[filter_definition]      AS [FilterDefinition]
          ,CAST([s].[user_created] AS BIT) AS [IsUserCreated]
FROM  	   [sys].[stats]   AS [s] WITH (NOLOCK) 
INNER JOIN [sys].[objects] AS [o] WITH (NOLOCK) ON [o].[object_id] = [s].[object_id] 
LEFT JOIN [sys].[tables] AS [ta] WITH (NOLOCK) ON [ta].[object_id] = [o].[object_id]
WHERE      [o].[type] IN (N'U', N'V')
           AND [s].[name] IS NOT NULL
           AND (CONVERT(bit, INDEXPROPERTY([s].[object_id], [s].[name], N'IsStatistics')) = 1)
           AND [s].[user_created] = 1
           AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) AS [_results] ORDER BY ColumnSourceId,StatisticsId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.SqlStatisticColumnPopulator' as [PopulatorName];
SELECT * FROM (
SELECT     SCHEMA_NAME([o].[schema_id]) AS [SchemaName]
          ,[s].[object_id]              AS [ColumnSourceId]
          ,[o].[name]                   AS [ColumnSourceName]
          ,[s].[stats_id]               AS [StatisticsId]
          ,[s].[name]                   AS [StatisticsName]
          ,[c].[column_id]              AS [ColumnId] 
          ,[sc].[name]                  AS [ColumnName]
          ,CASE [ta].[is_filetable] WHEN 1 THEN N'UF' ELSE [o].[type] END  AS [ColumnSourceType]
          ,[c].[stats_column_id]        AS [StatsColumnId]
FROM       [sys].[stats_columns] AS [c] WITH (NOLOCK)   
INNER JOIN [sys].[stats] AS [s] WITH (NOLOCK) ON [s].[object_id] = [c].[object_id] AND [s].[stats_id] = [c].[stats_id]
INNER JOIN [sys].[objects] AS [o] WITH (NOLOCK) ON [o].[object_id] = [c].[object_id] 
INNER JOIN [sys].[columns] AS [sc] WITH (NOLOCK) ON [sc].[object_id] = [c].[object_id] AND [sc].[column_id] = [c].[column_id]
LEFT JOIN [sys].[tables] AS [ta] WITH (NOLOCK) ON [ta].[object_id] = [o].[object_id]
WHERE      [o].[type] IN (N'U', N'V')
           AND [s].[name] IS NOT NULL 
           AND (CONVERT(bit, INDEXPROPERTY([s].[object_id], [s].[name], N'IsStatistics')) = 1)
           AND [s].[user_created] = 1
           AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) AS [_results] ORDER BY ColumnSourceId,StatisticsId,StatsColumnId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql100FullTextStopListPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [fs].[stoplist_id]        AS [StopListId],
        [fs].[name]               AS [StopListName],
        [fs].[principal_id]       AS [AuthorizerId],
        [dp].[name]               AS [AuthorizerName]
FROM 
        [sys].[fulltext_stoplists] [fs] WITH (NOLOCK)
        INNER JOIN [sys].[database_principals] [dp] WITH (NOLOCK) ON [dp].[principal_id] = [fs].[principal_id]
) AS [_results] ORDER BY StopListId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110SearchPropertyListPopulator' as [PopulatorName];
SELECT * FROM (SELECT 
 [spl].[name]              AS [Name],
 [spl].[property_list_id] AS [PropertyListId],
 [spl].[principal_id]     AS [AuthorizerId],
 [p].[name]              AS [Authorizer]
 FROM [sys].[registered_search_property_lists] AS [spl] WITH (NOLOCK)
 LEFT JOIN [sys].[database_principals] AS [p] WITH(NOLOCK) ON [spl].[principal_id] = [p].[principal_id]) AS [_results] ORDER BY PropertyListId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110SearchPropertyPopulator' as [PopulatorName];
SELECT * FROM (SELECT 
 [sp].[property_list_id] AS [PropertyListId],
 [sp].[property_int_id] AS [PropertyIntId],
 [sp].[property_name]   AS [Name],
 [sp].[property_description]     AS [Description],
 [sp].[property_int_id]   AS [Identifier],
 [sp].[property_set_guid] AS [PropertySetGuid],
 [spl].[name]             AS [PropertyListName]
 FROM 
[sys].[registered_search_properties] AS [sp] WITH (NOLOCK)
INNER JOIN [sys].[registered_search_property_lists] AS [spl] WITH(NOLOCK) ON [spl].[property_list_id] = [sp].[property_list_id]) AS [_results] ORDER BY PropertyListId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110FullTextIndexPopulator' as [PopulatorName];
SELECT * FROM (SELECT SCHEMA_NAME([o].[schema_id]) AS [SchemaName]
          ,[o].[object_id]              AS [ColumnSourceId]
          ,[o].[name]                   AS [ColumnSourceName]
          ,[o].[type]                   AS [ColumnSourceType]
          ,[i].[index_id]               AS [KeyId]
          ,[i].[name]                   AS [KeyName]
          ,[ft].[fulltext_catalog_id]   AS [FullTextCatalogId]
          ,[f].[name]                   AS [FullTextCatalogName]
          ,CONVERT(bit, CASE 
    WHEN [i].[is_primary_key] <> 0 THEN 1
    WHEN [i].[is_unique_constraint] <> 0 THEN 1
    WHEN [i].[is_unique] <> 0 THEN 1
    ELSE 0 END)
                                        AS [IsKeyConstraint] 
          ,[ft].[change_tracking_state] AS [ChangeTrackingState]
          ,[ft].[is_enabled]            AS [IsEnabled]
          ,[ft].[stoplist_id]           AS [StopListId]
          ,[fs].[name]                  AS [StopListName]
          ,[ds].[type]                  AS [DataspaceType]
          ,[ds].[data_space_id]         AS [DataspaceId]
          ,[ds].[name]                  AS [DataspaceName]
          ,CASE WHEN [o].[is_published] <> 0 OR [o].[is_schema_published] <> 0 THEN 1 ELSE 0 END AS [ReplInfo]
          ,[ft].[property_list_id]     AS [PropertyListId]
          ,[spl].[name]                AS [PropertyListName]
FROM
           [sys].[fulltext_indexes] AS [ft] WITH (NOLOCK)
           INNER JOIN [sys].[objects] AS [o] WITH (NOLOCK) ON [o].[object_id] = [ft].[object_id] 
           INNER JOIN [sys].[indexes] AS [i] WITH (NOLOCK) ON [i].[object_id] = [ft].[object_id] AND [i].[index_id] = [ft].[unique_index_id]
           INNER JOIN [sys].[fulltext_catalogs] AS [f] WITH (NOLOCK) ON [f].[fulltext_catalog_id] = [ft].[fulltext_catalog_id]
           INNER JOIN [sys].[data_spaces] AS [ds] WITH (NOLOCK) ON [ds].[data_space_id] = [ft].[data_space_id]
           LEFT JOIN [sys].[fulltext_stoplists] AS [fs] WITH (NOLOCK) ON [fs].[stoplist_id] = [ft].[stoplist_id]
           LEFT JOIN [sys].[registered_search_property_lists] AS [spl] WITH (NOLOCK) ON [ft].[property_list_id] = [spl].[property_list_id]
WHERE      [i].[is_hypothetical] = 0
           AND OBJECTPROPERTY([o].[object_id], N'IsSystemTable') = 0 
           AND [i].[name] IS NOT NULL
           AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) AS [_results] ORDER BY ColumnSourceId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110FullTextIndexColumnSpecifierPopulator' as [PopulatorName];
SELECT * FROM (
SELECT     SCHEMA_NAME([o].[schema_id]) AS [SchemaName]
          ,[c].[object_id]              AS [ColumnSourceId]
          ,[o].[name]                   AS [ColumnSourceName]
          ,[ft].[unique_index_id]       AS [KeyId]
          ,[ic].[column_id]             AS [ColumnId]
          ,[c].[name]                   AS [ColumnName]
          ,[ic].[type_column_id]        AS [TypeColumnId]
          ,[c2].[name]                  AS [TypeColumnName]
          ,[ic].[language_id]           AS [LCID]
          ,[o].[type]                   AS [ColumnSourceType]      
          ,CAST([ic].[statistical_semantics] AS BIT) AS [IsStatisticalSemantics]    
FROM       [sys].[columns] AS [c] WITH (NOLOCK)
INNER JOIN [sys].[fulltext_index_columns] AS [ic] WITH (NOLOCK) ON [ic].[object_id] = [c].[object_id] AND [ic].[column_id] = [c].[column_id]
INNER JOIN [sys].[objects] AS [o] WITH (NOLOCK) ON [o].[object_id] = [c].[object_id] 
INNER JOIN [sys].[fulltext_indexes] AS [ft] WITH (NOLOCK) ON [ft].[object_id] = [o].[object_id]
INNER JOIN [sys].[fulltext_catalogs] AS [f] WITH (NOLOCK) ON [f].[fulltext_catalog_id] = [ft].[fulltext_catalog_id]
LEFT JOIN  [sys].[columns] AS [c2] WITH (NOLOCK) ON [ic].[object_id] = [c2].[object_id] AND [ic].[type_column_id] = [c2].[column_id]
WHERE      OBJECTPROPERTY([o].[object_id], N'IsSystemTable') = 0 AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))
) AS [_results] ORDER BY ColumnSourceId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql130ColumnStoreIndexPopulator' as [PopulatorName];
SELECT * FROM (
SELECT * FROM (
SELECT DISTINCT
    SCHEMA_NAME([o].[schema_id]) AS [SchemaName]
   ,[i].[object_id]        AS [ColumnSourceId]
   ,[o].[name]             AS [ColumnSourceName]
   ,[o].[type]             AS [ColumnSourceType]
   ,[i].[index_id]         AS [IndexId]
   ,[i].[name]             AS [IndexName]
   ,[f].[type]             AS [DataspaceType]
   ,[f].[data_space_id]    AS [DataspaceId]
   ,[f].[name]             AS [DataspaceName]
   ,CASE WHEN exists(SELECT 1 FROM [sys].[columns] AS [c] WITH (NOLOCK) WHERE [c].[object_id] = [o].[object_id] AND  [c].[is_filestream] = 1) THEN
            [ds].[data_space_id]
        ELSE
            NULL
        END  AS [FileStreamId]
   ,[ds].[name]            AS [FileStreamName]
   ,[ds].[type]            AS [FileStreamType]   
   ,[i].[fill_factor]      AS [FillFactor]    
   ,CONVERT(bit, CASE [i].[type] WHEN 1 THEN 1 WHEN 5 THEN 1 ELSE 0 END) 
                           AS [IsClustered]
   ,[i].[is_unique]        AS [IsUnique]
   ,[i].[is_padded]        AS [IsPadded]
   ,[i].[ignore_dup_key]   AS [DoIgnoreDuplicateKey]
   ,[t].[no_recompute]     AS [NoRecomputeStatistics]
   ,[t].[is_incremental]   AS [DoIncrementalStatistics]
   ,[i].[allow_row_locks]  AS [DoAllowRowLocks]
   ,[i].[allow_page_locks] AS [DoAllowPageLocks]
   ,[i].[is_disabled]      AS [IsDisabled]
   ,[i].[filter_definition]
                           AS [Predicate]
   ,[i].[compression_delay] AS [CompressionDelay]
   ,CONVERT(bit, CASE WHEN [ti].[data_space_id] <> [i].[data_space_id] THEN 0 ELSE 1 END)
                           AS [EqualsParentDataSpace]

   ,[i].[type]             AS [IndexType]
  
FROM 
    [sys].[indexes] AS [i] WITH (NOLOCK)
    INNER JOIN [sys].[objects]           AS [o]  WITH (NOLOCK) ON [i].[object_id] = [o].[object_id]
    LEFT  JOIN [sys].[data_spaces]       AS [f]  WITH (NOLOCK) ON [i].[data_space_id] = [f].[data_space_id]
    LEFT  JOIN [sys].[stats]             AS [t]  WITH (NOLOCK) ON [t].[object_id] = [i].[object_id] AND [t].[name] = [i].[name]
    LEFT  JOIN [sys].[tables]            AS [ta] WITH (NOLOCK) ON [ta].[object_id] = [i].[object_id]
    LEFT  JOIN [sys].[data_spaces]       AS [ds] WITH (NOLOCK) ON [ds].[data_space_id] = [ta].[filestream_data_space_id]
    LEFT  JOIN (SELECT * FROM [sys].[indexes] WITH (NOLOCK) WHERE [index_id] < 2) AS [ti] ON [o].[object_id] = [ti].[object_id]
    
WHERE 
    ([o].[type] = N'U' OR [o].[type] = N'V')
    AND [i].[is_primary_key] = 0
    AND [i].[is_unique_constraint] = 0
    AND [i].[is_hypothetical] = 0
    AND [i].[name] IS NOT NULL
    AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) indexBase
WHERE [IndexType] IN (5, 6)
) AS [_results] ORDER BY ColumnSourceId,IndexId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110ColumnStoreIndexColumnSpecificationPopulator' as [PopulatorName];
SELECT * FROM (
SELECT * FROM (
SELECT  
    SCHEMA_NAME([o].[schema_id])  AS [SchemaName]
   ,[i].[object_id]          AS [ColumnSourceId]
   ,[o].[name]               AS [ColumnSourceName]
   ,[o].[type]               AS [ColumnSourceType]
   ,[i].[index_id]           AS [IndexId]
   ,[i].[name]               AS [IndexName]
   ,[c].[column_id]          AS [ColumnId]
   ,[c].[name]               AS [ColumnName]
   ,[ic].[is_descending_key] AS [IsDescending]
   ,[ic].[is_included_column] AS [IsIncludedColumn]
   ,[ic].[partition_ordinal]  AS [PartitionOrdinal]
   ,[ic].[key_ordinal]        AS [KeyOrdinal]
   ,[i].[type]                AS [IndexType]
FROM 
    [sys].[columns] AS [c] WITH (NOLOCK)
    LEFT JOIN [sys].[index_columns] AS [ic] WITH (NOLOCK) ON [c].[object_id] = [ic].[object_id] AND [c].[column_id] = [ic].[column_id]
    LEFT JOIN [sys].[indexes]       AS [i] WITH (NOLOCK) ON [i].[object_id] = [ic].[object_id] AND [i].[index_id] = [ic].[index_id]
    LEFT JOIN [sys].[objects]   AS [o] WITH (NOLOCK) ON [o].[object_id] = [i].[object_id]
    LEFT JOIN [sys].[tables] as [t] WITH (NOLOCK) ON [t].[object_id] = [c].[object_id]
WHERE 
    ([o].[type] = N'U' OR [o].[type] = N'V')
    AND [i].[is_primary_key] = 0
    AND [i].[is_unique_constraint] = 0
    AND [i].[name] IS NOT NULL
    AND [i].[is_hypothetical] = 0
    AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) columns
WHERE [IndexType] IN (5, 6)
) AS [_results] ORDER BY ColumnSourceId,IndexId,KeyOrdinal ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110SpatialIndexPopulator' as [PopulatorName];
SELECT * FROM (
SELECT 
        SCHEMA_NAME([o].[schema_id])    AS [SchemaName],
        [si].[object_id]                AS [ColumnSourceId],
        [o].[name]                      AS [ColumnSourceName],
        [o].[type]                      AS [ColumnSourceType],
        [ic].[column_id]                AS [ColumnId],
        [c].[name]                      AS [ColumnName],
        [si].[index_id]                 AS [IndexId],
        [si].[name]                     AS [IndexName],
        [ds].[type]                     AS [DataspaceType],
        [ds].[data_space_id]            AS [DataspaceId],
        [ds].[name]                     AS [DataspaceName],
        [si].[fill_factor]              AS [FillFactor],
        [si].[is_padded]                AS [IsPadded],
        [si].[is_disabled]              AS [IsDisabled],
        [si].[allow_page_locks]         AS [DoAllowPageLocks],
        [si].[allow_row_locks]          AS [DoAllowRowLocks],
        [sit].[cells_per_object]        AS [CellsPerObject],
        [sit].[bounding_box_xmin]       AS [XMin],
        [sit].[bounding_box_xmax]       AS [XMax],
        [sit].[bounding_box_ymin]       AS [YMin],
        [sit].[bounding_box_ymax]       AS [YMax],
        [sit].[level_1_grid]            AS [Level1Grid],
        [sit].[level_2_grid]            AS [Level2Grid],
        [sit].[level_3_grid]            AS [Level3Grid],
        [sit].[level_4_grid]            AS [Level4Grid],
        [sit].[tessellation_scheme]     AS [TessellationScheme],
        [s].[no_recompute]              AS [NoRecomputeStatistics],
        [p].[data_compression]          AS [DataCompressionId],
        CONVERT(bit, CASE WHEN [ti].[data_space_id] = [ds].[data_space_id] THEN 1 ELSE 0 END)
                                        AS [EqualsParentDataSpace]
FROM
        [sys].[spatial_indexes]          AS [si] WITH (NOLOCK)
        INNER JOIN [sys].[objects]       AS [o] WITH (NOLOCK) ON [si].[object_id] = [o].[object_id]
        INNER JOIN [sys].[spatial_index_tessellations] [sit] WITH (NOLOCK) ON [si].[object_id] = [sit].[object_id] AND [si].[index_id] = [sit].[index_id]
        INNER JOIN [sys].[data_spaces]   AS [ds] WITH (NOLOCK) ON [ds].[data_space_id] = [si].[data_space_id] 
        INNER JOIN [sys].[index_columns] AS [ic] WITH (NOLOCK) ON [si].[object_id] = [ic].[object_id] AND [si].[index_id] = [ic].[index_id]
        INNER JOIN [sys].[columns]       AS [c] WITH (NOLOCK) ON [si].[object_id] = [c].[object_id] AND [ic].[column_id] = [c].[column_id]
        INNER JOIN [sys].[objects]       AS [o2] WITH (NOLOCK) ON [o2].[parent_object_id] = [si].[object_id]
        INNER JOIN [sys].[stats]         AS [s] WITH (NOLOCK) ON [o2].[object_id] = [s].[object_id] AND [s].[name] = [si].[name]
        INNER JOIN [sys].[partitions]    AS [p] WITH (NOLOCK) ON [p].[object_id] = [o2].[object_id] AND [p].[partition_number] = 1
        LEFT  JOIN [sys].[indexes]       AS [ti] WITH (NOLOCK) ON [o].[object_id] = [ti].[object_id]
        LEFT JOIN [sys].[tables]         AS [t] WITH (NOLOCK) ON [t].[object_id] = [si].[object_id]
WHERE [si].[is_hypothetical] = 0
        AND [ti].[index_id] < 2
        AND OBJECTPROPERTY([o].[object_id], N'IsSystemTable') = 0
        AND ([t].[is_filetable] = 0 OR [t].[is_filetable] IS NULL)
        AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))
) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90XmlIndexPopulator' as [PopulatorName];
SELECT * FROM (
SELECT DISTINCT
    SCHEMA_NAME([o].[schema_id]) AS [SchemaName]
   ,[i].[object_id]        AS [ColumnSourceId]
   ,[o].[name]             AS [ColumnSourceName]
   ,[o].[type]             AS [ColumnSourceType]
   ,[i].[index_id]         AS [IndexId]
   ,[i].[name]             AS [IndexName]
   ,CONVERT(bit, CASE WHEN [i].[xml_index_type] = 0 THEN 1 ELSE 0 END) 
                               AS [IsPrimaryXmlIndex]
   ,[i].[using_xml_index_id]   AS [PrimaryXmlIndexId]
   ,[xi].[name]                AS [PrimaryXmlIndexName]
   ,[ic].[column_id]           AS [XmlIndexColumnId]
   ,[c].[name]                 AS [XmlIndexColumnName]
   ,[i].[secondary_type]       AS [SecondaryType]
   ,[i].[fill_factor]      AS [FillFactor]    
   ,[i].[is_padded]        AS [IsPadded]
   ,[t].[no_recompute]     AS [NoRecomputeStatistics]
   ,[i].[allow_row_locks]  AS [DoAllowRowLocks]
   ,[i].[allow_page_locks] AS [DoAllowPageLocks]
   ,[i].[is_disabled]      AS [IsDisabled]
FROM 
    [sys].[xml_indexes] AS [i] WITH (NOLOCK)
    INNER JOIN [sys].[objects]           AS [o]  WITH (NOLOCK) ON [i].[object_id] = [o].[object_id]
    INNER JOIN [sys].[index_columns]     AS [ic] WITH (NOLOCK) ON [i].[object_id] = [ic].[object_id] AND [i].[index_id] = [ic].[index_id]
    INNER JOIN [sys].[columns]           AS [c]  WITH (NOLOCK) ON [ic].[object_id] = [c].[object_id] AND [ic].[column_id] = [c].[column_id]
    INNER JOIN [sys].[objects]           AS [o2] WITH (NOLOCK) ON [o2].[parent_object_id] = [i].[object_id]
    INNER JOIN [sys].[stats]             AS [t]  WITH (NOLOCK) ON [t].[object_id] = [o2].[object_id] AND [t].[name] = [i].[name]    
    LEFT  JOIN [sys].[xml_indexes] AS [xi] WITH (NOLOCK) ON [xi].[object_id] = [i].[object_id] AND [xi].[index_id] = [i].[using_xml_index_id] 
    LEFT  JOIN [sys].[tables]        AS [ta] WITH (NOLOCK) ON [o].[object_id] = [ta].[object_id]   
WHERE 
    ([o].[type] = N'U' AND [ta].[is_filetable] = 0 OR [o].[type] = N'V')
    AND [i].[name] IS NOT NULL
    AND [i].[is_hypothetical] = 0
    AND ( [i].[xml_index_type] = 0 OR [i].[xml_index_type] = 1 )
    AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) AS [_results] ORDER BY ColumnSourceId,IndexId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110SelectiveXmlIndexPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
    SCHEMA_NAME([o].[schema_id]) AS [SchemaName]
    ,[i].[object_id]        AS [ColumnSourceId]
    ,[o].[name]             AS [ColumnSourceName]
    ,[o].[type]             AS [ColumnSourceType]
    ,[i].[index_id]         AS [IndexId]
    ,[i].[name]             AS [IndexName]
    ,CONVERT(bit, CASE WHEN [i].[xml_index_type] = 2 THEN 1 ELSE 0 END) 
                                AS [IsPrimaryXmlIndex]
    ,[i].[using_xml_index_id]   AS [PrimaryXmlIndexId]
    ,[i].[path_id]              AS [PrimaryXmlIndexPromotedPathId]
    ,[sxip].name                AS [PrimaryXmlIndexPromotedPathName]
    ,[xi].[name]                AS [PrimaryXmlIndexName]
    ,[ic].[column_id]           AS [XmlIndexColumnId]
    ,[c].[name]                 AS [XmlIndexColumnName]
    ,[i].[fill_factor]      AS [FillFactor]
    ,[i].[is_padded]        AS [IsPadded]
    ,[i].[allow_row_locks]  AS [DoAllowRowLocks]
    ,[i].[allow_page_locks] AS [DoAllowPageLocks]
    ,[i].[ignore_dup_key]   AS [DoIgnoreDuplicateKey]
    ,[t].[no_recompute]     AS [NoRecomputeStatistics]
    ,[i].[is_disabled]      AS [IsDisabled]
    ,[i].[xml_index_type]   AS [XmlIndexType]
FROM 
    [sys].[xml_indexes] AS [i] WITH (NOLOCK)
    INNER JOIN [sys].[objects]           AS [o]  WITH (NOLOCK) ON [i].[object_id] = [o].[object_id]
    INNER JOIN [sys].[index_columns]     AS [ic] WITH (NOLOCK) ON [i].[object_id] = [ic].[object_id] AND [i].[index_id] = [ic].[index_id]
    INNER JOIN [sys].[columns]           AS [c]  WITH (NOLOCK) ON [ic].[object_id] = [c].[object_id] AND [ic].[column_id] = [c].[column_id]
    INNER JOIN [sys].[objects]           AS [o2] WITH (NOLOCK) ON [o2].[parent_object_id] = [i].[object_id]
    INNER JOIN [sys].[stats]       AS [t]  WITH (NOLOCK) ON [t].[object_id] = [o2].[object_id] AND [t].[name] = [i].[name]
    LEFT  JOIN [sys].[xml_indexes] AS [xi] WITH (NOLOCK) ON [xi].[object_id] = [i].[object_id] AND [xi].[index_id] = [i].[using_xml_index_id] 
    LEFT  JOIN [sys].[tables]      AS [ta] WITH (NOLOCK) ON [o].[object_id] = [ta].[object_id]    
    LEFT  JOIN [sys].[selective_xml_index_paths] AS [sxip] WITH(NOLOCK) ON [sxip].[object_id] = [i].[object_id] AND [sxip].[index_id] = [i].[using_xml_index_id] AND [sxip].[path_id] = [i].[path_id]
WHERE 
    ([o].[type] = N'U' AND [ta].[is_filetable] = 0 OR [o].[type] = N'V')
    AND [i].[name] IS NOT NULL
    AND [i].[is_hypothetical] = 0
    AND ( [i].[xml_index_type] = 2 OR [i].[xml_index_type] = 3 )
    AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) AS [_results] ORDER BY XmlIndexType,ColumnSourceId,IndexId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110SelectiveXmlIndexNamespacePopulator' as [PopulatorName];
SELECT * FROM (
SELECT 
    SCHEMA_NAME([o].[schema_id]) AS [SchemaName]
    ,[i].[object_id]        AS [ColumnSourceId]
    ,[o].[name]             AS [ColumnSourceName]
    ,[o].[type]             AS [ColumnSourceType]
    ,[i].[index_id]         AS [IndexId]
    ,[i].[name]             AS [IndexName]
    ,CAST([sxin].[is_default_uri] AS BIT) AS [IsDefaultUri]
    ,[sxin].[uri] AS [Uri]
    ,[sxin].[prefix] AS [Prefix]
    ,[i].[xml_index_type]   AS [XmlIndexType]
FROM [sys].[selective_xml_index_namespaces] AS[sxin] WITH (NOLOCK) 
    INNER JOIN [sys].[xml_indexes] AS [i] WITH (NOLOCK) ON [sxin].[index_id] = [i].[index_id] AND [sxin].[object_id] = [i].[object_id]
    INNER JOIN [sys].[objects] AS [o]  WITH (NOLOCK) ON [i].[object_id] = [o].[object_id]
    LEFT JOIN [sys].[tables] as [ta] WITH (NOLOCK) ON [ta].[object_id] = [i].[object_id]
WHERE  [i].[xml_index_type]  IN (2,3) AND 
    ([o].[type] = N'U' AND [ta].[is_filetable] = 0 OR [o].[type] = N'V')  AND
    [i].[name] IS NOT NULL AND 
    [i].[is_hypothetical] = 0
    AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) AS [_results] ORDER BY XmlIndexType,ColumnSourceId,IndexId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110PromotedNodePathsPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
    SCHEMA_NAME([o].[schema_id]) AS [SchemaName]
    ,[i].[object_id]        AS [ColumnSourceId]
    ,[o].[name]             AS [ColumnSourceName]
    ,[o].[type]             AS [ColumnSourceType]
    ,[i].[index_id]         AS [IndexId]
    ,[i].[name]             AS [IndexName]
    ,[ip].[path_id]         AS [PromotedXmlPathId]
    ,[ip].[path]            AS [PromotedPath]
    ,[ip].[name]            AS [PathName]
    ,[ip].[path_type]       AS [PathType]
    ,[ip].[xml_component_id]                AS [XQueryTypeId]
    ,[ip].[xquery_type_description]         AS [XQueryType]    
    ,CAST(ISNULL([ip].[is_xquery_type_inferred], 0) AS BIT)  AS [IsXQueryTypeInferred]
    ,[ip].[xquery_max_length]               AS [XQueryMaxLength]
    ,CAST(ISNULL([ip].[is_xquery_max_length_inferred],0) AS BIT)  AS [IsXQueryMaxLengthInferred]
    ,[ip].[is_node]         AS [IsNode]
    ,[ip].[is_singleton]    AS [IsSingleton]
    ,SCHEMA_NAME([types].[schema_id]) AS [TypeSchemaName]
    ,[ip].[system_type_id]  AS [BaseTypeId]
    ,[basetypes].[name]     AS [BaseTypeName]
    ,[ip].[user_type_id]    AS [TypeId]
    ,[types].[name]         AS [TypeName]
    ,CASE WHEN [ip].[max_length] >= 0 AND [types].[name] IN (N'nchar', N'nvarchar') THEN ([ip].[max_length] / 2) ELSE [ip].[max_length] END AS [Length]
    ,[ip].[precision]       AS [Precision]
    ,[ip].[scale]           AS [Scale]
    ,[ip].[collation_name]  AS [Collation]
    ,[i].[xml_index_type]   AS [XmlIndexType]
FROM 
    [sys].[selective_xml_index_paths] AS [ip] WITH (NOLOCK)
    INNER JOIN [sys].[xml_indexes] AS [i] WITH (NOLOCK) ON [ip].[index_id] = [i].[index_id] AND [ip].[object_id] = [i].[object_id]
    INNER JOIN [sys].[objects]           AS [o]  WITH (NOLOCK) ON [i].[object_id] = [o].[object_id]    
    LEFT  JOIN [sys].[xml_indexes]  AS [xi] WITH (NOLOCK) ON [xi].[object_id] = [i].[object_id] AND [xi].[index_id] = [i].[using_xml_index_id] 
    LEFT  JOIN [sys].[tables]       AS [ta] WITH (NOLOCK) ON [o].[object_id] = [ta].[object_id]   
    LEFT  JOIN [sys].[types]        AS [basetypes] WITH (NOLOCK) ON [ip].[system_type_id] = [basetypes].[system_type_id] AND [basetypes].[system_type_id] = [basetypes].[user_type_id]
    LEFT  JOIN [sys].[types]        AS [types] WITH (NOLOCK) ON [ip].[user_type_id] = [types].[user_type_id]
WHERE 
    ([o].[type] = N'U' AND [ta].[is_filetable] = 0 OR [o].[type] = N'V')
    AND [i].[name] IS NOT NULL
    AND [i].[is_hypothetical] = 0
    AND ( [i].[xml_index_type] = 2 OR [i].[xml_index_type] = 3 )
    AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) AS [_results] ORDER BY XmlIndexType,ColumnSourceId,IndexId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90EncryptedAndClrDmlTriggerPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
           [s].[name]      AS [SchemaName],
           [t].[parent_id] AS [ParentId],
           [o].[name]      AS [ParentName],
           CASE [ta].[is_filetable] WHEN 1 THEN N'UF' ELSE [o].[type] END      AS [ObjectType],
           [t].[name]      AS [TriggerName],
           [t].[type]      AS [TriggerType],
           [t].[object_id] AS [ObjectId],
           CONVERT(bit, [sm].[uses_ansi_nulls]) AS [IsAnsiNulls],
           CONVERT(bit, [sm].[uses_quoted_identifier]) AS [IsQuotedIdentifier],
           CONVERT(bit, [t].[is_not_for_replication]) AS [IsNotForReplication],
           CONVERT(bit, [t].[is_instead_of_trigger])  AS [IsInsteadOfTrigger],
           CONVERT(bit, [t].[is_disabled])            AS [IsDisabled],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsUpdateTrigger')) AS [IsUpdateTrigger],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsDeleteTrigger')) AS [IsDeleteTrigger],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsInsertTrigger')) AS [IsInsertTrigger],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsAfterTrigger'))  AS [IsAfterTrigger],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsFirstDeleteTrigger')) AS [IsFirstDeleteTrigger],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsFirstInsertTrigger')) AS [IsFirstInsertTrigger],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsFirstUpdateTrigger')) AS [IsFirstUpdateTrigger],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsLastDeleteTrigger'))  AS [IsLastDeleteTrigger],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsLastInsertTrigger'))  AS [IsLastInsertTrigger],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsLastUpdateTrigger'))  AS [IsLastUpdateTrigger],
           NULL                           AS [AssemblyId],
           NULL                           AS [Assembly],
           NULL                           AS [AssemblyClass],
           NULL                           AS [AssemblyMethod],
           [sm].[execute_as_principal_id] AS [ExecuteAsId],
           [p].[name] AS [ExecuteAsName]

FROM
           [sys].[triggers]       AS [t] WITH (NOLOCK)
           INNER JOIN [sys].[objects]        AS [o] WITH (NOLOCK) ON [o].[object_id] = [t].[parent_id]
           LEFT JOIN [sys].[schemas]        AS [s] WITH (NOLOCK) ON [o].[schema_id] = [s].[schema_id]
           LEFT JOIN [sys].[sql_modules]    AS [sm] WITH (NOLOCK) ON [t].[object_id] = [sm].[object_id]
           LEFT JOIN [sys].[database_principals] [p] WITH (NOLOCK) ON [p].[principal_id] = [sm].[execute_as_principal_id]
           LEFT JOIN [sys].[tables] [ta] WITH (NOLOCK) ON [ta].[object_id] = [o].[object_id]
WHERE
           [t].[parent_class] = 1
           AND OBJECTPROPERTY([t].[object_id], N'IsEncrypted') = 1
           AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))

UNION

SELECT
           [s].[name]      AS [SchemaName],
           [t].[parent_id] AS [ParentId],
           [o].[name]      AS [ParentName],
           [o].[type]      AS [ObjectType],
           [t].[name]      AS [TriggerName],
           [t].[type]      AS [TriggerType],
           [t].[object_id] AS [ObjectId],
           NULL            AS [IsAnsiNulls],
           NULL            AS [IsQuotedIdentifier],
           CONVERT(bit, [t].[is_not_for_replication]) AS [IsNotForReplication],
           CONVERT(bit, [t].[is_instead_of_trigger])  AS [IsInsteadOfTrigger],
           CONVERT(bit, [t].[is_disabled])            AS [IsDisabled],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsUpdateTrigger')) AS [IsUpdateTrigger],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsDeleteTrigger')) AS [IsDeleteTrigger],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsInsertTrigger')) AS [IsInsertTrigger],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsAfterTrigger'))  AS [IsAfterTrigger],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsFirstDeleteTrigger')) AS [IsFirstDeleteTrigger],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsFirstInsertTrigger')) AS [IsFirstInsertTrigger],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsFirstUpdateTrigger')) AS [IsFirstUpdateTrigger],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsLastDeleteTrigger'))  AS [IsLastDeleteTrigger],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsLastInsertTrigger'))  AS [IsLastInsertTrigger],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsLastUpdateTrigger'))  AS [IsLastUpdateTrigger],
           [as].[assembly_id]            AS [AssemblyId],
           [as].[name]                   AS [Assembly],
           [am].[assembly_class]         AS [AssemblyClass],
           [am].[assembly_method]        AS [AssemblyMethod],
           [am].[execute_as_principal_id] AS [ExecuteAsId],
           [p].[name] AS [ExecuteAsName]
FROM
           [sys].[triggers]       AS [t] WITH (NOLOCK)
           INNER JOIN [sys].[objects]        AS [o] WITH (NOLOCK) ON [o].[object_id] = [t].[parent_id]
           LEFT JOIN [sys].[schemas]        AS [s] WITH (NOLOCK) ON [o].[schema_id] = [s].[schema_id]
           LEFT JOIN [sys].[assembly_modules]    AS [am] WITH (NOLOCK) ON [am].[object_id] = [t].[object_id]
           LEFT JOIN [sys].[assemblies]          AS [as] WITH (NOLOCK) ON [as].[assembly_id] = [am].[assembly_id]
           LEFT JOIN [sys].[database_principals] [p] WITH (NOLOCK) ON [p].[principal_id] = [am].[execute_as_principal_id]
WHERE
           [t].[parent_class] = 1
           AND [t].[type] = N'TA'
           AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))
) AS [_results] ORDER BY ParentId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql130DmlTriggerPopulator' as [PopulatorName];
SELECT * FROM (
SELECT DISTINCT
           [s].[name]      AS [SchemaName],
           [t].[parent_id] AS [ParentId],
           [o].[name]      AS [ParentName],
           CASE [ta].[is_filetable] WHEN 1 THEN N'UF' ELSE [o].[type] END      AS [ColumnSourceType],
           [t].[name]      AS [TriggerName],
           [t].[object_id] AS [ObjectId],
           [m].[uses_native_compilation] AS [UsesNativeCompilation],
           [m].[is_schema_bound] AS [IsSchemaBound],
           CONVERT(bit, [t].[is_disabled])            AS [IsDisabled],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'IsEncrypted')) AS [IsEncrypted],
           CONVERT(bit, [t].[is_not_for_replication]) AS [IsNotForReplication],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsUpdateTrigger')) AS [IsUpdateTrigger],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsDeleteTrigger')) AS [IsDeleteTrigger],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsInsertTrigger')) AS [IsInsertTrigger],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsAfterTrigger'))  AS [IsAfterTrigger],
           CONVERT(bit, [t].[is_instead_of_trigger])  AS [IsInsteadOfTrigger],
           CASE WHEN OBJECTPROPERTY([t].[object_id], N'ExecIsFirstDeleteTrigger') = 1 THEN 0 WHEN OBJECTPROPERTY([t].[object_id], N'ExecIsLastDeleteTrigger') = 1 THEN 2 ELSE 1 END AS [DeleteOrder],
           CASE WHEN OBJECTPROPERTY([t].[object_id], N'ExecIsFirstInsertTrigger') = 1 THEN 0 WHEN OBJECTPROPERTY([t].[object_id], N'ExecIsLastInsertTrigger') = 1 THEN 2 ELSE 1 END AS [InsertOrder],
           CASE WHEN OBJECTPROPERTY([t].[object_id], N'ExecIsFirstUpdateTrigger') = 1 THEN 0 WHEN OBJECTPROPERTY([t].[object_id], N'ExecIsLastUpdateTrigger') = 1 THEN 2 ELSE 1 END AS [UpdateOrder],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsQuotedIdentOn')) AS [IsQuotedIdentifier],
           CONVERT(bit, OBJECTPROPERTY([t].[object_id], N'ExecIsAnsiNullsOn'))   AS [IsAnsiNulls],
           [m].[definition] AS [Script],
           [m].[execute_as_principal_id] AS [ExecuteAsId],
           [p].[name] AS [ExecuteAsName],
           [ambiguous].[IsAmbiguous] AS [HasAmbiguousReference]
FROM
           [sys].[triggers]       AS [t] WITH (NOLOCK)
           INNER JOIN [sys].[objects]        AS [o] WITH (NOLOCK) ON [o].[object_id] = [t].[parent_id]
           LEFT JOIN [sys].[schemas]        AS [s] WITH (NOLOCK) ON [o].[schema_id] = [s].[schema_id]
           LEFT JOIN [sys].[sql_modules]    AS [m] WITH (NOLOCK) ON [t].[object_id] = [m].[object_id]
           LEFT JOIN [sys].[database_principals] [p] WITH (NOLOCK) ON [p].[principal_id] = [m].[execute_as_principal_id]
           OUTER APPLY (SELECT TOP 1 1 AS [IsAmbiguous] FROM [sys].[sql_expression_dependencies] AS [exp] WITH (NOLOCK)
            WHERE [exp].[referencing_id] = [t].[object_id] AND [exp].[referencing_class] = 1 AND ([exp].[is_ambiguous] = 1 OR ([exp].[is_caller_dependent] = 1 AND 1 <> (SELECT COUNT(1) FROM [sys].[objects] AS [o] WITH (NOLOCK) WHERE [o].[name] = [exp].[referenced_entity_name])) )) AS [ambiguous] 
           LEFT JOIN [sys].[tables] [ta] WITH(NOLOCK) ON [ta].[object_id] = [o].[object_id]
WHERE
           [t].[parent_class] = 1
           AND OBJECTPROPERTY([t].[object_id], N'IsEncrypted') = 0
           AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))
) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90EncryptedAndClrDdlTriggerPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [t].[object_id]         AS [ObjectId],
        [t].[name]              AS [Name],
        [t].[type]              AS [TriggerType],
        [t].[parent_class]      AS [TriggerScope],
        [t].[is_instead_of_trigger] AS [IsInsteadOfTrigger],
        [t].[is_disabled]           AS [IsDisabled],
        NULL                        AS [AssemblyId],
        NULL                        AS [Assembly],
        NULL                        AS [AssemblyClass],
        NULL                        AS [AssemblyMethod],
        CONVERT(bit, ISNULL([sm].[uses_ansi_nulls], 0)) AS [IsAnsiNulls],
        CONVERT(bit, ISNULL([sm].[uses_quoted_identifier], 0)) AS [IsQuotedIdentifier],
        [sm].[execute_as_principal_id] AS [ExecuteAsId],
        [p].[name] AS [ExecuteAsName]
FROM
        [sys].[triggers] [t] WITH (NOLOCK)
        LEFT JOIN [sys].[sql_modules] [sm] WITH (NOLOCK) ON [sm].[object_id] = [t].[object_id]
        LEFT JOIN [sys].[database_principals] [p] WITH (NOLOCK) ON [p].[principal_id] = [sm].[execute_as_principal_id]
WHERE
        [t].[parent_class] = 0
        AND [sm].[definition] IS NULL
        AND [t].[type] = N'TR'
        AND ([t].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [t].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))
UNION

SELECT
        [t].[object_id]         AS [ObjectId],
        [t].[name]              AS [Name],
        [t].[type]              AS [TriggerType],
        [t].[parent_class]      AS [TriggerScope],
        [t].[is_instead_of_trigger] AS [IsInsteadOfTrigger],
        [t].[is_disabled]           AS [IsDisabled],
        [a].[assembly_id]       AS [AssemblyId],
        [a].[name]              AS [Assembly],
        [am].[assembly_class]   AS [AssemblyClass],
        [am].[assembly_method]  AS [AssemblyMethod],
        NULL                    AS [IsAnsiNulls],
        NULL                    AS [IsQuotedIdentifier],
        [am].[execute_as_principal_id] AS [ExecuteAsId],
        [p].[name] AS [ExecuteAsName]
FROM
        [sys].[triggers] [t] WITH (NOLOCK)
        LEFT JOIN [sys].[assembly_modules] [am] WITH (NOLOCK) ON [am].[object_id] = [t].[object_id]
        LEFT JOIN [sys].[assemblies] [a] WITH (NOLOCK) ON [a].[assembly_id] = [am].[assembly_id]
        LEFT JOIN [sys].[database_principals] [p] WITH (NOLOCK) ON [p].[principal_id] = [am].[execute_as_principal_id]
WHERE
        [t].[parent_class] = 0
        AND [t].[type] = N'TA'
        AND ([t].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [t].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))
) AS [_results] ORDER BY ObjectId DESC;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90EncryptedDdlTriggerEventPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [e].[object_id] AS [TriggerId],
        [e].[type_desc] AS [EventType],
        [e].[is_first]  AS [IsFirst],
        [e].[is_last]   AS [IsLast],
        [t].[name]      AS [Name],
        [e].[type]      AS [EventTypeId],
        [e].[event_group_type] AS [EventGroupType]
FROM
        [sys].[trigger_events] [e] WITH (NOLOCK)
        INNER JOIN [sys].[triggers] [t] WITH (NOLOCK) ON [t].[object_id] = [e].[object_id]
        LEFT OUTER JOIN [sys].[sql_modules] [m] WITH (NOLOCK) ON [t].[object_id] = [m].[object_id]
WHERE
        [t].[parent_class] = 0
        AND ([m].[definition] IS NULL OR [t].[type] <> N'TR')
        AND ([t].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [t].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))
) AS [_results] ORDER BY TriggerId DESC;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90DdlTriggerPopulator' as [PopulatorName];
SELECT * FROM (
SELECT DISTINCT
        [t].[object_id]         AS [ObjectId],
        [t].[name]              AS [Name],
        [t].[parent_class]      AS [TriggerScope],
        CONVERT(bit, [t].[is_disabled])            AS [IsDisabled],
        CONVERT(bit, ISNULL([sm].[uses_ansi_nulls], 0)) AS [IsAnsiNulls],
        CONVERT(bit, ISNULL([sm].[uses_quoted_identifier], 0)) AS [IsQuotedIdentifier],
        [sm].[definition] AS [Script],
        [sm].[execute_as_principal_id] AS [ExecuteAsId],
        [p].[name] AS [ExecuteAsName]
FROM
        [sys].[triggers] [t] WITH (NOLOCK)
        LEFT JOIN [sys].[sql_modules] [sm] WITH (NOLOCK) ON [sm].[object_id] = [t].[object_id]
        LEFT JOIN [sys].[database_principals] [p] WITH (NOLOCK) ON [p].[principal_id] = [sm].[execute_as_principal_id]
WHERE
        [t].[parent_class] = 0
        AND [t].[type] = N'TR' AND [sm].[definition] IS NOT NULL
        AND ([t].[is_ms_shipped] = 0)
) AS [_results] ORDER BY ObjectId DESC;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90DdlTriggerEventPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [e].[object_id] AS [TriggerId],
        [e].[type_desc] AS [EventType],
        [e].[is_first]  AS [IsFirst],
        [e].[is_last]   AS [IsLast],
        [t].[name]      AS [Name],
        [e].[type]      AS [EventTypeId],
        [e].[event_group_type] AS [EventGroupType]
FROM
        [sys].[trigger_events] [e] WITH (NOLOCK)
        INNER JOIN [sys].[triggers] [t] WITH (NOLOCK) ON [t].[object_id] = [e].[object_id]
        LEFT OUTER JOIN [sys].[sql_modules] [m] WITH (NOLOCK) ON [t].[object_id] = [m].[object_id]
WHERE
        [t].[parent_class] = 0
        AND [m].[definition] IS NOT NULL
        AND [t].[type] = N'TR'
        AND ([t].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [t].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))
) AS [_results] ORDER BY TriggerId DESC;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90SynonymPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [ssy].[object_id]    AS [ObjectId],
        [ssy].[name] AS [Name],
        [ssy].[schema_id] AS [SchemaId],
        SCHEMA_NAME([ssy].[schema_id]) AS [SchemaName],
        [ssy].[base_object_name] AS [ReferenceObjectName]
FROM    [sys].[synonyms] [ssy] WITH (NOLOCK)
WHERE   ([ssy].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [ssy].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))
) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.SqlDefaultPopulator' as [PopulatorName];
SELECT * FROM (
SELECT       [so].[object_id]           AS [LegacyId],
          SCHEMA_NAME([so].[schema_id]) AS [LegacySchemaName],
          [so].[schema_id]              AS [LegacySchemaId],
          [so].[name]                   AS [LegacyName],
          [sm].[definition]             AS [Script]
FROM      [sys].[objects]     AS [so] WITH (NOLOCK)
LEFT JOIN [sys].[sql_modules] AS [sm] WITH (NOLOCK) ON [sm].[object_id] = [so].[object_id]
LEFT JOIN [sys].[tables] AS [t]  WITH (NOLOCK) ON [t].[object_id] = [so].[object_id]
WHERE     OBJECTPROPERTY([so].[object_id], N'IsDefault') <> 0
          AND ([so].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [so].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))
) AS [_results] ORDER BY LegacyId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.SqlLegacyDataConstriantColumnBindingPopulator' as [PopulatorName];
SELECT * FROM (
SELECT     SCHEMA_NAME([do].[object_id]) AS [LegacySchemaName]
          ,[do].[name] AS [LegacyName]
          ,[do].[object_id] AS [LegacyId]
          ,SCHEMA_NAME([co].[schema_id]) AS [SchemaName]
          ,[co].[name] AS [ColumnSourceName]
          ,[sc].[name] AS [ColumnName]
          ,[sc].[object_id] AS [ColumnSourceId]
          ,[sc].[column_id] AS [ColumnId]
FROM       [sys].[columns] AS [sc] WITH (NOLOCK)
INNER JOIN [sys].[objects] AS [co] WITH (NOLOCK) ON [sc].[object_id] = [co].[object_id]
INNER JOIN [sys].[objects] AS [do] WITH (NOLOCK) ON [sc].[default_object_id] = [do].[object_id]
LEFT JOIN  [sys].[tables] AS [t] WITH (NOLOCK) ON [t].[object_id] = [sc].[object_id]
WHERE      OBJECTPROPERTY([do].[object_id], N'IsDefault') <> 0
           AND [sc].[is_computed] = 0 AND ([do].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [do].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))
) AS [_results] ORDER BY LegacyId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.SqlLegacyDataConstraintUddtBindingPopulator' as [PopulatorName];
SELECT * FROM (
SELECT     [sc].[name]        AS [UddtSchemaName]
          ,[st].[name]        AS [UddtName]
          ,[st].[user_type_id]   AS [UddtId]
          ,[lo].[object_id]   AS [LegacyId]
          ,SCHEMA_NAME([lo].[object_id]) AS [LegacySchemaName]
          ,[lo].[name]        AS [LegacyName]
FROM       [sys].[types]   AS [st] WITH (NOLOCK)
INNER JOIN [sys].[objects] AS [lo] WITH (NOLOCK) ON [lo].[object_id] = [st].[default_object_id]
LEFT  JOIN [sys].[schemas] AS [sc] WITH (NOLOCK) ON [sc].[schema_id] = [st].[schema_id]
LEFT  JOIN [sys].[tables] AS [t] WITH (NOLOCK) ON [t].[object_id] = [lo].[object_id]
WHERE      [st].[user_type_id] > 256
           AND OBJECTPROPERTY([lo].[object_id], N'IsDefault') <> 0
           AND ([lo].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [lo].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))
) AS [_results] ORDER BY LegacyId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.SqlRulePopulator' as [PopulatorName];
SELECT * FROM (
SELECT    [so].[object_id]              AS [LegacyId],
          SCHEMA_NAME([so].[schema_id]) AS [LegacySchemaName],
          [so].[schema_id]              AS [LegacySchemaId],
          [so].[name]                   AS [LegacyName],
          [sm].[definition]             AS [Script]
FROM      [sys].[objects]     AS [so] WITH (NOLOCK)
LEFT JOIN [sys].[sql_modules] AS [sm] WITH (NOLOCK) ON [sm].[object_id] = [so].[object_id]
WHERE     OBJECTPROPERTY([so].[object_id], N'IsRule') <> 0
          AND ([so].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [so].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))
) AS [_results] ORDER BY LegacyId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.SqlLegacyDataConstriantColumnBindingPopulator' as [PopulatorName];
SELECT * FROM (
SELECT     SCHEMA_NAME([do].[object_id]) AS [LegacySchemaName]
          ,[do].[name] AS [LegacyName]
          ,[do].[object_id] AS [LegacyId]
          ,SCHEMA_NAME([co].[schema_id]) AS [SchemaName]
          ,[co].[name] AS [ColumnSourceName]
          ,[sc].[name] AS [ColumnName]
          ,[sc].[object_id] AS [ColumnSourceId]
          ,[sc].[column_id] AS [ColumnId]
FROM       [sys].[columns] AS [sc] WITH (NOLOCK)
INNER JOIN [sys].[objects] AS [co] WITH (NOLOCK) ON [sc].[object_id] = [co].[object_id]
INNER JOIN [sys].[objects] AS [do] WITH (NOLOCK) ON [sc].[rule_object_id] = [do].[object_id]
WHERE      OBJECTPROPERTY([do].[object_id], N'IsRule') <> 0
           AND [sc].[is_computed] = 0 AND ([do].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [do].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))
) AS [_results] ORDER BY LegacyId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.SqlLegacyDataConstraintUddtBindingPopulator' as [PopulatorName];
SELECT * FROM (
SELECT     [sc].[name]        AS [UddtSchemaName]
          ,[st].[name]        AS [UddtName]
          ,[st].[user_type_id]   AS [UddtId]
          ,[lo].[object_id]   AS [LegacyId]
          ,SCHEMA_NAME([lo].[object_id]) AS [LegacySchemaName]
          ,[lo].[name]        AS [LegacyName]
FROM       [sys].[types]   AS [st] WITH (NOLOCK)
INNER JOIN [sys].[objects] AS [lo] WITH (NOLOCK) ON [lo].[object_id] = [st].[rule_object_id]
LEFT  JOIN [sys].[schemas] AS [sc] WITH (NOLOCK) ON [sc].[schema_id] = [st].[schema_id]
WHERE      [st].[user_type_id] > 256
           AND OBJECTPROPERTY([lo].[object_id], N'IsRule') <> 0
           AND ([lo].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [lo].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))
) AS [_results] ORDER BY LegacyId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90MessageTypePopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [s].[message_type_id]         AS [ObjectId],
        [s].[name]                    AS [Name],
        [s].[principal_id]            AS [OwnerId],
        USER_NAME([s].[principal_id]) AS [Owner],
        [s].[validation]              AS [ValidationType],
        [s].[xml_collection_id]       AS [XmlCollectionId],
        [x].[name]                    AS [XmlCollectionName],
        SCHEMA_NAME([x].[schema_id])  AS [XmlCollectionSchemaName]
FROM    [sys].[service_message_types] [s] WITH (NOLOCK)
LEFT    JOIN [sys].[xml_schema_collections] [x] WITH (NOLOCK) ON [x].[xml_collection_id] = [s].[xml_collection_id]
) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql120QueuePopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [ssq].[object_id]               AS [ObjectId],
        [ssq].[schema_id]               AS [SchemaId],
        SCHEMA_NAME([ssq].[schema_id])  AS [SchemaName],
        [ssq].[name]                    AS [Name], 
        [ssq].[max_readers]             AS [MaxReaders], 
        [ssq].[activation_procedure]    AS [ActivationProcedure], 
        [sdp2].[name]                   AS [ExecuteAsName], 
        [ssq].[execute_as_principal_id] AS [ExecuteAsId],
        [ssq].[is_activation_enabled]   AS [IsActivationEnabled], 
        [ssq].[is_receive_enabled]      AS [IsReceiveEnabled], 
        [ssq].[is_enqueue_enabled]      AS [IsEnqueueEnabled], 
        [ssq].[is_retention_enabled]    AS [IsRetentionEnabled],
        [ssq].[is_poison_message_handling_enabled] AS [IsPoisonMessageHandlingEnabled],
        [sfg].[type]                    AS [DataspaceType],
        [sin].[data_space_id]           AS [DataspaceId],
        [sfg].[name]                    AS [DataspaceName],
        [ssq].[create_date]             AS [CreatedDate]
FROM    
        [sys].[service_queues] [ssq] WITH (NOLOCK)
LEFT    JOIN [sys].[database_principals] [sdp2] WITH (NOLOCK) ON [sdp2].[principal_id] = [ssq].[execute_as_principal_id]
INNER   JOIN [sys].[internal_tables] [sit] WITH (NOLOCK) ON [ssq].[object_id] = [sit].[parent_object_id]
INNER   JOIN [sys].[indexes] [sin] WITH (NOLOCK) ON [sin].[object_id] = [sit].[object_id] AND [sin].[index_id] < 2
INNER   JOIN [sys].[filegroups] [sfg] WITH (NOLOCK) ON [sfg].[data_space_id] = [sin].[data_space_id]
WHERE   ([ssq].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [ssq].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90ContractPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [ssc].[service_contract_id] AS [ContractId],
        [ssc].[name] AS [ContractName],
        [ssc].[principal_id] AS [OwnerId],
        USER_NAME([ssc].[principal_id]) AS [Owner]
FROM    [sys].[service_contracts] [ssc] WITH (NOLOCK)
) AS [_results] ORDER BY ContractId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90ContractMessageTypeUsagePopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [scmu].[service_contract_id] AS [ContractId],
        [scmu].[message_type_id] AS [MessageTypeId],
        [ssc].[name] AS [ContractName],
        [mt].[name] AS [MessageTypeName],
        [scmu].[is_sent_by_initiator] AS [IsSentByInitiator],
        [scmu].[is_sent_by_target] AS [IsSentByTarget]
FROM    [sys].[service_contract_message_usages] [scmu] WITH (NOLOCK)
LEFT    JOIN [sys].[service_contracts] [ssc] WITH (NOLOCK) ON [ssc].[service_contract_id] = [scmu].[service_contract_id]
LEFT    JOIN [sys].[service_message_types] [mt] WITH (NOLOCK) ON [mt].[message_type_id] = [scmu].[message_type_id]
WHERE [ssc].[name] IS NOT NULL
) AS [_results] ORDER BY ContractId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90ServicePopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [svc].[service_id]             AS [ServiceId],
        [svc].[name]                   AS [ServiceName],
        [svc].[principal_id]           AS [OwnerId],
        USER_NAME([svc].[principal_id]) AS [Owner],
        [svc].[service_queue_id]       AS [QueueId],
        [ssq].[name]                   AS [QueueName],
        SCHEMA_NAME([ssq].[schema_id]) AS [QueueSchema]
FROM
        [sys].[services] [svc] WITH (NOLOCK)
LEFT    JOIN [sys].[service_queues] [ssq] WITH (NOLOCK) ON [svc].[service_queue_id] = [ssq].[object_id]
) AS [_results] ORDER BY ServiceId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90ServiceContractUsagePopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [scu].[service_id]             AS [ServiceId],
        [svc].[name]                   AS [ServiceName],
        [sc].[service_contract_id]     AS [ContractId],
        [sc].[name]                    AS [ContractName]
FROM
        [sys].[service_contract_usages] [scu] WITH (NOLOCK)
LEFT    JOIN [sys].[services] [svc] WITH (NOLOCK) ON [svc].[service_id] = [scu].[service_id]
LEFT    JOIN [sys].[service_contracts] [sc] WITH (NOLOCK) ON [sc].[service_contract_id] = [scu].[service_contract_id]
WHERE [sc].[name] IS NOT NULL
) AS [_results] ORDER BY ServiceId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90RoutePopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [r].[route_id]            AS [ObjectId],
        [r].[name]            AS [Name],
        [r].[remote_service_name] AS [RemoteServiceName],
        [r].[broker_instance]     AS [BrokerInstance],
        CASE WHEN [r].[lifetime] IS NULL THEN NULL ELSE DATEDIFF(s, GETUTCDATE(), [r].[lifetime]) END AS [LifeTime],
        [r].[address]             AS [Address],
        [r].[mirror_address]      AS [MirrorAddress],
        [r].[principal_id]        AS [OwnerId],
        USER_NAME([r].[principal_id]) AS [Owner]
FROM    [sys].[routes] [r] WITH (NOLOCK)
) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90EventNotificationPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [en].[object_id]    AS [EventNotificationId],
        [en].[name]         AS [Name],
        [en].[parent_class] AS [ScopeId],
        [en].[parent_id]    AS [OnObjectId],
        [o].[name]          AS [OnObjectName],
        SCHEMA_NAME([o].[schema_id]) AS [OnObjectSchema],
        [o].[type]          AS [OnObjectType],
        CONVERT(bit, CASE WHEN [en].[creator_sid] IS NOT NULL THEN 1 ELSE 0 END) AS [FanIn],
        [en].[service_name]      AS [ServiceName],
        [en].[broker_instance]   AS [BrokerInstance],
        CAST(CASE WHEN CONVERT(nvarchar(128), [db].[service_broker_guid]) = [en].[broker_instance] THEN 1 ELSE 0 END AS bit)
        AS [IsCurrentDatabase]
FROM
        [sys].[event_notifications] [en] WITH (NOLOCK)
LEFT    JOIN [sys].[databases] [db] WITH (NOLOCK) ON DB_NAME() = [db].[name]
LEFT    JOIN [sys].[objects] [o] WITH (NOLOCK) ON [o].[object_id] = [en].[parent_id] AND [en].[parent_class] = 1
) AS [_results] ORDER BY EventNotificationId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90EventNotificationEventTypePopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [e].[object_id] AS [EventNotificationId],
        [e].[type_desc] AS [EventType],
        [en].[name]     AS [Name],
        [o].[name]      AS [OnObjectName],
        SCHEMA_NAME([o].[schema_id]) AS [OnObjectSchema]
FROM
        [sys].[events] [e] WITH (NOLOCK)
        INNER JOIN [sys].[event_notifications] [en] WITH (NOLOCK) ON [e].[object_id] = [en].[object_id]
        LEFT JOIN [sys].[objects] [o] WITH (NOLOCK) ON [o].[object_id] = [en].[parent_id] AND [en].[parent_class] = 1
WHERE    [e].[is_trigger_event] = 0 AND [e].[event_group_type] IS NULL
) AS [_results] ORDER BY EventNotificationId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql100EventNotificationEventGroupPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [e].[object_id] AS [EventNotificationId],
        [e].[event_group_type] as [EventGroupType],
        [en].[name]     AS [Name],
        [o].[name]      AS [OnObjectName],
        SCHEMA_NAME([o].[schema_id]) AS [OnObjectSchema]
FROM
        [sys].[events] [e] WITH (NOLOCK)
        INNER JOIN [sys].[event_notifications] [en] WITH (NOLOCK) ON [e].[object_id] = [en].[object_id] AND [e].[is_trigger_event] = 0 AND [e].[event_group_type] IS NOT NULL
        LEFT JOIN [sys].[objects] [o] WITH (NOLOCK) ON [o].[object_id] = [en].[parent_id] AND [en].[parent_class] = 1
GROUP BY  [e].[object_id], [e].[event_group_type], [en].[name], [o].[name], [o].[schema_id]
) AS [_results] ORDER BY EventNotificationId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90RemoteServiceBindingPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [rs].[remote_service_binding_id] AS [ObjectId],
        [rs].[name]                AS [Name],
        [rs].[remote_service_name] AS [RemoteServiceName],
        [rs].[principal_id]        AS [OwnerId],
        USER_NAME([rs].[principal_id]) AS [Owner],
        [rs].[remote_principal_id] AS [RemoteUserId],
        USER_NAME([rs].[remote_principal_id]) AS [RemoteUser],
        [rs].[is_anonymous_on]     AS [IsAnonymous]
FROM
        [sys].[remote_service_bindings] [rs] WITH (NOLOCK)
) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql100BrokerPriorityPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
    [p].[name]			AS [Name],
    [p].[service_contract_id]	AS [ContractId],
    [c].[name]			AS [ContractName],
    [p].[priority]			AS [Priority],
    [s].[service_id]		AS [LocalServiceId],
    [s].[name]			AS [LocalServiceName],
    [p].[remote_service_name]	AS [RemoteServiceName]
    
FROM
    [sys].[conversation_priorities] [p] WITH (NOLOCK)
    LEFT JOIN [sys].[service_contracts] [c] WITH (NOLOCK) ON [c].[service_contract_id] = [p].[service_contract_id]
    LEFT JOIN [sys].[services] [s] WITH (NOLOCK) ON [p].[local_service_id] = [s].[service_id]
) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql100DatabaseAuditSpecificationPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [das].[database_specification_id]      AS [AuditSpecificationId],
        [das].[name]                           AS [AuditSpecificationName],
        [sa].[audit_id]			               AS [AuditId],  	
        [sa].[name]                            AS [AuditName],
        [das].[is_state_enabled]               AS [IsEnabled]
FROM
        [sys].[database_audit_specifications] [das] WITH (NOLOCK)
        LEFT OUTER JOIN [sys].[server_audits] [sa] WITH (NOLOCK) ON [das].[audit_guid] = [sa].[audit_guid]
) AS [_results] ORDER BY AuditSpecificationId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql100DatabaseAuditSpecificationDetailPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [dasd].[database_specification_id]        AS [AuditSpecificationId],
        [dasd].[audit_action_name]                AS [AuditAction],  
        [dasd].[class]                            AS [Class],
        [dasd].[major_id]                         AS [MajorId],
        [dasd].[minor_id]                         AS [MinorId],
        [o].[type]                                AS [ObjectType],
        CASE [dasd].[class]
            WHEN  0 THEN NULL
            WHEN  1 THEN SCHEMA_NAME([o].[schema_id])
            WHEN  3 THEN SCHEMA_NAME([dasd].[major_id])
            WHEN  4 THEN USER_NAME([dasd].[major_id])
            WHEN  5 THEN (SELECT TOP 1 [sa2].[name]   FROM [sys].[assemblies] [sa2] WITH (NOLOCK) WHERE [sa2].[assembly_id] = [dasd].[major_id])
            WHEN  6 THEN SCHEMA_NAME((SELECT TOP 1 [st].[schema_id] FROM [sys].[types] [st] WITH (NOLOCK) WHERE [st].[user_type_id] = [dasd].[major_id]))
            WHEN 10 THEN SCHEMA_NAME((SELECT TOP 1 [sxsc2].[schema_id] FROM [sys].[xml_schema_collections] [sxsc2] WITH (NOLOCK) WHERE [sxsc2].[xml_collection_id] = [dasd].[major_id]))
            WHEN 15 THEN (SELECT TOP 1 [smt2].[name]  FROM [sys].[service_message_types] [smt2] WITH (NOLOCK) WHERE [smt2].[message_type_id] = [dasd].[major_id]) COLLATE database_default
            WHEN 16 THEN (SELECT TOP 1 [ssc2].[name]  FROM [sys].[service_contracts] [ssc2] WITH (NOLOCK) WHERE [ssc2].[service_contract_id] = [dasd].[major_id])
            WHEN 17 THEN (SELECT TOP 1 [ss2].[name]   FROM [sys].[services] [ss2] WITH (NOLOCK) WHERE [ss2].[service_id] = [dasd].[major_id])
            WHEN 18 THEN (SELECT TOP 1 [srs2].[name]  FROM [sys].[remote_service_bindings] [srs2] WITH (NOLOCK) WHERE [srs2].[remote_service_binding_id] = [dasd].[major_id])
            WHEN 19 THEN (SELECT TOP 1 [sr2].[name]   FROM [sys].[routes] [sr2] WITH (NOLOCK) WHERE [sr2].[route_id] = [dasd].[major_id])
            WHEN 23 THEN (SELECT TOP 1 [ft].[name]    FROM [sys].[fulltext_catalogs] [ft] WITH (NOLOCK) WHERE [ft].[fulltext_catalog_id] = [dasd].[major_id])
            WHEN 24 THEN (SELECT TOP 1 [sk].[name]	  FROM [sys].[symmetric_keys] [sk] WITH (NOLOCK) WHERE [sk].[symmetric_key_id] = [dasd].[major_id])
            WHEN 25 THEN (SELECT TOP 1 [cert].[name]  FROM [sys].[certificates] [cert] WITH (NOLOCK) WHERE [cert].[certificate_id] = [dasd].[major_id])			
            WHEN 26 THEN (SELECT TOP 1 [ak].[name]	  FROM [sys].[asymmetric_keys] [ak] WITH (NOLOCK) WHERE [ak].[asymmetric_key_id] = [dasd].[major_id])
        END AS [Level0Name],
        CASE [dasd].[class] 
            WHEN 1  THEN [o].[name]
            WHEN 6  THEN (SELECT TOP 1 [st2].[name] FROM [sys].[types] [st2] WITH (NOLOCK) WHERE [st2].[user_type_id] = [dasd].[major_id])
            WHEN 10 THEN (SELECT TOP 1 [sx2].[name] FROM [sys].[xml_schema_collections] [sx2] WITH (NOLOCK) WHERE [sx2].[xml_collection_id] = [dasd].[major_id])
        END AS [Level1Name],
        [dasd].[audited_principal_id]             AS [PrincipalId],
        [dp].[name]                               AS [PrincipalName],
        [dasd].[is_group]                         AS [IsGroup],
        [das].[name]                              AS [AuditSpecificationName]    
FROM
        [sys].[database_audit_specification_details] [dasd] WITH (NOLOCK)
        INNER JOIN [sys].[database_audit_specifications] [das] WITH (NOLOCK) ON [dasd].[database_specification_id] = [das].[database_specification_id]
        LEFT JOIN [sys].[database_principals] [dp] WITH (NOLOCK) ON [dp].[principal_id] = [dasd].[audited_principal_id] AND [dasd].[is_group] = 0
        LEFT JOIN [sys].[all_objects] [o] WITH (NOLOCK) ON [dasd].[major_id] = [o].[object_id]
) AS [_results] ORDER BY AuditSpecificationId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90SignaturePopulator' as [PopulatorName];
SELECT * FROM (

SELECT [c].[class]                      AS [ObjectClass]
      ,[c].[major_id]                   AS [ObjectId]
      ,[c].[thumbprint]                 AS [Thumbprint]
      ,[c].[crypt_type]                 AS [CryptographyType]
      ,[c].[crypt_property]             AS [SignedBlob]
      ,NULL                             AS [ObjectType]
      ,[a].[name]                       AS [ObjectName]
      ,[k].[asymmetric_key_id]          AS [KeyId]
      ,[k].[name]                       AS [KeyName]
      ,[k].[pvt_key_encryption_type]    AS [PrivateKeyEncryptionType]
      ,NULL                             AS [SchemaName]
FROM [sys].[crypt_properties] [c] WITH (NOLOCK)
LEFT JOIN [sys].[assemblies] [a] WITH (NOLOCK) ON ([a].[assembly_id] = [c].[major_id])
LEFT JOIN [sys].[asymmetric_keys] [k] WITH (NOLOCK) ON ([c].[thumbprint] = [k].[thumbprint])
WHERE (([c].[crypt_type] = N'CPVA' OR [c].[crypt_type] = N'SPVA') AND [c].[class] = 5)

UNION

SELECT [c].[class]                      AS [ObjectClass]
      ,[c].[major_id]                   AS [ObjectId]
      ,[c].[thumbprint]                 AS [Thumbprint]
      ,[c].[crypt_type]                 AS [CryptographyType]
      ,[c].[crypt_property]             AS [SignedBlob]
      ,NULL                             AS [ObjectType]
      ,[a].[name]                       AS [ObjectName]
      ,[k].[certificate_id]             AS [KeyId]
      ,[k].[name]                       AS [KeyName]
      ,[k].[pvt_key_encryption_type]    AS [PrivateKeyEncryptionType]
      ,NULL                             AS [SchemaName]
FROM [sys].[crypt_properties] [c] WITH (NOLOCK)
LEFT JOIN [sys].[assemblies] [a] WITH (NOLOCK) ON ([a].[assembly_id] = [c].[major_id])
LEFT JOIN [sys].[certificates] [k] WITH (NOLOCK) ON ([c].[thumbprint] = [k].[thumbprint])
WHERE (([c].[crypt_type] = N'CPVC' OR [c].[crypt_type] = N'SPVC') AND [c].[class] = 5)
) AS [_results] ORDER BY ObjectName ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90SignaturePopulator' as [PopulatorName];
SELECT * FROM (
SELECT [c].[class]                      AS [ObjectClass]
      ,[c].[major_id]                   AS [ObjectId]
      ,[c].[thumbprint]                 AS [Thumbprint]
      ,[c].[crypt_type]                 AS [CryptographyType]
      ,[c].[crypt_property]             AS [SignedBlob]
      ,[o].[type]                       AS [ObjectType]
      ,[o].[name]                       AS [ObjectName]
      ,[k].[asymmetric_key_id]          AS [KeyId]
      ,[k].[name]                       AS [KeyName]
      ,[k].[pvt_key_encryption_type]    AS [PrivateKeyEncryptionType]
      ,[s].[name]                       AS [SchemaName]
FROM [sys].[crypt_properties] [c] WITH (NOLOCK)
LEFT JOIN [sys].[objects] [o] WITH (NOLOCK) ON ([o].[object_id] = [c].[major_id])
LEFT JOIN [sys].[asymmetric_keys] [k] WITH (NOLOCK) ON ([c].[thumbprint] = [k].[thumbprint])
LEFT JOIN [sys].[schemas] [s] WITH (NOLOCK) ON ([s].[schema_id] = [o].[schema_id])
WHERE (([c].[crypt_type] = N'CPVA' OR [c].[crypt_type] = N'SPVA') AND [c].[class] = 1 AND [o].[is_ms_shipped] = 0)

UNION

SELECT [c].[class]                      AS [ObjectClass]
      ,[c].[major_id]                   AS [ObjectId]
      ,[c].[thumbprint]                 AS [Thumbprint]
      ,[c].[crypt_type]                 AS [CryptographyType]
      ,[c].[crypt_property]             AS [SignedBlob]
      ,[o].[type]                       AS [ObjectType]
      ,[o].[name]                       AS [ObjectName]
      ,[k].[certificate_id]             AS [KeyId]
      ,[k].[name]                       AS [KeyName]
      ,[k].[pvt_key_encryption_type]    AS [PrivateKeyEncryptionType]
      ,[s].[name]                       AS [SchemaName]
FROM [sys].[crypt_properties] [c] WITH (NOLOCK)
LEFT JOIN [sys].[objects] [o] WITH (NOLOCK) ON ([o].[object_id] = [c].[major_id])
LEFT JOIN [sys].[certificates] [k] WITH (NOLOCK) ON ([c].[thumbprint] = [k].[thumbprint])
LEFT JOIN [sys].[schemas] [s] WITH (NOLOCK) ON ([s].[schema_id] = [o].[schema_id])
WHERE (([c].[crypt_type] = N'CPVC' OR [c].[crypt_type] = N'SPVC') AND [c].[class] = 1) AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) AS [_results] ORDER BY ObjectName ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql120SpecifiesDataCompressionOptionsPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        CASE 
            WHEN [i].[is_primary_key] = 1 THEN [pk].[object_id]
            WHEN [i].[is_unique_constraint] = 1 THEN [uq].[object_id]
            ELSE [o].[object_id]
        END  AS [ObjectId],
        CASE 
            WHEN [i].[is_primary_key] = 0 AND [i].[is_unique_constraint] = 0 THEN [i].[index_id]
            ELSE 0
        END  AS [ChildObjectId],
        [i].[type]                      AS [IndexType],
        [i].[is_primary_key]            AS [IsPrimaryKey],
        [i].[is_unique_constraint]      AS [IsUniqueConstraint],
        ISNULL([t].[is_filetable],0)    AS [IsFileTable],
        [p].[partition_number]          AS [PartitionNumber],
        [p].[data_compression]          AS [DataCompression],
        CONVERT(bit, CASE WHEN [i].[type] in (5, 6) THEN 1 ELSE 0 END) AS [IsColumnStoreIndex],
        CASE 
            WHEN [i].[is_primary_key] = 1 THEN [pk].[name]
            WHEN [i].[is_unique_constraint] = 1 THEN [uq].[name]
            ELSE [o].[name]
        END  AS [Name],        
       CASE 
            WHEN [i].[is_primary_key] = 1 THEN SCHEMA_NAME([pk].[schema_id])
            WHEN [i].[is_unique_constraint] = 1 THEN SCHEMA_NAME([uq].[schema_id])
            ELSE SCHEMA_NAME([o].[schema_id])
        END  AS [SchemaName],
        CASE 
            WHEN [i].[type] <> 0 THEN[o].[object_id]
            ELSE null
        END  AS [ParentObjectId],
        CASE 
            WHEN [i].[type] <> 0 THEN [o].[schema_id]
            ELSE null
        END  AS [ParentSchemaId],
        CASE 
            WHEN [i].[type] <> 0 THEN [o].[is_ms_shipped]
            ELSE 0
        END  AS [ParentIsMsShipped]
FROM
        [sys].[partitions] [p] WITH (NOLOCK)
        INNER JOIN [sys].[indexes] [i] WITH (NOLOCK) ON [p].[object_id] = [i].[object_id] AND [p].[index_id] = [i].[index_id]
        INNER JOIN [sys].[objects] [o] WITH (NOLOCK) ON [i].[object_id] = [o].[object_id]
        LEFT  JOIN [sys].[tables] AS [t] WITH (NOLOCK) ON [t].[object_id] = [o].[object_id]
        LEFT JOIN [sys].[key_constraints] as [pk] WITH(NOLOCK) ON [pk].[parent_object_id] = [i].[object_id] and [i].[is_primary_key] = 1 and [pk].[type]=N'PK' and [i].[index_id] = [pk].[unique_index_id]
        LEFT JOIN [sys].[key_constraints] as [uq] WITH(NOLOCK) ON [uq].[parent_object_id] = [i].[object_id] and [i].[is_unique_constraint] = 1 and [uq].[type]=N'UQ' and [i].[index_id] = [uq].[unique_index_id]
WHERE [i].[is_hypothetical] = 0
        AND 
        ( 
            ( 
                ([i].[type] = 0 OR [i].[type] = 1 OR [i].[type] = 2) 
                 AND ([p].[data_compression] = 1 OR [p].[data_compression] = 2)
            )
            OR 
            (
                ([i].[type] = 5 OR [i].[type] = 6)
                AND ([p].[data_compression] = 4)
            )
        )
        AND ([o].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [o].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))
) AS [_results] ORDER BY ObjectId,ChildObjectId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110SequencePopulator' as [PopulatorName];
SELECT * FROM (SELECT 
[s].[schema_id] AS [SchemaId],
SCHEMA_NAME([s].[schema_id]) AS [SchemaName],
[s].[object_id] AS [ObjectId],
[s].[name] AS [Name],
[s].[user_type_id] AS [TypeId],
[s].[precision] AS [Precision],
0 AS [Scale],
CONVERT(bit, ISNULL([types].[is_user_defined], 0)) AS [IsUserDefinedType],
[types].[name] AS [TypeName],
[basetypes].[name] AS [BaseTypeName],
SCHEMA_NAME([types].[schema_id]) AS [TypeSchemaName],
CASE WHEN [basetypes].[max_length] >= 0 AND [basetypes].[name] IN (N'nchar', N'nvarchar') THEN ([basetypes].[max_length] / 2) ELSE [basetypes].[max_length] END AS [Length],
[s].[start_value] AS [StartValue],
[s].[increment] AS [Increment],
[s].[minimum_value] AS [MinimumValue],
[s].[maximum_value] AS [MaximumValue],
[s].[is_cycling] AS [IsCycling],
[s].[is_cached] AS [IsCached],
[s].[cache_size] AS [CacheSize],
[s].[current_value] AS [CurrentValue],
[s].[is_exhausted] AS [IsExhausted]
FROM [sys].[sequences] AS [s] WITH (NOLOCK)
INNER JOIN [sys].[types] [basetypes] WITH (NOLOCK) ON [s].[system_type_id] = [basetypes].[system_type_id] AND [basetypes].[system_type_id] = [basetypes].[user_type_id]
LEFT JOIN [sys].[types] [types] WITH (NOLOCK) ON [s].[user_type_id] = [types].[user_type_id]) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql130SecurityPolicyPopulator' as [PopulatorName];
SELECT * FROM (SELECT    

        [sp].[name]                         AS [Name],
        [sp].[object_id]                    AS [Id],
        [sp].[schema_id]                    AS [SchemaId],
        SCHEMA_NAME([sp].[schema_id])       AS [SchemaName], 
        [sp].[is_enabled]                   AS [Enabled],
        [sp].[is_not_for_replication]       AS [NotForReplication],
        [sp].[is_schema_bound]              AS [IsSchemaBound]
        FROM [sys].[security_policies] [sp] WITH (NOLOCK)
        WHERE [is_ms_shipped] <> 1) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql130SecurityPredicatePopulator' as [PopulatorName];
SELECT 
        
        [pr].[object_id]                AS [PolicyId],
        [pl].[name]                     AS [PolicyName],
        SCHEMA_NAME([pl].[schema_id])   AS [PolicySchemaName],
        [pr].[security_predicate_id]    AS [PredicateId],
        [pr].[target_object_id]         AS [TargetObjectId],
        SCHEMA_NAME([tbl].[schema_id])  AS [TargetSchemaName],
        [tbl].[name]                    AS [TargetObjectName],
        [pr].[predicate_definition]     AS [Script],
        [pr].[predicate_type]           AS [PredicateType],
        [pr].[operation]                AS [Operation]
        FROM [sys].[security_predicates] AS [pr] WITH (NOLOCK)
        INNER JOIN [sys].[security_policies] AS [pl] WITH(NOLOCK) ON [pr].[object_id] = [pl].[object_id] AND [pl].[is_ms_shipped] <> 1
        INNER JOIN [sys].[tables] AS [tbl] WITH(NOLOCK) ON [pr].[target_object_id] = [tbl].[object_id];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.SqlColumnEncryptionKeyPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [cek].[column_encryption_key_id] as [ColumnEncryptionKeyId],
        [cek].[name] as [ColumnEncryptionName]
FROM 
        [sys].[column_encryption_keys] [cek] WITH (NOLOCK)
) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.SqlColumnEncryptionKeyValuePopulator' as [PopulatorName];

SELECT
        [cekval].[column_encryption_key_id] as [ColumnEncryptionKeyId],
        [cekval].[column_master_key_id] as [ColumnMasterKeyId],
        [cek].[name] as [ColumnEncryptionName],
        [cmk].[name] as [ColumnMasterKeyName],
        CONVERT(VARCHAR(8000), [cekval].[encrypted_value], 1) as [EncryptedValue],
        [cekval].[encryption_algorithm_name] as [EncryptionAlgorithm]
FROM 
        [sys].[column_encryption_key_values] [cekval] WITH (NOLOCK)
        INNER JOIN [sys].[column_encryption_keys] [cek] WITH (NOLOCK) ON [cekval].[column_encryption_key_id] = [cek].[column_encryption_key_id]
        INNER JOIN [sys].[column_master_keys] [cmk] WITH (NOLOCK) ON [cekval].[column_master_key_id] = [cmk].[column_master_key_id]
;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.SqlColumnMasterKeyPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [cmk].[column_master_key_id] as [ColumnMasterKeyId],
        [cmk].[name] as [ColumnMasterKeyName],
        [cmk].[key_store_provider_name] as [KeyStoreProvider],
        [cmk].[key_path] as [KeyPath]
FROM 
        [sys].[column_master_keys] [cmk] WITH (NOLOCK)
) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql130ExternalDataSourcePopulator' as [PopulatorName];
SELECT * FROM (
SELECT
	[eds].[name]                                    AS [Name],
	[eds].[data_source_id]                          AS [DataSourceId],
	[eds].[type]                                    AS [DataSourceType],
	[eds].[location]                                AS [Location],
	[eds].[resource_manager_location]               AS [ResourceManagerLocation],
	[c].[name]                                      AS [Credential]
FROM
	[sys].[external_data_sources]	AS [eds] WITH (NOLOCK)
	LEFT  JOIN [sys].[database_credentials] AS [c]  WITH (NOLOCK) ON [c].[credential_id] = [eds].[credential_id]
    WHERE [eds].[type] <> 3) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql130ExternalFileFormatPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
    [eff].[name] 							AS [Name],
	[eff].[file_format_id] 					AS [FileFormatId],
	CASE [eff].[format_type]
		WHEN 'DELIMITEDTEXT' THEN 0
		WHEN 'RCFILE' THEN 1
		WHEN 'ORC' THEN 2
        WHEN 'PARQUET' THEN 3
	END                                     AS [FormatType],
	[eff].[serde_method] 		            AS [SerDeMethod],
	[eff].[field_terminator] 	            AS [FieldTerminator],
	[eff].[string_delimiter]                AS [StringDelimiter],
	[eff].[date_format]                     AS [DateFormat],
	CAST(CASE [eff].[use_type_default]
	        WHEN 'FALSE' THEN 0
	        WHEN NULL THEN 0
	        WHEN 'TRUE' then 1
         END AS BIT)                        AS [UseTypeDefault],
	[eff].[data_compression]                AS [DataCompression]
FROM
	[sys].[external_file_formats]	AS [eff] WITH (NOLOCK)) AS [_results];
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql130ExternalTablePopulator' as [PopulatorName];
SELECT * FROM (
SELECT  
        [t].[schema_id]                             AS [SchemaId],
        SCHEMA_NAME([t].[schema_id])                AS [SchemaName],
        [t].[name]                                  AS [ColumnSourceName], 
        [t].[object_id]                             AS [TableId],
        [t].[type]                                  AS [Type],
        [ds].[type]                                 AS [DataspaceType],
        [ds].[data_space_id]                        AS [DataspaceId],
        [ds].[name]                                 AS [DataspaceName],
        [si].[index_id]                             AS [IndexId],
        [si].[type]                                 AS [IndexType],
        [st].[uses_ansi_nulls]		                AS [IsAnsiNulls],
        ISNULL(CAST(OBJECTPROPERTY([st].[object_id],N'IsQuotedIdentOn') AS bit),0) 
                                                    AS [IsQuotedIdentifier],
        ISNULL(CAST([st].[lock_on_bulk_load] AS bit),0)
                                                    AS [IsLockedOnBulkLoad],
        ISNULL([st].[text_in_row_limit],0)	        AS [TextInRowLimit],
        CAST([st].[large_value_types_out_of_row] AS bit)
                                                    AS [LargeValuesOutOfRow],
        CAST(OBJECTPROPERTY([st].[object_id], N'TableHasVarDecimalStorageFormat') AS bit)
                                                    AS [HasVarDecimalStorageFormat],
        [st].[is_tracked_by_cdc]                    AS [IsTrackedByCDC],
        [st].[lock_escalation]                      AS [LockEscalation],     
        [t].[create_date]                           AS [CreateDate],
        [eds].[name]                                AS [DataSourceName],        
        [eff].[name]                                AS [FileFormatName],
        [et].[location]                             AS [Location],
        CASE [et].[reject_type]
            WHEN 'VALUE' THEN 0
            WHEN NULL THEN 0
            WHEN 'PERCENTAGE' then 1 
        END                                         AS [RejectType],
        ISNULL([et].[reject_value],0)               AS [RejectValue],
        ISNULL([et].[reject_sample_value],-1)        AS [RejectSampleValue]
FROM    
        [sys].[objects] [t] WITH (NOLOCK)
        LEFT    JOIN [sys].[tables] [st] WITH (NOLOCK) ON [t].[object_id] = [st].[object_id]
        LEFT    JOIN (SELECT * FROM [sys].[indexes] WITH (NOLOCK) WHERE ISNULL([index_id],0) < 2) [si] ON [si].[object_id] = [st].[object_id]
        LEFT    JOIN [sys].[data_spaces] [ds] WITH (NOLOCK) ON [ds].[data_space_id] = [si].[data_space_id]        
        LEFT    JOIN [sys].[external_tables] [et] WITH (NOLOCK) ON [et].[object_id] = [st].[object_id]
	    LEFT    JOIN [sys].[external_data_sources] [eds]  WITH (NOLOCK) ON [eds].[data_source_id] = [et].[data_source_id]
        LEFT    JOIN [sys].[external_file_formats] [eff]  WITH (NOLOCK) ON [eff].[file_format_id] = [et].[file_format_id]
WHERE  [t].[type] = N'U' AND ISNULL([st].[is_filetable],0) = 0 AND ISNULL([st].[is_external],0) = 1 AND ([t].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [t].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) AS [_results] ORDER BY TableId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql130ExternalTableColumnPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [t].[name]                                              AS [ColumnSourceName], 
        [t].[object_id]                                         AS [TableId],
        SCHEMA_NAME([t].[schema_id])                            AS [SchemaName], 
        [c].[name]                                              AS [ColumnName], 
        [c].[user_type_id]                                      AS [TypeId],
        CONVERT(bit, ISNULL([types].[is_user_defined], 0))      AS [IsUserDefinedType],
        [types].[name]                                          AS [TypeName],
        [basetypes].[name]                                      AS [BaseTypeName],
        SCHEMA_NAME([types].[schema_id])                        AS [TypeSchemaName],
        [c].[column_id]                                         AS [ColumnId], 
        [c].[precision]                                         AS [Precision],
        [c].[scale]                                             AS [Scale],
        CASE 
            WHEN [c].[max_length] >= 0 AND [types].[name] IN (N'nchar', N'nvarchar') THEN ([c].[max_length] / 2) 
            ELSE [c].[max_length] 
        END                                                     AS [Length],
        CONVERT(bit, [c].[is_identity])                         AS [IsIdentity],
        CONVERT(bit, [c].[is_computed])                         AS [IsComputed],
        CONVERT(bit, ISNULL([ic].[is_not_for_replication], 0))  AS [IsNotForReplication],
        CAST(ISNULL([ic].[seed_value], 0) AS DECIMAL(38))       AS [IdentitySeed],
        CAST(ISNULL([ic].[increment_value], 0) AS DECIMAL(38))  AS [IdentityIncrement],
        CONVERT(bit, [c].[is_nullable])                         AS [IsNullable],
        [cc].[definition]                                       AS [ComputedText],
        [c].[is_rowguidcol]                                     AS [IsRowGuidColumn],
        [c].[collation_name]                                    AS [Collation],
        [c].[is_xml_document]                                   AS [IsXmlDocument],
        [c].[xml_collection_id]                                 AS [XmlCollectionId],
        [xscs].[name]                                           AS [XmlCollection],
        SCHEMA_NAME([xscs].[schema_id])                         AS [XmlCollectionSchemaName],
        [c].[is_sparse]                                         AS [IsSparse],
        [c].[is_column_set]                                     AS [IsColumnSet],
        [c].[is_filestream]                                     AS [IsFilestream],
        CASE [ta].[is_filetable] 
            WHEN 1 THEN N'UF' 
            ELSE N'U ' 
        END                                                     AS [Type],
        CONVERT(bit, ISNULL([cc].[is_persisted], 0))            AS [IsPersisted],
        CAST(ISNULL([indexCol].[IsPartitionColumn],0) AS BIT)   AS [IsPartitionColumn],
        CAST(0 AS BIT)                                                     AS [IsPrimaryKey],
        CAST(0 AS BIT)                                                     AS [IsForeignKey]
FROM    [sys].[columns] [c] WITH (NOLOCK)
        INNER   JOIN [sys].[objects] [t] WITH (NOLOCK) ON [c].[object_id] = [t].[object_id]
        LEFT	JOIN [sys].[tables] [ta] WITH(NOLOCK) ON [ta].[object_id] = [t].[object_id]
        LEFT    JOIN [sys].[types] [basetypes] WITH (NOLOCK) ON [c].[system_type_id] = [basetypes].[system_type_id] AND [basetypes].[system_type_id] = [basetypes].[user_type_id]
        LEFT    JOIN [sys].[types] [types] WITH (NOLOCK) ON [c].[user_type_id] = [types].[user_type_id]
        LEFT    JOIN [sys].[identity_columns] [ic] WITH (NOLOCK) ON [ic].[object_id] = [c].[object_id] AND [ic].[column_id] = [c].[column_id]
        LEFT    JOIN [sys].[computed_columns] [cc] WITH (NOLOCK) ON [cc].[object_id] = [c].[object_id] AND [cc].[column_id] = [c].[column_id]
        LEFT    JOIN [sys].[xml_schema_collections] [xscs] WITH (NOLOCK) ON [xscs].[xml_collection_id] = [c].[xml_collection_id]
        LEFT    JOIN (
            SELECT 1 AS [IsPartitionColumn], [indexCol].[column_id], [ix].[object_id] FROM [sys].[index_columns] [indexCol] WITH (NOLOCK) 
                INNER JOIN [sys].[indexes] [ix] WITH (NOLOCK) ON [indexCol].[index_id] = [ix].[index_id] AND [ix].[is_hypothetical] = 0 AND [ix].[type] IN (0,1,5) and [ix].[object_id] = [indexCol].[object_id]
            WHERE [indexCol].[partition_ordinal] > 0) AS [indexCol] ON [c].[object_id] = [indexCol].[object_id] AND [c].[column_id] = [indexCol].[column_id]
WHERE   [t].[type] = N'U' AND ISNULL([ta].[is_filetable],0) = 0 AND ISNULL([ta].[is_external],0) = 1 AND ([t].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [t].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       ))) AS [_results] ORDER BY TableId,ColumnId ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90DatabasePermissionStatementPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [db].[class] AS [TypeId],
        [db].[major_id] AS [MajorId],
        [db].[minor_id] AS [MinorId],
        NULL AS [ObjectType],
        CONVERT(bit, 0) AS [IsMsShipped],
        [db].[grantee_principal_id] AS [GranteeId],
        USER_NAME([db].[grantee_principal_id]) AS [GranteeName],
        [db].[grantor_principal_id] AS [GrantorId],
        CONVERT(BIT, CASE WHEN ([dp].[owning_principal_id] IS NULL OR [dp].[owning_principal_id] <> [db].[grantor_principal_id]) AND ([dp].[is_fixed_role] <> 1 AND [dp].[principal_id] <> 0) THEN 1 ELSE 0 END) AS [ScriptGrantor],
        USER_NAME([db].[grantor_principal_id]) AS [GrantorName],
        [db].[type] AS [PermissionCode],
        [db].[permission_name] AS [PermissionType],
        [db].[state] AS [PermissionState],
        CASE [db].[class]
            WHEN  0 THEN NULL
            WHEN  3 THEN SCHEMA_NAME([db].[major_id])
            WHEN  4 THEN USER_NAME([db].[major_id])
            WHEN  5 THEN (SELECT TOP 1 [sa2].[name]   FROM [sys].[assemblies] [sa2] WITH (NOLOCK) WHERE [sa2].[assembly_id] = [db].[major_id])
            WHEN  6 THEN SCHEMA_NAME((SELECT TOP 1 [st].[schema_id] FROM [sys].[types] [st] WITH (NOLOCK) WHERE [st].[user_type_id] = [db].[major_id]))
            WHEN 10 THEN SCHEMA_NAME((SELECT TOP 1 [sxsc2].[schema_id] FROM [sys].[xml_schema_collections] [sxsc2] WITH (NOLOCK) WHERE [sxsc2].[xml_collection_id] = [db].[major_id]))
            WHEN 15 THEN (SELECT TOP 1 [smt2].[name]  FROM [sys].[service_message_types] [smt2] WITH (NOLOCK) WHERE [smt2].[message_type_id] = [db].[major_id]) COLLATE database_default
            WHEN 16 THEN (SELECT TOP 1 [ssc2].[name]  FROM [sys].[service_contracts] [ssc2] WITH (NOLOCK) WHERE [ssc2].[service_contract_id] = [db].[major_id])
            WHEN 17 THEN (SELECT TOP 1 [ss2].[name]   FROM [sys].[services] [ss2] WITH (NOLOCK) WHERE [ss2].[service_id] = [db].[major_id])
            WHEN 18 THEN (SELECT TOP 1 [srs2].[name]  FROM [sys].[remote_service_bindings] [srs2] WITH (NOLOCK) WHERE [srs2].[remote_service_binding_id] = [db].[major_id])
            WHEN 19 THEN (SELECT TOP 1 [sr2].[name]   FROM [sys].[routes] [sr2] WITH (NOLOCK) WHERE [sr2].[route_id] = [db].[major_id])
            WHEN 23 THEN (SELECT TOP 1 [ft].[name]    FROM [sys].[fulltext_catalogs] [ft] WITH (NOLOCK) WHERE [ft].[fulltext_catalog_id] = [db].[major_id])
            WHEN 24 THEN (SELECT TOP 1 [sk].[name]    FROM [sys].[symmetric_keys] [sk] WITH (NOLOCK) WHERE [sk].[symmetric_key_id] = [db].[major_id])
            WHEN 25 THEN (SELECT TOP 1 [cert].[name]  FROM [sys].[certificates] [cert] WITH (NOLOCK) WHERE [cert].[certificate_id] = [db].[major_id])
            WHEN 26 THEN (SELECT TOP 1 [ak].[name]    FROM [sys].[asymmetric_keys] [ak] WITH (NOLOCK) WHERE [ak].[asymmetric_key_id] = [db].[major_id])
            WHEN 29 THEN (SELECT TOP 1 [ftsl].[name]    FROM [sys].[fulltext_stoplists] [ftsl] WITH (NOLOCK) WHERE [ftsl].[stoplist_id] = [db].[major_id])
WHEN 31 THEN (SELECT TOP 1 [spl].[name] FROM [sys].[registered_search_property_lists] [spl] WITH (NOLOCK) WHERE [spl].[property_list_id] = [db].[major_id])
        END AS [Level0Name],
        CASE [db].[class]
            WHEN 6  THEN (SELECT TOP 1 [st2].[name] FROM [sys].[types] [st2] WITH (NOLOCK) WHERE [st2].[user_type_id] = [db].[major_id])
            WHEN 10 THEN (SELECT TOP 1 [sx2].[name] FROM [sys].[xml_schema_collections] [sx2] WITH (NOLOCK) WHERE [sx2].[xml_collection_id] = [db].[major_id])
        END AS [Level1Name],
        NULL AS [Level2Name],
        [dp].[type] AS [DataBasePrincipalType]
FROM
        [sys].[database_permissions] [db] WITH (NOLOCK)
        LEFT JOIN [sys].[database_principals] as [dp] WITH (NOLOCK) ON [db].[major_id] = [dp].[principal_id] AND [db].[class] = 4
        INNER JOIN [sys].[database_principals] [dp2] WITH (NOLOCK) ON [dp2].[principal_id] = [db].[grantor_principal_id]
        INNER JOIN [sys].[database_principals] [dp3] WITH (NOLOCK) ON [dp3].[principal_id] = [db].[grantee_principal_id]
WHERE
        [db].[class] <> 1
        AND [db].[state] <> N'R'
        AND [db].[grantee_principal_id] <> [db].[grantor_principal_id]
		AND USER_NAME([db].[grantor_principal_id]) <> N'cdc'
		AND USER_NAME([db].[grantee_principal_id]) <> N'cdc'
        AND ([dp2].[owning_principal_id] IS NULL OR USER_NAME([dp2].[owning_principal_id]) <> N'cdc')
        AND ([dp3].[owning_principal_id] IS NULL OR USER_NAME([dp3].[owning_principal_id]) <> N'cdc')) AS [_results] WHERE [TypeId] = 0
 ORDER BY TypeId,GranteeId,GrantorId,MajorId,MinorId,PermissionCode,PermissionState ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90DatabasePermissionStatementPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [db].[class] AS [TypeId],
        [db].[major_id] AS [MajorId],
        [db].[minor_id] AS [MinorId],
        NULL AS [ObjectType],
        CONVERT(bit, 0) AS [IsMsShipped],
        [db].[grantee_principal_id] AS [GranteeId],
        USER_NAME([db].[grantee_principal_id]) AS [GranteeName],
        [db].[grantor_principal_id] AS [GrantorId],
        CONVERT(BIT, CASE WHEN ([dp].[owning_principal_id] IS NULL OR [dp].[owning_principal_id] <> [db].[grantor_principal_id]) AND ([dp].[is_fixed_role] <> 1 AND [dp].[principal_id] <> 0) THEN 1 ELSE 0 END) AS [ScriptGrantor],
        USER_NAME([db].[grantor_principal_id]) AS [GrantorName],
        [db].[type] AS [PermissionCode],
        [db].[permission_name] AS [PermissionType],
        [db].[state] AS [PermissionState],
        CASE [db].[class]
            WHEN  0 THEN NULL
            WHEN  3 THEN SCHEMA_NAME([db].[major_id])
            WHEN  4 THEN USER_NAME([db].[major_id])
            WHEN  5 THEN (SELECT TOP 1 [sa2].[name]   FROM [sys].[assemblies] [sa2] WITH (NOLOCK) WHERE [sa2].[assembly_id] = [db].[major_id])
            WHEN  6 THEN SCHEMA_NAME((SELECT TOP 1 [st].[schema_id] FROM [sys].[types] [st] WITH (NOLOCK) WHERE [st].[user_type_id] = [db].[major_id]))
            WHEN 10 THEN SCHEMA_NAME((SELECT TOP 1 [sxsc2].[schema_id] FROM [sys].[xml_schema_collections] [sxsc2] WITH (NOLOCK) WHERE [sxsc2].[xml_collection_id] = [db].[major_id]))
            WHEN 15 THEN (SELECT TOP 1 [smt2].[name]  FROM [sys].[service_message_types] [smt2] WITH (NOLOCK) WHERE [smt2].[message_type_id] = [db].[major_id]) COLLATE database_default
            WHEN 16 THEN (SELECT TOP 1 [ssc2].[name]  FROM [sys].[service_contracts] [ssc2] WITH (NOLOCK) WHERE [ssc2].[service_contract_id] = [db].[major_id])
            WHEN 17 THEN (SELECT TOP 1 [ss2].[name]   FROM [sys].[services] [ss2] WITH (NOLOCK) WHERE [ss2].[service_id] = [db].[major_id])
            WHEN 18 THEN (SELECT TOP 1 [srs2].[name]  FROM [sys].[remote_service_bindings] [srs2] WITH (NOLOCK) WHERE [srs2].[remote_service_binding_id] = [db].[major_id])
            WHEN 19 THEN (SELECT TOP 1 [sr2].[name]   FROM [sys].[routes] [sr2] WITH (NOLOCK) WHERE [sr2].[route_id] = [db].[major_id])
            WHEN 23 THEN (SELECT TOP 1 [ft].[name]    FROM [sys].[fulltext_catalogs] [ft] WITH (NOLOCK) WHERE [ft].[fulltext_catalog_id] = [db].[major_id])
            WHEN 24 THEN (SELECT TOP 1 [sk].[name]    FROM [sys].[symmetric_keys] [sk] WITH (NOLOCK) WHERE [sk].[symmetric_key_id] = [db].[major_id])
            WHEN 25 THEN (SELECT TOP 1 [cert].[name]  FROM [sys].[certificates] [cert] WITH (NOLOCK) WHERE [cert].[certificate_id] = [db].[major_id])
            WHEN 26 THEN (SELECT TOP 1 [ak].[name]    FROM [sys].[asymmetric_keys] [ak] WITH (NOLOCK) WHERE [ak].[asymmetric_key_id] = [db].[major_id])
            WHEN 29 THEN (SELECT TOP 1 [ftsl].[name]    FROM [sys].[fulltext_stoplists] [ftsl] WITH (NOLOCK) WHERE [ftsl].[stoplist_id] = [db].[major_id])
WHEN 31 THEN (SELECT TOP 1 [spl].[name] FROM [sys].[registered_search_property_lists] [spl] WITH (NOLOCK) WHERE [spl].[property_list_id] = [db].[major_id])
        END AS [Level0Name],
        CASE [db].[class]
            WHEN 6  THEN (SELECT TOP 1 [st2].[name] FROM [sys].[types] [st2] WITH (NOLOCK) WHERE [st2].[user_type_id] = [db].[major_id])
            WHEN 10 THEN (SELECT TOP 1 [sx2].[name] FROM [sys].[xml_schema_collections] [sx2] WITH (NOLOCK) WHERE [sx2].[xml_collection_id] = [db].[major_id])
        END AS [Level1Name],
        NULL AS [Level2Name],
        [dp].[type] AS [DataBasePrincipalType]
FROM
        [sys].[database_permissions] [db] WITH (NOLOCK)
        LEFT JOIN [sys].[database_principals] as [dp] WITH (NOLOCK) ON [db].[major_id] = [dp].[principal_id] AND [db].[class] = 4
        INNER JOIN [sys].[database_principals] [dp2] WITH (NOLOCK) ON [dp2].[principal_id] = [db].[grantor_principal_id]
        INNER JOIN [sys].[database_principals] [dp3] WITH (NOLOCK) ON [dp3].[principal_id] = [db].[grantee_principal_id]
WHERE
        [db].[class] <> 1
        AND [db].[state] <> N'R'
        AND [db].[grantee_principal_id] <> [db].[grantor_principal_id]
		AND USER_NAME([db].[grantor_principal_id]) <> N'cdc'
		AND USER_NAME([db].[grantee_principal_id]) <> N'cdc'
        AND ([dp2].[owning_principal_id] IS NULL OR USER_NAME([dp2].[owning_principal_id]) <> N'cdc')
        AND ([dp3].[owning_principal_id] IS NULL OR USER_NAME([dp3].[owning_principal_id]) <> N'cdc')) AS [_results] WHERE [TypeId] = 2
 ORDER BY TypeId,GranteeId,GrantorId,MajorId,MinorId,PermissionCode,PermissionState ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90DatabasePermissionStatementPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [db].[class] AS [TypeId],
        [db].[major_id] AS [MajorId],
        [db].[minor_id] AS [MinorId],
        NULL AS [ObjectType],
        CONVERT(bit, 0) AS [IsMsShipped],
        [db].[grantee_principal_id] AS [GranteeId],
        USER_NAME([db].[grantee_principal_id]) AS [GranteeName],
        [db].[grantor_principal_id] AS [GrantorId],
        CONVERT(BIT, CASE WHEN ([dp].[owning_principal_id] IS NULL OR [dp].[owning_principal_id] <> [db].[grantor_principal_id]) AND ([dp].[is_fixed_role] <> 1 AND [dp].[principal_id] <> 0) THEN 1 ELSE 0 END) AS [ScriptGrantor],
        USER_NAME([db].[grantor_principal_id]) AS [GrantorName],
        [db].[type] AS [PermissionCode],
        [db].[permission_name] AS [PermissionType],
        [db].[state] AS [PermissionState],
        CASE [db].[class]
            WHEN  0 THEN NULL
            WHEN  3 THEN SCHEMA_NAME([db].[major_id])
            WHEN  4 THEN USER_NAME([db].[major_id])
            WHEN  5 THEN (SELECT TOP 1 [sa2].[name]   FROM [sys].[assemblies] [sa2] WITH (NOLOCK) WHERE [sa2].[assembly_id] = [db].[major_id])
            WHEN  6 THEN SCHEMA_NAME((SELECT TOP 1 [st].[schema_id] FROM [sys].[types] [st] WITH (NOLOCK) WHERE [st].[user_type_id] = [db].[major_id]))
            WHEN 10 THEN SCHEMA_NAME((SELECT TOP 1 [sxsc2].[schema_id] FROM [sys].[xml_schema_collections] [sxsc2] WITH (NOLOCK) WHERE [sxsc2].[xml_collection_id] = [db].[major_id]))
            WHEN 15 THEN (SELECT TOP 1 [smt2].[name]  FROM [sys].[service_message_types] [smt2] WITH (NOLOCK) WHERE [smt2].[message_type_id] = [db].[major_id]) COLLATE database_default
            WHEN 16 THEN (SELECT TOP 1 [ssc2].[name]  FROM [sys].[service_contracts] [ssc2] WITH (NOLOCK) WHERE [ssc2].[service_contract_id] = [db].[major_id])
            WHEN 17 THEN (SELECT TOP 1 [ss2].[name]   FROM [sys].[services] [ss2] WITH (NOLOCK) WHERE [ss2].[service_id] = [db].[major_id])
            WHEN 18 THEN (SELECT TOP 1 [srs2].[name]  FROM [sys].[remote_service_bindings] [srs2] WITH (NOLOCK) WHERE [srs2].[remote_service_binding_id] = [db].[major_id])
            WHEN 19 THEN (SELECT TOP 1 [sr2].[name]   FROM [sys].[routes] [sr2] WITH (NOLOCK) WHERE [sr2].[route_id] = [db].[major_id])
            WHEN 23 THEN (SELECT TOP 1 [ft].[name]    FROM [sys].[fulltext_catalogs] [ft] WITH (NOLOCK) WHERE [ft].[fulltext_catalog_id] = [db].[major_id])
            WHEN 24 THEN (SELECT TOP 1 [sk].[name]    FROM [sys].[symmetric_keys] [sk] WITH (NOLOCK) WHERE [sk].[symmetric_key_id] = [db].[major_id])
            WHEN 25 THEN (SELECT TOP 1 [cert].[name]  FROM [sys].[certificates] [cert] WITH (NOLOCK) WHERE [cert].[certificate_id] = [db].[major_id])
            WHEN 26 THEN (SELECT TOP 1 [ak].[name]    FROM [sys].[asymmetric_keys] [ak] WITH (NOLOCK) WHERE [ak].[asymmetric_key_id] = [db].[major_id])
            WHEN 29 THEN (SELECT TOP 1 [ftsl].[name]    FROM [sys].[fulltext_stoplists] [ftsl] WITH (NOLOCK) WHERE [ftsl].[stoplist_id] = [db].[major_id])
WHEN 31 THEN (SELECT TOP 1 [spl].[name] FROM [sys].[registered_search_property_lists] [spl] WITH (NOLOCK) WHERE [spl].[property_list_id] = [db].[major_id])
        END AS [Level0Name],
        CASE [db].[class]
            WHEN 6  THEN (SELECT TOP 1 [st2].[name] FROM [sys].[types] [st2] WITH (NOLOCK) WHERE [st2].[user_type_id] = [db].[major_id])
            WHEN 10 THEN (SELECT TOP 1 [sx2].[name] FROM [sys].[xml_schema_collections] [sx2] WITH (NOLOCK) WHERE [sx2].[xml_collection_id] = [db].[major_id])
        END AS [Level1Name],
        NULL AS [Level2Name],
        [dp].[type] AS [DataBasePrincipalType]
FROM
        [sys].[database_permissions] [db] WITH (NOLOCK)
        LEFT JOIN [sys].[database_principals] as [dp] WITH (NOLOCK) ON [db].[major_id] = [dp].[principal_id] AND [db].[class] = 4
        INNER JOIN [sys].[database_principals] [dp2] WITH (NOLOCK) ON [dp2].[principal_id] = [db].[grantor_principal_id]
        INNER JOIN [sys].[database_principals] [dp3] WITH (NOLOCK) ON [dp3].[principal_id] = [db].[grantee_principal_id]
WHERE
        [db].[class] <> 1
        AND [db].[state] <> N'R'
        AND [db].[grantee_principal_id] <> [db].[grantor_principal_id]
		AND USER_NAME([db].[grantor_principal_id]) <> N'cdc'
		AND USER_NAME([db].[grantee_principal_id]) <> N'cdc'
        AND ([dp2].[owning_principal_id] IS NULL OR USER_NAME([dp2].[owning_principal_id]) <> N'cdc')
        AND ([dp3].[owning_principal_id] IS NULL OR USER_NAME([dp3].[owning_principal_id]) <> N'cdc')) AS [_results] WHERE [TypeId] = 3 AND [Level2Name] IS NULL AND [Level1Name] IS NULL
 ORDER BY TypeId,GranteeId,GrantorId,MajorId,MinorId,PermissionCode,PermissionState ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90DatabasePermissionStatementPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [db].[class] AS [TypeId],
        [db].[major_id] AS [MajorId],
        [db].[minor_id] AS [MinorId],
        NULL AS [ObjectType],
        CONVERT(bit, 0) AS [IsMsShipped],
        [db].[grantee_principal_id] AS [GranteeId],
        USER_NAME([db].[grantee_principal_id]) AS [GranteeName],
        [db].[grantor_principal_id] AS [GrantorId],
        CONVERT(BIT, CASE WHEN ([dp].[owning_principal_id] IS NULL OR [dp].[owning_principal_id] <> [db].[grantor_principal_id]) AND ([dp].[is_fixed_role] <> 1 AND [dp].[principal_id] <> 0) THEN 1 ELSE 0 END) AS [ScriptGrantor],
        USER_NAME([db].[grantor_principal_id]) AS [GrantorName],
        [db].[type] AS [PermissionCode],
        [db].[permission_name] AS [PermissionType],
        [db].[state] AS [PermissionState],
        CASE [db].[class]
            WHEN  0 THEN NULL
            WHEN  3 THEN SCHEMA_NAME([db].[major_id])
            WHEN  4 THEN USER_NAME([db].[major_id])
            WHEN  5 THEN (SELECT TOP 1 [sa2].[name]   FROM [sys].[assemblies] [sa2] WITH (NOLOCK) WHERE [sa2].[assembly_id] = [db].[major_id])
            WHEN  6 THEN SCHEMA_NAME((SELECT TOP 1 [st].[schema_id] FROM [sys].[types] [st] WITH (NOLOCK) WHERE [st].[user_type_id] = [db].[major_id]))
            WHEN 10 THEN SCHEMA_NAME((SELECT TOP 1 [sxsc2].[schema_id] FROM [sys].[xml_schema_collections] [sxsc2] WITH (NOLOCK) WHERE [sxsc2].[xml_collection_id] = [db].[major_id]))
            WHEN 15 THEN (SELECT TOP 1 [smt2].[name]  FROM [sys].[service_message_types] [smt2] WITH (NOLOCK) WHERE [smt2].[message_type_id] = [db].[major_id]) COLLATE database_default
            WHEN 16 THEN (SELECT TOP 1 [ssc2].[name]  FROM [sys].[service_contracts] [ssc2] WITH (NOLOCK) WHERE [ssc2].[service_contract_id] = [db].[major_id])
            WHEN 17 THEN (SELECT TOP 1 [ss2].[name]   FROM [sys].[services] [ss2] WITH (NOLOCK) WHERE [ss2].[service_id] = [db].[major_id])
            WHEN 18 THEN (SELECT TOP 1 [srs2].[name]  FROM [sys].[remote_service_bindings] [srs2] WITH (NOLOCK) WHERE [srs2].[remote_service_binding_id] = [db].[major_id])
            WHEN 19 THEN (SELECT TOP 1 [sr2].[name]   FROM [sys].[routes] [sr2] WITH (NOLOCK) WHERE [sr2].[route_id] = [db].[major_id])
            WHEN 23 THEN (SELECT TOP 1 [ft].[name]    FROM [sys].[fulltext_catalogs] [ft] WITH (NOLOCK) WHERE [ft].[fulltext_catalog_id] = [db].[major_id])
            WHEN 24 THEN (SELECT TOP 1 [sk].[name]    FROM [sys].[symmetric_keys] [sk] WITH (NOLOCK) WHERE [sk].[symmetric_key_id] = [db].[major_id])
            WHEN 25 THEN (SELECT TOP 1 [cert].[name]  FROM [sys].[certificates] [cert] WITH (NOLOCK) WHERE [cert].[certificate_id] = [db].[major_id])
            WHEN 26 THEN (SELECT TOP 1 [ak].[name]    FROM [sys].[asymmetric_keys] [ak] WITH (NOLOCK) WHERE [ak].[asymmetric_key_id] = [db].[major_id])
            WHEN 29 THEN (SELECT TOP 1 [ftsl].[name]    FROM [sys].[fulltext_stoplists] [ftsl] WITH (NOLOCK) WHERE [ftsl].[stoplist_id] = [db].[major_id])
WHEN 31 THEN (SELECT TOP 1 [spl].[name] FROM [sys].[registered_search_property_lists] [spl] WITH (NOLOCK) WHERE [spl].[property_list_id] = [db].[major_id])
        END AS [Level0Name],
        CASE [db].[class]
            WHEN 6  THEN (SELECT TOP 1 [st2].[name] FROM [sys].[types] [st2] WITH (NOLOCK) WHERE [st2].[user_type_id] = [db].[major_id])
            WHEN 10 THEN (SELECT TOP 1 [sx2].[name] FROM [sys].[xml_schema_collections] [sx2] WITH (NOLOCK) WHERE [sx2].[xml_collection_id] = [db].[major_id])
        END AS [Level1Name],
        NULL AS [Level2Name],
        [dp].[type] AS [DataBasePrincipalType]
FROM
        [sys].[database_permissions] [db] WITH (NOLOCK)
        LEFT JOIN [sys].[database_principals] as [dp] WITH (NOLOCK) ON [db].[major_id] = [dp].[principal_id] AND [db].[class] = 4
        INNER JOIN [sys].[database_principals] [dp2] WITH (NOLOCK) ON [dp2].[principal_id] = [db].[grantor_principal_id]
        INNER JOIN [sys].[database_principals] [dp3] WITH (NOLOCK) ON [dp3].[principal_id] = [db].[grantee_principal_id]
WHERE
        [db].[class] <> 1
        AND [db].[state] <> N'R'
        AND [db].[grantee_principal_id] <> [db].[grantor_principal_id]
		AND USER_NAME([db].[grantor_principal_id]) <> N'cdc'
		AND USER_NAME([db].[grantee_principal_id]) <> N'cdc'
        AND ([dp2].[owning_principal_id] IS NULL OR USER_NAME([dp2].[owning_principal_id]) <> N'cdc')
        AND ([dp3].[owning_principal_id] IS NULL OR USER_NAME([dp3].[owning_principal_id]) <> N'cdc')) AS [_results] WHERE [TypeId] = 4 AND [Level2Name] IS NULL AND [Level1Name] IS NULL
 ORDER BY TypeId,GranteeId,GrantorId,MajorId,MinorId,PermissionCode,PermissionState ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90DatabasePermissionStatementPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [db].[class] AS [TypeId],
        [db].[major_id] AS [MajorId],
        [db].[minor_id] AS [MinorId],
        NULL AS [ObjectType],
        CONVERT(bit, 0) AS [IsMsShipped],
        [db].[grantee_principal_id] AS [GranteeId],
        USER_NAME([db].[grantee_principal_id]) AS [GranteeName],
        [db].[grantor_principal_id] AS [GrantorId],
        CONVERT(BIT, CASE WHEN ([dp].[owning_principal_id] IS NULL OR [dp].[owning_principal_id] <> [db].[grantor_principal_id]) AND ([dp].[is_fixed_role] <> 1 AND [dp].[principal_id] <> 0) THEN 1 ELSE 0 END) AS [ScriptGrantor],
        USER_NAME([db].[grantor_principal_id]) AS [GrantorName],
        [db].[type] AS [PermissionCode],
        [db].[permission_name] AS [PermissionType],
        [db].[state] AS [PermissionState],
        CASE [db].[class]
            WHEN  0 THEN NULL
            WHEN  3 THEN SCHEMA_NAME([db].[major_id])
            WHEN  4 THEN USER_NAME([db].[major_id])
            WHEN  5 THEN (SELECT TOP 1 [sa2].[name]   FROM [sys].[assemblies] [sa2] WITH (NOLOCK) WHERE [sa2].[assembly_id] = [db].[major_id])
            WHEN  6 THEN SCHEMA_NAME((SELECT TOP 1 [st].[schema_id] FROM [sys].[types] [st] WITH (NOLOCK) WHERE [st].[user_type_id] = [db].[major_id]))
            WHEN 10 THEN SCHEMA_NAME((SELECT TOP 1 [sxsc2].[schema_id] FROM [sys].[xml_schema_collections] [sxsc2] WITH (NOLOCK) WHERE [sxsc2].[xml_collection_id] = [db].[major_id]))
            WHEN 15 THEN (SELECT TOP 1 [smt2].[name]  FROM [sys].[service_message_types] [smt2] WITH (NOLOCK) WHERE [smt2].[message_type_id] = [db].[major_id]) COLLATE database_default
            WHEN 16 THEN (SELECT TOP 1 [ssc2].[name]  FROM [sys].[service_contracts] [ssc2] WITH (NOLOCK) WHERE [ssc2].[service_contract_id] = [db].[major_id])
            WHEN 17 THEN (SELECT TOP 1 [ss2].[name]   FROM [sys].[services] [ss2] WITH (NOLOCK) WHERE [ss2].[service_id] = [db].[major_id])
            WHEN 18 THEN (SELECT TOP 1 [srs2].[name]  FROM [sys].[remote_service_bindings] [srs2] WITH (NOLOCK) WHERE [srs2].[remote_service_binding_id] = [db].[major_id])
            WHEN 19 THEN (SELECT TOP 1 [sr2].[name]   FROM [sys].[routes] [sr2] WITH (NOLOCK) WHERE [sr2].[route_id] = [db].[major_id])
            WHEN 23 THEN (SELECT TOP 1 [ft].[name]    FROM [sys].[fulltext_catalogs] [ft] WITH (NOLOCK) WHERE [ft].[fulltext_catalog_id] = [db].[major_id])
            WHEN 24 THEN (SELECT TOP 1 [sk].[name]    FROM [sys].[symmetric_keys] [sk] WITH (NOLOCK) WHERE [sk].[symmetric_key_id] = [db].[major_id])
            WHEN 25 THEN (SELECT TOP 1 [cert].[name]  FROM [sys].[certificates] [cert] WITH (NOLOCK) WHERE [cert].[certificate_id] = [db].[major_id])
            WHEN 26 THEN (SELECT TOP 1 [ak].[name]    FROM [sys].[asymmetric_keys] [ak] WITH (NOLOCK) WHERE [ak].[asymmetric_key_id] = [db].[major_id])
            WHEN 29 THEN (SELECT TOP 1 [ftsl].[name]    FROM [sys].[fulltext_stoplists] [ftsl] WITH (NOLOCK) WHERE [ftsl].[stoplist_id] = [db].[major_id])
WHEN 31 THEN (SELECT TOP 1 [spl].[name] FROM [sys].[registered_search_property_lists] [spl] WITH (NOLOCK) WHERE [spl].[property_list_id] = [db].[major_id])
        END AS [Level0Name],
        CASE [db].[class]
            WHEN 6  THEN (SELECT TOP 1 [st2].[name] FROM [sys].[types] [st2] WITH (NOLOCK) WHERE [st2].[user_type_id] = [db].[major_id])
            WHEN 10 THEN (SELECT TOP 1 [sx2].[name] FROM [sys].[xml_schema_collections] [sx2] WITH (NOLOCK) WHERE [sx2].[xml_collection_id] = [db].[major_id])
        END AS [Level1Name],
        NULL AS [Level2Name],
        [dp].[type] AS [DataBasePrincipalType]
FROM
        [sys].[database_permissions] [db] WITH (NOLOCK)
        LEFT JOIN [sys].[database_principals] as [dp] WITH (NOLOCK) ON [db].[major_id] = [dp].[principal_id] AND [db].[class] = 4
        INNER JOIN [sys].[database_principals] [dp2] WITH (NOLOCK) ON [dp2].[principal_id] = [db].[grantor_principal_id]
        INNER JOIN [sys].[database_principals] [dp3] WITH (NOLOCK) ON [dp3].[principal_id] = [db].[grantee_principal_id]
WHERE
        [db].[class] <> 1
        AND [db].[state] <> N'R'
        AND [db].[grantee_principal_id] <> [db].[grantor_principal_id]
		AND USER_NAME([db].[grantor_principal_id]) <> N'cdc'
		AND USER_NAME([db].[grantee_principal_id]) <> N'cdc'
        AND ([dp2].[owning_principal_id] IS NULL OR USER_NAME([dp2].[owning_principal_id]) <> N'cdc')
        AND ([dp3].[owning_principal_id] IS NULL OR USER_NAME([dp3].[owning_principal_id]) <> N'cdc')) AS [_results] WHERE [TypeId] = 5 AND [Level2Name] IS NULL AND [Level1Name] IS NULL
 ORDER BY TypeId,GranteeId,GrantorId,MajorId,MinorId,PermissionCode,PermissionState ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90DatabasePermissionStatementPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [db].[class] AS [TypeId],
        [db].[major_id] AS [MajorId],
        [db].[minor_id] AS [MinorId],
        NULL AS [ObjectType],
        CONVERT(bit, 0) AS [IsMsShipped],
        [db].[grantee_principal_id] AS [GranteeId],
        USER_NAME([db].[grantee_principal_id]) AS [GranteeName],
        [db].[grantor_principal_id] AS [GrantorId],
        CONVERT(BIT, CASE WHEN ([dp].[owning_principal_id] IS NULL OR [dp].[owning_principal_id] <> [db].[grantor_principal_id]) AND ([dp].[is_fixed_role] <> 1 AND [dp].[principal_id] <> 0) THEN 1 ELSE 0 END) AS [ScriptGrantor],
        USER_NAME([db].[grantor_principal_id]) AS [GrantorName],
        [db].[type] AS [PermissionCode],
        [db].[permission_name] AS [PermissionType],
        [db].[state] AS [PermissionState],
        CASE [db].[class]
            WHEN  0 THEN NULL
            WHEN  3 THEN SCHEMA_NAME([db].[major_id])
            WHEN  4 THEN USER_NAME([db].[major_id])
            WHEN  5 THEN (SELECT TOP 1 [sa2].[name]   FROM [sys].[assemblies] [sa2] WITH (NOLOCK) WHERE [sa2].[assembly_id] = [db].[major_id])
            WHEN  6 THEN SCHEMA_NAME((SELECT TOP 1 [st].[schema_id] FROM [sys].[types] [st] WITH (NOLOCK) WHERE [st].[user_type_id] = [db].[major_id]))
            WHEN 10 THEN SCHEMA_NAME((SELECT TOP 1 [sxsc2].[schema_id] FROM [sys].[xml_schema_collections] [sxsc2] WITH (NOLOCK) WHERE [sxsc2].[xml_collection_id] = [db].[major_id]))
            WHEN 15 THEN (SELECT TOP 1 [smt2].[name]  FROM [sys].[service_message_types] [smt2] WITH (NOLOCK) WHERE [smt2].[message_type_id] = [db].[major_id]) COLLATE database_default
            WHEN 16 THEN (SELECT TOP 1 [ssc2].[name]  FROM [sys].[service_contracts] [ssc2] WITH (NOLOCK) WHERE [ssc2].[service_contract_id] = [db].[major_id])
            WHEN 17 THEN (SELECT TOP 1 [ss2].[name]   FROM [sys].[services] [ss2] WITH (NOLOCK) WHERE [ss2].[service_id] = [db].[major_id])
            WHEN 18 THEN (SELECT TOP 1 [srs2].[name]  FROM [sys].[remote_service_bindings] [srs2] WITH (NOLOCK) WHERE [srs2].[remote_service_binding_id] = [db].[major_id])
            WHEN 19 THEN (SELECT TOP 1 [sr2].[name]   FROM [sys].[routes] [sr2] WITH (NOLOCK) WHERE [sr2].[route_id] = [db].[major_id])
            WHEN 23 THEN (SELECT TOP 1 [ft].[name]    FROM [sys].[fulltext_catalogs] [ft] WITH (NOLOCK) WHERE [ft].[fulltext_catalog_id] = [db].[major_id])
            WHEN 24 THEN (SELECT TOP 1 [sk].[name]    FROM [sys].[symmetric_keys] [sk] WITH (NOLOCK) WHERE [sk].[symmetric_key_id] = [db].[major_id])
            WHEN 25 THEN (SELECT TOP 1 [cert].[name]  FROM [sys].[certificates] [cert] WITH (NOLOCK) WHERE [cert].[certificate_id] = [db].[major_id])
            WHEN 26 THEN (SELECT TOP 1 [ak].[name]    FROM [sys].[asymmetric_keys] [ak] WITH (NOLOCK) WHERE [ak].[asymmetric_key_id] = [db].[major_id])
            WHEN 29 THEN (SELECT TOP 1 [ftsl].[name]    FROM [sys].[fulltext_stoplists] [ftsl] WITH (NOLOCK) WHERE [ftsl].[stoplist_id] = [db].[major_id])
WHEN 31 THEN (SELECT TOP 1 [spl].[name] FROM [sys].[registered_search_property_lists] [spl] WITH (NOLOCK) WHERE [spl].[property_list_id] = [db].[major_id])
        END AS [Level0Name],
        CASE [db].[class]
            WHEN 6  THEN (SELECT TOP 1 [st2].[name] FROM [sys].[types] [st2] WITH (NOLOCK) WHERE [st2].[user_type_id] = [db].[major_id])
            WHEN 10 THEN (SELECT TOP 1 [sx2].[name] FROM [sys].[xml_schema_collections] [sx2] WITH (NOLOCK) WHERE [sx2].[xml_collection_id] = [db].[major_id])
        END AS [Level1Name],
        NULL AS [Level2Name],
        [dp].[type] AS [DataBasePrincipalType]
FROM
        [sys].[database_permissions] [db] WITH (NOLOCK)
        LEFT JOIN [sys].[database_principals] as [dp] WITH (NOLOCK) ON [db].[major_id] = [dp].[principal_id] AND [db].[class] = 4
        INNER JOIN [sys].[database_principals] [dp2] WITH (NOLOCK) ON [dp2].[principal_id] = [db].[grantor_principal_id]
        INNER JOIN [sys].[database_principals] [dp3] WITH (NOLOCK) ON [dp3].[principal_id] = [db].[grantee_principal_id]
WHERE
        [db].[class] <> 1
        AND [db].[state] <> N'R'
        AND [db].[grantee_principal_id] <> [db].[grantor_principal_id]
		AND USER_NAME([db].[grantor_principal_id]) <> N'cdc'
		AND USER_NAME([db].[grantee_principal_id]) <> N'cdc'
        AND ([dp2].[owning_principal_id] IS NULL OR USER_NAME([dp2].[owning_principal_id]) <> N'cdc')
        AND ([dp3].[owning_principal_id] IS NULL OR USER_NAME([dp3].[owning_principal_id]) <> N'cdc')) AS [_results] WHERE [TypeId] = 6
 ORDER BY TypeId,GranteeId,GrantorId,MajorId,MinorId,PermissionCode,PermissionState ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90DatabasePermissionStatementPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [db].[class] AS [TypeId],
        [db].[major_id] AS [MajorId],
        [db].[minor_id] AS [MinorId],
        NULL AS [ObjectType],
        CONVERT(bit, 0) AS [IsMsShipped],
        [db].[grantee_principal_id] AS [GranteeId],
        USER_NAME([db].[grantee_principal_id]) AS [GranteeName],
        [db].[grantor_principal_id] AS [GrantorId],
        CONVERT(BIT, CASE WHEN ([dp].[owning_principal_id] IS NULL OR [dp].[owning_principal_id] <> [db].[grantor_principal_id]) AND ([dp].[is_fixed_role] <> 1 AND [dp].[principal_id] <> 0) THEN 1 ELSE 0 END) AS [ScriptGrantor],
        USER_NAME([db].[grantor_principal_id]) AS [GrantorName],
        [db].[type] AS [PermissionCode],
        [db].[permission_name] AS [PermissionType],
        [db].[state] AS [PermissionState],
        CASE [db].[class]
            WHEN  0 THEN NULL
            WHEN  3 THEN SCHEMA_NAME([db].[major_id])
            WHEN  4 THEN USER_NAME([db].[major_id])
            WHEN  5 THEN (SELECT TOP 1 [sa2].[name]   FROM [sys].[assemblies] [sa2] WITH (NOLOCK) WHERE [sa2].[assembly_id] = [db].[major_id])
            WHEN  6 THEN SCHEMA_NAME((SELECT TOP 1 [st].[schema_id] FROM [sys].[types] [st] WITH (NOLOCK) WHERE [st].[user_type_id] = [db].[major_id]))
            WHEN 10 THEN SCHEMA_NAME((SELECT TOP 1 [sxsc2].[schema_id] FROM [sys].[xml_schema_collections] [sxsc2] WITH (NOLOCK) WHERE [sxsc2].[xml_collection_id] = [db].[major_id]))
            WHEN 15 THEN (SELECT TOP 1 [smt2].[name]  FROM [sys].[service_message_types] [smt2] WITH (NOLOCK) WHERE [smt2].[message_type_id] = [db].[major_id]) COLLATE database_default
            WHEN 16 THEN (SELECT TOP 1 [ssc2].[name]  FROM [sys].[service_contracts] [ssc2] WITH (NOLOCK) WHERE [ssc2].[service_contract_id] = [db].[major_id])
            WHEN 17 THEN (SELECT TOP 1 [ss2].[name]   FROM [sys].[services] [ss2] WITH (NOLOCK) WHERE [ss2].[service_id] = [db].[major_id])
            WHEN 18 THEN (SELECT TOP 1 [srs2].[name]  FROM [sys].[remote_service_bindings] [srs2] WITH (NOLOCK) WHERE [srs2].[remote_service_binding_id] = [db].[major_id])
            WHEN 19 THEN (SELECT TOP 1 [sr2].[name]   FROM [sys].[routes] [sr2] WITH (NOLOCK) WHERE [sr2].[route_id] = [db].[major_id])
            WHEN 23 THEN (SELECT TOP 1 [ft].[name]    FROM [sys].[fulltext_catalogs] [ft] WITH (NOLOCK) WHERE [ft].[fulltext_catalog_id] = [db].[major_id])
            WHEN 24 THEN (SELECT TOP 1 [sk].[name]    FROM [sys].[symmetric_keys] [sk] WITH (NOLOCK) WHERE [sk].[symmetric_key_id] = [db].[major_id])
            WHEN 25 THEN (SELECT TOP 1 [cert].[name]  FROM [sys].[certificates] [cert] WITH (NOLOCK) WHERE [cert].[certificate_id] = [db].[major_id])
            WHEN 26 THEN (SELECT TOP 1 [ak].[name]    FROM [sys].[asymmetric_keys] [ak] WITH (NOLOCK) WHERE [ak].[asymmetric_key_id] = [db].[major_id])
            WHEN 29 THEN (SELECT TOP 1 [ftsl].[name]    FROM [sys].[fulltext_stoplists] [ftsl] WITH (NOLOCK) WHERE [ftsl].[stoplist_id] = [db].[major_id])
WHEN 31 THEN (SELECT TOP 1 [spl].[name] FROM [sys].[registered_search_property_lists] [spl] WITH (NOLOCK) WHERE [spl].[property_list_id] = [db].[major_id])
        END AS [Level0Name],
        CASE [db].[class]
            WHEN 6  THEN (SELECT TOP 1 [st2].[name] FROM [sys].[types] [st2] WITH (NOLOCK) WHERE [st2].[user_type_id] = [db].[major_id])
            WHEN 10 THEN (SELECT TOP 1 [sx2].[name] FROM [sys].[xml_schema_collections] [sx2] WITH (NOLOCK) WHERE [sx2].[xml_collection_id] = [db].[major_id])
        END AS [Level1Name],
        NULL AS [Level2Name],
        [dp].[type] AS [DataBasePrincipalType]
FROM
        [sys].[database_permissions] [db] WITH (NOLOCK)
        LEFT JOIN [sys].[database_principals] as [dp] WITH (NOLOCK) ON [db].[major_id] = [dp].[principal_id] AND [db].[class] = 4
        INNER JOIN [sys].[database_principals] [dp2] WITH (NOLOCK) ON [dp2].[principal_id] = [db].[grantor_principal_id]
        INNER JOIN [sys].[database_principals] [dp3] WITH (NOLOCK) ON [dp3].[principal_id] = [db].[grantee_principal_id]
WHERE
        [db].[class] <> 1
        AND [db].[state] <> N'R'
        AND [db].[grantee_principal_id] <> [db].[grantor_principal_id]
		AND USER_NAME([db].[grantor_principal_id]) <> N'cdc'
		AND USER_NAME([db].[grantee_principal_id]) <> N'cdc'
        AND ([dp2].[owning_principal_id] IS NULL OR USER_NAME([dp2].[owning_principal_id]) <> N'cdc')
        AND ([dp3].[owning_principal_id] IS NULL OR USER_NAME([dp3].[owning_principal_id]) <> N'cdc')) AS [_results] WHERE [TypeId] = 7
 ORDER BY TypeId,GranteeId,GrantorId,MajorId,MinorId,PermissionCode,PermissionState ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90DatabasePermissionStatementPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [db].[class] AS [TypeId],
        [db].[major_id] AS [MajorId],
        [db].[minor_id] AS [MinorId],
        NULL AS [ObjectType],
        CONVERT(bit, 0) AS [IsMsShipped],
        [db].[grantee_principal_id] AS [GranteeId],
        USER_NAME([db].[grantee_principal_id]) AS [GranteeName],
        [db].[grantor_principal_id] AS [GrantorId],
        CONVERT(BIT, CASE WHEN ([dp].[owning_principal_id] IS NULL OR [dp].[owning_principal_id] <> [db].[grantor_principal_id]) AND ([dp].[is_fixed_role] <> 1 AND [dp].[principal_id] <> 0) THEN 1 ELSE 0 END) AS [ScriptGrantor],
        USER_NAME([db].[grantor_principal_id]) AS [GrantorName],
        [db].[type] AS [PermissionCode],
        [db].[permission_name] AS [PermissionType],
        [db].[state] AS [PermissionState],
        CASE [db].[class]
            WHEN  0 THEN NULL
            WHEN  3 THEN SCHEMA_NAME([db].[major_id])
            WHEN  4 THEN USER_NAME([db].[major_id])
            WHEN  5 THEN (SELECT TOP 1 [sa2].[name]   FROM [sys].[assemblies] [sa2] WITH (NOLOCK) WHERE [sa2].[assembly_id] = [db].[major_id])
            WHEN  6 THEN SCHEMA_NAME((SELECT TOP 1 [st].[schema_id] FROM [sys].[types] [st] WITH (NOLOCK) WHERE [st].[user_type_id] = [db].[major_id]))
            WHEN 10 THEN SCHEMA_NAME((SELECT TOP 1 [sxsc2].[schema_id] FROM [sys].[xml_schema_collections] [sxsc2] WITH (NOLOCK) WHERE [sxsc2].[xml_collection_id] = [db].[major_id]))
            WHEN 15 THEN (SELECT TOP 1 [smt2].[name]  FROM [sys].[service_message_types] [smt2] WITH (NOLOCK) WHERE [smt2].[message_type_id] = [db].[major_id]) COLLATE database_default
            WHEN 16 THEN (SELECT TOP 1 [ssc2].[name]  FROM [sys].[service_contracts] [ssc2] WITH (NOLOCK) WHERE [ssc2].[service_contract_id] = [db].[major_id])
            WHEN 17 THEN (SELECT TOP 1 [ss2].[name]   FROM [sys].[services] [ss2] WITH (NOLOCK) WHERE [ss2].[service_id] = [db].[major_id])
            WHEN 18 THEN (SELECT TOP 1 [srs2].[name]  FROM [sys].[remote_service_bindings] [srs2] WITH (NOLOCK) WHERE [srs2].[remote_service_binding_id] = [db].[major_id])
            WHEN 19 THEN (SELECT TOP 1 [sr2].[name]   FROM [sys].[routes] [sr2] WITH (NOLOCK) WHERE [sr2].[route_id] = [db].[major_id])
            WHEN 23 THEN (SELECT TOP 1 [ft].[name]    FROM [sys].[fulltext_catalogs] [ft] WITH (NOLOCK) WHERE [ft].[fulltext_catalog_id] = [db].[major_id])
            WHEN 24 THEN (SELECT TOP 1 [sk].[name]    FROM [sys].[symmetric_keys] [sk] WITH (NOLOCK) WHERE [sk].[symmetric_key_id] = [db].[major_id])
            WHEN 25 THEN (SELECT TOP 1 [cert].[name]  FROM [sys].[certificates] [cert] WITH (NOLOCK) WHERE [cert].[certificate_id] = [db].[major_id])
            WHEN 26 THEN (SELECT TOP 1 [ak].[name]    FROM [sys].[asymmetric_keys] [ak] WITH (NOLOCK) WHERE [ak].[asymmetric_key_id] = [db].[major_id])
            WHEN 29 THEN (SELECT TOP 1 [ftsl].[name]    FROM [sys].[fulltext_stoplists] [ftsl] WITH (NOLOCK) WHERE [ftsl].[stoplist_id] = [db].[major_id])
WHEN 31 THEN (SELECT TOP 1 [spl].[name] FROM [sys].[registered_search_property_lists] [spl] WITH (NOLOCK) WHERE [spl].[property_list_id] = [db].[major_id])
        END AS [Level0Name],
        CASE [db].[class]
            WHEN 6  THEN (SELECT TOP 1 [st2].[name] FROM [sys].[types] [st2] WITH (NOLOCK) WHERE [st2].[user_type_id] = [db].[major_id])
            WHEN 10 THEN (SELECT TOP 1 [sx2].[name] FROM [sys].[xml_schema_collections] [sx2] WITH (NOLOCK) WHERE [sx2].[xml_collection_id] = [db].[major_id])
        END AS [Level1Name],
        NULL AS [Level2Name],
        [dp].[type] AS [DataBasePrincipalType]
FROM
        [sys].[database_permissions] [db] WITH (NOLOCK)
        LEFT JOIN [sys].[database_principals] as [dp] WITH (NOLOCK) ON [db].[major_id] = [dp].[principal_id] AND [db].[class] = 4
        INNER JOIN [sys].[database_principals] [dp2] WITH (NOLOCK) ON [dp2].[principal_id] = [db].[grantor_principal_id]
        INNER JOIN [sys].[database_principals] [dp3] WITH (NOLOCK) ON [dp3].[principal_id] = [db].[grantee_principal_id]
WHERE
        [db].[class] <> 1
        AND [db].[state] <> N'R'
        AND [db].[grantee_principal_id] <> [db].[grantor_principal_id]
		AND USER_NAME([db].[grantor_principal_id]) <> N'cdc'
		AND USER_NAME([db].[grantee_principal_id]) <> N'cdc'
        AND ([dp2].[owning_principal_id] IS NULL OR USER_NAME([dp2].[owning_principal_id]) <> N'cdc')
        AND ([dp3].[owning_principal_id] IS NULL OR USER_NAME([dp3].[owning_principal_id]) <> N'cdc')) AS [_results] WHERE [TypeId] = 8
 ORDER BY TypeId,GranteeId,GrantorId,MajorId,MinorId,PermissionCode,PermissionState ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90DatabasePermissionStatementPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [db].[class] AS [TypeId],
        [db].[major_id] AS [MajorId],
        [db].[minor_id] AS [MinorId],
        NULL AS [ObjectType],
        CONVERT(bit, 0) AS [IsMsShipped],
        [db].[grantee_principal_id] AS [GranteeId],
        USER_NAME([db].[grantee_principal_id]) AS [GranteeName],
        [db].[grantor_principal_id] AS [GrantorId],
        CONVERT(BIT, CASE WHEN ([dp].[owning_principal_id] IS NULL OR [dp].[owning_principal_id] <> [db].[grantor_principal_id]) AND ([dp].[is_fixed_role] <> 1 AND [dp].[principal_id] <> 0) THEN 1 ELSE 0 END) AS [ScriptGrantor],
        USER_NAME([db].[grantor_principal_id]) AS [GrantorName],
        [db].[type] AS [PermissionCode],
        [db].[permission_name] AS [PermissionType],
        [db].[state] AS [PermissionState],
        CASE [db].[class]
            WHEN  0 THEN NULL
            WHEN  3 THEN SCHEMA_NAME([db].[major_id])
            WHEN  4 THEN USER_NAME([db].[major_id])
            WHEN  5 THEN (SELECT TOP 1 [sa2].[name]   FROM [sys].[assemblies] [sa2] WITH (NOLOCK) WHERE [sa2].[assembly_id] = [db].[major_id])
            WHEN  6 THEN SCHEMA_NAME((SELECT TOP 1 [st].[schema_id] FROM [sys].[types] [st] WITH (NOLOCK) WHERE [st].[user_type_id] = [db].[major_id]))
            WHEN 10 THEN SCHEMA_NAME((SELECT TOP 1 [sxsc2].[schema_id] FROM [sys].[xml_schema_collections] [sxsc2] WITH (NOLOCK) WHERE [sxsc2].[xml_collection_id] = [db].[major_id]))
            WHEN 15 THEN (SELECT TOP 1 [smt2].[name]  FROM [sys].[service_message_types] [smt2] WITH (NOLOCK) WHERE [smt2].[message_type_id] = [db].[major_id]) COLLATE database_default
            WHEN 16 THEN (SELECT TOP 1 [ssc2].[name]  FROM [sys].[service_contracts] [ssc2] WITH (NOLOCK) WHERE [ssc2].[service_contract_id] = [db].[major_id])
            WHEN 17 THEN (SELECT TOP 1 [ss2].[name]   FROM [sys].[services] [ss2] WITH (NOLOCK) WHERE [ss2].[service_id] = [db].[major_id])
            WHEN 18 THEN (SELECT TOP 1 [srs2].[name]  FROM [sys].[remote_service_bindings] [srs2] WITH (NOLOCK) WHERE [srs2].[remote_service_binding_id] = [db].[major_id])
            WHEN 19 THEN (SELECT TOP 1 [sr2].[name]   FROM [sys].[routes] [sr2] WITH (NOLOCK) WHERE [sr2].[route_id] = [db].[major_id])
            WHEN 23 THEN (SELECT TOP 1 [ft].[name]    FROM [sys].[fulltext_catalogs] [ft] WITH (NOLOCK) WHERE [ft].[fulltext_catalog_id] = [db].[major_id])
            WHEN 24 THEN (SELECT TOP 1 [sk].[name]    FROM [sys].[symmetric_keys] [sk] WITH (NOLOCK) WHERE [sk].[symmetric_key_id] = [db].[major_id])
            WHEN 25 THEN (SELECT TOP 1 [cert].[name]  FROM [sys].[certificates] [cert] WITH (NOLOCK) WHERE [cert].[certificate_id] = [db].[major_id])
            WHEN 26 THEN (SELECT TOP 1 [ak].[name]    FROM [sys].[asymmetric_keys] [ak] WITH (NOLOCK) WHERE [ak].[asymmetric_key_id] = [db].[major_id])
            WHEN 29 THEN (SELECT TOP 1 [ftsl].[name]    FROM [sys].[fulltext_stoplists] [ftsl] WITH (NOLOCK) WHERE [ftsl].[stoplist_id] = [db].[major_id])
WHEN 31 THEN (SELECT TOP 1 [spl].[name] FROM [sys].[registered_search_property_lists] [spl] WITH (NOLOCK) WHERE [spl].[property_list_id] = [db].[major_id])
        END AS [Level0Name],
        CASE [db].[class]
            WHEN 6  THEN (SELECT TOP 1 [st2].[name] FROM [sys].[types] [st2] WITH (NOLOCK) WHERE [st2].[user_type_id] = [db].[major_id])
            WHEN 10 THEN (SELECT TOP 1 [sx2].[name] FROM [sys].[xml_schema_collections] [sx2] WITH (NOLOCK) WHERE [sx2].[xml_collection_id] = [db].[major_id])
        END AS [Level1Name],
        NULL AS [Level2Name],
        [dp].[type] AS [DataBasePrincipalType]
FROM
        [sys].[database_permissions] [db] WITH (NOLOCK)
        LEFT JOIN [sys].[database_principals] as [dp] WITH (NOLOCK) ON [db].[major_id] = [dp].[principal_id] AND [db].[class] = 4
        INNER JOIN [sys].[database_principals] [dp2] WITH (NOLOCK) ON [dp2].[principal_id] = [db].[grantor_principal_id]
        INNER JOIN [sys].[database_principals] [dp3] WITH (NOLOCK) ON [dp3].[principal_id] = [db].[grantee_principal_id]
WHERE
        [db].[class] <> 1
        AND [db].[state] <> N'R'
        AND [db].[grantee_principal_id] <> [db].[grantor_principal_id]
		AND USER_NAME([db].[grantor_principal_id]) <> N'cdc'
		AND USER_NAME([db].[grantee_principal_id]) <> N'cdc'
        AND ([dp2].[owning_principal_id] IS NULL OR USER_NAME([dp2].[owning_principal_id]) <> N'cdc')
        AND ([dp3].[owning_principal_id] IS NULL OR USER_NAME([dp3].[owning_principal_id]) <> N'cdc')) AS [_results] WHERE [TypeId] = 10
 ORDER BY TypeId,GranteeId,GrantorId,MajorId,MinorId,PermissionCode,PermissionState ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90DatabasePermissionStatementPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [db].[class] AS [TypeId],
        [db].[major_id] AS [MajorId],
        [db].[minor_id] AS [MinorId],
        NULL AS [ObjectType],
        CONVERT(bit, 0) AS [IsMsShipped],
        [db].[grantee_principal_id] AS [GranteeId],
        USER_NAME([db].[grantee_principal_id]) AS [GranteeName],
        [db].[grantor_principal_id] AS [GrantorId],
        CONVERT(BIT, CASE WHEN ([dp].[owning_principal_id] IS NULL OR [dp].[owning_principal_id] <> [db].[grantor_principal_id]) AND ([dp].[is_fixed_role] <> 1 AND [dp].[principal_id] <> 0) THEN 1 ELSE 0 END) AS [ScriptGrantor],
        USER_NAME([db].[grantor_principal_id]) AS [GrantorName],
        [db].[type] AS [PermissionCode],
        [db].[permission_name] AS [PermissionType],
        [db].[state] AS [PermissionState],
        CASE [db].[class]
            WHEN  0 THEN NULL
            WHEN  3 THEN SCHEMA_NAME([db].[major_id])
            WHEN  4 THEN USER_NAME([db].[major_id])
            WHEN  5 THEN (SELECT TOP 1 [sa2].[name]   FROM [sys].[assemblies] [sa2] WITH (NOLOCK) WHERE [sa2].[assembly_id] = [db].[major_id])
            WHEN  6 THEN SCHEMA_NAME((SELECT TOP 1 [st].[schema_id] FROM [sys].[types] [st] WITH (NOLOCK) WHERE [st].[user_type_id] = [db].[major_id]))
            WHEN 10 THEN SCHEMA_NAME((SELECT TOP 1 [sxsc2].[schema_id] FROM [sys].[xml_schema_collections] [sxsc2] WITH (NOLOCK) WHERE [sxsc2].[xml_collection_id] = [db].[major_id]))
            WHEN 15 THEN (SELECT TOP 1 [smt2].[name]  FROM [sys].[service_message_types] [smt2] WITH (NOLOCK) WHERE [smt2].[message_type_id] = [db].[major_id]) COLLATE database_default
            WHEN 16 THEN (SELECT TOP 1 [ssc2].[name]  FROM [sys].[service_contracts] [ssc2] WITH (NOLOCK) WHERE [ssc2].[service_contract_id] = [db].[major_id])
            WHEN 17 THEN (SELECT TOP 1 [ss2].[name]   FROM [sys].[services] [ss2] WITH (NOLOCK) WHERE [ss2].[service_id] = [db].[major_id])
            WHEN 18 THEN (SELECT TOP 1 [srs2].[name]  FROM [sys].[remote_service_bindings] [srs2] WITH (NOLOCK) WHERE [srs2].[remote_service_binding_id] = [db].[major_id])
            WHEN 19 THEN (SELECT TOP 1 [sr2].[name]   FROM [sys].[routes] [sr2] WITH (NOLOCK) WHERE [sr2].[route_id] = [db].[major_id])
            WHEN 23 THEN (SELECT TOP 1 [ft].[name]    FROM [sys].[fulltext_catalogs] [ft] WITH (NOLOCK) WHERE [ft].[fulltext_catalog_id] = [db].[major_id])
            WHEN 24 THEN (SELECT TOP 1 [sk].[name]    FROM [sys].[symmetric_keys] [sk] WITH (NOLOCK) WHERE [sk].[symmetric_key_id] = [db].[major_id])
            WHEN 25 THEN (SELECT TOP 1 [cert].[name]  FROM [sys].[certificates] [cert] WITH (NOLOCK) WHERE [cert].[certificate_id] = [db].[major_id])
            WHEN 26 THEN (SELECT TOP 1 [ak].[name]    FROM [sys].[asymmetric_keys] [ak] WITH (NOLOCK) WHERE [ak].[asymmetric_key_id] = [db].[major_id])
            WHEN 29 THEN (SELECT TOP 1 [ftsl].[name]    FROM [sys].[fulltext_stoplists] [ftsl] WITH (NOLOCK) WHERE [ftsl].[stoplist_id] = [db].[major_id])
WHEN 31 THEN (SELECT TOP 1 [spl].[name] FROM [sys].[registered_search_property_lists] [spl] WITH (NOLOCK) WHERE [spl].[property_list_id] = [db].[major_id])
        END AS [Level0Name],
        CASE [db].[class]
            WHEN 6  THEN (SELECT TOP 1 [st2].[name] FROM [sys].[types] [st2] WITH (NOLOCK) WHERE [st2].[user_type_id] = [db].[major_id])
            WHEN 10 THEN (SELECT TOP 1 [sx2].[name] FROM [sys].[xml_schema_collections] [sx2] WITH (NOLOCK) WHERE [sx2].[xml_collection_id] = [db].[major_id])
        END AS [Level1Name],
        NULL AS [Level2Name],
        [dp].[type] AS [DataBasePrincipalType]
FROM
        [sys].[database_permissions] [db] WITH (NOLOCK)
        LEFT JOIN [sys].[database_principals] as [dp] WITH (NOLOCK) ON [db].[major_id] = [dp].[principal_id] AND [db].[class] = 4
        INNER JOIN [sys].[database_principals] [dp2] WITH (NOLOCK) ON [dp2].[principal_id] = [db].[grantor_principal_id]
        INNER JOIN [sys].[database_principals] [dp3] WITH (NOLOCK) ON [dp3].[principal_id] = [db].[grantee_principal_id]
WHERE
        [db].[class] <> 1
        AND [db].[state] <> N'R'
        AND [db].[grantee_principal_id] <> [db].[grantor_principal_id]
		AND USER_NAME([db].[grantor_principal_id]) <> N'cdc'
		AND USER_NAME([db].[grantee_principal_id]) <> N'cdc'
        AND ([dp2].[owning_principal_id] IS NULL OR USER_NAME([dp2].[owning_principal_id]) <> N'cdc')
        AND ([dp3].[owning_principal_id] IS NULL OR USER_NAME([dp3].[owning_principal_id]) <> N'cdc')) AS [_results] WHERE [TypeId] = 15 AND [Level2Name] IS NULL AND [Level1Name] IS NULL
 ORDER BY TypeId,GranteeId,GrantorId,MajorId,MinorId,PermissionCode,PermissionState ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90DatabasePermissionStatementPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [db].[class] AS [TypeId],
        [db].[major_id] AS [MajorId],
        [db].[minor_id] AS [MinorId],
        NULL AS [ObjectType],
        CONVERT(bit, 0) AS [IsMsShipped],
        [db].[grantee_principal_id] AS [GranteeId],
        USER_NAME([db].[grantee_principal_id]) AS [GranteeName],
        [db].[grantor_principal_id] AS [GrantorId],
        CONVERT(BIT, CASE WHEN ([dp].[owning_principal_id] IS NULL OR [dp].[owning_principal_id] <> [db].[grantor_principal_id]) AND ([dp].[is_fixed_role] <> 1 AND [dp].[principal_id] <> 0) THEN 1 ELSE 0 END) AS [ScriptGrantor],
        USER_NAME([db].[grantor_principal_id]) AS [GrantorName],
        [db].[type] AS [PermissionCode],
        [db].[permission_name] AS [PermissionType],
        [db].[state] AS [PermissionState],
        CASE [db].[class]
            WHEN  0 THEN NULL
            WHEN  3 THEN SCHEMA_NAME([db].[major_id])
            WHEN  4 THEN USER_NAME([db].[major_id])
            WHEN  5 THEN (SELECT TOP 1 [sa2].[name]   FROM [sys].[assemblies] [sa2] WITH (NOLOCK) WHERE [sa2].[assembly_id] = [db].[major_id])
            WHEN  6 THEN SCHEMA_NAME((SELECT TOP 1 [st].[schema_id] FROM [sys].[types] [st] WITH (NOLOCK) WHERE [st].[user_type_id] = [db].[major_id]))
            WHEN 10 THEN SCHEMA_NAME((SELECT TOP 1 [sxsc2].[schema_id] FROM [sys].[xml_schema_collections] [sxsc2] WITH (NOLOCK) WHERE [sxsc2].[xml_collection_id] = [db].[major_id]))
            WHEN 15 THEN (SELECT TOP 1 [smt2].[name]  FROM [sys].[service_message_types] [smt2] WITH (NOLOCK) WHERE [smt2].[message_type_id] = [db].[major_id]) COLLATE database_default
            WHEN 16 THEN (SELECT TOP 1 [ssc2].[name]  FROM [sys].[service_contracts] [ssc2] WITH (NOLOCK) WHERE [ssc2].[service_contract_id] = [db].[major_id])
            WHEN 17 THEN (SELECT TOP 1 [ss2].[name]   FROM [sys].[services] [ss2] WITH (NOLOCK) WHERE [ss2].[service_id] = [db].[major_id])
            WHEN 18 THEN (SELECT TOP 1 [srs2].[name]  FROM [sys].[remote_service_bindings] [srs2] WITH (NOLOCK) WHERE [srs2].[remote_service_binding_id] = [db].[major_id])
            WHEN 19 THEN (SELECT TOP 1 [sr2].[name]   FROM [sys].[routes] [sr2] WITH (NOLOCK) WHERE [sr2].[route_id] = [db].[major_id])
            WHEN 23 THEN (SELECT TOP 1 [ft].[name]    FROM [sys].[fulltext_catalogs] [ft] WITH (NOLOCK) WHERE [ft].[fulltext_catalog_id] = [db].[major_id])
            WHEN 24 THEN (SELECT TOP 1 [sk].[name]    FROM [sys].[symmetric_keys] [sk] WITH (NOLOCK) WHERE [sk].[symmetric_key_id] = [db].[major_id])
            WHEN 25 THEN (SELECT TOP 1 [cert].[name]  FROM [sys].[certificates] [cert] WITH (NOLOCK) WHERE [cert].[certificate_id] = [db].[major_id])
            WHEN 26 THEN (SELECT TOP 1 [ak].[name]    FROM [sys].[asymmetric_keys] [ak] WITH (NOLOCK) WHERE [ak].[asymmetric_key_id] = [db].[major_id])
            WHEN 29 THEN (SELECT TOP 1 [ftsl].[name]    FROM [sys].[fulltext_stoplists] [ftsl] WITH (NOLOCK) WHERE [ftsl].[stoplist_id] = [db].[major_id])
WHEN 31 THEN (SELECT TOP 1 [spl].[name] FROM [sys].[registered_search_property_lists] [spl] WITH (NOLOCK) WHERE [spl].[property_list_id] = [db].[major_id])
        END AS [Level0Name],
        CASE [db].[class]
            WHEN 6  THEN (SELECT TOP 1 [st2].[name] FROM [sys].[types] [st2] WITH (NOLOCK) WHERE [st2].[user_type_id] = [db].[major_id])
            WHEN 10 THEN (SELECT TOP 1 [sx2].[name] FROM [sys].[xml_schema_collections] [sx2] WITH (NOLOCK) WHERE [sx2].[xml_collection_id] = [db].[major_id])
        END AS [Level1Name],
        NULL AS [Level2Name],
        [dp].[type] AS [DataBasePrincipalType]
FROM
        [sys].[database_permissions] [db] WITH (NOLOCK)
        LEFT JOIN [sys].[database_principals] as [dp] WITH (NOLOCK) ON [db].[major_id] = [dp].[principal_id] AND [db].[class] = 4
        INNER JOIN [sys].[database_principals] [dp2] WITH (NOLOCK) ON [dp2].[principal_id] = [db].[grantor_principal_id]
        INNER JOIN [sys].[database_principals] [dp3] WITH (NOLOCK) ON [dp3].[principal_id] = [db].[grantee_principal_id]
WHERE
        [db].[class] <> 1
        AND [db].[state] <> N'R'
        AND [db].[grantee_principal_id] <> [db].[grantor_principal_id]
		AND USER_NAME([db].[grantor_principal_id]) <> N'cdc'
		AND USER_NAME([db].[grantee_principal_id]) <> N'cdc'
        AND ([dp2].[owning_principal_id] IS NULL OR USER_NAME([dp2].[owning_principal_id]) <> N'cdc')
        AND ([dp3].[owning_principal_id] IS NULL OR USER_NAME([dp3].[owning_principal_id]) <> N'cdc')) AS [_results] WHERE [TypeId] = 16 AND [Level2Name] IS NULL AND [Level1Name] IS NULL
 ORDER BY TypeId,GranteeId,GrantorId,MajorId,MinorId,PermissionCode,PermissionState ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90DatabasePermissionStatementPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [db].[class] AS [TypeId],
        [db].[major_id] AS [MajorId],
        [db].[minor_id] AS [MinorId],
        NULL AS [ObjectType],
        CONVERT(bit, 0) AS [IsMsShipped],
        [db].[grantee_principal_id] AS [GranteeId],
        USER_NAME([db].[grantee_principal_id]) AS [GranteeName],
        [db].[grantor_principal_id] AS [GrantorId],
        CONVERT(BIT, CASE WHEN ([dp].[owning_principal_id] IS NULL OR [dp].[owning_principal_id] <> [db].[grantor_principal_id]) AND ([dp].[is_fixed_role] <> 1 AND [dp].[principal_id] <> 0) THEN 1 ELSE 0 END) AS [ScriptGrantor],
        USER_NAME([db].[grantor_principal_id]) AS [GrantorName],
        [db].[type] AS [PermissionCode],
        [db].[permission_name] AS [PermissionType],
        [db].[state] AS [PermissionState],
        CASE [db].[class]
            WHEN  0 THEN NULL
            WHEN  3 THEN SCHEMA_NAME([db].[major_id])
            WHEN  4 THEN USER_NAME([db].[major_id])
            WHEN  5 THEN (SELECT TOP 1 [sa2].[name]   FROM [sys].[assemblies] [sa2] WITH (NOLOCK) WHERE [sa2].[assembly_id] = [db].[major_id])
            WHEN  6 THEN SCHEMA_NAME((SELECT TOP 1 [st].[schema_id] FROM [sys].[types] [st] WITH (NOLOCK) WHERE [st].[user_type_id] = [db].[major_id]))
            WHEN 10 THEN SCHEMA_NAME((SELECT TOP 1 [sxsc2].[schema_id] FROM [sys].[xml_schema_collections] [sxsc2] WITH (NOLOCK) WHERE [sxsc2].[xml_collection_id] = [db].[major_id]))
            WHEN 15 THEN (SELECT TOP 1 [smt2].[name]  FROM [sys].[service_message_types] [smt2] WITH (NOLOCK) WHERE [smt2].[message_type_id] = [db].[major_id]) COLLATE database_default
            WHEN 16 THEN (SELECT TOP 1 [ssc2].[name]  FROM [sys].[service_contracts] [ssc2] WITH (NOLOCK) WHERE [ssc2].[service_contract_id] = [db].[major_id])
            WHEN 17 THEN (SELECT TOP 1 [ss2].[name]   FROM [sys].[services] [ss2] WITH (NOLOCK) WHERE [ss2].[service_id] = [db].[major_id])
            WHEN 18 THEN (SELECT TOP 1 [srs2].[name]  FROM [sys].[remote_service_bindings] [srs2] WITH (NOLOCK) WHERE [srs2].[remote_service_binding_id] = [db].[major_id])
            WHEN 19 THEN (SELECT TOP 1 [sr2].[name]   FROM [sys].[routes] [sr2] WITH (NOLOCK) WHERE [sr2].[route_id] = [db].[major_id])
            WHEN 23 THEN (SELECT TOP 1 [ft].[name]    FROM [sys].[fulltext_catalogs] [ft] WITH (NOLOCK) WHERE [ft].[fulltext_catalog_id] = [db].[major_id])
            WHEN 24 THEN (SELECT TOP 1 [sk].[name]    FROM [sys].[symmetric_keys] [sk] WITH (NOLOCK) WHERE [sk].[symmetric_key_id] = [db].[major_id])
            WHEN 25 THEN (SELECT TOP 1 [cert].[name]  FROM [sys].[certificates] [cert] WITH (NOLOCK) WHERE [cert].[certificate_id] = [db].[major_id])
            WHEN 26 THEN (SELECT TOP 1 [ak].[name]    FROM [sys].[asymmetric_keys] [ak] WITH (NOLOCK) WHERE [ak].[asymmetric_key_id] = [db].[major_id])
            WHEN 29 THEN (SELECT TOP 1 [ftsl].[name]    FROM [sys].[fulltext_stoplists] [ftsl] WITH (NOLOCK) WHERE [ftsl].[stoplist_id] = [db].[major_id])
WHEN 31 THEN (SELECT TOP 1 [spl].[name] FROM [sys].[registered_search_property_lists] [spl] WITH (NOLOCK) WHERE [spl].[property_list_id] = [db].[major_id])
        END AS [Level0Name],
        CASE [db].[class]
            WHEN 6  THEN (SELECT TOP 1 [st2].[name] FROM [sys].[types] [st2] WITH (NOLOCK) WHERE [st2].[user_type_id] = [db].[major_id])
            WHEN 10 THEN (SELECT TOP 1 [sx2].[name] FROM [sys].[xml_schema_collections] [sx2] WITH (NOLOCK) WHERE [sx2].[xml_collection_id] = [db].[major_id])
        END AS [Level1Name],
        NULL AS [Level2Name],
        [dp].[type] AS [DataBasePrincipalType]
FROM
        [sys].[database_permissions] [db] WITH (NOLOCK)
        LEFT JOIN [sys].[database_principals] as [dp] WITH (NOLOCK) ON [db].[major_id] = [dp].[principal_id] AND [db].[class] = 4
        INNER JOIN [sys].[database_principals] [dp2] WITH (NOLOCK) ON [dp2].[principal_id] = [db].[grantor_principal_id]
        INNER JOIN [sys].[database_principals] [dp3] WITH (NOLOCK) ON [dp3].[principal_id] = [db].[grantee_principal_id]
WHERE
        [db].[class] <> 1
        AND [db].[state] <> N'R'
        AND [db].[grantee_principal_id] <> [db].[grantor_principal_id]
		AND USER_NAME([db].[grantor_principal_id]) <> N'cdc'
		AND USER_NAME([db].[grantee_principal_id]) <> N'cdc'
        AND ([dp2].[owning_principal_id] IS NULL OR USER_NAME([dp2].[owning_principal_id]) <> N'cdc')
        AND ([dp3].[owning_principal_id] IS NULL OR USER_NAME([dp3].[owning_principal_id]) <> N'cdc')) AS [_results] WHERE [TypeId] = 17 AND [Level2Name] IS NULL AND [Level1Name] IS NULL
 ORDER BY TypeId,GranteeId,GrantorId,MajorId,MinorId,PermissionCode,PermissionState ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90DatabasePermissionStatementPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [db].[class] AS [TypeId],
        [db].[major_id] AS [MajorId],
        [db].[minor_id] AS [MinorId],
        NULL AS [ObjectType],
        CONVERT(bit, 0) AS [IsMsShipped],
        [db].[grantee_principal_id] AS [GranteeId],
        USER_NAME([db].[grantee_principal_id]) AS [GranteeName],
        [db].[grantor_principal_id] AS [GrantorId],
        CONVERT(BIT, CASE WHEN ([dp].[owning_principal_id] IS NULL OR [dp].[owning_principal_id] <> [db].[grantor_principal_id]) AND ([dp].[is_fixed_role] <> 1 AND [dp].[principal_id] <> 0) THEN 1 ELSE 0 END) AS [ScriptGrantor],
        USER_NAME([db].[grantor_principal_id]) AS [GrantorName],
        [db].[type] AS [PermissionCode],
        [db].[permission_name] AS [PermissionType],
        [db].[state] AS [PermissionState],
        CASE [db].[class]
            WHEN  0 THEN NULL
            WHEN  3 THEN SCHEMA_NAME([db].[major_id])
            WHEN  4 THEN USER_NAME([db].[major_id])
            WHEN  5 THEN (SELECT TOP 1 [sa2].[name]   FROM [sys].[assemblies] [sa2] WITH (NOLOCK) WHERE [sa2].[assembly_id] = [db].[major_id])
            WHEN  6 THEN SCHEMA_NAME((SELECT TOP 1 [st].[schema_id] FROM [sys].[types] [st] WITH (NOLOCK) WHERE [st].[user_type_id] = [db].[major_id]))
            WHEN 10 THEN SCHEMA_NAME((SELECT TOP 1 [sxsc2].[schema_id] FROM [sys].[xml_schema_collections] [sxsc2] WITH (NOLOCK) WHERE [sxsc2].[xml_collection_id] = [db].[major_id]))
            WHEN 15 THEN (SELECT TOP 1 [smt2].[name]  FROM [sys].[service_message_types] [smt2] WITH (NOLOCK) WHERE [smt2].[message_type_id] = [db].[major_id]) COLLATE database_default
            WHEN 16 THEN (SELECT TOP 1 [ssc2].[name]  FROM [sys].[service_contracts] [ssc2] WITH (NOLOCK) WHERE [ssc2].[service_contract_id] = [db].[major_id])
            WHEN 17 THEN (SELECT TOP 1 [ss2].[name]   FROM [sys].[services] [ss2] WITH (NOLOCK) WHERE [ss2].[service_id] = [db].[major_id])
            WHEN 18 THEN (SELECT TOP 1 [srs2].[name]  FROM [sys].[remote_service_bindings] [srs2] WITH (NOLOCK) WHERE [srs2].[remote_service_binding_id] = [db].[major_id])
            WHEN 19 THEN (SELECT TOP 1 [sr2].[name]   FROM [sys].[routes] [sr2] WITH (NOLOCK) WHERE [sr2].[route_id] = [db].[major_id])
            WHEN 23 THEN (SELECT TOP 1 [ft].[name]    FROM [sys].[fulltext_catalogs] [ft] WITH (NOLOCK) WHERE [ft].[fulltext_catalog_id] = [db].[major_id])
            WHEN 24 THEN (SELECT TOP 1 [sk].[name]    FROM [sys].[symmetric_keys] [sk] WITH (NOLOCK) WHERE [sk].[symmetric_key_id] = [db].[major_id])
            WHEN 25 THEN (SELECT TOP 1 [cert].[name]  FROM [sys].[certificates] [cert] WITH (NOLOCK) WHERE [cert].[certificate_id] = [db].[major_id])
            WHEN 26 THEN (SELECT TOP 1 [ak].[name]    FROM [sys].[asymmetric_keys] [ak] WITH (NOLOCK) WHERE [ak].[asymmetric_key_id] = [db].[major_id])
            WHEN 29 THEN (SELECT TOP 1 [ftsl].[name]    FROM [sys].[fulltext_stoplists] [ftsl] WITH (NOLOCK) WHERE [ftsl].[stoplist_id] = [db].[major_id])
WHEN 31 THEN (SELECT TOP 1 [spl].[name] FROM [sys].[registered_search_property_lists] [spl] WITH (NOLOCK) WHERE [spl].[property_list_id] = [db].[major_id])
        END AS [Level0Name],
        CASE [db].[class]
            WHEN 6  THEN (SELECT TOP 1 [st2].[name] FROM [sys].[types] [st2] WITH (NOLOCK) WHERE [st2].[user_type_id] = [db].[major_id])
            WHEN 10 THEN (SELECT TOP 1 [sx2].[name] FROM [sys].[xml_schema_collections] [sx2] WITH (NOLOCK) WHERE [sx2].[xml_collection_id] = [db].[major_id])
        END AS [Level1Name],
        NULL AS [Level2Name],
        [dp].[type] AS [DataBasePrincipalType]
FROM
        [sys].[database_permissions] [db] WITH (NOLOCK)
        LEFT JOIN [sys].[database_principals] as [dp] WITH (NOLOCK) ON [db].[major_id] = [dp].[principal_id] AND [db].[class] = 4
        INNER JOIN [sys].[database_principals] [dp2] WITH (NOLOCK) ON [dp2].[principal_id] = [db].[grantor_principal_id]
        INNER JOIN [sys].[database_principals] [dp3] WITH (NOLOCK) ON [dp3].[principal_id] = [db].[grantee_principal_id]
WHERE
        [db].[class] <> 1
        AND [db].[state] <> N'R'
        AND [db].[grantee_principal_id] <> [db].[grantor_principal_id]
		AND USER_NAME([db].[grantor_principal_id]) <> N'cdc'
		AND USER_NAME([db].[grantee_principal_id]) <> N'cdc'
        AND ([dp2].[owning_principal_id] IS NULL OR USER_NAME([dp2].[owning_principal_id]) <> N'cdc')
        AND ([dp3].[owning_principal_id] IS NULL OR USER_NAME([dp3].[owning_principal_id]) <> N'cdc')) AS [_results] WHERE [TypeId] = 18 AND [Level2Name] IS NULL AND [Level1Name] IS NULL
 ORDER BY TypeId,GranteeId,GrantorId,MajorId,MinorId,PermissionCode,PermissionState ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90DatabasePermissionStatementPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [db].[class] AS [TypeId],
        [db].[major_id] AS [MajorId],
        [db].[minor_id] AS [MinorId],
        NULL AS [ObjectType],
        CONVERT(bit, 0) AS [IsMsShipped],
        [db].[grantee_principal_id] AS [GranteeId],
        USER_NAME([db].[grantee_principal_id]) AS [GranteeName],
        [db].[grantor_principal_id] AS [GrantorId],
        CONVERT(BIT, CASE WHEN ([dp].[owning_principal_id] IS NULL OR [dp].[owning_principal_id] <> [db].[grantor_principal_id]) AND ([dp].[is_fixed_role] <> 1 AND [dp].[principal_id] <> 0) THEN 1 ELSE 0 END) AS [ScriptGrantor],
        USER_NAME([db].[grantor_principal_id]) AS [GrantorName],
        [db].[type] AS [PermissionCode],
        [db].[permission_name] AS [PermissionType],
        [db].[state] AS [PermissionState],
        CASE [db].[class]
            WHEN  0 THEN NULL
            WHEN  3 THEN SCHEMA_NAME([db].[major_id])
            WHEN  4 THEN USER_NAME([db].[major_id])
            WHEN  5 THEN (SELECT TOP 1 [sa2].[name]   FROM [sys].[assemblies] [sa2] WITH (NOLOCK) WHERE [sa2].[assembly_id] = [db].[major_id])
            WHEN  6 THEN SCHEMA_NAME((SELECT TOP 1 [st].[schema_id] FROM [sys].[types] [st] WITH (NOLOCK) WHERE [st].[user_type_id] = [db].[major_id]))
            WHEN 10 THEN SCHEMA_NAME((SELECT TOP 1 [sxsc2].[schema_id] FROM [sys].[xml_schema_collections] [sxsc2] WITH (NOLOCK) WHERE [sxsc2].[xml_collection_id] = [db].[major_id]))
            WHEN 15 THEN (SELECT TOP 1 [smt2].[name]  FROM [sys].[service_message_types] [smt2] WITH (NOLOCK) WHERE [smt2].[message_type_id] = [db].[major_id]) COLLATE database_default
            WHEN 16 THEN (SELECT TOP 1 [ssc2].[name]  FROM [sys].[service_contracts] [ssc2] WITH (NOLOCK) WHERE [ssc2].[service_contract_id] = [db].[major_id])
            WHEN 17 THEN (SELECT TOP 1 [ss2].[name]   FROM [sys].[services] [ss2] WITH (NOLOCK) WHERE [ss2].[service_id] = [db].[major_id])
            WHEN 18 THEN (SELECT TOP 1 [srs2].[name]  FROM [sys].[remote_service_bindings] [srs2] WITH (NOLOCK) WHERE [srs2].[remote_service_binding_id] = [db].[major_id])
            WHEN 19 THEN (SELECT TOP 1 [sr2].[name]   FROM [sys].[routes] [sr2] WITH (NOLOCK) WHERE [sr2].[route_id] = [db].[major_id])
            WHEN 23 THEN (SELECT TOP 1 [ft].[name]    FROM [sys].[fulltext_catalogs] [ft] WITH (NOLOCK) WHERE [ft].[fulltext_catalog_id] = [db].[major_id])
            WHEN 24 THEN (SELECT TOP 1 [sk].[name]    FROM [sys].[symmetric_keys] [sk] WITH (NOLOCK) WHERE [sk].[symmetric_key_id] = [db].[major_id])
            WHEN 25 THEN (SELECT TOP 1 [cert].[name]  FROM [sys].[certificates] [cert] WITH (NOLOCK) WHERE [cert].[certificate_id] = [db].[major_id])
            WHEN 26 THEN (SELECT TOP 1 [ak].[name]    FROM [sys].[asymmetric_keys] [ak] WITH (NOLOCK) WHERE [ak].[asymmetric_key_id] = [db].[major_id])
            WHEN 29 THEN (SELECT TOP 1 [ftsl].[name]    FROM [sys].[fulltext_stoplists] [ftsl] WITH (NOLOCK) WHERE [ftsl].[stoplist_id] = [db].[major_id])
WHEN 31 THEN (SELECT TOP 1 [spl].[name] FROM [sys].[registered_search_property_lists] [spl] WITH (NOLOCK) WHERE [spl].[property_list_id] = [db].[major_id])
        END AS [Level0Name],
        CASE [db].[class]
            WHEN 6  THEN (SELECT TOP 1 [st2].[name] FROM [sys].[types] [st2] WITH (NOLOCK) WHERE [st2].[user_type_id] = [db].[major_id])
            WHEN 10 THEN (SELECT TOP 1 [sx2].[name] FROM [sys].[xml_schema_collections] [sx2] WITH (NOLOCK) WHERE [sx2].[xml_collection_id] = [db].[major_id])
        END AS [Level1Name],
        NULL AS [Level2Name],
        [dp].[type] AS [DataBasePrincipalType]
FROM
        [sys].[database_permissions] [db] WITH (NOLOCK)
        LEFT JOIN [sys].[database_principals] as [dp] WITH (NOLOCK) ON [db].[major_id] = [dp].[principal_id] AND [db].[class] = 4
        INNER JOIN [sys].[database_principals] [dp2] WITH (NOLOCK) ON [dp2].[principal_id] = [db].[grantor_principal_id]
        INNER JOIN [sys].[database_principals] [dp3] WITH (NOLOCK) ON [dp3].[principal_id] = [db].[grantee_principal_id]
WHERE
        [db].[class] <> 1
        AND [db].[state] <> N'R'
        AND [db].[grantee_principal_id] <> [db].[grantor_principal_id]
		AND USER_NAME([db].[grantor_principal_id]) <> N'cdc'
		AND USER_NAME([db].[grantee_principal_id]) <> N'cdc'
        AND ([dp2].[owning_principal_id] IS NULL OR USER_NAME([dp2].[owning_principal_id]) <> N'cdc')
        AND ([dp3].[owning_principal_id] IS NULL OR USER_NAME([dp3].[owning_principal_id]) <> N'cdc')) AS [_results] WHERE [TypeId] = 19 AND [Level2Name] IS NULL AND [Level1Name] IS NULL
 ORDER BY TypeId,GranteeId,GrantorId,MajorId,MinorId,PermissionCode,PermissionState ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90DatabasePermissionStatementPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [db].[class] AS [TypeId],
        [db].[major_id] AS [MajorId],
        [db].[minor_id] AS [MinorId],
        NULL AS [ObjectType],
        CONVERT(bit, 0) AS [IsMsShipped],
        [db].[grantee_principal_id] AS [GranteeId],
        USER_NAME([db].[grantee_principal_id]) AS [GranteeName],
        [db].[grantor_principal_id] AS [GrantorId],
        CONVERT(BIT, CASE WHEN ([dp].[owning_principal_id] IS NULL OR [dp].[owning_principal_id] <> [db].[grantor_principal_id]) AND ([dp].[is_fixed_role] <> 1 AND [dp].[principal_id] <> 0) THEN 1 ELSE 0 END) AS [ScriptGrantor],
        USER_NAME([db].[grantor_principal_id]) AS [GrantorName],
        [db].[type] AS [PermissionCode],
        [db].[permission_name] AS [PermissionType],
        [db].[state] AS [PermissionState],
        CASE [db].[class]
            WHEN  0 THEN NULL
            WHEN  3 THEN SCHEMA_NAME([db].[major_id])
            WHEN  4 THEN USER_NAME([db].[major_id])
            WHEN  5 THEN (SELECT TOP 1 [sa2].[name]   FROM [sys].[assemblies] [sa2] WITH (NOLOCK) WHERE [sa2].[assembly_id] = [db].[major_id])
            WHEN  6 THEN SCHEMA_NAME((SELECT TOP 1 [st].[schema_id] FROM [sys].[types] [st] WITH (NOLOCK) WHERE [st].[user_type_id] = [db].[major_id]))
            WHEN 10 THEN SCHEMA_NAME((SELECT TOP 1 [sxsc2].[schema_id] FROM [sys].[xml_schema_collections] [sxsc2] WITH (NOLOCK) WHERE [sxsc2].[xml_collection_id] = [db].[major_id]))
            WHEN 15 THEN (SELECT TOP 1 [smt2].[name]  FROM [sys].[service_message_types] [smt2] WITH (NOLOCK) WHERE [smt2].[message_type_id] = [db].[major_id]) COLLATE database_default
            WHEN 16 THEN (SELECT TOP 1 [ssc2].[name]  FROM [sys].[service_contracts] [ssc2] WITH (NOLOCK) WHERE [ssc2].[service_contract_id] = [db].[major_id])
            WHEN 17 THEN (SELECT TOP 1 [ss2].[name]   FROM [sys].[services] [ss2] WITH (NOLOCK) WHERE [ss2].[service_id] = [db].[major_id])
            WHEN 18 THEN (SELECT TOP 1 [srs2].[name]  FROM [sys].[remote_service_bindings] [srs2] WITH (NOLOCK) WHERE [srs2].[remote_service_binding_id] = [db].[major_id])
            WHEN 19 THEN (SELECT TOP 1 [sr2].[name]   FROM [sys].[routes] [sr2] WITH (NOLOCK) WHERE [sr2].[route_id] = [db].[major_id])
            WHEN 23 THEN (SELECT TOP 1 [ft].[name]    FROM [sys].[fulltext_catalogs] [ft] WITH (NOLOCK) WHERE [ft].[fulltext_catalog_id] = [db].[major_id])
            WHEN 24 THEN (SELECT TOP 1 [sk].[name]    FROM [sys].[symmetric_keys] [sk] WITH (NOLOCK) WHERE [sk].[symmetric_key_id] = [db].[major_id])
            WHEN 25 THEN (SELECT TOP 1 [cert].[name]  FROM [sys].[certificates] [cert] WITH (NOLOCK) WHERE [cert].[certificate_id] = [db].[major_id])
            WHEN 26 THEN (SELECT TOP 1 [ak].[name]    FROM [sys].[asymmetric_keys] [ak] WITH (NOLOCK) WHERE [ak].[asymmetric_key_id] = [db].[major_id])
            WHEN 29 THEN (SELECT TOP 1 [ftsl].[name]    FROM [sys].[fulltext_stoplists] [ftsl] WITH (NOLOCK) WHERE [ftsl].[stoplist_id] = [db].[major_id])
WHEN 31 THEN (SELECT TOP 1 [spl].[name] FROM [sys].[registered_search_property_lists] [spl] WITH (NOLOCK) WHERE [spl].[property_list_id] = [db].[major_id])
        END AS [Level0Name],
        CASE [db].[class]
            WHEN 6  THEN (SELECT TOP 1 [st2].[name] FROM [sys].[types] [st2] WITH (NOLOCK) WHERE [st2].[user_type_id] = [db].[major_id])
            WHEN 10 THEN (SELECT TOP 1 [sx2].[name] FROM [sys].[xml_schema_collections] [sx2] WITH (NOLOCK) WHERE [sx2].[xml_collection_id] = [db].[major_id])
        END AS [Level1Name],
        NULL AS [Level2Name],
        [dp].[type] AS [DataBasePrincipalType]
FROM
        [sys].[database_permissions] [db] WITH (NOLOCK)
        LEFT JOIN [sys].[database_principals] as [dp] WITH (NOLOCK) ON [db].[major_id] = [dp].[principal_id] AND [db].[class] = 4
        INNER JOIN [sys].[database_principals] [dp2] WITH (NOLOCK) ON [dp2].[principal_id] = [db].[grantor_principal_id]
        INNER JOIN [sys].[database_principals] [dp3] WITH (NOLOCK) ON [dp3].[principal_id] = [db].[grantee_principal_id]
WHERE
        [db].[class] <> 1
        AND [db].[state] <> N'R'
        AND [db].[grantee_principal_id] <> [db].[grantor_principal_id]
		AND USER_NAME([db].[grantor_principal_id]) <> N'cdc'
		AND USER_NAME([db].[grantee_principal_id]) <> N'cdc'
        AND ([dp2].[owning_principal_id] IS NULL OR USER_NAME([dp2].[owning_principal_id]) <> N'cdc')
        AND ([dp3].[owning_principal_id] IS NULL OR USER_NAME([dp3].[owning_principal_id]) <> N'cdc')) AS [_results] WHERE [TypeId] = 20 AND [Level2Name] IS NULL AND [Level1Name] IS NULL
 ORDER BY TypeId,GranteeId,GrantorId,MajorId,MinorId,PermissionCode,PermissionState ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90DatabasePermissionStatementPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [db].[class] AS [TypeId],
        [db].[major_id] AS [MajorId],
        [db].[minor_id] AS [MinorId],
        NULL AS [ObjectType],
        CONVERT(bit, 0) AS [IsMsShipped],
        [db].[grantee_principal_id] AS [GranteeId],
        USER_NAME([db].[grantee_principal_id]) AS [GranteeName],
        [db].[grantor_principal_id] AS [GrantorId],
        CONVERT(BIT, CASE WHEN ([dp].[owning_principal_id] IS NULL OR [dp].[owning_principal_id] <> [db].[grantor_principal_id]) AND ([dp].[is_fixed_role] <> 1 AND [dp].[principal_id] <> 0) THEN 1 ELSE 0 END) AS [ScriptGrantor],
        USER_NAME([db].[grantor_principal_id]) AS [GrantorName],
        [db].[type] AS [PermissionCode],
        [db].[permission_name] AS [PermissionType],
        [db].[state] AS [PermissionState],
        CASE [db].[class]
            WHEN  0 THEN NULL
            WHEN  3 THEN SCHEMA_NAME([db].[major_id])
            WHEN  4 THEN USER_NAME([db].[major_id])
            WHEN  5 THEN (SELECT TOP 1 [sa2].[name]   FROM [sys].[assemblies] [sa2] WITH (NOLOCK) WHERE [sa2].[assembly_id] = [db].[major_id])
            WHEN  6 THEN SCHEMA_NAME((SELECT TOP 1 [st].[schema_id] FROM [sys].[types] [st] WITH (NOLOCK) WHERE [st].[user_type_id] = [db].[major_id]))
            WHEN 10 THEN SCHEMA_NAME((SELECT TOP 1 [sxsc2].[schema_id] FROM [sys].[xml_schema_collections] [sxsc2] WITH (NOLOCK) WHERE [sxsc2].[xml_collection_id] = [db].[major_id]))
            WHEN 15 THEN (SELECT TOP 1 [smt2].[name]  FROM [sys].[service_message_types] [smt2] WITH (NOLOCK) WHERE [smt2].[message_type_id] = [db].[major_id]) COLLATE database_default
            WHEN 16 THEN (SELECT TOP 1 [ssc2].[name]  FROM [sys].[service_contracts] [ssc2] WITH (NOLOCK) WHERE [ssc2].[service_contract_id] = [db].[major_id])
            WHEN 17 THEN (SELECT TOP 1 [ss2].[name]   FROM [sys].[services] [ss2] WITH (NOLOCK) WHERE [ss2].[service_id] = [db].[major_id])
            WHEN 18 THEN (SELECT TOP 1 [srs2].[name]  FROM [sys].[remote_service_bindings] [srs2] WITH (NOLOCK) WHERE [srs2].[remote_service_binding_id] = [db].[major_id])
            WHEN 19 THEN (SELECT TOP 1 [sr2].[name]   FROM [sys].[routes] [sr2] WITH (NOLOCK) WHERE [sr2].[route_id] = [db].[major_id])
            WHEN 23 THEN (SELECT TOP 1 [ft].[name]    FROM [sys].[fulltext_catalogs] [ft] WITH (NOLOCK) WHERE [ft].[fulltext_catalog_id] = [db].[major_id])
            WHEN 24 THEN (SELECT TOP 1 [sk].[name]    FROM [sys].[symmetric_keys] [sk] WITH (NOLOCK) WHERE [sk].[symmetric_key_id] = [db].[major_id])
            WHEN 25 THEN (SELECT TOP 1 [cert].[name]  FROM [sys].[certificates] [cert] WITH (NOLOCK) WHERE [cert].[certificate_id] = [db].[major_id])
            WHEN 26 THEN (SELECT TOP 1 [ak].[name]    FROM [sys].[asymmetric_keys] [ak] WITH (NOLOCK) WHERE [ak].[asymmetric_key_id] = [db].[major_id])
            WHEN 29 THEN (SELECT TOP 1 [ftsl].[name]    FROM [sys].[fulltext_stoplists] [ftsl] WITH (NOLOCK) WHERE [ftsl].[stoplist_id] = [db].[major_id])
WHEN 31 THEN (SELECT TOP 1 [spl].[name] FROM [sys].[registered_search_property_lists] [spl] WITH (NOLOCK) WHERE [spl].[property_list_id] = [db].[major_id])
        END AS [Level0Name],
        CASE [db].[class]
            WHEN 6  THEN (SELECT TOP 1 [st2].[name] FROM [sys].[types] [st2] WITH (NOLOCK) WHERE [st2].[user_type_id] = [db].[major_id])
            WHEN 10 THEN (SELECT TOP 1 [sx2].[name] FROM [sys].[xml_schema_collections] [sx2] WITH (NOLOCK) WHERE [sx2].[xml_collection_id] = [db].[major_id])
        END AS [Level1Name],
        NULL AS [Level2Name],
        [dp].[type] AS [DataBasePrincipalType]
FROM
        [sys].[database_permissions] [db] WITH (NOLOCK)
        LEFT JOIN [sys].[database_principals] as [dp] WITH (NOLOCK) ON [db].[major_id] = [dp].[principal_id] AND [db].[class] = 4
        INNER JOIN [sys].[database_principals] [dp2] WITH (NOLOCK) ON [dp2].[principal_id] = [db].[grantor_principal_id]
        INNER JOIN [sys].[database_principals] [dp3] WITH (NOLOCK) ON [dp3].[principal_id] = [db].[grantee_principal_id]
WHERE
        [db].[class] <> 1
        AND [db].[state] <> N'R'
        AND [db].[grantee_principal_id] <> [db].[grantor_principal_id]
		AND USER_NAME([db].[grantor_principal_id]) <> N'cdc'
		AND USER_NAME([db].[grantee_principal_id]) <> N'cdc'
        AND ([dp2].[owning_principal_id] IS NULL OR USER_NAME([dp2].[owning_principal_id]) <> N'cdc')
        AND ([dp3].[owning_principal_id] IS NULL OR USER_NAME([dp3].[owning_principal_id]) <> N'cdc')) AS [_results] WHERE [TypeId] = 21 AND [Level2Name] IS NULL AND [Level1Name] IS NULL
 ORDER BY TypeId,GranteeId,GrantorId,MajorId,MinorId,PermissionCode,PermissionState ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90DatabasePermissionStatementPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [db].[class] AS [TypeId],
        [db].[major_id] AS [MajorId],
        [db].[minor_id] AS [MinorId],
        NULL AS [ObjectType],
        CONVERT(bit, 0) AS [IsMsShipped],
        [db].[grantee_principal_id] AS [GranteeId],
        USER_NAME([db].[grantee_principal_id]) AS [GranteeName],
        [db].[grantor_principal_id] AS [GrantorId],
        CONVERT(BIT, CASE WHEN ([dp].[owning_principal_id] IS NULL OR [dp].[owning_principal_id] <> [db].[grantor_principal_id]) AND ([dp].[is_fixed_role] <> 1 AND [dp].[principal_id] <> 0) THEN 1 ELSE 0 END) AS [ScriptGrantor],
        USER_NAME([db].[grantor_principal_id]) AS [GrantorName],
        [db].[type] AS [PermissionCode],
        [db].[permission_name] AS [PermissionType],
        [db].[state] AS [PermissionState],
        CASE [db].[class]
            WHEN  0 THEN NULL
            WHEN  3 THEN SCHEMA_NAME([db].[major_id])
            WHEN  4 THEN USER_NAME([db].[major_id])
            WHEN  5 THEN (SELECT TOP 1 [sa2].[name]   FROM [sys].[assemblies] [sa2] WITH (NOLOCK) WHERE [sa2].[assembly_id] = [db].[major_id])
            WHEN  6 THEN SCHEMA_NAME((SELECT TOP 1 [st].[schema_id] FROM [sys].[types] [st] WITH (NOLOCK) WHERE [st].[user_type_id] = [db].[major_id]))
            WHEN 10 THEN SCHEMA_NAME((SELECT TOP 1 [sxsc2].[schema_id] FROM [sys].[xml_schema_collections] [sxsc2] WITH (NOLOCK) WHERE [sxsc2].[xml_collection_id] = [db].[major_id]))
            WHEN 15 THEN (SELECT TOP 1 [smt2].[name]  FROM [sys].[service_message_types] [smt2] WITH (NOLOCK) WHERE [smt2].[message_type_id] = [db].[major_id]) COLLATE database_default
            WHEN 16 THEN (SELECT TOP 1 [ssc2].[name]  FROM [sys].[service_contracts] [ssc2] WITH (NOLOCK) WHERE [ssc2].[service_contract_id] = [db].[major_id])
            WHEN 17 THEN (SELECT TOP 1 [ss2].[name]   FROM [sys].[services] [ss2] WITH (NOLOCK) WHERE [ss2].[service_id] = [db].[major_id])
            WHEN 18 THEN (SELECT TOP 1 [srs2].[name]  FROM [sys].[remote_service_bindings] [srs2] WITH (NOLOCK) WHERE [srs2].[remote_service_binding_id] = [db].[major_id])
            WHEN 19 THEN (SELECT TOP 1 [sr2].[name]   FROM [sys].[routes] [sr2] WITH (NOLOCK) WHERE [sr2].[route_id] = [db].[major_id])
            WHEN 23 THEN (SELECT TOP 1 [ft].[name]    FROM [sys].[fulltext_catalogs] [ft] WITH (NOLOCK) WHERE [ft].[fulltext_catalog_id] = [db].[major_id])
            WHEN 24 THEN (SELECT TOP 1 [sk].[name]    FROM [sys].[symmetric_keys] [sk] WITH (NOLOCK) WHERE [sk].[symmetric_key_id] = [db].[major_id])
            WHEN 25 THEN (SELECT TOP 1 [cert].[name]  FROM [sys].[certificates] [cert] WITH (NOLOCK) WHERE [cert].[certificate_id] = [db].[major_id])
            WHEN 26 THEN (SELECT TOP 1 [ak].[name]    FROM [sys].[asymmetric_keys] [ak] WITH (NOLOCK) WHERE [ak].[asymmetric_key_id] = [db].[major_id])
            WHEN 29 THEN (SELECT TOP 1 [ftsl].[name]    FROM [sys].[fulltext_stoplists] [ftsl] WITH (NOLOCK) WHERE [ftsl].[stoplist_id] = [db].[major_id])
WHEN 31 THEN (SELECT TOP 1 [spl].[name] FROM [sys].[registered_search_property_lists] [spl] WITH (NOLOCK) WHERE [spl].[property_list_id] = [db].[major_id])
        END AS [Level0Name],
        CASE [db].[class]
            WHEN 6  THEN (SELECT TOP 1 [st2].[name] FROM [sys].[types] [st2] WITH (NOLOCK) WHERE [st2].[user_type_id] = [db].[major_id])
            WHEN 10 THEN (SELECT TOP 1 [sx2].[name] FROM [sys].[xml_schema_collections] [sx2] WITH (NOLOCK) WHERE [sx2].[xml_collection_id] = [db].[major_id])
        END AS [Level1Name],
        NULL AS [Level2Name],
        [dp].[type] AS [DataBasePrincipalType]
FROM
        [sys].[database_permissions] [db] WITH (NOLOCK)
        LEFT JOIN [sys].[database_principals] as [dp] WITH (NOLOCK) ON [db].[major_id] = [dp].[principal_id] AND [db].[class] = 4
        INNER JOIN [sys].[database_principals] [dp2] WITH (NOLOCK) ON [dp2].[principal_id] = [db].[grantor_principal_id]
        INNER JOIN [sys].[database_principals] [dp3] WITH (NOLOCK) ON [dp3].[principal_id] = [db].[grantee_principal_id]
WHERE
        [db].[class] <> 1
        AND [db].[state] <> N'R'
        AND [db].[grantee_principal_id] <> [db].[grantor_principal_id]
		AND USER_NAME([db].[grantor_principal_id]) <> N'cdc'
		AND USER_NAME([db].[grantee_principal_id]) <> N'cdc'
        AND ([dp2].[owning_principal_id] IS NULL OR USER_NAME([dp2].[owning_principal_id]) <> N'cdc')
        AND ([dp3].[owning_principal_id] IS NULL OR USER_NAME([dp3].[owning_principal_id]) <> N'cdc')) AS [_results] WHERE [TypeId] = 22 AND [Level2Name] IS NULL AND [Level1Name] IS NULL
 ORDER BY TypeId,GranteeId,GrantorId,MajorId,MinorId,PermissionCode,PermissionState ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90DatabasePermissionStatementPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [db].[class] AS [TypeId],
        [db].[major_id] AS [MajorId],
        [db].[minor_id] AS [MinorId],
        NULL AS [ObjectType],
        CONVERT(bit, 0) AS [IsMsShipped],
        [db].[grantee_principal_id] AS [GranteeId],
        USER_NAME([db].[grantee_principal_id]) AS [GranteeName],
        [db].[grantor_principal_id] AS [GrantorId],
        CONVERT(BIT, CASE WHEN ([dp].[owning_principal_id] IS NULL OR [dp].[owning_principal_id] <> [db].[grantor_principal_id]) AND ([dp].[is_fixed_role] <> 1 AND [dp].[principal_id] <> 0) THEN 1 ELSE 0 END) AS [ScriptGrantor],
        USER_NAME([db].[grantor_principal_id]) AS [GrantorName],
        [db].[type] AS [PermissionCode],
        [db].[permission_name] AS [PermissionType],
        [db].[state] AS [PermissionState],
        CASE [db].[class]
            WHEN  0 THEN NULL
            WHEN  3 THEN SCHEMA_NAME([db].[major_id])
            WHEN  4 THEN USER_NAME([db].[major_id])
            WHEN  5 THEN (SELECT TOP 1 [sa2].[name]   FROM [sys].[assemblies] [sa2] WITH (NOLOCK) WHERE [sa2].[assembly_id] = [db].[major_id])
            WHEN  6 THEN SCHEMA_NAME((SELECT TOP 1 [st].[schema_id] FROM [sys].[types] [st] WITH (NOLOCK) WHERE [st].[user_type_id] = [db].[major_id]))
            WHEN 10 THEN SCHEMA_NAME((SELECT TOP 1 [sxsc2].[schema_id] FROM [sys].[xml_schema_collections] [sxsc2] WITH (NOLOCK) WHERE [sxsc2].[xml_collection_id] = [db].[major_id]))
            WHEN 15 THEN (SELECT TOP 1 [smt2].[name]  FROM [sys].[service_message_types] [smt2] WITH (NOLOCK) WHERE [smt2].[message_type_id] = [db].[major_id]) COLLATE database_default
            WHEN 16 THEN (SELECT TOP 1 [ssc2].[name]  FROM [sys].[service_contracts] [ssc2] WITH (NOLOCK) WHERE [ssc2].[service_contract_id] = [db].[major_id])
            WHEN 17 THEN (SELECT TOP 1 [ss2].[name]   FROM [sys].[services] [ss2] WITH (NOLOCK) WHERE [ss2].[service_id] = [db].[major_id])
            WHEN 18 THEN (SELECT TOP 1 [srs2].[name]  FROM [sys].[remote_service_bindings] [srs2] WITH (NOLOCK) WHERE [srs2].[remote_service_binding_id] = [db].[major_id])
            WHEN 19 THEN (SELECT TOP 1 [sr2].[name]   FROM [sys].[routes] [sr2] WITH (NOLOCK) WHERE [sr2].[route_id] = [db].[major_id])
            WHEN 23 THEN (SELECT TOP 1 [ft].[name]    FROM [sys].[fulltext_catalogs] [ft] WITH (NOLOCK) WHERE [ft].[fulltext_catalog_id] = [db].[major_id])
            WHEN 24 THEN (SELECT TOP 1 [sk].[name]    FROM [sys].[symmetric_keys] [sk] WITH (NOLOCK) WHERE [sk].[symmetric_key_id] = [db].[major_id])
            WHEN 25 THEN (SELECT TOP 1 [cert].[name]  FROM [sys].[certificates] [cert] WITH (NOLOCK) WHERE [cert].[certificate_id] = [db].[major_id])
            WHEN 26 THEN (SELECT TOP 1 [ak].[name]    FROM [sys].[asymmetric_keys] [ak] WITH (NOLOCK) WHERE [ak].[asymmetric_key_id] = [db].[major_id])
            WHEN 29 THEN (SELECT TOP 1 [ftsl].[name]    FROM [sys].[fulltext_stoplists] [ftsl] WITH (NOLOCK) WHERE [ftsl].[stoplist_id] = [db].[major_id])
WHEN 31 THEN (SELECT TOP 1 [spl].[name] FROM [sys].[registered_search_property_lists] [spl] WITH (NOLOCK) WHERE [spl].[property_list_id] = [db].[major_id])
        END AS [Level0Name],
        CASE [db].[class]
            WHEN 6  THEN (SELECT TOP 1 [st2].[name] FROM [sys].[types] [st2] WITH (NOLOCK) WHERE [st2].[user_type_id] = [db].[major_id])
            WHEN 10 THEN (SELECT TOP 1 [sx2].[name] FROM [sys].[xml_schema_collections] [sx2] WITH (NOLOCK) WHERE [sx2].[xml_collection_id] = [db].[major_id])
        END AS [Level1Name],
        NULL AS [Level2Name],
        [dp].[type] AS [DataBasePrincipalType]
FROM
        [sys].[database_permissions] [db] WITH (NOLOCK)
        LEFT JOIN [sys].[database_principals] as [dp] WITH (NOLOCK) ON [db].[major_id] = [dp].[principal_id] AND [db].[class] = 4
        INNER JOIN [sys].[database_principals] [dp2] WITH (NOLOCK) ON [dp2].[principal_id] = [db].[grantor_principal_id]
        INNER JOIN [sys].[database_principals] [dp3] WITH (NOLOCK) ON [dp3].[principal_id] = [db].[grantee_principal_id]
WHERE
        [db].[class] <> 1
        AND [db].[state] <> N'R'
        AND [db].[grantee_principal_id] <> [db].[grantor_principal_id]
		AND USER_NAME([db].[grantor_principal_id]) <> N'cdc'
		AND USER_NAME([db].[grantee_principal_id]) <> N'cdc'
        AND ([dp2].[owning_principal_id] IS NULL OR USER_NAME([dp2].[owning_principal_id]) <> N'cdc')
        AND ([dp3].[owning_principal_id] IS NULL OR USER_NAME([dp3].[owning_principal_id]) <> N'cdc')) AS [_results] WHERE [TypeId] = 23 AND [Level2Name] IS NULL AND [Level1Name] IS NULL
 ORDER BY TypeId,GranteeId,GrantorId,MajorId,MinorId,PermissionCode,PermissionState ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90DatabasePermissionStatementPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [db].[class] AS [TypeId],
        [db].[major_id] AS [MajorId],
        [db].[minor_id] AS [MinorId],
        NULL AS [ObjectType],
        CONVERT(bit, 0) AS [IsMsShipped],
        [db].[grantee_principal_id] AS [GranteeId],
        USER_NAME([db].[grantee_principal_id]) AS [GranteeName],
        [db].[grantor_principal_id] AS [GrantorId],
        CONVERT(BIT, CASE WHEN ([dp].[owning_principal_id] IS NULL OR [dp].[owning_principal_id] <> [db].[grantor_principal_id]) AND ([dp].[is_fixed_role] <> 1 AND [dp].[principal_id] <> 0) THEN 1 ELSE 0 END) AS [ScriptGrantor],
        USER_NAME([db].[grantor_principal_id]) AS [GrantorName],
        [db].[type] AS [PermissionCode],
        [db].[permission_name] AS [PermissionType],
        [db].[state] AS [PermissionState],
        CASE [db].[class]
            WHEN  0 THEN NULL
            WHEN  3 THEN SCHEMA_NAME([db].[major_id])
            WHEN  4 THEN USER_NAME([db].[major_id])
            WHEN  5 THEN (SELECT TOP 1 [sa2].[name]   FROM [sys].[assemblies] [sa2] WITH (NOLOCK) WHERE [sa2].[assembly_id] = [db].[major_id])
            WHEN  6 THEN SCHEMA_NAME((SELECT TOP 1 [st].[schema_id] FROM [sys].[types] [st] WITH (NOLOCK) WHERE [st].[user_type_id] = [db].[major_id]))
            WHEN 10 THEN SCHEMA_NAME((SELECT TOP 1 [sxsc2].[schema_id] FROM [sys].[xml_schema_collections] [sxsc2] WITH (NOLOCK) WHERE [sxsc2].[xml_collection_id] = [db].[major_id]))
            WHEN 15 THEN (SELECT TOP 1 [smt2].[name]  FROM [sys].[service_message_types] [smt2] WITH (NOLOCK) WHERE [smt2].[message_type_id] = [db].[major_id]) COLLATE database_default
            WHEN 16 THEN (SELECT TOP 1 [ssc2].[name]  FROM [sys].[service_contracts] [ssc2] WITH (NOLOCK) WHERE [ssc2].[service_contract_id] = [db].[major_id])
            WHEN 17 THEN (SELECT TOP 1 [ss2].[name]   FROM [sys].[services] [ss2] WITH (NOLOCK) WHERE [ss2].[service_id] = [db].[major_id])
            WHEN 18 THEN (SELECT TOP 1 [srs2].[name]  FROM [sys].[remote_service_bindings] [srs2] WITH (NOLOCK) WHERE [srs2].[remote_service_binding_id] = [db].[major_id])
            WHEN 19 THEN (SELECT TOP 1 [sr2].[name]   FROM [sys].[routes] [sr2] WITH (NOLOCK) WHERE [sr2].[route_id] = [db].[major_id])
            WHEN 23 THEN (SELECT TOP 1 [ft].[name]    FROM [sys].[fulltext_catalogs] [ft] WITH (NOLOCK) WHERE [ft].[fulltext_catalog_id] = [db].[major_id])
            WHEN 24 THEN (SELECT TOP 1 [sk].[name]    FROM [sys].[symmetric_keys] [sk] WITH (NOLOCK) WHERE [sk].[symmetric_key_id] = [db].[major_id])
            WHEN 25 THEN (SELECT TOP 1 [cert].[name]  FROM [sys].[certificates] [cert] WITH (NOLOCK) WHERE [cert].[certificate_id] = [db].[major_id])
            WHEN 26 THEN (SELECT TOP 1 [ak].[name]    FROM [sys].[asymmetric_keys] [ak] WITH (NOLOCK) WHERE [ak].[asymmetric_key_id] = [db].[major_id])
            WHEN 29 THEN (SELECT TOP 1 [ftsl].[name]    FROM [sys].[fulltext_stoplists] [ftsl] WITH (NOLOCK) WHERE [ftsl].[stoplist_id] = [db].[major_id])
WHEN 31 THEN (SELECT TOP 1 [spl].[name] FROM [sys].[registered_search_property_lists] [spl] WITH (NOLOCK) WHERE [spl].[property_list_id] = [db].[major_id])
        END AS [Level0Name],
        CASE [db].[class]
            WHEN 6  THEN (SELECT TOP 1 [st2].[name] FROM [sys].[types] [st2] WITH (NOLOCK) WHERE [st2].[user_type_id] = [db].[major_id])
            WHEN 10 THEN (SELECT TOP 1 [sx2].[name] FROM [sys].[xml_schema_collections] [sx2] WITH (NOLOCK) WHERE [sx2].[xml_collection_id] = [db].[major_id])
        END AS [Level1Name],
        NULL AS [Level2Name],
        [dp].[type] AS [DataBasePrincipalType]
FROM
        [sys].[database_permissions] [db] WITH (NOLOCK)
        LEFT JOIN [sys].[database_principals] as [dp] WITH (NOLOCK) ON [db].[major_id] = [dp].[principal_id] AND [db].[class] = 4
        INNER JOIN [sys].[database_principals] [dp2] WITH (NOLOCK) ON [dp2].[principal_id] = [db].[grantor_principal_id]
        INNER JOIN [sys].[database_principals] [dp3] WITH (NOLOCK) ON [dp3].[principal_id] = [db].[grantee_principal_id]
WHERE
        [db].[class] <> 1
        AND [db].[state] <> N'R'
        AND [db].[grantee_principal_id] <> [db].[grantor_principal_id]
		AND USER_NAME([db].[grantor_principal_id]) <> N'cdc'
		AND USER_NAME([db].[grantee_principal_id]) <> N'cdc'
        AND ([dp2].[owning_principal_id] IS NULL OR USER_NAME([dp2].[owning_principal_id]) <> N'cdc')
        AND ([dp3].[owning_principal_id] IS NULL OR USER_NAME([dp3].[owning_principal_id]) <> N'cdc')) AS [_results] WHERE [TypeId] = 24 AND [Level2Name] IS NULL AND [Level1Name] IS NULL
 ORDER BY TypeId,GranteeId,GrantorId,MajorId,MinorId,PermissionCode,PermissionState ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90DatabasePermissionStatementPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [db].[class] AS [TypeId],
        [db].[major_id] AS [MajorId],
        [db].[minor_id] AS [MinorId],
        NULL AS [ObjectType],
        CONVERT(bit, 0) AS [IsMsShipped],
        [db].[grantee_principal_id] AS [GranteeId],
        USER_NAME([db].[grantee_principal_id]) AS [GranteeName],
        [db].[grantor_principal_id] AS [GrantorId],
        CONVERT(BIT, CASE WHEN ([dp].[owning_principal_id] IS NULL OR [dp].[owning_principal_id] <> [db].[grantor_principal_id]) AND ([dp].[is_fixed_role] <> 1 AND [dp].[principal_id] <> 0) THEN 1 ELSE 0 END) AS [ScriptGrantor],
        USER_NAME([db].[grantor_principal_id]) AS [GrantorName],
        [db].[type] AS [PermissionCode],
        [db].[permission_name] AS [PermissionType],
        [db].[state] AS [PermissionState],
        CASE [db].[class]
            WHEN  0 THEN NULL
            WHEN  3 THEN SCHEMA_NAME([db].[major_id])
            WHEN  4 THEN USER_NAME([db].[major_id])
            WHEN  5 THEN (SELECT TOP 1 [sa2].[name]   FROM [sys].[assemblies] [sa2] WITH (NOLOCK) WHERE [sa2].[assembly_id] = [db].[major_id])
            WHEN  6 THEN SCHEMA_NAME((SELECT TOP 1 [st].[schema_id] FROM [sys].[types] [st] WITH (NOLOCK) WHERE [st].[user_type_id] = [db].[major_id]))
            WHEN 10 THEN SCHEMA_NAME((SELECT TOP 1 [sxsc2].[schema_id] FROM [sys].[xml_schema_collections] [sxsc2] WITH (NOLOCK) WHERE [sxsc2].[xml_collection_id] = [db].[major_id]))
            WHEN 15 THEN (SELECT TOP 1 [smt2].[name]  FROM [sys].[service_message_types] [smt2] WITH (NOLOCK) WHERE [smt2].[message_type_id] = [db].[major_id]) COLLATE database_default
            WHEN 16 THEN (SELECT TOP 1 [ssc2].[name]  FROM [sys].[service_contracts] [ssc2] WITH (NOLOCK) WHERE [ssc2].[service_contract_id] = [db].[major_id])
            WHEN 17 THEN (SELECT TOP 1 [ss2].[name]   FROM [sys].[services] [ss2] WITH (NOLOCK) WHERE [ss2].[service_id] = [db].[major_id])
            WHEN 18 THEN (SELECT TOP 1 [srs2].[name]  FROM [sys].[remote_service_bindings] [srs2] WITH (NOLOCK) WHERE [srs2].[remote_service_binding_id] = [db].[major_id])
            WHEN 19 THEN (SELECT TOP 1 [sr2].[name]   FROM [sys].[routes] [sr2] WITH (NOLOCK) WHERE [sr2].[route_id] = [db].[major_id])
            WHEN 23 THEN (SELECT TOP 1 [ft].[name]    FROM [sys].[fulltext_catalogs] [ft] WITH (NOLOCK) WHERE [ft].[fulltext_catalog_id] = [db].[major_id])
            WHEN 24 THEN (SELECT TOP 1 [sk].[name]    FROM [sys].[symmetric_keys] [sk] WITH (NOLOCK) WHERE [sk].[symmetric_key_id] = [db].[major_id])
            WHEN 25 THEN (SELECT TOP 1 [cert].[name]  FROM [sys].[certificates] [cert] WITH (NOLOCK) WHERE [cert].[certificate_id] = [db].[major_id])
            WHEN 26 THEN (SELECT TOP 1 [ak].[name]    FROM [sys].[asymmetric_keys] [ak] WITH (NOLOCK) WHERE [ak].[asymmetric_key_id] = [db].[major_id])
            WHEN 29 THEN (SELECT TOP 1 [ftsl].[name]    FROM [sys].[fulltext_stoplists] [ftsl] WITH (NOLOCK) WHERE [ftsl].[stoplist_id] = [db].[major_id])
WHEN 31 THEN (SELECT TOP 1 [spl].[name] FROM [sys].[registered_search_property_lists] [spl] WITH (NOLOCK) WHERE [spl].[property_list_id] = [db].[major_id])
        END AS [Level0Name],
        CASE [db].[class]
            WHEN 6  THEN (SELECT TOP 1 [st2].[name] FROM [sys].[types] [st2] WITH (NOLOCK) WHERE [st2].[user_type_id] = [db].[major_id])
            WHEN 10 THEN (SELECT TOP 1 [sx2].[name] FROM [sys].[xml_schema_collections] [sx2] WITH (NOLOCK) WHERE [sx2].[xml_collection_id] = [db].[major_id])
        END AS [Level1Name],
        NULL AS [Level2Name],
        [dp].[type] AS [DataBasePrincipalType]
FROM
        [sys].[database_permissions] [db] WITH (NOLOCK)
        LEFT JOIN [sys].[database_principals] as [dp] WITH (NOLOCK) ON [db].[major_id] = [dp].[principal_id] AND [db].[class] = 4
        INNER JOIN [sys].[database_principals] [dp2] WITH (NOLOCK) ON [dp2].[principal_id] = [db].[grantor_principal_id]
        INNER JOIN [sys].[database_principals] [dp3] WITH (NOLOCK) ON [dp3].[principal_id] = [db].[grantee_principal_id]
WHERE
        [db].[class] <> 1
        AND [db].[state] <> N'R'
        AND [db].[grantee_principal_id] <> [db].[grantor_principal_id]
		AND USER_NAME([db].[grantor_principal_id]) <> N'cdc'
		AND USER_NAME([db].[grantee_principal_id]) <> N'cdc'
        AND ([dp2].[owning_principal_id] IS NULL OR USER_NAME([dp2].[owning_principal_id]) <> N'cdc')
        AND ([dp3].[owning_principal_id] IS NULL OR USER_NAME([dp3].[owning_principal_id]) <> N'cdc')) AS [_results] WHERE [TypeId] = 25 AND [Level2Name] IS NULL AND [Level1Name] IS NULL
 ORDER BY TypeId,GranteeId,GrantorId,MajorId,MinorId,PermissionCode,PermissionState ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90DatabasePermissionStatementPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [db].[class] AS [TypeId],
        [db].[major_id] AS [MajorId],
        [db].[minor_id] AS [MinorId],
        NULL AS [ObjectType],
        CONVERT(bit, 0) AS [IsMsShipped],
        [db].[grantee_principal_id] AS [GranteeId],
        USER_NAME([db].[grantee_principal_id]) AS [GranteeName],
        [db].[grantor_principal_id] AS [GrantorId],
        CONVERT(BIT, CASE WHEN ([dp].[owning_principal_id] IS NULL OR [dp].[owning_principal_id] <> [db].[grantor_principal_id]) AND ([dp].[is_fixed_role] <> 1 AND [dp].[principal_id] <> 0) THEN 1 ELSE 0 END) AS [ScriptGrantor],
        USER_NAME([db].[grantor_principal_id]) AS [GrantorName],
        [db].[type] AS [PermissionCode],
        [db].[permission_name] AS [PermissionType],
        [db].[state] AS [PermissionState],
        CASE [db].[class]
            WHEN  0 THEN NULL
            WHEN  3 THEN SCHEMA_NAME([db].[major_id])
            WHEN  4 THEN USER_NAME([db].[major_id])
            WHEN  5 THEN (SELECT TOP 1 [sa2].[name]   FROM [sys].[assemblies] [sa2] WITH (NOLOCK) WHERE [sa2].[assembly_id] = [db].[major_id])
            WHEN  6 THEN SCHEMA_NAME((SELECT TOP 1 [st].[schema_id] FROM [sys].[types] [st] WITH (NOLOCK) WHERE [st].[user_type_id] = [db].[major_id]))
            WHEN 10 THEN SCHEMA_NAME((SELECT TOP 1 [sxsc2].[schema_id] FROM [sys].[xml_schema_collections] [sxsc2] WITH (NOLOCK) WHERE [sxsc2].[xml_collection_id] = [db].[major_id]))
            WHEN 15 THEN (SELECT TOP 1 [smt2].[name]  FROM [sys].[service_message_types] [smt2] WITH (NOLOCK) WHERE [smt2].[message_type_id] = [db].[major_id]) COLLATE database_default
            WHEN 16 THEN (SELECT TOP 1 [ssc2].[name]  FROM [sys].[service_contracts] [ssc2] WITH (NOLOCK) WHERE [ssc2].[service_contract_id] = [db].[major_id])
            WHEN 17 THEN (SELECT TOP 1 [ss2].[name]   FROM [sys].[services] [ss2] WITH (NOLOCK) WHERE [ss2].[service_id] = [db].[major_id])
            WHEN 18 THEN (SELECT TOP 1 [srs2].[name]  FROM [sys].[remote_service_bindings] [srs2] WITH (NOLOCK) WHERE [srs2].[remote_service_binding_id] = [db].[major_id])
            WHEN 19 THEN (SELECT TOP 1 [sr2].[name]   FROM [sys].[routes] [sr2] WITH (NOLOCK) WHERE [sr2].[route_id] = [db].[major_id])
            WHEN 23 THEN (SELECT TOP 1 [ft].[name]    FROM [sys].[fulltext_catalogs] [ft] WITH (NOLOCK) WHERE [ft].[fulltext_catalog_id] = [db].[major_id])
            WHEN 24 THEN (SELECT TOP 1 [sk].[name]    FROM [sys].[symmetric_keys] [sk] WITH (NOLOCK) WHERE [sk].[symmetric_key_id] = [db].[major_id])
            WHEN 25 THEN (SELECT TOP 1 [cert].[name]  FROM [sys].[certificates] [cert] WITH (NOLOCK) WHERE [cert].[certificate_id] = [db].[major_id])
            WHEN 26 THEN (SELECT TOP 1 [ak].[name]    FROM [sys].[asymmetric_keys] [ak] WITH (NOLOCK) WHERE [ak].[asymmetric_key_id] = [db].[major_id])
            WHEN 29 THEN (SELECT TOP 1 [ftsl].[name]    FROM [sys].[fulltext_stoplists] [ftsl] WITH (NOLOCK) WHERE [ftsl].[stoplist_id] = [db].[major_id])
WHEN 31 THEN (SELECT TOP 1 [spl].[name] FROM [sys].[registered_search_property_lists] [spl] WITH (NOLOCK) WHERE [spl].[property_list_id] = [db].[major_id])
        END AS [Level0Name],
        CASE [db].[class]
            WHEN 6  THEN (SELECT TOP 1 [st2].[name] FROM [sys].[types] [st2] WITH (NOLOCK) WHERE [st2].[user_type_id] = [db].[major_id])
            WHEN 10 THEN (SELECT TOP 1 [sx2].[name] FROM [sys].[xml_schema_collections] [sx2] WITH (NOLOCK) WHERE [sx2].[xml_collection_id] = [db].[major_id])
        END AS [Level1Name],
        NULL AS [Level2Name],
        [dp].[type] AS [DataBasePrincipalType]
FROM
        [sys].[database_permissions] [db] WITH (NOLOCK)
        LEFT JOIN [sys].[database_principals] as [dp] WITH (NOLOCK) ON [db].[major_id] = [dp].[principal_id] AND [db].[class] = 4
        INNER JOIN [sys].[database_principals] [dp2] WITH (NOLOCK) ON [dp2].[principal_id] = [db].[grantor_principal_id]
        INNER JOIN [sys].[database_principals] [dp3] WITH (NOLOCK) ON [dp3].[principal_id] = [db].[grantee_principal_id]
WHERE
        [db].[class] <> 1
        AND [db].[state] <> N'R'
        AND [db].[grantee_principal_id] <> [db].[grantor_principal_id]
		AND USER_NAME([db].[grantor_principal_id]) <> N'cdc'
		AND USER_NAME([db].[grantee_principal_id]) <> N'cdc'
        AND ([dp2].[owning_principal_id] IS NULL OR USER_NAME([dp2].[owning_principal_id]) <> N'cdc')
        AND ([dp3].[owning_principal_id] IS NULL OR USER_NAME([dp3].[owning_principal_id]) <> N'cdc')) AS [_results] WHERE [TypeId] = 26 AND [Level2Name] IS NULL AND [Level1Name] IS NULL
 ORDER BY TypeId,GranteeId,GrantorId,MajorId,MinorId,PermissionCode,PermissionState ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90DatabasePermissionStatementPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [db].[class] AS [TypeId],
        [db].[major_id] AS [MajorId],
        [db].[minor_id] AS [MinorId],
        NULL AS [ObjectType],
        CONVERT(bit, 0) AS [IsMsShipped],
        [db].[grantee_principal_id] AS [GranteeId],
        USER_NAME([db].[grantee_principal_id]) AS [GranteeName],
        [db].[grantor_principal_id] AS [GrantorId],
        CONVERT(BIT, CASE WHEN ([dp].[owning_principal_id] IS NULL OR [dp].[owning_principal_id] <> [db].[grantor_principal_id]) AND ([dp].[is_fixed_role] <> 1 AND [dp].[principal_id] <> 0) THEN 1 ELSE 0 END) AS [ScriptGrantor],
        USER_NAME([db].[grantor_principal_id]) AS [GrantorName],
        [db].[type] AS [PermissionCode],
        [db].[permission_name] AS [PermissionType],
        [db].[state] AS [PermissionState],
        CASE [db].[class]
            WHEN  0 THEN NULL
            WHEN  3 THEN SCHEMA_NAME([db].[major_id])
            WHEN  4 THEN USER_NAME([db].[major_id])
            WHEN  5 THEN (SELECT TOP 1 [sa2].[name]   FROM [sys].[assemblies] [sa2] WITH (NOLOCK) WHERE [sa2].[assembly_id] = [db].[major_id])
            WHEN  6 THEN SCHEMA_NAME((SELECT TOP 1 [st].[schema_id] FROM [sys].[types] [st] WITH (NOLOCK) WHERE [st].[user_type_id] = [db].[major_id]))
            WHEN 10 THEN SCHEMA_NAME((SELECT TOP 1 [sxsc2].[schema_id] FROM [sys].[xml_schema_collections] [sxsc2] WITH (NOLOCK) WHERE [sxsc2].[xml_collection_id] = [db].[major_id]))
            WHEN 15 THEN (SELECT TOP 1 [smt2].[name]  FROM [sys].[service_message_types] [smt2] WITH (NOLOCK) WHERE [smt2].[message_type_id] = [db].[major_id]) COLLATE database_default
            WHEN 16 THEN (SELECT TOP 1 [ssc2].[name]  FROM [sys].[service_contracts] [ssc2] WITH (NOLOCK) WHERE [ssc2].[service_contract_id] = [db].[major_id])
            WHEN 17 THEN (SELECT TOP 1 [ss2].[name]   FROM [sys].[services] [ss2] WITH (NOLOCK) WHERE [ss2].[service_id] = [db].[major_id])
            WHEN 18 THEN (SELECT TOP 1 [srs2].[name]  FROM [sys].[remote_service_bindings] [srs2] WITH (NOLOCK) WHERE [srs2].[remote_service_binding_id] = [db].[major_id])
            WHEN 19 THEN (SELECT TOP 1 [sr2].[name]   FROM [sys].[routes] [sr2] WITH (NOLOCK) WHERE [sr2].[route_id] = [db].[major_id])
            WHEN 23 THEN (SELECT TOP 1 [ft].[name]    FROM [sys].[fulltext_catalogs] [ft] WITH (NOLOCK) WHERE [ft].[fulltext_catalog_id] = [db].[major_id])
            WHEN 24 THEN (SELECT TOP 1 [sk].[name]    FROM [sys].[symmetric_keys] [sk] WITH (NOLOCK) WHERE [sk].[symmetric_key_id] = [db].[major_id])
            WHEN 25 THEN (SELECT TOP 1 [cert].[name]  FROM [sys].[certificates] [cert] WITH (NOLOCK) WHERE [cert].[certificate_id] = [db].[major_id])
            WHEN 26 THEN (SELECT TOP 1 [ak].[name]    FROM [sys].[asymmetric_keys] [ak] WITH (NOLOCK) WHERE [ak].[asymmetric_key_id] = [db].[major_id])
            WHEN 29 THEN (SELECT TOP 1 [ftsl].[name]    FROM [sys].[fulltext_stoplists] [ftsl] WITH (NOLOCK) WHERE [ftsl].[stoplist_id] = [db].[major_id])
WHEN 31 THEN (SELECT TOP 1 [spl].[name] FROM [sys].[registered_search_property_lists] [spl] WITH (NOLOCK) WHERE [spl].[property_list_id] = [db].[major_id])
        END AS [Level0Name],
        CASE [db].[class]
            WHEN 6  THEN (SELECT TOP 1 [st2].[name] FROM [sys].[types] [st2] WITH (NOLOCK) WHERE [st2].[user_type_id] = [db].[major_id])
            WHEN 10 THEN (SELECT TOP 1 [sx2].[name] FROM [sys].[xml_schema_collections] [sx2] WITH (NOLOCK) WHERE [sx2].[xml_collection_id] = [db].[major_id])
        END AS [Level1Name],
        NULL AS [Level2Name],
        [dp].[type] AS [DataBasePrincipalType]
FROM
        [sys].[database_permissions] [db] WITH (NOLOCK)
        LEFT JOIN [sys].[database_principals] as [dp] WITH (NOLOCK) ON [db].[major_id] = [dp].[principal_id] AND [db].[class] = 4
        INNER JOIN [sys].[database_principals] [dp2] WITH (NOLOCK) ON [dp2].[principal_id] = [db].[grantor_principal_id]
        INNER JOIN [sys].[database_principals] [dp3] WITH (NOLOCK) ON [dp3].[principal_id] = [db].[grantee_principal_id]
WHERE
        [db].[class] <> 1
        AND [db].[state] <> N'R'
        AND [db].[grantee_principal_id] <> [db].[grantor_principal_id]
		AND USER_NAME([db].[grantor_principal_id]) <> N'cdc'
		AND USER_NAME([db].[grantee_principal_id]) <> N'cdc'
        AND ([dp2].[owning_principal_id] IS NULL OR USER_NAME([dp2].[owning_principal_id]) <> N'cdc')
        AND ([dp3].[owning_principal_id] IS NULL OR USER_NAME([dp3].[owning_principal_id]) <> N'cdc')) AS [_results] WHERE [TypeId] = 29 AND [Level2Name] IS NULL AND [Level1Name] IS NULL
 ORDER BY TypeId,GranteeId,GrantorId,MajorId,MinorId,PermissionCode,PermissionState ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90DatabasePermissionStatementPopulator' as [PopulatorName];
SELECT * FROM (
SELECT  [db].[class] AS [TypeId],
        [db].[major_id] AS [MajorId],
        [db].[minor_id] AS [MinorId],
        NULL AS [ObjectType],
        CONVERT(bit, 0) AS [IsMsShipped],
        [db].[grantee_principal_id] AS [GranteeId],
        USER_NAME([db].[grantee_principal_id]) AS [GranteeName],
        [db].[grantor_principal_id] AS [GrantorId],
        CONVERT(BIT, CASE WHEN ([dp].[owning_principal_id] IS NULL OR [dp].[owning_principal_id] <> [db].[grantor_principal_id]) AND ([dp].[is_fixed_role] <> 1 AND [dp].[principal_id] <> 0) THEN 1 ELSE 0 END) AS [ScriptGrantor],
        USER_NAME([db].[grantor_principal_id]) AS [GrantorName],
        [db].[type] AS [PermissionCode],
        [db].[permission_name] AS [PermissionType],
        [db].[state] AS [PermissionState],
        CASE [db].[class]
            WHEN  0 THEN NULL
            WHEN  3 THEN SCHEMA_NAME([db].[major_id])
            WHEN  4 THEN USER_NAME([db].[major_id])
            WHEN  5 THEN (SELECT TOP 1 [sa2].[name]   FROM [sys].[assemblies] [sa2] WITH (NOLOCK) WHERE [sa2].[assembly_id] = [db].[major_id])
            WHEN  6 THEN SCHEMA_NAME((SELECT TOP 1 [st].[schema_id] FROM [sys].[types] [st] WITH (NOLOCK) WHERE [st].[user_type_id] = [db].[major_id]))
            WHEN 10 THEN SCHEMA_NAME((SELECT TOP 1 [sxsc2].[schema_id] FROM [sys].[xml_schema_collections] [sxsc2] WITH (NOLOCK) WHERE [sxsc2].[xml_collection_id] = [db].[major_id]))
            WHEN 15 THEN (SELECT TOP 1 [smt2].[name]  FROM [sys].[service_message_types] [smt2] WITH (NOLOCK) WHERE [smt2].[message_type_id] = [db].[major_id]) COLLATE database_default
            WHEN 16 THEN (SELECT TOP 1 [ssc2].[name]  FROM [sys].[service_contracts] [ssc2] WITH (NOLOCK) WHERE [ssc2].[service_contract_id] = [db].[major_id])
            WHEN 17 THEN (SELECT TOP 1 [ss2].[name]   FROM [sys].[services] [ss2] WITH (NOLOCK) WHERE [ss2].[service_id] = [db].[major_id])
            WHEN 18 THEN (SELECT TOP 1 [srs2].[name]  FROM [sys].[remote_service_bindings] [srs2] WITH (NOLOCK) WHERE [srs2].[remote_service_binding_id] = [db].[major_id])
            WHEN 19 THEN (SELECT TOP 1 [sr2].[name]   FROM [sys].[routes] [sr2] WITH (NOLOCK) WHERE [sr2].[route_id] = [db].[major_id])
            WHEN 23 THEN (SELECT TOP 1 [ft].[name]    FROM [sys].[fulltext_catalogs] [ft] WITH (NOLOCK) WHERE [ft].[fulltext_catalog_id] = [db].[major_id])
            WHEN 24 THEN (SELECT TOP 1 [sk].[name]    FROM [sys].[symmetric_keys] [sk] WITH (NOLOCK) WHERE [sk].[symmetric_key_id] = [db].[major_id])
            WHEN 25 THEN (SELECT TOP 1 [cert].[name]  FROM [sys].[certificates] [cert] WITH (NOLOCK) WHERE [cert].[certificate_id] = [db].[major_id])
            WHEN 26 THEN (SELECT TOP 1 [ak].[name]    FROM [sys].[asymmetric_keys] [ak] WITH (NOLOCK) WHERE [ak].[asymmetric_key_id] = [db].[major_id])
            WHEN 29 THEN (SELECT TOP 1 [ftsl].[name]    FROM [sys].[fulltext_stoplists] [ftsl] WITH (NOLOCK) WHERE [ftsl].[stoplist_id] = [db].[major_id])
WHEN 31 THEN (SELECT TOP 1 [spl].[name] FROM [sys].[registered_search_property_lists] [spl] WITH (NOLOCK) WHERE [spl].[property_list_id] = [db].[major_id])
        END AS [Level0Name],
        CASE [db].[class]
            WHEN 6  THEN (SELECT TOP 1 [st2].[name] FROM [sys].[types] [st2] WITH (NOLOCK) WHERE [st2].[user_type_id] = [db].[major_id])
            WHEN 10 THEN (SELECT TOP 1 [sx2].[name] FROM [sys].[xml_schema_collections] [sx2] WITH (NOLOCK) WHERE [sx2].[xml_collection_id] = [db].[major_id])
        END AS [Level1Name],
        NULL AS [Level2Name],
        [dp].[type] AS [DataBasePrincipalType]
FROM
        [sys].[database_permissions] [db] WITH (NOLOCK)
        LEFT JOIN [sys].[database_principals] as [dp] WITH (NOLOCK) ON [db].[major_id] = [dp].[principal_id] AND [db].[class] = 4
        INNER JOIN [sys].[database_principals] [dp2] WITH (NOLOCK) ON [dp2].[principal_id] = [db].[grantor_principal_id]
        INNER JOIN [sys].[database_principals] [dp3] WITH (NOLOCK) ON [dp3].[principal_id] = [db].[grantee_principal_id]
WHERE
        [db].[class] <> 1
        AND [db].[state] <> N'R'
        AND [db].[grantee_principal_id] <> [db].[grantor_principal_id]
		AND USER_NAME([db].[grantor_principal_id]) <> N'cdc'
		AND USER_NAME([db].[grantee_principal_id]) <> N'cdc'
        AND ([dp2].[owning_principal_id] IS NULL OR USER_NAME([dp2].[owning_principal_id]) <> N'cdc')
        AND ([dp3].[owning_principal_id] IS NULL OR USER_NAME([dp3].[owning_principal_id]) <> N'cdc')) AS [_results] WHERE [TypeId] = 31 AND [Level2Name] IS NULL AND [Level1Name] IS NULL
 ORDER BY TypeId,GranteeId,GrantorId,MajorId,MinorId,PermissionCode,PermissionState ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90DatabasePermissionStatementPopulator' as [PopulatorName];
SELECT * FROM (
SELECT DISTINCT
        [db].[class] AS [TypeId],
        [db].[major_id] AS [MajorId],
        [db].[minor_id] AS [MinorId],
        CASE [ft].[is_filetable] WHEN 1 THEN N'UF' ELSE [so].[type] END AS [ObjectType],
        [so].[is_ms_shipped] AS [IsMsShipped],
        [db].[grantee_principal_id] AS [GranteeId],
        USER_NAME([db].[grantee_principal_id]) AS [GranteeName],
        [db].[grantor_principal_id] AS [GrantorId],
        CONVERT(BIT, CASE WHEN [dp].[is_fixed_role] = 1 OR [dp].[principal_id] = 0 THEN 0 ELSE 1 END) AS [ScriptGrantor],
        USER_NAME([db].[grantor_principal_id]) AS [GrantorName],
        [db].[type] AS [PermissionCode],
        [db].[permission_name] AS [PermissionType],
        [db].[state] AS [PermissionState],
        SCHEMA_NAME([so].[schema_id]) AS [Level0Name],
        [so].[name] AS [Level1Name],
        [c].[name] AS [Level2Name],
        NULL AS [DataBasePrincipalType]
FROM
        [sys].[database_permissions] [db] WITH (NOLOCK)
        INNER JOIN [sys].[objects] [so] WITH (NOLOCK) ON [so].[object_id] = [db].[major_id]
        INNER JOIN [sys].[database_principals] [dp] WITH (NOLOCK) ON [dp].[principal_id] = [db].[grantor_principal_id]
        INNER JOIN [sys].[database_principals] [dp2] WITH (NOLOCK) ON [dp2].[principal_id] = [db].[grantee_principal_id]
        LEFT JOIN [sys].[columns] [c] WITH (NOLOCK) ON [db].[major_id] = [c].[object_id]  AND [db].[minor_id] = [c].[column_id]
LEFT JOIN [sys].[tables] [ft] WITH(NOLOCK) ON [ft].[object_id] = [so].[object_id]
WHERE
        [db].[class] = 1
        AND [so].[object_id] NOT IN (SELECT [major_id] FROM [sys].[extended_properties] WITH (NOLOCK) WHERE [minor_id] = 0 AND [class] = 1 AND [name] = N'microsoft_database_tools_support')
        AND OBJECTPROPERTY([so].[object_id], N'IsSystemTable') = 0
        AND [db].[state] <> N'R'
        AND [db].[grantee_principal_id] <> [db].[grantor_principal_id]
        AND USER_NAME([db].[grantor_principal_id])  <> N'cdc'
		AND USER_NAME([db].[grantee_principal_id])  <> N'cdc'
        AND ([dp].[owning_principal_id] IS NULL OR USER_NAME([dp].[owning_principal_id]) <> N'cdc')
        AND ([dp2].[owning_principal_id] IS NULL OR USER_NAME([dp2].[owning_principal_id]) <> N'cdc')
        AND NOT EXISTS (SELECT TOP 1 1 FROM [sys].[database_permissions] [p] WITH (NOLOCK) WHERE
             [p].[major_id] = [db].[major_id]
            AND [p].[minor_id] = 0
            AND [p].[permission_name] = [db].[permission_name]
            AND [p].[state] = N'W'
            AND [db].[state] = N'G'
            AND [db].[minor_id] <> 0
            AND [p].[class] =  [db].[class]
            AND [p].[grantee_principal_id] = [db].[grantee_principal_id]
            AND [p].[grantor_principal_id] = [db].[grantor_principal_id])) AS [_results] WHERE [TypeId] = 1
 ORDER BY TypeId,GranteeId,GrantorId,MajorId,MinorId,PermissionCode,PermissionState ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql90PermissionStatementColumnPopulator' as [PopulatorName];
SELECT * FROM (
SELECT
        [db].[class] AS [TypeId],
        [so].[is_ms_shipped] AS [IsMsShipped],
        [db].[major_id] AS [MajorId],
        [db].[minor_id] AS [MinorId],
        [so].[type] AS [ObjectType],
        [db].[grantee_principal_id] AS [GranteeId],
        USER_NAME([db].[grantee_principal_id]) AS [GranteeName],
        [db].[grantor_principal_id] AS [GrantorId],
        CONVERT(BIT, 1) AS [ScriptGrantor],
        USER_NAME([db].[grantor_principal_id]) AS [GrantorName],
        [db].[type] AS [PermissionCode],
        [db].[permission_name] AS [PermissionType],
        [db].[state] AS [PermissionState],
        CASE [db].[state]
        WHEN N'R' THEN 0 ELSE 1 END AS [PermissionStateOrder],
        SCHEMA_NAME([so].[schema_id]) AS [Level0Name],
        [so].[name] AS [Level1Name],
        [sc].[name] AS [Level2Name],
        ISNULL((SELECT TOP 1 1 FROM [sys].[database_permissions] [p] WITH (NOLOCK) WHERE
             [p].[major_id] = [db].[major_id]
            AND [p].[minor_id] = 0
            AND [p].[permission_name] = [db].[permission_name]
            AND [p].[state] = N'W'
            AND [db].[state] = N'G'
            AND [db].[minor_id] <> 0
            AND [p].[class] =  [db].[class]
            AND [p].[grantee_principal_id] = [db].[grantee_principal_id]
            AND [p].[grantor_principal_id] = [db].[grantor_principal_id]), 0) AS [IsRevokeWithGrantOption]
FROM
        [sys].[database_permissions] [db] WITH (NOLOCK)
        INNER JOIN [sys].[objects] [so] WITH (NOLOCK) ON [so].[object_id] = [db].[major_id]
        INNER JOIN [sys].[columns] [sc] WITH (NOLOCK) ON [sc].[object_id] = [db].[major_id] AND [sc].[column_id] = [db].[minor_id]
        INNER JOIN [sys].[database_principals] [dp] WITH (NOLOCK) ON [dp].[principal_id] = [db].[grantor_principal_id]
        INNER JOIN [sys].[database_principals] [dp2] WITH (NOLOCK) ON [dp2].[principal_id] = [db].[grantee_principal_id] 
WHERE
        [db].[class] = 1
        AND [so].[object_id] NOT IN (SELECT [major_id] FROM [sys].[extended_properties] WITH (NOLOCK) WHERE [minor_id] = 0 AND [class] = 1 AND [name] = N'microsoft_database_tools_support')
        AND OBJECTPROPERTY([so].[object_id], N'IsSystemTable') = 0
        AND [db].[grantee_principal_id] <> [db].[grantor_principal_id]
        AND USER_NAME([db].[grantor_principal_id])  <> N'cdc'
		AND USER_NAME([db].[grantee_principal_id])  <> N'cdc'
        AND ([dp].[owning_principal_id] IS NULL OR USER_NAME([dp].[owning_principal_id]) <> N'cdc')
        AND ([dp2].[owning_principal_id] IS NULL OR USER_NAME([dp2].[owning_principal_id]) <> N'cdc')) AS [_results] WHERE [TypeId] = 1
 ORDER BY TypeId,GranteeId,GrantorId,MajorId,PermissionStateOrder,MinorId,PermissionCode,PermissionState ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.SqlSysCommentObjectDependencyPopulator' as [PopulatorName];
SELECT * FROM (SELECT
    CASE
        WHEN [d].[referencing_class] = 1 AND NOT [TableType].[user_type_id] IS NULL THEN
            [TableType].[user_type_id]
        ELSE
            [d].[referencing_id] 
        END AS [ReferencingId],
    [o].[type] AS [ReferencingType],
    CASE
        WHEN [d].[referencing_class] = 1 AND NOT [TableType].[user_type_id] IS NULL THEN
            [TableType].[name]
        ELSE [o].[name]
    END AS [ReferencingName],
    [c].[name] AS [ReferencingColumnName],
    CASE
        WHEN [d].[referencing_class] = 1 AND NOT [TableType].[user_type_id] IS NULL THEN
            SCHEMA_NAME([TableType].[schema_id])
        WHEN [d].[referencing_class] = 1 AND NOT [ParentTableType].[user_type_id] IS NULL THEN
            SCHEMA_NAME([ParentTableType].[schema_id])
        ELSE [o].[ReferencingSchemaName]
    END AS [ReferencingSchemaName],
    [d].[referencing_minor_id] AS [ReferencingMinorId],
    CASE 
        WHEN  NOT [ReferencedTableType].[user_type_id] IS NULL THEN [ReferencedTableType].[user_type_id]
        ELSE [d].[referenced_id] 
    END AS [ReferencedId],
    CASE
            WHEN [ReferencedTableType].[user_type_id] IS NULL AND [d].[referenced_schema_name] IS NOT NULL THEN [d].[referenced_schema_name]
            WHEN [d].[referenced_class] = 6 THEN SCHEMA_NAME([udt].[schema_id])
            WHEN d.referenced_class = 10 THEN SCHEMA_NAME([xsc].[schema_id])
            WHEN d.referenced_class = 21 THEN NULL
            WHEN NOT [ReferencedTableType].[user_type_id] IS NULL THEN SCHEMA_NAME([ReferencedTableType].[schema_id])
            WHEN [d].[is_caller_dependent] = 1 AND [d].[referenced_class] = 1 AND [d].[referenced_schema_name] IS NULL THEN (SELECT TOP 1 SCHEMA_NAME(schema_id)  FROM [sys].[objects] AS [o] WITH (NOLOCK) WHERE [o].[name] = [d].[referenced_entity_name])
            ELSE OBJECT_SCHEMA_NAME([d].[referenced_id])
    END AS [ReferencedSchemaName],
    CASE
        WHEN NOT [ReferencedTableType].[user_type_id] IS NULL THEN [ReferencedTableType].[name]
        ELSE [d].[referenced_entity_name] 
    END AS ReferencedName,
    [c2].[name] AS [ReferencedColumnName],
    [d].[referenced_minor_id] AS [ReferencedMinorId],
    CASE [t].[is_filetable] WHEN 1 THEN N'UF' ELSE [o2].[type] END AS [ReferencedType],
    [d].[referenced_class] AS [ReferencedClass],
    [d].[is_caller_dependent] AS [IsCallerDependent],
    [d].[is_ambiguous] AS [IsAmbiguous],
    [np].[procedure_number] AS [ProcedureNumber],
    CAST(ISNULL([parent].[object_id],  0) AS BIT) AS [ReferencingParentIsTableType],
    CASE 
        WHEN [d].[referencing_class]= 1 AND NOT [parent].[object_id] IS NULL THEN N'TT'
        ELSE [o].[type]
    END AS [FilterReferencingType],
    CASE 
        WHEN NOT [ParentTableType].[user_type_id] IS NULL THEN [ParentTableType].[name]
        WHEN [d].[referencing_class] = 1 AND NOT [TableType].[user_type_id] IS NULL THEN
            [TableType].[name]
        ELSE [o].[name]
    END AS [FilterReferencingName],
    CASE
        WHEN NOT [ParentTableType].[user_type_id] IS NULL THEN [ParentTableType].[type_table_object_id]
        ELSE [o].[parent_object_id] 
    END AS [ParentId],
    CASE 
        WHEN [ParentTableType].[user_type_id] IS NOT NULL THEN [ParentTableType].[schema_id]
        ELSE [referencingParent].[schema_id]
    END AS [ParentSchemaId],
    CASE
        WHEN [o].[type] = N'TT' THEN 0
        WHEN [ParentTableType].[user_type_id] IS NOT NULL THEN 0
        ELSE ISNULL([o].[is_ms_shipped], 0) 
    END AS [IsMsShipped]
    FROM [sys].[sql_expression_dependencies] AS [d] WITH (NOLOCK)
    LEFT JOIN ( 
        SELECT  SCHEMA_NAME([schema_id]) AS [ReferencingSchemaName], [name], [object_id] , 1 AS [parent_class] , [type], [parent_object_id], [is_ms_shipped] FROM [sys].[objects] WITH (NOLOCK)
        UNION
        SELECT [name] AS [ReferencingSchemaName], NULL AS [name], [object_id], [parent_class], N'DT',  0 AS [parent_object_id], [is_ms_shipped] FROM [sys].[triggers] WITH (NOLOCK) where [parent_class] = 0) 
        AS [o] ON [d].[referencing_class] in (1, 12) AND [o].[object_id] = [d].[referencing_id]
    LEFT JOIN [sys].[columns] AS [c] WITH (NOLOCK) ON [d].[referencing_class] = 1 AND [o].[object_id] = [c].[object_id] AND [d].[referencing_minor_id] = [c].[column_id]    
    LEFT JOIN [sys].[objects] AS [referencingParent] WITH (NOLOCK) ON [o].[parent_object_id] = [referencingParent].[object_id]
    LEFT JOIN [sys].[objects] AS [o2] WITH (NOLOCK) ON [d].[referenced_class] = 1 AND [o2].[object_id] = [d].[referenced_id]
    LEFT JOIN [sys].[columns] AS [c2] WITH (NOLOCK) ON [d].[referenced_class] = 1 AND [d].[referenced_id] = [c2].[object_id] AND [d].[referenced_minor_id] = [c2].[column_id]
    LEFT JOIN [sys].[types] as [udt] WITH (NOLOCK) ON [d].[referenced_class] = 6 AND [udt].[user_type_id] = [d].[referenced_id]
    LEFT JOIN [sys].[xml_schema_collections] AS [xsc] WITH (NOLOCK) ON  [d].[referenced_class] = 10 AND [xsc].[xml_collection_id] = [d].[referenced_id]
    LEFT JOIN [sys].[objects] as [parent] WITH (NOLOCK) ON [parent].[object_id] = [o].[parent_object_id] AND [parent].[type] = N'TT'
    LEFT JOIN [sys].[tables] AS [t] WITH (NOLOCK) ON [o2].[object_id] = [t].[object_id]
    LEFT JOIN [sys].[table_types] AS [ParentTableType] WITH (NOLOCK) ON [ParentTableType].[type_table_object_id] = [parent].[object_id]
    LEFT JOIN [sys].[table_types] AS [TableType] WITH (NOLOCK)ON [TableType].[type_table_object_id] = [o].[object_id]
    LEFT JOIN [sys].[table_types] AS [ReferencedTableType] WITH (NOLOCK) ON [ReferencedTableType].[type_table_object_id] = [o2].[object_id]
    LEFT JOIN [sys].[extended_properties] AS [exp] ON [exp].[major_id] = [o].[object_id] AND [exp].[minor_id] = 0 AND [exp].[class] = 1 AND [exp].[name] = N'microsoft_database_tools_support'
    LEFT JOIN
    (SELECT [object_id] , 1 AS [procedure_number] FROM [sys].[procedures] WITH (NOLOCK)
        union 
     SELECT [object_id], [procedure_number] FROM [sys].[numbered_procedures] WITH (NOLOCK)) AS [np] ON  [d].[referenced_id] = [np].[object_id] AND [d].[referenced_class] = 1 
    WHERE ([o].[ReferencingSchemaName] <> N'cdc' OR [o].[parent_class] <> 1) AND [o].[type] <> N'PG' AND [d].[referenced_database_name] IS NULL AND [d].[referenced_server_name] IS NULL AND [d].[is_ambiguous] = 0 AND  ([d].[is_caller_dependent] = 0 OR 1 = (SELECT COUNT(1) FROM [sys].[objects] WITH (NOLOCK) WHERE [name] = [d].[referenced_entity_name])) AND (([o].[is_ms_shipped] = 0 OR [o].[type] = 'TT' OR [ParentTableType].[user_type_id] IS NOT NULL) AND [exp].[major_id] IS NULL)) AS [_results] ORDER BY ReferencingSchemaName,ReferencingName,ReferencingColumnName ;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110ExtendedPropertyPopulator' as [PopulatorName];

SELECT
        [sp].[class] AS [TypeId],
        [sp].[major_id] AS [MajorId],
        [sp].[minor_id] AS [MinorId],
        [sp].[name] AS [PropertyName],
        [sp].[value] AS [PropertyValue],
        N'DT' AS [ObjectType],
        SQL_VARIANT_PROPERTY([sp].[value], N'BaseType') AS [BaseType],
        [t].[name] AS [Level0Name],
        NULL AS [Level1Name],
        NULL AS [Level2Name],
        CAST(0 AS BIT) AS [IsPartitionScheme],
        CAST(0 AS BIT) AS [IsDefault],
        CAST(NULL AS TINYINT) AS [IndexType]
FROM
        [sys].[extended_properties] [sp] WITH (NOLOCK)
        INNER JOIN [sys].[triggers] [t] WITH (NOLOCK) ON [t].[object_id] = [sp].[major_id]
        LEFT JOIN [sys].[all_objects] [parent] WITH (NOLOCK) ON [parent].[object_id] = [t].[parent_id]
WHERE
        [sp].[class] = 1
        AND [t].[parent_class] = 0
        AND [sp].[name] <> N'microsoft_database_tools_support'
        AND ([t].[parent_id] = 0 AND ([t].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [t].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )) OR [t].[parent_id] <> 0 AND ([parent].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [parent].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )))
;
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110ExtendedPropertyPopulator' as [PopulatorName];

SELECT
        [sp].[class] AS [TypeId],
        [sp].[major_id] AS [MajorId],
        [sp].[minor_id] AS [MinorId],
        [sp].[name] AS [PropertyName],
        [sp].[value] AS [PropertyValue],
        N'EN' AS [ObjectType],
        SQL_VARIANT_PROPERTY([sp].[value], N'BaseType') AS [BaseType],
        CASE [en].[parent_class]
            WHEN 1 THEN SCHEMA_NAME([q].[schema_id])
            ELSE [en].[name]
        END AS [Level0Name],
        CASE [en].[parent_class]
            WHEN 1 THEN [q].[name]
            ELSE NULL
        END AS [Level1Name],
        CASE [en].[parent_class]
            WHEN 1 THEN [en].[name]
            ELSE NULL
        END AS [Level2Name],
        CAST(0 AS BIT) AS [IsPartitionScheme],
        CAST(0 AS BIT) AS [IsDefault],
        CAST(NULL AS TINYINT) AS [IndexType]
FROM
        [sys].[extended_properties] [sp] WITH (NOLOCK)
        INNER JOIN [sys].[event_notifications] [en] WITH (NOLOCK) ON [en].[object_id] = [sp].[major_id]
        LEFT JOIN [sys].[service_queues] [q] WITH (NOLOCK) ON [en].[parent_id] = [q].[object_id] AND [en].[parent_class] = 1
WHERE
        [sp].[class] = 1
        AND [sp].[name] <> N'microsoft_database_tools_support';
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110ExtendedPropertyPopulator' as [PopulatorName];

SELECT
        [sp].[class] AS [TypeId],
        [sp].[major_id] AS [MajorId],
        [sp].[minor_id] AS [MinorId],
        [sp].[name] AS [PropertyName],
        [sp].[value] AS [PropertyValue],
        CASE [t].[is_filetable] WHEN 1 THEN N'UF' ELSE [so].[type] END AS [ObjectType],
        SQL_VARIANT_PROPERTY([sp].[value], N'BaseType') AS [BaseType],
        SCHEMA_NAME([so].[schema_id]) AS [Level0Name],
        [so].[name] AS [Level1Name],
        [sc].[name] AS [Level2Name],
        CAST(0 AS BIT) AS [IsPartitionScheme],
        CAST(OBJECTPROPERTY([so].[object_id], N'IsDefault') AS BIT) AS [IsDefault],
        CAST(NULL AS TINYINT) AS [IndexType],
        [parent].[object_id] AS [ParentId],
        [parent].[schema_id] AS [ParentSchemaId],
        ISNULL([parent].[is_ms_shipped], 0) AS [IsMsShipped]
FROM
        [sys].[extended_properties] [sp] WITH (NOLOCK)
        INNER JOIN [sys].[all_objects] [so] WITH (NOLOCK) ON [so].[object_id] = [sp].[major_id]
        LEFT JOIN [sys].[all_objects] [parent] WITH (NOLOCK) ON [so].[parent_object_id] = [parent].[object_id]
        LEFT JOIN [sys].[tables] [t] WITH (NOLOCK) ON [so].[object_id] = [t].[object_id]
        LEFT JOIN [sys].[all_columns] [sc] WITH (NOLOCK) ON [sc].[object_id] = [sp].[major_id] AND [sc].[column_id] = [sp].[minor_id]
        LEFT JOIN [sys].[filetable_system_defined_objects] AS [ftc] WITH (NOLOCK) ON [ftc].[object_id] = [so].[object_id]

WHERE
        [sp].[class] = 1 AND  SCHEMA_NAME([so].[schema_id]) IS NOT NULL 
        AND ([so].[type] <> N'PG' OR [so].[type] IS NULL)
        AND [sp].[name] <> N'microsoft_database_tools_support'
        AND ([ftc].[object_id] IS NULL OR [so].[type] IN (N'UQ',N'PK'))
        AND ([so].[parent_object_id] = 0 AND ([so].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [so].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )) OR [so].[parent_object_id] <> 0 AND ([parent].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [parent].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )))

    AND (OBJECTPROPERTY([so].[parent_object_id], N'TableIsFake') = 0 OR OBJECTPROPERTY([so].[parent_object_id], N'TableIsFake')  IS NULL);
SELECT N'Microsoft.Data.Tools.Schema.Sql.SchemaModel.ReverseEngineerPopulators.Sql110ExtendedPropertyPopulator' as [PopulatorName];

SELECT 
        [sp].[class] AS [TypeId],
        [sp].[major_id] AS [MajorId],
        [sp].[minor_id] AS [MinorId],
        [sp].[name] AS [PropertyName], 
        [sp].[value] AS [PropertyValue],
        [so].[type] AS [ObjectType],
        SQL_VARIANT_PROPERTY([sp].[value], N'BaseType') AS [BaseType],
        CASE [sp].[class]
            WHEN  0 THEN NULL
            WHEN  3 THEN SCHEMA_NAME([sp].[major_id])
            WHEN  4 THEN USER_NAME([sp].[major_id])
            WHEN  5 THEN (SELECT TOP 1 [sa2].[name]   FROM [sys].[assemblies] [sa2] WITH (NOLOCK) WHERE [sa2].[assembly_id] = [sp].[major_id])
            WHEN  6 THEN SCHEMA_NAME((SELECT TOP 1 [st].[schema_id] FROM [sys].[types] [st] WITH (NOLOCK) WHERE [st].[user_type_id] = [sp].[major_id]))
            WHEN  8 THEN SCHEMA_NAME([tt].[schema_id])
            WHEN 10 THEN SCHEMA_NAME((SELECT TOP 1 [sxsc2].[schema_id] FROM [sys].[xml_schema_collections] [sxsc2] WITH (NOLOCK) WHERE [sxsc2].[xml_collection_id] = [sp].[major_id]))
            WHEN 15 THEN (SELECT TOP 1 [smt2].[name]  FROM [sys].[service_message_types] [smt2] WITH (NOLOCK) WHERE [smt2].[message_type_id] = [sp].[major_id]) COLLATE database_default
            WHEN 16 THEN (SELECT TOP 1 [ssc2].[name]  FROM [sys].[service_contracts] [ssc2] WITH (NOLOCK) WHERE [ssc2].[service_contract_id] = [sp].[major_id])
            WHEN 17 THEN (SELECT TOP 1 [ss2].[name]   FROM [sys].[services] [ss2] WITH (NOLOCK) WHERE [ss2].[service_id] = [sp].[major_id])
            WHEN 18 THEN (SELECT TOP 1 [srs2].[name]  FROM [sys].[remote_service_bindings] [srs2] WITH (NOLOCK) WHERE [srs2].[remote_service_binding_id] = [sp].[major_id])
            WHEN 19 THEN (SELECT TOP 1 [sr2].[name]   FROM [sys].[routes] [sr2] WITH (NOLOCK) WHERE [sr2].[route_id] = [sp].[major_id])
            WHEN 20 THEN (SELECT TOP 1 [sd2].[name]  FROM [sys].[data_spaces] [sd2] WITH (NOLOCK) WHERE [sd2].[data_space_id] = [sp].[major_id])
            WHEN 21 THEN (SELECT TOP 1 [spf2].[name]  FROM [sys].[partition_functions] [spf2] WITH (NOLOCK) WHERE [spf2].[function_id] = [sp].[major_id])
            WHEN 22 THEN (SELECT TOP 1 [sdf2].[name]  FROM [sys].[database_files] [sdf2] WITH (NOLOCK) WHERE [sdf2].[file_id] = [sp].[major_id])
            ELSE SCHEMA_NAME([so].[schema_id])
        END AS [Level0Name],
        CASE [sp].[class] 
            WHEN 3 THEN NULL
            WHEN 4 THEN NULL
            WHEN 6  THEN (SELECT TOP 1 [st2].[name] FROM [sys].[types] [st2] WITH (NOLOCK) WHERE [st2].[user_type_id] = [sp].[major_id])
            WHEN 8  THEN [tt].[name]
            WHEN 10 THEN (SELECT TOP 1 [sx2].[name] FROM [sys].[xml_schema_collections] [sx2] WITH (NOLOCK) WHERE [sx2].[xml_collection_id] = [sp].[major_id])
            WHEN 22 THEN NULL
            ELSE [so].[name]
        END AS [Level1Name],
        CASE [sp].[class]			
            WHEN 2 THEN (SELECT TOP 1 [spa].[name] FROM [sys].[parameters] [spa] WITH (NOLOCK) WHERE [spa].[object_id] = [sp].[major_id] AND [spa].[parameter_id] = [sp].[minor_id])
            WHEN 7 THEN (SELECT TOP 1 [si].[name] FROM [sys].[indexes] [si] WITH (NOLOCK) WHERE [si].[object_id] = [sp].[major_id] AND [si].[index_id] = [sp].[minor_id])
            WHEN 8 THEN COL_NAME([tt].[type_table_object_id] ,[sp].[minor_id])
            ELSE NULL
        END AS [Level2Name],
        CASE 
            WHEN [sp].[class] = 20 AND EXISTS((SELECT TOP 1 [sd2].[name]  FROM [sys].[data_spaces] [sd2] WITH (NOLOCK) WHERE [sd2].[data_space_id] = [sp].[major_id] AND [sd2].[type] = N'PS')) THEN
                CAST(1 AS BIT)
            ELSE CAST(0 AS BIT)
        END AS IsPartitionScheme,
        CAST(0 AS BIT) AS [IsDefault],
        [i].[type] AS [IndexType],
        CASE [sp].[class]
            WHEN 6 THEN [tt].[type_table_object_id]
            WHEN 8 THEN [tt].[type_table_object_id]
            WHEN 1 THEN [parent].[object_id]
            ELSE NULL
        END AS [ParentId],
         CASE [sp].[class]
            WHEN 6 THEN [tt].[schema_id]
            WHEN 8 THEN [tt].[schema_id]
            WHEN 1 THEN [parent].[schema_id]
            ELSE NULL
        END AS [ParentSchemaId],
        [sxi].[xml_index_type] AS [XMLIndexType],
        ISNULL([parent].[is_ms_shipped], 0) AS [IsMsShipped]
FROM    [sys].[extended_properties] [sp] WITH (NOLOCK) 
        LEFT JOIN [sys].[all_objects] [so] WITH (NOLOCK) ON [so].[object_id] = [sp].[major_id] AND [sp].[class] IN (1, 2, 7)
        LEFT JOIN [sys].[all_objects] [parent] WITH(NOLOCK) ON [so].[parent_object_id] = [parent].[object_id]
        LEFT JOIN [sys].[indexes] [i] WITH (NOLOCk) ON [sp].[class] = 7 AND [so].[object_id] = [i].[object_id]  AND [sp].[minor_id] = [i].[index_id]
        LEFT JOIN [sys].[table_types] [tt] WITH (NOLOCK) ON [sp].[class] IN (6,8) AND [tt].[user_type_id] = [sp].[major_id]
        LEFT JOIN [sys].[xml_indexes] [sxi] WITH (NOLOCk) ON [sp].[class] = 7 AND [so].[object_id] = [sxi].[object_id]  AND [sp].[minor_id] = [sxi].[index_id]
WHERE 
        [sp].[class] <> 1
        AND (OBJECTPROPERTY([so].[parent_object_id], N'TableIsFake') = 0 OR OBJECTPROPERTY([so].[parent_object_id], N'TableIsFake')  IS NULL)
        AND [sp].[class] <> 27
        AND ([so].[type] <> N'PG' OR [so].[type] IS NULL)
        AND [sp].[name] <> N'microsoft_database_tools_support'
        AND ([i].[is_primary_key] IS NULL OR [i].[is_primary_key] = 0)
        AND ([sp].[class] <> 7 OR [i].[object_id] IS NOT NULL)
        AND ([sp].[class] <> 7 OR ([so].[is_ms_shipped] = 0 AND NOT EXISTS (SELECT *
                                        FROM [sys].[extended_properties]
                                        WHERE     [major_id] = [so].[object_id]
                                              AND [minor_id] = 0
                                              AND [class] = 1
                                              AND [name] = N'microsoft_database_tools_support'
                                       )))
;

";
    }

    
}
