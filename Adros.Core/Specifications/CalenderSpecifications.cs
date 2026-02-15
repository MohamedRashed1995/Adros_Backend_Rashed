using Adros.Core.Entities.Home;
using Adros.Shared.Specifications;
using LinqKit;

namespace Adros.Core.Specifications
{
    public class CalenderSpecifications : BaseSpecification<Calender>
    {
        public CalenderSpecifications(
            Guid? userId = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            string? color = null,
            int? skip = null,
            int? take = null)
            : base()
        {
            // Start with the base filter (non-deleted)
            var predicate = PredicateBuilder.New<Calender>(c => !c.Deleted);

            // Add User ID filter if provided
            if (userId.HasValue)
            {
                predicate = predicate.And(c => c.CreatedBy == userId.Value);
            }

            // Date range filters (startDate and/or endDate)
            if (startDate.HasValue || endDate.HasValue)
            {
                if (startDate.HasValue)
                {
                    predicate = predicate.And(c => c.Date >= startDate.Value);
                }
                if (endDate.HasValue)
                {
                    predicate = predicate.And(c => c.Date <= endDate.Value);
                }
            }

            // Color filter (only if provided)
            if (!string.IsNullOrEmpty(color))
            {
                predicate = predicate.And(c =>
                    c.Color.ToString().Equals(color, StringComparison.OrdinalIgnoreCase));
            }

            // Apply the final composed predicate
            AddCriteria(predicate);

            // Always order by date
            ApplyOrderBy(c => c.Date);

            // Paging (only if both skip and take are provided)
            if (skip.HasValue && take.HasValue)
            {
                ApplyPaging(skip.Value, take.Value);
            }
        }
    }
}

