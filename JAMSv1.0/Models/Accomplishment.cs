using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JAMSv1._0.Models
{
    /// <summary>
    /// Accomplisment class to store applicant's accomplishments
    /// </summary>
    public class Accomplishment
    {
        /// <summary>
        /// primary key for accomplishments
        /// </summary>
        public int AccomplishmentID { get; set; }

        /// <summary>
        /// Accomplishment date number one
        /// </summary>

        [Display(Name="Date Accomplished")]
        public DateTime? AccomplishmentDate1 { get; set; }

        [Display(Name = "Accomplishment Details")]
        /// <summary>
        /// Accomplishment number one
        /// </summary>
        public string Accomplishment1 { get; set; }
        /// <summary>
        /// Accomplishment date number two
        /// </summary>

        [Display(Name = "Date Accomplished")]
        public DateTime? AccomplishmentDate2 { get; set; }
        /// <summary>
        /// Accomplishment number two
        /// </summary>

        [Display(Name = "Accomplishment Details")]
        public string Accomplishment2 { get; set; }

        /// <summary>
        /// Accomplishment date number three
        /// </summary>

        [Display(Name = "Date Accomplished")]
        public DateTime? AccomplishmentDate3 { get; set; }

        /// <summary>
        /// Accomplishment date number three
        /// </summary>
        [Display(Name = "Accomplishment Details")]
        public string Accomplishment3 { get; set; }


    }
}