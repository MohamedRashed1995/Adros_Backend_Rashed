using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Adros.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class newnewnew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Subjects_SubjectId",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Teacher_TeacherId",
                table: "Lessons");

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("6b92de7f-fc65-495c-94b8-910a60e54a22"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("ac041a83-539f-4140-a759-9c0fb2eb5326"));

            migrationBuilder.DeleteData(
                table: "subscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("e912d884-b468-40b8-a96e-5b1d64c751e1"));

            migrationBuilder.AlterColumn<Guid>(
                name: "TopicId",
                table: "Lessons",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "TeacherId",
                table: "Lessons",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "SubjectId",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Subjects_SubjectId",
                table: "Lessons",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Teacher_TeacherId",
                table: "Lessons",
                column: "TeacherId",
                principalTable: "Teacher",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Subjects_SubjectId",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Teacher_TeacherId",
                table: "Lessons");

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

            migrationBuilder.AlterColumn<Guid>(
                name: "TeacherId",
                table: "Lessons",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "SubjectId",
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
                    { new Guid("6b92de7f-fc65-495c-94b8-910a60e54a22"), new DateTime(2026, 1, 7, 15, 45, 31, 263, DateTimeKind.Local).AddTicks(5742), new Guid("00000000-0000-0000-0000-000000000000"), false, "Annual subscription with discount", 365, true, "Annual Plan", "annual", 99.99m, null, null },
                    { new Guid("ac041a83-539f-4140-a759-9c0fb2eb5326"), new DateTime(2026, 1, 7, 15, 45, 31, 263, DateTimeKind.Local).AddTicks(5708), new Guid("00000000-0000-0000-0000-000000000000"), false, "Premium monthly subscription", 30, true, "Premium Monthly", "monthly", 19.99m, null, null },
                    { new Guid("e912d884-b468-40b8-a96e-5b1d64c751e1"), new DateTime(2026, 1, 7, 15, 45, 31, 260, DateTimeKind.Local).AddTicks(7105), new Guid("00000000-0000-0000-0000-000000000000"), false, "Basic monthly subscription", 30, true, "Basic Monthly", "monthly", 9.99m, null, null }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Subjects_SubjectId",
                table: "Lessons",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Teacher_TeacherId",
                table: "Lessons",
                column: "TeacherId",
                principalTable: "Teacher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
