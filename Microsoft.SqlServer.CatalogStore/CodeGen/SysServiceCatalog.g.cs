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
    
    
    [System.ComponentModel.DataAnnotations.Schema.TableAttribute("sys_services")]
    public partial class SysServiceCatalog
    {
        
        private string _name;
        
        private System.Nullable<int> _service_id;
        
        private System.Nullable<int> _principal_id;
        
        private System.Nullable<int> _service_queue_id;
        
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
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("service_id")]
        public System.Nullable<int> ServiceId
        {
            get
            {
                return this._service_id;
            }
            set
            {
                this._service_id = value;
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
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("service_queue_id")]
        public System.Nullable<int> ServiceQueueId
        {
            get
            {
                return this._service_queue_id;
            }
            set
            {
                this._service_queue_id = value;
            }
        }
    }
}
