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
    
    
    [System.ComponentModel.DataAnnotations.Schema.TableAttribute("sys_extended_properties")]
    public partial class SysExtendedPropertyCatalog
    {
        
        private System.Nullable<byte> _class;
        
        private string _class_desc;
        
        private System.Nullable<int> _major_id;
        
        private System.Nullable<int> _minor_id;
        
        private string _name;
        
        private object _value;
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("class")]
        public System.Nullable<byte> Class
        {
            get
            {
                return this._class;
            }
            set
            {
                this._class = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("class_desc")]
        public string ClassDesc
        {
            get
            {
                return this._class_desc;
            }
            set
            {
                this._class_desc = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("major_id")]
        public System.Nullable<int> MajorId
        {
            get
            {
                return this._major_id;
            }
            set
            {
                this._major_id = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("minor_id")]
        public System.Nullable<int> MinorId
        {
            get
            {
                return this._minor_id;
            }
            set
            {
                this._minor_id = value;
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
    }
}
