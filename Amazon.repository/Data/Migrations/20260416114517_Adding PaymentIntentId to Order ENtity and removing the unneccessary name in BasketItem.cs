using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Amazon.repository.Data.Migrations
{
    public partial class AddingPaymentIntentIdtoOrderENtityandremovingtheunneccessarynameinBasketItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentIntentId",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "DeliveryMethod",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentIntentId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "DeliveryMethod");
        }
    }
}
