using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Adros.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MakeLessonTopicNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { new Guid("04f92952-64d4-4aaf-9ee8-9edfb80a20df"), new DateTime(2026, 1, 7, 16, 35, 58, 718, DateTimeKind.Local).AddTicks(9055), new Guid("00000000-0000-0000-0000-000000000000"), false, "Basic monthly subscription", 30, true, "Basic Monthly", "monthly", 9.99m, null, null },
                    { new Guid("7773b20c-b672-4f75-8cc2-9d0f9d553099"), new DateTime(2026, 1, 7, 16, 35, 58, 721, DateTimeKind.Local).AddTicks(4237), new Guid("00000000-0000-0000-0000-000000000000"), false, "Annual subscription with discount", 365, true, "Annual Plan", "annual", 99.99m, null, null },
                    { new Guid("af7739c4-e5a3-4a36-b8d2-1b021eae0d68"), new DateTime(2026, 1, 7, 16, 35, 58, 721, DateTimeKind.Local).AddTicks(4201), new Guid("00000000-0000-0000-0000-000000000000"), false, "Premium monthly subscription", 30, true, "Premium Monthly", "monthly", 19.99m, null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { new Guid("b9d6acf3-2473-4823-bec3-52a274940bc8"), new DateTime(2026, 1, 7, 16, 25, 5, 88, DateTimeKind.Local).AddTicks(1372), new Guid("00000000-0000-0000-0000-000000000000"), false, "Basic monthly subscription", 30, true, "Basic Monthly", "monthly", 9.99m, null, null },
                    { new Guid("ed175958-4634-41ca-996d-279ff39ac705"), new DateTime(2026, 1, 7, 16, 25, 5, 91, DateTimeKind.Local).AddTicks(2697), new Guid("00000000-0000-0000-0000-000000000000"), false, "Annual subscription with discount", 365, true, "Annual Plan", "annual", 99.99m, null, null },
                    { new Guid("ed31c470-0778-4aff-9102-3910cd6af752"), new DateTime(2026, 1, 7, 16, 25, 5, 91, DateTimeKind.Local).AddTicks(2653), new Guid("00000000-0000-0000-0000-000000000000"), false, "Premium monthly subscription", 30, true, "Premium Monthly", "monthly", 19.99m, null, null }
                });
        }
    }
}
