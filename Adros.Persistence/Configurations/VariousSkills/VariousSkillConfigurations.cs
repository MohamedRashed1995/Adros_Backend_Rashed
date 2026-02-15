using Adros.Core.Entities.Home;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adros.Persistence.Configurations.VariousSkills
{
    public class VariousSkillConfigurations : IEntityTypeConfiguration<VariousSkillView>
    {
        public void Configure(EntityTypeBuilder<VariousSkillView> builder)
        {
            builder.HasKey(V => new { V.UserId, V.VariousSkillId });
        }
    }
}
