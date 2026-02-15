using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Adros.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class jjjsjjjsjjjaa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
         

            migrationBuilder.InsertData(
                table: "subscriptionPlans",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Description", "DurationInDays", "IsActive", "Name", "PlanType", "Price", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("19c2256d-9e6d-49ee-bbc8-4b82293f7999"), new DateTime(2026, 1, 7, 15, 34, 51, 702, DateTimeKind.Local).AddTicks(4523), new Guid("00000000-0000-0000-0000-000000000000"), false, "Basic monthly subscription", 30, true, "Basic Monthly", "monthly", 9.99m, null, null },
                    { new Guid("1e6010bb-3de2-4814-83f7-d405baa069a0"), new DateTime(2026, 1, 7, 15, 34, 51, 704, DateTimeKind.Local).AddTicks(8450), new Guid("00000000-0000-0000-0000-000000000000"), false, "Premium monthly subscription", 30, true, "Premium Monthly", "monthly", 19.99m, null, null },
                    { new Guid("ef4228be-bfba-426b-b48e-287e98c5641b"), new DateTime(2026, 1, 7, 15, 34, 51, 704, DateTimeKind.Local).AddTicks(8485), new Guid("00000000-0000-0000-0000-000000000000"), false, "Annual subscription with discount", 365, true, "Annual Plan", "annual", 99.99m, null, null }
                });
        }
    }
}
