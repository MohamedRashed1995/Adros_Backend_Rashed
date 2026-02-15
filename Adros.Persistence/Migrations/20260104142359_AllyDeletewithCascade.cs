using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Adros.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AllyDeletewithCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_Levels_LevelId",
                table: "Student");

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("a3a872b2-cdaa-49f8-8006-896dd10d34ce"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("e4f7740e-7c5c-4d21-ad51-5e34c3934f27"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("e87754cc-f9e1-4f85-b597-d0dfb33d2aaf"));

            migrationBuilder.InsertData(
                table: "subscriptionPlans",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Description", "DurationInDays", "IsActive", "Name", "PlanType", "Price", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("009ee745-6861-46ee-a60b-6d39ac1042a9"), new DateTime(2026, 1, 4, 16, 23, 58, 175, DateTimeKind.Local).AddTicks(8887), new Guid("00000000-0000-0000-0000-000000000000"), false, "Basic monthly subscription", 30, true, "Basic Monthly", "monthly", 9.99m, null, null },
                    { new Guid("03fd35a7-b165-4c50-b552-c63bed1a242b"), new DateTime(2026, 1, 4, 16, 23, 58, 180, DateTimeKind.Local).AddTicks(1955), new Guid("00000000-0000-0000-0000-000000000000"), false, "Annual subscription with discount", 365, true, "Annual Plan", "annual", 99.99m, null, null },
                    { new Guid("c667ffc2-85ec-4010-9556-487ccaa30df7"), new DateTime(2026, 1, 4, 16, 23, 58, 180, DateTimeKind.Local).AddTicks(1893), new Guid("00000000-0000-0000-0000-000000000000"), false, "Premium monthly subscription", 30, true, "Premium Monthly", "monthly", 19.99m, null, null }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Levels_LevelId",
                table: "Student",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_Levels_LevelId",
                table: "Student");

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("009ee745-6861-46ee-a60b-6d39ac1042a9"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("03fd35a7-b165-4c50-b552-c63bed1a242b"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("c667ffc2-85ec-4010-9556-487ccaa30df7"));

            migrationBuilder.InsertData(
                table: "subscriptionPlans",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Description", "DurationInDays", "IsActive", "Name", "PlanType", "Price", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("a3a872b2-cdaa-49f8-8006-896dd10d34ce"), new DateTime(2026, 1, 4, 15, 0, 20, 639, DateTimeKind.Local).AddTicks(282), new Guid("00000000-0000-0000-0000-000000000000"), false, "Premium monthly subscription", 30, true, "Premium Monthly", "monthly", 19.99m, null, null },
                    { new Guid("e4f7740e-7c5c-4d21-ad51-5e34c3934f27"), new DateTime(2026, 1, 4, 15, 0, 20, 636, DateTimeKind.Local).AddTicks(9092), new Guid("00000000-0000-0000-0000-000000000000"), false, "Basic monthly subscription", 30, true, "Basic Monthly", "monthly", 9.99m, null, null },
                    { new Guid("e87754cc-f9e1-4f85-b597-d0dfb33d2aaf"), new DateTime(2026, 1, 4, 15, 0, 20, 639, DateTimeKind.Local).AddTicks(314), new Guid("00000000-0000-0000-0000-000000000000"), false, "Annual subscription with discount", 365, true, "Annual Plan", "annual", 99.99m, null, null }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Levels_LevelId",
                table: "Student",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "Id");
        }
    }
}
