using MedHealth.Feedback.Application;
using MedHealth.Feedback.Infrastructure;
using MedHealth.Feedback.Infrastructure.Seeding;
using MedHealth.ServiceDefaults; // <--- Цей using обов'язковий
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// --- 1. ПІДКЛЮЧАЄМО ASPIRE DEFAULTS (ДОДАНО) ---
builder.AddServiceDefaults();

// --- 2. ЛОГУВАННЯ (Serilog) ---
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
builder.Host.UseSerilog();

// --- 3. ПІДКЛЮЧАЄМО ШАРИ ---
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// --- 4. СТАНДАРТНІ СЕРВІСИ ---
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// --- 5. ASPIRE ENDPOINTS (Health Checks) (ДОДАНО) ---
app.MapDefaultEndpoints();

// --- 6. СІДІНГ ДАНИХ ---
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
    // Запускаємо наповнення бази (якщо вона порожня)
    seeder.SeedAsync().Wait();
}

// --- 7. ПАЙПЛАЙН ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();