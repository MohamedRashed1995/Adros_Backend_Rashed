using Adros.Application.Interfaces.IService;
using Adros.Application.Mappings;
using Adros.Application.Services;
using Adros.Application.Services.Client;
using Adros.Application.Services.HomeService;
using Adros.Application.Services.Security;
using Adros.Application.Services.UsersServices;
using Adros.Core.DomainServices;
using Adros.Core.DomainServices.IDomainService;
using Adros.Core.Entities;
using Adros.Infrastructure.Data;
using Adros.Infrastructure.External;
using Adros.Persistence.Contexts;
using Adros.Persistence.Repositories;
using Adros.Persistence.UnitOfWork;
using Adros.Shared.Helpers;
using Adros.Shared.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace Adros.Apis.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // ========== 1. Database Context ==========
            services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(
            configuration.GetConnectionString("DefaultConnection"),
            sqlServerOptions => sqlServerOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null
            )
        ));

            // ========== 2. Identity Configuration ==========
            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;

                // User settings
                options.User.RequireUniqueEmail = true;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                options.Lockout.MaxFailedAccessAttempts = 5;
            })
            .AddEntityFrameworkStores<AppDbContext>() // **أضف هذا السطر!**
            .AddDefaultTokenProviders();

            // ========== 4. AutoMapper Configuration ==========
            services.AddAutoMapper(typeof(BaseProfile).Assembly);
<<<<<<< HEAD
            services.AddAutoMapper(typeof(AttachmentProfile));
