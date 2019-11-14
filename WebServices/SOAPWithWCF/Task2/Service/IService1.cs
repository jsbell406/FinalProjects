using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Entities.Models;

namespace Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        // Student Operations

        [OperationContract]
        IEnumerable<Student> GetStudents();

        [OperationContract]
        Student AddStudent(Student student);

        [OperationContract]
        Student GetStudentById(int id);

        [OperationContract]
        Student ModifyStudent(Student student, int id);

        [OperationContract]
        void DeleteStudentById(int id);

        [OperationContract]
        Student AddAddressToStudent(int addressId, int studentId);

        [OperationContract]
        Student AddFocusToStudentByIds(int studentId, int focusId);


        // Employer Operations

        [OperationContract]
        IEnumerable<Employer> GetEmployers();

        [OperationContract]
        Employer AddEmployer(Employer Employer);

        [OperationContract]
        Employer GetEmployerById(int id);

        [OperationContract]
        Employer ModifyEmployer(Employer employer, int id);

        [OperationContract]
        void DeleteEmployerById(int id);

        [OperationContract]
        Employer AddContactToEmployerByIds(int contactId, int employerId);


        // Address Operations

        [OperationContract]
        Address AddNewAddress(Address address);

        [OperationContract]
        IEnumerable<Address> GetAddresses();

        [OperationContract]
        Address GetAddressById(int id);

        [OperationContract]
        Address ModifyAddress(Address address, int id);

        [OperationContract]
        void DeleteAddressById(int id);


        // Project Operations

        [OperationContract]
        Project AddNewProject(Project project);

        [OperationContract]
        IEnumerable<Project> GetProjects();

        [OperationContract]
        Project GetProjectById(int id);

        [OperationContract]
        Project ModifyProject(Project project, int id);

        [OperationContract]
        void DeleteProjectById(int id);

        // Contact Operations

        [OperationContract]
        Contact AddNewContact(Contact contact);

        [OperationContract]
        IEnumerable<Contact> GetContacts();

        [OperationContract]
        Contact GetContactById(int id);

        [OperationContract]
        Contact ModifyContact(Contact contact, int id);

        [OperationContract]
        void DeleteContactById(int id);

        [OperationContract]
        IEnumerable<Contact> GetEmployerContacts(int employerId);

        // EmContact Operations

        [OperationContract]
        EmergencyContact AddNewEmContact(EmergencyContact person);

        [OperationContract]
        IEnumerable<EmergencyContact> GetEmContacts();

        [OperationContract]
        EmergencyContact GetEmContactById(int id);

        [OperationContract]
        EmergencyContact ModifyEmContact(EmergencyContact person, int id);

        [OperationContract]
        void DeleteEmContactById(int id);

        // Field Operations

        [OperationContract]
        Field AddNewField(Field field);

        [OperationContract]
        IEnumerable<Field> GetFields();

        [OperationContract]
        Field GetFieldById(int id);

        [OperationContract]
        Field ModifyField(Field field, int id);

        [OperationContract]
        void DeleteFieldById(int id);

        // Focus Operations

        [OperationContract]
        Focus AddNewFocus(Focus focus);

        [OperationContract]
        IEnumerable<Focus> GetFoci();

        [OperationContract]
        Focus GetFocusById(int id);

        [OperationContract]
        Focus ModifyFocus(Focus focus, int id);

        [OperationContract]
        void DeleteFocusById(int id);

        [OperationContract]
        void AddExistingFocusToField(int focusId, int fieldId);

        [OperationContract]
        Focus AddFocusToField(Focus focus, int fieldId);

    }

}
