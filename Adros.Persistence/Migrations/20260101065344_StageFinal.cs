using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Adros.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class StageFinal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Stages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "subscriptionPlans",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Description", "DurationInDays", "IsActive", "Name", "PlanType", "Price", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("57e5e004-c6e7-43b8-baa9-7b08b6f71c81"), new DateTime(2026, 1, 1, 8, 53, 42, 741, DateTimeKind.Local).AddTicks(5931), new Guid("00000000-0000-0000-0000-000000000000"), false, "Premium monthly subscription", 30, true, "Premium Monthly", "monthly", 19.99m, null, null },
                    { new Guid("78a10bde-e91e-4305-a1ef-71aa01bfc105"), new DateTime(2026, 1, 1, 8, 53, 42, 741, DateTimeKind.Local).AddTicks(6045), new Guid("00000000-0000-0000-0000-000000000000"), false, "Annual subscription with discount", 365, true, "Annual Plan", "annual", 99.99m, null, null },
                    { new Guid("f18997a4-4d6e-4795-81c3-8f989c91f991"), new DateTime(2026, 1, 1, 8, 53, 42, 737, DateTimeKind.Local).AddTicks(574), new Guid("00000000-0000-0000-0000-000000000000"), false, "Basic monthly subscription", 30, true, "Basic Monthly", "monthly", 9.99m, null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("57e5e004-c6e7-43b8-baa9-7b08b6f71c81"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("78a10bde-e91e-4305-a1ef-71aa01bfc105"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("f18997a4-4d6e-4795-81c3-8f989c91f991"));

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Stages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
    }
}
