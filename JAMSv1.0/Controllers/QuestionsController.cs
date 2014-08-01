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
    /// <summary>
    /// Controller for questions and quiz functionality
    /// </summary>
    public class QuestionsController :ApplicationController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// GET: Questions/Index
        /// </summary>
        /// <returns>View of index with quiz questions</returns>
        public ActionResult Index()
        {
            Quiz newQuiz = new Quiz();
            Random rand = new Random();

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

        /// <summary>
        /// POST: Questions/Index
        /// </summary>
        /// <param name="model">Quiz model</param>
        /// <param name="File">Resume file</param>
        /// <returns>Thank you page</returns>
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

        /// <summary>
        /// GET: Questions/Details
        /// </summary>
        /// <param name="id">Question id</param>
        /// <returns>Question detail</returns>
        [Authorize(Roles = "Admin")]
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

        /// <summary>
        /// GET: Questions/Create
        /// </summary>
        /// <returns>Create view</returns>
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// POST: Questions/Create
        /// </summary>
        /// <param name="question">Question</param>
        /// <returns>View of index</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
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

        /// <summary>
        /// GET: Questions/Edit
        /// </summary>
        /// <param name="id">Question id</param>
        /// <returns>View of edit questions</returns>
        [Authorize(Roles = "Admin")]
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

        /// <summary>
        /// POST: Questions/Edit
        /// </summary>
        /// <param name="question">Question id</param>
        /// <returns>View of question index</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(
            [Bind(Include = "QuestionID,QuestionString,Answer1,Answer2,Answer3,Answer4,CorrectAnswer")] Question
                question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListAllQuestions");
            }
            return View(question);
        }

        /// <summary>
        /// GET: Questions/Delete
        /// </summary>
        /// <param name="id">Question id</param>
        /// <returns>View of question delete</returns>
        [Authorize(Roles = "Admin")]
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

        /// <summary>
        /// POST: Questions/Delete
        /// </summary>
        /// <param name="id">Question id</param>
        /// <returns>View of quesstions index</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = db.Questions.Find(id);
            db.Questions.Remove(question);
            db.SaveChanges();
            return RedirectToAction("ListAllQuestions");
        }
        /// <summary>
        /// GET: Questions/Dispose
        /// </summary>
        /// <param name="disposing">bool</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        /// <summary>
        /// GET: Questions/ThankYou
        /// </summary>
        /// <returns>View of questions thankyou</returns>
        public ActionResult ThankYou()
        {
            return View("ThankYou");
        }

        /// <summary>
        /// GET: Questions/ListAllQuestions
        /// </summary>
        /// <returns>View of all questions</returns>
        [Authorize(Roles = "Admin")]
        public ActionResult ListAllQuestions()
        {
            List<Question> questions = db.Questions.ToList();
            return View(questions);
        }
    }
}