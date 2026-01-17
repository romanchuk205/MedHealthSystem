using System.Data;
using MedHealth.Appointments.Dal.Interfaces;
using Microsoft.Data.SqlClient;

namespace MedHealth.Appointments.Dal.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly IDbConnection _connection;
    private IDbTransaction _transaction;
    
    // Поля для репозиторіїв
    private IPatientRepository? _patientRepository;
    private IAppointmentRepository? _appointmentRepository;
    // private IServiceRepository? _serviceRepository; 

    public UnitOfWork(string connectionString)
    {
        _connection = new SqlConnection(connectionString);
        _connection.Open(); // Відкриваємо з'єднання
        _transaction = _connection.BeginTransaction(); // Починаємо транзакцію
    }

    // Лінива ініціалізація репозиторіїв (створюються тільки коли треба)
    public IPatientRepository Patients => 
        _patientRepository ??= new PatientRepository(_transaction);

    public IAppointmentRepository Appointments => 
        _appointmentRepository ??= new AppointmentRepository(_transaction);
        
    // Методи керування транзакцією
    public IDbTransaction BeginTransaction()
    {
        return _transaction;
    }

    public void Commit()
    {
        try
        {
            _transaction.Commit();
        }
        catch
        {
            _transaction.Rollback();
            throw;
        }
        finally
        {
            // Після коміту починаємо нову транзакцію для наступних запитів
            _transaction.Dispose();
            _transaction = _connection.BeginTransaction();
        }
    }

    public void Rollback()
    {
        _transaction.Rollback();
        _transaction.Dispose();
        _transaction = _connection.BeginTransaction();
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _connection?.Dispose();
    }
}