<<<<<<< HEAD
﻿////using Adros.Application.Interfaces.IService;
////using Adros.Application.Services;
////using Adros.Application.Services.Client;
////using Adros.Application.Services.HomeService;
////using Adros.Application.Services.Security;
////using Adros.Application.Services.UsersServices;
////using Adros.Core.DomainServices;
////using Adros.Core.DomainServices.IDomainService;
////using Adros.Core.Entities;
////using Adros.Persistence.Contexts;
////using Adros.Persistence.Repositories;
////using Adros.Persistence.UnitOfWork;
////using Adros.Shared.Interfaces;
////using Microsoft.AspNetCore.Authentication.JwtBearer;
////using Microsoft.AspNetCore.Identity;
////using Microsoft.EntityFrameworkCore;
////using Microsoft.EntityFrameworkCore.Diagnostics;
////using Microsoft.IdentityModel.Tokens;
////using Microsoft.OpenApi.Models;
////using System.IdentityModel.Tokens.Jwt;
////using System.Security.Claims;
////using System.Text;

////namespace Adros.Apis
////{
////    public class Program
////    {
////        public static async Task Main(string[] args)
////        {
////            var builder = WebApplication.CreateBuilder(args);

////            // ===== 1. الأساسيات =====
////            builder.Services.AddControllers();
////            builder.Services.AddEndpointsApiExplorer();

////            // ===== 2. Swagger Configuration =====
////            builder.Services.AddSwaggerGen(c =>
////            {
////                c.SwaggerDoc("v1", new OpenApiInfo
////                {
////                    Title = "Adros API",
////                    Version = "v1",
////                    Description = "Educational Platform API"
////                });

////                // تعريف JWT لـ Swagger
////                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
////                {
////                    Description = "أدخل JWT بصيغة: Bearer {token}",
////                    Name = "Authorization",
////                    In = ParameterLocation.Header,
////                    Type = SecuritySchemeType.ApiKey,
////                    Scheme = "Bearer"
////                });

////                c.AddSecurityRequirement(new OpenApiSecurityRequirement
////                {
////                    {
////                        new OpenApiSecurityScheme
////                        {
////                            Reference = new OpenApiReference
////                            {
////                                Type = ReferenceType.SecurityScheme,
////                                Id = "Bearer"
////                            }
////                        },
////                        new string[] { }
////                    }
////                });
////            });

////            // ===== 3. HttpContext Accessor =====
////            builder.Services.AddHttpContextAccessor();

////            // ===== 4. Database Configuration =====
////            builder.Services.AddDbContext<AppDbContext>(options =>
////            {
////                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
////                options.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
////            });

////            // ===== 5. Identity Configuration =====
////            builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
////                .AddEntityFrameworkStores<AppDbContext>()
////                .AddDefaultTokenProviders();

////            // ===== 6. AutoMapper =====
////            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

////            // ===== 7. Register ALL Custom Services =====
////            RegisterServices(builder.Services);

////            // ===== 8. CORS Configuration =====
////            builder.Services.AddCors(options =>
////            {
////                options.AddPolicy("AllowAll", policy =>
////                {
////                    policy.AllowAnyOrigin()
////                          .AllowAnyMethod()
////                          .AllowAnyHeader();
////                });
////            });

////            // ===== 9. JWT Authentication =====
////            var jwtKey = builder.Configuration["JWT:Key"];
////            if (string.IsNullOrEmpty(jwtKey) || jwtKey.Length < 32)
////                throw new Exception("JWT Key must be at least 32 characters in appsettings.json");

////            var key = Encoding.UTF8.GetBytes(jwtKey);
////            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

////            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
////                .AddJwtBearer(options =>
////                {
////                    options.TokenValidationParameters = new TokenValidationParameters
////                    {
////                        ValidateIssuer = true,
////                        ValidateAudience = true,
////                        ValidateLifetime = true,
////                        ValidateIssuerSigningKey = true,
////                        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
////                        ValidAudience = builder.Configuration["JWT:ValidAudience"],
////                        IssuerSigningKey = new SymmetricSecurityKey(key),
////                        ClockSkew = TimeSpan.FromMinutes(5),
////                        RoleClaimType = ClaimTypes.Role
////                    };
////                });

////            builder.Services.AddAuthorization();

////            // ===== 10. Build Application =====
////            var app = builder.Build();

////            // ===== 11. Configure Middleware Pipeline =====

////            // 11.1 Swagger دائمًا مفعل
////            app.UseSwagger();
////            app.UseSwaggerUI(c =>
////            {
////                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Adros API v1");
////                c.RoutePrefix = "swagger";
////                c.DisplayRequestDuration();
////            });

////            // 11.2 Static Files
////            app.UseStaticFiles();

////            // 11.3 Routing
////            app.UseRouting();

////            // 11.4 CORS
////            app.UseCors("AllowAll");

////            // 11.5 Authentication & Authorization
////            app.UseAuthentication();
////            app.UseAuthorization();

////            // 11.6 Map Controllers
////            app.MapControllers();

