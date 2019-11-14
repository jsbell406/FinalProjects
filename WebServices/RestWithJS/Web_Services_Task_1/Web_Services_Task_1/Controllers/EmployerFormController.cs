//  Name . . . : James Bell
//  Class. . . : CSCI-257 Web Services
//  Instructor : Bryon Steinwand
//  Date . . . : 11/17/2018
//  Assignment : Task One
//  File . . . : EmployerFormController.cs
//  Notes. . . : Controller for Employer Form View

using System.Web.Mvc;

namespace Web_Services_Task_1.Controllers
{
    public class EmployerFormController : Controller
    {
        // Direct user to employer form page
        // GET: EmployerForm
        public ActionResult Index()
        {
            return View();
        }
    }
}