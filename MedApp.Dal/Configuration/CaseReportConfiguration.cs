using MedApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedApp.DAL.Configurations
{
    public class CaseReportConfiguration : IEntityTypeConfiguration<CaseReport>
    {
        public void Configure(EntityTypeBuilder<CaseReport> builder)
        {
            const int maxLength = 50;

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                   .UseIdentityColumn();

            builder.Property(c => c.Diagnosis)
                   .IsRequired()
                   .HasMaxLength(maxLength);

            builder.HasOne(c => c.Patient)
                   .WithMany(p => p.CaseReports)
                   .HasForeignKey(c => c.PatientId);

            builder.ToTable("CaseReports");
        }
    }
}