using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stavki.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityType",
                table: "NearInCityNDS",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CityType",
                table: "NearInCity",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CityType",
                table: "InCityNDS",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CityType",
                table: "InCity",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CityType",
                table: "NearInCityNDS");

            migrationBuilder.DropColumn(
                name: "CityType",
                table: "NearInCity");

            migrationBuilder.DropColumn(
                name: "CityType",
                table: "InCityNDS");

            migrationBuilder.DropColumn(
                name: "CityType",
                table: "InCity");
        }
    }
}
