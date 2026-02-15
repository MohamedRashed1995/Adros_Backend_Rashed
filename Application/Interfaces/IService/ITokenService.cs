using Adros.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Adros.Application.Interfaces.IService
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(ApplicationUser user, UserManager<ApplicationUser> manager);

    }
}
