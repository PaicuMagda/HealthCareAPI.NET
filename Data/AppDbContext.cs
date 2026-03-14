using Microsoft.EntityFrameworkCore;
using HealthcareAPI.Models;

namespace HealthcareAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Doctor> Doctors { get; set; }
    }
}