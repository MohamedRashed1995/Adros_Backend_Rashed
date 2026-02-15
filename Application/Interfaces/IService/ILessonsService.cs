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
<<<<<<< HEAD
        Task<List<LessonDto>> GetAllLessonsAsync();
=======
        Task<IReadOnlyList<LessonDto>> GetAllLessonsAsync();
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        Task<LessonDto> CreateLessonAsync(LessonCreateDto lessonCreateDto);
        Task<IReadOnlyList<LessonDto>> GetLessonsBySubjectIdAsync(Guid subjectId);
        Task<LessonDto?> GetLessonByIdAsync(Guid lessonId);
        //Task<IReadOnlyList<LessonDto>> GetLessonsByTopicIdAsync(Guid topicId);
<<<<<<< HEAD
        Task<UnitWithLessonsDto> GetLessonsByUnitIdAsync(Guid topicId);
=======
        Task<List<LessonDto>> GetLessonsByTopicIdAsync(Guid topicId);
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        Task<LessonDto?> UpdateLessonAsync(Guid lessonId, LessonUpdateDto lessonUpdateDto);
        Task<bool> DeleteLessonAsync(Guid lessonId);
    }

}