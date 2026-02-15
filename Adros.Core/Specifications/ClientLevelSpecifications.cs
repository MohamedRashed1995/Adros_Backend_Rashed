using Adros.Core.Entities.Course;
using Adros.Shared.Specifications;

namespace Adros.Core.Specifications
{
    public class ClientLevelSpecifications:BaseSpecification<Level>
    {
        public ClientLevelSpecifications(Guid stageId) : base(l => !l.Deleted && l.StageId == stageId)
        {
            ApplyOrderBy(l => l.Title);
            
        }
        public ClientLevelSpecifications() : base(l => !l.Deleted)
        {
            ApplyOrderBy(l => l.Title);
            AddInclude(l => l.Subjects);
        }
        public ClientLevelSpecifications(int skip, int take) : base(l => !l.Deleted)
        {
            ApplyOrderBy(l => l.Title);
            ApplyPaging(skip, take);
            AddInclude(l => l.Subjects);
        }
    }
}
