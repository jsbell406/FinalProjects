//  Name . . . : James Bell
//  Class. . . : CSCI-257 Web Services
//  Instructor : Bryon Steinwand
//  Date . . . : 11/17/2018
//  Assignment : Task One
//  File . . . : DbCon.cs
//  Notes. . . : Context class for interactions with the database.

using System.Data.Entity;

namespace Web_Services_Task_1.Models
{
    public class DbCon : DbContext
    {

        // Database connection
        public DbCon()
            : base ("TaskOneConnect")
        {

        }

    
        // Collections of Entities
        public DbSet<Discipline> Disciplines { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<Student> Students { get; set; }

    }
}