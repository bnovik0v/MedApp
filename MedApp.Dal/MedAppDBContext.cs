using Microsoft.EntityFrameworkCore;
using MedApp.Core.Models;
using MedApp.DAL.Configurations;

namespace MedApp.DAL
{
    public class MedAppDbContext : DbContext
    {
        public DbSet<CaseReport> CaseReports { get; set; }
        public DbSet<Patient> Patients { get; set; }

        public MedAppDbContext(DbContextOptions<MedAppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CaseReportConfiguration());
            builder.ApplyConfiguration(new PatientConfiguration());
        }
    }
}