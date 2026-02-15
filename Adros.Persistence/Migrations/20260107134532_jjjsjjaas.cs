using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Adros.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class jjjsjjaas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.InsertData(
                table: "subscriptionPlans",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Description", "DurationInDays", "IsActive", "Name", "PlanType", "Price", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("6b92de7f-fc65-495c-94b8-910a60e54a22"), new DateTime(2026, 1, 7, 15, 45, 31, 263, DateTimeKind.Local).AddTicks(5742), new Guid("00000000-0000-0000-0000-000000000000"), false, "Annual subscription with discount", 365, true, "Annual Plan", "annual", 99.99m, null, null },
                    { new Guid("ac041a83-539f-4140-a759-9c0fb2eb5326"), new DateTime(2026, 1, 7, 15, 45, 31, 263, DateTimeKind.Local).AddTicks(5708), new Guid("00000000-0000-0000-0000-000000000000"), false, "Premium monthly subscription", 30, true, "Premium Monthly", "monthly", 19.99m, null, null },
                    { new Guid("e912d884-b468-40b8-a96e-5b1d64c751e1"), new DateTime(2026, 1, 7, 15, 45, 31, 260, DateTimeKind.Local).AddTicks(7105), new Guid("00000000-0000-0000-0000-000000000000"), false, "Basic monthly subscription", 30, true, "Basic Monthly", "monthly", 9.99m, null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.InsertData(
                table: "subscriptionPlans",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Description", "DurationInDays", "IsActive", "Name", "PlanType", "Price", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("2d66a9bc-6e24-426a-9a32-6073a67ba805"), new DateTime(2026, 1, 7, 15, 39, 16, 662, DateTimeKind.Local).AddTicks(8724), new Guid("00000000-0000-0000-0000-000000000000"), false, "Annual subscription with discount", 365, true, "Annual Plan", "annual", 99.99m, null, null },
                    { new Guid("a9cc58ac-bc9f-4380-be5a-8093ed5cf32a"), new DateTime(2026, 1, 7, 15, 39, 16, 662, DateTimeKind.Local).AddTicks(8667), new Guid("00000000-0000-0000-0000-000000000000"), false, "Premium monthly subscription", 30, true, "Premium Monthly", "monthly", 19.99m, null, null },
                    { new Guid("ecfa0ac1-3793-4ebf-b0cf-eebfed7ce5d6"), new DateTime(2026, 1, 7, 15, 39, 16, 659, DateTimeKind.Local).AddTicks(3365), new Guid("00000000-0000-0000-0000-000000000000"), false, "Basic monthly subscription", 30, true, "Basic Monthly", "monthly", 9.99m, null, null }
                });
        }
    }
}
