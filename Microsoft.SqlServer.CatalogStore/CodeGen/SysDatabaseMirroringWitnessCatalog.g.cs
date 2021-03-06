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
    
    
    [System.ComponentModel.DataAnnotations.Schema.TableAttribute("sys_database_mirroring_witnesses")]
    public partial class SysDatabaseMirroringWitnessCatalog
    {
        
        private string _database_name;
        
        private string _principal_server_name;
        
        private string _mirror_server_name;
        
        private System.Nullable<byte> _safety_level;
        
        private string _safety_level_desc;
        
        private System.Nullable<int> _safety_sequence_number;
        
        private System.Nullable<int> _role_sequence_number;
        
        private System.Nullable<System.Guid> _mirroring_guid;
        
        private System.Nullable<System.Guid> _family_guid;
        
        private System.Nullable<bool> _is_suspended;
        
        private System.Nullable<int> _is_suspended_sequence_number;
        
        private System.Nullable<byte> _partner_sync_state;
        
        private string _partner_sync_state_desc;
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("database_name")]
        [System.ComponentModel.DataAnnotations.KeyAttribute()]
        public string DatabaseName
        {
            get
            {
                return this._database_name;
            }
            set
            {
                this._database_name = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("principal_server_name")]
        public string PrincipalServerName
        {
            get
            {
                return this._principal_server_name;
            }
            set
            {
                this._principal_server_name = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("mirror_server_name")]
        public string MirrorServerName
        {
            get
            {
                return this._mirror_server_name;
            }
            set
            {
                this._mirror_server_name = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("safety_level")]
        public System.Nullable<byte> SafetyLevel
        {
            get
            {
                return this._safety_level;
            }
            set
            {
                this._safety_level = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("safety_level_desc")]
        public string SafetyLevelDesc
        {
            get
            {
                return this._safety_level_desc;
            }
            set
            {
                this._safety_level_desc = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("safety_sequence_number")]
        public System.Nullable<int> SafetySequenceNumber
        {
            get
            {
                return this._safety_sequence_number;
            }
            set
            {
                this._safety_sequence_number = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("role_sequence_number")]
        public System.Nullable<int> RoleSequenceNumber
        {
            get
            {
                return this._role_sequence_number;
            }
            set
            {
                this._role_sequence_number = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("mirroring_guid")]
        public System.Nullable<System.Guid> MirroringGuid
        {
            get
            {
                return this._mirroring_guid;
            }
            set
            {
                this._mirroring_guid = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("family_guid")]
        public System.Nullable<System.Guid> FamilyGuid
        {
            get
            {
                return this._family_guid;
            }
            set
            {
                this._family_guid = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_suspended")]
        public System.Nullable<bool> IsSuspended
        {
            get
            {
                return this._is_suspended;
            }
            set
            {
                this._is_suspended = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_suspended_sequence_number")]
        public System.Nullable<int> IsSuspendedSequenceNumber
        {
            get
            {
                return this._is_suspended_sequence_number;
            }
            set
            {
                this._is_suspended_sequence_number = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("partner_sync_state")]
        public System.Nullable<byte> PartnerSyncState
        {
            get
            {
                return this._partner_sync_state;
            }
            set
            {
                this._partner_sync_state = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("partner_sync_state_desc")]
        public string PartnerSyncStateDesc
        {
            get
            {
                return this._partner_sync_state_desc;
            }
            set
            {
                this._partner_sync_state_desc = value;
            }
        }
    }
}
