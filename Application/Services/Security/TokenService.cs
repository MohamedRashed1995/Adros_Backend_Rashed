//using Adros.Application.Interfaces.IService;
//using Adros.Core.Entities;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace Adros.Application.Services.Security
//{
//    public class TokenService : ITokenService
//    {
//        private readonly IConfiguration _configuration;
//        private readonly ILogger<TokenService> _logger;

//        public TokenService(IConfiguration configuration, ILogger<TokenService> logger)
//        {
//            _configuration = configuration;
//            _logger = logger;
//        }

//        public async Task<string> CreateTokenAsync(ApplicationUser user, UserManager<ApplicationUser> userManager)
//        {
//            try
//            {
//                // 1️⃣ إعداد Claims
//                var authClaims = new List<Claim>
//                {
//                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
//                    new Claim("userId", user.Id.ToString()),
//                    new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
//                    new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
//                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
//                };

//                // 2️⃣ إضافة الـ Roles
//                var userRoles = await userManager.GetRolesAsync(user);
//                foreach (var role in userRoles)
//                {
//                    authClaims.Add(new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", role));
//                }

//                // 3️⃣ JWT Key من appsettings.json
//                var jwtKey = _configuration["JWT:Key"];
//                if (string.IsNullOrEmpty(jwtKey) || jwtKey.Length < 32)
//                {
//                    throw new Exception("JWT Key is missing or too short (must be at least 32 chars) in appsettings.json");
//                }

//                var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

//                // 4️⃣ مدة صلاحية التوكن
//                double durationDays = 30; // Default
//                var durationConfig = _configuration["JWT:DurationInDays"];
//                if (!string.IsNullOrEmpty(durationConfig) && double.TryParse(durationConfig, out double parsedDuration))
//                {
//                    durationDays = parsedDuration;
//                }

//                // 5️⃣ إنشاء التوكن
//                var token = new JwtSecurityToken(
//                    issuer: _configuration["JWT:ValidIssuer"],   // نفس القيمة في Program.cs
//                    audience: _configuration["JWT:ValidAudience"], // نفس القيمة في Program.cs
//                    expires: DateTime.UtcNow.AddDays(durationDays),
//                    claims: authClaims,
//                    signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256)
//                );

//                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

//                _logger.LogInformation($"Token created successfully for user: {user.Email}");

//                return tokenString;
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error creating JWT token");
//                throw;
//            }
//        }
//    }
//}

using Adros.Application.Interfaces.IService;
using Adros.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Adros.Application.Services.Security
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<TokenService> _logger;

        public TokenService(IConfiguration configuration, ILogger<TokenService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<string> CreateTokenAsync(ApplicationUser user, UserManager<ApplicationUser> userManager)
        {
            try
            {
                var authClaims = new List<Claim>
                {
                                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // 🔥 مهم جدًا
                                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName ?? string.Empty)
                };

                var roles = await userManager.GetRolesAsync(user);

               
                foreach (var role in roles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                var jwtKey = _configuration["JWT:Key"];
                var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

                var durationDays = 30;
                //if (double.TryParse(_configuration["JWT:DurationInDays"], out double parsedDays))
                    //durationDays = double.Parse(parsedDays);

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.UtcNow.AddDays(durationDays),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256)
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                _logger.LogInformation($"Token created successfully for user: {user.Email}");

                return tokenString;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating JWT token");
                throw;
            }
         }
    }
}

