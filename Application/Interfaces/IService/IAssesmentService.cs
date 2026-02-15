using Adros.Application.DTOs.Assesment;
using Adros.Core.Entities.Assessements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adros.Application.Interfaces.IService
{
    public interface IAssesmentService
    {
        //Task<IEnumerable<AssesmentDto>> GetAllAsync();
        //Task<AssesmentDto> GetByIdAsync(Guid id);
        public Task<Assessment> CreateAssesmentAsync(AssessmentCreateDto dto);
        //Task<bool> UpdateAsync(Guid id, AssessmentUpdateDto dto);
        //Task<bool> DeleteAsync(Guid id);
    }
}
