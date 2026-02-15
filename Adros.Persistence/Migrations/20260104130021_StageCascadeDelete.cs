using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Adros.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class StageCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("650b45fb-743e-4c2d-846f-57178a2ab2ad"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("a7463ec5-45a1-4783-8633-a5140dc6e5da"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("df39759f-ca78-4e63-9171-9ddad0d8a83e"));

            migrationBuilder.InsertData(
                table: "subscriptionPlans",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Description", "DurationInDays", "IsActive", "Name", "PlanType", "Price", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("a3a872b2-cdaa-49f8-8006-896dd10d34ce"), new DateTime(2026, 1, 4, 15, 0, 20, 639, DateTimeKind.Local).AddTicks(282), new Guid("00000000-0000-0000-0000-000000000000"), false, "Premium monthly subscription", 30, true, "Premium Monthly", "monthly", 19.99m, null, null },
                    { new Guid("e4f7740e-7c5c-4d21-ad51-5e34c3934f27"), new DateTime(2026, 1, 4, 15, 0, 20, 636, DateTimeKind.Local).AddTicks(9092), new Guid("00000000-0000-0000-0000-000000000000"), false, "Basic monthly subscription", 30, true, "Basic Monthly", "monthly", 9.99m, null, null },
                    { new Guid("e87754cc-f9e1-4f85-b597-d0dfb33d2aaf"), new DateTime(2026, 1, 4, 15, 0, 20, 639, DateTimeKind.Local).AddTicks(314), new Guid("00000000-0000-0000-0000-000000000000"), false, "Annual subscription with discount", 365, true, "Annual Plan", "annual", 99.99m, null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                    { new Guid("650b45fb-743e-4c2d-846f-57178a2ab2ad"), new DateTime(2026, 1, 4, 14, 41, 19, 45, DateTimeKind.Local).AddTicks(7994), new Guid("00000000-0000-0000-0000-000000000000"), false, "Annual subscription with discount", 365, true, "Annual Plan", "annual", 99.99m, null, null },
                    { new Guid("a7463ec5-45a1-4783-8633-a5140dc6e5da"), new DateTime(2026, 1, 4, 14, 41, 19, 43, DateTimeKind.Local).AddTicks(7918), new Guid("00000000-0000-0000-0000-000000000000"), false, "Basic monthly subscription", 30, true, "Basic Monthly", "monthly", 9.99m, null, null },
                    { new Guid("df39759f-ca78-4e63-9171-9ddad0d8a83e"), new DateTime(2026, 1, 4, 14, 41, 19, 45, DateTimeKind.Local).AddTicks(7966), new Guid("00000000-0000-0000-0000-000000000000"), false, "Premium monthly subscription", 30, true, "Premium Monthly", "monthly", 19.99m, null, null }
                });
        }
    }
}
