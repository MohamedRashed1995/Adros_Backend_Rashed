using Adros.Application.DTOs.Banner;
using Adros.Application.DTOs.Lesson;
using Adros.Application.DTOs.Stage;
using Adros.Core.Entities.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adros.Application.Interfaces.IService
{
    public interface ILessonsService
    {
        Task<List<LessonDto>> GetAllLessonsAsync();
        Task<LessonDto> CreateLessonAsync(LessonCreateDto lessonCreateDto);
        Task<IReadOnlyList<LessonDto>> GetLessonsBySubjectIdAsync(Guid subjectId);
        Task<LessonDto?> GetLessonByIdAsync(Guid lessonId);
        //Task<IReadOnlyList<LessonDto>> GetLessonsByTopicIdAsync(Guid topicId);
        Task<UnitWithLessonsDto> GetLessonsByUnitIdAsync(Guid topicId);
        Task<LessonDto?> UpdateLessonAsync(Guid lessonId, LessonUpdateDto lessonUpdateDto);
        Task<bool> DeleteLessonAsync(Guid lessonId);
    }

}