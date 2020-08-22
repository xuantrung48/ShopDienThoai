using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopDienThoai.Migrations
{
    public partial class AddRoleIdToAppSettingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                "DefaultRoleId",
                "AppSettings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "DefaultRoleId",
                "AppSettings");
        }
    }
}