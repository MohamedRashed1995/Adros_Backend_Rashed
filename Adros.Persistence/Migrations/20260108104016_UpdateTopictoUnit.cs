using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Adros.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTopictoUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Topics_TopicId",
                table: "Videos");

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

            migrationBuilder.RenameColumn(
                name: "TopicId",
                table: "Videos",
                newName: "UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_Videos_TopicId",
                table: "Videos",
                newName: "IX_Videos_UnitId");

            migrationBuilder.InsertData(
                table: "subscriptionPlans",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Description", "DurationInDays", "IsActive", "Name", "PlanType", "Price", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("16c25a5f-e3a1-4cda-ae9d-06e91743ca48"), new DateTime(2026, 1, 8, 12, 40, 15, 502, DateTimeKind.Local).AddTicks(2395), new Guid("00000000-0000-0000-0000-000000000000"), false, "Annual subscription with discount", 365, true, "Annual Plan", "annual", 99.99m, null, null },
                    { new Guid("c066daf3-ab82-4358-b908-b501e9beb622"), new DateTime(2026, 1, 8, 12, 40, 15, 502, DateTimeKind.Local).AddTicks(2350), new Guid("00000000-0000-0000-0000-000000000000"), false, "Premium monthly subscription", 30, true, "Premium Monthly", "monthly", 19.99m, null, null },
                    { new Guid("fc87f15a-d553-4685-9d08-7166424b0e94"), new DateTime(2026, 1, 8, 12, 40, 15, 498, DateTimeKind.Local).AddTicks(8726), new Guid("00000000-0000-0000-0000-000000000000"), false, "Basic monthly subscription", 30, true, "Basic Monthly", "monthly", 9.99m, null, null }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Topics_UnitId",
                table: "Videos",
                column: "UnitId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Topics_UnitId",
                table: "Videos");

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("16c25a5f-e3a1-4cda-ae9d-06e91743ca48"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("c066daf3-ab82-4358-b908-b501e9beb622"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("fc87f15a-d553-4685-9d08-7166424b0e94"));

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "Videos",
                newName: "TopicId");

            migrationBuilder.RenameIndex(
                name: "IX_Videos_UnitId",
                table: "Videos",
                newName: "IX_Videos_TopicId");

            migrationBuilder.InsertData(
                table: "subscriptionPlans",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Description", "DurationInDays", "IsActive", "Name", "PlanType", "Price", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("25660276-7b3d-4603-b2a5-e5ff0b0a5ca4"), new DateTime(2026, 1, 7, 23, 46, 17, 525, DateTimeKind.Local).AddTicks(2075), new Guid("00000000-0000-0000-0000-000000000000"), false, "Basic monthly subscription", 30, true, "Basic Monthly", "monthly", 9.99m, null, null },
                    { new Guid("5947a3fa-9ba7-4b87-bc22-896caed26e16"), new DateTime(2026, 1, 7, 23, 46, 17, 527, DateTimeKind.Local).AddTicks(5491), new Guid("00000000-0000-0000-0000-000000000000"), false, "Annual subscription with discount", 365, true, "Annual Plan", "annual", 99.99m, null, null },
                    { new Guid("69853aeb-6aac-41db-a93a-22773dc61774"), new DateTime(2026, 1, 7, 23, 46, 17, 527, DateTimeKind.Local).AddTicks(5458), new Guid("00000000-0000-0000-0000-000000000000"), false, "Premium monthly subscription", 30, true, "Premium Monthly", "monthly", 19.99m, null, null }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Topics_TopicId",
                table: "Videos",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
