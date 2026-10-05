using Adros.Application.DTOs.Student;
using Adros.Application.Mappings.ImageResolver;
using Adros.Core.Entities;
using Adros.Core.Entities.Users;
using Adros.Shared.Helpers;

namespace Adros.Application.Mappings
{
    public class StudentProfile : BaseProfile
    {
        public StudentProfile()
        {
            CreateMap<Student, StudentListDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.ApplicationUser.FirstName))
                .ForMember(dest => dest.lastName, opt => opt.MapFrom(src => src.ApplicationUser.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.ApplicationUser.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.ApplicationUser.PhoneNumber))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.ApplicationUser.IsActive))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.LoginTimes, opt => opt.MapFrom(src => src.LoginTimes))
                .ForMember(dest => dest.ViewsCount, opt => opt.MapFrom(src => src.VideoViews.Count))
                .ForMember(dest => dest.DownloadsCount, opt => opt.MapFrom(src => src.VideoDownloads.Count))
                .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level != null ? src.Level.Title : ""))
                .ForMember(dest => dest.Stage, opt => opt.MapFrom(src => src.Level != null ? src.Level.Stage.Title : ""));


            CreateMap<Student, StudentEntityDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.ApplicationUser.FirstName))
                .ForMember(dest => dest.lastName, opt => opt.MapFrom(src => src.ApplicationUser.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.ApplicationUser.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.ApplicationUser.PhoneNumber))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.ApplicationUser.IsActive))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.LoginTimes, opt => opt.MapFrom(src => src.LoginTimes))
                .ForMember(dest => dest.ViewsCount, opt => opt.MapFrom(src => src.VideoViews.Count))
                .ForMember(dest => dest.DownloadsCount, opt => opt.MapFrom(src => src.VideoDownloads.Count))
                .ForMember(
                dest => dest.ImagePath,
                opt => opt.MapFrom<StudentImageUrlResolver<StudentEntityDto>>())
                .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level != null ? src.Level.Title : ""))
                .ForMember(dest => dest.Stage, opt => opt.MapFrom(src => src.Level != null ? src.Level.Stage.Title : ""));


            CreateMap<Student, StudentProfileDto>()
            .ForMember(
                dest => dest.ImagePath,
                opt => opt.MapFrom<StudentImageUrlResolver<StudentProfileDto>>()
            )
            // Read from ApplicationUser
            .ForMember(dest => dest.Name, opt => opt.MapFrom(s => s.ApplicationUser.UserName))
            // Read from Level
            .ForMember(dest => dest.Level, opt => opt.MapFrom(s => s.Level.Title))
            // Read direct counts
            //.ForMember(dest => dest.DownloadsCount, opt => opt.MapFrom(s => s.VideoViews.Count))
            .ForMember(dest => dest.DownloadsCount, opt => opt.MapFrom(s => s.VideoDownloads.Count))
            // Temporarily map zero for times. We'll fill these in after mapping or in the service.
            .ForMember(dest => dest.TotalStudyTime, opt => opt.Ignore())
            .ForMember(dest => dest.DailyAchievements, opt => opt.Ignore());
        }
    }
}
