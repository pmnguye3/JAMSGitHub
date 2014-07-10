using JAMSv1._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

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
        public ActionResult Index(Resume resume)
        {
            try
            {
                if (resume.File.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(resume.File.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/Resumes"), fileName);
                    resume.File.SaveAs(path);
                    
                }
                TempData["notice"] = "Resume Added";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View();
            } 
        }

        public ActionResult Upload()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}