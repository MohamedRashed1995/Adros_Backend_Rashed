using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adros.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddStagetoTeacher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StageId",
                table: "Teachers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_StageId",
                table: "Teachers",
                column: "StageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Stages_StageId",
                table: "Teachers",
                column: "StageId",
                principalTable: "Stages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Stages_StageId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_StageId",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "StageId",
                table: "Teachers");
        }
    }
}
