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
    
    
    [System.ComponentModel.DataAnnotations.Schema.TableAttribute("sys_destination_data_spaces")]
    public partial class SysDestinationDatumSpaceCatalog
    {
        
        private System.Nullable<int> _partition_scheme_id;
        
        private System.Nullable<int> _destination_id;
        
        private System.Nullable<int> _data_space_id;
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("partition_scheme_id")]
        [System.ComponentModel.DataAnnotations.KeyAttribute()]
        public System.Nullable<int> PartitionSchemeId
        {
            get
            {
                return this._partition_scheme_id;
            }
            set
            {
                this._partition_scheme_id = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("destination_id")]
        public System.Nullable<int> DestinationId
        {
            get
            {
                return this._destination_id;
            }
            set
            {
                this._destination_id = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("data_space_id")]
        public System.Nullable<int> DataSpaceId
        {
            get
            {
                return this._data_space_id;
            }
            set
            {
                this._data_space_id = value;
            }
        }
    }
}
