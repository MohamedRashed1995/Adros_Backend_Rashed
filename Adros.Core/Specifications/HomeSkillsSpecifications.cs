
using Adros.Core.Entities.Home;
using Adros.Shared.Specifications;

namespace Adros.Core.Specifications
{
    public class HomeSkillsSpecifications : BaseSpecification<VariousSkill>
    {
        public HomeSkillsSpecifications(int? take, int? skip) : base(b => !b.Deleted)
        {
            if (skip!=null && take!=null)
            {
                ApplyPaging(skip ?? 0, take ?? 0);
            }
        }
    }
}
