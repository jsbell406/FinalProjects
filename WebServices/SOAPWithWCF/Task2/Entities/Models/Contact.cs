using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Web;

namespace Entities.Models
{
    [DataContract]
    public class Contact : Person
    {
        [DataMember]
        [Key]    
        public int ContactId { get; set; }

        [DataMember]
        public Employer Employer { get; set; }

        [DataMember]
        public string BestContactPhone { get; set; }

        [DataMember]
        public string BestContactEmail { get; set; }

        [DataMember]
        public bool UsePersonalContact { get; set; } = false;

    }
}
