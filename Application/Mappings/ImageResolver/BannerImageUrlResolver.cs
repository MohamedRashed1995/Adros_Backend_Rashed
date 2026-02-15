using Adros.Core.Entities.Home;
using Adros.Shared.Constants;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Adros.Application.Mappings.ImageResolver
{
    public class BannerImageUrlResolver<TDestination>(
        IConfiguration configuration,
        IHttpContextAccessor httpContextAccessor)
        : IValueResolver<Banner, TDestination, string>
        where TDestination : class
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public string Resolve(
            Banner source,
            TDestination destination,
            string destMember,
            ResolutionContext context)
        {
            if (string.IsNullOrWhiteSpace(source.ImageName))
                return string.Empty;

            var request = _httpContextAccessor.HttpContext?.Request;
            if (request == null)
                return string.Empty;

            var baseImageUrl = _configuration["ImageSettings:BaseImageUrl"] ?? "/Images";
            var folderName = FoldersNames.Media.BannersFolder;

            var baseUrl = $"{request.Scheme}://{request.Host}";
            return $"{baseUrl}{baseImageUrl}/{folderName}/{source.ImageName}";
        }
    }
}
