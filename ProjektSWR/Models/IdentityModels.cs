using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.ModelConfiguration.Conventions;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace ProjektSWR.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Recipients = new List<Recipient>();
            Messages = new List<Message>();
            Notifications = new List<Notification>();
            Threads = new List<Thread>();
            PrivateEvents = new List<PrivateEvent>();
            GlobalEvents = new List<GlobalEvent>();
        }

        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }
        public string AcademicDegree { get; set; }
        public string Photo { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Description { get; set; }
        public string Homepage { get; set; }
        [Required] public virtual Cathedral CathedralID { get; set; }
        public virtual Admin AdminID { get; set; }
        public virtual NormalUser NormalUserID { get; set; }
        public bool UserConfirmed { get; set; }

        public virtual ICollection<Recipient> Recipients { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Thread> Threads { get; set; }
        public virtual ICollection<PrivateEvent> PrivateEvents { get; set; }
        public virtual ICollection<GlobalEvent> GlobalEvents { get; set; }
        

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
        public DbSet<Message> Messages { get; set; }
        public DbSet<Recipient> Recipients { get; set; }

        public DbSet<Cathedral> Cathedrals { get; set; }
        public DbSet<NormalUser> NormalUsers { get; set; }
        public DbSet<Admin> Admins { get; set; }

        public DbSet<GlobalEvent> GlobalEvents { get; set; }
        public DbSet<PrivateEvent> PrivateEvents { get; set; }
        public DbSet<Event> Events { get; set; }

        public DbSet<Thread> Threads { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Forum> Forums { get; set; }
        public DbSet<Reply> Replys { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //modelBuilder.Ignore<IdentityUserLogin>();
            //modelBuilder.Ignore<IdentityRole>();
            //modelBuilder.Ignore<IdentityUserClaim>();
            //modelBuilder.Ignore<IdentityRole>();
            //modelBuilder.Entity<IdentityUser>().Ignore(c => c.EmailConfirmed)
             //                                  .Ignore(c => c.TwoFactorEnabled);

            //modelBuilder.Entity<ApplicationUser>().ToTable("Users");
        }

        public ApplicationDbContext()
            : base("ApplicationDBContext", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    public class NormalUser
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
    }

    public class Admin
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
    }
}