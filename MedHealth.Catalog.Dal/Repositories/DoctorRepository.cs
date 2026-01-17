using MedHealth.Catalog.Dal.Interfaces;
using MedHealth.Catalog.Domain;
using Microsoft.EntityFrameworkCore;

namespace MedHealth.Catalog.Dal.Repositories;

public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
{
    public DoctorRepository(CatalogDbContext context) : base(context)
    {
    }

    public async Task<Doctor?> GetDoctorWithDetailsAsync(int id)
    {
        return await _dbSet
            .Include(d => d.Specialization)
            .Include(d => d.Office)
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    // Реалізація розумного отримання списку
    public async Task<IEnumerable<Doctor>> GetAllDoctorsWithDetailsAsync(
        string? searchTerm, 
        string? sortBy, 
        bool isDescending, 
        int page, 
        int pageSize)
    {
        // 1. Починаємо будувати запит
        var query = _dbSet
            .Include(d => d.Specialization)
            .Include(d => d.Office)
            .AsQueryable();

        // 2. Фільтрація (Пошук по імені)
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(d => d.FullName.Contains(searchTerm));
        }

        // 3. Сортування
        query = sortBy?.ToLower() switch
        {
            "experience" => isDescending 
                ? query.OrderByDescending(d => d.ExperienceYears) 
                : query.OrderBy(d => d.ExperienceYears),
            
            "name" or _ => isDescending 
                ? query.OrderByDescending(d => d.FullName) 
                : query.OrderBy(d => d.FullName) // За замовчуванням сортуємо по імені
        };

        // 4. Пагінація (Пропустити N сторінок, взяти M записів)
        return await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
}