////            // 11.7 Health Check Endpoints
////            //app.MapGet("/", () => Results.Json(new
////            //{
////            //    Application = "Adros Educational Platform",
////            //    Version = "1.0.0",
////            //    Status = "Running",
////            //    Documentation = "/swagger",
////            //    Health = "/health",
////            //    Time = DateTime.UtcNow,
////            //    Endpoints = new[]
////            //    {
////            //        "/api/Auth/login",
////            //        "/api/Auth/register",
////            //        "/api/Student/profile",
////            //        "/api/Teacher/lessons",
////            //        "/api/Student/info"
////            //    }
////            //}));

////            //app.MapGet("/health", () => Results.Ok(new
////            //{
////            //    Status = "Healthy",
////            //    Database = "Connected",
////            //    Time = DateTime.UtcNow
////            //}));

////            // ===== 12. Seed Database =====
////            try
////            {
////                using var scope = app.Services.CreateScope();
////                var services = scope.ServiceProvider;

////                var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
////                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

////                await SeedRoles(roleManager);
////                await SeedAdminUser(userManager);

////                Console.WriteLine("Database seeded successfully!");
////            }
////            catch (Exception ex)
////            {
////                Console.WriteLine($"Seeder Error: {ex.Message}");
////            }

////            // ===== 13. Run Application =====
////            app.Run();
////        }

////        // ===== Method لتسجيل كل الـ Services =====
////        private static void RegisterServices(IServiceCollection services)
////        {
////            // Generic Repository
////            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

////            // Unit of Work
////            services.AddScoped<IUnitOfWork, UnitOfWork>();

////            // Security Services
////            services.AddScoped<ITokenService, TokenService>();
////            services.AddScoped<IEmailService, MockEmailService>();
////            services.AddScoped<ICurrentUserService, CurrentUserService>();

////            // User Services
////            services.AddScoped<ISharedUserService, SharedUserService>();
////            services.AddScoped<IStudentService, StudentService>();
////            services.AddScoped<ITeacherService, TeacherService>();

////            // Home Services
////            services.AddScoped<ILevelService, LevelService>();
////            services.AddScoped<IAssesmentService, AssesmentService>();
////            services.AddScoped<IBannerService, BannerService>();
////            services.AddScoped<ICalenderService, CalenderService>();
////            services.AddScoped<ISkillsService, SkillsService>();
////            services.AddScoped<IStageService, StageService>();
////            services.AddScoped<ISubscriptionService, SubscriptionService>();
////            services.AddScoped<ISubjectService, SubjectService>();
////            services.AddScoped<ILessonsService, LessonsService>();

////            // Client Services
////            //services.AddScoped<IClientLevelService, ClientLevelService>();
////            //services.AddScoped<IClientAssesmentService, ClientAssesmentService>();
////            //services.AddScoped<IClientBannerService, ClientBannerService>();
////            //services.AddScoped<IClientCalenderService, ClientCalenderService>();
////            //services.AddScoped<IClientSkillsService, ClientSkillsService>();
////            //services.AddScoped<IClientStageService, ClientStageService>();
////            //services.AddScoped<IClientSubjectService, ClientSubjectService>();
////            //services.AddScoped<IClientLessonsService, ClientLessonsService>();

////            // Add any other services you have
////        }

////        // ===== Method لـ Seed Roles =====
////        private static async Task SeedRoles(RoleManager<IdentityRole<Guid>> roleManager)
////        {
////            string[] roles = { "Admin", "Teacher", "Student" };

////            foreach (var role in roles)
////            {
////                if (!await roleManager.RoleExistsAsync(role))
////                {
////                    await roleManager.CreateAsync(new IdentityRole<Guid>(role));
////                }
////            }
////        }

////        // ===== Method لـ Seed Admin User =====
////        private static async Task SeedAdminUser(UserManager<ApplicationUser> userManager)
////        {
////            var adminEmail = "admin@adros.com";
////            var adminUser = await userManager.FindByEmailAsync(adminEmail);

////            if (adminUser == null)
////            {
////                adminUser = new ApplicationUser
////                {
////                    UserName = adminEmail,
////                    Email = adminEmail,
////                    FirstName = "Admin",
////                    LastName = "User",
////                    IsActive = true
////                };

////                var result = await userManager.CreateAsync(adminUser, "Admin@123");
////                if (result.Succeeded)
////                {
////                    await userManager.AddToRoleAsync(adminUser, "Admin");
////                }
////            }
////        }
////    }
////}


//using Adros.Application.Interfaces.IService;
//using Adros.Application.Mappings;
//using Adros.Application.Services;
//using Adros.Application.Services.Client;
//using Adros.Application.Services.CourseService;
//using Adros.Application.Services.CourseServices;
=======
﻿//using Adros.Application.Interfaces.IService;
//using Adros.Application.Services;
//using Adros.Application.Services.Client;
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
//using Adros.Application.Services.HomeService;
//using Adros.Application.Services.Security;
//using Adros.Application.Services.UsersServices;
//using Adros.Core.DomainServices;
//using Adros.Core.DomainServices.IDomainService;
//using Adros.Core.Entities;
<<<<<<< HEAD
//using Adros.Infrastructure.External;
=======
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
//using Adros.Persistence.Contexts;
//using Adros.Persistence.Repositories;
//using Adros.Persistence.UnitOfWork;
//using Adros.Shared.Interfaces;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Diagnostics;
<<<<<<< HEAD
//using Microsoft.Extensions.FileProviders;
=======
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
//using Microsoft.IdentityModel.Tokens;
//using Microsoft.OpenApi.Models;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace Adros.Apis
//{
//    public class Program
//    {
//        public static async Task Main(string[] args)
//        {
//            var builder = WebApplication.CreateBuilder(args);

