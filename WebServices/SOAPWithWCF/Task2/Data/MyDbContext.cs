using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext()
            : base("DataConntectionString")
        {
           
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Focus> Foci { get; set; }
        public DbSet<EmergencyContact> EmergencyContacts { get; set; }
        public DbSet<Project> Projects { get; set; }

        //public DbSet<UserAccount> UserAccounts { get; set; }
        //public DbSet<Password> Passwords { get; set; }
        //public DbSet<Salt> Salts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
          

           
        }

    }
}
