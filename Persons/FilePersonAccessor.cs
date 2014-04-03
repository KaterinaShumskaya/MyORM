using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persons
{
    using System.IO;
    using System.Xml.Serialization;

    public class FilePersonAccessor : IPersonAccessor
    {
        private IList<Person> _persons;

        private readonly string _fileName;

        public FilePersonAccessor(string fileName, IList<Person> persons)
        {
            _persons = persons;
            _fileName = fileName;
            this.Serialize();
        }

        public IList<Person> GetAll()
        {
            return Deserialize();
        }

        public  IList<Person> GetByLastName(string lastName)
        {
            return this.Deserialize().Where(x=>x.LastName.Equals(lastName)).ToList();
        }

        public void DeleteByLastName(string lastName)
        {
            var people = this.GetByLastName(lastName);
            foreach (var person in people)
            {
                _persons.Remove(person);  
            }
            
            this.Serialize();
        }

        public void Insert(Person person)
        {
            _persons.Add(person);
            this.Serialize();
        }

        private void Serialize()
        {
            using (var fileStream = new FileStream(_fileName, FileMode.Create))
            {
                var xmlSerializer = new XmlSerializer(typeof(List<Person>));
                xmlSerializer.Serialize(fileStream, _persons);
            }
        }

        private IList<Person> Deserialize()
        {
            using (var fileStream = new FileStream(_fileName, FileMode.Open))
            {
                var xmlSerializer = new XmlSerializer(typeof(List<Person>));
                _persons = (IList<Person>)xmlSerializer.Deserialize(fileStream);
                return _persons;
            }
        }
    }
}
