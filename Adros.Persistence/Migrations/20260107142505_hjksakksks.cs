using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Adros.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class hjksakksks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("9dadca4e-bb61-44e9-bfcd-eef13ca3ed27"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("c8b93147-b3b6-4193-8520-62375fc3fb71"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("f239b155-ae15-46a8-ab70-abae6f4aa867"));

            migrationBuilder.InsertData(
                table: "subscriptionPlans",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Description", "DurationInDays", "IsActive", "Name", "PlanType", "Price", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("b9d6acf3-2473-4823-bec3-52a274940bc8"), new DateTime(2026, 1, 7, 16, 25, 5, 88, DateTimeKind.Local).AddTicks(1372), new Guid("00000000-0000-0000-0000-000000000000"), false, "Basic monthly subscription", 30, true, "Basic Monthly", "monthly", 9.99m, null, null },
                    { new Guid("ed175958-4634-41ca-996d-279ff39ac705"), new DateTime(2026, 1, 7, 16, 25, 5, 91, DateTimeKind.Local).AddTicks(2697), new Guid("00000000-0000-0000-0000-000000000000"), false, "Annual subscription with discount", 365, true, "Annual Plan", "annual", 99.99m, null, null },
                    { new Guid("ed31c470-0778-4aff-9102-3910cd6af752"), new DateTime(2026, 1, 7, 16, 25, 5, 91, DateTimeKind.Local).AddTicks(2653), new Guid("00000000-0000-0000-0000-000000000000"), false, "Premium monthly subscription", 30, true, "Premium Monthly", "monthly", 19.99m, null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("b9d6acf3-2473-4823-bec3-52a274940bc8"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("ed175958-4634-41ca-996d-279ff39ac705"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("ed31c470-0778-4aff-9102-3910cd6af752"));

            migrationBuilder.InsertData(
                table: "subscriptionPlans",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Description", "DurationInDays", "IsActive", "Name", "PlanType", "Price", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("9dadca4e-bb61-44e9-bfcd-eef13ca3ed27"), new DateTime(2026, 1, 7, 16, 19, 39, 658, DateTimeKind.Local).AddTicks(472), new Guid("00000000-0000-0000-0000-000000000000"), false, "Basic monthly subscription", 30, true, "Basic Monthly", "monthly", 9.99m, null, null },
                    { new Guid("c8b93147-b3b6-4193-8520-62375fc3fb71"), new DateTime(2026, 1, 7, 16, 19, 39, 660, DateTimeKind.Local).AddTicks(6923), new Guid("00000000-0000-0000-0000-000000000000"), false, "Annual subscription with discount", 365, true, "Annual Plan", "annual", 99.99m, null, null },
                    { new Guid("f239b155-ae15-46a8-ab70-abae6f4aa867"), new DateTime(2026, 1, 7, 16, 19, 39, 660, DateTimeKind.Local).AddTicks(6798), new Guid("00000000-0000-0000-0000-000000000000"), false, "Premium monthly subscription", 30, true, "Premium Monthly", "monthly", 19.99m, null, null }
                });
        }
    }
}