//            // ===== 1. الأساسيات =====
//            builder.Services.AddControllers();
//            builder.Services.AddEndpointsApiExplorer();

//            // ===== 2. Swagger Configuration =====
//            builder.Services.AddSwaggerGen(c =>
//            {
//                c.SwaggerDoc("v1", new OpenApiInfo
//                {
//                    Title = "Adros API",
//                    Version = "v1",
<<<<<<< HEAD
//                    Description = "Educational Platform API",
//                    Contact = new OpenApiContact
//                    {
//                        Name = "Adros Team",
//                        Email = "support@adros.com"
//                    }
=======
//                    Description = "Educational Platform API"
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
//                });

//                // تعريف JWT لـ Swagger
//                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//                {
<<<<<<< HEAD

//                    Name = "Authorization",
//                    In = ParameterLocation.Header,
//                    Type = SecuritySchemeType.ApiKey,
//                    Description = "ضع التوكن مباشرة بدون كلمة Bearer"
=======
//                    Description = "أدخل JWT بصيغة: Bearer {token}",
//                    Name = "Authorization",
//                    In = ParameterLocation.Header,
//                    Type = SecuritySchemeType.ApiKey,
//                    Scheme = "Bearer"
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
//                });

//                c.AddSecurityRequirement(new OpenApiSecurityRequirement
//                {
//                    {
//                        new OpenApiSecurityScheme
//                        {
//                            Reference = new OpenApiReference
//                            {
//                                Type = ReferenceType.SecurityScheme,
//                                Id = "Bearer"
//                            }
//                        },
<<<<<<< HEAD
//                        Array.Empty<string>()
//                    }
//                });

//                // تفعيل الـ Try It Out
//                //c.EnableAnnotations();
//                c.CustomSchemaIds(x => x.FullName);
=======
//                        new string[] { }
//                    }
//                });
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
//            });

//            // ===== 3. HttpContext Accessor =====
//            builder.Services.AddHttpContextAccessor();

//            // ===== 4. Database Configuration =====
//            builder.Services.AddDbContext<AppDbContext>(options =>
//            {
//                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
//                options.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
<<<<<<< HEAD

//                if (builder.Environment.IsDevelopment())
//                {
//                    options.EnableSensitiveDataLogging();
//                    options.EnableDetailedErrors();
//                }
=======
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
//            });

//            // ===== 5. Identity Configuration =====
//            builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
//                .AddEntityFrameworkStores<AppDbContext>()
//                .AddDefaultTokenProviders();

//            // ===== 6. AutoMapper =====
//            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
<<<<<<< HEAD
//            builder.Services.AddAutoMapper(typeof(BannerProfile));
//            builder.Services.AddAutoMapper(typeof(VideoProfile).Assembly);

//            // ===== 7. Register ALL Services =====
//            RegisterAllServices(builder.Services);
=======

//            // ===== 7. Register ALL Custom Services =====
//            RegisterServices(builder.Services);
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a

//            // ===== 8. CORS Configuration =====
//            builder.Services.AddCors(options =>
//            {
//                options.AddPolicy("AllowAll", policy =>
//                {
//                    policy.AllowAnyOrigin()
//                          .AllowAnyMethod()
<<<<<<< HEAD
//                          .AllowAnyHeader()
//                          .WithExposedHeaders("Authorization");
=======
//                          .AllowAnyHeader();
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
//                });
//            });

//            // ===== 9. JWT Authentication =====
//            var jwtKey = builder.Configuration["JWT:Key"];
<<<<<<< HEAD

//            // إذا مفيش Key في الإعدادات، استخدم واحد افتراضي للتنمية
//            if (string.IsNullOrEmpty(jwtKey))
//            {
//                jwtKey = "YourSuperSecretKeyForDevelopment123456789012345";
//                Console.WriteLine("Warning: Using development JWT key. Set a proper key in appsettings.json for production.");
//            }
//            else if (jwtKey.Length < 32)
//            {
//                Console.WriteLine("Warning: JWT Key is too short or missing. Using temporary development key.");
//                jwtKey = "ThisIsADevJWTKeyWith32CharsOrMore12345";

//                //throw new Exception("JWT Key must be at least 32 characters in appsettings.json");
//            }
=======
//            if (string.IsNullOrEmpty(jwtKey) || jwtKey.Length < 32)
//                throw new Exception("JWT Key must be at least 32 characters in appsettings.json");
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a

//            var key = Encoding.UTF8.GetBytes(jwtKey);
//            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

<<<<<<< HEAD
//            builder.Services.AddAuthentication(options =>
//            {
//                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//            })
//            .AddJwtBearer(options =>
//            {
//                options.RequireHttpsMetadata = false;
//                options.SaveToken = true;
//                options.TokenValidationParameters = new TokenValidationParameters
//                {
//                    ValidateIssuer = true,
//                    ValidateAudience = true,
//                    ValidateLifetime = true,
//                    ValidateIssuerSigningKey = true,
//                    ValidIssuer = builder.Configuration["JWT:ValidIssuer"] ?? "AdrosAPI",
//                    ValidAudience = builder.Configuration["JWT:ValidAudience"] ?? "AdrosUsers",
//                    IssuerSigningKey = new SymmetricSecurityKey(key),
//                    ClockSkew = TimeSpan.Zero,
//                    NameClaimType = ClaimTypes.Name,
//                    RoleClaimType = ClaimTypes.Role
//                };

