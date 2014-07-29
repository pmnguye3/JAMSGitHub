using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using JAMSv1._0.Models;

namespace JAMSv1._0.Controllers
{
    public class QuestionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Questions
        public ActionResult Index()
        {
            Quiz newQuiz = new Quiz();
            //newQuiz.Questions = db.Questions.ToList();
            Random rand = new Random();
            //List<int> numberList = new List<int>();
            //for (int i = 0; i < newQuiz.Questions.Count(); i++)
            //{
            //    numberList.Add(i + 1);
            //}
            for (int i = 0; i < 5; i++)
            {
                var index = rand.Next()%db.Questions.ToList().Count();
                var k = db.Questions.ToList()[index];
                db.Questions.ToList().RemoveAt(index);

                foreach (var question in db.Questions.ToList())
                {
                    if (question == k)
                    {
                        newQuiz.Questions.Add(question);
                    }
                }
            }
            return View(newQuiz);
        }

        [HttpPost]

        //the HttpPostedFileBase File is the "resume" object that we are passing in. I think its not passing in correctly thats why the resume won't attach.
        public ActionResult Index(Quiz model, HttpPostedFileBase File)
        {
            if (ModelState.IsValid)
            {
                foreach (Question q in model.Questions)
                {
                    if (q.SelectedAnswer == q.CorrectAnswer)
                    {
                        model.rightAnswers++;
                    }
                }

                //Auto send email like a boss.
                using (MailMessage mail = new MailMessage("jams.cis440@gmail.com", "jams.cis440@gmail.com"))
                {
                    mail.Body = "Test Email Body";
                    mail.Subject = "Test Email Subject";
                    if (File != null)
                    {
                        string fileName = Path.GetFileName(File.FileName);
                        mail.Attachments.Add(new Attachment(File.InputStream, fileName));

                    }
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = new NetworkCredential("jams.cis440@gmail.com", "whatalegitpassword");
                    smtp.Send(mail);
                    return RedirectToAction("ThankYou");
                }
                
            }
            
            return View(model);
        }

        // GET: Questions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // GET: Questions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "QuestionID,QuestionString,Answer1,Answer2,Answer3,Answer4,CorrectAnswer")] Question
                question)
        {
            if (ModelState.IsValid)
            {
                db.Questions.Add(question);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(question);
        }

        // GET: Questions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "QuestionID,QuestionString,Answer1,Answer2,Answer3,Answer4,CorrectAnswer")] Question
                question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(question);
        }

        // GET: Questions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = db.Questions.Find(id);
            db.Questions.Remove(question);
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

        public ActionResult ThankYou()
        {
            return View("ThankYou");
        }
    }
}