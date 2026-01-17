using MedHealth.Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MedHealth.Catalog.Dal;

public class CatalogDbContext : DbContext
{
    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
    {
    }

    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Specialization> Specializations { get; set; }
    public DbSet<Office> Offices { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Цей магічний рядок знайде всі класи конфігурації (як DoctorConfiguration) і застосує їх
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        // Data Seeding (Початкове наповнення даними - вимога 4.1)
        modelBuilder.Entity<Specialization>().HasData(
            new Specialization { Id = 1, Title = "Терапевт" },
            new Specialization { Id = 2, Title = "Хірург" },
            new Specialization { Id = 3, Title = "Окуліст" }
        );

        modelBuilder.Entity<Office>().HasData(
            new Office { Id = 1, RoomNumber = "101", Floor = 1 },
            new Office { Id = 2, RoomNumber = "205", Floor = 2 }
        );
    }
}