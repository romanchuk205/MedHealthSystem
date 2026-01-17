using System.Data;
using Dapper;
using MedHealth.Appointments.Dal.Interfaces;
using MedHealth.Appointments.Domain;

namespace MedHealth.Appointments.Dal.Repositories;

// Цей репозиторій працює на Dapper (швидко і зручно)
public class AppointmentRepository : IAppointmentRepository
{
    private readonly IDbTransaction _transaction;

    public AppointmentRepository(IDbTransaction transaction)
    {
        _transaction = transaction;
    }

    // Dapper використовує розширення для Connection, тому все просто:
    public async Task<IEnumerable<Appointment>> GetAllAsync()
    {
        return await _transaction.Connection.QueryAsync<Appointment>(
            "SELECT * FROM Appointments", 
            transaction: _transaction);
    }

    public async Task<int> AddAsync(Appointment entity)
    {
        var sql = @"INSERT INTO Appointments (PatientId, DoctorId, AppointmentDate, IsConfirmed) 
                    VALUES (@PatientId, @DoctorId, @AppointmentDate, @IsConfirmed);
                    SELECT CAST(SCOPE_IDENTITY() as int)";
        
        return await _transaction.Connection.QuerySingleAsync<int>(sql, entity, transaction: _transaction);
    }
    
    // Заглушки
    public Task<Appointment?> GetByIdAsync(int id) => throw new NotImplementedException();
    public Task UpdateAsync(Appointment entity) => throw new NotImplementedException();
    public Task DeleteAsync(int id) => throw new NotImplementedException();
}