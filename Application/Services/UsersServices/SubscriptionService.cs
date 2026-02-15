using Adros.Application.DTOs.Subscription;
using Adros.Application.Interfaces.IService;
using Adros.Core.DomainServices.IDomainService;
using Adros.Core.Entities.Subscription;
using Adros.Core.Entities.Users;
using Adros.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adros.Application.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubscriptionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SubscriptionResponseDto> CreateAsync(CreateSubscriptionDto dto)
        {
            var subscription = new Subscription
            {
                Name = dto.Name,
                Price = dto.Price,
                Duration = dto.Duration,
                Benefits = dto.Benefits,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Repository<Subscription>().AddAsync(subscription);
            await _unitOfWork.CompleteAsync();

            return MapToDto(subscription);
        }

        public async Task<SubscriptionResponseDto> GetByIdAsync(Guid id)
        {
            var subscription = await _unitOfWork.Repository<Subscription>()
                .GetByIdAsync(id);

            if (subscription == null) return null;

            return MapToDto(subscription);
        }

        //public async Task<IEnumerable<SubscriptionResponseDto>> GetAllAsync()
        //{
        //    var list = await _unitOfWork.Repository<Subscription>().GetAllAsync();
        //    var result = new List<SubscriptionResponseDto>();
        //    foreach (var sub in list)
        //    {
        //        result.Add(MapToDto(sub));
        //    }
        //    return result;
        //}
        public async Task<SubscriptionsWithStudentDto> GetAllAsync(ICurrentUserService currentUserService,IStudentService studentService)
        {
            var userId = currentUserService.UserId;
            var studentId = await studentService.GetStudentIdByUserIdAsync(userId);

            var studentSubscriptions = await _unitOfWork
                .Repository<StudentSubscription>()
                .GetAllAsync(filter: q => q.Where(x => x.StudentId == studentId));

            // نخزن الـ ids في HashSet عشان السرعة
            var subscribedIds = studentSubscriptions 
                .Select(x => x.SubscriptionId)
                .ToHashSet();

            var subscriptions = await _unitOfWork
                .Repository<Subscription>()
                .GetAllAsync();

            var result = subscriptions.Select(sub => new SubscriptionResponseDto
            {
                Id = sub.Id,
                Name = sub.Name,
                Price = sub.Price,
                Duration = sub.Duration,
                Benefits = sub.Benefits,
                CreatedAt = sub.CreatedAt,
                UpdatedAt = sub.UpdatedAt,
                IsSubscribed = subscribedIds.Contains(sub.Id)
            }).ToList();

            return new SubscriptionsWithStudentDto
            {
                StudentId = studentId.ToString(),
                Subscriptions = result
            };
        }





        public async Task<SubscriptionResponseDto> UpdateAsync(Guid id, UpdateSubscriptionDto dto)
        {
            var subscription = await _unitOfWork.Repository<Subscription>().GetByIdAsync(id);
            if (subscription == null) return null;

            subscription.Name = dto.Name ?? subscription.Name;
            subscription.Price = dto.Price ?? subscription.Price;
            subscription.Duration = dto.Duration ?? subscription.Duration;
            subscription.Benefits = dto.Benefits ?? subscription.Benefits;
            subscription.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Repository<Subscription>().Update(subscription);
            await _unitOfWork.CompleteAsync();

            return MapToDto(subscription);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var subscription = await _unitOfWork.Repository<Subscription>().GetByIdAsync(id);
            if (subscription == null) return false;

            _unitOfWork.Repository<Subscription>().Delete(subscription);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        private SubscriptionResponseDto MapToDto(Subscription sub)
        {
            return new SubscriptionResponseDto
            {
                Id = sub.Id,
                Name = sub.Name,
                Price = sub.Price,
                Duration = sub.Duration,
                Benefits = sub.Benefits,
                CreatedAt = sub.CreatedAt,
                UpdatedAt = sub.UpdatedAt
            };
        }
    }
}
