using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Adros.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialClean : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("1706193a-870d-4d9f-8f7c-2461269ff9de"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("3f5cc1b4-d993-4f1f-ae7d-cacfe69d32c2"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("51083dd1-61da-48eb-b97f-ab4672f85edb"));

            migrationBuilder.InsertData(
                table: "subscriptionPlans",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Description", "DurationInDays", "IsActive", "Name", "PlanType", "Price", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("3bb6a146-34a5-4063-ad3b-dc305274e6f1"), new DateTime(2026, 1, 6, 14, 45, 38, 907, DateTimeKind.Local).AddTicks(1491), new Guid("00000000-0000-0000-0000-000000000000"), false, "Basic monthly subscription", 30, true, "Basic Monthly", "monthly", 9.99m, null, null },
                    { new Guid("3ce98836-deba-482a-bd2b-331f55e79952"), new DateTime(2026, 1, 6, 14, 45, 38, 910, DateTimeKind.Local).AddTicks(3745), new Guid("00000000-0000-0000-0000-000000000000"), false, "Annual subscription with discount", 365, true, "Annual Plan", "annual", 99.99m, null, null },
                    { new Guid("a262ba82-e96d-40a6-ba74-0d2d3ee1d69c"), new DateTime(2026, 1, 6, 14, 45, 38, 910, DateTimeKind.Local).AddTicks(3701), new Guid("00000000-0000-0000-0000-000000000000"), false, "Premium monthly subscription", 30, true, "Premium Monthly", "monthly", 19.99m, null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("3bb6a146-34a5-4063-ad3b-dc305274e6f1"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("3ce98836-deba-482a-bd2b-331f55e79952"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("a262ba82-e96d-40a6-ba74-0d2d3ee1d69c"));

            migrationBuilder.InsertData(
                table: "subscriptionPlans",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Description", "DurationInDays", "IsActive", "Name", "PlanType", "Price", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("1706193a-870d-4d9f-8f7c-2461269ff9de"), new DateTime(2026, 1, 4, 17, 30, 30, 813, DateTimeKind.Local).AddTicks(8809), new Guid("00000000-0000-0000-0000-000000000000"), false, "Premium monthly subscription", 30, true, "Premium Monthly", "monthly", 19.99m, null, null },
                    { new Guid("3f5cc1b4-d993-4f1f-ae7d-cacfe69d32c2"), new DateTime(2026, 1, 4, 17, 30, 30, 813, DateTimeKind.Local).AddTicks(8853), new Guid("00000000-0000-0000-0000-000000000000"), false, "Annual subscription with discount", 365, true, "Annual Plan", "annual", 99.99m, null, null },
                    { new Guid("51083dd1-61da-48eb-b97f-ab4672f85edb"), new DateTime(2026, 1, 4, 17, 30, 30, 811, DateTimeKind.Local).AddTicks(9067), new Guid("00000000-0000-0000-0000-000000000000"), false, "Basic monthly subscription", 30, true, "Basic Monthly", "monthly", 9.99m, null, null }
                });
        }
    }
}
