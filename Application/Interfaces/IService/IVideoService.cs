using Adros.Application.DTOs.Pagination;
using Adros.Application.DTOs.Video;
//using Adros.Application.DTOs.Video;
using Adros.Core.Entities.Course;
using Adros.Core.Enums;
//using Adros.Core.Specifications.QueryParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adros.Application.Interfaces.IService
{
    public interface IVideoService
    {
        Task<VideoDto> GetVideoByIdAsync(Guid id);
        Task<IEnumerable<VideoDto>> GetAllVideosAsync();
        Task<IEnumerable<VideoDto>> GetVideosByLessonIdAsync(Guid lessonId);
        Task RecordVideoViewAsync(Guid videoId, Guid studentId,int watchedseconds);
        Task<int> GetVideoViewsCountAsync(Guid videoId);
        Task<VideoDto> CreateVideoAsync(CreateVideoDto dto ,VideoSourceType videoSourceType);
        Task<VideoDto> UpdateVideoAsync(Guid id, UpdateVideoDto dto);
        Task<List<WatchLaterVideoDto>> GetWatchLaterVideosAsync(Guid Student);
        Task<bool> DeleteVideoAsync(Guid id);
        Task<bool> ChangeVideoStatusAsync(Guid id, VideoStatus status);
        Task<bool> ToggleWatchLaterAsync(Guid videoId, Guid studentId);
        Task<int> GetVideoViewsCountForCurrentStudentAsync(Guid videoId);
    }
}