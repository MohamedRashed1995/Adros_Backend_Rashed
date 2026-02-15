using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Adros.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class VideoViewDurationUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "VideoViews",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.InsertData(
                table: "subscriptionPlans",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Description", "DurationInDays", "IsActive", "Name", "PlanType", "Price", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("2d3312b0-10f7-4ac9-98b3-8a26e8bd6730"), new DateTime(2026, 1, 7, 13, 31, 5, 151, DateTimeKind.Local).AddTicks(9189), new Guid("00000000-0000-0000-0000-000000000000"), false, "Premium monthly subscription", 30, true, "Premium Monthly", "monthly", 19.99m, null, null },
                    { new Guid("54a56688-539c-45da-be52-d283823de939"), new DateTime(2026, 1, 7, 13, 31, 5, 151, DateTimeKind.Local).AddTicks(9278), new Guid("00000000-0000-0000-0000-000000000000"), false, "Annual subscription with discount", 365, true, "Annual Plan", "annual", 99.99m, null, null },
                    { new Guid("5b54acd4-7dc5-4f29-a385-5efe2077d9b6"), new DateTime(2026, 1, 7, 13, 31, 5, 149, DateTimeKind.Local).AddTicks(5514), new Guid("00000000-0000-0000-0000-000000000000"), false, "Basic monthly subscription", 30, true, "Basic Monthly", "monthly", 9.99m, null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "VideoViews");

            migrationBuilder.InsertData(
                table: "subscriptionPlans",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Description", "DurationInDays", "IsActive", "Name", "PlanType", "Price", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("1ab196b5-a035-43ec-a9b4-95df4bd638bf"), new DateTime(2026, 1, 6, 17, 10, 42, 345, DateTimeKind.Local).AddTicks(3332), new Guid("00000000-0000-0000-0000-000000000000"), false, "Basic monthly subscription", 30, true, "Basic Monthly", "monthly", 9.99m, null, null },
                    { new Guid("b189ff6d-f0b8-423d-9f85-4689295bf2d4"), new DateTime(2026, 1, 6, 17, 10, 42, 348, DateTimeKind.Local).AddTicks(6015), new Guid("00000000-0000-0000-0000-000000000000"), false, "Premium monthly subscription", 30, true, "Premium Monthly", "monthly", 19.99m, null, null },
                    { new Guid("c50aa3d5-7070-48a8-b992-244f99ca5d3e"), new DateTime(2026, 1, 6, 17, 10, 42, 348, DateTimeKind.Local).AddTicks(6059), new Guid("00000000-0000-0000-0000-000000000000"), false, "Annual subscription with discount", 365, true, "Annual Plan", "annual", 99.99m, null, null }
                });
        }
    }
}
