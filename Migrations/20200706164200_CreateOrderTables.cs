using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopDienThoai.Migrations
{
    public partial class CreateOrderTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Customers",
                table => new
                {
                    CustomerId = table.Column<string>(),
                    UserId = table.Column<string>(nullable: true),
                    CustomerName = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(),
                    Address = table.Column<string>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                    table.ForeignKey(
                        "FK_Customers_AspNetUsers_UserId",
                        x => x.UserId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "Orders",
                table => new
                {
                    OrderId = table.Column<string>(),
                    CustomerId = table.Column<string>(nullable: true),
                    OrderTime = table.Column<DateTime>(),
                    CompleteTime = table.Column<DateTime>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        "FK_Orders_Customers_CustomerId",
                        x => x.CustomerId,
                        "Customers",
                        "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "OrderDetails",
                table => new
                {
                    OrderId = table.Column<string>(),
                    ProductId = table.Column<string>(),
                    Price = table.Column<int>(),
                    Quantity = table.Column<int>()
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        "FK_OrderDetails_Orders_OrderId",
                        x => x.OrderId,
                        "Orders",
                        "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_OrderDetails_Products_ProductId",
                        x => x.ProductId,
                        "Products",
                        "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_Customers_UserId",
                "Customers",
                "UserId");

            migrationBuilder.CreateIndex(
                "IX_OrderDetails_OrderId",
                "OrderDetails",
                "OrderId");

            migrationBuilder.CreateIndex(
                "IX_OrderDetails_ProductId",
                "OrderDetails",
                "ProductId");

            migrationBuilder.CreateIndex(
                "IX_Orders_CustomerId",
                "Orders",
                "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "OrderDetails");

            migrationBuilder.DropTable(
                "Orders");

            migrationBuilder.DropTable(
                "Customers");
        }
    }
}