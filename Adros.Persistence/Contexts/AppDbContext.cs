using Adros.Core.Entities;
using Adros.Core.Entities.Assessements;
using Adros.Core.Entities.Course;
using Adros.Core.Entities.Home;
using Adros.Core.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Adros.Persistence.Contexts
{
    public class AppDbContext : 
        IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options)
        {
            
        }

        #region IdentityModels
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public new DbSet<IdentityRole<Guid>> Roles { get; set; }
        public new DbSet<IdentityUserClaim<Guid>> UserClaims { get; set; }
        public new DbSet<IdentityUserRole<Guid>> UserRoles { get; set; }
        public new DbSet<IdentityUserLogin<Guid>> UserLogins { get; set; }
        public new DbSet<IdentityRoleClaim<Guid>> RoleClaims { get; set; }
        public new DbSet<IdentityUserToken<Guid>> UserTokens { get; set; }
        public DbSet<UserOtp> UserOtps { get; set; }
        #endregion
        #region Users
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        #endregion
        #region Home
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Calender> Calenders { get; set; }
        public DbSet<VariousSkill> VariousSkills { get; set; }
        public DbSet<VariousSkillView> VariousSkillsViews { get; set; }
        public DbSet<Stage> Stages { get; set; }
        #endregion
        #region Courses
        public DbSet<Level> Levels { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<VideoView> VideoViews { get; set; }
        public DbSet<VideoDownload> VideoDownloads { get; set; }
        public DbSet<StudentProgress> StudentProgresses { get; set; }
        public DbSet<AssessmentQuestion> AssessmentQuestions { get; set; }
        #endregion
        #region Exams
        public DbSet<Assessment> Assessments { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<DificultyLevel> DificultyLevels { get; set; }
        public DbSet<Answer> Answers { get; set; }

        #endregion



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .ToTable("Users");

            modelBuilder.Entity<IdentityRole<Guid>>()
                .ToTable("Roles");

            modelBuilder.Entity<IdentityUserClaim<Guid>>()
                .ToTable("UserClaims");

            modelBuilder.Entity<IdentityUserRole<Guid>>()
                .ToTable("UserRoles");

            modelBuilder.Entity<IdentityUserLogin<Guid>>()
                .ToTable("UserLogins");

            modelBuilder.Entity<IdentityRoleClaim<Guid>>()
                .ToTable("RoleClaims");

            modelBuilder.Entity<IdentityUserToken<Guid>>()
                .ToTable("UserTokens");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
