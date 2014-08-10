using JAMSv1._0.Models;
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
        static ApplicationUser currentUser;
        public void StoreCurrentUser(ApplicationUser user)
        {
            currentUser = user;
        }
        public ApplicationUser GetCurrentUser()
        {
            return currentUser;
        }

        public void StoreResumeFilePath(string filePath) {ResumeFilePath = filePath;}

        public string GetResumeFilePath() {return ResumeFilePath;}

        public string GetFullName() { return fullName; }

        public ApplicationController()
        {

        }
    }
}
