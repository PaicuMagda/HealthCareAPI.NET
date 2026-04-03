using HealthcareAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthcareAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Consultation> Consultations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Patient>().HasIndex(p => p.Cnp).IsUnique();

            modelBuilder
                .Entity<Consultation>()
                .HasOne(c => c.Patient)
                .WithMany(p => p.Consultations)
                .HasForeignKey(c => c.Cnp)
                .HasPrincipalKey(p => p.Cnp)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
