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
    
    
    [System.ComponentModel.DataAnnotations.Schema.TableAttribute("sys_service_contract_message_usages")]
    public partial class SysServiceContractMessageUsageCatalog
    {
        
        private System.Nullable<int> _service_contract_id;
        
        private System.Nullable<int> _message_type_id;
        
        private System.Nullable<bool> _is_sent_by_initiator;
        
        private System.Nullable<bool> _is_sent_by_target;
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("service_contract_id")]
        [System.ComponentModel.DataAnnotations.KeyAttribute()]
        public System.Nullable<int> ServiceContractId
        {
            get
            {
                return this._service_contract_id;
            }
            set
            {
                this._service_contract_id = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("message_type_id")]
        public System.Nullable<int> MessageTypeId
        {
            get
            {
                return this._message_type_id;
            }
            set
            {
                this._message_type_id = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_sent_by_initiator")]
        public System.Nullable<bool> IsSentByInitiator
        {
            get
            {
                return this._is_sent_by_initiator;
            }
            set
            {
                this._is_sent_by_initiator = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_sent_by_target")]
        public System.Nullable<bool> IsSentByTarget
        {
            get
            {
                return this._is_sent_by_target;
            }
            set
            {
                this._is_sent_by_target = value;
            }
        }
    }
}
