using MedHealth.Feedback.Domain.Interfaces;
using MedHealth.Feedback.Infrastructure.Persistence;
using MedHealth.Feedback.Infrastructure.Repositories;
using MedHealth.Feedback.Infrastructure.Seeding;
using MedHealth.Feedback.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MedHealth.Feedback.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // --- ЛОГІКА ПІДКЛЮЧЕННЯ (ЗМІНЕНО ДЛЯ ASPIRE) ---
        
        // 1. Спробуємо отримати рядок підключення від Aspire (ім'я ресурсу "feedback-db")
        var aspireConnectionString = configuration.GetConnectionString("feedback-db");

        if (!string.IsNullOrEmpty(aspireConnectionString))
        {
            // Якщо запускаємо через Aspire - налаштовуємо Mongo вручну з цим рядком
            services.Configure<MongoDbSettings>(options =>
            {
                options.ConnectionString = aspireConnectionString;
                options.DatabaseName = "MedHealthFeedbackDB";
            });
        }
        else
        {
            // Якщо запускаємо окремо (без Aspire) - беремо зі старого файлу налаштувань
            services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));
        }
        
        // --- РЕЄСТРАЦІЯ СЕРВІСІВ ---
        
        // Реєструємо контекст бази
        services.AddSingleton<MongoDbContext>();

        // Реєстрація Репозиторію
        services.AddScoped<IReviewRepository, ReviewRepository>();
        
        // Реєстрація Сідера
        services.AddScoped<DataSeeder>();
        
        return services;
    }
}