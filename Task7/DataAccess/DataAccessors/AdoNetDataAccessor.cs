namespace Persons.DataAccessors
{
    using System.Collections.Generic;
    using System.Data.SqlServerCe;

    using Persons.DataAccessors.DBUtils;
    using Persons.Domain;

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
                            int.Parse(reader["id"].ToString()),
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
           
            Person person;
            using (var command = new Command(queryString))
            {           
                command.SqlCommand.Parameters.AddWithValue("@id", id);
                SqlCeDataReader reader = command.SqlCommand.ExecuteReader();

                if (reader.Read())
                {
                    person = new Person(
                        int.Parse(reader["id"].ToString()),
                        reader["lastName"].ToString(),
                        reader["firstName"].ToString(),
                        reader["middleName"].ToString(),
                        int.Parse(reader["age"].ToString()));
                }
                else
                {
                    person = null;
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

        public int Insert(Person person)
        {
            string queryString = "INSERT INTO Person (FirstName, LastName, MiddleName, Age) VALUES (@firstName,@lastName,@middleName,@age)";

            var id = 0;
            using (var command = new Command(queryString))
            {
                command.SqlCommand.Parameters.AddWithValue("@lastName", person.LastName);
                command.SqlCommand.Parameters.AddWithValue("@firstName", person.FirstName);
                command.SqlCommand.Parameters.AddWithValue("@middleName", person.MiddleName);
                command.SqlCommand.Parameters.AddWithValue("@age", person.Age);
                command.SqlCommand.ExecuteNonQuery();
                command.SqlCommand.CommandText = "SELECT @@IDENTITY";
                int.TryParse(command.SqlCommand.ExecuteScalar().ToString(), out id);
            }

            return id;
        }
    }
}
