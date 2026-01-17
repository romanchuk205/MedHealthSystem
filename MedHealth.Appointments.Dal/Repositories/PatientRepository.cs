using System.Data;
using MedHealth.Appointments.Dal.Interfaces;
using MedHealth.Appointments.Domain;
using Microsoft.Data.SqlClient;

namespace MedHealth.Appointments.Dal.Repositories;

// Цей репозиторій працює на чистому ADO.NET (вимога завдання)
public class PatientRepository : IPatientRepository
{
    private readonly IDbTransaction _transaction;

    // Ми приймаємо транзакцію, щоб працювати в рамках Unit of Work
    public PatientRepository(IDbTransaction transaction)
    {
        _transaction = transaction;
    }

    // Допоміжний метод, щоб отримати активне з'єднання
    private SqlConnection Connection => (SqlConnection)_transaction.Connection!;

    public async Task<IEnumerable<Patient>> GetAllAsync()
    {
        var result = new List<Patient>();
        
        // Створюємо команду вручну
        var command = Connection.CreateCommand();
        command.Transaction = (SqlTransaction)_transaction;
        command.CommandText = "SELECT * FROM Patients";

        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            // Вручну мапимо дані з таблиці в об'єкт (ADO.NET стиль)
            result.Add(new Patient
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                FullName = reader.GetString(reader.GetOrdinal("FullName")),
                PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber")),
                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString(reader.GetOrdinal("Email")),
                CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
            });
        }
        return result;
    }

    public async Task<int> AddAsync(Patient entity)
    {
        var command = Connection.CreateCommand();
        command.Transaction = (SqlTransaction)_transaction;
        // Параметризований запит (захист від SQL Injection)
        command.CommandText = @"INSERT INTO Patients (FullName, PhoneNumber, Email, CreatedAt) 
                                VALUES (@FullName, @PhoneNumber, @Email, @CreatedAt);
                                SELECT SCOPE_IDENTITY();";

        // Додаємо параметри вручну
        command.Parameters.AddWithValue("@FullName", entity.FullName);
        command.Parameters.AddWithValue("@PhoneNumber", entity.PhoneNumber);
        command.Parameters.AddWithValue("@Email", (object?)entity.Email ?? DBNull.Value);
        command.Parameters.AddWithValue("@CreatedAt", entity.CreatedAt);

        // Виконуємо і отримуємо ID нового запису
        var newId = await command.ExecuteScalarAsync();
        return Convert.ToInt32(newId);
    }

    // Заглушки для інших методів (щоб код компілювався)
    public Task<Patient?> GetByIdAsync(int id) => throw new NotImplementedException();
    public Task UpdateAsync(Patient entity) => throw new NotImplementedException();
    public Task DeleteAsync(int id) => throw new NotImplementedException();
}