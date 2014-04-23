namespace Persons.Domain
{
    using System;

    using Persons.Attributes;

    /// <summary>
    /// Человек.
    /// </summary>
    [Serializable]
    [Table("PersonTable")]
    public class Person : EntityBase
    {
        public Person()
        {
            
        }

        /// <summary>
        /// Инстанцирует объект класса <see cref="Person"/>.
        /// </summary>
        /// <param name="firstName">Имя.</param>
        /// <param name="lastName">Фамилия.</param>
        /// <param name="middleName">Отчество.</param>
        /// <param name="age">Дата рождения.</param>
        public Person(string lastName, string firstName, string middleName, int age)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.MiddleName = middleName;
            this.Age = age;
        }

        public Person(int id, string lastName, string firstName, string middleName, int age) : 
            this(lastName, firstName, middleName, age)
        {
            this.Id = id;
        }

        /// <summary>
        /// Имя.
        /// </summary>
        [Field("FirstNameField", "string")]
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия.
        /// </summary>
        [Field("LastNameField", "string")]
        public string LastName { get; set; }

        /// <summary>
        /// Отчество.
        /// </summary>
        [Field("MiddleNameField", "string")]
        public string MiddleName { get; set; }

        /// <summary>
        /// Дата рождения.
        /// </summary>
        [Field("AgeField", "int")]
        public int Age { get; set; }

        public override string ToString()
        {
            return String.Format("{4} {0} {1} {2} {3}", this.LastName, this.FirstName, this.MiddleName, this.Age, this.Id);
        }
    }
}
