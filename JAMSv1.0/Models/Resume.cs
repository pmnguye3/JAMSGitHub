using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JAMSv1._0.Models
{
    public class Resume
    {
        [Key]
        public int ResumeId { get; set; }

        public HttpPostedFileBase File  { get; set; }

    }
    //Test Mensur Hussien
    //Test Mo Moorthy
}