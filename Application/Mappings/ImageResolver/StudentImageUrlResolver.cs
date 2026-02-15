using Adros.Application.DTOs.Student;
using Adros.Core.Entities.Users;
using Adros.Shared.Constants;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Adros.Application.Mappings.ImageResolver
{
    /// <summary>
    /// Resolves the student's image URL from the Photo field
    /// </summary>
    /// <remarks>
    /// Generates a full URL using the incoming HTTP context request and config settings
    /// </remarks>
    public class StudentImageUrlResolver<TDestination>(
        IConfiguration configuration,
        IHttpContextAccessor httpContextAccessor) : IValueResolver<Student, TDestination, string>
    where TDestination : class
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public string Resolve(
            Student source,
            TDestination destination,
            string destMember,
            ResolutionContext context)
        {
            var photo = source.ApplicationUser?.Photo;
            if (string.IsNullOrWhiteSpace(photo))
            {
                return string.Empty;
            }

            var baseImageUrl = _configuration["ImageSettings:BaseImageUrl"] ?? "/Images";

            var folderName = FoldersNames.Media.StudentsFolder;
            if (context.Items.TryGetValue("FolderName", out var folderNameObj) &&
                folderNameObj is string folderNameValue &&
                !string.IsNullOrWhiteSpace(folderNameValue))
            {
                folderName = folderNameValue;
            }

            var request = _httpContextAccessor.HttpContext?.Request;
            if (request == null)
            {
                return string.Empty;
            }

            var baseUrl = $"{request.Scheme}://{request.Host}";
            return $"{baseUrl}{baseImageUrl.TrimEnd('/')}/{folderName}/{photo}";
        }
    }
}
