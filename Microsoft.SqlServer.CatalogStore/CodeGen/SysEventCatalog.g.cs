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
    
    
    [System.ComponentModel.DataAnnotations.Schema.TableAttribute("sys_events")]
    public partial class SysEventCatalog
    {
        
        private System.Nullable<int> _object_id;
        
        private System.Nullable<int> _type;
        
        private string _type_desc;
        
        private System.Nullable<bool> _is_trigger_event;
        
        private System.Nullable<int> _event_group_type;
        
        private string _event_group_type_desc;
        
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
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("type")]
        public System.Nullable<int> Type
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
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_trigger_event")]
        public System.Nullable<bool> IsTriggerEvent
        {
            get
            {
                return this._is_trigger_event;
            }
            set
            {
                this._is_trigger_event = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("event_group_type")]
        public System.Nullable<int> EventGroupType
        {
            get
            {
                return this._event_group_type;
            }
            set
            {
                this._event_group_type = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("event_group_type_desc")]
        public string EventGroupTypeDesc
        {
            get
            {
                return this._event_group_type_desc;
            }
            set
            {
                this._event_group_type_desc = value;
            }
        }
    }
}
