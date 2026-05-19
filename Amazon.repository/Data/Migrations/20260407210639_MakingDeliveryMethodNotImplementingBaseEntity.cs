using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Amazon.repository.Data.Migrations
{
    public partial class MakingDeliveryMethodNotImplementingBaseEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "DeliveryMethod");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "DeliveryMethod",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
