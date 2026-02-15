using Adros.Core.Entities.Course;
using Adros.Shared.Specifications;

namespace Adros.Core.Specifications
{
    public class SubjectSpecifications: BaseSpecification<Subject>
    {
        public SubjectSpecifications(SubjectSpecParams specParams)
        : base(x =>
            (string.IsNullOrEmpty(specParams.SearchVal) || x.Title.Contains(specParams.SearchVal, StringComparison.CurrentCultureIgnoreCase)) &&
            (!specParams.LevelId.HasValue || x.LevelId == specParams.LevelId) &&
            !x.Deleted)
        {
            ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort.ToLower())
                {
                    case "title":
                        ApplyOrderBy(x => x.Title);
                        break;
                    
                        
                    default:
                        ApplyOrderBy(x => x.Title);
                        break;
                }
            }
        }
    }
    public class SubjectWithFilterForCountSpecification(SubjectSpecParams specParams) : BaseSpecification<Subject>(x =>
                (string.IsNullOrEmpty(specParams.SearchVal) || x.Title.Contains(specParams.SearchVal, StringComparison.CurrentCultureIgnoreCase)) &&
                (!specParams.LevelId.HasValue || x.LevelId == specParams.LevelId)
               && !x.Deleted)
    {
    }
}
