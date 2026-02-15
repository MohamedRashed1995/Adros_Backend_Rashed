using Adros.Core.Entities.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Adros.Persistence.Configurations.Users
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {

            builder.Property(s => s.Government)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(s => s.City)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(s => s.LoginTimes)
                .HasDefaultValue(0);

            builder.HasOne(s => s.ApplicationUser)
                .WithOne(u => u.Student)
                .HasForeignKey<Student>(s => s.ApplicationUserId);
        }
    }

}
