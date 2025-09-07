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
using Infrastructure.Middlewares;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Infrastructure.Configurations.Filter;
using Application.Helper.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Shared.Interfaces;
using Shared;
using Application.Interfaces.Responses.Errors;
using Application.Responses.Errors;


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
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddUserSecrets<Program>()
                .AddEnvironmentVariables()
                .Build();

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

                options.OperationFilter<AddAcceptLanguageHeaderParameter>();

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
            builder.Services.AddScoped<IMinistryRepository, MinistryRepository>();
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IPostRepository, PosteRepository>();
            builder.Services.AddScoped<IProgramRepository, ProgramRepository>();
            builder.Services.AddScoped<IDepartmentProgramRepository, DepartmentProgramRepository>();
            builder.Services.AddScoped<IDepartmentMemberRepository, DepartmentMemberRepository>();
            builder.Services.AddScoped<IServiceRepository, TabServiceRepository>();
            builder.Services.AddScoped<IPrgDateRepository, PrgDateRepository>();
            builder.Services.AddScoped<ITabServicePrgRepository, TabServicePrgRepository>();
            builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            builder.Services.AddScoped<IAvailabilityRepository, AvailabilityRepository>();
            builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();

            builder.Services.AddScoped<IAccountResponseError, AccountResponseError>();

            //Services             
            builder.Services.AddScoped<IRessourceLocalizer, RessourceLocalizer>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<ISendEmailService, SendEmailService>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<IMinistryService, MinistryService>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IProgramService, ProgramService>();
            builder.Services.AddScoped<IServiceTabService, ServiceTabService>();
            builder.Services.AddScoped<ITabServicePrgService, TabServicePrgService>();
            builder.Services.AddScoped<IAvailabilityService, AvailabilityService>();

            builder.Services.AddScoped<CustomJwtBearerEventHandler>();

            //Validation


            builder.Services.AddSingleton(resolver =>
            resolver.GetRequiredService<IOptions<AppSetting>>().Value);

            // Mapper 
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Chaîne de connexion a la DB
            var conString = builder.Configuration.GetConnectionString("IccPlannerDb") ?? throw new InvalidOperationException("Connection string not found");
            builder.Services.AddDbContext<IccPlannerContext>(option =>
                option.UseNpgsql(conString));

            //Pour l'authentification la gestion de token
            builder.Services.AddSingleton<ITokenProvider, TokenProvider>();

            builder.Services.AddIdentity<User, Role>(opt =>
            {
                opt.User.RequireUniqueEmail = true;
                opt.SignIn.RequireConfirmedEmail = true;
            })
             .AddEntityFrameworkStores<IccPlannerContext>()
             .AddDefaultTokenProviders();

            builder.Services.Configure<DataProtectionTokenProviderOptions>
               (options => options.TokenLifespan = TimeSpan.FromMinutes(20));

            builder.Services.AddAuthentication(opt =>
                    {
                        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; 
                        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    }
                )
                .AddJwtBearer(o =>
                {
                    o.EventsType = typeof(CustomJwtBearerEventHandler);
                    o.RequireHttpsMetadata = appSetting.SecureToken ?? false;
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
                }
             );

            builder.Services.AddAuthorization(options =>
            {
                AuthorizationPolicies.AddPolicies(options);
            });

            builder.Services.AddRouting(op => op.LowercaseUrls = true);

            builder.Services.AddValidatorsFromAssemblyContaining<AddDepartmentMemberImportFileRequestValidator>();
            builder.Services.AddFluentValidationAutoValidation();

            //Localization
            builder.Services.AddLocalization(options => options.ResourcesPath = "Ressources");

            // Définir les cultures acceptées (français et anglais US)
            builder.Services.Configure<RequestLocalizationOptions>(
               options =>
               {
                   var supportedCultures = new List<CultureInfo>
                   {
                        new CultureInfo("fr-FR"),
                        new CultureInfo("en-US")
                   };

                   options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");
                   options.SupportedCultures = supportedCultures;
                   options.SupportedUICultures = supportedCultures;

                   options.RequestCultureProviders = new List<IRequestCultureProvider>
                   {
                        new AcceptLanguageHeaderRequestCultureProvider()
                   };
               });


            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            //Autoriser les appels du front
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins, policy =>
                {
                    policy.WithOrigins(appSetting.FrontUrl!).AllowCredentials().AllowAnyHeader().AllowAnyMethod();
                });
            });

            var app = builder.Build();

            app.UseRequestLocalization();

            // ConfiguInvalidOperationException : 'The service collection cannot be modified because it is read-only.'re the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseSwagger(opt =>
            {
                opt.SerializeAsV2 = true;
            });
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseRouting();
            app.UseCors(MyAllowSpecificOrigins);
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHttpsRedirection();
            app.MapControllers();

            app.Run();
        }
    }
}
