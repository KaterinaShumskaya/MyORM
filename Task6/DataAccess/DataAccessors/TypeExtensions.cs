using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persons.DataAccessors
{
    using System.Reflection;

    using Persons.Attributes;

    public static class TypeExtensions
    {
        public static IDictionary<PropertyInfo, FieldAttribute> GetAllFieldAttributes(this Type type)
        {
            return
                type.GetProperties()
                         .Where(property => (FieldAttribute)type.GetFieldAttribute(property.Name) != null)
                         .ToDictionary(
                             property => property, property => (FieldAttribute)type.GetFieldAttribute(property.Name));
        }

        public static Attribute GetFieldAttribute(this Type type, string propertyName)
        {
            return type.GetProperty(propertyName).GetCustomAttribute(typeof(FieldAttribute));
        }
    }
}
