using Microsoft.EntityFrameworkCore.Migrations;

namespace CMLSmartHomeController.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IPAddress",
                table: "Controller");

            migrationBuilder.DropColumn(
                name: "IPAddress",
                table: "Collectors");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IPAddress",
                table: "Controller",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IPAddress",
                table: "Collectors",
                nullable: true);
        }
    }
}
