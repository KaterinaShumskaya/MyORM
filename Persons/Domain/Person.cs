using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persons
{
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
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            this.Age = age;
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
            return String.Format("{4} {0} {1} {2} {3}", LastName, FirstName, MiddleName, Age, Id);
        }
    }
}
