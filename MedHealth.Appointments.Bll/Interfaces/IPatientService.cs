using MedHealth.Appointments.Bll.Models;

namespace MedHealth.Appointments.Bll.Interfaces;

public interface IPatientService
{
    Task<IEnumerable<PatientDto>> GetAllPatientsAsync();
    Task<int> CreatePatientAsync(CreatePatientDto dto);
}