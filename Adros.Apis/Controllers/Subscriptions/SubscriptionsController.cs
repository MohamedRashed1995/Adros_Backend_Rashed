using Adros.Application.DTOs.Subscription;
using Adros.Application.Interfaces.IService;
using Adros.Core.DomainServices.IDomainService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Adros.Apis.Controllers.Subscriptions
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionsController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IStudentService _studentService;
        public SubscriptionsController(ISubscriptionService subscriptionService, IStudentService studentService,ICurrentUserService currentUserService)
        {
            _subscriptionService = subscriptionService;
            _studentService = studentService;
            _currentUserService = currentUserService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSubscriptionDto dto)
        {
            var result = await _subscriptionService.CreateAsync(dto);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _subscriptionService.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _subscriptionService.GetAllAsync(_currentUserService, _studentService);
            return Ok(result);
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSubscriptionDto dto)
        {
            var result = await _subscriptionService.UpdateAsync(id, dto);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _subscriptionService.DeleteAsync(id);
            if (!success) return NotFound();
            return Ok(new { Success = true });
        }
    }
}
