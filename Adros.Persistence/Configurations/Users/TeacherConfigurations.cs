using Adros.Core.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adros.Persistence.Configurations.Users
{
    public class TeacherConfigurations : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {

            builder.HasOne(s => s.ApplicationUser)
                .WithOne(u => u.Teacher)
                .HasForeignKey<Teacher>(s => s.ApplicationUserId);
        }
    }
}
