using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopDienThoai.Migrations
{
    public partial class AddDataToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                "Brands",
                new[] {"BrandId", "Name"},
                new object[,]
                {
                    {1, "Apple"},
                    {2, "Samsung"},
                    {3, "VSmart"},
                    {4, "Oppo"},
                    {5, "Sony"}
                });

            migrationBuilder.InsertData(
                "Categories",
                new[] {"CategoryId", "Name"},
                new object[,]
                {
                    {1, "Smartphone"},
                    {2, "Tablet"},
                    {3, "Laptop"}
                });

            migrationBuilder.InsertData(
                "Products",
                new[]
                {
                    "ProductId", "BrandId", "CPU", "CategoryId", "Description", "FrontCamera", "ImageFileName", "Name",
                    "OS", "Price", "Ram", "RearCamera", "Remain", "Rom", "Screen"
                },
                new object[]
                {
                    "1", null, "Exynos 9611 8 nhân", null,
                    "Galaxy A51 8GB là phiên bản nâng cấp RAM của smartphone tầm trung đình đám Galaxy A51 từ Samsung. Sản phẩm nổi bật với thiết kế sang trọng, màn hình Infinity-O cùng cụm 4 camera đột phá.",
                    "32 MP", "samsung-galaxy-a51-8gb-blue.png", "Galaxy A51", "Android 10", 7790000, 8,
                    "Chính 48 MP & Phụ 12 MP, 5 MP, 5 MP", 10, 128, "Super AMOLED, \"6.5\",Full HD + "
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                "Brands",
                "BrandId",
                1);

            migrationBuilder.DeleteData(
                "Brands",
                "BrandId",
                2);

            migrationBuilder.DeleteData(
                "Brands",
                "BrandId",
                3);

            migrationBuilder.DeleteData(
                "Brands",
                "BrandId",
                4);

            migrationBuilder.DeleteData(
                "Brands",
                "BrandId",
                5);

            migrationBuilder.DeleteData(
                "Categories",
                "CategoryId",
                1);

            migrationBuilder.DeleteData(
                "Categories",
                "CategoryId",
                2);

            migrationBuilder.DeleteData(
                "Categories",
                "CategoryId",
                3);

            migrationBuilder.DeleteData(
                "Products",
                "ProductId",
                "1");
        }
    }
}