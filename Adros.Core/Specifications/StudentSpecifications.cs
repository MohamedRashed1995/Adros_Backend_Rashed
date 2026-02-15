using Adros.Core.Entities.Users;
using Adros.Core.Specifications.QueryParams;
using Adros.Shared.Settings;
using Adros.Shared.Specifications;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Adros.Core.Specifications
{
    public class StudentSpecifications : BaseSpecification<Student>
    {
        private static readonly IReadOnlyDictionary<string, Expression<Func<Student, object>>> SortMap =
            new Dictionary<string, Expression<Func<Student, object>>>(
                StringComparer.OrdinalIgnoreCase
            )
            {
                [SortField.Name] = s => s.ApplicationUser.UserName,
                //[SortField.CreatedAt] = s => s.CreatedAt,
                [SortField.Email] = s => s.ApplicationUser.Email,
                [SortField.Government] = s => s.Government,
                [SortField.City] = s => s.City
            };

        // Default includes for all student queries
        private static readonly List<Expression<Func<Student, object>>> DefaultIncludes =
        [
            s => s.ApplicationUser,
            s => s.Level,
            s => s.Level.Stage,
            s => s.WatchLater,
            s => s.VideoViews
        ];

        public StudentSpecifications(
            StudentQueryParameters parameters,
            int page = 1,
            int pageSize = 10)
        {
            AddDefaultIncludes();
            ApplyFilters(parameters);
            ApplySorting(parameters?.Sort);
            ApplySafePaging(page, pageSize);
        }

        private void AddDefaultIncludes()
        {
            foreach (var include in DefaultIncludes)
            {
                Includes.Add(include);
            }
        }

        private void ApplyFilters(StudentQueryParameters parameters)
        {
            if (parameters == null) return;

            ApplyActiveFilter(parameters.IsActive);
            ApplySearchFilter(parameters.Search);

            ApplyExactMatchFilter(parameters.Name, s => s.ApplicationUser.UserName);
            ApplyExactMatchFilter(parameters.Email, s => s.ApplicationUser.Email);
            ApplyExactMatchFilter(parameters.Government, s => s.Government);
            ApplyExactMatchFilter(parameters.City, s => s.City);
            ApplyExactMatchFilter(parameters.Level, s => s.Level.Title);
            ApplyExactMatchFilter(parameters.Stage, s => s.Level.Stage.Title);
        }

        private void ApplyActiveFilter(bool? isActive)
        {
            if (isActive.HasValue)
            {
                AddCriteria(s => s.ApplicationUser.IsActive == isActive);
            }
        }

        private void ApplySearchFilter(string? searchTerm)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                AddCriteria(s =>
                    EF.Functions.Like(s.ApplicationUser.UserName, $"%{searchTerm}%") ||
                    EF.Functions.Like(s.ApplicationUser.Email, $"%{searchTerm}%") ||
                    EF.Functions.Like(s.Government, $"%{searchTerm}%") ||
                    EF.Functions.Like(s.City, $"%{searchTerm}%") ||
                    EF.Functions.Like(s.Level.Title, $"%{searchTerm}%") ||
                    EF.Functions.Like(s.Level.Stage.Title, $"%{searchTerm}%")
                );
            }
        }

        private void ApplyExactMatchFilter<TValue>(
            TValue? value,
            Expression<Func<Student, TValue>> selector)
            where TValue : struct
        {
            if (value != null)
            {
                AddCriteria(Expression.Lambda<Func<Student, bool>>(
                    Expression.Equal(
                        selector.Body,
                        Expression.Constant(value, typeof(TValue))
                    ),
                    selector.Parameters
                ));
            }
        }

        private void ApplyExactMatchFilter(
            string? value,
            Expression<Func<Student, string>> selector)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                AddCriteria(Expression.Lambda<Func<Student, bool>>(
                    Expression.Equal(
                        selector.Body,
                        Expression.Constant(value)
                    ),
                    selector.Parameters
                ));
            }
        }

        private void ApplySafePaging(int page, int pageSize)
        {
            page = Math.Max(page, 1);
            pageSize = Math.Clamp(pageSize, 1, SystemSettings.Pagination.MaxPageSize);

            ApplyPaging(pageSize * (page - 1), pageSize);
        }

        private void ApplySorting(string? sort)
        {
            if (string.IsNullOrWhiteSpace(sort))
            {
                ApplyDefaultSorting();
                return;
            }

            var (sortField, sortOrder) = ParseSortParameter(sort);

            if (SortMap.TryGetValue(sortField, out var expression))
            {
                ApplySort(expression, sortOrder);
            }
            else
            {
                ApplyDefaultSorting();
            }
        }

        private static (string Field, SortDirection Direction) ParseSortParameter(string sort)
        {
            var parts = sort.Split('_', 2);
            var field = parts[0];
            var direction = parts.Length > 1 && parts[1].Equals("desc", StringComparison.OrdinalIgnoreCase)
                ? SortDirection.Descending
                : SortDirection.Ascending;

            return (field, direction);
        }

        private void ApplyDefaultSorting()
        {
            //ApplyOrderByDescending(s => s.CreatedAt);
        }

        private void ApplySort(
            Expression<Func<Student, object>> expression,
            SortDirection direction)
        {
            if (direction == SortDirection.Ascending)
            {
                ApplyOrderBy(expression);
            }
            else
            {
                ApplyOrderByDescending(expression);
            }
        }

        private static class SortField
        {
            public const string Name = "name";
            public const string CreatedAt = "createdat";
            public const string Email = "email";
            public const string Government = "government";
            public const string City = "city";
        }

        private enum SortDirection { Ascending, Descending }
    }
}