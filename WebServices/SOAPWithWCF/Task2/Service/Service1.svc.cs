using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Data;
using Entities.Models;
using System.Data.Entity;


namespace Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        // Student Operations --------------------------------------------------------------------

        // Get all Students
        public IEnumerable<Student> GetStudents()
        {
            List<Student> students;

            using (MyDbContext db = new MyDbContext())
            {
                students = db.Students.Include(s => s.EmergencyContact).ToList();
                return students;
            }
        }

        // Add new Student
        public Student AddStudent(Student student)
        {
            using (MyDbContext db = new MyDbContext())
            {
                student = db.Students.Add(student);
                db.SaveChanges();
            }
            return student;
        }

        // Get student by Id
        public Student GetStudentById(int id)
        {
            Student student;
            using (MyDbContext db = new MyDbContext())
            {
                student = db.Students.Find(id);
            }
            return student;
        }

        // Modify Student
        public Student ModifyStudent(Student student, int id)
        {
            Student studentToModify;
            using (MyDbContext db = new MyDbContext())
            {
                studentToModify = db.Students.Find(id);

                studentToModify.FirstName = student.FirstName;
                studentToModify.LastName = student.LastName;
                studentToModify.Phone = student.Phone;
                studentToModify.StudentEmail = student.StudentEmail;
                studentToModify.EmergencyContact = student.EmergencyContact;
                studentToModify.Email = student.Email;
                studentToModify.Address = student.Address;
 
                db.Entry(studentToModify).State = EntityState.Modified;
                db.SaveChanges();


                studentToModify = db.Students.Where(x => x.StudentId == id)
                    .Include(x => x.Address).FirstOrDefault();
            }
            return studentToModify;
        }


        // Delete Student by Id
        public void DeleteStudentById(int id)
        {
            using (MyDbContext db = new MyDbContext())
            {
                Student studentToGo = db.Students.Find(id);
                db.Students.Remove(studentToGo);
                db.SaveChanges();
            }
        }

        // Add existing address to existing student
        public Student AddAddressToStudent(int addressId, int studentId)
        {
            Student student;
            Address address;

            using (MyDbContext db = new MyDbContext())
            {
                student = db.Students.Find(studentId);
                address = db.Addresses.Find(addressId);

                student.Address = address;

                db.Entry(student).State = EntityState.Modified;

                db.SaveChanges();
            }
            return student;
        }

        public Student AddFocusToStudentByIds(int studentId, int focusId)
        {
            Student stud;
            Focus fo;

            using (MyDbContext db = new MyDbContext())
            {
                stud = db.Students.Find(studentId);
                fo = db.Foci.Find(focusId);

                stud.Focus = fo;
                
                db.Entry(stud).State = EntityState.Modified;

                db.SaveChanges();

                return stud;
            }
        }



        // Employer Operations --------------------------------------------------------------------

        // Get employers
        public IEnumerable<Employer> GetEmployers()
        {
            List<Employer> employers;

            using (MyDbContext db = new MyDbContext())
            {
                employers = db.Employers.ToList();
                return employers;
            }
        }


        // Add Employer
        public Employer AddEmployer(Employer employer)
        {
            using (MyDbContext db = new MyDbContext())
            {
                employer = db.Employers.Add(employer);
                db.SaveChanges();
            }
            return employer;
        }

        // Get Employer by Id
        public Employer GetEmployerById(int id)
        {
            Employer employer;
            using (MyDbContext db = new MyDbContext())
            {
                employer = db.Employers.Where(x => x.EmployerId == id).Include(x => x.Address).Include(x => x.Projects).FirstOrDefault();
            }
            return employer;
        }

        // Modify Employer
        public Employer ModifyEmployer(Employer employer, int id)
        {
            Employer employerToMod;

            using (MyDbContext db = new MyDbContext())
            {
                employerToMod = db.Employers.Find(id);
                   
                employerToMod.Address = employer.Address;
                employerToMod.EmployerEmail = employer.EmployerEmail;
                employerToMod.EmployerName = employer.EmployerName;
                employerToMod.EmployerPhone = employer.EmployerPhone;
                employerToMod.Projects = employer.Projects;

                db.Entry(employerToMod).State = EntityState.Modified;
                db.SaveChanges();

                employerToMod = db.Employers.Where(e => e.EmployerId == id)
                    .Include(e => e.Address)
                    .Include(e => e.Projects).FirstOrDefault();
            }
            return employerToMod;
        }

        // Delete Employer by Id
        public void DeleteEmployerById(int id)
        {
            using (MyDbContext db = new MyDbContext())
            {
                Employer employerToGo = db.Employers.Find(id);
                db.Employers.Remove(employerToGo);
                db.SaveChanges();
            }
        }

        public Employer AddContactToEmployerByIds(int contactId, int employerId)
        {
            Employer emp;
            Contact con;

            using (MyDbContext db = new MyDbContext())
            {

                emp = db.Employers.Find(employerId);
                con = db.Contacts.Find(contactId);
                
                if(emp != null & con != null)
                {
  
                    con.Employer = emp;

                    db.Entry(con).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return emp;
            }
        }


        // Address Operations --------------------------------------------------------------------

        // Get Addresses
        public IEnumerable<Address> GetAddresses()
        {
            List<Address> addresses;

            using (MyDbContext db = new MyDbContext())
            {
                addresses = db.Addresses.ToList();
                return addresses;
            }
        }

        // Add Address
        public Address AddNewAddress(Address address)
        {
            using (MyDbContext db = new MyDbContext())
            {
                address = db.Addresses.Add(address);
                db.SaveChanges();
                return address;
            }
        }

        // Get Address by Id
        public Address GetAddressById(int id)
        {
            Address address;
            using (MyDbContext db = new MyDbContext())
            {
                address = db.Addresses.Find(id);
            }
            return address;
        }

        // Modify Address
        public Address ModifyAddress(Address address, int id)
        {
            Address addressToMod;

            using (MyDbContext db = new MyDbContext())
            {
                addressToMod = db.Addresses.Find(id);
                addressToMod = address;
                addressToMod.AddressId = id;
                db.Entry(addressToMod).State = EntityState.Modified;
                db.SaveChanges();
            }
            return addressToMod;
        }

        // Delete Address by Id
        public void DeleteAddressById(int id)
        {
            using (MyDbContext db = new MyDbContext())
            {
                Address addressToGo = db.Addresses.Find(id);
                db.Addresses.Remove(addressToGo);
                db.SaveChanges();
            }
        }


        // Project Operations --------------------------------------------------------------------

        // Get Projects
        public IEnumerable<Project> GetProjects()
        {
            List<Project> projects;

            using (MyDbContext db = new MyDbContext())
            {
                projects = db.Projects.ToList();
                return projects;
            }
        }

        // Add Project
        public Project AddNewProject(Project project)
        {
            using (MyDbContext db = new MyDbContext())
            {
                project = db.Projects.Add(project);
                db.SaveChanges();
                return project;
            }
        }

        // Get Project by Id
        public Project GetProjectById(int id)
        {
            Project project;
            using (MyDbContext db = new MyDbContext())
            {
                project = db.Projects.Where(x => x.ProjectId == id)
                    .Include(x => x.Employer)
                    .Include(x => x.ProjectSupervisor)
                    .Include(x => x.RequiredFields)
                    .Include(x => x.AssignedStudents).FirstOrDefault();
            }
            return project;
        }

        // Modify Project
        public Project ModifyProject(Project project, int id)
        {
            Project projectToMod;

            using (MyDbContext db = new MyDbContext())
            {
                projectToMod = db.Projects.Where(x => x.ProjectId == id)
                    .Include(x => x.Employer)
                    .Include(x => x.ProjectSupervisor)
                    .Include(x => x.RequiredFields)
                    .Include(x => x.AssignedStudents).FirstOrDefault();

                projectToMod = project;
                projectToMod.ProjectId = id;
                db.Entry(projectToMod).State = EntityState.Modified;
                db.SaveChanges();
            }
            return projectToMod;
        }

        // Delete Project by Id
        public void DeleteProjectById(int id)
        {
            using (MyDbContext db = new MyDbContext())
            {
                Project projectToGo = db.Projects.Find(id);
                db.Projects.Remove(projectToGo);
                db.SaveChanges();
            }
        }



        // Contact Operations --------------------------------------------------------------------

        // Get Contacts
        public IEnumerable<Contact> GetContacts()
        {
            List<Contact> contacts;

            using (MyDbContext db = new MyDbContext())
            {
                contacts = db.Contacts.ToList();
                return contacts;
            }
        }


        // Add Contact
        public Contact AddNewContact(Contact contact)
        {
            
            using (MyDbContext db = new MyDbContext())
            {
             

                contact = db.Contacts.Add(contact);
                db.SaveChanges();
                return contact;
            }
        }

        // Get Contact by Id
        public Contact GetContactById(int id)
        {
            Contact contact;
            using (MyDbContext db = new MyDbContext())
            {
                contact = db.Contacts.Where(x => x.ContactId == id)
                    .Include(x => x.Employer).FirstOrDefault();
            }
            return contact;
        }

        // Modify Contact
        public Contact ModifyContact(Contact contact, int id)
        {
            Contact contactToMod;

            using (MyDbContext db = new MyDbContext())
            {
                contactToMod = db.Contacts.Where(x => x.ContactId == id)
                    .Include(x => x.Employer).FirstOrDefault();

                contactToMod = contact;
                contactToMod.ContactId = id;
                db.Entry(contactToMod).State = EntityState.Modified;
                db.SaveChanges();
            }
            return contactToMod;
        }

        // Delete Contact by Id
        public void DeleteContactById(int id)
        {
            using (MyDbContext db = new MyDbContext())
            {
                Contact contactToGo = db.Contacts.Find(id);
                db.Contacts.Remove(contactToGo);
                db.SaveChanges();
            }
        }

        public IEnumerable<Contact> GetEmployerContacts(int employerId)
        {
            List<Contact> contacts;
            using (MyDbContext db = new MyDbContext())
            {
                contacts = db.Contacts.Where(x => x.Employer.EmployerId == employerId)
                    .Include(x => x.Employer).ToList();

                return contacts;
            }
        }



        // Person Operations --------------------------------------------------------------------

        // Get Persons
        public IEnumerable<EmergencyContact> GetEmContacts()
        {
            List<EmergencyContact> person;

            using (MyDbContext db = new MyDbContext())
            {
                person = db.EmergencyContacts.ToList();
                return person;
            }
        }

        // Add Person
        public EmergencyContact AddNewEmContact(EmergencyContact person)
        {
            using (MyDbContext db = new MyDbContext())
            {
                person = db.EmergencyContacts.Add(person);
                db.SaveChanges();
                return person;
            }
        }


        // Get Person by Id
        public EmergencyContact GetEmContactById(int id)
        {
            EmergencyContact person;

            using (MyDbContext db = new MyDbContext())
            {
                person = db.EmergencyContacts.Where(x => x.EmergencyContactId == id)
                    .Include(x => x.Address).FirstOrDefault();
            }
            return person;
        }

        // Modify Person
        public EmergencyContact ModifyEmContact(EmergencyContact person, int id)
        {
            EmergencyContact emContactToMod;

            using (MyDbContext db = new MyDbContext())
            {
                emContactToMod = db.EmergencyContacts.Where(x => x.EmergencyContactId == id)
                    .Include(x => x.Address).FirstOrDefault();

                emContactToMod = person;
                emContactToMod.EmergencyContactId = id;
                db.Entry(emContactToMod).State = EntityState.Modified;
                db.SaveChanges();
            }
            return emContactToMod;
        }

        // Delete Person by Id
        public void DeleteEmContactById(int id)
        {
            using (MyDbContext db = new MyDbContext())
            {
                EmergencyContact emContactToGo = db.EmergencyContacts.Find(id);
                db.EmergencyContacts.Remove(emContactToGo);
                db.SaveChanges();
            }
        }


        // Field Operations --------------------------------------------------------------------

        // Get Fields
        public IEnumerable<Field> GetFields()
        {
            List<Field> fields;

            using (MyDbContext db = new MyDbContext())
            {
                fields = db.Fields.ToList();
                return fields;
            }
        }


        // Add Field
        public Field AddNewField(Field field)
        {
            using (MyDbContext db = new MyDbContext())
            {
                field = db.Fields.Add(field);
                db.SaveChanges();
                //field = db.Fields.Where(f => f.FieldId == field.FieldId).First();

                return field;
            }
        }

        // Get Field by Id
        public Field GetFieldById(int id)
        {
            Field field;

            using (MyDbContext db = new MyDbContext())
            {
                field = db.Fields.Find(id);
            }
            return field;
        }

        // Modify Field
        public Field ModifyField(Field field, int id)
        {
            Field fieldToMod;

            using (MyDbContext db = new MyDbContext())
            {
                fieldToMod = db.Fields.Where(x => x.FieldId == id)
                    .Include(x => x.Foci).FirstOrDefault();

                fieldToMod = field;
                fieldToMod.FieldId = id;
                db.Entry(fieldToMod).State = EntityState.Modified;
                db.SaveChanges();
            }
            return fieldToMod;
        }

        // Delete Field by Id
        public void DeleteFieldById(int id)
        {
            using (MyDbContext db = new MyDbContext())
            {
                Field fieldToGo = db.Fields.Find(id);
                db.Fields.Remove(fieldToGo);
                db.SaveChanges();
            }
        }

        // Focus Operations --------------------------------------------------------------------


        // Get Foci
        public IEnumerable<Focus> GetFoci()
        {
            List<Focus> foci;

            using (MyDbContext db = new MyDbContext())
            {
                foci = db.Foci.Include(f => f.Field).ToList();
                return foci;
            }
        }


        // Add Focus
        public Focus AddNewFocus(Focus focus)
        {
            Field field;

            using (MyDbContext db = new MyDbContext())
            {

                focus = db.Foci.Add(focus);
                db.SaveChanges();
                //focus = db.Foci.Where(f => f.FocusId == focus.FocusId).Include(f => f.Field).FirstOrDefault();

                return focus;
            }
        }

        // Get Focus by Id
        public Focus GetFocusById(int id)
        {
            Focus focus;

            using (MyDbContext db = new MyDbContext())
            {
                focus = db.Foci.Find(id);
            }
            return focus;
        }

        // Modify Focus
        public Focus ModifyFocus(Focus focus, int id)
        {
            Focus focusToMod;

            using (MyDbContext db = new MyDbContext())
            {
                focusToMod = db.Foci.Find(id);


                focusToMod = focus;
                focusToMod.FocusId = id;
                db.Entry(focusToMod).State = EntityState.Modified;
                db.SaveChanges();
            }
            return focusToMod;
        }

        // Delete Focus by Id
        public void DeleteFocusById(int id)
        {
            using (MyDbContext db = new MyDbContext())
            {
                Focus focusToGo = db.Foci.Find(id);
                db.Foci.Remove(focusToGo);
                db.SaveChanges();
            }
        }

        // Add Existing Focus to Existing Field
        public void AddExistingFocusToField(int focusId, int fieldId)
        {
            Focus focus;
            Field field;

            using (MyDbContext db = new MyDbContext())
            {
                focus = db.Foci.Find(focusId);
                field = db.Fields.Find(fieldId);

                if (focus != null & field != null)
                {
                    focus.Field = field;
                    db.Entry(focus).State = EntityState.Modified;
                    db.SaveChanges();

                    //focus = db.Foci.Where(f => f.FocusId == focusId).Include(f => f.Field).FirstOrDefault();
                }

                //return focus;
            }
        }

        public Focus AddFocusToField(Focus focus, int fieldId)
        {
            Field field;
            using (MyDbContext db = new MyDbContext())
            {
                field = db.Fields.Find(fieldId);
                focus.Field = field;

                db.Entry(focus).State = EntityState.Modified;
                db.SaveChanges();

                return focus;
            }
        }
    }
}
