using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JAMSv1._0.Models
{
    /// <summary>
    /// Resume class to store resume to file structure
    /// </summary>
    public class Resume
    {
        [Key]
        public int ResumeId { get; set; }

        public HttpPostedFileBase File  { get; set; }

    }
}