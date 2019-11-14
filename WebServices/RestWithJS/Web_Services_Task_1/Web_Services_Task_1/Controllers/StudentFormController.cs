//  Name . . . : James Bell
//  Class. . . : CSCI-257 Web Services
//  Instructor : Bryon Steinwand
//  Date . . . : 11/17/2018
//  Assignment : Task One
//  File . . . : StudentFormController.cs
//  Notes. . . : Controller for Student Form View

using System.Web.Mvc;

namespace Web_Services_Task_1.Controllers
{
    public class StudentFormController : Controller
    {
        // GET: StudentForm
        public ActionResult Index()
        {
            return View();
        }
    }
}