=======
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
            services.AddTransient(typeof(ImageUrlResolver<>));

            // ========== 5. External Services ==========
            services.AddHttpClient();
            services.AddScoped<BunnyVideoService>();

            // ========== 6. Additional Services ==========
            services.AddHttpContextAccessor();
            services.AddControllers();
            services.AddEndpointsApiExplorer();

            return services;
        }

        private static void RegisterAllServices(IServiceCollection services)
        {
            Console.WriteLine("🔍 Starting automatic service registration...");

            // جلب جميع الـAssemblies
            var assemblies = new[]
            {
        Assembly.Load("Adros.Application"),
        Assembly.Load("Adros.Core"),
        Assembly.Load("Adros.Infrastructure"),
        Assembly.Load("Adros.Persistence"),
        Assembly.Load("Adros.Shared") // أضف هذا
    };

            // ========== الخطوة 1: تسجيل الـ UnitOfWork أولاً ==========
            var unitOfWorkType = assemblies.SelectMany(a => a.GetTypes())
                .FirstOrDefault(t => t.IsClass && !t.IsAbstract &&
                                   (t.Name == "UnitOfWork" || t.Name.EndsWith("UnitOfWork")));

            var unitOfWorkInterface = assemblies.SelectMany(a => a.GetTypes())
                .FirstOrDefault(t => t.IsInterface &&
                                   (t.Name == "IUnitOfWork" || t.Name.EndsWith("IUnitOfWork")));

            if (unitOfWorkType != null && unitOfWorkInterface != null)
            {
                services.AddScoped(unitOfWorkInterface, unitOfWorkType);
                Console.WriteLine($"✅ Registered UoW: {unitOfWorkInterface.Name} -> {unitOfWorkType.Name}");
            }
            else
            {
                Console.WriteLine("⚠️ WARNING: IUnitOfWork/UnitOfWork not found!");
            }

            // ========== الخطوة 2: تسجيل جميع الـ Repositories ==========
            foreach (var assembly in assemblies)
            {
                try
                {
                    // البحث عن جميع الـRepositories (تنتهي بـ Repository أو بدء بـ Repository)
                    var repositoryTypes = assembly.GetTypes()
                        .Where(t => t.IsClass && !t.IsAbstract &&
                                  (t.Name.EndsWith("Repository") || t.Name.StartsWith("Repository")))
                        .ToList();

                    foreach (var repoType in repositoryTypes)
                    {
                        // البحث عن الـInterface المناسب
                        var interfaceName = "I" + repoType.Name;
                        var interfaceType = repoType.GetInterfaces()
                            .FirstOrDefault(i => i.Name == interfaceName);

                        if (interfaceType != null)
                        {
                            services.AddScoped(interfaceType, repoType);
                            Console.WriteLine($"✅ Registered Repo: {interfaceType.Name} -> {repoType.Name}");
                        }
                        else
                        {
                            // جرب أي interface ينتهي بـ Repository
                            var anyInterface = repoType.GetInterfaces()
                                .FirstOrDefault(i => i.Name.EndsWith("Repository"));

                            if (anyInterface != null)
                            {
                                services.AddScoped(anyInterface, repoType);
                                Console.WriteLine($"ℹ️ Registered Repo with generic interface: {anyInterface.Name} -> {repoType.Name}");
                            }
                            else
                            {
                                // تسجيل بدون interface (كـ concrete type)
                                services.AddScoped(repoType);
                                Console.WriteLine($"📝 Registered concrete Repo: {repoType.Name}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Error in assembly {assembly.FullName}: {ex.Message}");
                }
            }

            // ========== الخطوة 3: تسجيل جميع الـ Services (كما كان) ==========
            foreach (var assembly in assemblies)
            {
                try
                {
                    // جلب كل الـClasses التي تنتهي بـ "Service" وتكون concrete
                    var serviceTypes = assembly.GetTypes()
                        .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Service"))
                        .ToList();

                    foreach (var serviceType in serviceTypes)
                    {
                        // البحث عن الـInterface المناسب (بنفس الاسم مع حرف I في البداية)
                        var interfaceName = "I" + serviceType.Name;
                        var interfaceType = serviceType.GetInterfaces()
                            .FirstOrDefault(i => i.Name == interfaceName);

                        if (interfaceType != null)
                        {
                            services.AddScoped(interfaceType, serviceType);
                            Console.WriteLine($"✅ Registered Service: {interfaceType.Name} -> {serviceType.Name}");
                        }
                        else
                        {
                            // إذا لم يجد Interface بنفس الاسم، جرب أول Interface ينتهي بـ "Service"
                            var anyInterface = serviceType.GetInterfaces()
                                .FirstOrDefault(i => i.Name.EndsWith("Service"));

                            if (anyInterface != null)
                            {
                                services.AddScoped(anyInterface, serviceType);
                                Console.WriteLine($"⚠️ Registered Service with generic interface: {anyInterface.Name} -> {serviceType.Name}");
                            }
                            else
                            {
                                // تسجيل الـService بدون Interface
                                services.AddScoped(serviceType);
                                Console.WriteLine($"ℹ️ Registered Service without interface: {serviceType.Name}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Error in assembly {assembly.FullName}: {ex.Message}");
                }
            }

            Console.WriteLine("🎉 Automatic service registration completed!");
        }

        public static IServiceCollection AddAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // ========== Token Service ==========
            services.AddScoped<ITokenService, TokenService>();

            // ========== JWT Authentication ==========
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,

                    ValidAudience = configuration["JWT:ValidAudience"],
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
                };

                // Handle token in query string (for WebSocket connections)
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;

                        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hub"))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            return services;
        }

        public static IServiceCollection AddAuthorizationPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                // Role-based policies
                options.AddPolicy("AdminOnly", policy =>
                    policy.RequireRole("Admin", "Master"));

                options.AddPolicy("TeacherOnly", policy =>
                    policy.RequireRole("Teacher", "Admin", "Master"));

                options.AddPolicy("StudentOnly", policy =>
                    policy.RequireRole("Student", "Admin", "Master"));

                options.AddPolicy("AuthenticatedUser", policy =>
                    policy.RequireAuthenticatedUser());

                // Custom claim-based policies
                options.AddPolicy("ActiveUser", policy =>
                    policy.RequireClaim("IsActive", "true"));
            });

            return services;
        }

        public static IServiceCollection AddCorsPolicies(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithExposedHeaders("X-Pagination")); // لـ pagination headers

                options.AddPolicy("ProductionCors",
                    builder => builder
                        .WithOrigins(
                            "https://adros.com",
                            "https://www.adros.com",
                            "https://admin.adros.com")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .WithExposedHeaders("X-Pagination"));
            });

            return services;
        }

        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Adros Educational Platform API",
                    Version = "v1",
                    Description = "ASP.NET Core Web API for Adros Educational Platform",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Adros Team",
                        Email = "support@adros.com"
                    }
                });

                // **الجزء المهم - غير كده:**
                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                    Name = "Authorization",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http, // **غير لـ Http مش ApiKey**
                    Scheme = "bearer", // **حروف صغيرة**
                    BearerFormat = "JWT"
                });

                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
        {
            {
                new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference
                    {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
        });

                // Include XML comments
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath);
                }
            });

            return services;
        }

        //public static IServiceCollection AddHealthChecksConfiguration(this IServiceCollection services, IConfiguration configuration)
        //{
        //    services.AddHealthChecks()
        //        .AddDbContextCheck<AppDbContext>("Database")
        //        .AddUrlGroup(new Uri("https://google.com"), "Google API")
        //        .AddDiskStorageHealthCheck(s => s.AddDrive("C:\\", 1024), "Storage");

        //    services.AddHealthChecksUI(setup =>
        //    {
        //        setup.AddHealthCheckEndpoint("API", "/health");
        //        setup.SetEvaluationTimeInSeconds(60);
        //        setup.SetApiMaxActiveRequests(1);
        //        setup.MaximumHistoryEntriesPerEndpoint(50);
        //    }).AddInMemoryStorage();

        //    return services;
        //}
    }
}