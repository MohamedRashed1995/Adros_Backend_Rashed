using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Adros.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class jhasjdhakjshdadsasss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("c8052a50-7aa4-4e51-9d6c-c84f05338983"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("dfdcc957-809f-4e1e-a1f5-dbc0de6491d5"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("ee7fa4d2-d2ba-461c-a33b-e4a72406b3d6"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "subscriptionPlans",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Description", "DurationInDays", "IsActive", "Name", "PlanType", "Price", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("c8052a50-7aa4-4e51-9d6c-c84f05338983"), new DateTime(2025, 12, 30, 14, 2, 24, 444, DateTimeKind.Local).AddTicks(6207), new Guid("00000000-0000-0000-0000-000000000000"), false, "Premium monthly subscription", 30, true, "Premium Monthly", "monthly", 19.99m, null, null },
                    { new Guid("dfdcc957-809f-4e1e-a1f5-dbc0de6491d5"), new DateTime(2025, 12, 30, 14, 2, 24, 444, DateTimeKind.Local).AddTicks(6268), new Guid("00000000-0000-0000-0000-000000000000"), false, "Annual subscription with discount", 365, true, "Annual Plan", "annual", 99.99m, null, null },
                    { new Guid("ee7fa4d2-d2ba-461c-a33b-e4a72406b3d6"), new DateTime(2025, 12, 30, 14, 2, 24, 438, DateTimeKind.Local).AddTicks(7642), new Guid("00000000-0000-0000-0000-000000000000"), false, "Basic monthly subscription", 30, true, "Basic Monthly", "monthly", 9.99m, null, null }
                });
        }
    }
}
