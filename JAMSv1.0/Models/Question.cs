using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JAMSv1._0.Models
{
    /// <summary>
    /// Question class for questions and its properties
    /// </summary>
    public class Question
    {
        public int QuestionID { get; set; }

        [Display(Name = "Question")]
        [Required]
        public string QuestionString { get; set; }
        [Display(Name = "Answer 1")]
        [Required]
        public string Answer1 { get; set; }
        [Display(Name = "Answer 2")]
        [Required]
        public string Answer2 { get; set; }
        [Display(Name = "Answer 3")]
        [Required]
        public string Answer3 { get; set; }
        [Display(Name = "Answer 4")]
        [Required]
        public string Answer4 { get; set; }
        [Display(Name = "Correct Answer")]
        [Required]
        public string CorrectAnswer { get; set; }

        [Display(Name = "Selected Answer")]
        public string SelectedAnswer { get; set; }
    }
}