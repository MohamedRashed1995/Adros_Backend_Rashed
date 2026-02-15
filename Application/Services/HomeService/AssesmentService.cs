using Adros.Application.DTOs.Assesment;
using Adros.Application.DTOs.Banner;
using Adros.Application.DTOs.Level;
using Adros.Application.Interfaces.IService;
using Adros.Application.Services.UsersServices;
using Adros.Core.DomainServices.IDomainService;
using Adros.Core.Entities.Assessements;
using Adros.Core.Entities.Course;
using Adros.Core.Entities.Home;
using Adros.Shared.Constants;
using Adros.Shared.Helpers;
using Adros.Shared.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Adros.Application.Services.HomeService
{
    public class AssesmentService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<AssesmentService> logger, ICurrentUserService currentUserService) : IAssesmentService
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<AssesmentService> _logger = logger;
        private readonly ICurrentUserService _currentUserService = currentUserService;


       
        public async Task<Assessment> CreateAssesmentAsync(AssessmentCreateDto assessmentCreateDto)
        {



            try
            {

                _logger.LogInformation("🚀 بدء إنشاء تقييم...");
                _logger.LogInformation("📦 البيانات: TopicId={TopicId}, Title={Title}, Score={Score}",
                    assessmentCreateDto.TopicId, assessmentCreateDto.Title, assessmentCreateDto.Score);

                var assessment = new Assessment
                {
                    Id = new Guid(),
                    UnitId = assessmentCreateDto.TopicId,
                    Title = assessmentCreateDto.Title,
                    Score = assessmentCreateDto.Score,
                    //CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    CreatedBy = _currentUserService.UserId,
                    UpdatedBy = _currentUserService.UserId,
                    Deleted = false

                };
                _logger.LogInformation("👷 تم إنشاء الـ Entity: {@Assessment}", assessment);
                await _unitOfWork.Repository<Assessment>().AddAsync(assessment);
                await _unitOfWork.CompleteAsync();
                _logger.LogInformation("🎉 النجاح! تم الحفظ - ID: {Id}", assessment.Id);
                return assessment;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "💥 خطأ في إنشاء التقييم");
                throw;
            }
        }
    }
}

    