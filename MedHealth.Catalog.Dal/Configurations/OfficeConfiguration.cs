using MedHealth.Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedHealth.Catalog.Dal.Configurations;

public class OfficeConfiguration : IEntityTypeConfiguration<Office>
{
    public void Configure(EntityTypeBuilder<Office> builder)
    {
        builder.ToTable("Offices");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.RoomNumber)
            .IsRequired()
            .HasMaxLength(10); // Номер кімнати не має бути довгим
    }
}