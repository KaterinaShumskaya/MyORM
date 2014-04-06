using System.Collections.Generic;

namespace Persons
{
    using System.Data.SqlServerCe;

    public class AdoNetDataAccessor : IDataAccessor<Person>
    {
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

        public Person GetById(int id)
        {
            string queryString = "SELECT * from person where Id = @id";
           
            var person = new Person();
            using (var command = new Command(queryString))
            {           
                command.SqlCommand.Parameters.AddWithValue("@id", id);
                SqlCeDataReader reader = command.SqlCommand.ExecuteReader();

                if (reader.Read())
                {
                    person = 
                        new Person(
                            reader["lastName"].ToString(),
                            reader["firstName"].ToString(),
                            reader["middleName"].ToString(),
                            int.Parse(reader["age"].ToString()));
                }

                reader.Close();
            }

            return person;
        }

        public void DeleteById(int id)
        {
            string queryString = "DELETE from person where (Id = @id)";
           
            using (var command = new Command(queryString))
            {
                command.SqlCommand.Parameters.AddWithValue("@id", id);
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
