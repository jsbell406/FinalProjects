//  Name . . . : James Bell
//  Class. . . : CSCI-257 Web Services
//  Instructor : Bryon Steinwand
//  Date . . . : 11/17/2018
//  Assignment : Task One
//  File . . . : StudentController.cs
//  Notes. . . : Controller for Student Resource

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web_Services_Task_1.Models;

namespace Web_Services_Task_1.Controllers
{
    public class StudentController : ApiController
    {

        // establish connecting to database
        private DbCon db = new DbCon();


        // GET: Get all students that are with in a requested discipline. Return includes students discipline information
        // PATH:
        [Route("api/discipline/{disciplineId}/student", Name = "getStudentsInDiscipline")]
        public IEnumerable<Student> GetStudentsWithinDiscipline(int disciplineId)
        {
            return db.Students.Where(x => x.Discipline.DisciplineId == disciplineId).Include(d => d.Discipline).AsEnumerable();
        }


        // POST: Insert a new student with in a requested discipline given the disciplineId
        // PATH:
        [Route("api/discipline/{disciplineId}/student", Name = "postStudentInDiscipline")]
        public HttpResponseMessage PostStudentWithinDiscipline(Student student, int disciplineId)
        {

            // Find the requested discipline with provided disciplineId
            Discipline dis = db.Disciplines.Find(disciplineId);

            // Validate student model and check if requested discipline exists
            if(dis == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            if(!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            // set students discipline to the requested discipline
            student.Discipline = dis;

            // add student to database
            db.Students.Add(student);

            // save changes
            db.SaveChanges();

            // Redirect to all students within the requested discipline
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, student);
            response.Headers.Location = new Uri(Url.Link("getStudentsInDiscipline", new { disciplineId = dis.DisciplineId }));

            return response;
        }


        // GET: Get all students including their disciplines
        // PATH: api/student
        public IEnumerable<Student> GetStudents()
        {
            return db.Students.Include(s => s.Discipline).AsEnumerable();
        }

        // GET: Get single student by id
        // PATH: api/student/{id}
        public IHttpActionResult GetStudent(int id)
        {
            // find requested student
            Student stud = db.Students.Find(id);

            // verify that student exists
            if (stud == null)
                return NotFound();
            return Ok(stud);
        }

        // POST: Insert student with the provided new student object
        // PATH: api/student
        public HttpResponseMessage PostStudent(Student student)
        {

            // Validate student object
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            // add student to database
            db.Students.Add(student);

            // save changes
            db.SaveChanges();


            // Redirect to GetStudent providing the new student's id
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created,student);
            response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = student.StudentId }));
            return response;
        }

        // PUT: Replace student with new student object and matching id
        // PATH: api/student/{id}
        public HttpResponseMessage PutStudent(Student student, int id)
        {
            // validate object and verify matching ids
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            if (student.StudentId != id)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "INCORRECT IDS");

            // inform context the student entry has been modified
            db.Entry(student).State = EntityState.Modified;
            

            // save changes
            try
            {
                db.SaveChanges();
            }
            catch(DBConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);

        }

        // DELETE: Removes student by id
        // PATH: api/student/{id}
        public HttpResponseMessage DeleteStudent(int id)
        {
            // find requested student
            Student stud = db.Students.Find(id);

            // verify student exists
            if (stud == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            // remove requested student from database
            db.Students.Remove(stud);


            // save changes
            try
            {
                db.SaveChanges();
            }
            catch(DBConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }
        
    }
}