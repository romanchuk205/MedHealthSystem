using MedHealth.Appointments.Domain;

namespace MedHealth.Appointments.Dal.Interfaces;

// Ми кажемо: "Цей репозиторій працює конкретно з сутністю Appointment"
public interface IAppointmentRepository : IRepository<Appointment>
{
}