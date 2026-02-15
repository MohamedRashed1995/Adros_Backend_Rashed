using Microsoft.EntityFrameworkCore.Migrations;
#nullable disable

namespace Adros.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class LLLLLLLLLLL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // أول حاجة شيل أي Index مرتبط بالأعمدة اللي هتمسحها
            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_SubscriptionPlanId",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_Status",
                table: "Subscriptions");

            // دلوقتي حذف الأعمدة القديمة
            migrationBuilder.DropColumn(name: "SubscriptionPlanId", table: "Subscriptions");
            migrationBuilder.DropColumn(name: "PaymentMethod", table: "Subscriptions");
            migrationBuilder.DropColumn(name: "AmountPaid", table: "Subscriptions");
            migrationBuilder.DropColumn(name: "Status", table: "Subscriptions");
            migrationBuilder.DropColumn(name: "IsAutoRenew", table: "Subscriptions");
            migrationBuilder.DropColumn(name: "Deleted", table: "Subscriptions");
            migrationBuilder.DropColumn(name: "UpdatedBy", table: "Subscriptions");
            migrationBuilder.DropColumn(name: "CreatedBy", table: "Subscriptions");

            // إضافة الأعمدة الجديدة
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Subscriptions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Subscriptions",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // إعادة الأعمدة القديمة
            migrationBuilder.AddColumn<Guid>(name: "SubscriptionPlanId", table: "Subscriptions", type: "uniqueidentifier", nullable: true);
            migrationBuilder.AddColumn<string>(name: "PaymentMethod", table: "Subscriptions", type: "nvarchar(max)", nullable: true);
            migrationBuilder.AddColumn<decimal>(name: "AmountPaid", table: "Subscriptions", type: "decimal(18,2)", nullable: true);
            migrationBuilder.AddColumn<string>(name: "Status", table: "Subscriptions", type: "nvarchar(max)", nullable: true);
            migrationBuilder.AddColumn<bool>(name: "IsAutoRenew", table: "Subscriptions", type: "bit", nullable: true);
            migrationBuilder.AddColumn<bool>(name: "Deleted", table: "Subscriptions", type: "bit", nullable: true);
            migrationBuilder.AddColumn<Guid>(name: "UpdatedBy", table: "Subscriptions", type: "uniqueidentifier", nullable: true);
            migrationBuilder.AddColumn<Guid>(name: "CreatedBy", table: "Subscriptions", type: "uniqueidentifier", nullable: true);

            // إعادة أي Index قديم
            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_SubscriptionPlanId",
                table: "Subscriptions",
                column: "SubscriptionPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_Status",
                table: "Subscriptions",
                column: "Status");

            // حذف الأعمدة الجديدة
            migrationBuilder.DropColumn(name: "Price", table: "Subscriptions");
            migrationBuilder.DropColumn(name: "IsDeleted", table: "Subscriptions");
        }
    }
}
