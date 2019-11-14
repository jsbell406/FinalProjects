//  Name . . . : James Bell
//  Class. . . : CSCI-257 Web Services
//  Instructor : Bryon Steinwand
//  Date . . . : 11/17/2018
//  Assignment : Task One
//  File . . . : Discipline.cs
//  Notes. . . : Model Class for Discipline resource

using System.ComponentModel.DataAnnotations;

namespace Web_Services_Task_1.Models
{
    public class Discipline
    {
        [Key]
        public int DisciplineId { get; set; } // Primary Key

        [Required]
        [MaxLength(30)]
        public string Focus { get; set; } // Programming

        [Required]
        [MaxLength(30)]
        public string Field { get; set; } // Computer Science
    }
}