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
    class Password
    {
        [DataMember]
        [Key]
        public int PasswordId { get; set; }

        [DataMember]
        [Required]
        [MaxLength(30)]
        public string UserPassword { get; set; }

        [DataMember]
        [Required]
        public Salt Salt { get; set; }

    }
}
