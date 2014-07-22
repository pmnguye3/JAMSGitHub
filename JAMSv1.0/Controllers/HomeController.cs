using System;
using System.IO;
using System.Web.Mvc;
using JAMSv1._0.Models;

namespace JAMSv1._0.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Upload(Resume resume, string command)
        {
            if (command == "Next")
            {
                RedirectToAction("Create", "Accomplishments");
            }
            try
            {
                if (resume.File.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(resume.File.FileName);
                    string path = Path.Combine(Server.MapPath("~/Content/Resumes"), fileName);
                    resume.File.SaveAs(path);
                }
                TempData["notice"] = "Resume Added";
                return RedirectToAction("Upload");
            }
            catch (Exception)
            {
                ViewBag.Message = "Upload Error";
                return View("Upload");
            }
        }

        public ActionResult Upload()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}