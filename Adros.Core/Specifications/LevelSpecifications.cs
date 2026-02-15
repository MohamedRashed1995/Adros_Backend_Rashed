using Adros.Core.Entities.Course;
using Adros.Shared.Specifications;

namespace Adros.Core.Specifications
{
    public class LevelSpecifications : BaseSpecification<Level>
    {
        public LevelSpecifications(Guid? stageId = null,
            bool includeDetails = false,
            Guid? levelId = null)
        {
            if (levelId.HasValue)
            {
                AddCriteria(l => l.Id == levelId.Value);
            }

            if (stageId.HasValue)
            {
                AddCriteria(l => l.StageId == stageId.Value);
            }

            if (includeDetails)
            {
                AddInclude(l => l.Stage);
                // Add other includes as needed
            }
        }
    }
}
