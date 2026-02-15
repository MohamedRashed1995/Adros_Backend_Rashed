using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Adros.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class kkaskkakskka : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("195ec8c3-57f6-42c1-99b7-72e934f2035e"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("1cbb64a7-db25-47e1-86df-82a11ccc6396"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("5de3eef7-0779-4f20-bea4-3bf51fcf1154"));

            migrationBuilder.InsertData(
                table: "subscriptionPlans",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Description", "DurationInDays", "IsActive", "Name", "PlanType", "Price", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("cc99b3f9-217f-467a-8df2-304d91bc9a39"), new DateTime(2026, 1, 7, 17, 50, 31, 55, DateTimeKind.Local).AddTicks(3385), new Guid("00000000-0000-0000-0000-000000000000"), false, "Annual subscription with discount", 365, true, "Annual Plan", "annual", 99.99m, null, null },
                    { new Guid("cdbb16c2-29c7-4603-ab2a-13f510c875e5"), new DateTime(2026, 1, 7, 17, 50, 31, 55, DateTimeKind.Local).AddTicks(3166), new Guid("00000000-0000-0000-0000-000000000000"), false, "Premium monthly subscription", 30, true, "Premium Monthly", "monthly", 19.99m, null, null },
                    { new Guid("d788fe9c-e9f4-4ea4-a0d9-03cc91a5f881"), new DateTime(2026, 1, 7, 17, 50, 31, 51, DateTimeKind.Local).AddTicks(9971), new Guid("00000000-0000-0000-0000-000000000000"), false, "Basic monthly subscription", 30, true, "Basic Monthly", "monthly", 9.99m, null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("cc99b3f9-217f-467a-8df2-304d91bc9a39"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("cdbb16c2-29c7-4603-ab2a-13f510c875e5"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("d788fe9c-e9f4-4ea4-a0d9-03cc91a5f881"));

            migrationBuilder.InsertData(
                table: "subscriptionPlans",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Description", "DurationInDays", "IsActive", "Name", "PlanType", "Price", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("195ec8c3-57f6-42c1-99b7-72e934f2035e"), new DateTime(2026, 1, 7, 17, 44, 55, 581, DateTimeKind.Local).AddTicks(1053), new Guid("00000000-0000-0000-0000-000000000000"), false, "Basic monthly subscription", 30, true, "Basic Monthly", "monthly", 9.99m, null, null },
                    { new Guid("1cbb64a7-db25-47e1-86df-82a11ccc6396"), new DateTime(2026, 1, 7, 17, 44, 55, 584, DateTimeKind.Local).AddTicks(703), new Guid("00000000-0000-0000-0000-000000000000"), false, "Annual subscription with discount", 365, true, "Annual Plan", "annual", 99.99m, null, null },
                    { new Guid("5de3eef7-0779-4f20-bea4-3bf51fcf1154"), new DateTime(2026, 1, 7, 17, 44, 55, 584, DateTimeKind.Local).AddTicks(651), new Guid("00000000-0000-0000-0000-000000000000"), false, "Premium monthly subscription", 30, true, "Premium Monthly", "monthly", 19.99m, null, null }
                });
        }
    }
}
