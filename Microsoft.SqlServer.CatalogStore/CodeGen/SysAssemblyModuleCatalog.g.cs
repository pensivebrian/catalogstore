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
    
    
    [System.ComponentModel.DataAnnotations.Schema.TableAttribute("sys_assembly_modules")]
    public partial class SysAssemblyModuleCatalog
    {
        
        private System.Nullable<int> _object_id;
        
        private System.Nullable<int> _assembly_id;
        
        private string _assembly_class;
        
        private string _assembly_method;
        
        private System.Nullable<bool> _null_on_null_input;
        
        private System.Nullable<int> _execute_as_principal_id;
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("object_id")]
        [System.ComponentModel.DataAnnotations.KeyAttribute()]
        public System.Nullable<int> ObjectId
        {
            get
            {
                return this._object_id;
            }
            set
            {
                this._object_id = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("assembly_id")]
        public System.Nullable<int> AssemblyId
        {
            get
            {
                return this._assembly_id;
            }
            set
            {
                this._assembly_id = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("assembly_class")]
        public string AssemblyClass
        {
            get
            {
                return this._assembly_class;
            }
            set
            {
                this._assembly_class = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("assembly_method")]
        public string AssemblyMethod
        {
            get
            {
                return this._assembly_method;
            }
            set
            {
                this._assembly_method = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("null_on_null_input")]
        public System.Nullable<bool> NullOnNullInput
        {
            get
            {
                return this._null_on_null_input;
            }
            set
            {
                this._null_on_null_input = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("execute_as_principal_id")]
        public System.Nullable<int> ExecuteAsPrincipalId
        {
            get
            {
                return this._execute_as_principal_id;
            }
            set
            {
                this._execute_as_principal_id = value;
            }
        }
    }
}