//                // Debug events
//                options.Events = new JwtBearerEvents
//                {
//                    OnAuthenticationFailed = context =>
//                    {
//                        Console.WriteLine($"Authentication failed: {context.Exception.Message}");
//                        return Task.CompletedTask;
//                    },
//                    OnTokenValidated = context =>
//                    {
//                        var userId = context.Principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//                        var roles = context.Principal?.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();
//                        Console.WriteLine($"Token validated for user: {userId}, Roles: {string.Join(", ", roles ?? new List<string>())}");
//                        return Task.CompletedTask;
//                    }
//                };
//            });

//            // ===== 10. Authorization =====
//            builder.Services.AddAuthorization(options =>
//            {
//                options.AddPolicy("StudentOnly", policy =>
//                    policy.RequireRole("Student"));

//                options.AddPolicy("TeacherOnly", policy =>
//                    policy.RequireRole("Teacher"));

//                options.AddPolicy("AdminOnly", policy =>
//                    policy.RequireRole("Admin"));
//            });
//            // ===== Ensure Upload Folders Exist =====
//            void EnsureUploadFolders()
//            {
//                string rootFolder = @"D:\AdrosUploads";
//                string[] subFolders = { "Attachments", "Images", "Videos" };

//                if (!Directory.Exists(rootFolder))
//                    Directory.CreateDirectory(rootFolder);

//                foreach (var folder in subFolders)
//                {
//                    string path = Path.Combine(rootFolder, folder);
//                    if (!Directory.Exists(path))
//                        Directory.CreateDirectory(path);
//                }

//                Console.WriteLine("Upload folders ensured:");
//                Console.WriteLine($"Root: {rootFolder}");
//                foreach (var folder in subFolders)
//                    Console.WriteLine($" - {folder}");
//            }

//            // Call it before app.Run()
//            EnsureUploadFolders();

//            // ===== 11. Build Application =====
//            var app = builder.Build();

//            // ===== 12. Configure Middleware Pipeline =====

//            // 12.1 Exception Handling
//            if (app.Environment.IsDevelopment())
//            {
//                app.UseDeveloperExceptionPage();
//            }
//            else
//            {
//                app.UseExceptionHandler("/error");
//                app.UseHsts();
//            }

//            // 12.2 Swagger ALWAYS enabled
=======
//            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//                .AddJwtBearer(options =>
//                {
//                    options.TokenValidationParameters = new TokenValidationParameters
//                    {
//                        ValidateIssuer = true,
//                        ValidateAudience = true,
//                        ValidateLifetime = true,
//                        ValidateIssuerSigningKey = true,
//                        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
//                        ValidAudience = builder.Configuration["JWT:ValidAudience"],
//                        IssuerSigningKey = new SymmetricSecurityKey(key),
//                        ClockSkew = TimeSpan.FromMinutes(5),
//                        RoleClaimType = ClaimTypes.Role
//                    };
//                });

//            builder.Services.AddAuthorization();

//            // ===== 10. Build Application =====
//            var app = builder.Build();

//            // ===== 11. Configure Middleware Pipeline =====

//            // 11.1 Swagger دائمًا مفعل
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
//            app.UseSwagger();
//            app.UseSwaggerUI(c =>
//            {
//                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Adros API v1");
//                c.RoutePrefix = "swagger";
//                c.DisplayRequestDuration();
<<<<<<< HEAD
//                c.EnableTryItOutByDefault();
//                c.EnablePersistAuthorization();
//                c.DisplayOperationId();
//                c.EnableFilter();
//                c.DefaultModelExpandDepth(2);
//                c.DefaultModelsExpandDepth(-1);
//                c.ShowCommonExtensions();
//                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
//            });
//            app.UseStaticFiles(new StaticFileOptions
//            {
//                FileProvider = new PhysicalFileProvider(@"D:\AdrosUploads"),
//                RequestPath = "/uploads"
//            });

//            // 12.3 Static Files
//            app.UseStaticFiles();

//            // 12.4 Routing
//            app.UseRouting();

//            // 12.5 CORS (يجب يكون قبل Authentication)
//            app.UseCors("AllowAll");

//            // 12.6 Authentication & Authorization
//            app.UseAuthentication();
//            app.UseAuthorization();

//            // 12.7 Map Controllers
//            app.MapControllers();

//            // 12.8 Health Check and Info Endpoints


//            // 12.9 Request Logging Middleware
//            app.Use(async (context, next) =>
//            {
//                var request = context.Request;
//                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {request.Method} {request.Path}");

//                await next();

//                var response = context.Response;
//                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Response: {response.StatusCode}");
//            });


//            app.Use(async (context, next) =>
//            {
//                try
//                {
//                    await next();
//                }
//                catch (Exception ex)
//                {
//                    Console.WriteLine($"[Error] {ex.Message}\n{ex.StackTrace}");
//                    context.Response.StatusCode = 500;
//                    await context.Response.WriteAsJsonAsync(new
//                    {
//                        statusCode = 500,
//                        message = "Internal server error",
//                        detail = ex.Message
//                    });
//                }
//            });



