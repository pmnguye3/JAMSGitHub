using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JAMSv1._0.Models
{
    public class Accomplishment
    {

        public int AccomplishmentID { get; set; }

        [Display(Name="Date Accomplished")]
        public DateTime? AccomplishmentDate1 { get; set; }

        [Display(Name = "Accomplishment Details")]
        public string Accomplishment1 { get; set; }

        [Display(Name = "Date Accomplished")]
        public DateTime? AccomplishmentDate2 { get; set; }

        [Display(Name = "Accomplishment Details")]
        public string Accomplishment2 { get; set; }

        [Display(Name = "Date Accomplished")]
        public DateTime? AccomplishmentDate3 { get; set; }

        [Display(Name = "Accomplishment Details")]
        public string Accomplishment3 { get; set; }


    }
}