//  Name . . . : James Bell
//  Class. . . : CSCI-257 Web Services
//  Instructor : Bryon Steinwand
//  Date . . . : 11/17/2018
//  Assignment : Task One
//  File . . . : Project.cs
//  Notes. . . : Model Class for Project resource

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web_Services_Task_1.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; } // Primary Key

        [Required]
        [MaxLength(30)]
        public string Name { get; set; } // Name of the project

        [Required]
        public Employer Employer { get; set; } // A project is unique to the employer

        public List<Discipline> RequiredDisciplines { get; set; } // A list of the dsciplines needed to complete the project.  Example: "Computer Science" and/or "Business" 

        public List<Student> StudentsOnProject { get; set; } // A list of students assigned to the project

        


    }
}