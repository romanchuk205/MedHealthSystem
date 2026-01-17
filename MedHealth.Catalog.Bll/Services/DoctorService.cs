using AutoMapper;
using MedHealth.Catalog.Bll.Interfaces;
using MedHealth.Catalog.Bll.Models;
using MedHealth.Catalog.Dal.Interfaces;
using MedHealth.Catalog.Domain;

namespace MedHealth.Catalog.Bll.Services;

public class DoctorService : IDoctorService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DoctorService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    // Оновлений метод: просто передає параметри далі в репозиторій
    public async Task<IEnumerable<DoctorDto>> GetAllAsync(
        string? searchTerm, 
        string? sortBy, 
        bool isDescending, 
        int page, 
        int pageSize)
    {
        var doctors = await _unitOfWork.Doctors.GetAllDoctorsWithDetailsAsync(
            searchTerm, sortBy, isDescending, page, pageSize);
            
        return _mapper.Map<IEnumerable<DoctorDto>>(doctors);
    }

    public async Task<DoctorDto?> GetByIdAsync(int id)
    {
        var doctor = await _unitOfWork.Doctors.GetDoctorWithDetailsAsync(id);
        if (doctor == null) return null;
        
        return _mapper.Map<DoctorDto>(doctor);
    }

    public async Task CreateAsync(CreateDoctorDto dto)
    {
        var doctor = _mapper.Map<Doctor>(dto);
        
        await _unitOfWork.Doctors.AddAsync(doctor);
        await _unitOfWork.SaveAsync();
    }
}