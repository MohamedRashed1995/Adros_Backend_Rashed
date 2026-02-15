using Adros.Application.DTOs.Subscription;
using Adros.Core.DomainServices.IDomainService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adros.Application.Interfaces.IService
{
    public interface ISubscriptionService
    {
        // ============================
        // Basic CRUD Operations
        // ============================
        Task<SubscriptionResponseDto> CreateAsync(CreateSubscriptionDto dto);
        Task<SubscriptionResponseDto> GetByIdAsync(Guid id);
        Task<SubscriptionsWithStudentDto> GetAllAsync(ICurrentUserService currentUserService, IStudentService studentService);
        Task<SubscriptionResponseDto> UpdateAsync(Guid id, UpdateSubscriptionDto dto);
        Task<bool> DeleteAsync(Guid id);

        // ============================
        // Optional: Get Subscriptions by User
        // ============================
        //Task<IEnumerable<SubscriptionResponseDto>> GetByUserAsync(Guid userId);

        // ============================
        // Subscription Status Checks
        // ============================
        //Task<bool> CheckUserSubscriptionStatusAsync(Guid userId);

        // ============================
        // Active / Expired Filtering
        // ============================
        //Task<IEnumerable<SubscriptionResponseDto>> GetActiveSubscriptionsAsync();
        //Task<IEnumerable<SubscriptionResponseDto>> GetExpiredSubscriptionsAsync();

        // ============================
        // Statistics & Reports
        // ============================
        //Task<SubscriptionStatsDto> GetSubscriptionStatsAsync();

        // ============================
        // Cancel Subscription (Soft Delete or Business Logic)
        // ============================
        //Task<bool> CancelSubscriptionAsync(Guid subscriptionId);
    }
}
