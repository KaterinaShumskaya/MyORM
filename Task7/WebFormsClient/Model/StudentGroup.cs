using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebFormsClient.Model
{
    using Persons.Attributes;
    using Persons.Domain;

    [Table("StudentGroup")]
    [Serializable]
    public class StudentGroup : EntityBase
    {
        public StudentGroup(string name)
        {
            Name = name;
        }

        public StudentGroup(string name, int id) : this(name)
        {
            Id = id;
        }

        public StudentGroup()
        {
            
        }

        [Field("Name", "string")]
        public string Name { get; set; }
    }
}