using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JAMSv1._0.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace JAMSv1._0.Controllers
{
    /// <summary>
    /// Controller for Accomplishments with CRUD functionality
    /// </summary>
    public class AccomplishmentsController : ApplicationController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// GET: Accomplishments/Index
        /// </summary>
        /// <returns>View list of accomplishments</returns>
        public ActionResult Index()
        {
            //Changes I tried to get only the users Accomplishments to display, this broke all the views though.
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(User.Identity.GetUserId());
            return View(currentUser.Accomplishment);
            //return View(db.Accomplishments.ToList());
        }

        /// <summary>
        /// GET: Accomplishments/Details
        /// </summary>
        /// <param name="id">Accomplishment ID</param>
        /// <returns>Accomplishment Details</returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accomplishment accomplishment = db.Accomplishments.Find(id);
            if (accomplishment == null)
            {
                return HttpNotFound();
            }
            return View(accomplishment);
        }

        /// <summary>
        /// GET: Accomplishments/Create
        /// </summary>
        /// <returns>Create view</returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// POST: Accomplishments/Create
        /// </summary>
        /// <param name="accomplishment">Stores user input</param>
        /// <returns>View to Index</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccomplishmentID,AccomplishmentDate1,Accomplishment1,AccomplishmentDate2,Accomplishment2,AccomplishmentDate3,Accomplishment3")] Accomplishment accomplishment)
        {
            if (ModelState.IsValid)
            {
                db.Accomplishments.Add(accomplishment);
                db.SaveChanges();
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var currentUser = manager.FindById(User.Identity.GetUserId());
                currentUser.Accomplishment = accomplishment;
                manager.UpdateAsync(currentUser);
                return RedirectToAction("Index");
            }

            return View(accomplishment);
        }

        /// <summary>
        /// GET: Accomplishments/Edit/
        /// </summary>
        /// <param name="id">Accomplishment Id</param>
        /// <returns>Details of the Accomplishment</returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accomplishment accomplishment = db.Accomplishments.Find(id);
            if (accomplishment == null)
            {
                return HttpNotFound();
            }
            return View(accomplishment);
        }

        /// <summary>
        /// POST: Accomplishments/Edit/
        /// </summary>
        /// <param name="accomplishment">User Input</param>
        /// <returns>View to Index</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccomplishmentID,AccomplishmentDate1,Accomplishment1,AccomplishmentDate2,Accomplishment2,AccomplishmentDate3,Accomplishment3")] Accomplishment accomplishment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accomplishment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accomplishment);
        }

        /// <summary>
        /// GET: Accomplishments/Delete/
        /// </summary>
        /// <param name="id">Accomplishment Id</param>
        /// <returns>Accomplishment Index</returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accomplishment accomplishment = db.Accomplishments.Find(id);
            if (accomplishment == null)
            {
                return HttpNotFound();
            }
            return View(accomplishment);
        }

        /// <summary>
        /// POST: Accomplishments/Delete
        /// </summary>
        /// <param name="id">Accomplishment Id</param>
        /// <returns>View to Index</returns>
 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Accomplishment accomplishment = db.Accomplishments.Find(id);
            db.Accomplishments.Remove(accomplishment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        /// <summary>
        /// Dispose of Accomplishment
        /// </summary>
        /// <param name="disposing">Accomplishment to Dispose</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
