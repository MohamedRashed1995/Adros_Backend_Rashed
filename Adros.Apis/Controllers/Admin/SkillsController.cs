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
<<<<<<< HEAD
        public async Task<IActionResult> Create([FromForm] SkillDto dto)
=======
        public async Task<IActionResult> Create(SkillDto dto)
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
            => Ok(await _service.CreateAsync(dto));

        [HttpGet("GetAllSkills")]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAllAsync(null, null));

<<<<<<< HEAD
        [HttpGet("{skillId:guid}")]
=======
        [HttpGet("{SkillId:guid}")]
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        public async Task<IActionResult> GetSkillById(Guid skillId)
        {
            var skill = await _service.GetByIdAsync(skillId);

            if (skill == null)
                return NotFound();

            return Ok(skill);
        }


<<<<<<< HEAD
        [HttpPut("Update/{Id:guid}")]
        public async Task<IActionResult> Update(Guid Id ,SkillDto dto)
        {
            var skill = await _service.UpdateAsync(Id, dto);
=======
        [HttpPut("Update/{Skillid}")]
        public async Task<IActionResult> Update(SkillDto dto)
        {
            var skill = await _service.UpdateAsync(dto);
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
            return skill == null ? NotFound() : Ok(skill);
        }

        [HttpDelete("Delete/{Skillid}")]
        public async Task<IActionResult> Delete(Guid Skillid)
            => Ok(await _service.DeleteAsync(Skillid));
    }

}
