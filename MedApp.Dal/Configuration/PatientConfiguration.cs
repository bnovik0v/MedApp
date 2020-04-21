using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedApp.Core.Models;

namespace MedApp.DAL.Configurations
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            const int maxLength = 50;

            builder.HasKey(p => p.Id);

            builder.Property(c => c.Id)
                   .UseIdentityColumn();

            builder.Property(c => c.FullName)
                   .IsRequired()
                   .HasMaxLength(maxLength);

            builder.ToTable("Patients");
        }
    }
}