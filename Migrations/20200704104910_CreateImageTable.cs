using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopDienThoai.Migrations
{
    public partial class CreateImageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "ImageFileName",
                "Products");

            migrationBuilder.CreateTable(
                "Images",
                table => new
                {
                    ImageId = table.Column<string>(),
                    ImageName = table.Column<string>(nullable: true),
                    ProductId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ImageId);
                    table.ForeignKey(
                        "FK_Images_Products_ProductId",
                        x => x.ProductId,
                        "Products",
                        "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_Images_ProductId",
                "Images",
                "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Images");

            migrationBuilder.AddColumn<string>(
                "ImageFileName",
                "Products",
                "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                "Products",
                "ProductId",
                "1",
                "ImageFileName",
                "samsung-galaxy-a51-8gb-blue.png");
        }
    }
}