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
    
    
    [System.ComponentModel.DataAnnotations.Schema.TableAttribute("sys_filetable_system_defined_objects")]
    public partial class SysFiletableSystemDefinedObjectCatalog
    {
        
        private System.Nullable<int> _object_id;
        
        private System.Nullable<int> _parent_object_id;
        
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
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("parent_object_id")]
        public System.Nullable<int> ParentObjectId
        {
            get
            {
                return this._parent_object_id;
            }
            set
            {
                this._parent_object_id = value;
            }
        }
    }
}
