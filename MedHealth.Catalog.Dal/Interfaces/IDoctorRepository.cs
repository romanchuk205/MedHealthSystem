using MedHealth.Catalog.Domain;

namespace MedHealth.Catalog.Dal.Interfaces;

public interface IDoctorRepository : IGenericRepository<Doctor>
{
    // Оновлений метод з параметрами для пошуку, сортування та пагінації
    Task<IEnumerable<Doctor>> GetAllDoctorsWithDetailsAsync(
        string? searchTerm, 
        string? sortBy, 
        bool isDescending, 
        int page, 
        int pageSize);
        
    Task<Doctor?> GetDoctorWithDetailsAsync(int id);
}