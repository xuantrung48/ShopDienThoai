using Microsoft.EntityFrameworkCore.Migrations;

namespace TheGioiDienThoai.Migrations
{
    public partial class AddRoleIdToAppSettingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DefaultRoleId",
                table: "AppSettings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultRoleId",
                table: "AppSettings");
        }
    }
}
