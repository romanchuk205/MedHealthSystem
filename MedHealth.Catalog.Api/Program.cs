using FluentValidation; // 
using FluentValidation.AspNetCore;
using MedHealth.Catalog.Api.Middlewares;
using MedHealth.Catalog.Bll.Profiles;
using MedHealth.Catalog.Bll.Services;
using MedHealth.Catalog.Bll.Validators;
using MedHealth.Catalog.Dal;
using MedHealth.Catalog.Dal.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;
using MedHealth.ServiceDefaults; 
using MedHealth.Catalog.Bll.Interfaces;
using MedHealth.Catalog.Dal.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// --- 1. ПІДКЛЮЧАЄМО ASPIRE ---
builder.AddServiceDefaults();

// --- 2. БАЗА ДАНИХ ---

// ВИПРАВЛЕНО 1: Змінна для Catalog DB (включає TrustServerCertificate)
var catalogConnectionString = builder.Configuration.GetConnectionString("catalog-db") + ";TrustServerCertificate=True";

// ВИПРАВЛЕНО 2: Змінна для Appointments DB (потрібна лише для хаку)
var appointmentsConnectionString = builder.Configuration.GetConnectionString("appointments-db") + ";TrustServerCertificate=True";

// ВИПРАВЛЕНО 3: Використовуємо правильну змінну (catalogConnectionString)
builder.Services.AddDbContext<CatalogDbContext>(options =>
    options.UseSqlServer(catalogConnectionString,
        sqlOptions => sqlOptions.EnableRetryOnFailure()));

// --- 3. РЕЄСТРАЦІЯ ШАРІВ ---
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDoctorService, DoctorService>();

builder.Services.AddAutoMapper(typeof(CatalogMappingProfile));
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssembly(typeof(CreateDoctorDtoValidator).Assembly);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapDefaultEndpoints();


// --- 4. СТВОРЕННЯ БАЗИ (ФІНАЛЬНИЙ ХАК ІНІЦІАЛІЗАЦІЇ) ---
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
    // 1. Створюємо базу для Каталогу
    dbContext.Database.EnsureCreated();

    // 2. ХАК: СТВОРЕННЯ БАЗИ APPOINTMENTS ТА ТАБЛИЦІ
    try 
    {
        var initScript = @"
            -- Створення бази AppointmentsDB, якщо її немає
            IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'MedHealthAppointmentsDB')
            BEGIN
                CREATE DATABASE MedHealthAppointmentsDB;
            END;

            USE MedHealthAppointmentsDB;

            -- Створення таблиці Patients, якщо її немає
            IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Patients' and xtype='U')
            CREATE TABLE Patients (
                Id int IDENTITY(1,1) PRIMARY KEY,
                FullName nvarchar(MAX) NOT NULL,
                PhoneNumber nvarchar(MAX) NOT NULL,
                Email nvarchar(MAX) NULL
            );";
        
        // Виконуємо скрипт через контекст Catalog
        // Використання dbContext.Database.ExecuteSqlRaw гарантує, що ми використовуємо робоче підключення
        dbContext.Database.ExecuteSqlRaw(initScript);
        Console.WriteLine("" + "[Appointments Init] DB/Table created successfully via Catalog API hack.");
    }
    catch (Exception ex)
    {
        // Логуємо, але не блокуємо запуск сервісу
        Console.WriteLine("" + "[Appointments Init] Warning during DB creation: " + ex.Message);
    }
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();