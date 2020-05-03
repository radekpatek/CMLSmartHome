using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CMLSmartHomeController.Migrations
{
    public partial class InitDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Controller",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    MACAddress = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Controller", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SensorRecords",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SensorId = table.Column<long>(nullable: false),
                    CollectorId = table.Column<long>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    Unit = table.Column<int>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensorRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeatherForecast",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherForecast", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Collectors",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    MACAddress = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    SmartHomeControllerId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collectors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Collectors_Controller_SmartHomeControllerId",
                        column: x => x.SmartHomeControllerId,
                        principalTable: "Controller",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WeatherForecastCurrentState",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    WeatherForecastId = table.Column<long>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false),
                    AtmosphericTemperature = table.Column<double>(nullable: false),
                    Pressure = table.Column<int>(nullable: false),
                    Humidity = table.Column<int>(nullable: false),
                    Cloudiness = table.Column<int>(nullable: false),
                    WindSpeed = table.Column<double>(nullable: false),
                    WindDirection = table.Column<int>(nullable: false),
                    SunriseTime = table.Column<DateTime>(nullable: false),
                    SunsetTime = table.Column<DateTime>(nullable: false),
                    Temperature = table.Column<double>(nullable: false),
                    FeelsLikeTemperature = table.Column<double>(nullable: false),
                    UVIndex = table.Column<double>(nullable: false),
                    AverageVisibility = table.Column<int>(nullable: false),
                    Rain = table.Column<double>(nullable: true),
                    Snow = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherForecastCurrentState", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeatherForecastCurrentState_WeatherForecast_WeatherForecastId",
                        column: x => x.WeatherForecastId,
                        principalTable: "WeatherForecast",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WeatherForecastDailyState",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    WeatherForecastId = table.Column<long>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false),
                    AtmosphericTemperature = table.Column<double>(nullable: false),
                    Pressure = table.Column<int>(nullable: false),
                    Humidity = table.Column<int>(nullable: false),
                    Cloudiness = table.Column<int>(nullable: false),
                    WindSpeed = table.Column<double>(nullable: false),
                    WindDirection = table.Column<int>(nullable: false),
                    SunriseTime = table.Column<DateTime>(nullable: false),
                    SunsetTime = table.Column<DateTime>(nullable: false),
                    UVIndex = table.Column<double>(nullable: false),
                    AverageVisibility = table.Column<int>(nullable: false),
                    Rain = table.Column<double>(nullable: false),
                    Snow = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherForecastDailyState", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeatherForecastDailyState_WeatherForecast_WeatherForecastId",
                        column: x => x.WeatherForecastId,
                        principalTable: "WeatherForecast",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WeatherForecastHourlyState",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    WeatherForecastId = table.Column<long>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false),
                    AtmosphericTemperature = table.Column<double>(nullable: false),
                    Pressure = table.Column<int>(nullable: false),
                    Humidity = table.Column<int>(nullable: false),
                    Cloudiness = table.Column<int>(nullable: false),
                    WindSpeed = table.Column<double>(nullable: false),
                    WindDirection = table.Column<int>(nullable: false),
                    Temperature = table.Column<double>(nullable: false),
                    FeelsLikeTemperature = table.Column<double>(nullable: false),
                    Rain = table.Column<double>(nullable: true),
                    Snow = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherForecastHourlyState", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeatherForecastHourlyState_WeatherForecast_WeatherForecastId",
                        column: x => x.WeatherForecastId,
                        principalTable: "WeatherForecast",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dashboard",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OutdoorCollectorId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dashboard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dashboard_Collectors_OutdoorCollectorId",
                        column: x => x.OutdoorCollectorId,
                        principalTable: "Collectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sensors",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    Unit = table.Column<int>(nullable: false),
                    CollectorId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sensors_Collectors_CollectorId",
                        column: x => x.CollectorId,
                        principalTable: "Collectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WeatherForecastDailyTemperature",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DailyStateTemperatureId = table.Column<long>(nullable: true),
                    DailyStateFeelsLikeTemperatureId = table.Column<long>(nullable: true),
                    DayTemperature = table.Column<double>(nullable: false),
                    MinDailyTemperature = table.Column<double>(nullable: false),
                    MaxDailyTemperature = table.Column<double>(nullable: false),
                    NightTemperature = table.Column<double>(nullable: false),
                    EveningTemperature = table.Column<double>(nullable: false),
                    MorningTemperature = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherForecastDailyTemperature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeatherForecastDailyTemperature_WeatherForecastDailyState_Da~",
                        column: x => x.DailyStateFeelsLikeTemperatureId,
                        principalTable: "WeatherForecastDailyState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WeatherForecastDailyTemperature_WeatherForecastDailyState_D~1",
                        column: x => x.DailyStateTemperatureId,
                        principalTable: "WeatherForecastDailyState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WeatherForecastWeather",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    WeatherConditionId = table.Column<int>(nullable: false),
                    Main = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Icon = table.Column<string>(nullable: true),
                    CurrentStateId = table.Column<long>(nullable: true),
                    DailyStateId = table.Column<long>(nullable: true),
                    HourlyStateId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherForecastWeather", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeatherForecastWeather_WeatherForecastCurrentState_CurrentSt~",
                        column: x => x.CurrentStateId,
                        principalTable: "WeatherForecastCurrentState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WeatherForecastWeather_WeatherForecastDailyState_DailyStateId",
                        column: x => x.DailyStateId,
                        principalTable: "WeatherForecastDailyState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WeatherForecastWeather_WeatherForecastHourlyState_HourlyStat~",
                        column: x => x.HourlyStateId,
                        principalTable: "WeatherForecastHourlyState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Collectors_SmartHomeControllerId",
                table: "Collectors",
                column: "SmartHomeControllerId");

            migrationBuilder.CreateIndex(
                name: "IX_Dashboard_OutdoorCollectorId",
                table: "Dashboard",
                column: "OutdoorCollectorId");

            migrationBuilder.CreateIndex(
                name: "IX_SensorRecords_SensorId_DateTime",
                table: "SensorRecords",
                columns: new[] { "SensorId", "DateTime" });

            migrationBuilder.CreateIndex(
                name: "IX_Sensors_CollectorId",
                table: "Sensors",
                column: "CollectorId");

            migrationBuilder.CreateIndex(
                name: "IX_WeatherForecastCurrentState_WeatherForecastId",
                table: "WeatherForecastCurrentState",
                column: "WeatherForecastId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WeatherForecastDailyState_WeatherForecastId",
                table: "WeatherForecastDailyState",
                column: "WeatherForecastId");

            migrationBuilder.CreateIndex(
                name: "IX_WeatherForecastDailyTemperature_DailyStateFeelsLikeTemperatu~",
                table: "WeatherForecastDailyTemperature",
                column: "DailyStateFeelsLikeTemperatureId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WeatherForecastDailyTemperature_DailyStateTemperatureId",
                table: "WeatherForecastDailyTemperature",
                column: "DailyStateTemperatureId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WeatherForecastHourlyState_WeatherForecastId",
                table: "WeatherForecastHourlyState",
                column: "WeatherForecastId");

            migrationBuilder.CreateIndex(
                name: "IX_WeatherForecastWeather_CurrentStateId",
                table: "WeatherForecastWeather",
                column: "CurrentStateId");

            migrationBuilder.CreateIndex(
                name: "IX_WeatherForecastWeather_DailyStateId",
                table: "WeatherForecastWeather",
                column: "DailyStateId");

            migrationBuilder.CreateIndex(
                name: "IX_WeatherForecastWeather_HourlyStateId",
                table: "WeatherForecastWeather",
                column: "HourlyStateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dashboard");

            migrationBuilder.DropTable(
                name: "SensorRecords");

            migrationBuilder.DropTable(
                name: "Sensors");

            migrationBuilder.DropTable(
                name: "WeatherForecastDailyTemperature");

            migrationBuilder.DropTable(
                name: "WeatherForecastWeather");

            migrationBuilder.DropTable(
                name: "Collectors");

            migrationBuilder.DropTable(
                name: "WeatherForecastCurrentState");

            migrationBuilder.DropTable(
                name: "WeatherForecastDailyState");

            migrationBuilder.DropTable(
                name: "WeatherForecastHourlyState");

            migrationBuilder.DropTable(
                name: "Controller");

            migrationBuilder.DropTable(
                name: "WeatherForecast");
        }
    }
}
