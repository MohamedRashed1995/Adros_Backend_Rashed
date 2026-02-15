using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Adros.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUnitIdToLessons : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("46fcf328-7698-4c5d-b107-45371c3666d8"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("51ea46e2-db88-4a7c-a119-321f206106b7"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("e4b15461-a59c-422f-a474-11068ebc78e1"));

            migrationBuilder.InsertData(
                table: "subscriptionPlans",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Description", "DurationInDays", "IsActive", "Name", "PlanType", "Price", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("299d99fc-c025-45d4-be79-39a6a431216e"), new DateTime(2026, 1, 8, 15, 29, 53, 87, DateTimeKind.Local).AddTicks(5544), new Guid("00000000-0000-0000-0000-000000000000"), false, "Premium monthly subscription", 30, true, "Premium Monthly", "monthly", 19.99m, null, null },
                    { new Guid("30d55309-fa38-43b7-910b-0a1a7d0a327e"), new DateTime(2026, 1, 8, 15, 29, 53, 85, DateTimeKind.Local).AddTicks(2908), new Guid("00000000-0000-0000-0000-000000000000"), false, "Basic monthly subscription", 30, true, "Basic Monthly", "monthly", 9.99m, null, null },
                    { new Guid("e0419d92-ae8f-4778-ab5c-2a8d6657f4ea"), new DateTime(2026, 1, 8, 15, 29, 53, 87, DateTimeKind.Local).AddTicks(5579), new Guid("00000000-0000-0000-0000-000000000000"), false, "Annual subscription with discount", 365, true, "Annual Plan", "annual", 99.99m, null, null }
                });
            migrationBuilder.AddColumn<Guid>(
                    name: "UnitId",
                    table: "Lessons",
                    nullable: true);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("299d99fc-c025-45d4-be79-39a6a431216e"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("30d55309-fa38-43b7-910b-0a1a7d0a327e"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("e0419d92-ae8f-4778-ab5c-2a8d6657f4ea"));

            migrationBuilder.InsertData(
                table: "subscriptionPlans",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Description", "DurationInDays", "IsActive", "Name", "PlanType", "Price", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("46fcf328-7698-4c5d-b107-45371c3666d8"), new DateTime(2026, 1, 8, 15, 16, 34, 853, DateTimeKind.Local).AddTicks(9730), new Guid("00000000-0000-0000-0000-000000000000"), false, "Annual subscription with discount", 365, true, "Annual Plan", "annual", 99.99m, null, null },
                    { new Guid("51ea46e2-db88-4a7c-a119-321f206106b7"), new DateTime(2026, 1, 8, 15, 16, 34, 851, DateTimeKind.Local).AddTicks(7972), new Guid("00000000-0000-0000-0000-000000000000"), false, "Basic monthly subscription", 30, true, "Basic Monthly", "monthly", 9.99m, null, null },
                    { new Guid("e4b15461-a59c-422f-a474-11068ebc78e1"), new DateTime(2026, 1, 8, 15, 16, 34, 853, DateTimeKind.Local).AddTicks(9700), new Guid("00000000-0000-0000-0000-000000000000"), false, "Premium monthly subscription", 30, true, "Premium Monthly", "monthly", 19.99m, null, null }
                });
        }
    }
}
