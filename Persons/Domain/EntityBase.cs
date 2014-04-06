using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persons
{
    using Persons.Attributes;

    public class EntityBase
    {
        [Identity("Id", "int")]
        public int Id { get; set; }
    }
}
