using System.ComponentModel.DataAnnotations.Schema;

namespace Adros.Core.Entities.Home
{
    public class VariousSkillView
    {

        [ForeignKey(nameof(VariousSkill))]
        public Guid VariousSkillId { get; set; }
        public VariousSkill VariousSkill { get; set; }


        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
