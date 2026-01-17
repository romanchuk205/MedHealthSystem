using System.Net;
using System.Text.Json;

namespace MedHealth.Catalog.Api.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Передаємо запит далі по ланцюжку
            await _next(context);
        }
        catch (Exception ex)
        {
            // Якщо сталася помилка, ловимо її тут
            _logger.LogError(ex, "Сталася непередбачена помилка");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = new
        {
            StatusCode = context.Response.StatusCode,
            Message = "Internal Server Error",
            Detailed = exception.Message // У продакшні це поле краще прибрати для безпеки
        };

        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}