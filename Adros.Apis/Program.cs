//using Adros.Apis.Configurations;
//using Adros.Apis.DbIntializers;
//using Adros.Application.Interfaces.IService;
//using Adros.Application.Services;
//using Adros.Application.Services.Client;
//using Adros.Application.Services.HomeService;
//using Adros.Application.Services.Security;
//using Adros.Application.Services.UsersServices;
//using Adros.Core.DomainServices;
//using Adros.Core.DomainServices.IDomainService;
//using Adros.Persistence.Contexts;
//using Adros.Persistence.Repositories;
//using Adros.Persistence.UnitOfWork;
//using Adros.Shared.Interfaces;
//using Microsoft.EntityFrameworkCore;

//namespace Adros.Apis;

//public class Program
//{
//    public static async Task Main(string[] args)
//    {
//        var builder = WebApplication.CreateBuilder(args);

//        // تسجيل بسيط
//        builder.Logging.ClearProviders();
//        builder.Logging.AddConsole();

//        // إعدادات
//        builder.Services.AddApplicationServices(builder.Configuration)
//                        .AddAuthenticationServices(builder.Configuration)
//                        .AddAuthorizationPolicies()
//                        .AddCorsPolicies();

//        // تكوين قاعدة البيانات - بدون RetryOnFailure
//        builder.Services.AddDbContext<AppDbContext>(options =>
//            options.UseSqlServer(
//                builder.Configuration.GetConnectionString("DefaultConnection"),
//                sqlOptions =>
//                {
//                    sqlOptions.CommandTimeout(120); // وقت أطول
//                }));

//        // Swagger - مفعل دايماً (حتى في Production)
//        builder.Services.AddEndpointsApiExplorer();
//        builder.Services.AddSwaggerGen(c =>
//        {
//            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
//            {
//                Title = "Adros API",
//                Version = "v1",
//                Description = "Adros API Documentation"
//            });

//            // إضافة تعليقات XML إذا عندك
//            // var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
//            // var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
//            // c.IncludeXmlComments(xmlPath);
//        });

//        builder.Services.AddControllers();

//        builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
//        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//        // Authentication Services
//        builder.Services.AddScoped<ITokenService, TokenService>();
//        builder.Services.AddScoped<IEmailService, MockEmailService>();
//        builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
//        builder.Services.AddScoped<ISharedUserService, SharedUserService>();

//        // Business Services
//        builder.Services.AddScoped<IStudentService, StudentService>();
//        builder.Services.AddScoped<ITeacherService, TeacherService>();
//        builder.Services.AddScoped<ILevelService, LevelService>();
//        builder.Services.AddScoped<IAssesmentService, AssesmentService>();
//        builder.Services.AddScoped<IBannerService, BannerService>();
//        builder.Services.AddScoped<ICalenderService, CalenderService>();
//        builder.Services.AddScoped<ISkillsService, SkillsService>();
//        builder.Services.AddScoped<IStageService, StageService>();
//        builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
//        builder.Services.AddScoped<ISubjectService, SubjectService>();


//        // CORS مفتوح
//        builder.Services.AddCors(options =>
//        {
//            options.AddPolicy("AllowAll", policy =>
//            {
//                policy.AllowAnyOrigin()
//                      .AllowAnyMethod()
//                      .AllowAnyHeader();
//            });
//        });

//        var app = builder.Build();

//        // CORS
//        app.UseCors("AllowAll");

//        // Swagger - مفعل دايماً
//        app.UseSwagger();
//        app.UseSwaggerUI(c =>
//        {
//            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Adros API v1");
//            c.RoutePrefix = "swagger"; // هيكون على /swagger
//            c.DocumentTitle = "Adros API Documentation";
//            c.DisplayOperationId();
//            c.DisplayRequestDuration();
//        });

//        // Middlewares الأساسية
//        app.UseHttpsRedirection();
//        app.UseAuthentication();
//        app.UseAuthorization();

//        app.MapControllers();

//        // صفحات رئيسية
//        app.MapGet("/", () => "🚀 Adros API جاهز على MonsterASP!");

//        app.MapGet("/api/health", async (AppDbContext dbContext) =>
//        {
//            try
//            {
//                var canConnect = await dbContext.Database.CanConnectAsync();
//                return Results.Json(new
//                {
//                    status = canConnect ? "healthy" : "degraded",
//                    database = canConnect ? "connected" : "disconnected",
//                    timestamp = DateTime.Now,
//                    environment = app.Environment.EnvironmentName
//                });
//            }
//            catch
//            {
//                return Results.Json(new
//                {
//                    status = "unhealthy",
//                    database = "error",
//                    timestamp = DateTime.Now
//                });
//            }
//        });

