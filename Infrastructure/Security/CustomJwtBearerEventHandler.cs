using System.Text.Json;
using Application.Responses.Errors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Security
{
    /// <summary>
    /// Permet de gérer le retour 401 et 403
    /// </summary>
    public class CustomJwtBearerEventHandler : JwtBearerEvents
    {
        private readonly string ContentType = "application/json";
        public override async Task Challenge(JwtBearerChallengeContext context)
        {
            context.HandleResponse();

            //configuration
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = ContentType;
            await context.Response.WriteAsync(JsonSerializer.Serialize(ApiError.AuthError(true)));
        }

        public override async Task Forbidden(ForbiddenContext context)
        {
            //configuration
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            context.Response.ContentType = ContentType;
            await context.Response.WriteAsync(JsonSerializer.Serialize(ApiError.AuthError()));
        }
    }
}
