using MedHealth.Appointments.Domain;

namespace MedHealth.Appointments.Dal.Interfaces;

// Ми кажемо: "Цей репозиторій працює конкретно з сутністю Service"
public interface IServiceRepository : IRepository<Service>
{
}