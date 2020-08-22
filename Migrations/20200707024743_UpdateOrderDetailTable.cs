using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopDienThoai.Migrations
{
    public partial class UpdateOrderDetailTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                "OrderDetailId",
                "OrderDetails",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                "PK_OrderDetails",
                "OrderDetails",
                "OrderDetailId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                "PK_OrderDetails",
                "OrderDetails");

            migrationBuilder.DropColumn(
                "OrderDetailId",
                "OrderDetails");
        }
    }
}