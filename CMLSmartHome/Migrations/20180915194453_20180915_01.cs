using Microsoft.EntityFrameworkCore.Migrations;

namespace CMLSmartHome.Migrations
{
    public partial class _20180915_01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Unit",
                table: "Sensors",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unit",
                table: "Sensors");
        }
    }
}
