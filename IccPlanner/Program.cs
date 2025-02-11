using Application.Helper;
using Application.Interfaces.Services;
using Application.Services;
using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models; 
using Microsoft.Extensions.Options;
using Infrastructure.Repositories;
using Application.Interfaces.Repositories;
using Infrastructure.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Infrastructure.Security;
using Infrastructure.Configurations.Interface;

namespace IccPlanner
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
            builder.Logging.AddDebug();

            // Configuration
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appSettings.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddUserSecrets<Program>()
                .AddEnvironmentVariables()
                .Build();

            // builder.Services.Configure<AppSetting>(builder.Configuration.GetSection("AppSetting"));

            builder.Services.Configure<AppSetting>(config.GetRequiredSection("AppSetting"));

            AppSetting appSetting = config.GetRequiredSection("AppSetting").Get<AppSetting>()!;

            // Add services to the container.
            builder.Services.AddControllers()
                .ConfigureApiBehaviorOptions(op =>
                    {
                        op.InvalidModelStateResponseFactory = context =>
                        {
                            var response = ApiResponseHelper.CreateValidationErrorResponse(context);
                            return new BadRequestObjectResult(response);
                        };
                       // op.ClientErrorMapping[StatusCodes.Status401Unauthorized] = ApiError.AuthError();
                    })
                
                ;

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

                var securitySchema = new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat = "JWT"
                };

                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securitySchema);

                var securityRequirement = new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        }, new string[] {}
                    }
                };
                options.AddSecurityRequirement(securityRequirement);
            });

            //Repositories  
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();

            //Services
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<ISendEmailService, SendEmailService>();
            builder.Services.AddScoped<IRoleService, RoleService>();

            builder.Services.AddSingleton(resolver =>
            resolver.GetRequiredService<IOptions<AppSetting>>().Value);

            // Mapper 
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Chaine de connexion a la DB
            var conString = builder.Configuration.GetConnectionString("IccPlannerDb") ?? throw new InvalidOperationException("Connection string not found");
            builder.Services.AddDbContext<IccPlannerContext>(option =>
                option.UseNpgsql(conString));

            //Pour l'authentification la gestion de token
            builder.Services.AddSingleton<ITokenProvider,TokenProvider>();

            builder.Services.AddIdentity<User, Role>(opt =>
            {
                opt.User.RequireUniqueEmail = true;
                opt.SignIn.RequireConfirmedEmail = true;
            })
             .AddEntityFrameworkStores<IccPlannerContext>()
             .AddDefaultTokenProviders();

            builder.Services.Configure<DataProtectionTokenProviderOptions>
               (options => options.TokenLifespan = TimeSpan.FromMinutes(20));

            builder.Services.AddAuthentication(
                op =>
                {
                    op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; 
                })
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true, 
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = appSetting?.JwtSetting?.Issuer,
                        ValidAudience = appSetting?.JwtSetting?.Audiance,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSetting?.JwtSetting.Secret!)), 
                        ClockSkew = TimeSpan.Zero
                    };
                    o.MapInboundClaims = false;
                });

            builder.Services.AddAuthorization(options =>
            {
                AuthorizationPolicies.AddPolicies(options);
            });

            builder.Services.AddRouting(op => op.LowercaseUrls = true);

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHttpsRedirection();
            app.MapControllers();

            app.Run();
        }
    }
}
