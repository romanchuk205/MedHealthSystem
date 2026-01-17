using MedHealth.Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedHealth.Catalog.Dal.Configurations;

public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        // Назва таблиці
        builder.ToTable("Doctors");

        // Ключ
        builder.HasKey(d => d.Id);

        // Обмеження полів
        builder.Property(d => d.FullName)
            .IsRequired()
            .HasMaxLength(100);

        // Налаштування зв'язків
        builder.HasOne(d => d.Specialization)
            .WithMany(s => s.Doctors)
            .HasForeignKey(d => d.SpecializationId)
            .OnDelete(DeleteBehavior.Restrict); // Якщо видалити спеціалізацію, лікарі не видаляться (захист)

        builder.HasOne(d => d.Office)
            .WithMany(o => o.Doctors)
            .HasForeignKey(d => d.OfficeId);
    }
}