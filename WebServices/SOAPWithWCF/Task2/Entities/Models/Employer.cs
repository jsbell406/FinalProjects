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
    public class Employer
    {
        [DataMember]
        [Key]
        public int EmployerId { get; set; }

        [DataMember]
        [Required]
        [MaxLength(50)]
        public string EmployerName { get; set; }

        [DataMember]
        [Required]
        [Phone]
        [MaxLength(12)]
        public string EmployerPhone { get; set; }

        [DataMember]
        [Required]
        [EmailAddress]
        public string EmployerEmail { get; set; }

        [DataMember]
        public Address Address { get; set; }

        [DataMember]
        public List<Project> Projects { get; set; }

    }
}
