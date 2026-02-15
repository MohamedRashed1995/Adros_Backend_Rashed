using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Adros.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class llappakdkkd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("25660276-7b3d-4603-b2a5-e5ff0b0a5ca4"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("5947a3fa-9ba7-4b87-bc22-896caed26e16"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("69853aeb-6aac-41db-a93a-22773dc61774"));

            migrationBuilder.InsertData(
                table: "subscriptionPlans",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Description", "DurationInDays", "IsActive", "Name", "PlanType", "Price", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("cc99b3f9-217f-467a-8df2-304d91bc9a39"), new DateTime(2026, 1, 7, 17, 50, 31, 55, DateTimeKind.Local).AddTicks(3385), new Guid("00000000-0000-0000-0000-000000000000"), false, "Annual subscription with discount", 365, true, "Annual Plan", "annual", 99.99m, null, null },
                    { new Guid("cdbb16c2-29c7-4603-ab2a-13f510c875e5"), new DateTime(2026, 1, 7, 17, 50, 31, 55, DateTimeKind.Local).AddTicks(3166), new Guid("00000000-0000-0000-0000-000000000000"), false, "Premium monthly subscription", 30, true, "Premium Monthly", "monthly", 19.99m, null, null },
                    { new Guid("d788fe9c-e9f4-4ea4-a0d9-03cc91a5f881"), new DateTime(2026, 1, 7, 17, 50, 31, 51, DateTimeKind.Local).AddTicks(9971), new Guid("00000000-0000-0000-0000-000000000000"), false, "Basic monthly subscription", 30, true, "Basic Monthly", "monthly", 9.99m, null, null }
                });
        }
    }
}
