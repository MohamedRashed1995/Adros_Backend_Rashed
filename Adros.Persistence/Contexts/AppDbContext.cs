using Adros.Core.Entities;
using Adros.Core.Entities.Assessements;
using Adros.Core.Entities.Course;
using Adros.Core.Entities.Home;
using Adros.Core.Entities.Subscription;
using Adros.Core.Entities.Users;
using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;



namespace Adros.Persistence.Contexts
{


    public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // DbSets
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        //public DbSet<IdentityRole<Guid>> Roles { get; set; }
        //public DbSet<IdentityUserClaim<Guid>> UserClaims { get; set; }
        //public DbSet<IdentityUserRole<Guid>> UserRoles { get; set; }
        //public DbSet<IdentityUserLogin<Guid>> UserLogins { get; set; }
        //public DbSet<IdentityRoleClaim<Guid>> RoleClaims { get; set; }
        //public DbSet<IdentityUserToken<Guid>> UserTokens { get; set; }

        public DbSet<UserOtp> UserOtps { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Calender> Calenders { get; set; }
        public DbSet<VariousSkill> VariousSkills { get; set; }
        //public DbSet<VariousSkillView> VariousSkillsViews { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Stage> Stages { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Lesson> Lessons { get; set; } = default!;
        public DbSet<Unit> Units { get; set; } = default!;
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<VideoView> VideoViews { get; set; }
        public DbSet<WatchLater> watchlater { get; set; }
        public DbSet<StudentProgress> StudentProgresses { get; set; }
        public DbSet<AssessmentQuestion> AssessmentQuestions { get; set; }
        public DbSet<Assessment> Assessments { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<DificultyLevel> DificultyLevels { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<StudentSubscription> StudentSubscriptions { get; set; }
        public DbSet<Video> videos { get; set; } = default!;
        //public DbSet<>
        public DbSet<Teacher> Teachers { get; set; } = default!;
        public DbSet<Student> Students { get; set; }
        public DbSet<PendingPayment> pendingPayments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Rename Identity tables
            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            modelBuilder.Entity<IdentityRole<Guid>>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens");
    //        modelBuilder.Entity<Unit>()
    //.Ignore(u => u.);


            modelBuilder.Entity<Level>()
                .HasOne(l => l.Stage)
                .WithMany(s => s.Levels)
                .HasForeignKey(l => l.StageId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Subject>()
                .HasOne(s => s.Level)
                .WithMany(l => l.Subjects)
                .HasForeignKey(s => s.LevelId)
                .OnDelete(DeleteBehavior.Cascade);

            // ← السطر الجديد/المؤكد:
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Level)
                .WithMany(l => l.Students)
                .HasForeignKey(s => s.LevelId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Unit>()
                .HasMany(t => t.Lessons)
                .WithOne(l => l.Unit)
                .HasForeignKey(l => l.UnitId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Lesson>()
                .HasMany(l => l.Videos)
                .WithOne(v => v.Lesson)
                .HasForeignKey(v => v.LessonId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Video>().ToTable("Videos");
            // Apply configurations from assembly
            modelBuilder.Entity<Lesson>()
                .HasOne(l => l.Unit)
                .WithMany(u => u.Lessons)
                .HasForeignKey(l => l.UnitId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Video>()
                .HasOne(v => v.Lesson)
                .WithMany(l => l.Videos)
                .HasForeignKey(v => v.LessonId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Subscription>()
                    .Property(s => s.Benefits)
                    .HasConversion(
                        v => string.Join("||", v),
                        v => v.Split("||", StringSplitOptions.RemoveEmptyEntries).ToList()
                    );

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=db36362.public.databaseasp.net; Database=db36362; User Id=db36362; Password=H_b2zE4#!9yB; Encrypt=True; TrustServerCertificate=True; MultipleActiveResultSets=True;");
            }

            // هذا السطر مهم لحل التحذير
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }
    }
}
