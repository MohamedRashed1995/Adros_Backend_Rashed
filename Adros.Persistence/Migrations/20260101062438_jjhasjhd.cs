using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Adros.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class jjhasjhd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("43209994-41d0-4d64-b5bf-3699ec5ff22c"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("4cdeaaee-cc91-4d95-84fe-5a67770aca93"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("56f089dd-a447-4df5-8039-db38809a454c"));

            migrationBuilder.AlterColumn<string>(
                name: "ImageName",
                table: "Banners",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Banners",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "subscriptionPlans",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Description", "DurationInDays", "IsActive", "Name", "PlanType", "Price", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("a35ae9a9-e91d-4cd0-b01d-923ac9e5e42c"), new DateTime(2026, 1, 1, 8, 24, 36, 780, DateTimeKind.Local).AddTicks(8976), new Guid("00000000-0000-0000-0000-000000000000"), false, "Premium monthly subscription", 30, true, "Premium Monthly", "monthly", 19.99m, null, null },
                    { new Guid("c87c0a1b-f182-4bd2-8138-6c8f3bbbe28e"), new DateTime(2026, 1, 1, 8, 24, 36, 780, DateTimeKind.Local).AddTicks(9068), new Guid("00000000-0000-0000-0000-000000000000"), false, "Annual subscription with discount", 365, true, "Annual Plan", "annual", 99.99m, null, null },
                    { new Guid("fdf63b38-2bfd-43d4-bd79-8b9bf53ddab7"), new DateTime(2026, 1, 1, 8, 24, 36, 777, DateTimeKind.Local).AddTicks(3449), new Guid("00000000-0000-0000-0000-000000000000"), false, "Basic monthly subscription", 30, true, "Basic Monthly", "monthly", 9.99m, null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("a35ae9a9-e91d-4cd0-b01d-923ac9e5e42c"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("c87c0a1b-f182-4bd2-8138-6c8f3bbbe28e"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("fdf63b38-2bfd-43d4-bd79-8b9bf53ddab7"));

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Banners");

            migrationBuilder.AlterColumn<string>(
                name: "ImageName",
                table: "Banners",
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
                    { new Guid("43209994-41d0-4d64-b5bf-3699ec5ff22c"), new DateTime(2025, 12, 31, 0, 27, 7, 409, DateTimeKind.Local).AddTicks(5731), new Guid("00000000-0000-0000-0000-000000000000"), false, "Basic monthly subscription", 30, true, "Basic Monthly", "monthly", 9.99m, null, null },
                    { new Guid("4cdeaaee-cc91-4d95-84fe-5a67770aca93"), new DateTime(2025, 12, 31, 0, 27, 7, 417, DateTimeKind.Local).AddTicks(4015), new Guid("00000000-0000-0000-0000-000000000000"), false, "Annual subscription with discount", 365, true, "Annual Plan", "annual", 99.99m, null, null },
                    { new Guid("56f089dd-a447-4df5-8039-db38809a454c"), new DateTime(2025, 12, 31, 0, 27, 7, 417, DateTimeKind.Local).AddTicks(3908), new Guid("00000000-0000-0000-0000-000000000000"), false, "Premium monthly subscription", 30, true, "Premium Monthly", "monthly", 19.99m, null, null }
                });
        }
    }
}
