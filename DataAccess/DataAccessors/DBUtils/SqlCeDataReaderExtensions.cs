namespace Persons.DataAccessors.DBUtils
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlServerCe;
    using System.Reflection;

    using Persons.Attributes;

    public static class SqlCeDataReaderExtensions
    {
        public static T GetEntity<T>(this SqlCeDataReader reader, IDictionary<PropertyInfo, FieldAttribute> fieldAttrs)
        {
            var constructor = typeof(T).GetConstructor(new Type[0]);
            if (constructor == null)
            {
                throw new NullReferenceException(
                    String.Format("В классе {0} нет конструктора без параметров.", typeof(T)));
            }

            var entity = (T)constructor.Invoke(new object[0]);
            foreach (var property in fieldAttrs)
            {
                var fieldAttribute = property.Value;
               
                var type = fieldAttribute.Type;
                var name = fieldAttribute.Name;
                switch (type)
                {
                    case "int":
                        property.Key.SetValue(entity, int.Parse(reader[name].ToString()));
                        break;
                    case "string":
                        property.Key.SetValue(entity, reader[name].ToString());
                        break;
                }
            }   

            return entity;
        }
    }
}
