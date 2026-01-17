namespace MedHealth.Aggregator.Models;

// Дані про лікаря (приходять з Catalog)
public record DoctorDto(int Id, string FullName, string SpecializationTitle);

// Дані про пацієнта (приходять з Appointments)
public record PatientDto(int Id, string FullName, string PhoneNumber);

// Дані про запис (приходять з Appointments)
public record AppointmentDto(int Id, int PatientId, int DoctorId, DateTime Date);

// Фінальний об'єкт, який ми віддамо на фронтенд (все разом)
public record DashboardData(
    PatientDto? Patient, 
    IEnumerable<AppointmentDto> Appointments, 
    IEnumerable<DoctorDto> RecommendedDoctors
);