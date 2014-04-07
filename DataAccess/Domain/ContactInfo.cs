using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persons.Domain
{
    using Persons.Attributes;

    [Table("ContactInfoTable")]
    public class ContactInfo : EntityBase
    {
        [Field("AddressField", "string")]
        public string Address { get; set; }

        [Field("PhoneField", "string")]
        public string Phone { get; set; }

        [Field("EmailField", "string")]
        public string Email { get; set; }

        public override string ToString()
        {
            return String.Format("{0} {1} {2} {3}", Id, Address, Phone, Email);
        }
    }
}
