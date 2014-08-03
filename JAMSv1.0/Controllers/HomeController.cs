using System;
using System.IO;
using System.Web.Mvc;
using JAMSv1._0.Models;

namespace JAMSv1._0.Controllers
{
    /// <summary>
    /// Controller that holds home page functionality
    /// </summary>
    public class HomeController : ApplicationController
    {
        /// <summary>
        /// GET: /Home/Register
        /// </summary>
        /// <returns>View of index</returns>
        public ActionResult Index()
        {
            return View(); 
        }

        /// <summary>
        /// GET: /Home/Upload
        /// </summary>
        /// <returns>View of upload</returns>
        public ActionResult Upload(int? jobId)
        {
            return View();
        }

        /// <summary>
        /// POST: /Home/Upload
        /// </summary>
        /// <param name="resume">Resume</param>
        /// <param name="command">string</param>
        /// <returns>Resume added and continue</returns>
        [HttpPost]
        [Authorize]
        public ActionResult Upload(Resume resume, string command)
        {
            //This block of code does nothing. I left it here just in case somebody else was using it.
            //if (command == "Next")
            //{
            //    RedirectToAction("Create", "Accomplishments");
            //}
            try
            {
                if (resume.File.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(resume.File.FileName);
                    string path = Path.Combine(Server.MapPath("~/Content/Resumes"), fileName);
                    resume.File.SaveAs(path);
                }
                TempData["notice"] = "Resume Added:  "+ resume.File.FileName;
                return View(resume);
            }
            catch (Exception)
            {
                ViewBag.Message = "Upload Error";
                return View("Upload");
            }
        }

        /// <summary>
        /// Contact page (unused)
        /// </summary>
        /// <returns>View of contact</returns>
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}