using Adros.Application.DTOs.Subscription;
<<<<<<< HEAD
using Adros.Core.DomainServices.IDomainService;
=======
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adros.Application.Interfaces.IService
{
    public interface ISubscriptionService
    {
<<<<<<< HEAD
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
=======


        // Basic CRUD Operations
        Task<SubscriptionResponseDto> CreateAsync(CreateSubscriptionDto dto);
        Task<SubscriptionResponseDto> GetByIdAsync(Guid id);
        Task<IEnumerable<SubscriptionResponseDto>> GetAllAsync();
        Task<IEnumerable<SubscriptionResponseDto>> GetByUserAsync(Guid userId);
        Task<SubscriptionResponseDto> UpdateAsync(Guid id, UpdateSubscriptionDto dto);
        Task<bool> DeleteAsync(Guid id);

        // Subscription Management
        Task<bool> CancelSubscriptionAsync(Guid subscriptionId);

        // Status Checks
        Task<bool> CheckUserSubscriptionStatusAsync(Guid userId);

        // Filtering and Searching
        Task<IEnumerable<SubscriptionResponseDto>> GetActiveSubscriptionsAsync();
        Task<IEnumerable<SubscriptionResponseDto>> GetExpiredSubscriptionsAsync();

        // Statistics and Reports
        Task<SubscriptionStatsDto> GetSubscriptionStatsAsync();
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
    }
}
