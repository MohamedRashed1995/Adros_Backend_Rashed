using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Adros.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixTopicLessonRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("08fdba46-d669-459e-9155-8e5bfdbc0654"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("c30e09fa-e3b4-4b2a-8079-93cc3df4d2fe"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("f56da234-60e4-4721-a4b0-56a588a02ed6"));

            migrationBuilder.AlterColumn<Guid>(
                name: "TopicId",
                table: "Lessons",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "subscriptionPlans",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Description", "DurationInDays", "IsActive", "Name", "PlanType", "Price", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("5b7d5655-4956-4156-805c-b0d805f0c4c4"), new DateTime(2026, 1, 7, 16, 5, 8, 129, DateTimeKind.Local).AddTicks(1010), new Guid("00000000-0000-0000-0000-000000000000"), false, "Premium monthly subscription", 30, true, "Premium Monthly", "monthly", 19.99m, null, null },
                    { new Guid("b586f386-9964-43a0-8f55-7e838c49177a"), new DateTime(2026, 1, 7, 16, 5, 8, 129, DateTimeKind.Local).AddTicks(1067), new Guid("00000000-0000-0000-0000-000000000000"), false, "Annual subscription with discount", 365, true, "Annual Plan", "annual", 99.99m, null, null },
                    { new Guid("f907a3b9-ea35-43dc-ad91-17f333ccc59e"), new DateTime(2026, 1, 7, 16, 5, 8, 125, DateTimeKind.Local).AddTicks(4396), new Guid("00000000-0000-0000-0000-000000000000"), false, "Basic monthly subscription", 30, true, "Basic Monthly", "monthly", 9.99m, null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("5b7d5655-4956-4156-805c-b0d805f0c4c4"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("b586f386-9964-43a0-8f55-7e838c49177a"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("f907a3b9-ea35-43dc-ad91-17f333ccc59e"));

            migrationBuilder.AlterColumn<Guid>(
                name: "TopicId",
                table: "Lessons",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "subscriptionPlans",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Description", "DurationInDays", "IsActive", "Name", "PlanType", "Price", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("08fdba46-d669-459e-9155-8e5bfdbc0654"), new DateTime(2026, 1, 7, 15, 51, 59, 527, DateTimeKind.Local).AddTicks(6524), new Guid("00000000-0000-0000-0000-000000000000"), false, "Basic monthly subscription", 30, true, "Basic Monthly", "monthly", 9.99m, null, null },
                    { new Guid("c30e09fa-e3b4-4b2a-8079-93cc3df4d2fe"), new DateTime(2026, 1, 7, 15, 51, 59, 530, DateTimeKind.Local).AddTicks(3381), new Guid("00000000-0000-0000-0000-000000000000"), false, "Annual subscription with discount", 365, true, "Annual Plan", "annual", 99.99m, null, null },
                    { new Guid("f56da234-60e4-4721-a4b0-56a588a02ed6"), new DateTime(2026, 1, 7, 15, 51, 59, 530, DateTimeKind.Local).AddTicks(3344), new Guid("00000000-0000-0000-0000-000000000000"), false, "Premium monthly subscription", 30, true, "Premium Monthly", "monthly", 19.99m, null, null }
                });
        }
    }
}
