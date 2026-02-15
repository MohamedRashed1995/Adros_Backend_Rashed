using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Adros.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Sob7anAlah : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("2abee02e-45d8-41fb-be6d-8cc3ced28a89"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("a80051b9-5fd6-429e-b9d1-c6214e7b8815"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("ac80dc25-37de-49f3-a6b4-dd9bc5c8a215"));

            migrationBuilder.InsertData(
                table: "subscriptionPlans",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Description", "DurationInDays", "IsActive", "Name", "PlanType", "Price", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("5530ad50-d924-460a-8a73-489c6fca97c0"), new DateTime(2025, 12, 30, 12, 21, 0, 236, DateTimeKind.Local).AddTicks(6371), new Guid("00000000-0000-0000-0000-000000000000"), false, "Basic monthly subscription", 30, true, "Basic Monthly", "monthly", 9.99m, null, null },
                    { new Guid("7c4e3b13-75d4-4df8-a35c-5dc2fc47bbd1"), new DateTime(2025, 12, 30, 12, 21, 0, 240, DateTimeKind.Local).AddTicks(26), new Guid("00000000-0000-0000-0000-000000000000"), false, "Annual subscription with discount", 365, true, "Annual Plan", "annual", 99.99m, null, null },
                    { new Guid("a90d0b33-6785-4ea7-8ac9-9a6eccd99256"), new DateTime(2025, 12, 30, 12, 21, 0, 239, DateTimeKind.Local).AddTicks(9958), new Guid("00000000-0000-0000-0000-000000000000"), false, "Premium monthly subscription", 30, true, "Premium Monthly", "monthly", 19.99m, null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("5530ad50-d924-460a-8a73-489c6fca97c0"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("7c4e3b13-75d4-4df8-a35c-5dc2fc47bbd1"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("a90d0b33-6785-4ea7-8ac9-9a6eccd99256"));

            migrationBuilder.InsertData(
                table: "subscriptionPlans",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Description", "DurationInDays", "IsActive", "Name", "PlanType", "Price", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("2abee02e-45d8-41fb-be6d-8cc3ced28a89"), new DateTime(2025, 12, 30, 12, 13, 36, 643, DateTimeKind.Local).AddTicks(9971), new Guid("00000000-0000-0000-0000-000000000000"), false, "Basic monthly subscription", 30, true, "Basic Monthly", "monthly", 9.99m, null, null },
                    { new Guid("a80051b9-5fd6-429e-b9d1-c6214e7b8815"), new DateTime(2025, 12, 30, 12, 13, 36, 646, DateTimeKind.Local).AddTicks(8437), new Guid("00000000-0000-0000-0000-000000000000"), false, "Annual subscription with discount", 365, true, "Annual Plan", "annual", 99.99m, null, null },
                    { new Guid("ac80dc25-37de-49f3-a6b4-dd9bc5c8a215"), new DateTime(2025, 12, 30, 12, 13, 36, 646, DateTimeKind.Local).AddTicks(8394), new Guid("00000000-0000-0000-0000-000000000000"), false, "Premium monthly subscription", 30, true, "Premium Monthly", "monthly", 19.99m, null, null }
                });
        }
    }
}
