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
    
    
    [System.ComponentModel.DataAnnotations.Schema.TableAttribute("sys_event_notifications")]
    public partial class SysEventNotificationCatalog
    {
        
        private string _name;
        
        private System.Nullable<int> _object_id;
        
        private System.Nullable<byte> _parent_class;
        
        private string _parent_class_desc;
        
        private System.Nullable<int> _parent_id;
        
        private System.Nullable<System.DateTime> _create_date;
        
        private System.Nullable<System.DateTime> _modify_date;
        
        private string _service_name;
        
        private string _broker_instance;
        
        private byte[] _creator_sid;
        
        private System.Nullable<int> _principal_id;
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("name")]
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
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("parent_class")]
        public System.Nullable<byte> ParentClass
        {
            get
            {
                return this._parent_class;
            }
            set
            {
                this._parent_class = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("parent_class_desc")]
        public string ParentClassDesc
        {
            get
            {
                return this._parent_class_desc;
            }
            set
            {
                this._parent_class_desc = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("parent_id")]
        public System.Nullable<int> ParentId
        {
            get
            {
                return this._parent_id;
            }
            set
            {
                this._parent_id = value;
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
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("service_name")]
        public string ServiceName
        {
            get
            {
                return this._service_name;
            }
            set
            {
                this._service_name = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("broker_instance")]
        public string BrokerInstance
        {
            get
            {
                return this._broker_instance;
            }
            set
            {
                this._broker_instance = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("creator_sid")]
        public byte[] CreatorSid
        {
            get
            {
                return this._creator_sid;
            }
            set
            {
                this._creator_sid = value;
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
    }
}
