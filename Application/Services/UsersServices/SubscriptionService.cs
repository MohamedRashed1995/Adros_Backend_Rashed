using Adros.Application.DTOs.Subscription;
using Adros.Application.Interfaces.IService;
using Adros.Core.Entities.Subscription;
using Adros.Core.Entities.Users;
using Adros.Shared.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adros.Application.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SubscriptionService> _logger;

        public SubscriptionService(
            IUnitOfWork unitOfWork,
            ILogger<SubscriptionService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<SubscriptionResponseDto> CreateAsync(CreateSubscriptionDto dto)
        {
            try
            {
                _logger.LogInformation("Creating subscription for user {UserId}", dto.UserId);

                // 1️⃣ Get student from userId - بالطريقة اللي كانت شغالة عندك
                var student = (await _unitOfWork.Repository<Student>()
                              .ListAllAsync())
                              .FirstOrDefault(s => s.ApplicationUserId == dto.UserId);

                if (student == null)
                    throw new Exception($"Student not found for user ID {dto.UserId}");

                // 2️⃣ Get subscription plan
                var subscriptionPlan = await _unitOfWork.Repository<SubscriptionPlan>()
                    .GetByIdAsync(dto.SubscriptionPlanId);

                if (subscriptionPlan == null)
                    throw new Exception($"Subscription plan with ID {dto.SubscriptionPlanId} not found");

                if (!subscriptionPlan.IsActive)
                    throw new Exception("Subscription plan is not active");

                // 3️⃣ Check for existing active subscription
                var allSubscriptions = await _unitOfWork.Repository<Subscription>()
                    .ListAllAsync();

                var existingActiveSubscription = allSubscriptions
                    .FirstOrDefault(s => s.StudentId == student.Id &&
                                        s.Status == "active" &&
                                        s.EndDate > DateTime.UtcNow);

                if (existingActiveSubscription != null)
                    throw new Exception("Student already has an active subscription");

                // 4️⃣ Check if payment transaction already exists
                var existingTransaction = allSubscriptions
                    .FirstOrDefault(s => s.PaymentTransactionId == dto.PaymentTransactionId);

                if (existingTransaction != null)
                    throw new Exception("Payment transaction ID already used");

                // 5️⃣ Create subscription
                var subscription = new Subscription
                {
                    StudentId = student.Id,
                    SubscriptionPlanId = dto.SubscriptionPlanId,
                    PaymentTransactionId = dto.PaymentTransactionId,
                    PaymentMethod = dto.PaymentMethod,
                    AmountPaid = dto.AmountPaid ?? subscriptionPlan.Price,
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(subscriptionPlan.DurationInDays),
                    Status = "active",
                    IsAutoRenew = dto.IsAutoRenew,
                    Notes = dto.Notes,
                    CreatedAt = DateTime.UtcNow
                };

                // 6️⃣ Save subscription
                await _unitOfWork.Repository<Subscription>().AddAsync(subscription);

                // 7️⃣ Update student subscription status
                student.SubscriptionStatus = "active";
                _unitOfWork.Repository<Student>().Update(student);

                await _unitOfWork.CompleteAsync();

                _logger.LogInformation("Subscription created successfully with ID {SubscriptionId}", subscription.Id);

                // 8️⃣ Prepare response
                return new SubscriptionResponseDto
                {
                    Id = subscription.Id,
                    UserId = dto.UserId,
                    StudentId = student.Id,
                    //StudentName = $"{student.FirstName} {student.LastName}",
                    StudentEmail = student.Email,
                    SubscriptionPlanId = subscription.SubscriptionPlanId,
                    SubscriptionPlanName = subscriptionPlan.Name,
                    PaymentTransactionId = subscription.PaymentTransactionId,
                    PaymentMethod = subscription.PaymentMethod,
                    StartDate = subscription.StartDate,
                    EndDate = subscription.EndDate,
                    Status = subscription.Status,
                    Amount = subscription.AmountPaid,
                    IsAutoRenew = subscription.IsAutoRenew,
                    Notes = subscription.Notes,
                    CreatedAt = subscription.CreatedAt
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating subscription for user {UserId}", dto.UserId);
                throw;
            }
        }

        public async Task<SubscriptionResponseDto> GetByIdAsync(Guid id)
        {
            try
            {
                var subscription = await _unitOfWork.Repository<Subscription>()
                    .GetByIdAsync(id);

                if (subscription == null)
                    return null;

                // Get student details
                var student = await _unitOfWork.Repository<Student>()
                    .GetByIdAsync(subscription.StudentId);

                // Get subscription plan details
                var subscriptionPlan = await _unitOfWork.Repository<SubscriptionPlan>()
                    .GetByIdAsync(subscription.SubscriptionPlanId);

                return new SubscriptionResponseDto
                {
                    Id = subscription.Id,
                    UserId = student?.ApplicationUserId ?? Guid.Empty,
                    StudentId = subscription.StudentId,
                    //StudentName = student != null ? $"{student.FirstName} {student.LastName}" : string.Empty,
                    StudentEmail = student?.Email ?? string.Empty,
                    SubscriptionPlanId = subscription.SubscriptionPlanId,
                    SubscriptionPlanName = subscriptionPlan?.Name ?? string.Empty,
                    PaymentTransactionId = subscription.PaymentTransactionId,
                    PaymentMethod = subscription.PaymentMethod,
                    StartDate = subscription.StartDate,
                    EndDate = subscription.EndDate,
                    Status = subscription.Status,
                    Amount = subscription.AmountPaid,
                    IsAutoRenew = subscription.IsAutoRenew,
                    Notes = subscription.Notes,
                    CreatedAt = subscription.CreatedAt,
                    UpdatedAt = subscription.UpdatedAt
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting subscription by ID {Id}", id);
                throw;
            }
        }

        public async Task<IEnumerable<SubscriptionResponseDto>> GetByUserAsync(Guid userId)
        {
            try
            {
                // Get student from userId
                var student = (await _unitOfWork.Repository<Student>()
                              .ListAllAsync())
                              .FirstOrDefault(s => s.ApplicationUserId == userId);

                if (student == null)
                    return Enumerable.Empty<SubscriptionResponseDto>();

                // Get all subscriptions for this student
                var allSubscriptions = await _unitOfWork.Repository<Subscription>()
                    .ListAllAsync();

                var subscriptions = allSubscriptions
                    .Where(s => s.StudentId == student.Id)
                    .OrderByDescending(s => s.CreatedAt)
                    .ToList();

                var result = new List<SubscriptionResponseDto>();

                foreach (var subscription in subscriptions)
                {
                    // Get subscription plan details
                    var subscriptionPlan = await _unitOfWork.Repository<SubscriptionPlan>()
                        .GetByIdAsync(subscription.SubscriptionPlanId);

                    result.Add(new SubscriptionResponseDto
                    {
                        Id = subscription.Id,
                        UserId = userId,
                        StudentId = student.Id,
                        //StudentName = $"{student.FirstName} {student.LastName}",
                        StudentEmail = student.Email,
                        SubscriptionPlanId = subscription.SubscriptionPlanId,
                        SubscriptionPlanName = subscriptionPlan?.Name ?? string.Empty,
                        PaymentTransactionId = subscription.PaymentTransactionId,
                        PaymentMethod = subscription.PaymentMethod,
                        StartDate = subscription.StartDate,
                        EndDate = subscription.EndDate,
                        Status = subscription.Status,
                        Amount = subscription.AmountPaid,
                        IsAutoRenew = subscription.IsAutoRenew,
                        Notes = subscription.Notes,
                        CreatedAt = subscription.CreatedAt,
                        UpdatedAt = subscription.UpdatedAt
                    });
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting subscriptions for user {UserId}", userId);
                throw;
            }
        }

        public async Task<IEnumerable<SubscriptionResponseDto>> GetAllAsync()
        {
            try
            {
                var allSubscriptions = await _unitOfWork.Repository<Subscription>()
                    .ListAllAsync();

                var allStudents = await _unitOfWork.Repository<Student>()
                    .ListAllAsync();

                var allPlans = await _unitOfWork.Repository<SubscriptionPlan>()
                    .ListAllAsync();

                var result = new List<SubscriptionResponseDto>();

                foreach (var subscription in allSubscriptions.OrderByDescending(s => s.CreatedAt))
                {
                    var student = allStudents.FirstOrDefault(s => s.Id == subscription.StudentId);
                    var plan = allPlans.FirstOrDefault(p => p.Id == subscription.SubscriptionPlanId);

                    result.Add(new SubscriptionResponseDto
                    {
                        Id = subscription.Id,
                        UserId = student?.ApplicationUserId ?? Guid.Empty,
                        StudentId = subscription.StudentId,
                        //StudentName = student != null ? $"{student.FirstName} {student.LastName}" : string.Empty,
                        StudentEmail = student?.Email ?? string.Empty,
                        SubscriptionPlanId = subscription.SubscriptionPlanId,
                        SubscriptionPlanName = plan?.Name ?? string.Empty,
                        PaymentTransactionId = subscription.PaymentTransactionId,
                        PaymentMethod = subscription.PaymentMethod,
                        StartDate = subscription.StartDate,
                        EndDate = subscription.EndDate,
                        Status = subscription.Status,
                        Amount = subscription.AmountPaid,
                        IsAutoRenew = subscription.IsAutoRenew,
                        Notes = subscription.Notes,
                        CreatedAt = subscription.CreatedAt,
                        UpdatedAt = subscription.UpdatedAt
                    });
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all subscriptions");
                throw;
            }
        }

        public async Task<SubscriptionResponseDto> UpdateAsync(Guid id, UpdateSubscriptionDto dto)
        {
            try
            {
                var subscription = await _unitOfWork.Repository<Subscription>()
                    .GetByIdAsync(id);

                if (subscription == null)
                    return null;

                // Update properties
                if (!string.IsNullOrEmpty(dto.Status))
                    subscription.Status = dto.Status;

                if (dto.IsAutoRenew.HasValue)
                    subscription.IsAutoRenew = dto.IsAutoRenew.Value;

                if (!string.IsNullOrEmpty(dto.Notes))
                    subscription.Notes = dto.Notes;

                if (dto.EndDate.HasValue)
                    subscription.EndDate = dto.EndDate.Value;

                subscription.UpdatedAt = DateTime.UtcNow;

                _unitOfWork.Repository<Subscription>().Update(subscription);
                await _unitOfWork.CompleteAsync();

                // Get updated details for response
                var student = await _unitOfWork.Repository<Student>()
                    .GetByIdAsync(subscription.StudentId);

                var subscriptionPlan = await _unitOfWork.Repository<SubscriptionPlan>()
                    .GetByIdAsync(subscription.SubscriptionPlanId);

                return new SubscriptionResponseDto
                {
                    Id = subscription.Id,
                    UserId = student?.ApplicationUserId ?? Guid.Empty,
                    StudentId = subscription.StudentId,
                    //StudentName = student != null ? $"{student.FirstName} {student.LastName}" : string.Empty,
                    StudentEmail = student?.Email ?? string.Empty,
                    SubscriptionPlanId = subscription.SubscriptionPlanId,
                    SubscriptionPlanName = subscriptionPlan?.Name ?? string.Empty,
                    PaymentTransactionId = subscription.PaymentTransactionId,
                    PaymentMethod = subscription.PaymentMethod,
                    StartDate = subscription.StartDate,
                    EndDate = subscription.EndDate,
                    Status = subscription.Status,
                    Amount = subscription.AmountPaid,
                    IsAutoRenew = subscription.IsAutoRenew,
                    Notes = subscription.Notes,
                    CreatedAt = subscription.CreatedAt,
                    UpdatedAt = subscription.UpdatedAt
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating subscription {Id}", id);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var subscription = await _unitOfWork.Repository<Subscription>()
                    .GetByIdAsync(id);

                if (subscription == null)
                    return false;

                _unitOfWork.Repository<Subscription>().Delete(subscription);
                await _unitOfWork.CompleteAsync();

                _logger.LogInformation("Subscription {Id} deleted successfully", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting subscription {Id}", id);
                throw;
            }
        }

        public async Task<bool> CancelSubscriptionAsync(Guid subscriptionId)
        {
            try
            {
                var subscription = await _unitOfWork.Repository<Subscription>()
                    .GetByIdAsync(subscriptionId);

                if (subscription == null)
                    return false;

                if (subscription.Status == "cancelled")
                    return true;

                subscription.Status = "cancelled";
                subscription.IsAutoRenew = false;
                subscription.UpdatedAt = DateTime.UtcNow;

                _unitOfWork.Repository<Subscription>().Update(subscription);
                await _unitOfWork.CompleteAsync();

                // Update student status
                var student = await _unitOfWork.Repository<Student>()
                    .GetByIdAsync(subscription.StudentId);

                if (student != null)
                {
                    student.SubscriptionStatus = "inactive";
                    _unitOfWork.Repository<Student>().Update(student);
                    await _unitOfWork.CompleteAsync();
                }

                _logger.LogInformation("Subscription {Id} cancelled successfully", subscriptionId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error cancelling subscription {Id}", subscriptionId);
                throw;
            }
        }

        public async Task<bool> CheckUserSubscriptionStatusAsync(Guid userId)
        {
            try
            {
                // Get student from userId
                var student = (await _unitOfWork.Repository<Student>()
                              .ListAllAsync())
                              .FirstOrDefault(s => s.ApplicationUserId == userId);

                if (student == null)
                    return false;

                var allSubscriptions = await _unitOfWork.Repository<Subscription>()
                    .ListAllAsync();

                var activeSubscription = allSubscriptions
                    .FirstOrDefault(s => s.StudentId == student.Id &&
                                        s.Status == "active" &&
                                        s.EndDate > DateTime.UtcNow);

                return activeSubscription != null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking subscription status for user {UserId}", userId);
                throw;
            }
        }

        public async Task<IEnumerable<SubscriptionResponseDto>> GetActiveSubscriptionsAsync()
        {
            try
            {
                var allSubscriptions = await _unitOfWork.Repository<Subscription>()
                    .ListAllAsync();

                var allStudents = await _unitOfWork.Repository<Student>()
                    .ListAllAsync();

                var allPlans = await _unitOfWork.Repository<SubscriptionPlan>()
                    .ListAllAsync();

                var activeSubscriptions = allSubscriptions
                    .Where(s => s.Status == "active" && s.EndDate > DateTime.UtcNow)
                    .OrderByDescending(s => s.CreatedAt)
                    .ToList();

                var result = new List<SubscriptionResponseDto>();

                foreach (var subscription in activeSubscriptions)
                {
                    var student = allStudents.FirstOrDefault(s => s.Id == subscription.StudentId);
                    var plan = allPlans.FirstOrDefault(p => p.Id == subscription.SubscriptionPlanId);

                    result.Add(new SubscriptionResponseDto
                    {
                        Id = subscription.Id,
                        UserId = student?.ApplicationUserId ?? Guid.Empty,
                        StudentId = subscription.StudentId,
                        //StudentName = student != null ? $"{student.FirstName} {student.LastName}" : string.Empty,
                        StudentEmail = student?.Email ?? string.Empty,
                        SubscriptionPlanId = subscription.SubscriptionPlanId,
                        SubscriptionPlanName = plan?.Name ?? string.Empty,
                        PaymentTransactionId = subscription.PaymentTransactionId,
                        PaymentMethod = subscription.PaymentMethod,
                        StartDate = subscription.StartDate,
                        EndDate = subscription.EndDate,
                        Status = subscription.Status,
                        Amount = subscription.AmountPaid,
                        IsAutoRenew = subscription.IsAutoRenew,
                        Notes = subscription.Notes,
                        CreatedAt = subscription.CreatedAt,
                        UpdatedAt = subscription.UpdatedAt
                    });
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting active subscriptions");
                throw;
            }
        }

        public async Task<IEnumerable<SubscriptionResponseDto>> GetExpiredSubscriptionsAsync()
        {
            try
            {
                var allSubscriptions = await _unitOfWork.Repository<Subscription>()
                    .ListAllAsync();

                var allStudents = await _unitOfWork.Repository<Student>()
                    .ListAllAsync();

                var allPlans = await _unitOfWork.Repository<SubscriptionPlan>()
                    .ListAllAsync();

                var expiredSubscriptions = allSubscriptions
                    .Where(s => (s.Status == "active" && s.EndDate <= DateTime.UtcNow) ||
                                s.Status == "expired")
                    .OrderBy(s => s.EndDate)
                    .ToList();

                var result = new List<SubscriptionResponseDto>();

                foreach (var subscription in expiredSubscriptions)
                {
                    var student = allStudents.FirstOrDefault(s => s.Id == subscription.StudentId);
                    var plan = allPlans.FirstOrDefault(p => p.Id == subscription.SubscriptionPlanId);

                    result.Add(new SubscriptionResponseDto
                    {
                        Id = subscription.Id,
                        UserId = student?.ApplicationUserId ?? Guid.Empty,
                        StudentId = subscription.StudentId,
                        //StudentName = student != null ? $"{student.FirstName} {student.LastName}" : string.Empty,
                        StudentEmail = student?.Email ?? string.Empty,
                        SubscriptionPlanId = subscription.SubscriptionPlanId,
                        SubscriptionPlanName = plan?.Name ?? string.Empty,
                        PaymentTransactionId = subscription.PaymentTransactionId,
                        PaymentMethod = subscription.PaymentMethod,
                        StartDate = subscription.StartDate,
                        EndDate = subscription.EndDate,
                        Status = subscription.Status,
                        Amount = subscription.AmountPaid,
                        IsAutoRenew = subscription.IsAutoRenew,
                        Notes = subscription.Notes,
                        CreatedAt = subscription.CreatedAt,
                        UpdatedAt = subscription.UpdatedAt
                    });
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting expired subscriptions");
                throw;
            }
        }

        public async Task<SubscriptionStatsDto> GetSubscriptionStatsAsync()
        {
            try
            {
                var allSubscriptions = await _unitOfWork.Repository<Subscription>()
                    .ListAllAsync();

                var allPlans = await _unitOfWork.Repository<SubscriptionPlan>()
                    .ListAllAsync();

                var now = DateTime.UtcNow;
                var startOfMonth = new DateTime(now.Year, now.Month, 1);
                var startOfYear = new DateTime(now.Year, 1, 1);

                var stats = new SubscriptionStatsDto
                {
                    TotalSubscriptions = allSubscriptions.Count,
                    ActiveSubscriptions = allSubscriptions.Count(s =>
                        s.Status == "active" && s.EndDate > now),
                    ExpiredSubscriptions = allSubscriptions.Count(s =>
                        s.Status == "expired" || (s.Status == "active" && s.EndDate <= now)),
                    PendingSubscriptions = allSubscriptions.Count(s => s.Status == "pending"),
                    CancelledSubscriptions = allSubscriptions.Count(s => s.Status == "cancelled"),
                    TotalRevenue = allSubscriptions.Sum(s => s.AmountPaid),
                    MonthlyRevenue = allSubscriptions
                        .Where(s => s.CreatedAt >= startOfMonth)
                        .Sum(s => s.AmountPaid),
                    YearlyRevenue = allSubscriptions
                        .Where(s => s.CreatedAt >= startOfYear)
                        .Sum(s => s.AmountPaid)
                };

                // Group by plan
                foreach (var plan in allPlans)
                {
                    var count = allSubscriptions.Count(s => s.SubscriptionPlanId == plan.Id);
                    if (count > 0)
                        stats.SubscriptionsByPlan[plan.Name] = count;
                }

                // Group by status
                stats.SubscriptionsByStatus = allSubscriptions
                    .GroupBy(s => s.Status)
                    .ToDictionary(g => g.Key, g => g.Count());

                // Group by month
                stats.SubscriptionsByMonth = allSubscriptions
                    .GroupBy(s => new { s.CreatedAt.Year, s.CreatedAt.Month })
                    .OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Month)
                    .ToDictionary(
                        g => $"{g.Key.Year}-{g.Key.Month:D2}",
                        g => g.Count()
                    );

                return stats;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting subscription statistics");
                throw;
            }
        }
    }
}