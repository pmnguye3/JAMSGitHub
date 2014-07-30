using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JAMSv1._0.Models
{
    /// <summary>
    /// Quiz class to generate questions and store correct answer
    /// </summary>
    public class Quiz
    {
        public List<Question> Questions { set; get; }

        public int rightAnswers = 0;

        public Quiz()
        {
            Questions = new List<Question>();
        }
    }
}