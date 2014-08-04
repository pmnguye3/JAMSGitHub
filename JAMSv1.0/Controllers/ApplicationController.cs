﻿using JAMSv1._0.Models;
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
        //store filepath to upload to resume
        //problem: variable ResumeFilePath keeps resetting to null. Have no clue why.
        string ResumeFilePath;
        public void StoreResumeFilePath(string filePath)
        {
            ResumeFilePath = filePath;
        }
        public string GetResumeFilePath()
        {
            return ResumeFilePath;
        }

        public ApplicationController()
        {

        }
    }
}
