using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persons
{
    using System.Data.SqlClient;
    using System.Data.SqlServerCe;

    class ADONetPersonAccessor : IPersonAccessor
    {
        private IList<Person> _persons = new List<Person>(); 

        public IList<Person> GetAll()
        {
            string queryString = "SELECT * from person";
            var people = new List<Person>();
            using (var command = new Command(queryString))
            {
                SqlCeDataReader reader = command.SqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    people.Add(
                        new Person(
                            reader["lastName"].ToString(),
                            reader["firstName"].ToString(),
                            reader["middleName"].ToString(),
                            int.Parse(reader["age"].ToString())));
                }

                reader.Close();
            }

            return people;
        }

        public IList<Person> GetByLastName(string lastName)
        {
            string queryString = "SELECT * from person where LastName LIKE @lastName";
           
            var people = new List<Person>();
            using (var command = new Command(queryString))
            {           
                command.SqlCommand.Parameters.AddWithValue("@lastName", lastName);
                SqlCeDataReader reader = command.SqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    people.Add(
                        new Person(
                            reader["lastName"].ToString(),
                            reader["firstName"].ToString(),
                            reader["middleName"].ToString(),
                            int.Parse(reader["age"].ToString())));
                }

                reader.Close();
            }
            return people;
        }

        public void DeleteByLastName(string lastName)
        {
            string queryString = "DELETE from person where (LastName = @lastName)";
           
            using (var command = new Command(queryString))
            {
                command.SqlCommand.Parameters.AddWithValue("@lastName", lastName);
                command.SqlCommand.ExecuteNonQuery();
            }
        }

        public void Insert(Person person)
        {
            string queryString = "INSERT INTO Person (FirstName, LastName, MiddleName, Age) VALUES (@firstName,@lastName,@middleName,@age)";

            using (var command = new Command(queryString))
            {
                command.SqlCommand.Parameters.AddWithValue("@lastName", person.LastName);
                command.SqlCommand.Parameters.AddWithValue("@firstName", person.FirstName);
                command.SqlCommand.Parameters.AddWithValue("@middleName", person.MiddleName);
                command.SqlCommand.Parameters.AddWithValue("@age", person.Age);
                command.SqlCommand.ExecuteNonQuery();
            }
        }
    }
}
