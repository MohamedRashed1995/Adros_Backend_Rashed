using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Adros.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class KoKoandS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("878dbca3-0ba1-49d0-ae03-372b6f1eab5d"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("97138c3a-f890-4f34-a20e-947233c70215"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("d71ef31c-c0d0-4f67-9015-e4e23e1aaee2"));

            migrationBuilder.AlterColumn<string>(
                name: "StageName",
                table: "Levels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "subscriptionPlans",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Description", "DurationInDays", "IsActive", "Name", "PlanType", "Price", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("2abee02e-45d8-41fb-be6d-8cc3ced28a89"), new DateTime(2025, 12, 30, 12, 13, 36, 643, DateTimeKind.Local).AddTicks(9971), new Guid("00000000-0000-0000-0000-000000000000"), false, "Basic monthly subscription", 30, true, "Basic Monthly", "monthly", 9.99m, null, null },
                    { new Guid("a80051b9-5fd6-429e-b9d1-c6214e7b8815"), new DateTime(2025, 12, 30, 12, 13, 36, 646, DateTimeKind.Local).AddTicks(8437), new Guid("00000000-0000-0000-0000-000000000000"), false, "Annual subscription with discount", 365, true, "Annual Plan", "annual", 99.99m, null, null },
                    { new Guid("ac80dc25-37de-49f3-a6b4-dd9bc5c8a215"), new DateTime(2025, 12, 30, 12, 13, 36, 646, DateTimeKind.Local).AddTicks(8394), new Guid("00000000-0000-0000-0000-000000000000"), false, "Premium monthly subscription", 30, true, "Premium Monthly", "monthly", 19.99m, null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("2abee02e-45d8-41fb-be6d-8cc3ced28a89"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("a80051b9-5fd6-429e-b9d1-c6214e7b8815"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("ac80dc25-37de-49f3-a6b4-dd9bc5c8a215"));

            migrationBuilder.AlterColumn<string>(
                name: "StageName",
                table: "Levels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "subscriptionPlans",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Description", "DurationInDays", "IsActive", "Name", "PlanType", "Price", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("878dbca3-0ba1-49d0-ae03-372b6f1eab5d"), new DateTime(2025, 12, 29, 15, 38, 6, 524, DateTimeKind.Local).AddTicks(9584), new Guid("00000000-0000-0000-0000-000000000000"), false, "Premium monthly subscription", 30, true, "Premium Monthly", "monthly", 19.99m, null, null },
                    { new Guid("97138c3a-f890-4f34-a20e-947233c70215"), new DateTime(2025, 12, 29, 15, 38, 6, 524, DateTimeKind.Local).AddTicks(9624), new Guid("00000000-0000-0000-0000-000000000000"), false, "Annual subscription with discount", 365, true, "Annual Plan", "annual", 99.99m, null, null },
                    { new Guid("d71ef31c-c0d0-4f67-9015-e4e23e1aaee2"), new DateTime(2025, 12, 29, 15, 38, 6, 522, DateTimeKind.Local).AddTicks(1324), new Guid("00000000-0000-0000-0000-000000000000"), false, "Basic monthly subscription", 30, true, "Basic Monthly", "monthly", 9.99m, null, null }
                });
        }
    }
}
