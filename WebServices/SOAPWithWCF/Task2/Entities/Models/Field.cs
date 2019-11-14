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
    public class Field
    {
        [DataMember]
        [Key]
        public int FieldId { get; set; }

        [DataMember]
        [Required]
        [MaxLength(30)]
        public string FieldOfStudy { get; set; }

        [DataMember]
        public List<Focus> Foci { get; set; }

    }
}
