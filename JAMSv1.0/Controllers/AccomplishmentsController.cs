using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JAMSv1._0.Models;

namespace JAMSv1._0.Controllers
{
    public class AccomplishmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Accomplishments
        public ActionResult Index()
        {
            return View(db.Accomplishments.ToList());
        }

        // GET: Accomplishments/Details/5
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

        // GET: Accomplishments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Accomplishments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccomplishmentID,AccomplishmentDate1,Accomplishment1,AccomplishmentDate2,Accomplishment2,AccomplishmentDate3,Accomplishment3")] Accomplishment accomplishment)
        {
            if (ModelState.IsValid)
            {
                db.Accomplishments.Add(accomplishment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(accomplishment);
        }

        // GET: Accomplishments/Edit/5
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

        // POST: Accomplishments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Accomplishments/Delete/5
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

        // POST: Accomplishments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Accomplishment accomplishment = db.Accomplishments.Find(id);
            db.Accomplishments.Remove(accomplishment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