//            // ===== 13. Seed Database =====
=======
//            });

//            // 11.2 Static Files
//            app.UseStaticFiles();

//            // 11.3 Routing
//            app.UseRouting();

//            // 11.4 CORS
//            app.UseCors("AllowAll");

//            // 11.5 Authentication & Authorization
//            app.UseAuthentication();
//            app.UseAuthorization();

//            // 11.6 Map Controllers
//            app.MapControllers();

//            // 11.7 Health Check Endpoints
//            //app.MapGet("/", () => Results.Json(new
//            //{
//            //    Application = "Adros Educational Platform",
//            //    Version = "1.0.0",
//            //    Status = "Running",
//            //    Documentation = "/swagger",
//            //    Health = "/health",
//            //    Time = DateTime.UtcNow,
//            //    Endpoints = new[]
//            //    {
//            //        "/api/Auth/login",
//            //        "/api/Auth/register",
//            //        "/api/Student/profile",
//            //        "/api/Teacher/lessons",
//            //        "/api/Student/info"
//            //    }
//            //}));

//            //app.MapGet("/health", () => Results.Ok(new
//            //{
//            //    Status = "Healthy",
//            //    Database = "Connected",
//            //    Time = DateTime.UtcNow
//            //}));

//            // ===== 12. Seed Database =====
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
//            try
//            {
//                using var scope = app.Services.CreateScope();
//                var services = scope.ServiceProvider;

<<<<<<< HEAD
//                var context = services.GetRequiredService<AppDbContext>();
//                await context.Database.EnsureCreatedAsync();

//                var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
//                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

//                await SeedRolesAndUsers(roleManager, userManager);
=======
//                var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
//                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

//                await SeedRoles(roleManager);
//                await SeedAdminUser(userManager);
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a

//                Console.WriteLine("Database seeded successfully!");
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Seeder Error: {ex.Message}");
<<<<<<< HEAD
//                Console.WriteLine($"StackTrace: {ex.StackTrace}");
//            }

//            // ===== 14. Run Application =====
//            Console.WriteLine($"Starting Adros API in {app.Environment.EnvironmentName} environment...");
//            Console.WriteLine($"Swagger UI: https://localhost:7173/swagger");
//            Console.WriteLine($"Health Check: https://localhost:7173/health");

//            await app.RunAsync();
//        }

//        // ===== Method لتسجيل كل الـ Services =====
//        private static void RegisterAllServices(IServiceCollection services)
=======
//            }

//            // ===== 13. Run Application =====
//            app.Run();
//        }

//        // ===== Method لتسجيل كل الـ Services =====
//        private static void RegisterServices(IServiceCollection services)
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
//        {
//            // Generic Repository
//            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

//            // Unit of Work
//            services.AddScoped<IUnitOfWork, UnitOfWork>();

//            // Security Services
//            services.AddScoped<ITokenService, TokenService>();
//            services.AddScoped<IEmailService, MockEmailService>();
//            services.AddScoped<ICurrentUserService, CurrentUserService>();

//            // User Services
//            services.AddScoped<ISharedUserService, SharedUserService>();
//            services.AddScoped<IStudentService, StudentService>();
//            services.AddScoped<ITeacherService, TeacherService>();

//            // Home Services
//            services.AddScoped<ILevelService, LevelService>();
//            services.AddScoped<IAssesmentService, AssesmentService>();
//            services.AddScoped<IBannerService, BannerService>();
//            services.AddScoped<ICalenderService, CalenderService>();
//            services.AddScoped<ISkillsService, SkillsService>();
//            services.AddScoped<IStageService, StageService>();
//            services.AddScoped<ISubscriptionService, SubscriptionService>();
//            services.AddScoped<ISubjectService, SubjectService>();
//            services.AddScoped<ILessonsService, LessonsService>();
<<<<<<< HEAD
//            services.AddScoped<IUnitService, UnitService>();
//            services.AddScoped<IVideoService, VideoService>();
//            services.AddHttpClient<BunnyVideoService>();
//            services.AddScoped<IAttachmentService, AttachmentService>();
//        }

//        // ===== Method لـ Seed Roles and Users =====
//        private static async Task SeedRolesAndUsers(
//            RoleManager<IdentityRole<Guid>> roleManager,
//            UserManager<ApplicationUser> userManager)
//        {
//            // Seed Roles
=======

//            // Client Services
//            //services.AddScoped<IClientLevelService, ClientLevelService>();
//            //services.AddScoped<IClientAssesmentService, ClientAssesmentService>();
//            //services.AddScoped<IClientBannerService, ClientBannerService>();
//            //services.AddScoped<IClientCalenderService, ClientCalenderService>();
//            //services.AddScoped<IClientSkillsService, ClientSkillsService>();
//            //services.AddScoped<IClientStageService, ClientStageService>();
//            //services.AddScoped<IClientSubjectService, ClientSubjectService>();
//            //services.AddScoped<IClientLessonsService, ClientLessonsService>();

//            // Add any other services you have
//        }

//        // ===== Method لـ Seed Roles =====
//        private static async Task SeedRoles(RoleManager<IdentityRole<Guid>> roleManager)
//        {
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
//            string[] roles = { "Admin", "Teacher", "Student" };

