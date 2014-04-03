using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persons
{
    using System.Data.SqlServerCe;
    using System.Reflection;

    public static class IMapExtensions
    {
        public static string GetTableName<T>() where T : IMap
        {
            return
                typeof(T).CustomAttributes.Single(x => x.AttributeType == typeof(TableAttribute))
                         .NamedArguments.Single(x => x.MemberName == "Name")
                         .TypedValue.Value.ToString();
        }

        public static T Read<T>(this T person, SqlCeDataReader reader) where T : IMap
        {
            foreach (var property in typeof(T).GetProperties())
            {
                var type = GetFieldType<T>(property.Name);
                if (type == "int")
                {
                    property.SetValue(person, int.Parse(reader[GetFieldName<T>(property.Name)].ToString()));
                }
                else if (type == "string")
                {
                    property.SetValue(person, reader[GetFieldName<T>(property.Name)].ToString()); 
                }
            }

            return person;
        } 

        private static IList<CustomAttributeNamedArgument> GetField<T>(string propertyName) where T : IMap
        {
             return typeof(T).GetProperty(propertyName)
                     .CustomAttributes.Single(x => x.AttributeType == typeof(FieldAttribute))
                     .NamedArguments;
        }

        public static string GetFieldName<T>(string propertyName)  where T : IMap
        {
            return GetField<T>(propertyName).Single(x => x.MemberName == "Name")
                          .TypedValue.Value.ToString();
        }

        public static string GetFieldType<T>(string propertyName) where T : IMap
        {
            return GetField<T>(propertyName).Single(x => x.MemberName == "Type")
                          .TypedValue.Value.ToString();
        }
    }
}
