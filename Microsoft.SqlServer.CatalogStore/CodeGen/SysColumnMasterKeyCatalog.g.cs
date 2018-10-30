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
    
    
    [System.ComponentModel.DataAnnotations.Schema.TableAttribute("sys_column_master_keys")]
    public partial class SysColumnMasterKeyCatalog
    {
        
        private string _name;
        
        private System.Nullable<int> _column_master_key_id;
        
        private System.Nullable<System.DateTime> _create_date;
        
        private System.Nullable<System.DateTime> _modify_date;
        
        private string _key_store_provider_name;
        
        private string _key_path;
        
        private System.Nullable<int> _allow_enclave_computations;
        
        private byte[] _signature;
        
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
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("column_master_key_id")]
        public System.Nullable<int> ColumnMasterKeyId
        {
            get
            {
                return this._column_master_key_id;
            }
            set
            {
                this._column_master_key_id = value;
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
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("key_store_provider_name")]
        public string KeyStoreProviderName
        {
            get
            {
                return this._key_store_provider_name;
            }
            set
            {
                this._key_store_provider_name = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("key_path")]
        public string KeyPath
        {
            get
            {
                return this._key_path;
            }
            set
            {
                this._key_path = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("allow_enclave_computations")]
        public System.Nullable<int> AllowEnclaveComputations
        {
            get
            {
                return this._allow_enclave_computations;
            }
            set
            {
                this._allow_enclave_computations = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("signature")]
        public byte[] Signature
        {
            get
            {
                return this._signature;
            }
            set
            {
                this._signature = value;
            }
        }
    }
}