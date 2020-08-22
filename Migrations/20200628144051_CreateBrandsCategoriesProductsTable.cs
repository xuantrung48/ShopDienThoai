using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopDienThoai.Migrations
{
    public partial class CreateBrandsCategoriesProductsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Brands",
                table => new
                {
                    BrandId = table.Column<int>()
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Brands", x => x.BrandId); });

            migrationBuilder.CreateTable(
                "Categories",
                table => new
                {
                    CategoryId = table.Column<int>()
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 20)
                },
                constraints: table => { table.PrimaryKey("PK_Categories", x => x.CategoryId); });

            migrationBuilder.CreateTable(
                "Products",
                table => new
                {
                    ProductId = table.Column<string>(),
                    Name = table.Column<string>(maxLength: 50),
                    Price = table.Column<int>(),
                    BrandId = table.Column<int>(nullable: true),
                    Remain = table.Column<int>(),
                    CategoryId = table.Column<int>(nullable: true),
                    ImageFileName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Screen = table.Column<string>(nullable: true),
                    CPU = table.Column<string>(nullable: true),
                    OS = table.Column<string>(nullable: true),
                    RearCamera = table.Column<string>(nullable: true),
                    FrontCamera = table.Column<string>(nullable: true),
                    Ram = table.Column<int>(nullable: true),
                    Rom = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        "FK_Products_Brands_BrandId",
                        x => x.BrandId,
                        "Brands",
                        "BrandId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_Products_Categories_CategoryId",
                        x => x.CategoryId,
                        "Categories",
                        "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_Products_BrandId",
                "Products",
                "BrandId");

            migrationBuilder.CreateIndex(
                "IX_Products_CategoryId",
                "Products",
                "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Products");

            migrationBuilder.DropTable(
                "Brands");

            migrationBuilder.DropTable(
                "Categories");
        }
    }
}