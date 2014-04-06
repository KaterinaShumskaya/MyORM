using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persons
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FieldAttribute : TableAttribute
    {
        public readonly string Type;

        public FieldAttribute(string name, string type) : base(name)
        {
            Type = type;
        }
    }
}
