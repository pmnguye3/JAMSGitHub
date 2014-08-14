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
using Microsoft.AspNet.Identity;
using JAMSv1._0.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace JAMSv1._0.Controllers
{
    /// <summary>
    /// Controller for questions and quiz functionality
    /// </summary>
    public class QuestionsController : ApplicationController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// GET: Questions/Index
        /// </summary>
        /// <returns>View of index with quiz questions</returns>
        public ActionResult Index()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(User.Identity.GetUserId());
            currentUser.AccomplishmentComplete = true;
            manager.UpdateAsync(currentUser);

            Quiz newQuiz = new Quiz();
            Random rand = new Random();
            List<Question> questionList = new List<Question>(db.Questions.ToList().Count);
            foreach (var item in db.Questions.ToList())
            {
                questionList.Add(item);
            }
            int counter = 0;

            do
            {
                //var index = rand.Next() % db.Questions.ToList().Count();
                var index = rand.Next() % questionList.Count;
                //var k = db.Questions.ToList()[index];
                var k = questionList[index];
                //db.Questions.ToList().RemoveAt(index);
                

                foreach (var question in db.Questions.ToList())
                {
                    if (question == k && question.JobId == currentUser.JobId)
                    {
                        newQuiz.Questions.Add(question);
                        counter++;
                    }
                }
                questionList.RemoveAt(index);
                
                
            } while (counter < 5);
                
            
            return View(newQuiz);
        }

        /// <summary>
        /// POST: Questions/Index
        /// </summary>
        /// <param name="model">Quiz model</param>
        /// <param name="File">Resume file</param>
        /// <returns>Thank you page</returns>
        [HttpPost]
        public ActionResult Index(Quiz model)
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
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var currentUser = manager.FindById(User.Identity.GetUserId());
                currentUser.PrescreeningComplete = true;
                manager.UpdateAsync(currentUser);

                Job newJob = new Job();

                foreach (var job in db.Jobs.ToList())
                {
                    if (job.JobId == currentUser.JobId)
                    {
                        newJob = job;
                    }
                }

                SendEmail(model, newJob);
                currentUser.ApplicationComplete = true;
                return RedirectToAction("ThankYou");
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
            Job job = db.Jobs.Find(question.JobId);
            ViewBag.tempJobName = job.JobName;
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
            ViewBag.JobId = new SelectList(db.Jobs, "JobId", "JobName");
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
            [Bind(Include = "QuestionID,QuestionString,Answer1,Answer2,Answer3,Answer4,CorrectAnswer,JobId")] Question
                question)
        {
            if (ModelState.IsValid)
            {
                db.Questions.Add(question);
                db.SaveChanges();
                return RedirectToAction("ListAllQuestions");
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
        public ActionResult ThankYou(Quiz model)
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
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var currentUser = manager.FindById(User.Identity.GetUserId());
                Job newJob = new Job();

                foreach (var job in db.Jobs.ToList())
                {
                    if (job.JobId == currentUser.JobId)
                    {
                        newJob = job;
                    }
                }
                if (currentUser.ApplicationComplete != true)
                {
                    SendEmail(model, newJob);
                }
                
                // Here is some code to uncomment in order to reset the workflow and start from the beginning

                currentUser.ApplyComplete = false;
                currentUser.UploadComplete = false;
                currentUser.AccomplishmentComplete = false;
                currentUser.PrescreeningComplete = false;
                currentUser.ResumeFilePath = null;
                manager.UpdateAsync(currentUser);
                
                return View("ThankYou");
            }
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

        private void SendEmail(Quiz model, Job jobModel)
        {
            using (MailMessage mail = new MailMessage("jams.cis440@gmail.com", "jams.cis440@gmail.com"))
            {
                mail.Body = "Date: "+ DateTime.Today + "\nName: " + GetFullName() + "\nApplicant got " + model.rightAnswers + " answers right out of 5. \n\n" + 
                    "Job Name: " + jobModel.JobName + "\nJob Type: " + jobModel.JobType;
                mail.Subject = ("Applicant: " + GetFullName() + " has applied for " + jobModel.JobType + " job: " + jobModel.JobName + ".");
                mail.Attachments.Add(new Attachment(GetResumeFilePath()));

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = new NetworkCredential("jams.cis440@gmail.com", "whatalegitpassword");
                smtp.Send(mail);
            }
        }
    }
}