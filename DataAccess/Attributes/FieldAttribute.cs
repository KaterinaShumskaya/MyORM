namespace Persons.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Property)]
    public class FieldAttribute : TableAttribute
    {
        public readonly string Type;

        public FieldAttribute(string name, string type) : base(name)
        {
            this.Type = type;
        }
    }
}
