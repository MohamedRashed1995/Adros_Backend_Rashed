using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Adros.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTopictounit2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("16c25a5f-e3a1-4cda-ae9d-06e91743ca48"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("c066daf3-ab82-4358-b908-b501e9beb622"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("fc87f15a-d553-4685-9d08-7166424b0e94"));

            migrationBuilder.InsertData(
                table: "subscriptionPlans",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Description", "DurationInDays", "IsActive", "Name", "PlanType", "Price", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("33382f5d-5e34-45ad-8db9-226335ee023c"), new DateTime(2026, 1, 8, 12, 52, 45, 444, DateTimeKind.Local).AddTicks(330), new Guid("00000000-0000-0000-0000-000000000000"), false, "Basic monthly subscription", 30, true, "Basic Monthly", "monthly", 9.99m, null, null },
                    { new Guid("58b5a220-ffea-4dcd-b7ae-2d39d79fac75"), new DateTime(2026, 1, 8, 12, 52, 45, 448, DateTimeKind.Local).AddTicks(1811), new Guid("00000000-0000-0000-0000-000000000000"), false, "Premium monthly subscription", 30, true, "Premium Monthly", "monthly", 19.99m, null, null },
                    { new Guid("f670d413-0f4b-4e3e-b6d9-b198ac613eb7"), new DateTime(2026, 1, 8, 12, 52, 45, 448, DateTimeKind.Local).AddTicks(1925), new Guid("00000000-0000-0000-0000-000000000000"), false, "Annual subscription with discount", 365, true, "Annual Plan", "annual", 99.99m, null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("33382f5d-5e34-45ad-8db9-226335ee023c"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("58b5a220-ffea-4dcd-b7ae-2d39d79fac75"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("f670d413-0f4b-4e3e-b6d9-b198ac613eb7"));

            migrationBuilder.InsertData(
                table: "subscriptionPlans",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Description", "DurationInDays", "IsActive", "Name", "PlanType", "Price", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("16c25a5f-e3a1-4cda-ae9d-06e91743ca48"), new DateTime(2026, 1, 8, 12, 40, 15, 502, DateTimeKind.Local).AddTicks(2395), new Guid("00000000-0000-0000-0000-000000000000"), false, "Annual subscription with discount", 365, true, "Annual Plan", "annual", 99.99m, null, null },
                    { new Guid("c066daf3-ab82-4358-b908-b501e9beb622"), new DateTime(2026, 1, 8, 12, 40, 15, 502, DateTimeKind.Local).AddTicks(2350), new Guid("00000000-0000-0000-0000-000000000000"), false, "Premium monthly subscription", 30, true, "Premium Monthly", "monthly", 19.99m, null, null },
                    { new Guid("fc87f15a-d553-4685-9d08-7166424b0e94"), new DateTime(2026, 1, 8, 12, 40, 15, 498, DateTimeKind.Local).AddTicks(8726), new Guid("00000000-0000-0000-0000-000000000000"), false, "Basic monthly subscription", 30, true, "Basic Monthly", "monthly", 9.99m, null, null }
                });
        }
    }
}
