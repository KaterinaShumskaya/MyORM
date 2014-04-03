using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persons
{
    public class ListPersonAccessor : IPersonAccessor
    {
        private readonly IList<Person> _persons; 

        public ListPersonAccessor(IList<Person> persons)
        {
            _persons = persons;
        }

        public IList<Person> GetAll()
        {
            return _persons;
        }

        public IList<Person> GetByLastName(string lastName)
        {
            return _persons.Where(x=>x.LastName.Equals(lastName)).ToList();
        }

        public void DeleteByLastName(string lastName)
        {
            var people = GetByLastName(lastName);
            foreach (var person in people)
            {
                _persons.Remove(person); 
            }
            
        }

        public void Insert(Person person)
        {
            _persons.Add(person);
        }
    }
}
