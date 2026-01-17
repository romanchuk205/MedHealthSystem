using System.Data;
using Dapper;
using MedHealth.Appointments.Dal.Interfaces;
using MedHealth.Appointments.Domain;

namespace MedHealth.Appointments.Dal.Repositories;

public class ServiceRepository : IServiceRepository
{
    private readonly IDbTransaction _transaction;

    public ServiceRepository(IDbTransaction transaction)
    {
        _transaction = transaction;
    }

    public async Task<IEnumerable<Service>> GetAllAsync()
    {
        // Використовуємо Dapper для отримання списку послуг
        return await _transaction.Connection.QueryAsync<Service>(
            "SELECT * FROM Services", 
            transaction: _transaction);
    }

    public async Task<int> AddAsync(Service entity)
    {
        // SQL-запит для додавання послуги (ServiceName, Price)
        var sql = @"INSERT INTO Services (ServiceName, Price) 
                    VALUES (@ServiceName, @Price);
                    SELECT CAST(SCOPE_IDENTITY() as int)";
        
        // Виконуємо запит і повертаємо ID нової послуги
        return await _transaction.Connection.QuerySingleAsync<int>(sql, entity, transaction: _transaction);
    }

    // Заглушки для інших методів
    public Task<Service?> GetByIdAsync(int id) => throw new NotImplementedException();
    public Task UpdateAsync(Service entity) => throw new NotImplementedException();
    public Task DeleteAsync(int id) => throw new NotImplementedException();
}