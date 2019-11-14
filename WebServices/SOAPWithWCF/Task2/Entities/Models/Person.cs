using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    [DataContract]
    public class Person
    {
        [DataMember]
        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [DataMember]
        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }

        [DataMember]
        [Phone]
        [MaxLength(12)]
        public string Phone { get; set; }

        [DataMember]
        [EmailAddress]
        public string Email { get; set; }

        [DataMember]
        public Address Address { get; set; }
    }
}
