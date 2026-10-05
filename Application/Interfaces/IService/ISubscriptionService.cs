using Adros.Application.DTOs.Subscription;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adros.Application.Interfaces.IService
{
    public interface ISubscriptionService
    {


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
    }
}