//            foreach (var role in roles)
//            {
//                if (!await roleManager.RoleExistsAsync(role))
//                {
//                    await roleManager.CreateAsync(new IdentityRole<Guid>(role));
<<<<<<< HEAD
//                    Console.WriteLine($"Created role: {role}");
//                }
//            }

//            // Seed Admin User
=======
//                }
//            }
//        }

//        // ===== Method لـ Seed Admin User =====
//        private static async Task SeedAdminUser(UserManager<ApplicationUser> userManager)
//        {
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
//            var adminEmail = "admin@adros.com";
//            var adminUser = await userManager.FindByEmailAsync(adminEmail);

//            if (adminUser == null)
//            {
//                adminUser = new ApplicationUser
//                {
//                    UserName = adminEmail,
//                    Email = adminEmail,
//                    FirstName = "Admin",
//                    LastName = "User",
<<<<<<< HEAD
//                    IsActive = true,
//                    EmailConfirmed = true
=======
//                    IsActive = true
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
//                };

//                var result = await userManager.CreateAsync(adminUser, "Admin@123");
//                if (result.Succeeded)
//                {
//                    await userManager.AddToRoleAsync(adminUser, "Admin");
<<<<<<< HEAD
//                    Console.WriteLine($"Created admin user: {adminEmail}");
//                }
//                else
//                {
//                    Console.WriteLine($"Failed to create admin user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
//                }
//            }

//            // Seed Student User (للاختبار)
//            var studentEmail = "student@adros.com";
//            var studentUser = await userManager.FindByEmailAsync(studentEmail);

//            if (studentUser == null)
//            {
//                studentUser = new ApplicationUser
//                {
//                    UserName = studentEmail,
//                    Email = studentEmail,
//                    FirstName = "Test",
//                    LastName = "Student",
//                    IsActive = true,
//                    EmailConfirmed = true
//                };

//                var result = await userManager.CreateAsync(studentUser, "Student@123");
//                if (result.Succeeded)
//                {
//                    await userManager.AddToRoleAsync(studentUser, "Student");
//                    Console.WriteLine($"Created student user: {studentEmail}");
=======
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
//                }
//            }
//        }
//    }
//}

<<<<<<< HEAD
using Adros.Application.DTOs;
=======

>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
using Adros.Application.Interfaces.IService;
using Adros.Application.Mappings;
using Adros.Application.Services;
using Adros.Application.Services.Client;
using Adros.Application.Services.CourseService;
using Adros.Application.Services.CourseServices;
<<<<<<< HEAD
using Adros.Application.Services.CoursesSevices;
=======
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
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
<<<<<<< HEAD
using Adros.Shared.Helpers;
=======
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
using Adros.Shared.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
<<<<<<< HEAD
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;
//using Microsoft.Extensions.FileProviders;

=======
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a

namespace Adros.Apis
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ===== 1. الأساسيات =====
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            // ===== 2. Swagger Configuration =====
            builder.Services.AddSwaggerGen(c =>
            {
<<<<<<< HEAD
                c.SwaggerDoc("v1", new() { Title = "Adros API", Version = "v1" });

                // إضافة دعم الـ Bearer Token
             c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Name = "Authorization",
                            Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                            Scheme = "bearer",
                            BearerFormat = "JWT",
                            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                            Description = "Enter JWT token like this: Bearer {your token}"
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
                    }
                    ,
                    Array.Empty<string>()
                    }
                }
                );
            });



=======
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Adros API",
                    Version = "v1",
                    Description = "Educational Platform API",
                    Contact = new OpenApiContact
                    {
                        Name = "Adros Team",
                        Email = "support@adros.com"
                    }
                });

                // تعريف JWT لـ Swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                   
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Description = "ضع التوكن مباشرة بدون كلمة Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                // تفعيل الـ Try It Out
                //c.EnableAnnotations();
                c.CustomSchemaIds(x => x.FullName);
            });

>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
            // ===== 3. HttpContext Accessor =====
            builder.Services.AddHttpContextAccessor();

            // ===== 4. Database Configuration =====
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                options.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));

<<<<<<< HEAD
                //if (builder.Environment.IsDevelopment())
                //{
                //    options.EnableSensitiveDataLogging();
                //    options.EnableDetailedErrors();
                //}
=======
                if (builder.Environment.IsDevelopment())
                {
                    options.EnableSensitiveDataLogging();
                    options.EnableDetailedErrors();
                }
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
            });

            // ===== 5. Identity Configuration =====
            builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            // ===== 6. AutoMapper =====
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddAutoMapper(typeof(BannerProfile));
            builder.Services.AddAutoMapper(typeof(VideoProfile).Assembly);
<<<<<<< HEAD
            //builder.Services.AddControllers();
=======
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a

            // ===== 7. Register ALL Services =====
            RegisterAllServices(builder.Services);

            // ===== 8. CORS Configuration =====
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader()
<<<<<<< HEAD
                          ;
=======
                          .WithExposedHeaders("Authorization");
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
                });
            });

            // ===== 9. JWT Authentication =====
            var jwtKey = builder.Configuration["JWT:Key"];
