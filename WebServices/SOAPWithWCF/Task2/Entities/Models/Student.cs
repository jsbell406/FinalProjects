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
    public class Student : Person
    {
        [DataMember]
        [Key]
        public int StudentId { get; set; }

        [DataMember]
        [EmailAddress]
        public string StudentEmail { get; set; }

        [DataMember]
        public EmergencyContact EmergencyContact { get; set; }

        [DataMember]
        public Focus Focus { get; set; }

    }
}
