using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Adros.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class kkkkkkkkkkkkk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VariousSkillsViews_Users_UserId",
                table: "VariousSkillsViews");

            migrationBuilder.DropForeignKey(
                name: "FK_VariousSkillsViews_VariousSkills_VariousSkillId",
                table: "VariousSkillsViews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VariousSkillsViews",
                table: "VariousSkillsViews");

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("57e5e004-c6e7-43b8-baa9-7b08b6f71c81"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("78a10bde-e91e-4305-a1ef-71aa01bfc105"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("f18997a4-4d6e-4795-81c3-8f989c91f991"));

            migrationBuilder.RenameTable(
                name: "VariousSkillsViews",
                newName: "VariousSkillView");

            migrationBuilder.RenameIndex(
                name: "IX_VariousSkillsViews_VariousSkillId",
                table: "VariousSkillView",
                newName: "IX_VariousSkillView_VariousSkillId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VariousSkillView",
                table: "VariousSkillView",
                columns: new[] { "UserId", "VariousSkillId" });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VideoURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ViewsCount = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "subscriptionPlans",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Description", "DurationInDays", "IsActive", "Name", "PlanType", "Price", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("3956f135-a395-4f94-be9e-62b6a179343e"), new DateTime(2026, 1, 1, 13, 19, 50, 244, DateTimeKind.Local).AddTicks(4875), new Guid("00000000-0000-0000-0000-000000000000"), false, "Basic monthly subscription", 30, true, "Basic Monthly", "monthly", 9.99m, null, null },
                    { new Guid("552f04dc-43be-4cf2-9869-c37e0181921c"), new DateTime(2026, 1, 1, 13, 19, 50, 246, DateTimeKind.Local).AddTicks(5937), new Guid("00000000-0000-0000-0000-000000000000"), false, "Premium monthly subscription", 30, true, "Premium Monthly", "monthly", 19.99m, null, null },
                    { new Guid("eaa3260e-3ed2-4833-9354-afec16eda0be"), new DateTime(2026, 1, 1, 13, 19, 50, 246, DateTimeKind.Local).AddTicks(5972), new Guid("00000000-0000-0000-0000-000000000000"), false, "Annual subscription with discount", 365, true, "Annual Plan", "annual", 99.99m, null, null }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_VariousSkillView_Users_UserId",
                table: "VariousSkillView",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VariousSkillView_VariousSkills_VariousSkillId",
                table: "VariousSkillView",
                column: "VariousSkillId",
                principalTable: "VariousSkills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VariousSkillView_Users_UserId",
                table: "VariousSkillView");

            migrationBuilder.DropForeignKey(
                name: "FK_VariousSkillView_VariousSkills_VariousSkillId",
                table: "VariousSkillView");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VariousSkillView",
                table: "VariousSkillView");

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("3956f135-a395-4f94-be9e-62b6a179343e"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("552f04dc-43be-4cf2-9869-c37e0181921c"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("eaa3260e-3ed2-4833-9354-afec16eda0be"));

            migrationBuilder.RenameTable(
                name: "VariousSkillView",
                newName: "VariousSkillsViews");

            migrationBuilder.RenameIndex(
                name: "IX_VariousSkillView_VariousSkillId",
                table: "VariousSkillsViews",
                newName: "IX_VariousSkillsViews_VariousSkillId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VariousSkillsViews",
                table: "VariousSkillsViews",
                columns: new[] { "UserId", "VariousSkillId" });

            migrationBuilder.InsertData(
                table: "subscriptionPlans",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Description", "DurationInDays", "IsActive", "Name", "PlanType", "Price", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("57e5e004-c6e7-43b8-baa9-7b08b6f71c81"), new DateTime(2026, 1, 1, 8, 53, 42, 741, DateTimeKind.Local).AddTicks(5931), new Guid("00000000-0000-0000-0000-000000000000"), false, "Premium monthly subscription", 30, true, "Premium Monthly", "monthly", 19.99m, null, null },
                    { new Guid("78a10bde-e91e-4305-a1ef-71aa01bfc105"), new DateTime(2026, 1, 1, 8, 53, 42, 741, DateTimeKind.Local).AddTicks(6045), new Guid("00000000-0000-0000-0000-000000000000"), false, "Annual subscription with discount", 365, true, "Annual Plan", "annual", 99.99m, null, null },
                    { new Guid("f18997a4-4d6e-4795-81c3-8f989c91f991"), new DateTime(2026, 1, 1, 8, 53, 42, 737, DateTimeKind.Local).AddTicks(574), new Guid("00000000-0000-0000-0000-000000000000"), false, "Basic monthly subscription", 30, true, "Basic Monthly", "monthly", 9.99m, null, null }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_VariousSkillsViews_Users_UserId",
                table: "VariousSkillsViews",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VariousSkillsViews_VariousSkills_VariousSkillId",
                table: "VariousSkillsViews",
                column: "VariousSkillId",
                principalTable: "VariousSkills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
