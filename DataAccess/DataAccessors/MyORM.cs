namespace Persons.DataAccessors
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlServerCe;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    using Persons.Attributes;
    using Persons.DataAccessors.DBUtils;
    using Persons.Domain;

    public class MyORM<T> : IDataAccessor<T> where T : EntityBase
    {
        public IList<T> GetAll()
        {
            var tableName = GetTableName<T>();
            string queryString = String.Format("SELECT * from {0}", tableName);
            var data = new List<T>();
          
            using (var command = new Command(queryString))
            {
                SqlCeDataReader reader = command.SqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    var entity = reader.GetEntity<T>(GetAllFieldAttributes<T>());
                    if (entity == null)
                    {
                        break;
                    }

                    data.Add(entity);
                }

                reader.Close();
            }

            return data;
        }

        public T GetById(int id)
        {
            var tableName = GetTableName<T>();
            string queryString = String.Format(
                "SELECT * from {0} where {1} = @id", tableName, GetIdentityFieldName<T>());

            T entity = null;
            using (var command = new Command(queryString))
            {           
                command.SqlCommand.Parameters.AddWithValue("@id", id);
                SqlCeDataReader reader = command.SqlCommand.ExecuteReader();

                if (reader.Read())
                {
                    entity = reader.GetEntity<T>(GetAllFieldAttributes<T>());
                }

                reader.Close();
            }

            return entity;
        }

        public void DeleteById(int id)
        {
            var tableName = GetTableName<T>();
            string queryString = String.Format("DELETE from {0} where {1} = @id", tableName, GetIdentityFieldName<T>());

            using (var command = new Command(queryString))
            {
                command.SqlCommand.Parameters.AddWithValue("@id", id);
                command.SqlCommand.ExecuteNonQuery();
            }
        }

        public void Insert(T entity)
        {
            var tableName = GetTableName<T>();
            var fieldString = new StringBuilder("(");
            var paramsNameString = new StringBuilder("(");
            var attrs = GetAllFieldAttributes<T>();
            attrs.Remove(attrs.Single(x => x.Value.GetType() == typeof(IdentityAttribute)));
            var parameters = new Dictionary<string, object>();
            foreach (var fieldAttribute in attrs)
            {
                var fieldName = fieldAttribute.Value.Name;
                fieldString.Append(fieldName);
                parameters.Add(
                    String.Format("@{0}", fieldName), fieldAttribute.Key.GetValue(entity));
                paramsNameString.Append(String.Format("@{0}", fieldName));

                if (!attrs.ElementAt(attrs.Count - 1).Equals(fieldAttribute))
                {
                    fieldString.Append(", ");
                    paramsNameString.Append(", ");
                }
            }

            fieldString.Append(")");
            paramsNameString.Append(")");

            string queryString = String.Format("INSERT INTO {0} {1} VALUES {2}", tableName, fieldString, paramsNameString);

            using (var command = new Command(queryString))
            {
                foreach (var parameter in parameters)
                {
                    command.SqlCommand.Parameters.AddWithValue(parameter.Key, parameter.Value); 
                }
               
                command.SqlCommand.ExecuteNonQuery();
            }
        }

        private static string GetTableName<T>() where T : EntityBase
        {
            return ((TableAttribute)typeof(T).GetCustomAttribute(typeof(TableAttribute))).Name;
        }

        private static IDictionary<PropertyInfo, FieldAttribute> GetAllFieldAttributes<T>()
        {
            return
                typeof(T).GetProperties()
                         .Where(property => (FieldAttribute)GetFieldAttribute<T>(property.Name) != null)
                         .ToDictionary(
                             property => property, property => (FieldAttribute)GetFieldAttribute<T>(property.Name));
        }

        private static string GetIdentityFieldName<T>()
        {
            var property = typeof(T).GetProperties()
                                    .Single(p => p.GetCustomAttribute(typeof(IdentityAttribute)) != null);
            return ((IdentityAttribute)GetFieldAttribute<T>(property.Name)).Name;
        }

        private static Attribute GetFieldAttribute<T>(string propertyName)
        {
            return typeof(T).GetProperty(propertyName).GetCustomAttribute(typeof(FieldAttribute));
        }
    }
}
