using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopDienThoai.Migrations
{
    public partial class ExtendUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Products_Brands_BrandId",
                "Products");

            migrationBuilder.DropForeignKey(
                "FK_Products_Categories_CategoryId",
                "Products");

            migrationBuilder.AlterColumn<int>(
                "CategoryId",
                "Products",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                "BrandId",
                "Products",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                "Address",
                "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                "Gender",
                "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                "ProfilePicture",
                "AspNetUsers",
                nullable: true);

            migrationBuilder.UpdateData(
                "Products",
                "ProductId",
                "1",
                new[] {"BrandId", "CategoryId"},
                new object[] {0, 0});

            migrationBuilder.AddForeignKey(
                "FK_Products_Brands_BrandId",
                "Products",
                "BrandId",
                "Brands",
                principalColumn: "BrandId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_Products_Categories_CategoryId",
                "Products",
                "CategoryId",
                "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Products_Brands_BrandId",
                "Products");

            migrationBuilder.DropForeignKey(
                "FK_Products_Categories_CategoryId",
                "Products");

            migrationBuilder.DropColumn(
                "Address",
                "AspNetUsers");

            migrationBuilder.DropColumn(
                "Gender",
                "AspNetUsers");

            migrationBuilder.DropColumn(
                "ProfilePicture",
                "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                "CategoryId",
                "Products",
                "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                "BrandId",
                "Products",
                "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.UpdateData(
                "Products",
                "ProductId",
                "1",
                new[] {"BrandId", "CategoryId"},
                new object[] {null, null});

            migrationBuilder.AddForeignKey(
                "FK_Products_Brands_BrandId",
                "Products",
                "BrandId",
                "Brands",
                principalColumn: "BrandId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_Products_Categories_CategoryId",
                "Products",
                "CategoryId",
                "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}