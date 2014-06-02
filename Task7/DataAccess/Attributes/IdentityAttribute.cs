using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persons.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class IdentityAttribute : FieldAttribute
    {
        public IdentityAttribute(string name, string type)
            : base(name, type)
        {
            
        }
    }
}
