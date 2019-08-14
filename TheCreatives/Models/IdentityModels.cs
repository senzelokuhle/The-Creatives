using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ModelsLogic;

namespace TheCreatives.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
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

        public DbSet<SiteUser> siteusers { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<PatientRecord> PatientRecords { get; set; }
        public DbSet<DermatologyService> dermatologyserivcies { get; set; }
        public DbSet<DoctorProfesion> doctorprefesions { get; set; }
        public DbSet<DermatologistsDoctor> dermatologytDoctors { get; set; }
        public DbSet<Appointment> dermatologistappointments { get; set; }


    }
}