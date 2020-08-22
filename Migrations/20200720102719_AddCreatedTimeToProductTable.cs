using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopDienThoai.Migrations
{
    public partial class AddCreatedTimeToProductTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                "CreatedTime",
                "Products",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "CreatedTime",
                "Products");
        }
    }
}