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
    
    
    [System.ComponentModel.DataAnnotations.Schema.TableAttribute("sys_dm_database_encryption_keys")]
    public partial class SysDmDatabaseEncryptionKeyCatalog
    {
        
        private System.Nullable<int> _database_id;
        
        private System.Nullable<int> _encryption_state;
        
        private System.Nullable<System.DateTime> _create_date;
        
        private System.Nullable<System.DateTime> _regenerate_date;
        
        private System.Nullable<System.DateTime> _modify_date;
        
        private System.Nullable<System.DateTime> _set_date;
        
        private System.Nullable<System.DateTime> _opened_date;
        
        private string _key_algorithm;
        
        private System.Nullable<int> _key_length;
        
        private byte[] _encryptor_thumbprint;
        
        private string _encryptor_type;
        
        private System.Nullable<float> _percent_complete;
        
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
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("encryption_state")]
        public System.Nullable<int> EncryptionState
        {
            get
            {
                return this._encryption_state;
            }
            set
            {
                this._encryption_state = value;
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
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("regenerate_date")]
        public System.Nullable<System.DateTime> RegenerateDate
        {
            get
            {
                return this._regenerate_date;
            }
            set
            {
                this._regenerate_date = value;
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
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("set_date")]
        public System.Nullable<System.DateTime> SetDate
        {
            get
            {
                return this._set_date;
            }
            set
            {
                this._set_date = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("opened_date")]
        public System.Nullable<System.DateTime> OpenedDate
        {
            get
            {
                return this._opened_date;
            }
            set
            {
                this._opened_date = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("key_algorithm")]
        public string KeyAlgorithm
        {
            get
            {
                return this._key_algorithm;
            }
            set
            {
                this._key_algorithm = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("key_length")]
        public System.Nullable<int> KeyLength
        {
            get
            {
                return this._key_length;
            }
            set
            {
                this._key_length = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("encryptor_thumbprint")]
        public byte[] EncryptorThumbprint
        {
            get
            {
                return this._encryptor_thumbprint;
            }
            set
            {
                this._encryptor_thumbprint = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("encryptor_type")]
        public string EncryptorType
        {
            get
            {
                return this._encryptor_type;
            }
            set
            {
                this._encryptor_type = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("percent_complete")]
        public System.Nullable<float> PercentComplete
        {
            get
            {
                return this._percent_complete;
            }
            set
            {
                this._percent_complete = value;
            }
        }
    }
}