<<<<<<< HEAD
            var key = Encoding.UTF8.GetBytes(jwtKey);
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            builder.Services.AddAuthentication(op =>
            {
                op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    //options.RequireHttpsMetadata = false;
                    //options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["JWT:ValidIssuer"] ?? "AdrosAPI",
                        ValidAudience = builder.Configuration["JWT:ValidAudience"] ?? "AdrosUsers",
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ClockSkew = TimeSpan.Zero,
                        NameClaimType = ClaimTypes.NameIdentifier,
                        RoleClaimType = ClaimTypes.Role

                    };
                });


            builder.Services.Configure<PaymobSettings>(
                        builder.Configuration.GetSection("Paymob"));


            //builder.Services.AddAuthorization();



            // طباعة كل الـ Controllers مع Assembly
            var allControllerTypes = Assembly.GetExecutingAssembly()
                                             .GetTypes()
                                             .Where(t => t.IsSubclassOf(typeof(Microsoft.AspNetCore.Mvc.ControllerBase)));

            Console.WriteLine("=== Controllers in Current Assembly ===");
            foreach (var ctrl in allControllerTypes)
            {
                Console.WriteLine($"{ctrl.FullName}  |  Assembly: {ctrl.Assembly.FullName}");
            }

            // لو عايز تفحص assemblies تانية (زي Adros.Application):
            var otherAssembly = typeof(Adros.Apis.Controllers.Admin.LessonsController).Assembly;
            var otherControllers = otherAssembly.GetTypes()
                                                .Where(t => t.IsSubclassOf(typeof(Microsoft.AspNetCore.Mvc.ControllerBase)));

            Console.WriteLine("=== Controllers in Other Assembly ===");
            foreach (var ctrl in otherControllers)
            {
                Console.WriteLine($"{ctrl.FullName}  |  Assembly: {ctrl.Assembly.FullName}");
            }



            // ===== 10. Ensure Upload Folders Exist =====
            string uploadsRoot = Path.Combine(builder.Environment.ContentRootPath, "Uploads");
            string[] subFolders = { "Attachments", "Images", "Videos" };

            if (!Directory.Exists(uploadsRoot))
                Directory.CreateDirectory(uploadsRoot);

            foreach (var folder in subFolders)
            {
                string path = Path.Combine(uploadsRoot, folder);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
            }

            Console.WriteLine("Upload folders ensured at: " + uploadsRoot);
=======

            // إذا مفيش Key في الإعدادات، استخدم واحد افتراضي للتنمية
            if (string.IsNullOrEmpty(jwtKey))
            {
                jwtKey = "YourSuperSecretKeyForDevelopment123456789012345";
                Console.WriteLine("Warning: Using development JWT key. Set a proper key in appsettings.json for production.");
            }
            else if (jwtKey.Length < 32)
            {
                Console.WriteLine("Warning: JWT Key is too short or missing. Using temporary development key.");
                jwtKey = "ThisIsADevJWTKeyWith32CharsOrMore12345";

                //throw new Exception("JWT Key must be at least 32 characters in appsettings.json");
            }

            var key = Encoding.UTF8.GetBytes(jwtKey);
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["JWT:ValidIssuer"] ?? "AdrosAPI",
                    ValidAudience = builder.Configuration["JWT:ValidAudience"] ?? "AdrosUsers",
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero,
                    NameClaimType = ClaimTypes.Name,
                    RoleClaimType = ClaimTypes.Role
                };

                // Debug events
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        var userId = context.Principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                        var roles = context.Principal?.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();
                        Console.WriteLine($"Token validated for user: {userId}, Roles: {string.Join(", ", roles ?? new List<string>())}");
                        return Task.CompletedTask;
                    }
                };
            });

            // ===== 10. Authorization =====
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("StudentOnly", policy =>
                    policy.RequireRole("Student"));

                options.AddPolicy("TeacherOnly", policy =>
                    policy.RequireRole("Teacher"));

                options.AddPolicy("AdminOnly", policy =>
                    policy.RequireRole("Admin"));
            });
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a

            // ===== 11. Build Application =====
            var app = builder.Build();

<<<<<<< HEAD
            // ===== 12. Middleware =====
            //if (app.Environment.IsDevelopment())
            //    app.UseDeveloperExceptionPage();
            //else
            //    app.UseExceptionHandler("/error");

            // Swagger
=======
            // ===== 12. Configure Middleware Pipeline =====

            // 12.1 Exception Handling
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
                app.UseHsts();
            }

            // 12.2 Swagger ALWAYS enabled
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Adros API v1");
                c.RoutePrefix = "swagger";
<<<<<<< HEAD
            });

            // Static files for uploads
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(uploadsRoot),
            //    RequestPath = "/uploads"
            //});
            app.UseStaticFiles(); // wwwroot الافتراضي

            FileManager.Init(app.Services.GetRequiredService<IWebHostEnvironment>());
            

            FileManager.Init(app.Environment);

            //""
            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(uploadsPath),
                RequestPath = "/Uploads"
            });





            app.UseCors("AllowAll");

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            // ===== 13. Error Logging Middleware =====
            //app.Use(async (context, next) =>
            //{
            //    try
            //    {
            //        await next();
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine($"[Error] {ex.Message}");
            //        context.Response.StatusCode = 500;
            //        await context.Response.WriteAsJsonAsync(new
            //        {
            //            statusCode = 500,
            //            message = "Internal server error",
            //            detail = ex.Message
            //        });
            //    }
            //});

            // ===== 14. Seed Database =====
