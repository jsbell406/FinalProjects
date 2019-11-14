//  Name . . . : James Bell
//  Class. . . : CSCI-257 Web Services
//  Instructor : Bryon Steinwand
//  Date . . . : 11/17/2018
//  Assignment : Task One
//  File . . . : DisciplineController.cs
//  Notes. . . : I did use the entity framework code generation feature for this class.  
//               I was crashing the program when attempting to send an empty request while using the return type HttpResponseMessage.
//               I used code generation in this class to help troubleshoot the problem I was having.  
//               You will notice bits that are clearly taken from the code generation litered through the other controllers.

using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Web_Services_Task_1.Models;

namespace Web_Services_Task_1.Controllers
{
    public class DisciplineController : ApiController
    {
        // Database connection
        private DbCon db = new DbCon();

        // GET: All disciplines
        // PATH: api/discipline
        public IQueryable<Discipline> GetDisciplines()
        {
            return db.Disciplines;
        }

        // GET: singular discipline by id
        // PATH: api/discipline/{id}
        [ResponseType(typeof(Discipline))]
        public IHttpActionResult GetDiscipline(int id)
        {
            // finds discipline or throws not found if null
            Discipline discipline = db.Disciplines.Find(id);
            if (discipline == null)
            {
                return NotFound();
            }

            return Ok(discipline);
        }

        // PUT: updates discipline given new discipline object and id
        // PATH: api/discipline/{id}
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDiscipline(int id, Discipline discipline)
        {
            // If Discipline model is not valid, throw a bad request
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // If the primary key's don't match, throw a bad request
            if (id != discipline.DisciplineId)
            {
                return BadRequest();
            }

            // track that the entity has been modified
            db.Entry(discipline).State = EntityState.Modified;


            // Save changes, or the id doesn't exist, throw a not found
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DisciplineExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: inserts new discipline given the new discipline object
        // PATH: api/discipline
        [ResponseType(typeof(Discipline))]
        public IHttpActionResult PostDiscipline(Discipline discipline)
        {
            // if the model is not valid throw a bad request
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // add valid discipline to db
            db.Disciplines.Add(discipline);

            // save changes 
            db.SaveChanges();

            // redirect to newly created discipline
            return CreatedAtRoute("DefaultApi", new { id = discipline.DisciplineId }, discipline);
        }

        // DELETE: removes discipline by id
        // PATH: api/discipline/{id}
        [ResponseType(typeof(Discipline))]
        public IHttpActionResult DeleteDiscipline(int id)
        {
            // Find discipline or throw a not found
            Discipline discipline = db.Disciplines.Find(id);
            if (discipline == null)
            {
                return NotFound();
            }

            // remove discipline from database
            db.Disciplines.Remove(discipline);

            // save changes
            db.SaveChanges();

            return Ok(discipline);
        }


        

       // -----------------------------------------------------------------------------

       // Dispose method came from using code generation. 
       // not really sure what it does.
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // Came from code generation
        // Pretty straight forward method
        private bool DisciplineExists(int id)
        {
            return db.Disciplines.Count(e => e.DisciplineId == id) > 0;
        }
    }
}