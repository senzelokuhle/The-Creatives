using System.Data.Entity;
using ModelsLogic;

namespace DataAccessLogic
{
    public class EntityContext :DbContext
    {
        public EntityContext() : base("DefaultConnection")
        {
        }

        public DbSet<PatientRecord> patientRecords { get; set; }
        public DbSet<Gender> genders { get; set; }

        public DbSet<SiteUser> siteusers { get; set; }

        public DbSet<DermatologyService> dermatologyservices { get; set; }

        public DbSet<DoctorProfesion> doctorprefesions { get; set; }

        public DbSet<DermatologistsDoctor> dermatologists { get; set; }

        public DbSet<Appointment> dermatologistappointments { get; set; }

    }
}
