using System;
using System.Collections.Generic;

namespace Persons.DataAccessors
{
    using System.Reflection;
    using System.Xml.XPath;

    using Persons.Attributes;
    using Persons.Domain;

    public class XPathDataAccessor<T> where T: EntityBase
    {
        private readonly string _fileName;
        public XPathDataAccessor(string fileName)
        {
            _fileName = fileName;
        }

        public T GetById(int id)
        {
            XPathDocument document = new XPathDocument(_fileName);
            XPathNavigator navigator = document.CreateNavigator();

            XPathExpression expression = XPathExpression.Compile(String.Format(".//{0}[Id = {1}]", typeof(T).Name, id));
            XPathNavigator node = navigator.SelectSingleNode(expression);
            var properties = node.SelectChildren(XPathNodeType.Element);
            var propertiesList = new Dictionary<string, string>();
            while (properties.MoveNext())
            {
                propertiesList.Add(properties.Current.Name, properties.Current.Value);
            }

            return this.GetEntity(propertiesList, typeof(T).GetAllFieldAttributes());
        }

        private T GetEntity(IDictionary<string, string> reader, IDictionary<PropertyInfo, FieldAttribute> fieldAttrs)
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
                var fieldAttribute = property.Key;

                var type = property.Value.Type;
                var name = fieldAttribute.Name;
                switch (type)
                {
                    case "int":
                        property.Key.SetValue(entity, int.Parse(reader[name]));
                        break;
                    case "string":
                        property.Key.SetValue(entity, reader[name]);
                        break;
                    case "datetime":
                        property.Key.SetValue(entity, DateTime.Parse(reader[name]));
                        break;
                }
            }

            return entity;
        }
    }
}
