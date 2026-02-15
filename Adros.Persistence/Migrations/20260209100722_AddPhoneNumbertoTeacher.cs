using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adros.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPhoneNumbertoTeacher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "phoneNumber",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "phoneNumber",
                table: "Teachers");
        }
    }
}
