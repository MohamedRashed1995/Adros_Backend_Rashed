using Adros.Core.Entities.Home;
using Adros.Shared.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adros.Core.Specifications
{
    public class HomeStageSpecifications : BaseSpecification<Stage>
    {
        // Default: Filter undeleted stages and order by Order (or max value if null)
        public HomeStageSpecifications() : base(s => !s.Deleted)
        {
            ApplyOrderBy(s => s.Order ?? int.MaxValue);
            AddInclude(s => s.Levels);
        }

        // Filter by Title and order by Order (or max value if null)
        public HomeStageSpecifications(string title) : base(s => !s.Deleted && s.Title.Contains(title))
        {
            ApplyOrderBy(s => s.Order ?? int.MaxValue);
            AddInclude(s => s.Levels);
        }

        // Paginated results ordered by Order (or max value if null)
        public HomeStageSpecifications(int skip, int take) : base(s => !s.Deleted)
        {
            ApplyOrderBy(s => s.Order ?? int.MaxValue);
            ApplyPaging(skip, take);
            AddInclude(s => s.Levels);
        }
    }
}
