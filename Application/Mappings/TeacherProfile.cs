using Adros.Application.DTOs.Teacher;
using Adros.Core.Entities.Users;
using Adros.Core.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adros.Application.Mappings
{
    public class TeacherProfile : Profile
    {
        public TeacherProfile()
        {
            // Mapping for internal operations
            CreateMap<Teacher, TeacherEntityDto>()
                .ForMember(dest => dest.TeacherId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.ApplicationUser.Email))
                .ForMember(dest => dest.About, opt => opt.MapFrom(src => src.About))
                .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => src.ApplicationUser.Photo))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.ApplicationUser.IsActive))
                .ForMember(dest => dest.LessonCount, opt => opt.MapFrom(src => src.Lessons.Count));

            // Mapping for client-facing operations
            CreateMap<Teacher, ClientTeacherDto>()
                .ForMember(dest => dest.TeacherId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.ApplicationUser.Email))
                .ForMember(dest => dest.About, opt => opt.MapFrom(src => src.About))
                .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => src.ApplicationUser.Photo))
                .ForMember(dest => dest.LessonCount, opt => opt.MapFrom(src => src.Lessons.Count));

            // Mapping for creating ApplicationUser from TeacherCreateDto
            CreateMap<TeacherCreateDto, ApplicationUser>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Photo, opt => opt.Ignore()); // Handled externally

            // Mapping for updating ApplicationUser from TeacherUpdateDto
            CreateMap<TeacherUpdateDto, ApplicationUser>()
                .ForMember(dest => dest.Email, opt => opt.Condition(src => !string.IsNullOrEmpty(src.Email)))
                .ForMember(dest => dest.UserName, opt => opt.Condition(src => !string.IsNullOrEmpty(src.Email)))
                .ForMember(dest => dest.Photo, opt => opt.Ignore()); // Handled externally
        }
    }
}
