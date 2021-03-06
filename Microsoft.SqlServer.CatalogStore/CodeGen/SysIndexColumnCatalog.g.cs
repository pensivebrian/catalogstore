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
    
    
    [System.ComponentModel.DataAnnotations.Schema.TableAttribute("sys_index_columns")]
    public partial class SysIndexColumnCatalog
    {
        
        private System.Nullable<int> _object_id;
        
        private System.Nullable<int> _index_id;
        
        private System.Nullable<int> _index_column_id;
        
        private System.Nullable<int> _column_id;
        
        private System.Nullable<byte> _key_ordinal;
        
        private System.Nullable<byte> _partition_ordinal;
        
        private System.Nullable<bool> _is_descending_key;
        
        private System.Nullable<bool> _is_included_column;
        
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
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("index_id")]
        public System.Nullable<int> IndexId
        {
            get
            {
                return this._index_id;
            }
            set
            {
                this._index_id = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("index_column_id")]
        public System.Nullable<int> IndexColumnId
        {
            get
            {
                return this._index_column_id;
            }
            set
            {
                this._index_column_id = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("column_id")]
        public System.Nullable<int> ColumnId
        {
            get
            {
                return this._column_id;
            }
            set
            {
                this._column_id = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("key_ordinal")]
        public System.Nullable<byte> KeyOrdinal
        {
            get
            {
                return this._key_ordinal;
            }
            set
            {
                this._key_ordinal = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("partition_ordinal")]
        public System.Nullable<byte> PartitionOrdinal
        {
            get
            {
                return this._partition_ordinal;
            }
            set
            {
                this._partition_ordinal = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_descending_key")]
        public System.Nullable<bool> IsDescendingKey
        {
            get
            {
                return this._is_descending_key;
            }
            set
            {
                this._is_descending_key = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_included_column")]
        public System.Nullable<bool> IsIncludedColumn
        {
            get
            {
                return this._is_included_column;
            }
            set
            {
                this._is_included_column = value;
            }
        }
    }
}
