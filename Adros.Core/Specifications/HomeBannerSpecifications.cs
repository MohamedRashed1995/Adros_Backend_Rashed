using Adros.Core.Entities.Home;
using Adros.Shared.Specifications;

namespace Adros.Core.Specifications
{
    public class HomeBannerSpecifications : BaseSpecification<Banner>
    {
        public HomeBannerSpecifications() : base(b => !b.Deleted)
        {
            ApplyOrderBy(b => b.Order ?? int.MaxValue);
        }
    }
}
