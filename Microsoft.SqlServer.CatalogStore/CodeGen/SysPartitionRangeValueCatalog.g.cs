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
    
    
    [System.ComponentModel.DataAnnotations.Schema.TableAttribute("sys_partition_range_values")]
    public partial class SysPartitionRangeValueCatalog
    {
        
        private System.Nullable<int> _function_id;
        
        private System.Nullable<int> _boundary_id;
        
        private System.Nullable<int> _parameter_id;
        
        private object _value;
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("function_id")]
        [System.ComponentModel.DataAnnotations.KeyAttribute()]
        public System.Nullable<int> FunctionId
        {
            get
            {
                return this._function_id;
            }
            set
            {
                this._function_id = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("boundary_id")]
        public System.Nullable<int> BoundaryId
        {
            get
            {
                return this._boundary_id;
            }
            set
            {
                this._boundary_id = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("parameter_id")]
        public System.Nullable<int> ParameterId
        {
            get
            {
                return this._parameter_id;
            }
            set
            {
                this._parameter_id = value;
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
