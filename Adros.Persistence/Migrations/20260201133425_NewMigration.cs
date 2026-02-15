using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adros.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubscriptionStatus",
                table: "Student",
                newName: "IsSubscriped");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsSubscriped",
                table: "Student",
                newName: "SubscriptionStatus");
        }
    }
}
