using Adros.Application.DTOs.Video;
using Adros.Core.Entities.Course;
using Adros.Core.Enums;
using AutoMapper;

namespace Adros.Application.Mappings
{
    public class VideoProfile : Profile
    {
        public VideoProfile()
        {
<<<<<<< HEAD
            // ================= CreateVideoDto => Video =================
            CreateMap<CreateVideoDto, Video>()
    .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
    .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
    .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => VideoStatus.Processing))
    .ForMember(dest => dest.SourceType, opt => opt.MapFrom(src => src.Url != null ? VideoSourceType.Upload : VideoSourceType.ExternalLink))
    .ForMember(dest => dest.Lesson, opt => opt.Ignore())
    .ForMember(dest => dest.Unit, opt => opt.Ignore())
    .ForMember(dest => dest.Views, opt => opt.Ignore())
    .ForMember(dest => dest.Watchlater, opt => opt.Ignore());


            // ================= UpdateVideoDto => Video =================
            CreateMap<UpdateVideoDto, Video>()
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcMember) => srcMember != null));

            // ================= Video => VideoDto =================
            CreateMap<Video, VideoDto>()
                // Lesson & Unit
                .ForMember(dest => dest.LessonTitle,
                    opt => opt.MapFrom(src => src.Lesson != null ? src.Lesson.Title : null))
                .ForMember(dest => dest.UnitTitle,
                    opt => opt.MapFrom(src => src.Unit != null ? src.Unit.Title : null))

                // Teacher 🆕
                .ForMember(dest => dest.TeacherName,
                    opt => opt.MapFrom(src =>
                        src.Lesson != null && src.Lesson.Teacher != null
                            ? src.Lesson.Teacher.FirstName + " "+src.Lesson.Teacher.LastName
                            : null))
                .ForMember(dest => dest.TeacherAbout,
                    opt => opt.MapFrom(src =>
                        src.Lesson != null && src.Lesson.Teacher != null
                            ? src.Lesson.Teacher.About
                            : null))

                // Stats
                .ForMember(dest => dest.ViewsCount,
                    opt => opt.MapFrom(src => src.Views != null ? src.Views.Count : 0))
                .ForMember(dest => dest.WatchLaterCount,
                    opt => opt.MapFrom(src => src.Watchlater != null ? src.Watchlater.Count : 0))

                // Media
                //.ForMember(dest => dest.ThumbnailUrl,
                //    opt => opt.MapFrom(src => src.ThumbnailUrl))
                //.ForMember(dest => dest.Duration,
                //    opt => opt.MapFrom(src => src.Duration))
                .ForMember(dest => dest.Url,
                    opt => opt.MapFrom(src =>
                        //"http://adros-mrashed.runasp.net" +
                        src.Url));
=======
            CreateMap<CreateVideoDto, Video>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => VideoStatus.Processing))
                .ForMember(dest => dest.Lesson, opt => opt.Ignore())
                .ForMember(dest => dest.Topic, opt => opt.Ignore())
                .ForMember(dest => dest.Views, opt => opt.Ignore())
                .ForMember(dest => dest.Downloads, opt => opt.Ignore());

            CreateMap<UpdateVideoDto, Video>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Video, VideoDto>()
                .ForMember(dest => dest.LessonTitle, opt => opt.MapFrom(src => src.Lesson.Title))
                .ForMember(dest => dest.UnitTitle, opt => opt.MapFrom(src => src.Topic.Title))
                .ForMember(dest => dest.ViewsCount, opt => opt.MapFrom(src => src.Views.Count))
                .ForMember(dest => dest.DownloadsCount, opt => opt.MapFrom(src => src.Downloads.Count));
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        }
    }
}
