using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogStoreCodeGenerator
{
    public class CatalogObject
    {
        public string Name { get; set; }

        public static List<CatalogObject> Load(string connectionString)
        {
            List<CatalogObject> catalogObjects = new List<CatalogObject>();

            string sql = @"
select sys.all_objects.name from sys.all_objects
join sys.schemas on sys.all_objects.schema_id = sys.schemas.schema_id
where sys.schemas.name = 'sys' and sys.all_objects.type = 'V'
order by sys.all_objects.name";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = sql;
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string catalogObjectName = "sys." + reader.GetString(0);
                        catalogObjects.Add(new CatalogObject { Name = catalogObjectName });
                    }
                }
            }

            return catalogObjects;
        }
    }
}
