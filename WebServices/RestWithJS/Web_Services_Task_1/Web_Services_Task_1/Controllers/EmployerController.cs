//  Name . . . : James Bell
//  Class. . . : CSCI-257 Web Services
//  Instructor : Bryon Steinwand
//  Date . . . : 11/17/2018
//  Assignment : Task One
//  File . . . : EmployerController.cs
//  Notes. . . : Controller for Employer resource

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
    public class EmployerController : ApiController
    {

        // Datavase connection
        private DbCon db = new DbCon();


        // GET: Get all employers
        // PATH: api/employer
        public IEnumerable<Employer> GetEmployers()
        {
            return db.Employers.AsEnumerable();
        }

        // GET: Get a single employer by id
        // PATH: api/employer/{id}
        public IHttpActionResult GetEmployer(int id)
        {
            Employer emp = db.Employers.Find(id);

            if (emp == null)
                return NotFound();
            return Ok(emp);
        }

        // POST: inserts a new employer
        // PATH: api/employer
        public HttpResponseMessage PostEmployer(Employer employer)
        {
            // Employer object needs to match requirements established in the employer model class
            if (ModelState.IsValid)
            {
                db.Employers.Add(employer);
                db.SaveChanges();

                // Redirect to get employer by id and provide ID of newly created employer
                HttpResponseMessage reponse = Request.CreateResponse(HttpStatusCode.Created, employer);
                reponse.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = employer.EmployerId }));
                return reponse;
            }
            else
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        // PUT: Updates employer given a new employer object and matching id
        // PATH: api/employer/{id}
        public HttpResponseMessage PutEmployer(Employer employer, int id)
        {

            // Validate request
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            if (employer.EmployerId != id)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,"Non-matching Ids");


            // inform context entity has been modified
            db.Entry(employer).State = EntityState.Modified;

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


        // DELETE: Deletes employer resource by id
        // PATH: api/employer/{id}
        public HttpResponseMessage DeleteEmployer(int id)
        {

            // find requested employer
            Employer emp = db.Employers.Find(id);

            // Check if it exists
            if (emp == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);


            // remove the employer
            db.Employers.Remove(emp);


            // Save changes
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