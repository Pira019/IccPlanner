using System.Text.Json;
using Application.Responses.Errors;
using Infrastructure.Middlewares.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Middlewares
{
    public class ExceptionHandlingMiddleware : IExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly string contentType = "application/json";

        public ExceptionHandlingMiddleware(RequestDelegate requestDelegate, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _requestDelegate = requestDelegate;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur interne survenue lors de la requête {Method} {Path}. Utilisateur IP: {IpAddress}, Requête ID: {RequestId}",
                    context.Request.Method,
                    context.Request.Path,
                    context.Connection.RemoteIpAddress?.ToString(),
                    context.TraceIdentifier
                    );

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = contentType;
                context.Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate, max-age=0";
                context.Response.Headers["Pragma"] = "no-cache";
                context.Response.Headers["Expires"] = "0";

                await context.Response.WriteAsync(JsonSerializer.Serialize(ApiError.InternalServerError()));
            }

        }
    }
}
