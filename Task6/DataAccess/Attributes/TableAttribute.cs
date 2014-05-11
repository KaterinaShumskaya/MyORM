namespace Persons.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public class TableAttribute : Attribute
    {
        public readonly string Name;

        public TableAttribute(string name)
        {
            this.Name = name;
        }
    }
}
