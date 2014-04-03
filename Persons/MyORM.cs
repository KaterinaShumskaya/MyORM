using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persons
{
    using System.Data.SqlServerCe;

    public class MyORM : IPersonAccessor
    {
        public IList<Person> GetAll()
        {
            var tableName = IMapExtensions.GetTableName<Person>();
            string queryString = "SELECT * from "+tableName;
            var people = new List<Person>();
          
            using (var command = new Command(queryString))
            {
                SqlCeDataReader reader = command.SqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    var person = new Person().Read(reader);
                    people.Add(person);
                }

                reader.Close();
            }

            return people;
        }

        public IList<Person> GetByLastName(string lastName)
        {
            var tableName = IMapExtensions.GetTableName<Person>();
            string queryString = "SELECT * from "+tableName+" where "+
                IMapExtensions.GetFieldName<Person>("LastName")+" LIKE @lastName";
           
            var people = new List<Person>();
            using (var command = new Command(queryString))
            {           
                command.SqlCommand.Parameters.AddWithValue("@lastName", lastName);
                SqlCeDataReader reader = command.SqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    var person = new Person().Read(reader);
                    people.Add(person);
                }

                reader.Close();
            }
            return people;
        }

        public void DeleteByLastName(string lastName)
        {
            throw new NotImplementedException();
        }

        public void Insert(Person person)
        {
            throw new NotImplementedException();
        }
    }
}
