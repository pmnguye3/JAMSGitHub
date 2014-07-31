using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using JAMSv1._0.Models;

namespace JAMSv1._0.Controllers
{
    public class RolesController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        /// <summary>
        /// GET: /Roles/Index
        /// </summary>
        /// <returns>View of roles index</returns>
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var roles = context.Roles.ToList();
            return View(roles);
        }

        /// <summary>
        /// GET: /Roles/Create
        /// </summary>
        /// <returns>View of roles create</returns>
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// POST: /Roles/Create
        /// </summary>
        /// <param name="collection">Form collection</param>
        /// <returns>View of roles index</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                context.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole()
                {
                    Name = collection["RoleName"]
                });
                context.SaveChanges();
                ViewBag.ResultMessage = "Role created successfully !";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// GET: /Roles/Edit
        /// </summary>
        /// <param name="roleName">string</param>
        /// <returns>View of selected role</returns>
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(string roleName)
        {
            var thisRole = context.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            return View(thisRole);
        }

        /// <summary>
        /// POST: /Roles/Edit
        /// </summary>
        /// <param name="role">Identity role</param>
        /// <returns>View of roles index</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Microsoft.AspNet.Identity.EntityFramework.IdentityRole role)
        {
            try
            {
                context.Entry(role).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// GET: /Roles/Delete
        /// </summary>
        /// <param name="RoleName">string</param>
        /// <returns>View of roles index</returns>
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string RoleName)
        {
            var thisRole = context.Roles.Where(r => r.Name.Equals(RoleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            context.Roles.Remove(thisRole);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// GET: /Roles/ManageUserRoles
        /// </summary>
        /// <returns>View of manage user roles</returns>
        [Authorize(Roles = "Admin")]
        public ActionResult ManageUserRoles()
        {
            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;
            return View();
        }

        /// <summary>
        /// POST: /Roles/AddUserRoles
        /// </summary>
        /// <param name="UserName">string</param>
        /// <param name="RoleName">string</param>
        /// <returns>View of manage user roles</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult AddUserRoles(string UserName, string RoleName)
        {
            ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            var account = new AccountController();
            account.UserManager.AddToRole(user.Id, RoleName);

            ViewBag.ResultMessage = "Role created successfully !";

            // prepopulat roles for the view dropdown
            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;

            return View("ManageUserRoles");
        }

        /// <summary>
        /// POST: /Roles/GetRoles
        /// </summary>
        /// <param name="UserName">string</param>
        /// <returns>View of manage user roles</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult GetRoles(string UserName)
        {
            if (!string.IsNullOrWhiteSpace(UserName))
            {
                ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                var account = new AccountController();

                ViewBag.RolesForThisUser = account.UserManager.GetRoles(user.Id);

                // prepopulat roles for the view dropdown
                var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
                ViewBag.Roles = list;
            }

            return View("ManageUserRoles");
        }

        /// <summary>
        /// POST: /Roles/DeleteUserRoles
        /// </summary>
        /// <param name="UserName">string</param>
        /// <param name="RoleName">string</param>
        /// <returns>View of manage user roles</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteUserRoles(string UserName, string RoleName)
        {
            var account = new AccountController();
            ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            if (account.UserManager.IsInRole(user.Id, RoleName))
            {
                account.UserManager.RemoveFromRole(user.Id, RoleName);
                ViewBag.ResultMessage = "Role removed from this user successfully !";
            }
            else
            {
                ViewBag.ResultMessage = "This user doesn't belong to selected role.";
            }
            // prepopulat roles for the view dropdown
            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;

            return View("ManageUserRoles");
        }
    }
}