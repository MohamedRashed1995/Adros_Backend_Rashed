using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Adros.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MakeTopicIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Subjects_SubjectId",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_SubjectId",
                table: "Lessons");

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("5b7d5655-4956-4156-805c-b0d805f0c4c4"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("b586f386-9964-43a0-8f55-7e838c49177a"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("f907a3b9-ea35-43dc-ad91-17f333ccc59e"));

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Lessons");

            migrationBuilder.InsertData(
                table: "subscriptionPlans",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Description", "DurationInDays", "IsActive", "Name", "PlanType", "Price", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("9dadca4e-bb61-44e9-bfcd-eef13ca3ed27"), new DateTime(2026, 1, 7, 16, 19, 39, 658, DateTimeKind.Local).AddTicks(472), new Guid("00000000-0000-0000-0000-000000000000"), false, "Basic monthly subscription", 30, true, "Basic Monthly", "monthly", 9.99m, null, null },
                    { new Guid("c8b93147-b3b6-4193-8520-62375fc3fb71"), new DateTime(2026, 1, 7, 16, 19, 39, 660, DateTimeKind.Local).AddTicks(6923), new Guid("00000000-0000-0000-0000-000000000000"), false, "Annual subscription with discount", 365, true, "Annual Plan", "annual", 99.99m, null, null },
                    { new Guid("f239b155-ae15-46a8-ab70-abae6f4aa867"), new DateTime(2026, 1, 7, 16, 19, 39, 660, DateTimeKind.Local).AddTicks(6798), new Guid("00000000-0000-0000-0000-000000000000"), false, "Premium monthly subscription", 30, true, "Premium Monthly", "monthly", 19.99m, null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("9dadca4e-bb61-44e9-bfcd-eef13ca3ed27"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("c8b93147-b3b6-4193-8520-62375fc3fb71"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("f239b155-ae15-46a8-ab70-abae6f4aa867"));

            migrationBuilder.AddColumn<Guid>(
                name: "SubjectId",
                table: "Lessons",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "subscriptionPlans",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Description", "DurationInDays", "IsActive", "Name", "PlanType", "Price", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("5b7d5655-4956-4156-805c-b0d805f0c4c4"), new DateTime(2026, 1, 7, 16, 5, 8, 129, DateTimeKind.Local).AddTicks(1010), new Guid("00000000-0000-0000-0000-000000000000"), false, "Premium monthly subscription", 30, true, "Premium Monthly", "monthly", 19.99m, null, null },
                    { new Guid("b586f386-9964-43a0-8f55-7e838c49177a"), new DateTime(2026, 1, 7, 16, 5, 8, 129, DateTimeKind.Local).AddTicks(1067), new Guid("00000000-0000-0000-0000-000000000000"), false, "Annual subscription with discount", 365, true, "Annual Plan", "annual", 99.99m, null, null },
                    { new Guid("f907a3b9-ea35-43dc-ad91-17f333ccc59e"), new DateTime(2026, 1, 7, 16, 5, 8, 125, DateTimeKind.Local).AddTicks(4396), new Guid("00000000-0000-0000-0000-000000000000"), false, "Basic monthly subscription", 30, true, "Basic Monthly", "monthly", 9.99m, null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_SubjectId",
                table: "Lessons",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Subjects_SubjectId",
                table: "Lessons",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id");
        }
    }
}
