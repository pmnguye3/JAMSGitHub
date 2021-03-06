﻿using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace JAMSv1._0.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public override string PhoneNumber { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }

        public bool ApplyComplete { get; set; }
        public bool UploadComplete { get; set; }
        public bool AccomplishmentComplete { get; set; }
        public bool PrescreeningComplete { get; set; }
        public bool ApplicationComplete { get; set; }
        public string ResumeFilePath { get; set; }
        public int JobId { get; set; }

        public virtual Accomplishment Accomplishment { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<JAMSv1._0.Models.Question> Questions { get; set; }

        public System.Data.Entity.DbSet<JAMSv1._0.Models.Accomplishment> Accomplishments { get; set; }

        public System.Data.Entity.DbSet<JAMSv1._0.Models.Job> Jobs { get; set; }
    }
}