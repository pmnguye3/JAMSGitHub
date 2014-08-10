using JAMSv1._0.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace JAMSv1._0.Controllers
{
    /// <summary>
    /// Controller to setup the first and last name to display at login
    /// </summary>
    public abstract class ApplicationController : Controller
    {
        static string ResumeFilePath;
        static string fullName;

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (User != null)
            {
                var context = new ApplicationDbContext();
                var username = User.Identity.Name;

                if (!string.IsNullOrEmpty(username))
                {
                    var user = context.Users.SingleOrDefault(u => u.UserName == username);
                    fullName = string.Concat(new string[] { user.FirstName, " ", user.LastName });
                    ViewData.Add("FullName", fullName);
                }
            }
            base.OnActionExecuted(filterContext);
        }

        public void StoreResumeFilePath(string filePath) {ResumeFilePath = filePath;}

        public string GetResumeFilePath()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(User.Identity.GetUserId());
            return currentUser.ResumeFilePath;
        }

        public string GetFullName() { return fullName; }

        public ApplicationController()
        {

        }
    }
}
