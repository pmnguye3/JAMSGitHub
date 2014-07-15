using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JAMSv1._0.Models
{
    public class Question
    {
        public int QuestionID { get; set; }

        [Display(Name = "Question")]
        public string QuestionString { get; set; }
        [Display(Name = "Answer Choice 1")]
        public string Answer1 { get; set; }
        [Display(Name = "Answer Choice 2")]
        public string Answer2 { get; set; }
        [Display(Name = "Answer Choice 3")]
        public string Answer3 { get; set; }
        [Display(Name = "Answer Choice 4")]
        public string Answer4 { get; set; }
        [Display(Name = "Correct Answer")]
        public string CorrectAnswer { get; set; }
    }
}