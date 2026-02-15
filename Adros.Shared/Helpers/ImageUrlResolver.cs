using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

public class ImageUrlResolver<TSource> : IValueResolver<TSource, object, string>
    where TSource : class
{
    private readonly string _baseImageUrl;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ImageUrlResolver(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _baseImageUrl = configuration["ImageSettings:BaseImageUrl"] ?? "/Images";
        _httpContextAccessor = httpContextAccessor;
    }

    public string Resolve(TSource source, object destination, string destMember, ResolutionContext context)
    {
        // 1️⃣ FolderName (safe)
        var folderName = context.Items.TryGetValue("FolderName", out var folderObj) && folderObj is string f
            ? f
            : "Images";

        // 2️⃣ ImageName (SAFE – no exception)
        var imageNameProp = source.GetType().GetProperty("ImageName");
        if (imageNameProp == null)
            return string.Empty;

        var imageName = imageNameProp.GetValue(source)?.ToString();
        if (string.IsNullOrWhiteSpace(imageName))
            return string.Empty;

        // 3️⃣ Base URL (SAFE)
        var request = _httpContextAccessor.HttpContext?.Request;
        var baseUrl = request != null
            ? $"{request.Scheme}://{request.Host}"
            : string.Empty;

        return $"{baseUrl}{_baseImageUrl.TrimEnd('/')}/{folderName}/{imageName}";
    }
}
