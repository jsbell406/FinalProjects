//  Name . . . : James Bell
//  Class. . . : CSCI-257 Web Services
//  Instructor : Bryon Steinwand
//  Date . . . : 11/17/2018
//  Assignment : Task One
//  File . . . : Employer.cs
//  Notes. . . : Model Class for Employer resource

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web_Services_Task_1.Models
{
    public class Employer
    {
        [Key]
        public int EmployerId { get; set; } // Primary Key


        [Required]
        [MaxLength(30)]
        public string CompanyName { get; set; } // Name of Company "McDonald's"

        [Required]
        [MaxLength(30)]
        public string ContactFirstName { get; set; } // Main Contacts's first name "John"

        [Required]
        [MaxLength(30)]
        public string ContactLastName { get; set; } // Main Contact's last name "Smith"

        [Required]
        [Phone]
        [MaxLength(12)]
        public string ContactPhone { get; set; } // Main Contact's phone number "458-457-5500"

        [Required]
        [EmailAddress]
        [MaxLength(30)]
        public string ContactEmail { get; set; } // Main Contact's email "john.smith@domain.com" 


        public List<Project> Projects { get; set; } // An employer can have a list of projects

        public override string ToString()
        {
            return String.Format("{0}'s Contact Info {1} {2} Phone: {3} Email: {4}", CompanyName, ContactFirstName, ContactLastName, ContactPhone, ContactEmail);
        }


    }
}