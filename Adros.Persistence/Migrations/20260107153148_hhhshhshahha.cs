using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Adros.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class hhhshhshahha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("04f92952-64d4-4aaf-9ee8-9edfb80a20df"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("7773b20c-b672-4f75-8cc2-9d0f9d553099"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("af7739c4-e5a3-4a36-b8d2-1b021eae0d68"));

            migrationBuilder.InsertData(
                table: "subscriptionPlans",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Description", "DurationInDays", "IsActive", "Name", "PlanType", "Price", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("02d47c14-a1ae-4007-93ac-882963507349"), new DateTime(2026, 1, 7, 17, 31, 47, 569, DateTimeKind.Local).AddTicks(4952), new Guid("00000000-0000-0000-0000-000000000000"), false, "Annual subscription with discount", 365, true, "Annual Plan", "annual", 99.99m, null, null },
                    { new Guid("45c01355-6abc-4acf-b276-e2bf12be1cf7"), new DateTime(2026, 1, 7, 17, 31, 47, 569, DateTimeKind.Local).AddTicks(4873), new Guid("00000000-0000-0000-0000-000000000000"), false, "Premium monthly subscription", 30, true, "Premium Monthly", "monthly", 19.99m, null, null },
                    { new Guid("e161707a-d324-406c-8595-b707442e8882"), new DateTime(2026, 1, 7, 17, 31, 47, 566, DateTimeKind.Local).AddTicks(3291), new Guid("00000000-0000-0000-0000-000000000000"), false, "Basic monthly subscription", 30, true, "Basic Monthly", "monthly", 9.99m, null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("02d47c14-a1ae-4007-93ac-882963507349"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("45c01355-6abc-4acf-b276-e2bf12be1cf7"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("e161707a-d324-406c-8595-b707442e8882"));

            migrationBuilder.InsertData(
                table: "subscriptionPlans",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Description", "DurationInDays", "IsActive", "Name", "PlanType", "Price", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("04f92952-64d4-4aaf-9ee8-9edfb80a20df"), new DateTime(2026, 1, 7, 16, 35, 58, 718, DateTimeKind.Local).AddTicks(9055), new Guid("00000000-0000-0000-0000-000000000000"), false, "Basic monthly subscription", 30, true, "Basic Monthly", "monthly", 9.99m, null, null },
                    { new Guid("7773b20c-b672-4f75-8cc2-9d0f9d553099"), new DateTime(2026, 1, 7, 16, 35, 58, 721, DateTimeKind.Local).AddTicks(4237), new Guid("00000000-0000-0000-0000-000000000000"), false, "Annual subscription with discount", 365, true, "Annual Plan", "annual", 99.99m, null, null },
                    { new Guid("af7739c4-e5a3-4a36-b8d2-1b021eae0d68"), new DateTime(2026, 1, 7, 16, 35, 58, 721, DateTimeKind.Local).AddTicks(4201), new Guid("00000000-0000-0000-0000-000000000000"), false, "Premium monthly subscription", 30, true, "Premium Monthly", "monthly", 19.99m, null, null }
                });
        }
    }
}
