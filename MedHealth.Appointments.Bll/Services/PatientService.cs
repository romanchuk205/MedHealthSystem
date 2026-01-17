using AutoMapper;
using MedHealth.Appointments.Bll.Interfaces;
using MedHealth.Appointments.Bll.Models;
using MedHealth.Appointments.Dal.Interfaces;
using MedHealth.Appointments.Domain;

namespace MedHealth.Appointments.Bll.Services;

public class PatientService : IPatientService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PatientService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PatientDto>> GetAllPatientsAsync()
    {
        // 1. Беремо дані з бази через репозиторій
        var patients = await _unitOfWork.Patients.GetAllAsync();

        // 2. Перетворюємо (мапимо) Сутності в DTO
        return _mapper.Map<IEnumerable<PatientDto>>(patients);
    }

    public async Task<int> CreatePatientAsync(CreatePatientDto dto)
    {
        // 1. Перетворюємо DTO в Сутність
        var patient = _mapper.Map<Patient>(dto);

        // 2. Додаємо логіку (наприклад, проставляємо дату)
        patient.CreatedAt = DateTime.Now;

        // 3. Відкриваємо транзакцію (через UoW)
        // (У нашому випадку UnitOfWork вже відкрив її в конструкторі, нам треба просто зафіксувати)

        var newId = await _unitOfWork.Patients.AddAsync(patient);

        // 4. Фіксуємо зміни (Commit) - це ОБОВ'ЯЗКОВО
        _unitOfWork.Commit();

        return newId;
    }
}