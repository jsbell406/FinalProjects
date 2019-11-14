using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    [DataContract]
    public class EmergencyContact : Person
    {
        [DataMember]
        [Key]
        public int EmergencyContactId { get; set; }

    }
}
