using Microsoft.AspNetCore.Http;
namespace Infrastructure.Middlewares.Interfaces
{
    public interface IExceptionHandlingMiddleware
    {
        public Task InvokeAsync(HttpContext context);
    }
}
