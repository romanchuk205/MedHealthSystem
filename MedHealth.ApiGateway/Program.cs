using MedHealth.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

// 1. Підключаємо Aspire Defaults (вже включає Service Discovery)
builder.AddServiceDefaults();

// 2. Підключаємо YARP
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));


var app = builder.Build();

app.MapDefaultEndpoints();

// 3. Запускаємо проксі
app.MapReverseProxy();

app.Run();