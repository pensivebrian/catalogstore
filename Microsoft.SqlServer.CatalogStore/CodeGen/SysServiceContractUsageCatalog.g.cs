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
    
    
    [System.ComponentModel.DataAnnotations.Schema.TableAttribute("sys_service_contract_usages")]
    public partial class SysServiceContractUsageCatalog
    {
        
        private System.Nullable<int> _service_id;
        
        private System.Nullable<int> _service_contract_id;
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("service_id")]
        [System.ComponentModel.DataAnnotations.KeyAttribute()]
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
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("service_contract_id")]
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
    }
}
