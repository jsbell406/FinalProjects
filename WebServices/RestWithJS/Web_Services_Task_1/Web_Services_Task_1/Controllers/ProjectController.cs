//  Name . . . : James Bell
//  Class. . . : CSCI-257 Web Services
//  Instructor : Bryon Steinwand
//  Date . . . : 11/17/2018
//  Assignment : Task One
//  File . . . : ProjectController.cs
//  Notes. . . : Controller for Project Resource

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web_Services_Task_1.Models;
using System.Data.Entity;
using System.Data;

namespace Web_Services_Task_1.Controllers
{
    public class ProjectController : ApiController
    {
        // database connection
        private DbCon db = new DbCon();


        // GET: Get all projects
        // PATH: api/project
        public IEnumerable<Project> GetProjects()
        {
            return db.Projects.AsEnumerable();
        }


        // GET: Get single project by id
        // PATH: api/project/{id}
        public IHttpActionResult GetProject(int id)
        {
            Project proj = db.Projects.Find(id);

            if (proj == null)
                return NotFound();

            return Ok(proj);
        }


        // POST: Inserts project given new project object
        // PATH: api/project/{id}
        public HttpResponseMessage PostProject(Project project)
        {
            // Model validation
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            // Add project to database
            db.Projects.Add(project);

            // save changes
            db.SaveChanges();

            // Get project by id providing the project id of the new project
            HttpResponseMessage reponse = Request.CreateResponse(HttpStatusCode.Created, project);
            reponse.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = project.ProjectId }));
            return reponse;

        }


        // PUT: Replaces project with new project object with matching id
        public HttpResponseMessage PutProject(Project project, int id)
        {

            // Validate project model and verify matching IDs
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            if (project.ProjectId != id)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "BAD IDS");

            // inform the context that the entity has been modified
            db.Entry(project).State = EntityState.Modified;


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


        // DELETE: Removes the project by id
        // PATH: api/project/{id}
        public HttpResponseMessage DeleteProject(int id)
        {
            // find the project with the provided Id
            Project proj = db.Projects.Find(id);

            // Check if project was found
            if (proj == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);


            // Remove project from database
            db.Projects.Remove(proj);

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
