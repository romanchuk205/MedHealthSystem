using MedHealth.Catalog.Bll.Models;

namespace MedHealth.Catalog.Bll.Interfaces;

public interface IDoctorService
{
    // Додали параметри в інтерфейс
    Task<IEnumerable<DoctorDto>> GetAllAsync(
        string? searchTerm, 
        string? sortBy, 
        bool isDescending, 
        int page, 
        int pageSize);
        
    Task<DoctorDto?> GetByIdAsync(int id);
    Task CreateAsync(CreateDoctorDto dto);
}