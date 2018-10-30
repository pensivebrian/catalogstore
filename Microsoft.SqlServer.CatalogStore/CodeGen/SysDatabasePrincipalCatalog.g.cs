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
    
    
    [System.ComponentModel.DataAnnotations.Schema.TableAttribute("sys_database_principals")]
    public partial class SysDatabasePrincipalCatalog
    {
        
        private string _name;
        
        private System.Nullable<int> _principal_id;
        
        private string _type;
        
        private string _type_desc;
        
        private string _default_schema_name;
        
        private System.Nullable<System.DateTime> _create_date;
        
        private System.Nullable<System.DateTime> _modify_date;
        
        private System.Nullable<int> _owning_principal_id;
        
        private byte[] _sid;
        
        private System.Nullable<bool> _is_fixed_role;
        
        private System.Nullable<int> _authentication_type;
        
        private string _authentication_type_desc;
        
        private string _default_language_name;
        
        private System.Nullable<int> _default_language_lcid;
        
        private System.Nullable<bool> _allow_encrypted_value_modifications;
        
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
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("type")]
        public string Type
        {
            get
            {
                return this._type;
            }
            set
            {
                this._type = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("type_desc")]
        public string TypeDesc
        {
            get
            {
                return this._type_desc;
            }
            set
            {
                this._type_desc = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("default_schema_name")]
        public string DefaultSchemaName
        {
            get
            {
                return this._default_schema_name;
            }
            set
            {
                this._default_schema_name = value;
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
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("owning_principal_id")]
        public System.Nullable<int> OwningPrincipalId
        {
            get
            {
                return this._owning_principal_id;
            }
            set
            {
                this._owning_principal_id = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("sid")]
        public byte[] Sid
        {
            get
            {
                return this._sid;
            }
            set
            {
                this._sid = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_fixed_role")]
        public System.Nullable<bool> IsFixedRole
        {
            get
            {
                return this._is_fixed_role;
            }
            set
            {
                this._is_fixed_role = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("authentication_type")]
        public System.Nullable<int> AuthenticationType
        {
            get
            {
                return this._authentication_type;
            }
            set
            {
                this._authentication_type = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("authentication_type_desc")]
        public string AuthenticationTypeDesc
        {
            get
            {
                return this._authentication_type_desc;
            }
            set
            {
                this._authentication_type_desc = value;
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
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("default_language_lcid")]
        public System.Nullable<int> DefaultLanguageLcid
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
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("allow_encrypted_value_modifications")]
        public System.Nullable<bool> AllowEncryptedValueModifications
        {
            get
            {
                return this._allow_encrypted_value_modifications;
            }
            set
            {
                this._allow_encrypted_value_modifications = value;
            }
        }
    }
}