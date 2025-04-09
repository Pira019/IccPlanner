using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Infrastructure.Configurations.Filter
{
    public class AddAcceptLanguageHeaderParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "Accept-Language",  // Nom de l'en-tête
                In = ParameterLocation.Header, 
                Description = "Langue de la requête (par exemple, en-US, fr, etc.)",
                Required = false,  // Ne pas obligatoire, car il peut y avoir une langue par défaut
                Schema = new OpenApiSchema
                {
                    Type = "string",
                    Default = new OpenApiString("en-US")  // Valeur par défaut, si aucune langue n'est spécifiée
                }
            });
        }
    }
}
