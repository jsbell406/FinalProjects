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
    public class Focus
    {
        [DataMember]
        [Key]
        public int FocusId { get; set; }

        [DataMember]
        [Required]
        [MaxLength(30)]
        public string FocusForField { get; set; }

        [DataMember]
        public Field Field { get; set; }

        
    }
}
