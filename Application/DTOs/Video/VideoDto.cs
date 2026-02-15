using Adros.Core.Enums;
<<<<<<< HEAD
using Microsoft.AspNetCore.Http;
=======
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
<<<<<<< HEAD
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;



=======
using System.Threading.Tasks;
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a

namespace Adros.Application.DTOs.Video
{
    public class VideoDto
    {

        public Guid Id { get; set; }
        public string Title { get; set; }
<<<<<<< HEAD
        //[JsonIgnore]
        public int? Duration { get; set; } 
        public string? Description { get; set; }      // 🆕
        //public string? ThumbnailUrl { get; set; }
        public string Url { get; set; }
        public string? TeacherName { get; set; }
        public string? TeacherAbout { get; set; }
        public int? Order { get; set; }
        public Guid LessonId { get; set; }
        public string? LessonTitle { get; set; }
        public Guid UnitId { get; set; }
        public string? UnitTitle { get; set; }
        public int ViewsCount { get; set; }
        public bool IsWatchLater { get; set; }
        public int WatchLaterCount { get; set; }
=======
        public TimeSpan Duration { get; set; } // Formatted as "HH:mm:ss"
        public string Url { get; set; }
        public int? Order { get; set; }
        public Guid LessonId { get; set; }
        public string LessonTitle { get; set; }
        public Guid UnitId { get; set; }
        public string UnitTitle { get; set; }
        public int ViewsCount { get; set; }
        public int DownloadsCount { get; set; }
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        public VideoStatus Status { get; set; }
        public DateTime? ProcessedAt { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public class CreateVideoDto
    {
        public string Title { get; set; }
<<<<<<< HEAD
        public string? Description { get; set; }
        public int? Duration { get; set; }
        public string Url { get; set; }
        public string? ThumbnailUrl { get; set; }
        public int? Order { get; set; }
        public Guid LessonId { get; set; }
        public Guid UnitId { get; set; }
        
    }

    public class CreateVideoRequest
    {
        public string Title { get; set; }
        //[JsonIgnore]
        //public string? Duration { get; set; } // "00:10:30"
        public string? Description { get; set; }

        public int? Order { get; set; }
        public Guid LessonId { get; set; }
        public Guid UnitId { get; set; }
        public string? VideoUrl { get; set; }
        //public IFormFile? VideoFile { get; set; }
        //public IFormFile? ImageFile { get; set; }
    }

    public class VideoViewRequest
    {
        public int WatchedSeconds { get; set; }
    }


    public class UpdateVideoDto
    {
        public string Title { get; set; }
        public int? Duration { get; set; }
        public string? Description { get; set; }      // 🆕
        public string? ThumbnailUrl { get; set; }
=======
        public TimeSpan Duration { get; set; }
        public string Url { get; set; }
        public int? Order { get; set; }
        public Guid LessonId { get; set; }
        public Guid UnitId { get; set; }
        //public string BunnyVideoId { get; set; }
    }

    public class UpdateVideoDto
    {
        public string Title { get; set; }
        public TimeSpan? Duration { get; set; }
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        public string Url { get; set; }
        public int? Order { get; set; }
        public VideoStatus? Status { get; set; }
    }

    public class VideoDownloadDto
    {
        public Guid Id { get; set; }
        public string DownloadUrl { get; set; }
        public DateTime DownloadedAt { get; set; }
        public string VideoTitle { get; set; }
    }

    public class VideoStatsDto
    {
        public Guid VideoId { get; set; }
        public string Title { get; set; }
        public int TotalViews { get; set; }
        public int TotalDownloads { get; set; }
        public TimeSpan TotalWatchTime { get; set; }
        public double AverageWatchPercentage { get; set; }
    }
<<<<<<< HEAD
    public class WatchLaterVideoDto
    {
        public Guid WatchLaterId { get; set; }
        public bool IsWatchLater { get; set; }
        public Guid VideoId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } 
    }

=======
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
}

