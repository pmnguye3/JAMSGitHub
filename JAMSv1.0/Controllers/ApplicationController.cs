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
        /// <summary>
        /// Allows logged in user's first and last name to display
        /// </summary>
        /// <param name="filterContext">Action executed context</param>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (User != null)
            {
                var context = new ApplicationDbContext();
                var username = User.Identity.Name;

                if (!string.IsNullOrEmpty(username))
                {
                    var user = context.Users.SingleOrDefault(u => u.UserName == username);
                    string fullName = string.Concat(new string[] { user.FirstName, " ", user.LastName });
                    ViewData.Add("FullName", fullName);
                }
            }
            base.OnActionExecuted(filterContext);
        }

        public ApplicationController()
        {

        }
    }
}