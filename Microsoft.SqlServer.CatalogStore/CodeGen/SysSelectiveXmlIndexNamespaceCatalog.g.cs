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
    
    
    [System.ComponentModel.DataAnnotations.Schema.TableAttribute("sys_selective_xml_index_namespaces")]
    public partial class SysSelectiveXmlIndexNamespaceCatalog
    {
        
        private System.Nullable<int> _object_id;
        
        private System.Nullable<int> _index_id;
        
        private System.Nullable<bool> _is_default_uri;
        
        private string _uri;
        
        private string _prefix;
        
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
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("is_default_uri")]
        public System.Nullable<bool> IsDefaultUri
        {
            get
            {
                return this._is_default_uri;
            }
            set
            {
                this._is_default_uri = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("uri")]
        public string Uri
        {
            get
            {
                return this._uri;
            }
            set
            {
                this._uri = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.Schema.ColumnAttribute("prefix")]
        public string Prefix
        {
            get
            {
                return this._prefix;
            }
            set
            {
                this._prefix = value;
            }
        }
    }
}
