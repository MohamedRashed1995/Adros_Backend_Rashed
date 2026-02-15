using Adros.Shared.Interfaces;
using Adros.Shared;
using Microsoft.EntityFrameworkCore;

namespace Adros.Persistence.Repositories
{
    public static class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> query, ISpecification<T> spec)
        {
            query = ApplyCriteria(query, spec);
            query = ApplyOrdering(query, spec);
            query = ApplyPaging(query, spec);
            query = ApplyIncludes(query, spec);
            return query;
        }

        private static IQueryable<T> ApplyCriteria(IQueryable<T> query, ISpecification<T> spec)
            => spec.Criteria != null ? query.Where(spec.Criteria) : query;

        private static IQueryable<T> ApplyOrdering(IQueryable<T> query, ISpecification<T> spec)
        {
            if (spec.OrderBy != null)
                return query.OrderBy(spec.OrderBy);
            if (spec.OrderByDescending != null)
                return query.OrderByDescending(spec.OrderByDescending);

            return query;
        }

        private static IQueryable<T> ApplyPaging(IQueryable<T> query, ISpecification<T> spec)
        {
            if (!spec.IsPagingEnabled || spec.Skip < 0 || spec.Take <= 0)
                return query;

            return query.Skip(spec.Skip).Take(spec.Take);
        }

        private static IQueryable<T> ApplyIncludes(IQueryable<T> query, ISpecification<T> spec)
        {
            // Expression-based includes
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            // String-based includes
            query = spec.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }
    }
}