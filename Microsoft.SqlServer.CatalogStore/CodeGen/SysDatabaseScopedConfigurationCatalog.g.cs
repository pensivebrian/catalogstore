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
    
    
    [System.ComponentModel.DataAnnotations.Schema.TableAttribute("sys_database_scoped_configurations")]
    public partial class SysDatabaseScopedConfigurationCatalog
    {
        
        private System.Nullable<int> _configuration_id;
        
        private string _name;
        
        private object _value;
        
        private object _value_for_secondary;
        
        private System.Nullable<bool> _is_value_default;
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("configuration_id")]
        public System.Nullable<int> ConfigurationId
        {
            get
            {
                return this._configuration_id;
            }
            set
            {
                this._configuration_id = value;
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
        
        [System.ComponentModel.DataAnnotations.Schema.NotMappedAttribute()]
        public object Value
        {
            get
            {
                return this._value;
            }
            set
            {
                this._value = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.NotMappedAttribute()]
        public object ValueForSecondary
        {
            get
            {
                return this._value_for_secondary;
            }
            set
            {
                this._value_for_secondary = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_value_default")]
        public System.Nullable<bool> IsValueDefault
        {
            get
            {
                return this._is_value_default;
            }
            set
            {
                this._is_value_default = value;
            }
        }
    }
}
