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
using Adros.Infrastructure.External;
using Adros.Persistence.Contexts;
using Adros.Persistence.Repositories;
using Adros.Persistence.UnitOfWork;
using Adros.Shared.Helpers;
using Adros.Shared.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Adros.Apis.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {


            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            //    {
            //        Title = "Adros Educational Platform API",
            //        Version = "v1", // ✅ هنا لازم يكون موجود
            //        Description = "ASP.NET Core Web API for Adros Educational Platform",
            //    });
            //});

            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
            {

                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            #region Application Services Registration

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<ISharedUserService, SharedUserService>();

            services.AddAutoMapper(typeof(BaseProfile).Assembly);
            services.AddTransient(typeof(ImageUrlResolver<>));

            services.AddScoped<ISubjectService, SubjectService>();
            services.AddScoped<IBannerService, BannerService>();
            services.AddScoped<ISkillsService, SkillsService>();
            services.AddScoped<IStageService, StageService>();
            services.AddScoped<ICalenderService, CalenderService>();
            services.AddScoped<ILevelService, LevelService>();
            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();
            services.AddScoped<IAssesmentService, AssesmentService>();
            #endregion
            #region Externals
            services.AddHttpClient();
            services.AddScoped<BunnyVideoService>();
            #endregion
            services.AddHttpContextAccessor();
            services.AddControllers();

            return services;
        }


        public static IServiceCollection AddAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<ITokenService, TokenService>();
            // Configure Authentication with JWT
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
                    ValidAudience = configuration["JWT:ValidAudience"],
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
                };
            });

            return services;
        }

        public static IServiceCollection AddAuthorizationPolicies(this IServiceCollection services)
        {


            // Configure Authorization
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
                // Add other policies as needed
            });

            return services;
        }

        public static IServiceCollection AddCorsPolicies(this IServiceCollection services)
        {
           
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader());
            });

            return services;
        }
    }
}
