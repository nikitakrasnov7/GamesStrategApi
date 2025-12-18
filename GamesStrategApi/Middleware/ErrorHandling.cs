using System.Net;
using System.Text.Json;

namespace GamesStrategApi.Middleware
{
    public class ErrorHandling
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandling> _logger;

        /// <summary>
        /// Конструктор
        /// </summary>
        public ErrorHandling(RequestDelegate next, ILogger<ErrorHandling> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Обработать запрос
        /// </summary>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Обработать исключение
        /// </summary>
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "Ошибка: {Message}", exception.Message);

            var response = context.Response;
            response.ContentType = "application/json";

            var error = new
            {
                success = false,
                message = "Произошла ошибка",
                error = exception.Message
            };

            response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var json = JsonSerializer.Serialize(error);
            await response.WriteAsync(json);
        }
    }

    /// <summary>
    /// Расширения для регистрации middleware
    /// </summary>
    public static class ErrorHandlingMiddlewareExtensions
    {
        /// <summary>
        /// Использовать обработку ошибок
        /// </summary>
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ErrorHandling>();
        }
    }
}

