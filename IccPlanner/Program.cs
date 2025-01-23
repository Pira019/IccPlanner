using Application.Helper;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.Services;
using Domain.Entities;
using Application.Configurations;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.Extensions.Options;

namespace Application
{
    /// <summary>
    ///   Configurer l'API
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            IHostEnvironment environment = builder.Environment;

            //Ajouter le log
            builder.Logging.AddConsole();

            // Configuration
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appSettings.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build(); 

            builder.Services.Configure<AppSetting>(builder.Configuration.GetSection("AppSetting"));

            builder.Services.Configure<AppSetting>(config.GetRequiredSection("AppSetting"));

            AppSetting? appSetting = config.GetRequiredSection("AppSetting").Get<AppSetting>();

            // Add services to the container.
            builder.Services.AddControllers()
                .ConfigureApiBehaviorOptions(op =>
                    {
                        op.InvalidModelStateResponseFactory = context =>
                        {
                            var response = ApiResponseHelper.CreateValidationErrorResponse(context);
                            return new BadRequestObjectResult(response);
                        };
                    });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = appSetting?.AppName,
                        Version = "v1",
                        Contact = new OpenApiContact
                        {
                            Name = appSetting?.Contact?.Name,
                            Email = appSetting?.Contact?.Email,
                        }
                    });

                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            builder.Services.AddScoped<IAccountRepository, AccountRepository>();

            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<ISendEmailService, SendEmailService>();

            builder.Services.AddSingleton(resolver =>
            resolver.GetRequiredService<IOptions<AppSetting>>().Value);

            // Mapper 
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());  

            // Chaine de connexion a la DB
            var conString = builder.Configuration.GetConnectionString("IccPlannerDb") ?? throw new InvalidOperationException("Connection string not found");
            builder.Services.AddDbContext<IccPlannerContext>(option =>
                option.UseNpgsql(conString));

            //Pour l'authentification ou Identity
            builder.Services.AddIdentity<User, IdentityRole>(opt =>
            {
                opt.User.RequireUniqueEmail = true;
                opt.SignIn.RequireConfirmedEmail = true;
            })
             .AddEntityFrameworkStores<IccPlannerContext>()
             .AddDefaultTokenProviders();

            builder.Services.Configure<DataProtectionTokenProviderOptions>
               (options => options.TokenLifespan = TimeSpan.FromHours(3)); 

            /*builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, 
                    options => builder.Configuration.Bind("JwtSettings", options));*/

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseSwagger(opt =>
            {
                opt.SerializeAsV2 = true;
            });

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
