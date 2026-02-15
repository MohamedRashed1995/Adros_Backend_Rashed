using Adros.Shared;

namespace Adros.Core.Entities.Home
{
    public class VariousSkill : BaseEntity
    {
        public string VideoURL { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public ICollection<VariousSkillView> Views { get; set; } = [];
    }
}
