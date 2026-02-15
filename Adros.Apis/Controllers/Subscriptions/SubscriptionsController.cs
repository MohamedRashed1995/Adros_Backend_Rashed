<<<<<<< HEAD
﻿using Adros.Application.DTOs.Subscription;
using Adros.Application.Interfaces.IService;
using Adros.Core.DomainServices.IDomainService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
=======
﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Adros.Application.Interfaces.IService;
using Adros.Application.DTOs.Subscription;
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a

namespace Adros.Apis.Controllers.Subscriptions
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionsController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;
<<<<<<< HEAD
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
=======
        private readonly ILogger<SubscriptionsController> _logger;

        public SubscriptionsController(
            ISubscriptionService subscriptionService,
            ILogger<SubscriptionsController> logger)
        {
            _subscriptionService = subscriptionService;
            _logger = logger;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSubscriptionDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { Success = false, Message = "Invalid data", Errors = ModelState });

                // Optionally, verify the user is creating subscription for themselves
                var currentUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                if (dto.UserId != currentUserId && !User.IsInRole("Admin"))
                    return Unauthorized(new { Success = false, Message = "Unauthorized" });

                var subscription = await _subscriptionService.CreateAsync(dto);

                return Ok(new
                {
                    Success = true,
                    Message = "Subscription created successfully",
                    Data = subscription
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating subscription");
                return BadRequest(new { Success = false, Message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var subscription = await _subscriptionService.GetByIdAsync(id);
                if (subscription == null)
                    return NotFound(new { Success = false, Message = "Subscription not found" });

                return Ok(new { Success = true, Data = subscription });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting subscription {Id}", id);
                return StatusCode(500, new { Success = false, Message = "Internal server error" });
            }
        }

        [Authorize]
        [HttpGet("my-subscriptions")]
        public async Task<IActionResult> GetMySubscriptions()
        {
            try
            {
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var subscriptions = await _subscriptionService.GetByUserAsync(userId);

                return Ok(new { Success = true, Data = subscriptions });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting subscriptions for user");
                return StatusCode(500, new { Success = false, Message = "Internal server error" });
            }
        }

        [Authorize]
        [HttpGet("check-status")]
        public async Task<IActionResult> CheckMySubscriptionStatus()
        {
            try
            {
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var hasActiveSubscription = await _subscriptionService.CheckUserSubscriptionStatusAsync(userId);

                return Ok(new
                {
                    Success = true,
                    Data = new { HasActiveSubscription = hasActiveSubscription }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking subscription status");
                return StatusCode(500, new { Success = false, Message = "Internal server error" });
            }
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var subscriptions = await _subscriptionService.GetAllAsync();
                return Ok(new { Success = true, Data = subscriptions });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all subscriptions");
                return StatusCode(500, new { Success = false, Message = "Internal server error" });
            }
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet("active")]
        public async Task<IActionResult> GetActiveSubscriptions()
        {
            try
            {
                var subscriptions = await _subscriptionService.GetActiveSubscriptionsAsync();
                return Ok(new { Success = true, Data = subscriptions });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting active subscriptions");
                return StatusCode(500, new { Success = false, Message = "Internal server error" });
            }
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet("expired")]
        public async Task<IActionResult> GetExpiredSubscriptions()
        {
            try
            {
                var subscriptions = await _subscriptionService.GetExpiredSubscriptionsAsync();
                return Ok(new { Success = true, Data = subscriptions });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting expired subscriptions");
                return StatusCode(500, new { Success = false, Message = "Internal server error" });
            }
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet("stats")]
        public async Task<IActionResult> GetStats()
        {
            try
            {
                var stats = await _subscriptionService.GetSubscriptionStatsAsync();
                return Ok(new { Success = true, Data = stats });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting subscription stats");
                return StatusCode(500, new { Success = false, Message = "Internal server error" });
            }
        }

        [Authorize]
        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            try
            {
                var cancelled = await _subscriptionService.CancelSubscriptionAsync(id);
                if (!cancelled)
                    return NotFound(new { Success = false, Message = "Subscription not found" });

                return Ok(new { Success = true, Message = "Subscription cancelled successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error cancelling subscription {Id}", id);
                return StatusCode(500, new { Success = false, Message = "Internal server error" });
            }
        }
    }
}
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
