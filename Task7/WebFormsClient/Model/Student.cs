using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebFormsClient.Model
{
    using Persons.Attributes;
    using Persons.Domain;

    [Serializable]
    [Table("Student")]
    public class Student : EntityBase
    {
          public Student()
        {
            
        }

        /// <summary>
        /// Инстанцирует объект класса <see cref="Person"/>.
        /// </summary>
        /// <param name="firstName">Имя.</param>
        /// <param name="lastName">Фамилия.</param>
        /// <param name="middleName">Отчество.</param>
        /// <param name="age">Дата рождения.</param>
        public Student(string lastName, string firstName, string middleName, DateTime date, int studentGroupId)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.MiddleName = middleName;
            this.DateOfBirth = date;
            StudentGroupId = studentGroupId;

        }

        public Student(int id, string lastName, string firstName, string middleName, DateTime date, int studentGroupId) : 
            this(lastName, firstName, middleName, date, studentGroupId)
        {
            this.Id = id;
        }

        /// <summary>
        /// Имя.
        /// </summary>
        [Field("FirstName", "string")]
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия.
        /// </summary>
        [Field("LastName", "string")]
        public string LastName { get; set; }

        /// <summary>
        /// Отчество.
        /// </summary>
        [Field("MiddleName", "string")]
        public string MiddleName { get; set; }

        /// <summary>
        /// Дата рождения.
        /// </summary>
        [Field("DateOfBirth", "datetime")]
        public DateTime DateOfBirth { get; set; }

        [Field("StudentGroupId", "int")]
        public int StudentGroupId { get; set; }

        public override string ToString()
        {
            return String.Format("{4} {0} {1} {2} {3}", this.LastName, this.FirstName, 
                this.MiddleName, this.DateOfBirth, this.Id);
        }
    }
}