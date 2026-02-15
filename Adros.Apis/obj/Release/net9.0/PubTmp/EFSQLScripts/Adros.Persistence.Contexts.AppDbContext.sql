IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE TABLE [Banners] (
        [Id] uniqueidentifier NOT NULL,
        [ImageName] nvarchar(max) NOT NULL,
        [Order] int NULL,
        [UpdatedAt] datetime2 NULL,
        [CreatedBy] uniqueidentifier NULL,
        [UpdatedBy] uniqueidentifier NULL,
        [Deleted] bit NOT NULL,
        CONSTRAINT [PK_Banners] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE TABLE [Calenders] (
        [Id] uniqueidentifier NOT NULL,
        [Date] datetime2 NOT NULL,
        [Notes] nvarchar(max) NOT NULL,
        [Color] int NOT NULL,
        [UpdatedAt] datetime2 NULL,
        [CreatedBy] uniqueidentifier NULL,
        [UpdatedBy] uniqueidentifier NULL,
        [Deleted] bit NOT NULL,
        CONSTRAINT [PK_Calenders] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE TABLE [DificultyLevels] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [UpdatedAt] datetime2 NULL,
        [CreatedBy] uniqueidentifier NULL,
        [UpdatedBy] uniqueidentifier NULL,
        [Deleted] bit NOT NULL,
        CONSTRAINT [PK_DificultyLevels] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE TABLE [Roles] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_Roles] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE TABLE [Stages] (
        [Id] uniqueidentifier NOT NULL,
        [ImageName] nvarchar(max) NOT NULL,
        [Title] nvarchar(max) NOT NULL,
        [Order] int NULL,
        [Type] int NOT NULL,
        [UpdatedAt] datetime2 NULL,
        [CreatedBy] uniqueidentifier NULL,
        [UpdatedBy] uniqueidentifier NULL,
        [Deleted] bit NOT NULL,
        CONSTRAINT [PK_Stages] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE TABLE [subscriptionPlans] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(100) NOT NULL,
        [Description] nvarchar(500) NOT NULL,
        [Price] decimal(18,2) NOT NULL,
        [DurationInDays] int NOT NULL,
        [PlanType] nvarchar(50) NOT NULL,
        [IsActive] bit NOT NULL,
        [UpdatedAt] datetime2 NULL,
        [CreatedBy] uniqueidentifier NULL,
        [UpdatedBy] uniqueidentifier NULL,
        [Deleted] bit NOT NULL,
        CONSTRAINT [PK_subscriptionPlans] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE TABLE [Users] (
        [Id] uniqueidentifier NOT NULL,
        [ResetPasswordCode] nvarchar(max) NULL,
        [ResetPasswordCodeExpiry] datetime2 NULL,
        [ResetPasswordToken] nvarchar(max) NULL,
        [ResetPasswordTokenExpiry] datetime2 NULL,
        [FirstName] nvarchar(max) NOT NULL,
        [LastName] nvarchar(max) NOT NULL,
        [Photo] nvarchar(500) NULL,
        [StudentId] uniqueidentifier NULL,
        [TeacherId] uniqueidentifier NULL,
        [IsActive] bit NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE TABLE [VariousSkills] (
        [Id] uniqueidentifier NOT NULL,
        [VideoURL] nvarchar(max) NOT NULL,
        [Title] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [UpdatedAt] datetime2 NULL,
        [CreatedBy] uniqueidentifier NULL,
        [UpdatedBy] uniqueidentifier NULL,
        [Deleted] bit NOT NULL,
        CONSTRAINT [PK_VariousSkills] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE TABLE [RoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] uniqueidentifier NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_RoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_RoleClaims_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE TABLE [Levels] (
        [Id] uniqueidentifier NOT NULL,
        [Title] nvarchar(max) NOT NULL,
        [StageId] uniqueidentifier NOT NULL,
        [UpdatedAt] datetime2 NULL,
        [CreatedBy] uniqueidentifier NULL,
        [UpdatedBy] uniqueidentifier NULL,
        [Deleted] bit NOT NULL,
        CONSTRAINT [PK_Levels] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Levels_Stages_StageId] FOREIGN KEY ([StageId]) REFERENCES [Stages] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE TABLE [Teacher] (
        [Id] uniqueidentifier NOT NULL,
        [ApplicationUserId] uniqueidentifier NOT NULL,
        [TeacherID] uniqueidentifier NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        [FirstName] nvarchar(max) NOT NULL,
        [LastName] nvarchar(max) NOT NULL,
        [About] nvarchar(max) NOT NULL,
        [UpdatedAt] datetime2 NULL,
        [CreatedBy] uniqueidentifier NULL,
        [UpdatedBy] uniqueidentifier NULL,
        [Deleted] bit NOT NULL,
        CONSTRAINT [PK_Teacher] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Teacher_Users_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE TABLE [UserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] uniqueidentifier NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_UserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_UserClaims_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE TABLE [UserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_UserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_UserLogins_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE TABLE [UserOtps] (
        [Id] uniqueidentifier NOT NULL,
        [Code] int NOT NULL,
        [ExpireDate] datetime2 NOT NULL,
        [ResendCount] int NOT NULL,
        [TryCount] int NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        [UpdatedAt] datetime2 NULL,
        [CreatedBy] uniqueidentifier NULL,
        [UpdatedBy] uniqueidentifier NULL,
        [Deleted] bit NOT NULL,
        CONSTRAINT [PK_UserOtps] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_UserOtps_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE TABLE [UserRoles] (
        [UserId] uniqueidentifier NOT NULL,
        [RoleId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_UserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_UserRoles_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_UserRoles_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE TABLE [UserTokens] (
        [UserId] uniqueidentifier NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_UserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_UserTokens_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE TABLE [VariousSkillsViews] (
        [VariousSkillId] uniqueidentifier NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_VariousSkillsViews] PRIMARY KEY ([UserId], [VariousSkillId]),
        CONSTRAINT [FK_VariousSkillsViews_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_VariousSkillsViews_VariousSkills_VariousSkillId] FOREIGN KEY ([VariousSkillId]) REFERENCES [VariousSkills] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE TABLE [Student] (
        [Id] uniqueidentifier NOT NULL,
        [FirstName] nvarchar(max) NOT NULL,
        [LastName] nvarchar(max) NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        [Government] nvarchar(100) NULL,
        [City] nvarchar(100) NULL,
        [BirthDate] date NULL,
        [ApplicationUserId] uniqueidentifier NOT NULL,
        [LoginTimes] int NOT NULL DEFAULT 0,
        [LevelId] uniqueidentifier NULL,
        [SubscriptionStatus] nvarchar(max) NOT NULL,
        [UpdatedAt] datetime2 NULL,
        [CreatedBy] uniqueidentifier NULL,
        [UpdatedBy] uniqueidentifier NULL,
        [Deleted] bit NOT NULL,
        CONSTRAINT [PK_Student] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Student_Levels_LevelId] FOREIGN KEY ([LevelId]) REFERENCES [Levels] ([Id]),
        CONSTRAINT [FK_Student_Users_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE TABLE [Subjects] (
        [Id] uniqueidentifier NOT NULL,
        [Title] nvarchar(max) NOT NULL,
        [LevelId] uniqueidentifier NOT NULL,
        [UpdatedAt] datetime2 NULL,
        [CreatedBy] uniqueidentifier NULL,
        [UpdatedBy] uniqueidentifier NULL,
        [Deleted] bit NOT NULL,
        CONSTRAINT [PK_Subjects] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Subjects_Levels_LevelId] FOREIGN KEY ([LevelId]) REFERENCES [Levels] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE TABLE [Subscriptions] (
        [Id] uniqueidentifier NOT NULL,
        [StudentId] uniqueidentifier NOT NULL,
        [SubscriptionPlanId] uniqueidentifier NOT NULL,
        [PaymentTransactionId] nvarchar(100) NOT NULL,
        [PaymentMethod] nvarchar(50) NOT NULL,
        [AmountPaid] decimal(18,2) NOT NULL,
        [StartDate] datetime2 NOT NULL,
        [EndDate] datetime2 NOT NULL,
        [Status] nvarchar(50) NOT NULL DEFAULT N'active',
        [IsAutoRenew] bit NOT NULL,
        [Notes] nvarchar(max) NOT NULL,
        [UpdatedAt] datetime2 NULL,
        [CreatedBy] uniqueidentifier NULL,
        [UpdatedBy] uniqueidentifier NULL,
        [Deleted] bit NOT NULL,
        CONSTRAINT [PK_Subscriptions] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Subscriptions_Student_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [Student] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Subscriptions_subscriptionPlans_SubscriptionPlanId] FOREIGN KEY ([SubscriptionPlanId]) REFERENCES [subscriptionPlans] ([Id]) ON DELETE NO ACTION
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE TABLE [Lessons] (
        [Id] uniqueidentifier NOT NULL,
        [Order] int NOT NULL,
        [Title] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [TeacherId] uniqueidentifier NOT NULL,
        [SubjectId] uniqueidentifier NOT NULL,
        [ExamId] uniqueidentifier NULL,
        [UpdatedAt] datetime2 NULL,
        [CreatedBy] uniqueidentifier NULL,
        [UpdatedBy] uniqueidentifier NULL,
        [Deleted] bit NOT NULL,
        CONSTRAINT [PK_Lessons] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Lessons_Subjects_SubjectId] FOREIGN KEY ([SubjectId]) REFERENCES [Subjects] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Lessons_Teacher_TeacherId] FOREIGN KEY ([TeacherId]) REFERENCES [Teacher] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE TABLE [Attachments] (
        [Id] uniqueidentifier NOT NULL,
        [Title] nvarchar(max) NOT NULL,
        [Url] nvarchar(max) NOT NULL,
        [LessonId] uniqueidentifier NOT NULL,
        [UpdatedAt] datetime2 NULL,
        [CreatedBy] uniqueidentifier NULL,
        [UpdatedBy] uniqueidentifier NULL,
        [Deleted] bit NOT NULL,
        CONSTRAINT [PK_Attachments] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Attachments_Lessons_LessonId] FOREIGN KEY ([LessonId]) REFERENCES [Lessons] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE TABLE [Topics] (
        [Id] uniqueidentifier NOT NULL,
        [Title] nvarchar(200) NOT NULL,
        [LessonId] uniqueidentifier NOT NULL,
        [UpdatedAt] datetime2 NOT NULL,
        [CreatedBy] uniqueidentifier NOT NULL,
        [UpdatedBy] uniqueidentifier NOT NULL,
        [Deleted] bit NOT NULL,
        CONSTRAINT [PK_Topics] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Topics_Lessons_LessonId] FOREIGN KEY ([LessonId]) REFERENCES [Lessons] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE TABLE [Assessments] (
        [Id] uniqueidentifier NOT NULL,
        [TopicId] uniqueidentifier NOT NULL,
        [Title] nvarchar(max) NOT NULL,
        [Score] int NOT NULL,
        [UpdatedAt] datetime2 NULL,
        [CreatedBy] uniqueidentifier NULL,
        [UpdatedBy] uniqueidentifier NULL,
        [Deleted] bit NOT NULL,
        CONSTRAINT [PK_Assessments] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Assessments_Topics_TopicId] FOREIGN KEY ([TopicId]) REFERENCES [Topics] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE TABLE [Questions] (
        [Id] uniqueidentifier NOT NULL,
        [Title] nvarchar(max) NOT NULL,
        [QuestionType] int NOT NULL,
        [TopicId] uniqueidentifier NOT NULL,
        [DificultyLevelId] uniqueidentifier NOT NULL,
        [UpdatedAt] datetime2 NULL,
        [CreatedBy] uniqueidentifier NULL,
        [UpdatedBy] uniqueidentifier NULL,
        [Deleted] bit NOT NULL,
        CONSTRAINT [PK_Questions] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Questions_DificultyLevels_DificultyLevelId] FOREIGN KEY ([DificultyLevelId]) REFERENCES [DificultyLevels] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Questions_Topics_TopicId] FOREIGN KEY ([TopicId]) REFERENCES [Topics] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE TABLE [StudentProgresses] (
        [StudentId] uniqueidentifier NOT NULL,
        [TopicId] uniqueidentifier NOT NULL,
        [ProficiencyLevel] nvarchar(50) NOT NULL,
        CONSTRAINT [PK_StudentProgresses] PRIMARY KEY ([StudentId], [TopicId]),
        CONSTRAINT [FK_StudentProgresses_Student_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [Student] ([Id]),
        CONSTRAINT [FK_StudentProgresses_Topics_TopicId] FOREIGN KEY ([TopicId]) REFERENCES [Topics] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE TABLE [TopicPrerequisites] (
        [PostrequisitesId] uniqueidentifier NOT NULL,
        [PrerequisitesId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_TopicPrerequisites] PRIMARY KEY ([PostrequisitesId], [PrerequisitesId]),
        CONSTRAINT [FK_TopicPrerequisites_Topics_PostrequisitesId] FOREIGN KEY ([PostrequisitesId]) REFERENCES [Topics] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_TopicPrerequisites_Topics_PrerequisitesId] FOREIGN KEY ([PrerequisitesId]) REFERENCES [Topics] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE TABLE [Videos] (
        [Id] uniqueidentifier NOT NULL,
        [Title] nvarchar(max) NOT NULL,
        [Duration] time NOT NULL,
        [Url] nvarchar(max) NOT NULL,
        [Order] int NULL,
        [LessonId] uniqueidentifier NOT NULL,
        [TopicId] uniqueidentifier NOT NULL,
        [BunnyVideoId] nvarchar(max) NOT NULL,
        [Status] int NOT NULL,
        [ProcessedAt] datetime2 NULL,
        [UpdatedAt] datetime2 NULL,
        [CreatedBy] uniqueidentifier NULL,
        [UpdatedBy] uniqueidentifier NULL,
        [Deleted] bit NOT NULL,
        CONSTRAINT [PK_Videos] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Videos_Lessons_LessonId] FOREIGN KEY ([LessonId]) REFERENCES [Lessons] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Videos_Topics_TopicId] FOREIGN KEY ([TopicId]) REFERENCES [Topics] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE TABLE [Answers] (
        [Id] uniqueidentifier NOT NULL,
        [Text] nvarchar(max) NOT NULL,
        [Order] int NULL,
        [IsCorrect] bit NOT NULL,
        [QuestionId] uniqueidentifier NOT NULL,
        [UpdatedAt] datetime2 NULL,
        [CreatedBy] uniqueidentifier NULL,
        [UpdatedBy] uniqueidentifier NULL,
        [Deleted] bit NOT NULL,
        CONSTRAINT [PK_Answers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Answers_Questions_QuestionId] FOREIGN KEY ([QuestionId]) REFERENCES [Questions] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE TABLE [AssessmentQuestions] (
        [AssessmentId] uniqueidentifier NOT NULL,
        [QuestionId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_AssessmentQuestions] PRIMARY KEY ([AssessmentId], [QuestionId]),
        CONSTRAINT [FK_AssessmentQuestions_Assessments_AssessmentId] FOREIGN KEY ([AssessmentId]) REFERENCES [Assessments] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AssessmentQuestions_Questions_QuestionId] FOREIGN KEY ([QuestionId]) REFERENCES [Questions] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE TABLE [VideoDownloads] (
        [Id] uniqueidentifier NOT NULL,
        [VideoId] uniqueidentifier NOT NULL,
        [StudentId] uniqueidentifier NOT NULL,
        [UpdatedAt] datetime2 NULL,
        [CreatedBy] uniqueidentifier NULL,
        [UpdatedBy] uniqueidentifier NULL,
        [Deleted] bit NOT NULL,
        CONSTRAINT [PK_VideoDownloads] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_VideoDownloads_Student_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [Student] ([Id]),
        CONSTRAINT [FK_VideoDownloads_Videos_VideoId] FOREIGN KEY ([VideoId]) REFERENCES [Videos] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE TABLE [VideoViews] (
        [Id] uniqueidentifier NOT NULL,
        [VideoId] uniqueidentifier NOT NULL,
        [StudentId] uniqueidentifier NOT NULL,
        [UpdatedAt] datetime2 NULL,
        [CreatedBy] uniqueidentifier NULL,
        [UpdatedBy] uniqueidentifier NULL,
        [Deleted] bit NOT NULL,
        CONSTRAINT [PK_VideoViews] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_VideoViews_Student_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [Student] ([Id]),
        CONSTRAINT [FK_VideoViews_Videos_VideoId] FOREIGN KEY ([VideoId]) REFERENCES [Videos] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] ON;
    EXEC(N'INSERT INTO [subscriptionPlans] ([Id], [CreatedBy], [Deleted], [Description], [DurationInDays], [IsActive], [Name], [PlanType], [Price], [UpdatedAt], [UpdatedBy])
    VALUES (''dbd7013e-e4e2-41fd-a586-e1793282b083'', NULL, CAST(0 AS bit), N''Premium monthly subscription'', 30, CAST(1 AS bit), N''Premium Monthly'', N''monthly'', 19.99, NULL, NULL),
    (''dc146682-ac7d-4e4c-857c-077da52c5eb1'', NULL, CAST(0 AS bit), N''Basic monthly subscription'', 30, CAST(1 AS bit), N''Basic Monthly'', N''monthly'', 9.99, NULL, NULL),
    (''f3b7bf25-ee10-4464-bb12-0ba04c792f98'', NULL, CAST(0 AS bit), N''Annual subscription with discount'', 365, CAST(1 AS bit), N''Annual Plan'', N''annual'', 99.99, NULL, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Answers_QuestionId] ON [Answers] ([QuestionId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_AssessmentQuestions_QuestionId] ON [AssessmentQuestions] ([QuestionId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Assessments_TopicId] ON [Assessments] ([TopicId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Attachments_LessonId] ON [Attachments] ([LessonId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Lessons_SubjectId] ON [Lessons] ([SubjectId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Lessons_TeacherId] ON [Lessons] ([TeacherId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Levels_StageId] ON [Levels] ([StageId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Questions_DificultyLevelId] ON [Questions] ([DificultyLevelId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Questions_TopicId] ON [Questions] ([TopicId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_RoleClaims_RoleId] ON [RoleClaims] ([RoleId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [Roles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Student_ApplicationUserId] ON [Student] ([ApplicationUserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Student_LevelId] ON [Student] ([LevelId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_StudentProgresses_TopicId] ON [StudentProgresses] ([TopicId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Subjects_LevelId] ON [Subjects] ([LevelId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Subscriptions_EndDate] ON [Subscriptions] ([EndDate]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Subscriptions_Status] ON [Subscriptions] ([Status]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Subscriptions_StudentId] ON [Subscriptions] ([StudentId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Subscriptions_SubscriptionPlanId] ON [Subscriptions] ([SubscriptionPlanId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Teacher_ApplicationUserId] ON [Teacher] ([ApplicationUserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_TopicPrerequisites_PrerequisitesId] ON [TopicPrerequisites] ([PrerequisitesId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Topics_LessonId] ON [Topics] ([LessonId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_UserClaims_UserId] ON [UserClaims] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_UserLogins_UserId] ON [UserLogins] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_UserOtps_UserId] ON [UserOtps] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_UserRoles_RoleId] ON [UserRoles] ([RoleId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE INDEX [EmailIndex] ON [Users] ([NormalizedEmail]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [Users] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_VariousSkillsViews_VariousSkillId] ON [VariousSkillsViews] ([VariousSkillId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_VideoDownloads_StudentId] ON [VideoDownloads] ([StudentId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_VideoDownloads_VideoId] ON [VideoDownloads] ([VideoId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Videos_LessonId] ON [Videos] ([LessonId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Videos_TopicId] ON [Videos] ([TopicId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_VideoViews_StudentId] ON [VideoViews] ([StudentId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_VideoViews_VideoId] ON [VideoViews] ([VideoId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251228191512_InitialCreate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251228191512_InitialCreate', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229122152_LessonUpdate'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''dbd7013e-e4e2-41fd-a586-e1793282b083'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229122152_LessonUpdate'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''dc146682-ac7d-4e4c-857c-077da52c5eb1'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229122152_LessonUpdate'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''f3b7bf25-ee10-4464-bb12-0ba04c792f98'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229122152_LessonUpdate'
)
BEGIN
    ALTER TABLE [Lessons] ADD [LessonfileName] nvarchar(max) NOT NULL DEFAULT N'';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229122152_LessonUpdate'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] ON;
    EXEC(N'INSERT INTO [subscriptionPlans] ([Id], [CreatedBy], [Deleted], [Description], [DurationInDays], [IsActive], [Name], [PlanType], [Price], [UpdatedAt], [UpdatedBy])
    VALUES (''9ddcbbf2-30cd-4229-a927-922460c0ba28'', NULL, CAST(0 AS bit), N''Basic monthly subscription'', 30, CAST(1 AS bit), N''Basic Monthly'', N''monthly'', 9.99, NULL, NULL),
    (''ac1df104-d6cb-4ced-b192-5fae8dc7907a'', NULL, CAST(0 AS bit), N''Premium monthly subscription'', 30, CAST(1 AS bit), N''Premium Monthly'', N''monthly'', 19.99, NULL, NULL),
    (''c92b0aa9-0d67-4e24-9f6e-f93d6b04ee62'', NULL, CAST(0 AS bit), N''Annual subscription with discount'', 365, CAST(1 AS bit), N''Annual Plan'', N''annual'', 99.99, NULL, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229122152_LessonUpdate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251229122152_LessonUpdate', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229133808_AddLevelAuditColumns'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''9ddcbbf2-30cd-4229-a927-922460c0ba28'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229133808_AddLevelAuditColumns'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''ac1df104-d6cb-4ced-b192-5fae8dc7907a'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229133808_AddLevelAuditColumns'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''c92b0aa9-0d67-4e24-9f6e-f93d6b04ee62'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229133808_AddLevelAuditColumns'
)
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[VideoViews]') AND [c].[name] = N'CreatedBy');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [VideoViews] DROP CONSTRAINT [' + @var0 + '];');
    EXEC(N'UPDATE [VideoViews] SET [CreatedBy] = ''00000000-0000-0000-0000-000000000000'' WHERE [CreatedBy] IS NULL');
    ALTER TABLE [VideoViews] ALTER COLUMN [CreatedBy] uniqueidentifier NOT NULL;
    ALTER TABLE [VideoViews] ADD DEFAULT '00000000-0000-0000-0000-000000000000' FOR [CreatedBy];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229133808_AddLevelAuditColumns'
)
BEGIN
    ALTER TABLE [VideoViews] ADD [CreatedAt] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229133808_AddLevelAuditColumns'
)
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Videos]') AND [c].[name] = N'CreatedBy');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Videos] DROP CONSTRAINT [' + @var1 + '];');
    EXEC(N'UPDATE [Videos] SET [CreatedBy] = ''00000000-0000-0000-0000-000000000000'' WHERE [CreatedBy] IS NULL');
    ALTER TABLE [Videos] ALTER COLUMN [CreatedBy] uniqueidentifier NOT NULL;
    ALTER TABLE [Videos] ADD DEFAULT '00000000-0000-0000-0000-000000000000' FOR [CreatedBy];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229133808_AddLevelAuditColumns'
)
BEGIN
    ALTER TABLE [Videos] ADD [CreatedAt] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229133808_AddLevelAuditColumns'
)
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[VideoDownloads]') AND [c].[name] = N'CreatedBy');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [VideoDownloads] DROP CONSTRAINT [' + @var2 + '];');
    EXEC(N'UPDATE [VideoDownloads] SET [CreatedBy] = ''00000000-0000-0000-0000-000000000000'' WHERE [CreatedBy] IS NULL');
    ALTER TABLE [VideoDownloads] ALTER COLUMN [CreatedBy] uniqueidentifier NOT NULL;
    ALTER TABLE [VideoDownloads] ADD DEFAULT '00000000-0000-0000-0000-000000000000' FOR [CreatedBy];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229133808_AddLevelAuditColumns'
)
BEGIN
    ALTER TABLE [VideoDownloads] ADD [CreatedAt] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229133808_AddLevelAuditColumns'
)
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[VariousSkills]') AND [c].[name] = N'CreatedBy');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [VariousSkills] DROP CONSTRAINT [' + @var3 + '];');
    EXEC(N'UPDATE [VariousSkills] SET [CreatedBy] = ''00000000-0000-0000-0000-000000000000'' WHERE [CreatedBy] IS NULL');
    ALTER TABLE [VariousSkills] ALTER COLUMN [CreatedBy] uniqueidentifier NOT NULL;
    ALTER TABLE [VariousSkills] ADD DEFAULT '00000000-0000-0000-0000-000000000000' FOR [CreatedBy];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229133808_AddLevelAuditColumns'
)
BEGIN
    ALTER TABLE [VariousSkills] ADD [CreatedAt] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229133808_AddLevelAuditColumns'
)
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[UserOtps]') AND [c].[name] = N'CreatedBy');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [UserOtps] DROP CONSTRAINT [' + @var4 + '];');
    EXEC(N'UPDATE [UserOtps] SET [CreatedBy] = ''00000000-0000-0000-0000-000000000000'' WHERE [CreatedBy] IS NULL');
    ALTER TABLE [UserOtps] ALTER COLUMN [CreatedBy] uniqueidentifier NOT NULL;
    ALTER TABLE [UserOtps] ADD DEFAULT '00000000-0000-0000-0000-000000000000' FOR [CreatedBy];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229133808_AddLevelAuditColumns'
)
BEGIN
    ALTER TABLE [UserOtps] ADD [CreatedAt] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229133808_AddLevelAuditColumns'
)
BEGIN
    ALTER TABLE [Topics] ADD [CreatedAt] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229133808_AddLevelAuditColumns'
)
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Teacher]') AND [c].[name] = N'CreatedBy');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Teacher] DROP CONSTRAINT [' + @var5 + '];');
    EXEC(N'UPDATE [Teacher] SET [CreatedBy] = ''00000000-0000-0000-0000-000000000000'' WHERE [CreatedBy] IS NULL');
    ALTER TABLE [Teacher] ALTER COLUMN [CreatedBy] uniqueidentifier NOT NULL;
    ALTER TABLE [Teacher] ADD DEFAULT '00000000-0000-0000-0000-000000000000' FOR [CreatedBy];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229133808_AddLevelAuditColumns'
)
BEGIN
    ALTER TABLE [Teacher] ADD [CreatedAt] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229133808_AddLevelAuditColumns'
)
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Subscriptions]') AND [c].[name] = N'CreatedBy');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Subscriptions] DROP CONSTRAINT [' + @var6 + '];');
    EXEC(N'UPDATE [Subscriptions] SET [CreatedBy] = ''00000000-0000-0000-0000-000000000000'' WHERE [CreatedBy] IS NULL');
    ALTER TABLE [Subscriptions] ALTER COLUMN [CreatedBy] uniqueidentifier NOT NULL;
    ALTER TABLE [Subscriptions] ADD DEFAULT '00000000-0000-0000-0000-000000000000' FOR [CreatedBy];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229133808_AddLevelAuditColumns'
)
BEGIN
    ALTER TABLE [Subscriptions] ADD [CreatedAt] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229133808_AddLevelAuditColumns'
)
BEGIN
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[subscriptionPlans]') AND [c].[name] = N'CreatedBy');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [subscriptionPlans] DROP CONSTRAINT [' + @var7 + '];');
    EXEC(N'UPDATE [subscriptionPlans] SET [CreatedBy] = ''00000000-0000-0000-0000-000000000000'' WHERE [CreatedBy] IS NULL');
    ALTER TABLE [subscriptionPlans] ALTER COLUMN [CreatedBy] uniqueidentifier NOT NULL;
    ALTER TABLE [subscriptionPlans] ADD DEFAULT '00000000-0000-0000-0000-000000000000' FOR [CreatedBy];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229133808_AddLevelAuditColumns'
)
BEGIN
    ALTER TABLE [subscriptionPlans] ADD [CreatedAt] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229133808_AddLevelAuditColumns'
)
BEGIN
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Subjects]') AND [c].[name] = N'CreatedBy');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Subjects] DROP CONSTRAINT [' + @var8 + '];');
    EXEC(N'UPDATE [Subjects] SET [CreatedBy] = ''00000000-0000-0000-0000-000000000000'' WHERE [CreatedBy] IS NULL');
    ALTER TABLE [Subjects] ALTER COLUMN [CreatedBy] uniqueidentifier NOT NULL;
    ALTER TABLE [Subjects] ADD DEFAULT '00000000-0000-0000-0000-000000000000' FOR [CreatedBy];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229133808_AddLevelAuditColumns'
)
BEGIN
    ALTER TABLE [Subjects] ADD [CreatedAt] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229133808_AddLevelAuditColumns'
)
BEGIN
    DECLARE @var9 sysname;
    SELECT @var9 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Student]') AND [c].[name] = N'CreatedBy');
    IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Student] DROP CONSTRAINT [' + @var9 + '];');
    EXEC(N'UPDATE [Student] SET [CreatedBy] = ''00000000-0000-0000-0000-000000000000'' WHERE [CreatedBy] IS NULL');
    ALTER TABLE [Student] ALTER COLUMN [CreatedBy] uniqueidentifier NOT NULL;
    ALTER TABLE [Student] ADD DEFAULT '00000000-0000-0000-0000-000000000000' FOR [CreatedBy];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229133808_AddLevelAuditColumns'
)
BEGIN
    ALTER TABLE [Student] ADD [CreatedAt] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229133808_AddLevelAuditColumns'
)
BEGIN
    DECLARE @var10 sysname;
    SELECT @var10 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Stages]') AND [c].[name] = N'CreatedBy');
    IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [Stages] DROP CONSTRAINT [' + @var10 + '];');
    EXEC(N'UPDATE [Stages] SET [CreatedBy] = ''00000000-0000-0000-0000-000000000000'' WHERE [CreatedBy] IS NULL');
    ALTER TABLE [Stages] ALTER COLUMN [CreatedBy] uniqueidentifier NOT NULL;
    ALTER TABLE [Stages] ADD DEFAULT '00000000-0000-0000-0000-000000000000' FOR [CreatedBy];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229133808_AddLevelAuditColumns'
)
BEGIN
    ALTER TABLE [Stages] ADD [CreatedAt] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229133808_AddLevelAuditColumns'
)
BEGIN
    DECLARE @var11 sysname;
    SELECT @var11 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Questions]') AND [c].[name] = N'CreatedBy');
    IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [Questions] DROP CONSTRAINT [' + @var11 + '];');
    EXEC(N'UPDATE [Questions] SET [CreatedBy] = ''00000000-0000-0000-0000-000000000000'' WHERE [CreatedBy] IS NULL');
    ALTER TABLE [Questions] ALTER COLUMN [CreatedBy] uniqueidentifier NOT NULL;
    ALTER TABLE [Questions] ADD DEFAULT '00000000-0000-0000-0000-000000000000' FOR [CreatedBy];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229133808_AddLevelAuditColumns'
)
BEGIN
    ALTER TABLE [Questions] ADD [CreatedAt] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229133808_AddLevelAuditColumns'
)
BEGIN
    DECLARE @var12 sysname;
    SELECT @var12 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Levels]') AND [c].[name] = N'CreatedBy');
    IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [Levels] DROP CONSTRAINT [' + @var12 + '];');
    EXEC(N'UPDATE [Levels] SET [CreatedBy] = ''00000000-0000-0000-0000-000000000000'' WHERE [CreatedBy] IS NULL');
    ALTER TABLE [Levels] ALTER COLUMN [CreatedBy] uniqueidentifier NOT NULL;
    ALTER TABLE [Levels] ADD DEFAULT '00000000-0000-0000-0000-000000000000' FOR [CreatedBy];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229133808_AddLevelAuditColumns'
)
BEGIN
    ALTER TABLE [Levels] ADD [CreatedAt] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229133808_AddLevelAuditColumns'
)
BEGIN
    ALTER TABLE [Levels] ADD [StageName] nvarchar(max) NOT NULL DEFAULT N'';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229133808_AddLevelAuditColumns'
)
BEGIN
    DECLARE @var13 sysname;
    SELECT @var13 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Lessons]') AND [c].[name] = N'CreatedBy');
    IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [Lessons] DROP CONSTRAINT [' + @var13 + '];');
    EXEC(N'UPDATE [Lessons] SET [CreatedBy] = ''00000000-0000-0000-0000-000000000000'' WHERE [CreatedBy] IS NULL');
    ALTER TABLE [Lessons] ALTER COLUMN [CreatedBy] uniqueidentifier NOT NULL;
    ALTER TABLE [Lessons] ADD DEFAULT '00000000-0000-0000-0000-000000000000' FOR [CreatedBy];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229133808_AddLevelAuditColumns'
)
BEGIN
    ALTER TABLE [Lessons] ADD [CreatedAt] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229133808_AddLevelAuditColumns'
)
BEGIN
    DECLARE @var14 sysname;
    SELECT @var14 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[DificultyLevels]') AND [c].[name] = N'CreatedBy');
    IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [DificultyLevels] DROP CONSTRAINT [' + @var14 + '];');
    EXEC(N'UPDATE [DificultyLevels] SET [CreatedBy] = ''00000000-0000-0000-0000-000000000000'' WHERE [CreatedBy] IS NULL');
    ALTER TABLE [DificultyLevels] ALTER COLUMN [CreatedBy] uniqueidentifier NOT NULL;
    ALTER TABLE [DificultyLevels] ADD DEFAULT '00000000-0000-0000-0000-000000000000' FOR [CreatedBy];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229133808_AddLevelAuditColumns'
)
BEGIN
    ALTER TABLE [DificultyLevels] ADD [CreatedAt] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229133808_AddLevelAuditColumns'
)
BEGIN
    DECLARE @var15 sysname;
    SELECT @var15 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Calenders]') AND [c].[name] = N'CreatedBy');
    IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [Calenders] DROP CONSTRAINT [' + @var15 + '];');
    EXEC(N'UPDATE [Calenders] SET [CreatedBy] = ''00000000-0000-0000-0000-000000000000'' WHERE [CreatedBy] IS NULL');
    ALTER TABLE [Calenders] ALTER COLUMN [CreatedBy] uniqueidentifier NOT NULL;
    ALTER TABLE [Calenders] ADD DEFAULT '00000000-0000-0000-0000-000000000000' FOR [CreatedBy];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229133808_AddLevelAuditColumns'
)
BEGIN
    ALTER TABLE [Calenders] ADD [CreatedAt] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229133808_AddLevelAuditColumns'
)
BEGIN
    DECLARE @var16 sysname;
    SELECT @var16 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Banners]') AND [c].[name] = N'CreatedBy');
    IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [Banners] DROP CONSTRAINT [' + @var16 + '];');
    EXEC(N'UPDATE [Banners] SET [CreatedBy] = ''00000000-0000-0000-0000-000000000000'' WHERE [CreatedBy] IS NULL');
    ALTER TABLE [Banners] ALTER COLUMN [CreatedBy] uniqueidentifier NOT NULL;
    ALTER TABLE [Banners] ADD DEFAULT '00000000-0000-0000-0000-000000000000' FOR [CreatedBy];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229133808_AddLevelAuditColumns'
)
BEGIN
    ALTER TABLE [Banners] ADD [CreatedAt] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229133808_AddLevelAuditColumns'
)
BEGIN
    DECLARE @var17 sysname;
    SELECT @var17 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Attachments]') AND [c].[name] = N'CreatedBy');
    IF @var17 IS NOT NULL EXEC(N'ALTER TABLE [Attachments] DROP CONSTRAINT [' + @var17 + '];');
    EXEC(N'UPDATE [Attachments] SET [CreatedBy] = ''00000000-0000-0000-0000-000000000000'' WHERE [CreatedBy] IS NULL');
    ALTER TABLE [Attachments] ALTER COLUMN [CreatedBy] uniqueidentifier NOT NULL;
    ALTER TABLE [Attachments] ADD DEFAULT '00000000-0000-0000-0000-000000000000' FOR [CreatedBy];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229133808_AddLevelAuditColumns'
)
BEGIN
    ALTER TABLE [Attachments] ADD [CreatedAt] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229133808_AddLevelAuditColumns'
)
BEGIN
    DECLARE @var18 sysname;
    SELECT @var18 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Assessments]') AND [c].[name] = N'CreatedBy');
    IF @var18 IS NOT NULL EXEC(N'ALTER TABLE [Assessments] DROP CONSTRAINT [' + @var18 + '];');
    EXEC(N'UPDATE [Assessments] SET [CreatedBy] = ''00000000-0000-0000-0000-000000000000'' WHERE [CreatedBy] IS NULL');
    ALTER TABLE [Assessments] ALTER COLUMN [CreatedBy] uniqueidentifier NOT NULL;
    ALTER TABLE [Assessments] ADD DEFAULT '00000000-0000-0000-0000-000000000000' FOR [CreatedBy];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229133808_AddLevelAuditColumns'
)
BEGIN
    ALTER TABLE [Assessments] ADD [CreatedAt] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229133808_AddLevelAuditColumns'
)
BEGIN
    DECLARE @var19 sysname;
    SELECT @var19 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Answers]') AND [c].[name] = N'CreatedBy');
    IF @var19 IS NOT NULL EXEC(N'ALTER TABLE [Answers] DROP CONSTRAINT [' + @var19 + '];');
    EXEC(N'UPDATE [Answers] SET [CreatedBy] = ''00000000-0000-0000-0000-000000000000'' WHERE [CreatedBy] IS NULL');
    ALTER TABLE [Answers] ALTER COLUMN [CreatedBy] uniqueidentifier NOT NULL;
    ALTER TABLE [Answers] ADD DEFAULT '00000000-0000-0000-0000-000000000000' FOR [CreatedBy];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229133808_AddLevelAuditColumns'
)
BEGIN
    ALTER TABLE [Answers] ADD [CreatedAt] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229133808_AddLevelAuditColumns'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] ON;
    EXEC(N'INSERT INTO [subscriptionPlans] ([Id], [CreatedAt], [CreatedBy], [Deleted], [Description], [DurationInDays], [IsActive], [Name], [PlanType], [Price], [UpdatedAt], [UpdatedBy])
    VALUES (''878dbca3-0ba1-49d0-ae03-372b6f1eab5d'', ''2025-12-29T15:38:06.5249584+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Premium monthly subscription'', 30, CAST(1 AS bit), N''Premium Monthly'', N''monthly'', 19.99, NULL, NULL),
    (''97138c3a-f890-4f34-a20e-947233c70215'', ''2025-12-29T15:38:06.5249624+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Annual subscription with discount'', 365, CAST(1 AS bit), N''Annual Plan'', N''annual'', 99.99, NULL, NULL),
    (''d71ef31c-c0d0-4f67-9015-e4e23e1aaee2'', ''2025-12-29T15:38:06.5221324+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Basic monthly subscription'', 30, CAST(1 AS bit), N''Basic Monthly'', N''monthly'', 9.99, NULL, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251229133808_AddLevelAuditColumns'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251229133808_AddLevelAuditColumns', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251230101338_KoKoandS'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''878dbca3-0ba1-49d0-ae03-372b6f1eab5d'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251230101338_KoKoandS'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''97138c3a-f890-4f34-a20e-947233c70215'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251230101338_KoKoandS'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''d71ef31c-c0d0-4f67-9015-e4e23e1aaee2'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251230101338_KoKoandS'
)
BEGIN
    DECLARE @var20 sysname;
    SELECT @var20 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Levels]') AND [c].[name] = N'StageName');
    IF @var20 IS NOT NULL EXEC(N'ALTER TABLE [Levels] DROP CONSTRAINT [' + @var20 + '];');
    ALTER TABLE [Levels] ALTER COLUMN [StageName] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251230101338_KoKoandS'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] ON;
    EXEC(N'INSERT INTO [subscriptionPlans] ([Id], [CreatedAt], [CreatedBy], [Deleted], [Description], [DurationInDays], [IsActive], [Name], [PlanType], [Price], [UpdatedAt], [UpdatedBy])
    VALUES (''2abee02e-45d8-41fb-be6d-8cc3ced28a89'', ''2025-12-30T12:13:36.6439971+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Basic monthly subscription'', 30, CAST(1 AS bit), N''Basic Monthly'', N''monthly'', 9.99, NULL, NULL),
    (''a80051b9-5fd6-429e-b9d1-c6214e7b8815'', ''2025-12-30T12:13:36.6468437+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Annual subscription with discount'', 365, CAST(1 AS bit), N''Annual Plan'', N''annual'', 99.99, NULL, NULL),
    (''ac80dc25-37de-49f3-a6b4-dd9bc5c8a215'', ''2025-12-30T12:13:36.6468394+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Premium monthly subscription'', 30, CAST(1 AS bit), N''Premium Monthly'', N''monthly'', 19.99, NULL, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251230101338_KoKoandS'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251230101338_KoKoandS', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251230102101_Sob7anAlah'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''2abee02e-45d8-41fb-be6d-8cc3ced28a89'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251230102101_Sob7anAlah'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''a80051b9-5fd6-429e-b9d1-c6214e7b8815'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251230102101_Sob7anAlah'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''ac80dc25-37de-49f3-a6b4-dd9bc5c8a215'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251230102101_Sob7anAlah'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] ON;
    EXEC(N'INSERT INTO [subscriptionPlans] ([Id], [CreatedAt], [CreatedBy], [Deleted], [Description], [DurationInDays], [IsActive], [Name], [PlanType], [Price], [UpdatedAt], [UpdatedBy])
    VALUES (''5530ad50-d924-460a-8a73-489c6fca97c0'', ''2025-12-30T12:21:00.2366371+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Basic monthly subscription'', 30, CAST(1 AS bit), N''Basic Monthly'', N''monthly'', 9.99, NULL, NULL),
    (''7c4e3b13-75d4-4df8-a35c-5dc2fc47bbd1'', ''2025-12-30T12:21:00.2400026+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Annual subscription with discount'', 365, CAST(1 AS bit), N''Annual Plan'', N''annual'', 99.99, NULL, NULL),
    (''a90d0b33-6785-4ea7-8ac9-9a6eccd99256'', ''2025-12-30T12:21:00.2399958+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Premium monthly subscription'', 30, CAST(1 AS bit), N''Premium Monthly'', N''monthly'', 19.99, NULL, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251230102101_Sob7anAlah'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251230102101_Sob7anAlah', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251230120225_lklaskdlad'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''5530ad50-d924-460a-8a73-489c6fca97c0'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251230120225_lklaskdlad'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''7c4e3b13-75d4-4df8-a35c-5dc2fc47bbd1'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251230120225_lklaskdlad'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''a90d0b33-6785-4ea7-8ac9-9a6eccd99256'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251230120225_lklaskdlad'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] ON;
    EXEC(N'INSERT INTO [subscriptionPlans] ([Id], [CreatedAt], [CreatedBy], [Deleted], [Description], [DurationInDays], [IsActive], [Name], [PlanType], [Price], [UpdatedAt], [UpdatedBy])
    VALUES (''c8052a50-7aa4-4e51-9d6c-c84f05338983'', ''2025-12-30T14:02:24.4446207+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Premium monthly subscription'', 30, CAST(1 AS bit), N''Premium Monthly'', N''monthly'', 19.99, NULL, NULL),
    (''dfdcc957-809f-4e1e-a1f5-dbc0de6491d5'', ''2025-12-30T14:02:24.4446268+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Annual subscription with discount'', 365, CAST(1 AS bit), N''Annual Plan'', N''annual'', 99.99, NULL, NULL),
    (''ee7fa4d2-d2ba-461c-a33b-e4a72406b3d6'', ''2025-12-30T14:02:24.4387642+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Basic monthly subscription'', 30, CAST(1 AS bit), N''Basic Monthly'', N''monthly'', 9.99, NULL, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251230120225_lklaskdlad'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251230120225_lklaskdlad', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251230134852_jhasjdhakjshdadsasss'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''c8052a50-7aa4-4e51-9d6c-c84f05338983'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251230134852_jhasjdhakjshdadsasss'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''dfdcc957-809f-4e1e-a1f5-dbc0de6491d5'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251230134852_jhasjdhakjshdadsasss'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''ee7fa4d2-d2ba-461c-a33b-e4a72406b3d6'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251230134852_jhasjdhakjshdadsasss'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] ON;
    EXEC(N'INSERT INTO [subscriptionPlans] ([Id], [CreatedAt], [CreatedBy], [Deleted], [Description], [DurationInDays], [IsActive], [Name], [PlanType], [Price], [UpdatedAt], [UpdatedBy])
    VALUES (''3ba7ddf7-f9e3-40c1-9549-acfeb38d79b0'', ''2025-12-30T15:48:51.2917869+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Annual subscription with discount'', 365, CAST(1 AS bit), N''Annual Plan'', N''annual'', 99.99, NULL, NULL),
    (''43195e8a-d2f8-4d8d-be05-d16029de665c'', ''2025-12-30T15:48:51.2849495+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Basic monthly subscription'', 30, CAST(1 AS bit), N''Basic Monthly'', N''monthly'', 9.99, NULL, NULL),
    (''57bc1e49-d0e2-4e08-a886-156132587aaf'', ''2025-12-30T15:48:51.2917781+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Premium monthly subscription'', 30, CAST(1 AS bit), N''Premium Monthly'', N''monthly'', 19.99, NULL, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251230134852_jhasjdhakjshdadsasss'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251230134852_jhasjdhakjshdadsasss', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251230222708_RemoveTeacherIdfromteacher'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''3ba7ddf7-f9e3-40c1-9549-acfeb38d79b0'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251230222708_RemoveTeacherIdfromteacher'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''43195e8a-d2f8-4d8d-be05-d16029de665c'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251230222708_RemoveTeacherIdfromteacher'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''57bc1e49-d0e2-4e08-a886-156132587aaf'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251230222708_RemoveTeacherIdfromteacher'
)
BEGIN
    DECLARE @var21 sysname;
    SELECT @var21 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Teacher]') AND [c].[name] = N'TeacherID');
    IF @var21 IS NOT NULL EXEC(N'ALTER TABLE [Teacher] DROP CONSTRAINT [' + @var21 + '];');
    ALTER TABLE [Teacher] DROP COLUMN [TeacherID];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251230222708_RemoveTeacherIdfromteacher'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] ON;
    EXEC(N'INSERT INTO [subscriptionPlans] ([Id], [CreatedAt], [CreatedBy], [Deleted], [Description], [DurationInDays], [IsActive], [Name], [PlanType], [Price], [UpdatedAt], [UpdatedBy])
    VALUES (''43209994-41d0-4d64-b5bf-3699ec5ff22c'', ''2025-12-31T00:27:07.4095731+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Basic monthly subscription'', 30, CAST(1 AS bit), N''Basic Monthly'', N''monthly'', 9.99, NULL, NULL),
    (''4cdeaaee-cc91-4d95-84fe-5a67770aca93'', ''2025-12-31T00:27:07.4174015+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Annual subscription with discount'', 365, CAST(1 AS bit), N''Annual Plan'', N''annual'', 99.99, NULL, NULL),
    (''56f089dd-a447-4df5-8039-db38809a454c'', ''2025-12-31T00:27:07.4173908+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Premium monthly subscription'', 30, CAST(1 AS bit), N''Premium Monthly'', N''monthly'', 19.99, NULL, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251230222708_RemoveTeacherIdfromteacher'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251230222708_RemoveTeacherIdfromteacher', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260101062438_jjhasjhd'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''43209994-41d0-4d64-b5bf-3699ec5ff22c'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260101062438_jjhasjhd'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''4cdeaaee-cc91-4d95-84fe-5a67770aca93'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260101062438_jjhasjhd'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''56f089dd-a447-4df5-8039-db38809a454c'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260101062438_jjhasjhd'
)
BEGIN
    DECLARE @var22 sysname;
    SELECT @var22 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Banners]') AND [c].[name] = N'ImageName');
    IF @var22 IS NOT NULL EXEC(N'ALTER TABLE [Banners] DROP CONSTRAINT [' + @var22 + '];');
    ALTER TABLE [Banners] ALTER COLUMN [ImageName] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260101062438_jjhasjhd'
)
BEGIN
    ALTER TABLE [Banners] ADD [Title] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260101062438_jjhasjhd'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] ON;
    EXEC(N'INSERT INTO [subscriptionPlans] ([Id], [CreatedAt], [CreatedBy], [Deleted], [Description], [DurationInDays], [IsActive], [Name], [PlanType], [Price], [UpdatedAt], [UpdatedBy])
    VALUES (''a35ae9a9-e91d-4cd0-b01d-923ac9e5e42c'', ''2026-01-01T08:24:36.7808976+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Premium monthly subscription'', 30, CAST(1 AS bit), N''Premium Monthly'', N''monthly'', 19.99, NULL, NULL),
    (''c87c0a1b-f182-4bd2-8138-6c8f3bbbe28e'', ''2026-01-01T08:24:36.7809068+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Annual subscription with discount'', 365, CAST(1 AS bit), N''Annual Plan'', N''annual'', 99.99, NULL, NULL),
    (''fdf63b38-2bfd-43d4-bd79-8b9bf53ddab7'', ''2026-01-01T08:24:36.7773449+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Basic monthly subscription'', 30, CAST(1 AS bit), N''Basic Monthly'', N''monthly'', 9.99, NULL, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260101062438_jjhasjhd'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260101062438_jjhasjhd', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260101065344_StageFinal'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''a35ae9a9-e91d-4cd0-b01d-923ac9e5e42c'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260101065344_StageFinal'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''c87c0a1b-f182-4bd2-8138-6c8f3bbbe28e'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260101065344_StageFinal'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''fdf63b38-2bfd-43d4-bd79-8b9bf53ddab7'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260101065344_StageFinal'
)
BEGIN
    DECLARE @var23 sysname;
    SELECT @var23 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Stages]') AND [c].[name] = N'Type');
    IF @var23 IS NOT NULL EXEC(N'ALTER TABLE [Stages] DROP CONSTRAINT [' + @var23 + '];');
    ALTER TABLE [Stages] ALTER COLUMN [Type] int NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260101065344_StageFinal'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] ON;
    EXEC(N'INSERT INTO [subscriptionPlans] ([Id], [CreatedAt], [CreatedBy], [Deleted], [Description], [DurationInDays], [IsActive], [Name], [PlanType], [Price], [UpdatedAt], [UpdatedBy])
    VALUES (''57e5e004-c6e7-43b8-baa9-7b08b6f71c81'', ''2026-01-01T08:53:42.7415931+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Premium monthly subscription'', 30, CAST(1 AS bit), N''Premium Monthly'', N''monthly'', 19.99, NULL, NULL),
    (''78a10bde-e91e-4305-a1ef-71aa01bfc105'', ''2026-01-01T08:53:42.7416045+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Annual subscription with discount'', 365, CAST(1 AS bit), N''Annual Plan'', N''annual'', 99.99, NULL, NULL),
    (''f18997a4-4d6e-4795-81c3-8f989c91f991'', ''2026-01-01T08:53:42.7370574+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Basic monthly subscription'', 30, CAST(1 AS bit), N''Basic Monthly'', N''monthly'', 9.99, NULL, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260101065344_StageFinal'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260101065344_StageFinal', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260101111951_kkkkkkkkkkkkk'
)
BEGIN
    ALTER TABLE [VariousSkillsViews] DROP CONSTRAINT [FK_VariousSkillsViews_Users_UserId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260101111951_kkkkkkkkkkkkk'
)
BEGIN
    ALTER TABLE [VariousSkillsViews] DROP CONSTRAINT [FK_VariousSkillsViews_VariousSkills_VariousSkillId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260101111951_kkkkkkkkkkkkk'
)
BEGIN
    ALTER TABLE [VariousSkillsViews] DROP CONSTRAINT [PK_VariousSkillsViews];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260101111951_kkkkkkkkkkkkk'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''57e5e004-c6e7-43b8-baa9-7b08b6f71c81'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260101111951_kkkkkkkkkkkkk'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''78a10bde-e91e-4305-a1ef-71aa01bfc105'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260101111951_kkkkkkkkkkkkk'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''f18997a4-4d6e-4795-81c3-8f989c91f991'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260101111951_kkkkkkkkkkkkk'
)
BEGIN
    EXEC sp_rename N'[VariousSkillsViews]', N'VariousSkillView', 'OBJECT';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260101111951_kkkkkkkkkkkkk'
)
BEGIN
    EXEC sp_rename N'[VariousSkillView].[IX_VariousSkillsViews_VariousSkillId]', N'IX_VariousSkillView_VariousSkillId', 'INDEX';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260101111951_kkkkkkkkkkkkk'
)
BEGIN
    ALTER TABLE [VariousSkillView] ADD CONSTRAINT [PK_VariousSkillView] PRIMARY KEY ([UserId], [VariousSkillId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260101111951_kkkkkkkkkkkkk'
)
BEGIN
    CREATE TABLE [Skills] (
        [Id] uniqueidentifier NOT NULL,
        [VideoURL] nvarchar(max) NOT NULL,
        [Title] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [ViewsCount] int NOT NULL,
        [CreatedAt] datetime2 NULL,
        [UpdatedAt] datetime2 NULL,
        [CreatedBy] uniqueidentifier NOT NULL,
        [UpdatedBy] uniqueidentifier NULL,
        [Deleted] bit NOT NULL,
        CONSTRAINT [PK_Skills] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260101111951_kkkkkkkkkkkkk'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] ON;
    EXEC(N'INSERT INTO [subscriptionPlans] ([Id], [CreatedAt], [CreatedBy], [Deleted], [Description], [DurationInDays], [IsActive], [Name], [PlanType], [Price], [UpdatedAt], [UpdatedBy])
    VALUES (''3956f135-a395-4f94-be9e-62b6a179343e'', ''2026-01-01T13:19:50.2444875+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Basic monthly subscription'', 30, CAST(1 AS bit), N''Basic Monthly'', N''monthly'', 9.99, NULL, NULL),
    (''552f04dc-43be-4cf2-9869-c37e0181921c'', ''2026-01-01T13:19:50.2465937+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Premium monthly subscription'', 30, CAST(1 AS bit), N''Premium Monthly'', N''monthly'', 19.99, NULL, NULL),
    (''eaa3260e-3ed2-4833-9354-afec16eda0be'', ''2026-01-01T13:19:50.2465972+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Annual subscription with discount'', 365, CAST(1 AS bit), N''Annual Plan'', N''annual'', 99.99, NULL, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260101111951_kkkkkkkkkkkkk'
)
BEGIN
    ALTER TABLE [VariousSkillView] ADD CONSTRAINT [FK_VariousSkillView_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260101111951_kkkkkkkkkkkkk'
)
BEGIN
    ALTER TABLE [VariousSkillView] ADD CONSTRAINT [FK_VariousSkillView_VariousSkills_VariousSkillId] FOREIGN KEY ([VariousSkillId]) REFERENCES [VariousSkills] ([Id]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260101111951_kkkkkkkkkkkkk'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260101111951_kkkkkkkkkkkkk', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260104124119_Remove_Title_from_entity'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''3956f135-a395-4f94-be9e-62b6a179343e'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260104124119_Remove_Title_from_entity'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''552f04dc-43be-4cf2-9869-c37e0181921c'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260104124119_Remove_Title_from_entity'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''eaa3260e-3ed2-4833-9354-afec16eda0be'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260104124119_Remove_Title_from_entity'
)
BEGIN
    DECLARE @var24 sysname;
    SELECT @var24 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Banners]') AND [c].[name] = N'Title');
    IF @var24 IS NOT NULL EXEC(N'ALTER TABLE [Banners] DROP CONSTRAINT [' + @var24 + '];');
    ALTER TABLE [Banners] DROP COLUMN [Title];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260104124119_Remove_Title_from_entity'
)
BEGIN
    DECLARE @var25 sysname;
    SELECT @var25 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Stages]') AND [c].[name] = N'ImageName');
    IF @var25 IS NOT NULL EXEC(N'ALTER TABLE [Stages] DROP CONSTRAINT [' + @var25 + '];');
    ALTER TABLE [Stages] ALTER COLUMN [ImageName] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260104124119_Remove_Title_from_entity'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] ON;
    EXEC(N'INSERT INTO [subscriptionPlans] ([Id], [CreatedAt], [CreatedBy], [Deleted], [Description], [DurationInDays], [IsActive], [Name], [PlanType], [Price], [UpdatedAt], [UpdatedBy])
    VALUES (''650b45fb-743e-4c2d-846f-57178a2ab2ad'', ''2026-01-04T14:41:19.0457994+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Annual subscription with discount'', 365, CAST(1 AS bit), N''Annual Plan'', N''annual'', 99.99, NULL, NULL),
    (''a7463ec5-45a1-4783-8633-a5140dc6e5da'', ''2026-01-04T14:41:19.0437918+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Basic monthly subscription'', 30, CAST(1 AS bit), N''Basic Monthly'', N''monthly'', 9.99, NULL, NULL),
    (''df39759f-ca78-4e63-9171-9ddad0d8a83e'', ''2026-01-04T14:41:19.0457966+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Premium monthly subscription'', 30, CAST(1 AS bit), N''Premium Monthly'', N''monthly'', 19.99, NULL, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260104124119_Remove_Title_from_entity'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260104124119_Remove_Title_from_entity', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260104130021_StageCascadeDelete'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''650b45fb-743e-4c2d-846f-57178a2ab2ad'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260104130021_StageCascadeDelete'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''a7463ec5-45a1-4783-8633-a5140dc6e5da'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260104130021_StageCascadeDelete'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''df39759f-ca78-4e63-9171-9ddad0d8a83e'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260104130021_StageCascadeDelete'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] ON;
    EXEC(N'INSERT INTO [subscriptionPlans] ([Id], [CreatedAt], [CreatedBy], [Deleted], [Description], [DurationInDays], [IsActive], [Name], [PlanType], [Price], [UpdatedAt], [UpdatedBy])
    VALUES (''a3a872b2-cdaa-49f8-8006-896dd10d34ce'', ''2026-01-04T15:00:20.6390282+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Premium monthly subscription'', 30, CAST(1 AS bit), N''Premium Monthly'', N''monthly'', 19.99, NULL, NULL),
    (''e4f7740e-7c5c-4d21-ad51-5e34c3934f27'', ''2026-01-04T15:00:20.6369092+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Basic monthly subscription'', 30, CAST(1 AS bit), N''Basic Monthly'', N''monthly'', 9.99, NULL, NULL),
    (''e87754cc-f9e1-4f85-b597-d0dfb33d2aaf'', ''2026-01-04T15:00:20.6390314+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Annual subscription with discount'', 365, CAST(1 AS bit), N''Annual Plan'', N''annual'', 99.99, NULL, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260104130021_StageCascadeDelete'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260104130021_StageCascadeDelete', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260104142359_AllyDeletewithCascade'
)
BEGIN
    ALTER TABLE [Student] DROP CONSTRAINT [FK_Student_Levels_LevelId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260104142359_AllyDeletewithCascade'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''a3a872b2-cdaa-49f8-8006-896dd10d34ce'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260104142359_AllyDeletewithCascade'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''e4f7740e-7c5c-4d21-ad51-5e34c3934f27'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260104142359_AllyDeletewithCascade'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''e87754cc-f9e1-4f85-b597-d0dfb33d2aaf'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260104142359_AllyDeletewithCascade'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] ON;
    EXEC(N'INSERT INTO [subscriptionPlans] ([Id], [CreatedAt], [CreatedBy], [Deleted], [Description], [DurationInDays], [IsActive], [Name], [PlanType], [Price], [UpdatedAt], [UpdatedBy])
    VALUES (''009ee745-6861-46ee-a60b-6d39ac1042a9'', ''2026-01-04T16:23:58.1758887+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Basic monthly subscription'', 30, CAST(1 AS bit), N''Basic Monthly'', N''monthly'', 9.99, NULL, NULL),
    (''03fd35a7-b165-4c50-b552-c63bed1a242b'', ''2026-01-04T16:23:58.1801955+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Annual subscription with discount'', 365, CAST(1 AS bit), N''Annual Plan'', N''annual'', 99.99, NULL, NULL),
    (''c667ffc2-85ec-4010-9556-487ccaa30df7'', ''2026-01-04T16:23:58.1801893+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Premium monthly subscription'', 30, CAST(1 AS bit), N''Premium Monthly'', N''monthly'', 19.99, NULL, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260104142359_AllyDeletewithCascade'
)
BEGIN
    ALTER TABLE [Student] ADD CONSTRAINT [FK_Student_Levels_LevelId] FOREIGN KEY ([LevelId]) REFERENCES [Levels] ([Id]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260104142359_AllyDeletewithCascade'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260104142359_AllyDeletewithCascade', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260104153031_jahsdkjhaskjdhad'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''009ee745-6861-46ee-a60b-6d39ac1042a9'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260104153031_jahsdkjhaskjdhad'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''03fd35a7-b165-4c50-b552-c63bed1a242b'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260104153031_jahsdkjhaskjdhad'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''c667ffc2-85ec-4010-9556-487ccaa30df7'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260104153031_jahsdkjhaskjdhad'
)
BEGIN
    ALTER TABLE [Levels] ADD [ImageName] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260104153031_jahsdkjhaskjdhad'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] ON;
    EXEC(N'INSERT INTO [subscriptionPlans] ([Id], [CreatedAt], [CreatedBy], [Deleted], [Description], [DurationInDays], [IsActive], [Name], [PlanType], [Price], [UpdatedAt], [UpdatedBy])
    VALUES (''1706193a-870d-4d9f-8f7c-2461269ff9de'', ''2026-01-04T17:30:30.8138809+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Premium monthly subscription'', 30, CAST(1 AS bit), N''Premium Monthly'', N''monthly'', 19.99, NULL, NULL),
    (''3f5cc1b4-d993-4f1f-ae7d-cacfe69d32c2'', ''2026-01-04T17:30:30.8138853+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Annual subscription with discount'', 365, CAST(1 AS bit), N''Annual Plan'', N''annual'', 99.99, NULL, NULL),
    (''51083dd1-61da-48eb-b97f-ab4672f85edb'', ''2026-01-04T17:30:30.8119067+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Basic monthly subscription'', 30, CAST(1 AS bit), N''Basic Monthly'', N''monthly'', 9.99, NULL, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260104153031_jahsdkjhaskjdhad'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260104153031_jahsdkjhaskjdhad', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260106124540_InitialClean'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''1706193a-870d-4d9f-8f7c-2461269ff9de'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260106124540_InitialClean'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''3f5cc1b4-d993-4f1f-ae7d-cacfe69d32c2'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260106124540_InitialClean'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''51083dd1-61da-48eb-b97f-ab4672f85edb'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260106124540_InitialClean'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] ON;
    EXEC(N'INSERT INTO [subscriptionPlans] ([Id], [CreatedAt], [CreatedBy], [Deleted], [Description], [DurationInDays], [IsActive], [Name], [PlanType], [Price], [UpdatedAt], [UpdatedBy])
    VALUES (''3bb6a146-34a5-4063-ad3b-dc305274e6f1'', ''2026-01-06T14:45:38.9071491+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Basic monthly subscription'', 30, CAST(1 AS bit), N''Basic Monthly'', N''monthly'', 9.99, NULL, NULL),
    (''3ce98836-deba-482a-bd2b-331f55e79952'', ''2026-01-06T14:45:38.9103745+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Annual subscription with discount'', 365, CAST(1 AS bit), N''Annual Plan'', N''annual'', 99.99, NULL, NULL),
    (''a262ba82-e96d-40a6-ba74-0d2d3ee1d69c'', ''2026-01-06T14:45:38.9103701+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Premium monthly subscription'', 30, CAST(1 AS bit), N''Premium Monthly'', N''monthly'', 19.99, NULL, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260106124540_InitialClean'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260106124540_InitialClean', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260106151043_kjslkdjlkajsdkss'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''3bb6a146-34a5-4063-ad3b-dc305274e6f1'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260106151043_kjslkdjlkajsdkss'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''3ce98836-deba-482a-bd2b-331f55e79952'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260106151043_kjslkdjlkajsdkss'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''a262ba82-e96d-40a6-ba74-0d2d3ee1d69c'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260106151043_kjslkdjlkajsdkss'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] ON;
    EXEC(N'INSERT INTO [subscriptionPlans] ([Id], [CreatedAt], [CreatedBy], [Deleted], [Description], [DurationInDays], [IsActive], [Name], [PlanType], [Price], [UpdatedAt], [UpdatedBy])
    VALUES (''1ab196b5-a035-43ec-a9b4-95df4bd638bf'', ''2026-01-06T17:10:42.3453332+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Basic monthly subscription'', 30, CAST(1 AS bit), N''Basic Monthly'', N''monthly'', 9.99, NULL, NULL),
    (''b189ff6d-f0b8-423d-9f85-4689295bf2d4'', ''2026-01-06T17:10:42.3486015+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Premium monthly subscription'', 30, CAST(1 AS bit), N''Premium Monthly'', N''monthly'', 19.99, NULL, NULL),
    (''c50aa3d5-7070-48a8-b992-244f99ca5d3e'', ''2026-01-06T17:10:42.3486059+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Annual subscription with discount'', 365, CAST(1 AS bit), N''Annual Plan'', N''annual'', 99.99, NULL, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260106151043_kjslkdjlkajsdkss'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260106151043_kjslkdjlkajsdkss', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107113106_VideoViewDurationUpdate'
)
BEGIN
    ALTER TABLE [VideoViews] ADD [Duration] time NOT NULL DEFAULT '00:00:00';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107113106_VideoViewDurationUpdate'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] ON;
    EXEC(N'INSERT INTO [subscriptionPlans] ([Id], [CreatedAt], [CreatedBy], [Deleted], [Description], [DurationInDays], [IsActive], [Name], [PlanType], [Price], [UpdatedAt], [UpdatedBy])
    VALUES (''2d3312b0-10f7-4ac9-98b3-8a26e8bd6730'', ''2026-01-07T13:31:05.1519189+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Premium monthly subscription'', 30, CAST(1 AS bit), N''Premium Monthly'', N''monthly'', 19.99, NULL, NULL),
    (''54a56688-539c-45da-be52-d283823de939'', ''2026-01-07T13:31:05.1519278+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Annual subscription with discount'', 365, CAST(1 AS bit), N''Annual Plan'', N''annual'', 99.99, NULL, NULL),
    (''5b54acd4-7dc5-4f29-a385-5efe2077d9b6'', ''2026-01-07T13:31:05.1495514+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Basic monthly subscription'', 30, CAST(1 AS bit), N''Basic Monthly'', N''monthly'', 9.99, NULL, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107113106_VideoViewDurationUpdate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260107113106_VideoViewDurationUpdate', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107133453_SubjectIdinTopicTable'
)
BEGIN
    EXEC sp_rename N'[Topics].[LessonId]', N'SubjectId', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107133453_SubjectIdinTopicTable'
)
BEGIN
    EXEC sp_rename N'[Topics].[IX_Topics_LessonId]', N'IX_Topics_SubjectId', 'INDEX';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107133453_SubjectIdinTopicTable'
)
BEGIN
    ALTER TABLE [Lessons] ADD [TopicId] uniqueidentifier NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107133453_SubjectIdinTopicTable'
)
BEGIN
    CREATE INDEX [IX_Lessons_TopicId] ON [Lessons] ([TopicId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107133453_SubjectIdinTopicTable'
)
BEGIN
    ALTER TABLE [Lessons] ADD CONSTRAINT [FK_Lessons_Topics_TopicId] FOREIGN KEY ([TopicId]) REFERENCES [Topics] ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107133453_SubjectIdinTopicTable'
)
BEGIN
    ALTER TABLE [Topics] ADD CONSTRAINT [FK_Topics_Subjects_SubjectId] FOREIGN KEY ([SubjectId]) REFERENCES [Subjects] ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107133453_SubjectIdinTopicTable'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260107133453_SubjectIdinTopicTable', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107133917_jjjsjjjsjjjaa'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] ON;
    EXEC(N'INSERT INTO [subscriptionPlans] ([Id], [CreatedAt], [CreatedBy], [Deleted], [Description], [DurationInDays], [IsActive], [Name], [PlanType], [Price], [UpdatedAt], [UpdatedBy])
    VALUES (''2d66a9bc-6e24-426a-9a32-6073a67ba805'', ''2026-01-07T15:39:16.6628724+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Annual subscription with discount'', 365, CAST(1 AS bit), N''Annual Plan'', N''annual'', 99.99, NULL, NULL),
    (''a9cc58ac-bc9f-4380-be5a-8093ed5cf32a'', ''2026-01-07T15:39:16.6628667+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Premium monthly subscription'', 30, CAST(1 AS bit), N''Premium Monthly'', N''monthly'', 19.99, NULL, NULL),
    (''ecfa0ac1-3793-4ebf-b0cf-eebfed7ce5d6'', ''2026-01-07T15:39:16.6593365+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Basic monthly subscription'', 30, CAST(1 AS bit), N''Basic Monthly'', N''monthly'', 9.99, NULL, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107133917_jjjsjjjsjjjaa'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260107133917_jjjsjjjsjjjaa', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107134532_jjjsjjaas'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] ON;
    EXEC(N'INSERT INTO [subscriptionPlans] ([Id], [CreatedAt], [CreatedBy], [Deleted], [Description], [DurationInDays], [IsActive], [Name], [PlanType], [Price], [UpdatedAt], [UpdatedBy])
    VALUES (''6b92de7f-fc65-495c-94b8-910a60e54a22'', ''2026-01-07T15:45:31.2635742+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Annual subscription with discount'', 365, CAST(1 AS bit), N''Annual Plan'', N''annual'', 99.99, NULL, NULL),
    (''ac041a83-539f-4140-a759-9c0fb2eb5326'', ''2026-01-07T15:45:31.2635708+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Premium monthly subscription'', 30, CAST(1 AS bit), N''Premium Monthly'', N''monthly'', 19.99, NULL, NULL),
    (''e912d884-b468-40b8-a96e-5b1d64c751e1'', ''2026-01-07T15:45:31.2607105+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Basic monthly subscription'', 30, CAST(1 AS bit), N''Basic Monthly'', N''monthly'', 9.99, NULL, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107134532_jjjsjjaas'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260107134532_jjjsjjaas', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107135200_newnewnew'
)
BEGIN
    ALTER TABLE [Lessons] DROP CONSTRAINT [FK_Lessons_Subjects_SubjectId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107135200_newnewnew'
)
BEGIN
    ALTER TABLE [Lessons] DROP CONSTRAINT [FK_Lessons_Teacher_TeacherId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107135200_newnewnew'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''6b92de7f-fc65-495c-94b8-910a60e54a22'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107135200_newnewnew'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''ac041a83-539f-4140-a759-9c0fb2eb5326'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107135200_newnewnew'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''e912d884-b468-40b8-a96e-5b1d64c751e1'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107135200_newnewnew'
)
BEGIN
    DECLARE @var26 sysname;
    SELECT @var26 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Lessons]') AND [c].[name] = N'TopicId');
    IF @var26 IS NOT NULL EXEC(N'ALTER TABLE [Lessons] DROP CONSTRAINT [' + @var26 + '];');
    ALTER TABLE [Lessons] ALTER COLUMN [TopicId] uniqueidentifier NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107135200_newnewnew'
)
BEGIN
    DECLARE @var27 sysname;
    SELECT @var27 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Lessons]') AND [c].[name] = N'TeacherId');
    IF @var27 IS NOT NULL EXEC(N'ALTER TABLE [Lessons] DROP CONSTRAINT [' + @var27 + '];');
    ALTER TABLE [Lessons] ALTER COLUMN [TeacherId] uniqueidentifier NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107135200_newnewnew'
)
BEGIN
    DECLARE @var28 sysname;
    SELECT @var28 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Lessons]') AND [c].[name] = N'SubjectId');
    IF @var28 IS NOT NULL EXEC(N'ALTER TABLE [Lessons] DROP CONSTRAINT [' + @var28 + '];');
    ALTER TABLE [Lessons] ALTER COLUMN [SubjectId] uniqueidentifier NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107135200_newnewnew'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] ON;
    EXEC(N'INSERT INTO [subscriptionPlans] ([Id], [CreatedAt], [CreatedBy], [Deleted], [Description], [DurationInDays], [IsActive], [Name], [PlanType], [Price], [UpdatedAt], [UpdatedBy])
    VALUES (''08fdba46-d669-459e-9155-8e5bfdbc0654'', ''2026-01-07T15:51:59.5276524+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Basic monthly subscription'', 30, CAST(1 AS bit), N''Basic Monthly'', N''monthly'', 9.99, NULL, NULL),
    (''c30e09fa-e3b4-4b2a-8079-93cc3df4d2fe'', ''2026-01-07T15:51:59.5303381+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Annual subscription with discount'', 365, CAST(1 AS bit), N''Annual Plan'', N''annual'', 99.99, NULL, NULL),
    (''f56da234-60e4-4721-a4b0-56a588a02ed6'', ''2026-01-07T15:51:59.5303344+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Premium monthly subscription'', 30, CAST(1 AS bit), N''Premium Monthly'', N''monthly'', 19.99, NULL, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107135200_newnewnew'
)
BEGIN
    ALTER TABLE [Lessons] ADD CONSTRAINT [FK_Lessons_Subjects_SubjectId] FOREIGN KEY ([SubjectId]) REFERENCES [Subjects] ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107135200_newnewnew'
)
BEGIN
    ALTER TABLE [Lessons] ADD CONSTRAINT [FK_Lessons_Teacher_TeacherId] FOREIGN KEY ([TeacherId]) REFERENCES [Teacher] ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107135200_newnewnew'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260107135200_newnewnew', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107140508_FixTopicLessonRelation'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''08fdba46-d669-459e-9155-8e5bfdbc0654'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107140508_FixTopicLessonRelation'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''c30e09fa-e3b4-4b2a-8079-93cc3df4d2fe'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107140508_FixTopicLessonRelation'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''f56da234-60e4-4721-a4b0-56a588a02ed6'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107140508_FixTopicLessonRelation'
)
BEGIN
    DROP INDEX [IX_Lessons_TopicId] ON [Lessons];
    DECLARE @var29 sysname;
    SELECT @var29 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Lessons]') AND [c].[name] = N'TopicId');
    IF @var29 IS NOT NULL EXEC(N'ALTER TABLE [Lessons] DROP CONSTRAINT [' + @var29 + '];');
    EXEC(N'UPDATE [Lessons] SET [TopicId] = ''00000000-0000-0000-0000-000000000000'' WHERE [TopicId] IS NULL');
    ALTER TABLE [Lessons] ALTER COLUMN [TopicId] uniqueidentifier NOT NULL;
    ALTER TABLE [Lessons] ADD DEFAULT '00000000-0000-0000-0000-000000000000' FOR [TopicId];
    CREATE INDEX [IX_Lessons_TopicId] ON [Lessons] ([TopicId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107140508_FixTopicLessonRelation'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] ON;
    EXEC(N'INSERT INTO [subscriptionPlans] ([Id], [CreatedAt], [CreatedBy], [Deleted], [Description], [DurationInDays], [IsActive], [Name], [PlanType], [Price], [UpdatedAt], [UpdatedBy])
    VALUES (''5b7d5655-4956-4156-805c-b0d805f0c4c4'', ''2026-01-07T16:05:08.1291010+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Premium monthly subscription'', 30, CAST(1 AS bit), N''Premium Monthly'', N''monthly'', 19.99, NULL, NULL),
    (''b586f386-9964-43a0-8f55-7e838c49177a'', ''2026-01-07T16:05:08.1291067+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Annual subscription with discount'', 365, CAST(1 AS bit), N''Annual Plan'', N''annual'', 99.99, NULL, NULL),
    (''f907a3b9-ea35-43dc-ad91-17f333ccc59e'', ''2026-01-07T16:05:08.1254396+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Basic monthly subscription'', 30, CAST(1 AS bit), N''Basic Monthly'', N''monthly'', 9.99, NULL, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107140508_FixTopicLessonRelation'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260107140508_FixTopicLessonRelation', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107141940_MakeTopicIdNullable'
)
BEGIN
    ALTER TABLE [Lessons] DROP CONSTRAINT [FK_Lessons_Subjects_SubjectId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107141940_MakeTopicIdNullable'
)
BEGIN
    DROP INDEX [IX_Lessons_SubjectId] ON [Lessons];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107141940_MakeTopicIdNullable'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''5b7d5655-4956-4156-805c-b0d805f0c4c4'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107141940_MakeTopicIdNullable'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''b586f386-9964-43a0-8f55-7e838c49177a'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107141940_MakeTopicIdNullable'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''f907a3b9-ea35-43dc-ad91-17f333ccc59e'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107141940_MakeTopicIdNullable'
)
BEGIN
    DECLARE @var30 sysname;
    SELECT @var30 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Lessons]') AND [c].[name] = N'SubjectId');
    IF @var30 IS NOT NULL EXEC(N'ALTER TABLE [Lessons] DROP CONSTRAINT [' + @var30 + '];');
    ALTER TABLE [Lessons] DROP COLUMN [SubjectId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107141940_MakeTopicIdNullable'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] ON;
    EXEC(N'INSERT INTO [subscriptionPlans] ([Id], [CreatedAt], [CreatedBy], [Deleted], [Description], [DurationInDays], [IsActive], [Name], [PlanType], [Price], [UpdatedAt], [UpdatedBy])
    VALUES (''9dadca4e-bb61-44e9-bfcd-eef13ca3ed27'', ''2026-01-07T16:19:39.6580472+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Basic monthly subscription'', 30, CAST(1 AS bit), N''Basic Monthly'', N''monthly'', 9.99, NULL, NULL),
    (''c8b93147-b3b6-4193-8520-62375fc3fb71'', ''2026-01-07T16:19:39.6606923+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Annual subscription with discount'', 365, CAST(1 AS bit), N''Annual Plan'', N''annual'', 99.99, NULL, NULL),
    (''f239b155-ae15-46a8-ab70-abae6f4aa867'', ''2026-01-07T16:19:39.6606798+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Premium monthly subscription'', 30, CAST(1 AS bit), N''Premium Monthly'', N''monthly'', 19.99, NULL, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107141940_MakeTopicIdNullable'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260107141940_MakeTopicIdNullable', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107142505_hjksakksks'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''9dadca4e-bb61-44e9-bfcd-eef13ca3ed27'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107142505_hjksakksks'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''c8b93147-b3b6-4193-8520-62375fc3fb71'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107142505_hjksakksks'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''f239b155-ae15-46a8-ab70-abae6f4aa867'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107142505_hjksakksks'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] ON;
    EXEC(N'INSERT INTO [subscriptionPlans] ([Id], [CreatedAt], [CreatedBy], [Deleted], [Description], [DurationInDays], [IsActive], [Name], [PlanType], [Price], [UpdatedAt], [UpdatedBy])
    VALUES (''b9d6acf3-2473-4823-bec3-52a274940bc8'', ''2026-01-07T16:25:05.0881372+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Basic monthly subscription'', 30, CAST(1 AS bit), N''Basic Monthly'', N''monthly'', 9.99, NULL, NULL),
    (''ed175958-4634-41ca-996d-279ff39ac705'', ''2026-01-07T16:25:05.0912697+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Annual subscription with discount'', 365, CAST(1 AS bit), N''Annual Plan'', N''annual'', 99.99, NULL, NULL),
    (''ed31c470-0778-4aff-9102-3910cd6af752'', ''2026-01-07T16:25:05.0912653+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Premium monthly subscription'', 30, CAST(1 AS bit), N''Premium Monthly'', N''monthly'', 19.99, NULL, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107142505_hjksakksks'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260107142505_hjksakksks', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107143559_MakeLessonTopicNullable'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''b9d6acf3-2473-4823-bec3-52a274940bc8'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107143559_MakeLessonTopicNullable'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''ed175958-4634-41ca-996d-279ff39ac705'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107143559_MakeLessonTopicNullable'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''ed31c470-0778-4aff-9102-3910cd6af752'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107143559_MakeLessonTopicNullable'
)
BEGIN
    DECLARE @var31 sysname;
    SELECT @var31 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Lessons]') AND [c].[name] = N'TopicId');
    IF @var31 IS NOT NULL EXEC(N'ALTER TABLE [Lessons] DROP CONSTRAINT [' + @var31 + '];');
    ALTER TABLE [Lessons] ALTER COLUMN [TopicId] uniqueidentifier NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107143559_MakeLessonTopicNullable'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] ON;
    EXEC(N'INSERT INTO [subscriptionPlans] ([Id], [CreatedAt], [CreatedBy], [Deleted], [Description], [DurationInDays], [IsActive], [Name], [PlanType], [Price], [UpdatedAt], [UpdatedBy])
    VALUES (''04f92952-64d4-4aaf-9ee8-9edfb80a20df'', ''2026-01-07T16:35:58.7189055+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Basic monthly subscription'', 30, CAST(1 AS bit), N''Basic Monthly'', N''monthly'', 9.99, NULL, NULL),
    (''7773b20c-b672-4f75-8cc2-9d0f9d553099'', ''2026-01-07T16:35:58.7214237+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Annual subscription with discount'', 365, CAST(1 AS bit), N''Annual Plan'', N''annual'', 99.99, NULL, NULL),
    (''af7739c4-e5a3-4a36-b8d2-1b021eae0d68'', ''2026-01-07T16:35:58.7214201+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Premium monthly subscription'', 30, CAST(1 AS bit), N''Premium Monthly'', N''monthly'', 19.99, NULL, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107143559_MakeLessonTopicNullable'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260107143559_MakeLessonTopicNullable', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107153148_hhhshhshahha'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''04f92952-64d4-4aaf-9ee8-9edfb80a20df'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107153148_hhhshhshahha'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''7773b20c-b672-4f75-8cc2-9d0f9d553099'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107153148_hhhshhshahha'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''af7739c4-e5a3-4a36-b8d2-1b021eae0d68'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107153148_hhhshhshahha'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] ON;
    EXEC(N'INSERT INTO [subscriptionPlans] ([Id], [CreatedAt], [CreatedBy], [Deleted], [Description], [DurationInDays], [IsActive], [Name], [PlanType], [Price], [UpdatedAt], [UpdatedBy])
    VALUES (''02d47c14-a1ae-4007-93ac-882963507349'', ''2026-01-07T17:31:47.5694952+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Annual subscription with discount'', 365, CAST(1 AS bit), N''Annual Plan'', N''annual'', 99.99, NULL, NULL),
    (''45c01355-6abc-4acf-b276-e2bf12be1cf7'', ''2026-01-07T17:31:47.5694873+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Premium monthly subscription'', 30, CAST(1 AS bit), N''Premium Monthly'', N''monthly'', 19.99, NULL, NULL),
    (''e161707a-d324-406c-8595-b707442e8882'', ''2026-01-07T17:31:47.5663291+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Basic monthly subscription'', 30, CAST(1 AS bit), N''Basic Monthly'', N''monthly'', 9.99, NULL, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107153148_hhhshhshahha'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260107153148_hhhshhshahha', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107154456_RemoveLessonIdFromTopics'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''02d47c14-a1ae-4007-93ac-882963507349'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107154456_RemoveLessonIdFromTopics'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''45c01355-6abc-4acf-b276-e2bf12be1cf7'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107154456_RemoveLessonIdFromTopics'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''e161707a-d324-406c-8595-b707442e8882'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107154456_RemoveLessonIdFromTopics'
)
BEGIN
    DECLARE @var32 sysname;
    SELECT @var32 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Topics]') AND [c].[name] = N'UpdatedAt');
    IF @var32 IS NOT NULL EXEC(N'ALTER TABLE [Topics] DROP CONSTRAINT [' + @var32 + '];');
    ALTER TABLE [Topics] ADD DEFAULT (GETUTCDATE()) FOR [UpdatedAt];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107154456_RemoveLessonIdFromTopics'
)
BEGIN
    DECLARE @var33 sysname;
    SELECT @var33 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Topics]') AND [c].[name] = N'CreatedAt');
    IF @var33 IS NOT NULL EXEC(N'ALTER TABLE [Topics] DROP CONSTRAINT [' + @var33 + '];');
    EXEC(N'UPDATE [Topics] SET [CreatedAt] = GETUTCDATE() WHERE [CreatedAt] IS NULL');
    ALTER TABLE [Topics] ALTER COLUMN [CreatedAt] datetime2 NOT NULL;
    ALTER TABLE [Topics] ADD DEFAULT (GETUTCDATE()) FOR [CreatedAt];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107154456_RemoveLessonIdFromTopics'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] ON;
    EXEC(N'INSERT INTO [subscriptionPlans] ([Id], [CreatedAt], [CreatedBy], [Deleted], [Description], [DurationInDays], [IsActive], [Name], [PlanType], [Price], [UpdatedAt], [UpdatedBy])
    VALUES (''195ec8c3-57f6-42c1-99b7-72e934f2035e'', ''2026-01-07T17:44:55.5811053+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Basic monthly subscription'', 30, CAST(1 AS bit), N''Basic Monthly'', N''monthly'', 9.99, NULL, NULL),
    (''1cbb64a7-db25-47e1-86df-82a11ccc6396'', ''2026-01-07T17:44:55.5840703+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Annual subscription with discount'', 365, CAST(1 AS bit), N''Annual Plan'', N''annual'', 99.99, NULL, NULL),
    (''5de3eef7-0779-4f20-bea4-3bf51fcf1154'', ''2026-01-07T17:44:55.5840651+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Premium monthly subscription'', 30, CAST(1 AS bit), N''Premium Monthly'', N''monthly'', 19.99, NULL, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107154456_RemoveLessonIdFromTopics'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260107154456_RemoveLessonIdFromTopics', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107155032_kkaskkakskka'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''195ec8c3-57f6-42c1-99b7-72e934f2035e'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107155032_kkaskkakskka'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''1cbb64a7-db25-47e1-86df-82a11ccc6396'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107155032_kkaskkakskka'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''5de3eef7-0779-4f20-bea4-3bf51fcf1154'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107155032_kkaskkakskka'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] ON;
    EXEC(N'INSERT INTO [subscriptionPlans] ([Id], [CreatedAt], [CreatedBy], [Deleted], [Description], [DurationInDays], [IsActive], [Name], [PlanType], [Price], [UpdatedAt], [UpdatedBy])
    VALUES (''cc99b3f9-217f-467a-8df2-304d91bc9a39'', ''2026-01-07T17:50:31.0553385+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Annual subscription with discount'', 365, CAST(1 AS bit), N''Annual Plan'', N''annual'', 99.99, NULL, NULL),
    (''cdbb16c2-29c7-4603-ab2a-13f510c875e5'', ''2026-01-07T17:50:31.0553166+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Premium monthly subscription'', 30, CAST(1 AS bit), N''Premium Monthly'', N''monthly'', 19.99, NULL, NULL),
    (''d788fe9c-e9f4-4ea4-a0d9-03cc91a5f881'', ''2026-01-07T17:50:31.0519971+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Basic monthly subscription'', 30, CAST(1 AS bit), N''Basic Monthly'', N''monthly'', 9.99, NULL, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107155032_kkaskkakskka'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260107155032_kkaskkakskka', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260107214618_llappakdkkd'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260107214618_llappakdkkd', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260108104016_UpdateTopictoUnit'
)
BEGIN
    ALTER TABLE [Videos] DROP CONSTRAINT [FK_Videos_Topics_TopicId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260108104016_UpdateTopictoUnit'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''25660276-7b3d-4603-b2a5-e5ff0b0a5ca4'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260108104016_UpdateTopictoUnit'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''5947a3fa-9ba7-4b87-bc22-896caed26e16'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260108104016_UpdateTopictoUnit'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''69853aeb-6aac-41db-a93a-22773dc61774'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260108104016_UpdateTopictoUnit'
)
BEGIN
    EXEC sp_rename N'[Videos].[TopicId]', N'UnitId', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260108104016_UpdateTopictoUnit'
)
BEGIN
    EXEC sp_rename N'[Videos].[IX_Videos_TopicId]', N'IX_Videos_UnitId', 'INDEX';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260108104016_UpdateTopictoUnit'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] ON;
    EXEC(N'INSERT INTO [subscriptionPlans] ([Id], [CreatedAt], [CreatedBy], [Deleted], [Description], [DurationInDays], [IsActive], [Name], [PlanType], [Price], [UpdatedAt], [UpdatedBy])
    VALUES (''16c25a5f-e3a1-4cda-ae9d-06e91743ca48'', ''2026-01-08T12:40:15.5022395+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Annual subscription with discount'', 365, CAST(1 AS bit), N''Annual Plan'', N''annual'', 99.99, NULL, NULL),
    (''c066daf3-ab82-4358-b908-b501e9beb622'', ''2026-01-08T12:40:15.5022350+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Premium monthly subscription'', 30, CAST(1 AS bit), N''Premium Monthly'', N''monthly'', 19.99, NULL, NULL),
    (''fc87f15a-d553-4685-9d08-7166424b0e94'', ''2026-01-08T12:40:15.4988726+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Basic monthly subscription'', 30, CAST(1 AS bit), N''Basic Monthly'', N''monthly'', 9.99, NULL, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260108104016_UpdateTopictoUnit'
)
BEGIN
    ALTER TABLE [Videos] ADD CONSTRAINT [FK_Videos_Topics_UnitId] FOREIGN KEY ([UnitId]) REFERENCES [Topics] ([Id]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260108104016_UpdateTopictoUnit'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260108104016_UpdateTopictoUnit', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260108105246_UpdateTopictounit2'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''16c25a5f-e3a1-4cda-ae9d-06e91743ca48'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260108105246_UpdateTopictounit2'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''c066daf3-ab82-4358-b908-b501e9beb622'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260108105246_UpdateTopictounit2'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''fc87f15a-d553-4685-9d08-7166424b0e94'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260108105246_UpdateTopictounit2'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] ON;
    EXEC(N'INSERT INTO [subscriptionPlans] ([Id], [CreatedAt], [CreatedBy], [Deleted], [Description], [DurationInDays], [IsActive], [Name], [PlanType], [Price], [UpdatedAt], [UpdatedBy])
    VALUES (''33382f5d-5e34-45ad-8db9-226335ee023c'', ''2026-01-08T12:52:45.4440330+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Basic monthly subscription'', 30, CAST(1 AS bit), N''Basic Monthly'', N''monthly'', 9.99, NULL, NULL),
    (''58b5a220-ffea-4dcd-b7ae-2d39d79fac75'', ''2026-01-08T12:52:45.4481811+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Premium monthly subscription'', 30, CAST(1 AS bit), N''Premium Monthly'', N''monthly'', 19.99, NULL, NULL),
    (''f670d413-0f4b-4e3e-b6d9-b198ac613eb7'', ''2026-01-08T12:52:45.4481925+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Annual subscription with discount'', 365, CAST(1 AS bit), N''Annual Plan'', N''annual'', 99.99, NULL, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260108105246_UpdateTopictounit2'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260108105246_UpdateTopictounit2', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260108115210_MakeBunnytakeNull'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''33382f5d-5e34-45ad-8db9-226335ee023c'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260108115210_MakeBunnytakeNull'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''58b5a220-ffea-4dcd-b7ae-2d39d79fac75'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260108115210_MakeBunnytakeNull'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''f670d413-0f4b-4e3e-b6d9-b198ac613eb7'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260108115210_MakeBunnytakeNull'
)
BEGIN
    DECLARE @var34 sysname;
    SELECT @var34 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Videos]') AND [c].[name] = N'BunnyVideoId');
    IF @var34 IS NOT NULL EXEC(N'ALTER TABLE [Videos] DROP CONSTRAINT [' + @var34 + '];');
    ALTER TABLE [Videos] ALTER COLUMN [BunnyVideoId] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260108115210_MakeBunnytakeNull'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] ON;
    EXEC(N'INSERT INTO [subscriptionPlans] ([Id], [CreatedAt], [CreatedBy], [Deleted], [Description], [DurationInDays], [IsActive], [Name], [PlanType], [Price], [UpdatedAt], [UpdatedBy])
    VALUES (''1d96fef7-2728-4caa-9fb7-5aa1cdb0f90b'', ''2026-01-08T13:52:09.5550466+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Basic monthly subscription'', 30, CAST(1 AS bit), N''Basic Monthly'', N''monthly'', 9.99, NULL, NULL),
    (''93e5f616-e374-4091-89e0-2f73a170b64c'', ''2026-01-08T13:52:09.5573287+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Annual subscription with discount'', 365, CAST(1 AS bit), N''Annual Plan'', N''annual'', 99.99, NULL, NULL),
    (''aaa1949a-bd3e-4f66-a7d7-72dad28c42ee'', ''2026-01-08T13:52:09.5573254+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Premium monthly subscription'', 30, CAST(1 AS bit), N''Premium Monthly'', N''monthly'', 19.99, NULL, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260108115210_MakeBunnytakeNull'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260108115210_MakeBunnytakeNull', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260108131635_newnewnewnew'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260108131635_newnewnewnew', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260108132953_AddUnitIdToLessons'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''46fcf328-7698-4c5d-b107-45371c3666d8'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260108132953_AddUnitIdToLessons'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''51ea46e2-db88-4a7c-a119-321f206106b7'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260108132953_AddUnitIdToLessons'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''e4b15461-a59c-422f-a474-11068ebc78e1'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260108132953_AddUnitIdToLessons'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] ON;
    EXEC(N'INSERT INTO [subscriptionPlans] ([Id], [CreatedAt], [CreatedBy], [Deleted], [Description], [DurationInDays], [IsActive], [Name], [PlanType], [Price], [UpdatedAt], [UpdatedBy])
    VALUES (''299d99fc-c025-45d4-be79-39a6a431216e'', ''2026-01-08T15:29:53.0875544+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Premium monthly subscription'', 30, CAST(1 AS bit), N''Premium Monthly'', N''monthly'', 19.99, NULL, NULL),
    (''30d55309-fa38-43b7-910b-0a1a7d0a327e'', ''2026-01-08T15:29:53.0852908+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Basic monthly subscription'', 30, CAST(1 AS bit), N''Basic Monthly'', N''monthly'', 9.99, NULL, NULL),
    (''e0419d92-ae8f-4778-ab5c-2a8d6657f4ea'', ''2026-01-08T15:29:53.0875579+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Annual subscription with discount'', 365, CAST(1 AS bit), N''Annual Plan'', N''annual'', 99.99, NULL, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260108132953_AddUnitIdToLessons'
)
BEGIN
    ALTER TABLE [Lessons] ADD [UnitId] uniqueidentifier NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260108132953_AddUnitIdToLessons'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260108132953_AddUnitIdToLessons', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260108152312_AddDescriptionColumntoUnit'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''299d99fc-c025-45d4-be79-39a6a431216e'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260108152312_AddDescriptionColumntoUnit'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''30d55309-fa38-43b7-910b-0a1a7d0a327e'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260108152312_AddDescriptionColumntoUnit'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''e0419d92-ae8f-4778-ab5c-2a8d6657f4ea'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260108152312_AddDescriptionColumntoUnit'
)
BEGIN
    ALTER TABLE [Topics] ADD [Description] nvarchar(2000) NOT NULL DEFAULT N'';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260108152312_AddDescriptionColumntoUnit'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] ON;
    EXEC(N'INSERT INTO [subscriptionPlans] ([Id], [CreatedAt], [CreatedBy], [Deleted], [Description], [DurationInDays], [IsActive], [Name], [PlanType], [Price], [UpdatedAt], [UpdatedBy])
    VALUES (''05dd33d4-a743-4c4b-ae4f-f062eb8c14c7'', ''2026-01-08T17:23:11.3911511+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Basic monthly subscription'', 30, CAST(1 AS bit), N''Basic Monthly'', N''monthly'', 9.99, NULL, NULL),
    (''98900d7a-8654-4dc5-83ef-9195cc6d2881'', ''2026-01-08T17:23:11.3943487+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Premium monthly subscription'', 30, CAST(1 AS bit), N''Premium Monthly'', N''monthly'', 19.99, NULL, NULL),
    (''9ef89d77-c76a-4e95-8dd1-acda3cc1291f'', ''2026-01-08T17:23:11.3943532+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Annual subscription with discount'', 365, CAST(1 AS bit), N''Annual Plan'', N''annual'', 99.99, NULL, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260108152312_AddDescriptionColumntoUnit'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260108152312_AddDescriptionColumntoUnit', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260111143419_UpdateVideoTable'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''05dd33d4-a743-4c4b-ae4f-f062eb8c14c7'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260111143419_UpdateVideoTable'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''98900d7a-8654-4dc5-83ef-9195cc6d2881'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260111143419_UpdateVideoTable'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''9ef89d77-c76a-4e95-8dd1-acda3cc1291f'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260111143419_UpdateVideoTable'
)
BEGIN
    ALTER TABLE [Videos] ADD [Description] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260111143419_UpdateVideoTable'
)
BEGIN
    ALTER TABLE [Videos] ADD [ThumbnailUrl] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260111143419_UpdateVideoTable'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] ON;
    EXEC(N'INSERT INTO [subscriptionPlans] ([Id], [CreatedAt], [CreatedBy], [Deleted], [Description], [DurationInDays], [IsActive], [Name], [PlanType], [Price], [UpdatedAt], [UpdatedBy])
    VALUES (''58ebfb4c-1117-4991-b219-fbf2403e859a'', ''2026-01-11T16:34:18.9844915+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Premium monthly subscription'', 30, CAST(1 AS bit), N''Premium Monthly'', N''monthly'', 19.99, NULL, NULL),
    (''e794ecc3-54ac-4df1-9779-f4f5fdfc6a03'', ''2026-01-11T16:34:18.9449602+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Basic monthly subscription'', 30, CAST(1 AS bit), N''Basic Monthly'', N''monthly'', 9.99, NULL, NULL),
    (''fdba8a3e-7343-4081-ab30-de2e690b810c'', ''2026-01-11T16:34:18.9844948+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Annual subscription with discount'', 365, CAST(1 AS bit), N''Annual Plan'', N''annual'', 99.99, NULL, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260111143419_UpdateVideoTable'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260111143419_UpdateVideoTable', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260111154014_hjdshjadshjasdads'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''58ebfb4c-1117-4991-b219-fbf2403e859a'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260111154014_hjdshjadshjasdads'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''e794ecc3-54ac-4df1-9779-f4f5fdfc6a03'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260111154014_hjdshjadshjasdads'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''fdba8a3e-7343-4081-ab30-de2e690b810c'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260111154014_hjdshjadshjasdads'
)
BEGIN
    DECLARE @var35 sysname;
    SELECT @var35 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Videos]') AND [c].[name] = N'Duration');
    IF @var35 IS NOT NULL EXEC(N'ALTER TABLE [Videos] DROP CONSTRAINT [' + @var35 + '];');
    ALTER TABLE [Videos] ALTER COLUMN [Duration] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260111154014_hjdshjadshjasdads'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] ON;
    EXEC(N'INSERT INTO [subscriptionPlans] ([Id], [CreatedAt], [CreatedBy], [Deleted], [Description], [DurationInDays], [IsActive], [Name], [PlanType], [Price], [UpdatedAt], [UpdatedBy])
    VALUES (''6d2782d4-6520-4c7d-9dd5-dd79ebc81ee0'', ''2026-01-11T17:40:13.2847307+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Annual subscription with discount'', 365, CAST(1 AS bit), N''Annual Plan'', N''annual'', 99.99, NULL, NULL),
    (''72c3b347-ff8f-4fe8-b582-5508bb206edf'', ''2026-01-11T17:40:13.2814790+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Basic monthly subscription'', 30, CAST(1 AS bit), N''Basic Monthly'', N''monthly'', 9.99, NULL, NULL),
    (''fc062c7c-9b08-46c3-b1dd-a3c1162dc361'', ''2026-01-11T17:40:13.2847263+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Premium monthly subscription'', 30, CAST(1 AS bit), N''Premium Monthly'', N''monthly'', 19.99, NULL, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260111154014_hjdshjadshjasdads'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260111154014_hjdshjadshjasdads', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260112092256_UpdateLessonTable'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''6d2782d4-6520-4c7d-9dd5-dd79ebc81ee0'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260112092256_UpdateLessonTable'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''72c3b347-ff8f-4fe8-b582-5508bb206edf'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260112092256_UpdateLessonTable'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''fc062c7c-9b08-46c3-b1dd-a3c1162dc361'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260112092256_UpdateLessonTable'
)
BEGIN
    DECLARE @var36 sysname;
    SELECT @var36 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Lessons]') AND [c].[name] = N'LessonfileName');
    IF @var36 IS NOT NULL EXEC(N'ALTER TABLE [Lessons] DROP CONSTRAINT [' + @var36 + '];');
    ALTER TABLE [Lessons] DROP COLUMN [LessonfileName];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260112092256_UpdateLessonTable'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] ON;
    EXEC(N'INSERT INTO [subscriptionPlans] ([Id], [CreatedAt], [CreatedBy], [Deleted], [Description], [DurationInDays], [IsActive], [Name], [PlanType], [Price], [UpdatedAt], [UpdatedBy])
    VALUES (''0f6a3885-6da5-4de1-8d67-d5ef5d9cd3ff'', ''2026-01-12T11:22:54.5733563+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Basic monthly subscription'', 30, CAST(1 AS bit), N''Basic Monthly'', N''monthly'', 9.99, NULL, NULL),
    (''59d4cabe-ef2f-4bb9-9afa-c8edc9cc172d'', ''2026-01-12T11:22:54.5774224+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Premium monthly subscription'', 30, CAST(1 AS bit), N''Premium Monthly'', N''monthly'', 19.99, NULL, NULL),
    (''a8d9b0bc-2eeb-4a59-ae62-d9d8afc4f994'', ''2026-01-12T11:22:54.5774275+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Annual subscription with discount'', 365, CAST(1 AS bit), N''Annual Plan'', N''annual'', 99.99, NULL, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260112092256_UpdateLessonTable'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260112092256_UpdateLessonTable', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260112100653_UpdateTableTopicstoUnits'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''0f6a3885-6da5-4de1-8d67-d5ef5d9cd3ff'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260112100653_UpdateTableTopicstoUnits'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''59d4cabe-ef2f-4bb9-9afa-c8edc9cc172d'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260112100653_UpdateTableTopicstoUnits'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''a8d9b0bc-2eeb-4a59-ae62-d9d8afc4f994'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260112100653_UpdateTableTopicstoUnits'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] ON;
    EXEC(N'INSERT INTO [subscriptionPlans] ([Id], [CreatedAt], [CreatedBy], [Deleted], [Description], [DurationInDays], [IsActive], [Name], [PlanType], [Price], [UpdatedAt], [UpdatedBy])
    VALUES (''2a7db809-3cd5-44c0-ba95-9b8554d6ccf2'', ''2026-01-12T12:06:52.6430224+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Premium monthly subscription'', 30, CAST(1 AS bit), N''Premium Monthly'', N''monthly'', 19.99, NULL, NULL),
    (''b7d7509e-e202-4567-96c8-3e0f5e50f4b0'', ''2026-01-12T12:06:52.6390578+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Basic monthly subscription'', 30, CAST(1 AS bit), N''Basic Monthly'', N''monthly'', 9.99, NULL, NULL),
    (''e6751cd6-ff03-482e-99f9-455b52532c37'', ''2026-01-12T12:06:52.6430258+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Annual subscription with discount'', 365, CAST(1 AS bit), N''Annual Plan'', N''annual'', 99.99, NULL, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260112100653_UpdateTableTopicstoUnits'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260112100653_UpdateTableTopicstoUnits', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260112102127_LevelSubjectsUnitsLessonsVideos'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''2a7db809-3cd5-44c0-ba95-9b8554d6ccf2'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260112102127_LevelSubjectsUnitsLessonsVideos'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''b7d7509e-e202-4567-96c8-3e0f5e50f4b0'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260112102127_LevelSubjectsUnitsLessonsVideos'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''e6751cd6-ff03-482e-99f9-455b52532c37'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260112102127_LevelSubjectsUnitsLessonsVideos'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] ON;
    EXEC(N'INSERT INTO [subscriptionPlans] ([Id], [CreatedAt], [CreatedBy], [Deleted], [Description], [DurationInDays], [IsActive], [Name], [PlanType], [Price], [UpdatedAt], [UpdatedBy])
    VALUES (''295e5ff7-40e9-42b9-893c-20820a422339'', ''2026-01-12T12:21:26.2115348+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Annual subscription with discount'', 365, CAST(1 AS bit), N''Annual Plan'', N''annual'', 99.99, NULL, NULL),
    (''6df36a83-d493-45a5-b94a-74703224a36a'', ''2026-01-12T12:21:26.2115294+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Premium monthly subscription'', 30, CAST(1 AS bit), N''Premium Monthly'', N''monthly'', 19.99, NULL, NULL),
    (''a5a5daee-9c9c-4b47-ba93-9b032551725a'', ''2026-01-12T12:21:26.2087043+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Basic monthly subscription'', 30, CAST(1 AS bit), N''Basic Monthly'', N''monthly'', 9.99, NULL, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260112102127_LevelSubjectsUnitsLessonsVideos'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260112102127_LevelSubjectsUnitsLessonsVideos', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260112114200_FixVideoFKClean'
)
BEGIN
    ALTER TABLE [Videos] DROP CONSTRAINT [FK_Videos_Lessons_LessonId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260112114200_FixVideoFKClean'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''295e5ff7-40e9-42b9-893c-20820a422339'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260112114200_FixVideoFKClean'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''6df36a83-d493-45a5-b94a-74703224a36a'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260112114200_FixVideoFKClean'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''a5a5daee-9c9c-4b47-ba93-9b032551725a'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260112114200_FixVideoFKClean'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] ON;
    EXEC(N'INSERT INTO [subscriptionPlans] ([Id], [CreatedAt], [CreatedBy], [Deleted], [Description], [DurationInDays], [IsActive], [Name], [PlanType], [Price], [UpdatedAt], [UpdatedBy])
    VALUES (''03a878a1-193a-4437-bcd0-12f0ff2b081c'', ''2026-01-12T13:41:59.9140518+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Basic monthly subscription'', 30, CAST(1 AS bit), N''Basic Monthly'', N''monthly'', 9.99, NULL, NULL),
    (''5c04cdbf-ec2c-45c6-8ece-b42c011af063'', ''2026-01-12T13:41:59.9162328+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Annual subscription with discount'', 365, CAST(1 AS bit), N''Annual Plan'', N''annual'', 99.99, NULL, NULL),
    (''fba227e0-caaf-4290-ab76-cc3e64ef4f50'', ''2026-01-12T13:41:59.9162296+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Premium monthly subscription'', 30, CAST(1 AS bit), N''Premium Monthly'', N''monthly'', 19.99, NULL, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260112114200_FixVideoFKClean'
)
BEGIN
    ALTER TABLE [Lessons] ADD CONSTRAINT [FK_Lessons_Units_UnitId] FOREIGN KEY ([UnitId]) REFERENCES [Units] ([Id]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260112114200_FixVideoFKClean'
)
BEGIN
    ALTER TABLE [Units] ADD CONSTRAINT [FK_Units_Subjects_SubjectId] FOREIGN KEY ([SubjectId]) REFERENCES [Subjects] ([Id]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260112114200_FixVideoFKClean'
)
BEGIN
    ALTER TABLE [Videos] ADD CONSTRAINT [FK_Videos_Lessons_LessonId] FOREIGN KEY ([LessonId]) REFERENCES [Lessons] ([Id]) ON DELETE NO ACTION;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260112114200_FixVideoFKClean'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260112114200_FixVideoFKClean', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260113083915_LocalDatabaseMigration'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''03a878a1-193a-4437-bcd0-12f0ff2b081c'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260113083915_LocalDatabaseMigration'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''5c04cdbf-ec2c-45c6-8ece-b42c011af063'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260113083915_LocalDatabaseMigration'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''fba227e0-caaf-4290-ab76-cc3e64ef4f50'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260113083915_LocalDatabaseMigration'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] ON;
    EXEC(N'INSERT INTO [subscriptionPlans] ([Id], [CreatedAt], [CreatedBy], [Deleted], [Description], [DurationInDays], [IsActive], [Name], [PlanType], [Price], [UpdatedAt], [UpdatedBy])
    VALUES (''10e43969-0672-4419-8d1e-d3b6e50838a9'', ''2026-01-13T10:39:13.6250086+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Basic monthly subscription'', 30, CAST(1 AS bit), N''Basic Monthly'', N''monthly'', 9.99, NULL, NULL),
    (''1eccea22-9f8d-4108-baed-373e65ca9f75'', ''2026-01-13T10:39:13.6288303+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Premium monthly subscription'', 30, CAST(1 AS bit), N''Premium Monthly'', N''monthly'', 19.99, NULL, NULL),
    (''468c2515-57bf-42ae-9dd1-ec28940c8082'', ''2026-01-13T10:39:13.6288387+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Annual subscription with discount'', 365, CAST(1 AS bit), N''Annual Plan'', N''annual'', 99.99, NULL, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260113083915_LocalDatabaseMigration'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260113083915_LocalDatabaseMigration', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260113132602_UpdateAssessmenta'
)
BEGIN

                        IF EXISTS (
                            SELECT 1 
                            FROM sys.foreign_keys 
                            WHERE name = 'FK_Assessments_Units_TopicId'
                        )
                        BEGIN
                            ALTER TABLE Assessments DROP CONSTRAINT FK_Assessments_Units_TopicId
                        END
                    
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260113132602_UpdateAssessmenta'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''10e43969-0672-4419-8d1e-d3b6e50838a9'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260113132602_UpdateAssessmenta'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''1eccea22-9f8d-4108-baed-373e65ca9f75'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260113132602_UpdateAssessmenta'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''468c2515-57bf-42ae-9dd1-ec28940c8082'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260113132602_UpdateAssessmenta'
)
BEGIN
    EXEC sp_rename N'[Assessments].[TopicId]', N'UnitId', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260113132602_UpdateAssessmenta'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] ON;
    EXEC(N'INSERT INTO [subscriptionPlans] ([Id], [CreatedAt], [CreatedBy], [Deleted], [Description], [DurationInDays], [IsActive], [Name], [PlanType], [Price], [UpdatedAt], [UpdatedBy])
    VALUES (''61bee8ae-92e6-4d62-b9d2-fde3f2e8bcd2'', ''2026-01-13T15:26:01.3379295+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Premium monthly subscription'', 30, CAST(1 AS bit), N''Premium Monthly'', N''monthly'', 19.99, NULL, NULL),
    (''ab8ea4af-3ebe-47d5-9db3-074aea0b01e1'', ''2026-01-13T15:26:01.3346914+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Basic monthly subscription'', 30, CAST(1 AS bit), N''Basic Monthly'', N''monthly'', 9.99, NULL, NULL),
    (''dc941b85-b5f6-49f6-98d2-773e8d187d13'', ''2026-01-13T15:26:01.3379338+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Annual subscription with discount'', 365, CAST(1 AS bit), N''Annual Plan'', N''annual'', 99.99, NULL, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260113132602_UpdateAssessmenta'
)
BEGIN
    ALTER TABLE [Assessments] ADD CONSTRAINT [FK_Assessments_Units_UnitId] FOREIGN KEY ([UnitId]) REFERENCES [Units] ([Id]) ON DELETE NO ACTION;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260113132602_UpdateAssessmenta'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260113132602_UpdateAssessmenta', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260113190254_LocalDbSql'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''61bee8ae-92e6-4d62-b9d2-fde3f2e8bcd2'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260113190254_LocalDbSql'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''ab8ea4af-3ebe-47d5-9db3-074aea0b01e1'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260113190254_LocalDbSql'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''dc941b85-b5f6-49f6-98d2-773e8d187d13'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260113190254_LocalDbSql'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] ON;
    EXEC(N'INSERT INTO [subscriptionPlans] ([Id], [CreatedAt], [CreatedBy], [Deleted], [Description], [DurationInDays], [IsActive], [Name], [PlanType], [Price], [UpdatedAt], [UpdatedBy])
    VALUES (''b5df4b4a-6012-4cf6-b1a3-6ea59f0ccb7c'', ''2026-01-13T21:02:53.6800721+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Premium monthly subscription'', 30, CAST(1 AS bit), N''Premium Monthly'', N''monthly'', 19.99, NULL, NULL),
    (''ddd4572f-7a89-4ad1-9d0e-15858669850c'', ''2026-01-13T21:02:53.6800749+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Annual subscription with discount'', 365, CAST(1 AS bit), N''Annual Plan'', N''annual'', 99.99, NULL, NULL),
    (''e05876a6-d4b5-4090-9eaa-645be606bfd0'', ''2026-01-13T21:02:53.6777641+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Basic monthly subscription'', 30, CAST(1 AS bit), N''Basic Monthly'', N''monthly'', 9.99, NULL, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260113190254_LocalDbSql'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260113190254_LocalDbSql', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260113222450_InitIniT'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''b5df4b4a-6012-4cf6-b1a3-6ea59f0ccb7c'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260113222450_InitIniT'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''ddd4572f-7a89-4ad1-9d0e-15858669850c'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260113222450_InitIniT'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''e05876a6-d4b5-4090-9eaa-645be606bfd0'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260113222450_InitIniT'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] ON;
    EXEC(N'INSERT INTO [subscriptionPlans] ([Id], [CreatedAt], [CreatedBy], [Deleted], [Description], [DurationInDays], [IsActive], [Name], [PlanType], [Price], [UpdatedAt], [UpdatedBy])
    VALUES (''38bf50c7-e47c-482f-a130-f1a7cc94f71d'', ''2026-01-14T00:24:49.4317335+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Basic monthly subscription'', 30, CAST(1 AS bit), N''Basic Monthly'', N''monthly'', 9.99, NULL, NULL),
    (''4112f26c-9d0c-4d93-aba2-319eef217210'', ''2026-01-14T00:24:49.4350523+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Premium monthly subscription'', 30, CAST(1 AS bit), N''Premium Monthly'', N''monthly'', 19.99, NULL, NULL),
    (''9c00aae7-5bb1-4236-b90b-ccfa82c1ebef'', ''2026-01-14T00:24:49.4350575+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Annual subscription with discount'', 365, CAST(1 AS bit), N''Annual Plan'', N''annual'', 99.99, NULL, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260113222450_InitIniT'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260113222450_InitIniT', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260113223338_InitItyt'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''38bf50c7-e47c-482f-a130-f1a7cc94f71d'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260113223338_InitItyt'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''4112f26c-9d0c-4d93-aba2-319eef217210'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260113223338_InitItyt'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''9c00aae7-5bb1-4236-b90b-ccfa82c1ebef'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260113223338_InitItyt'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] ON;
    EXEC(N'INSERT INTO [subscriptionPlans] ([Id], [CreatedAt], [CreatedBy], [Deleted], [Description], [DurationInDays], [IsActive], [Name], [PlanType], [Price], [UpdatedAt], [UpdatedBy])
    VALUES (''1aacec9e-4431-4ff5-b840-fb8c9ca1d9d1'', ''2026-01-14T00:33:37.3463914+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Premium monthly subscription'', 30, CAST(1 AS bit), N''Premium Monthly'', N''monthly'', 19.99, NULL, NULL),
    (''cb5f981a-e391-4bc7-b58f-fdc54fed9d68'', ''2026-01-14T00:33:37.3463964+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Annual subscription with discount'', 365, CAST(1 AS bit), N''Annual Plan'', N''annual'', 99.99, NULL, NULL),
    (''d11a8b4f-b725-4395-8fe8-b680aeff1fd7'', ''2026-01-14T00:33:37.3425307+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Basic monthly subscription'', 30, CAST(1 AS bit), N''Basic Monthly'', N''monthly'', 9.99, NULL, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260113223338_InitItyt'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260113223338_InitItyt', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260114141536_NewSkillsUpdate'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''1aacec9e-4431-4ff5-b840-fb8c9ca1d9d1'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260114141536_NewSkillsUpdate'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''cb5f981a-e391-4bc7-b58f-fdc54fed9d68'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260114141536_NewSkillsUpdate'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''d11a8b4f-b725-4395-8fe8-b680aeff1fd7'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260114141536_NewSkillsUpdate'
)
BEGIN
    ALTER TABLE [Skills] ADD [ThumbnailUrl] nvarchar(max) NOT NULL DEFAULT N'';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260114141536_NewSkillsUpdate'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] ON;
    EXEC(N'INSERT INTO [subscriptionPlans] ([Id], [CreatedAt], [CreatedBy], [Deleted], [Description], [DurationInDays], [IsActive], [Name], [PlanType], [Price], [UpdatedAt], [UpdatedBy])
    VALUES (''063347df-a888-4090-9289-ee3f4dea7053'', ''2026-01-14T16:15:34.6861381+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Annual subscription with discount'', 365, CAST(1 AS bit), N''Annual Plan'', N''annual'', 99.99, NULL, NULL),
    (''099973bc-16bb-4792-befb-2e649eab4a87'', ''2026-01-14T16:15:34.6253165+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Basic monthly subscription'', 30, CAST(1 AS bit), N''Basic Monthly'', N''monthly'', 9.99, NULL, NULL),
    (''faea2a64-675b-4d57-b45e-85e841540bc7'', ''2026-01-14T16:15:34.6861239+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Premium monthly subscription'', 30, CAST(1 AS bit), N''Premium Monthly'', N''monthly'', 19.99, NULL, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260114141536_NewSkillsUpdate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260114141536_NewSkillsUpdate', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260115113211_Iniuytr'
)
BEGIN
    ALTER TABLE [Lessons] DROP CONSTRAINT [FK_Lessons_Teacher_TeacherId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260115113211_Iniuytr'
)
BEGIN
    ALTER TABLE [Teacher] DROP CONSTRAINT [FK_Teacher_Users_ApplicationUserId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260115113211_Iniuytr'
)
BEGIN
    ALTER TABLE [Teacher] DROP CONSTRAINT [PK_Teacher];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260115113211_Iniuytr'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''063347df-a888-4090-9289-ee3f4dea7053'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260115113211_Iniuytr'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''099973bc-16bb-4792-befb-2e649eab4a87'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260115113211_Iniuytr'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''faea2a64-675b-4d57-b45e-85e841540bc7'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260115113211_Iniuytr'
)
BEGIN
    EXEC sp_rename N'[Teacher]', N'Teachers', 'OBJECT';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260115113211_Iniuytr'
)
BEGIN
    EXEC sp_rename N'[Teachers].[IX_Teacher_ApplicationUserId]', N'IX_Teachers_ApplicationUserId', 'INDEX';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260115113211_Iniuytr'
)
BEGIN
    ALTER TABLE [Teachers] ADD CONSTRAINT [PK_Teachers] PRIMARY KEY ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260115113211_Iniuytr'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] ON;
    EXEC(N'INSERT INTO [subscriptionPlans] ([Id], [CreatedAt], [CreatedBy], [Deleted], [Description], [DurationInDays], [IsActive], [Name], [PlanType], [Price], [UpdatedAt], [UpdatedBy])
    VALUES (''4cb573b9-6df9-48b7-b83f-4b0598d5f5c3'', ''2026-01-15T13:32:09.6639451+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Basic monthly subscription'', 30, CAST(1 AS bit), N''Basic Monthly'', N''monthly'', 9.99, NULL, NULL),
    (''bc43c144-491e-4535-9872-fd1069a9cd41'', ''2026-01-15T13:32:09.6702913+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Annual subscription with discount'', 365, CAST(1 AS bit), N''Annual Plan'', N''annual'', 99.99, NULL, NULL),
    (''f29d6d64-f33d-4e01-b3ec-5361207e5caa'', ''2026-01-15T13:32:09.6702816+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Premium monthly subscription'', 30, CAST(1 AS bit), N''Premium Monthly'', N''monthly'', 19.99, NULL, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260115113211_Iniuytr'
)
BEGIN
    ALTER TABLE [Lessons] ADD CONSTRAINT [FK_Lessons_Teachers_TeacherId] FOREIGN KEY ([TeacherId]) REFERENCES [Teachers] ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260115113211_Iniuytr'
)
BEGIN
    ALTER TABLE [Teachers] ADD CONSTRAINT [FK_Teachers_Users_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260115113211_Iniuytr'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260115113211_Iniuytr', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260115142423_UpdateVideoEntity'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''4cb573b9-6df9-48b7-b83f-4b0598d5f5c3'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260115142423_UpdateVideoEntity'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''bc43c144-491e-4535-9872-fd1069a9cd41'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260115142423_UpdateVideoEntity'
)
BEGIN
    EXEC(N'DELETE FROM [subscriptionPlans]
    WHERE [Id] = ''f29d6d64-f33d-4e01-b3ec-5361207e5caa'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260115142423_UpdateVideoEntity'
)
BEGIN
    ALTER TABLE [Videos] ADD [SourceType] int NOT NULL DEFAULT 0;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260115142423_UpdateVideoEntity'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] ON;
    EXEC(N'INSERT INTO [subscriptionPlans] ([Id], [CreatedAt], [CreatedBy], [Deleted], [Description], [DurationInDays], [IsActive], [Name], [PlanType], [Price], [UpdatedAt], [UpdatedBy])
    VALUES (''4da31b2d-d4b9-4879-8bf2-de9029eb9977'', ''2026-01-15T16:24:22.5684139+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Basic monthly subscription'', 30, CAST(1 AS bit), N''Basic Monthly'', N''monthly'', 9.99, NULL, NULL),
    (''ab663f01-5711-4a84-a8b8-eff8c0507ee8'', ''2026-01-15T16:24:22.5707160+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Annual subscription with discount'', 365, CAST(1 AS bit), N''Annual Plan'', N''annual'', 99.99, NULL, NULL),
    (''d9b940dc-0e62-48b9-acdb-cf18a775f74c'', ''2026-01-15T16:24:22.5707127+02:00'', ''00000000-0000-0000-0000-000000000000'', CAST(0 AS bit), N''Premium monthly subscription'', 30, CAST(1 AS bit), N''Premium Monthly'', N''monthly'', 19.99, NULL, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'CreatedBy', N'Deleted', N'Description', N'DurationInDays', N'IsActive', N'Name', N'PlanType', N'Price', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[subscriptionPlans]'))
        SET IDENTITY_INSERT [subscriptionPlans] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260115142423_UpdateVideoEntity'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260115142423_UpdateVideoEntity', N'9.0.0');
END;

COMMIT;
GO

