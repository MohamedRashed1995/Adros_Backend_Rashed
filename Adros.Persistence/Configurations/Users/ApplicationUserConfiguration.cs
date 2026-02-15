using Adros.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Adros.Core.Entities.Users;

namespace Adros.Persistence.Configurations.Users
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder
                .HasOne(a => a.Student)
                .WithOne(s => s.ApplicationUser)
                .HasForeignKey<Student>(s => s.ApplicationUserId);

            builder.Property(u => u.Photo)
                .HasMaxLength(500)
                .IsRequired(false);

            builder
              .HasOne(a => a.Teacher)
              .WithOne(s => s.ApplicationUser)
              .HasForeignKey<Teacher>(s => s.ApplicationUserId);
        }
    }
}
