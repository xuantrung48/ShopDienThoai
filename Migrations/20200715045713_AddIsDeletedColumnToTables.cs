using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopDienThoai.Migrations
{
    public partial class AddIsDeletedColumnToTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                "IsDeleted",
                "Products",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                "IsDeleted",
                "Categories",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                "IsDeleted",
                "Brands",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                "IsDeleted",
                "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "IsDeleted",
                "Products");

            migrationBuilder.DropColumn(
                "IsDeleted",
                "Categories");

            migrationBuilder.DropColumn(
                "IsDeleted",
                "Brands");

            migrationBuilder.DropColumn(
                "IsDeleted",
                "AspNetUsers");
        }
    }
}