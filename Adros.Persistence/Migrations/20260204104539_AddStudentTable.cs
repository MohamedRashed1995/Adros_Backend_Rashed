using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adros.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_Levels_LevelId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Users_ApplicationUserId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentProgresses_Student_StudentId",
                table: "StudentProgresses");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubscriptions_Student_StudentId",
                table: "StudentSubscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_VideoViews_Student_StudentId",
                table: "VideoViews");

            migrationBuilder.DropForeignKey(
                name: "FK_watchlater_Student_StudentId",
                table: "watchlater");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Student",
                table: "Student");

            migrationBuilder.RenameTable(
                name: "Student",
                newName: "Students");

            migrationBuilder.RenameIndex(
                name: "IX_Student_LevelId",
                table: "Students",
                newName: "IX_Students_LevelId");

            migrationBuilder.RenameIndex(
                name: "IX_Student_ApplicationUserId",
                table: "Students",
                newName: "IX_Students_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentProgresses_Students_StudentId",
                table: "StudentProgresses",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Levels_LevelId",
                table: "Students",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Users_ApplicationUserId",
                table: "Students",
                column: "ApplicationUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubscriptions_Students_StudentId",
                table: "StudentSubscriptions",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VideoViews_Students_StudentId",
                table: "VideoViews",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_watchlater_Students_StudentId",
                table: "watchlater",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentProgresses_Students_StudentId",
                table: "StudentProgresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Levels_LevelId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Users_ApplicationUserId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubscriptions_Students_StudentId",
                table: "StudentSubscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_VideoViews_Students_StudentId",
                table: "VideoViews");

            migrationBuilder.DropForeignKey(
                name: "FK_watchlater_Students_StudentId",
                table: "watchlater");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.RenameTable(
                name: "Students",
                newName: "Student");

            migrationBuilder.RenameIndex(
                name: "IX_Students_LevelId",
                table: "Student",
                newName: "IX_Student_LevelId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_ApplicationUserId",
                table: "Student",
                newName: "IX_Student_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Student",
                table: "Student",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Levels_LevelId",
                table: "Student",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Users_ApplicationUserId",
                table: "Student",
                column: "ApplicationUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentProgresses_Student_StudentId",
                table: "StudentProgresses",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubscriptions_Student_StudentId",
                table: "StudentSubscriptions",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VideoViews_Student_StudentId",
                table: "VideoViews",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_watchlater_Student_StudentId",
                table: "watchlater",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id");
        }
    }
}
