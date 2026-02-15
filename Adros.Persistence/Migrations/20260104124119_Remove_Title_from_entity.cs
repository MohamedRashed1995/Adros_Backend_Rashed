using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Adros.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Remove_Title_from_entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("3956f135-a395-4f94-be9e-62b6a179343e"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("552f04dc-43be-4cf2-9869-c37e0181921c"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("eaa3260e-3ed2-4833-9354-afec16eda0be"));

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Banners");

            migrationBuilder.AlterColumn<string>(
                name: "ImageName",
                table: "Stages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "ImageName",
                table: "Stages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
                    { new Guid("3956f135-a395-4f94-be9e-62b6a179343e"), new DateTime(2026, 1, 1, 13, 19, 50, 244, DateTimeKind.Local).AddTicks(4875), new Guid("00000000-0000-0000-0000-000000000000"), false, "Basic monthly subscription", 30, true, "Basic Monthly", "monthly", 9.99m, null, null },
                    { new Guid("552f04dc-43be-4cf2-9869-c37e0181921c"), new DateTime(2026, 1, 1, 13, 19, 50, 246, DateTimeKind.Local).AddTicks(5937), new Guid("00000000-0000-0000-0000-000000000000"), false, "Premium monthly subscription", 30, true, "Premium Monthly", "monthly", 19.99m, null, null },
                    { new Guid("eaa3260e-3ed2-4833-9354-afec16eda0be"), new DateTime(2026, 1, 1, 13, 19, 50, 246, DateTimeKind.Local).AddTicks(5972), new Guid("00000000-0000-0000-0000-000000000000"), false, "Annual subscription with discount", 365, true, "Annual Plan", "annual", 99.99m, null, null }
                });
        }
    }
}
