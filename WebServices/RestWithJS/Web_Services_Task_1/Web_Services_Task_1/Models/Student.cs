//  Name . . . : James Bell
//  Class. . . : CSCI-257 Web Services
//  Instructor : Bryon Steinwand
//  Date . . . : 11/17/2018
//  Assignment : Task One
//  File . . . : Studnet.cs
//  Notes. . . : Model Class for Student resource

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web_Services_Task_1.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; } // Primary Key

        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; } // Student's first name

        [Required]
        [MaxLength(30)]
        public string LastName { get; set; } // Student's last name

        public Discipline Discipline { get; set; } // Student's field of study

        public List<Project> ActiveProjects { get; set; } // List of projects the student is currently working on

        public List<Project> CompletedProjects { get; set; } // List of projects the student has completed

    }
}