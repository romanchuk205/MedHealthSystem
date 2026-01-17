using MedHealth.Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedHealth.Catalog.Dal.Configurations;

public class SpecializationConfiguration : IEntityTypeConfiguration<Specialization>
{
    public void Configure(EntityTypeBuilder<Specialization> builder)
    {
        // Явно вказуємо назву таблиці
        builder.ToTable("Specializations");

        // Первинний ключ
        builder.HasKey(s => s.Id);

        // Налаштування поля Title (обов'язкове, макс. 100 символів)
        builder.Property(s => s.Title)
            .IsRequired()
            .HasMaxLength(100);
    }
}