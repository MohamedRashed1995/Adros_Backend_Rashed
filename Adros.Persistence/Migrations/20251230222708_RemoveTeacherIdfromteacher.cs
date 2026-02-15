using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Adros.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTeacherIdfromteacher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("3ba7ddf7-f9e3-40c1-9549-acfeb38d79b0"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("43195e8a-d2f8-4d8d-be05-d16029de665c"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("57bc1e49-d0e2-4e08-a886-156132587aaf"));

            migrationBuilder.DropColumn(
                name: "TeacherID",
                table: "Teacher");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<Guid>(
                name: "TeacherID",
                table: "Teacher",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "subscriptionPlans",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Description", "DurationInDays", "IsActive", "Name", "PlanType", "Price", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("3ba7ddf7-f9e3-40c1-9549-acfeb38d79b0"), new DateTime(2025, 12, 30, 15, 48, 51, 291, DateTimeKind.Local).AddTicks(7869), new Guid("00000000-0000-0000-0000-000000000000"), false, "Annual subscription with discount", 365, true, "Annual Plan", "annual", 99.99m, null, null },
                    { new Guid("43195e8a-d2f8-4d8d-be05-d16029de665c"), new DateTime(2025, 12, 30, 15, 48, 51, 284, DateTimeKind.Local).AddTicks(9495), new Guid("00000000-0000-0000-0000-000000000000"), false, "Basic monthly subscription", 30, true, "Basic Monthly", "monthly", 9.99m, null, null },
                    { new Guid("57bc1e49-d0e2-4e08-a886-156132587aaf"), new DateTime(2025, 12, 30, 15, 48, 51, 291, DateTimeKind.Local).AddTicks(7781), new Guid("00000000-0000-0000-0000-000000000000"), false, "Premium monthly subscription", 30, true, "Premium Monthly", "monthly", 19.99m, null, null }
                });
        }
    }
}
