using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Chatter.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        //public ICollection<object> Followers { get; internal set; }



        
        public virtual ICollection<ApplicationUser> Following { get; set; }
        public virtual ICollection<ApplicationUser> Followers { get; set; }
        



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


        public class MyEntities : DbContext
        {
            public DbSet<ApplicationUser> ApplicationUser { get; set; }

            protected override void OnModelCreating(DbModelBuilder modelBuilder)

            {
                modelBuilder.Entity<ApplicationUser>()
                    .HasMany(x => x.Followers).WithMany(x => x.Following)
                    .Map(x => x.ToTable("Followers")
                    .MapLeftKey("ApplicationUserId")
                    .MapRightKey("FollowerId"));
                base.OnModelCreating(modelBuilder);
            }
        }

        public System.Data.Entity.DbSet<WebApplication3.Models.Message> Messages { get; set; }

        public System.Data.Entity.DbSet<Chatter.Models.ApplicationUser> ApplicationUsers { get; set; }

        //public System.Data.Entity.DbSet<WebApplication3.Models.Message> Messages { get; set; }

        //public System.Data.Entity.DbSet<Chatter.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}