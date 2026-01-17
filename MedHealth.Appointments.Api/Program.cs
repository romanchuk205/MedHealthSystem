using MedHealth.Appointments.Bll.Interfaces;
using MedHealth.Appointments.Bll.Profiles;
using MedHealth.Appointments.Bll.Services;
using MedHealth.Appointments.Dal.Interfaces;
using MedHealth.Appointments.Dal.Repositories;
using MedHealth.ServiceDefaults;
using Microsoft.Data.SqlClient;
using Dapper;

var builder = WebApplication.CreateBuilder(args);

// 1. Aspire Defaults
builder.AddServiceDefaults();

// 2. Отримуємо рядок підключення
//  ВИПРАВЛЕНО: Додаємо TrustServerCertificate=True, щоб вирішити проблему Login failed.
var connectionString = builder.Configuration.GetConnectionString("appointments-db") + ";TrustServerCertificate=True";

// 3. Реєструємо сервіси
builder.Services.AddScoped<IUnitOfWork>(provider => new UnitOfWork(connectionString!));
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

// ВИКЛИК МЕТОДУ ІНІЦІАЛІЗАЦІЇ 
InitializeDatabase(connectionString, app.Services);

app.Run();


//  ДОПОМІЖНИЙ МЕТОД ДЛЯ СТВОРЕННЯ БАЗИ 
static void InitializeDatabase(string connectionString, IServiceProvider serviceProvider)
{
    using var scope = serviceProvider.CreateScope();
    
    try
    {
        var builderDb = new SqlConnectionStringBuilder(connectionString);
        var targetDbName = builderDb.InitialCatalog;
        builderDb.InitialCatalog = "master"; // Підключаємося до master

        // 1. Створення бази MedHealthAppointmentsDB
        using (var connection = new SqlConnection(builderDb.ConnectionString))
        {
            connection.Open();
            var checkDbQuery = $"SELECT database_id FROM sys.databases WHERE name = '{targetDbName}'";
            var dbId = connection.ExecuteScalar(checkDbQuery);

            if (dbId == null)
            {
                connection.Execute($"CREATE DATABASE [{targetDbName}]");
                Console.WriteLine($"[Appointments Init] Database '{targetDbName}' created successfully!");
            }
        }

        // 2. Створення таблиці Patients у новій базі
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
             var tableQuery = @"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Patients' and xtype='U')
                CREATE TABLE Patients (
                    Id int IDENTITY(1,1) PRIMARY KEY,
                    FullName nvarchar(MAX) NOT NULL,
                    PhoneNumber nvarchar(MAX) NOT NULL,
                    Email nvarchar(MAX) NULL
                )";
             connection.Execute(tableQuery);
             Console.WriteLine("[Appointments Init] Table 'Patients' created.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[Appointments Init ERROR] Failed to initialize database structures: {ex.Message}");
    }
}