using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;

namespace MedHealth.Feedback.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Цей рядок автоматично знаходить всі Commands, Queries і Handlers
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        // Цей рядок знаходить всі валідатори (якщо ми їх додамо пізніше)
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        return services;
    }
}