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
    class Salt
    {
        [DataMember]
        [Key]
        public int SaltId { get; set; }

        [DataMember]
        [Required]
        public string UniqueSalt { get; set; }
    }
}
