using MedHealth.Appointments.Domain;

namespace MedHealth.Appointments.Dal.Interfaces;

public interface IPatientRepository : IRepository<Patient>
{
    // Тут можуть бути специфічні методи, наприклад пошук по телефону
}