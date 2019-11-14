using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Entities.Models
{
    [DataContract]
    public class Address
    {
        [DataMember]
        [Key]
        public int AddressId { get; set; }

        [DataMember]
        [Required]
        [MaxLength(9)]
        public string Number { get; set; }

        [DataMember]
        [Required]
        [MaxLength(200)]
        public string Street { get; set; }

        [DataMember]
        [Required]
        [MaxLength(30)]
        public string City { get; set; }

        [DataMember]
        [Required]
        [MaxLength(15)]
        public string State { get; set; }
    }
}
