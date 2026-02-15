using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adros.Persistence.Migrations
{
    public partial class SubjectIdinTopicTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Rename LessonId -> SubjectId
            migrationBuilder.RenameColumn(
                name: "LessonId",
                table: "Topics",
                newName: "SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Topics_LessonId",
                table: "Topics",
                newName: "IX_Topics_SubjectId");

            // Add TopicId to Lessons
            migrationBuilder.AddColumn<Guid>(
                name: "TopicId",
                table: "Lessons",
                type: "uniqueidentifier",
                nullable: false
                //defaultValue: new Guid("SOME_EXISTING_TOPIC_GUID")
                );

            // Create index for TopicId
            migrationBuilder.CreateIndex(
                name: "IX_Lessons_TopicId",
                table: "Lessons",
                column: "TopicId");

            // Foreign key from Lessons to Topics (No cascade)
            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Topics_TopicId",
                table: "Lessons",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            // Foreign key from Topics to Subjects (No cascade)
            migrationBuilder.AddForeignKey(
                name: "FK_Topics_Subjects_SubjectId",
                table: "Topics",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Topics_TopicId",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Topics_Subjects_SubjectId",
                table: "Topics");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_TopicId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "TopicId",
                table: "Lessons");

            migrationBuilder.RenameColumn(
                name: "SubjectId",
                table: "Topics",
                newName: "LessonId");

            migrationBuilder.RenameIndex(
                name: "IX_Topics_SubjectId",
                table: "Topics",
                newName: "IX_Topics_LessonId");
        }
    }
}
