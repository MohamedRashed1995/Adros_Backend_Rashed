using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Adros.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class jahsdkjhaskjdhad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Levels",
                type: "nvarchar(max)",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Levels");

            migrationBuilder.InsertData(
                table: "subscriptionPlans",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Description", "DurationInDays", "IsActive", "Name", "PlanType", "Price", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("009ee745-6861-46ee-a60b-6d39ac1042a9"), new DateTime(2026, 1, 4, 16, 23, 58, 175, DateTimeKind.Local).AddTicks(8887), new Guid("00000000-0000-0000-0000-000000000000"), false, "Basic monthly subscription", 30, true, "Basic Monthly", "monthly", 9.99m, null, null },
                    { new Guid("03fd35a7-b165-4c50-b552-c63bed1a242b"), new DateTime(2026, 1, 4, 16, 23, 58, 180, DateTimeKind.Local).AddTicks(1955), new Guid("00000000-0000-0000-0000-000000000000"), false, "Annual subscription with discount", 365, true, "Annual Plan", "annual", 99.99m, null, null },
                    { new Guid("c667ffc2-85ec-4010-9556-487ccaa30df7"), new DateTime(2026, 1, 4, 16, 23, 58, 180, DateTimeKind.Local).AddTicks(1893), new Guid("00000000-0000-0000-0000-000000000000"), false, "Premium monthly subscription", 30, true, "Premium Monthly", "monthly", 19.99m, null, null }
                });
        }
    }
}
