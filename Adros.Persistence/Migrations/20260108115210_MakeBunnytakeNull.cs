using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Adros.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MakeBunnytakeNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "BunnyVideoId",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "subscriptionPlans",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Description", "DurationInDays", "IsActive", "Name", "PlanType", "Price", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("1d96fef7-2728-4caa-9fb7-5aa1cdb0f90b"), new DateTime(2026, 1, 8, 13, 52, 9, 555, DateTimeKind.Local).AddTicks(466), new Guid("00000000-0000-0000-0000-000000000000"), false, "Basic monthly subscription", 30, true, "Basic Monthly", "monthly", 9.99m, null, null },
                    { new Guid("93e5f616-e374-4091-89e0-2f73a170b64c"), new DateTime(2026, 1, 8, 13, 52, 9, 557, DateTimeKind.Local).AddTicks(3287), new Guid("00000000-0000-0000-0000-000000000000"), false, "Annual subscription with discount", 365, true, "Annual Plan", "annual", 99.99m, null, null },
                    { new Guid("aaa1949a-bd3e-4f66-a7d7-72dad28c42ee"), new DateTime(2026, 1, 8, 13, 52, 9, 557, DateTimeKind.Local).AddTicks(3254), new Guid("00000000-0000-0000-0000-000000000000"), false, "Premium monthly subscription", 30, true, "Premium Monthly", "monthly", 19.99m, null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("1d96fef7-2728-4caa-9fb7-5aa1cdb0f90b"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("93e5f616-e374-4091-89e0-2f73a170b64c"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("aaa1949a-bd3e-4f66-a7d7-72dad28c42ee"));

            migrationBuilder.AlterColumn<string>(
                name: "BunnyVideoId",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
    }
}
