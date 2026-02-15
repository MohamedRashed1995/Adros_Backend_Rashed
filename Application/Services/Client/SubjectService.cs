using Adros.Application.Interfaces.IService;
using Adros.Application.Services.HomeService;
using Adros.Core.DomainServices.IDomainService;
using Adros.Core.Entities.Course;
using Adros.Core.Specifications;
using Adros.Shared.Helpers;
using Adros.Shared.Interfaces;
using Adros.Shared.Specifications;
using AutoMapper;
using Microsoft.Extensions.Logging;

<<<<<<< HEAD


namespace Adros.Application.Services.Client
{
    public class SubjectService(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService) : ISubjectService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        
=======
namespace Adros.Application.Services.Client
{
    public class SubjectService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<BannerService> logger, ICurrentUserService currentUserService) : ISubjectService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<BannerService> _logger = logger;
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        private readonly ICurrentUserService _currentUserService = currentUserService;




        public async Task<Pagination<SubjectDto>> GetSubjectsAsync(SubjectSpecParams subjectSpecParams)
        {
            var subjectRepo = _unitOfWork.Repository<Subject>();

            if (subjectRepo == null)
            {
<<<<<<< HEAD
                
=======
                _logger.LogError("Subject repository not found");
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
                throw new DirectoryNotFoundException("Subject repository not found");
            }

            // Apply filtering, searching, and pagination specifications
            var spec = new SubjectSpecifications(subjectSpecParams);
            var subjects = await subjectRepo.ListAsync(spec);

            // Map the retrieved subjects to DTOs
            var data = _mapper.Map<IReadOnlyList<Subject>, IReadOnlyList<SubjectDto>>(subjects);

            // Get the total count of filtered subjects
            var countSpec = new SubjectWithFilterForCountSpecification(subjectSpecParams);
            var count = await subjectRepo.GetCountWithSpecAsync(countSpec);

            // Return paginated response
            return new Pagination<SubjectDto>(subjectSpecParams.PageIndex, subjectSpecParams.PageSize, count, data);
        }

        public async Task<SubjectDto> GetSubjectByIdAsync(Guid id)
        {
            var subject = await _unitOfWork.Repository<Subject>().GetByIdAsync(id);
            if (subject == null || subject.Deleted == true) throw new KeyNotFoundException("Subject not found");

            return _mapper.Map<SubjectDto>(subject);
        }

        public async Task<SubjectDto> CreateSubjectAsync(SubjectInputDto dto)
        {
            var subject = _mapper.Map<Subject>(dto);

            subject.CreatedBy = _currentUserService.UserId; 
            subject.UpdatedBy = _currentUserService.UserId;

            var subjects = await _unitOfWork.Repository<Subject>().ListAllAsync();
<<<<<<< HEAD
            //if (subjects.Select(x=>x.Title).Contains(dto.Title))
            //    throw new Exception("there is subject with same name in database");
=======
            if (subjects.Select(x=>x.Title).Contains(dto.Title))
                throw new Exception("there is subject with same name in database");
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
            await _unitOfWork.Repository<Subject>().AddAsync(subject);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<SubjectDto>(subject);
        }

        public async Task UpdateSubjectAsync(Guid id, SubjectInputDto dto)
        {
            var subject = await _unitOfWork.Repository<Subject>().GetByIdAsync(id);
            if (subject == null) throw new KeyNotFoundException("Subject not found");
            var subjects = await _unitOfWork.Repository<Subject>().ListAllAsync();
            if (subjects.Select(x => x.Title).Contains(dto.Title))
                throw new Exception("there is subject with same name in database");
            _mapper.Map(dto, subject);
            await _unitOfWork.CompleteAsync();
        }
        public async Task<IReadOnlyList<SubjectDto>> GetSubjectsByLevelIdAsync(Guid levelId)
        {
            var specParams = new SubjectSpecParams
            {
                LevelId = levelId,
                PageIndex = 1,
                PageSize = int.MaxValue // علشان تجيب كل المواد بدون Pagination
            };

            var spec = new SubjectSpecifications(specParams);
            var subjects = await _unitOfWork.Repository<Subject>().ListAsync(spec);

            if (!subjects.Any())
                throw new KeyNotFoundException("No subjects found for this level.");

            return _mapper.Map<IReadOnlyList<SubjectDto>>(subjects);
        }


        public async Task DeleteSubjectAsync(Guid id)
        {
            var subject = await _unitOfWork.Repository<Subject>().GetByIdAsync(id);
            if (subject == null) throw new KeyNotFoundException("Subject not found");

            _unitOfWork.Repository<Subject>().Delete(subject);
            await _unitOfWork.CompleteAsync();
        }
    }

}
