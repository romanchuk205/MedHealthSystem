using System.Data;

namespace MedHealth.Appointments.Dal.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IPatientRepository Patients { get; }
    // IAppointmentRepository Appointments { get; } // Додаси пізніше
    // IServiceRepository Services { get; } // Додаси пізніше
    
    IDbTransaction BeginTransaction();
    void Commit();
    void Rollback();
}