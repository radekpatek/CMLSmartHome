using Microsoft.EntityFrameworkCore.Migrations;

namespace CMLSmartHomeController.Migrations
{
    public partial class Dashboard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dashboard_Collectors_InternalCollectorId",
                table: "Dashboard");

            migrationBuilder.DropIndex(
                name: "IX_Dashboard_InternalCollectorId",
                table: "Dashboard");

            migrationBuilder.DropColumn(
                name: "InternalCollectorId",
                table: "Dashboard");

            migrationBuilder.AddColumn<long>(
                name: "DashboardId",
                table: "Collectors",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Collectors_DashboardId",
                table: "Collectors",
                column: "DashboardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collectors_Dashboard_DashboardId",
                table: "Collectors",
                column: "DashboardId",
                principalTable: "Dashboard",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collectors_Dashboard_DashboardId",
                table: "Collectors");

            migrationBuilder.DropIndex(
                name: "IX_Collectors_DashboardId",
                table: "Collectors");

            migrationBuilder.DropColumn(
                name: "DashboardId",
                table: "Collectors");

            migrationBuilder.AddColumn<long>(
                name: "InternalCollectorId",
                table: "Dashboard",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dashboard_InternalCollectorId",
                table: "Dashboard",
                column: "InternalCollectorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dashboard_Collectors_InternalCollectorId",
                table: "Dashboard",
                column: "InternalCollectorId",
                principalTable: "Collectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