//        // تعطيل التهيئة التلقائية للبيانات
//        try
//        {
//            using var scope = app.Services.CreateScope();
//            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

//            // مجرد اختبار الاتصال
//            var canConnect = await dbContext.Database.CanConnectAsync();
//            Console.WriteLine($"Database connection: {(canConnect ? "✅ SUCCESS" : "⚠️  LIMITED")}");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"⚠️  Database warning: {ex.Message}");
//            // استمر بدون قاعدة بيانات
//        }

//        app.Run();
//    }
//}


using Adros.Application.Interfaces.IService;
using Adros.Application.Services;
using Adros.Application.Services.Client;
using Adros.Application.Services.HomeService;
using Adros.Application.Services.Security;
using Adros.Application.Services.UsersServices;
using Adros.Core.DomainServices;
using Adros.Core.DomainServices.IDomainService;
using Adros.Core.Entities;
using Adros.Persistence.Contexts;
using Adros.Persistence.Repositories;
using Adros.Persistence.UnitOfWork;
using Adros.Shared.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Adros.Apis;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // ========== الأساسيات ==========
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        // ========== مهم جداً: HttpContext Accessor ==========
        builder.Services.AddHttpContextAccessor(); // ⬅️ هذا ناقص!

        // ========== قاعدة البيانات ==========
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        // ========== Identity ==========
        builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        // ========== AutoMapper ==========
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        // ========== Register Custom Services ==========

        // Repository و UnitOfWork
        builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Authentication Services
        builder.Services.AddScoped<ITokenService, TokenService>();
        builder.Services.AddScoped<IEmailService, MockEmailService>();
        builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
        builder.Services.AddScoped<ISharedUserService, SharedUserService>();

        // Business Services
        builder.Services.AddScoped<IStudentService, StudentService>();
        builder.Services.AddScoped<ITeacherService, TeacherService>();
        builder.Services.AddScoped<ILevelService, LevelService>();
        builder.Services.AddScoped<IAssesmentService, AssesmentService>();
        builder.Services.AddScoped<IBannerService, BannerService>();
        builder.Services.AddScoped<ICalenderService, CalenderService>();
        builder.Services.AddScoped<ISkillsService, SkillsService>();
        builder.Services.AddScoped<IStageService, StageService>();
        builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
        builder.Services.AddScoped<ISubjectService, SubjectService>();

        // ========== CORS ==========
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });

        // ========== JWT Authentication ==========
        var jwtKey = builder.Configuration["JWT:Key"] ?? "GhkSgyIMsKlPm0RiW09tQuFKyACdpouO";
        var validIssuer = builder.Configuration["JWT:ValidIssuer"] ?? "https://localhost:7173";
        var validAudience = builder.Configuration["JWT:ValidAudience"] ?? "AdrosUsers";

        // Debug info
        Console.WriteLine($"=== JWT Configuration ===");
        Console.WriteLine($"Key: {jwtKey}");
        Console.WriteLine($"Issuer: {validIssuer}");
        Console.WriteLine($"Audience: {validAudience}");

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false; // للتطوير فقط
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = validIssuer,
                ValidAudience = validAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                ClockSkew = TimeSpan.Zero
            };

            // للـ Debugging
            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    Console.WriteLine($"🔴 JWT Auth Failed: {context.Exception.Message}");
                    return Task.CompletedTask;
                },
                OnTokenValidated = context =>
                {
                    Console.WriteLine($"✅ JWT Token Validated for: {context.Principal?.Identity?.Name}");
                    return Task.CompletedTask;
                }
            };
        });

        builder.Services.AddAuthorization();

        // ========== Swa   gger ==========
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "Adros API",
                Version = "v1",
                Description = "Adros Platform API"
            });

            // تعريف JWT Authentication لـ Swagger
            c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                Name = "Authorization",
                In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                Scheme = "bearer",
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
                    Array.Empty<string>()
                }
            });
        });

        var app = builder.Build();

        // ========== Middleware Pipeline ==========

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseStaticFiles();
        app.UseRouting();

        app.UseCors("AllowAll");

        app.UseAuthentication(); // ⬅️ قبل Authorization
        app.UseAuthorization();  // ⬅️ بعد Authentication

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Adros API v1");
            c.RoutePrefix = "swagger";
            c.DisplayRequestDuration();
        });

        app.MapControllers();

        app.MapGet("/", () => Results.Redirect("/swagger"));

        app.Run();
    }
}