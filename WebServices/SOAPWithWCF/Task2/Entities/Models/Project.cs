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
    public class Project
    {
        [DataMember]
        [Key]
        public int ProjectId { get; set; }

        [DataMember]
        [Required]
        [MaxLength(50)]
        public string ProjectName { get; set; }

        [DataMember]
        [Required]
        [MaxLength(250)]
        public string ProjectDescription { get; set; }

        [DataMember]
        [Required]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [DataMember]
        public DateTime? ScheduledEndDate { get; set; }

        [DataMember]
        [Required]
        public Employer Employer { get; set; }

        [DataMember]
        [Required]
        public Contact ProjectSupervisor { get; set; }

        [DataMember]
        public List<Field> RequiredFields { get; set; }

        [DataMember]
        public List<Student> AssignedStudents { get; set; }

        [DataMember]
        public bool IsComplete { get; set; } = false;

    }
}
