using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Adros.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class LessonUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("dbd7013e-e4e2-41fd-a586-e1793282b083"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("dc146682-ac7d-4e4c-857c-077da52c5eb1"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("f3b7bf25-ee10-4464-bb12-0ba04c792f98"));

            migrationBuilder.AddColumn<string>(
                name: "LessonfileName",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "subscriptionPlans",
                columns: new[] { "Id", "CreatedBy", "Deleted", "Description", "DurationInDays", "IsActive", "Name", "PlanType", "Price", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("9ddcbbf2-30cd-4229-a927-922460c0ba28"), null, false, "Basic monthly subscription", 30, true, "Basic Monthly", "monthly", 9.99m, null, null },
                    { new Guid("ac1df104-d6cb-4ced-b192-5fae8dc7907a"), null, false, "Premium monthly subscription", 30, true, "Premium Monthly", "monthly", 19.99m, null, null },
                    { new Guid("c92b0aa9-0d67-4e24-9f6e-f93d6b04ee62"), null, false, "Annual subscription with discount", 365, true, "Annual Plan", "annual", 99.99m, null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("9ddcbbf2-30cd-4229-a927-922460c0ba28"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("ac1df104-d6cb-4ced-b192-5fae8dc7907a"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("c92b0aa9-0d67-4e24-9f6e-f93d6b04ee62"));

            migrationBuilder.DropColumn(
                name: "LessonfileName",
                table: "Lessons");

            migrationBuilder.InsertData(
                table: "subscriptionPlans",
                columns: new[] { "Id", "CreatedBy", "Deleted", "Description", "DurationInDays", "IsActive", "Name", "PlanType", "Price", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("dbd7013e-e4e2-41fd-a586-e1793282b083"), null, false, "Premium monthly subscription", 30, true, "Premium Monthly", "monthly", 19.99m, null, null },
                    { new Guid("dc146682-ac7d-4e4c-857c-077da52c5eb1"), null, false, "Basic monthly subscription", 30, true, "Basic Monthly", "monthly", 9.99m, null, null },
                    { new Guid("f3b7bf25-ee10-4464-bb12-0ba04c792f98"), null, false, "Annual subscription with discount", 365, true, "Annual Plan", "annual", 99.99m, null, null }
                });
        }
    }
}
