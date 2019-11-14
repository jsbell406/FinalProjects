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
    class UserAccount
    {
        [DataMember]
        [Key]
        public int UserAccountId { get; set; }

        [DataMember]
        [Required]
        [MaxLength(50)]
        public string AccountName { get; set; }
      
    }
}
