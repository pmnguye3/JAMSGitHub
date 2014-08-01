using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JAMSv1._0.Models
{
    /// <summary>
    /// Job class for new job postings by administrator
    /// </summary>
    public class Job
    {
        /// <summary>
        /// primary key for tracking jobs
        /// </summary>
        [Required]
        public int JobId { get; set; }

        /// <summary>
        /// Name of the job 
        /// </summary>
        [Display(Name = "Position")]
        [Required]
        public string JobName { get; set; }

        /// <summary>
        /// Describes the job duties and responsibilities
        /// </summary>
        [Display(Name = "Description")]
        [Required]
        public string JobDescription { get; set; }

        /// <summary>
        /// Lets the user know who posted the job
        /// </summary>
        [Display(Name = "Posted By")]
        [Required]
        public string JobPostedBy { get; set; }

        /// <summary>
        /// Date the position was made available
        /// </summary>
        [Display(Name = "Position Posted On")]
        [Required]
        public DateTime JobStartDate { get; set; }

        /// <summary>
        /// State whether this is a .Net or Java type
        /// </summary>
        [Display(Name="Job Type")]
        [Required]
        public string JobType { get; set; }

    }
}