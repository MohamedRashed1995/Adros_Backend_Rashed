using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Adros.Shared.Helpers
{
    public class ImageUrlResolver<TSource>(IConfiguration configuration , IHttpContextAccessor httpContextAccessor) : IValueResolver<TSource, object, string> where TSource : class
    {
        private readonly string _baseImageUrl = configuration["ImageSettings:BaseImageUrl"] ?? "/Images";
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public string Resolve(TSource source, object destination, string destMember, ResolutionContext context)
        {
            
            string folderName;

            
            if (!context.Items.TryGetValue("FolderName", out var folderNameObj) || folderNameObj is not string folderNameValue)
            {
                folderName = "Images"; 
            }
            else
            {
                folderName = folderNameValue;
            }


            
            var imageNameProperty = typeof(TSource).GetProperty("ImageName");

            
            string imageName;
            if (imageNameProperty != null)
            {
                var value = imageNameProperty.GetValue(source);
                imageName = value?.ToString() ?? string.Empty;
            }
            else
            {
                throw new ArgumentException($"Type {typeof(TSource).Name} does not contain an 'ImageName' property.");
            }
            var request = _httpContextAccessor.HttpContext?.Request;
            if (request == null)
                return string.Empty;
            var baseUrl = $"{request.Scheme}://{request.Host}";

            return $"{baseUrl}{_baseImageUrl.TrimEnd('/')}/{folderName}/{imageName}";
        }
    }
}
