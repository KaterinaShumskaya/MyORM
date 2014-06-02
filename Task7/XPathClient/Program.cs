using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPathClient
{
    using Persons.DataAccessors;
    using Persons.Domain;

    class Program
    {
        static void Main(string[] args)
        {
            var accessor = new XPathDataAccessor<Person>("persons.xml");
            var person = accessor.GetById(2);
            Console.WriteLine(person.ToString());
            Console.ReadKey();
        }
    }
}
