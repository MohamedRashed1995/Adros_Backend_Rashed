using Adros.Shared.Settings;

namespace Adros.Application.Interfaces.IService
{
    public interface IBunnyNetService
    {
        Task<VideoUploadResponse> CreateVideo(string title);
        Task<bool> DeleteVideo(string bunnyVideoId);
        Task<string> GetVideoUrl(string bunnyVideoId);
        Task<string> GetDirectUploadUrl(string bunnyVideoId);
    }
}