=======
                c.DisplayRequestDuration();
                c.EnableTryItOutByDefault();
                c.EnablePersistAuthorization();
                c.DisplayOperationId();
                c.EnableFilter();
                c.DefaultModelExpandDepth(2);
                c.DefaultModelsExpandDepth(-1);
                c.ShowCommonExtensions();
                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
            });

            // 12.3 Static Files
            app.UseStaticFiles();

            // 12.4 Routing
            app.UseRouting();

            // 12.5 CORS (يجب يكون قبل Authentication)
            app.UseCors("AllowAll");

            // 12.6 Authentication & Authorization
            app.UseAuthentication();
            app.UseAuthorization();

            // 12.7 Map Controllers
            app.MapControllers();

            // 12.8 Health Check and Info Endpoints
            

            // 12.9 Request Logging Middleware
            app.Use(async (context, next) =>
            {
                var request = context.Request;
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {request.Method} {request.Path}");

                await next();

                var response = context.Response;
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Response: {response.StatusCode}");
            });

            app.Use(async (context, next) =>
            {
                try
                {
                    await next();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Error] {ex.Message}\n{ex.StackTrace}");
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsJsonAsync(new
                    {
                        statusCode = 500,
                        message = "Internal server error",
                        detail = ex.Message
                    });
                }
            });



            // ===== 13. Seed Database =====
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
            try
            {
                using var scope = app.Services.CreateScope();
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<AppDbContext>();
                await context.Database.EnsureCreatedAsync();

                var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

                await SeedRolesAndUsers(roleManager, userManager);
<<<<<<< HEAD
=======

                Console.WriteLine("Database seeded successfully!");
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Seeder Error: {ex.Message}");
<<<<<<< HEAD
            }

            // ===== 15. Run App =====
            await app.RunAsync();
        }

        private static void RegisterAllServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IEmailService, MockEmailService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<ISharedUserService, SharedUserService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ITeacherService, TeacherService>();
=======
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
            }

            // ===== 14. Run Application =====
            Console.WriteLine($"Starting Adros API in {app.Environment.EnvironmentName} environment...");
            Console.WriteLine($"Swagger UI: https://localhost:7173/swagger");
            Console.WriteLine($"Health Check: https://localhost:7173/health");

            await app.RunAsync();
        }

        // ===== Method لتسجيل كل الـ Services =====
        private static void RegisterAllServices(IServiceCollection services)
        {
            // Generic Repository
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

            // Unit of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Security Services
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IEmailService, MockEmailService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            // User Services
            services.AddScoped<ISharedUserService, SharedUserService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ITeacherService, TeacherService>();

            // Home Services
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
            services.AddScoped<ILevelService, LevelService>();
            services.AddScoped<IAssesmentService, AssesmentService>();
            services.AddScoped<IBannerService, BannerService>();
            services.AddScoped<ICalenderService, CalenderService>();
            services.AddScoped<ISkillsService, SkillsService>();
            services.AddScoped<IStageService, StageService>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();
            services.AddScoped<ISubjectService, SubjectService>();
            services.AddScoped<ILessonsService, LessonsService>();
            services.AddScoped<IUnitService, UnitService>();
            services.AddScoped<IVideoService, VideoService>();
            services.AddHttpClient<BunnyVideoService>();
<<<<<<< HEAD
            services.AddScoped<IAttachmentService, AttachmentService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddHttpClient<IVimeoService, VimeoService>()
            .ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri("https://api.vimeo.com/");
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "VIMEO_ACCESS_TOKEN");
            });

            // ===== Paymob Services =====
            services.AddHttpClient<IPaymobService, PaymobService>();

        }

=======
        }

        // ===== Method لـ Seed Roles and Users =====
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        private static async Task SeedRolesAndUsers(
            RoleManager<IdentityRole<Guid>> roleManager,
            UserManager<ApplicationUser> userManager)
        {
<<<<<<< HEAD
            string[] roles = { "Admin", "Teacher", "Student" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole<Guid>(role));
            }

            var adminEmail = "admin@adros.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
=======
            // Seed Roles
            string[] roles = { "Admin", "Teacher", "Student" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>(role));
                    Console.WriteLine($"Created role: {role}");
                }
            }

            // Seed Admin User
            var adminEmail = "admin@adros.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "Admin",
                    LastName = "User",
                    IsActive = true,
                    EmailConfirmed = true
                };
<<<<<<< HEAD
                var result = await userManager.CreateAsync(adminUser, "Admin@123");
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}
=======

                var result = await userManager.CreateAsync(adminUser, "Admin@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                    Console.WriteLine($"Created admin user: {adminEmail}");
                }
                else
                {
                    Console.WriteLine($"Failed to create admin user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }

            // Seed Student User (للاختبار)
            var studentEmail = "student@adros.com";
            var studentUser = await userManager.FindByEmailAsync(studentEmail);

            if (studentUser == null)
            {
                studentUser = new ApplicationUser
                {
                    UserName = studentEmail,
                    Email = studentEmail,
                    FirstName = "Test",
                    LastName = "Student",
                    IsActive = true,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(studentUser, "Student@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(studentUser, "Student");
                    Console.WriteLine($"Created student user: {studentEmail}");
                }
            }
        }
    }
}
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
