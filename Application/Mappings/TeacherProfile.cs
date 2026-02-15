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
            CreateMap<Teacher, TeacherEntityDto>()
                .ForMember(d => d.TeacherID,
                    o => o.MapFrom(s => s.Id))

                .ForMember(d => d.Email,
                    o => o.MapFrom(s =>
                        s.ApplicationUser != null
                            ? s.ApplicationUser.Email
                            : s.Email))

                .ForMember(d => d.FirstName,
                    o => o.MapFrom(s => s.FirstName))

                .ForMember(d => d.LastName,
                    o => o.MapFrom(s => s.LastName))

                .ForMember(d => d.About,
                    o => o.MapFrom(s => s.About))

                .ForMember(d => d.ApplicationUserId,
                    o => o.MapFrom(s => s.ApplicationUserId))

                // ✅ IsActive جاية من ApplicationUser
                .ForMember(d => d.IsActive,
                    o => o.MapFrom(s =>
                        s.IsActive != null && s.IsActive))

                // ✅ LessonCount محسوبة
                .ForMember(d => d.LessonCount,
                    o => o.MapFrom(s =>
                        s.Lessons != null ? s.Lessons.Count : 0))

                .ForMember(d => d.ProfilePictureUrl,
                        o => o.MapFrom(s =>
                            s.ApplicationUser != null
                                ? s.ApplicationUser.Photo
                                : s.ProfilePictureUrl))
            
                .ForMember(d => d.StageId,
                    o => o.MapFrom(s => s.StageId))

                .ForMember(d => d.PhoneNumber, q => 
                q.MapFrom(s => s.phoneNumber));
        }
    }

}
