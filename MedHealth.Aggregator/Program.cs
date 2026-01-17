using MedHealth.Aggregator.Services;
using MedHealth.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// --- РЕЄСТРАЦІЯ КЛІЄНТІВ (Service Discovery) ---
// Ми використовуємо імена "catalog-api" та "appointments-api", які задали в AppHost!

builder.Services.AddHttpClient<CatalogClient>(client =>
{
    client.BaseAddress = new Uri("http://catalog-api");
});

builder.Services.AddHttpClient<AppointmentsClient>(client =>
{
    client.BaseAddress = new Uri("http://appointments-api");
});

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

app.Run();