using Adros.Apis.ApiResponse;
using Adros.Application.DTOs.Skills;
using Adros.Application.Interfaces.IService;
using Adros.Application.Services.HomeService;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Adros.Apis.Controllers.Admin
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillsService _service;

        public SkillsController(ISkillsService service)
        {
            _service = service;
        }

        [HttpPost("CreateSkill")]
        public async Task<IActionResult> Create([FromForm] SkillDto dto)
            => Ok(await _service.CreateAsync(dto));

        [HttpGet("GetAllSkills")]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAllAsync(null, null));

        [HttpGet("{skillId:guid}")]
        public async Task<IActionResult> GetSkillById(Guid skillId)
        {
            var skill = await _service.GetByIdAsync(skillId);

            if (skill == null)
                return NotFound();

            return Ok(skill);
        }


        [HttpPut("Update/{Skillid}")]
        public async Task<IActionResult> Update(Guid Id ,SkillDto dto)
        {
            var skill = await _service.UpdateAsync(Id, dto);
            return skill == null ? NotFound() : Ok(skill);
        }

        [HttpDelete("Delete/{Skillid}")]
        public async Task<IActionResult> Delete(Guid Skillid)
            => Ok(await _service.DeleteAsync(Skillid));
    }

}
