using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stavki.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InCity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UpTo24Tons = table.Column<int>(type: "int", nullable: true),
                    From24UpTo27Tons = table.Column<int>(type: "int", nullable: true),
                    From27Tons = table.Column<int>(type: "int", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Distance = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InCity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InCityNDS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UpTo24Tons = table.Column<int>(type: "int", nullable: true),
                    From24UpTo27Tons = table.Column<int>(type: "int", nullable: true),
                    From27Tons = table.Column<int>(type: "int", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Distance = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InCityNDS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NearInCity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Feet20 = table.Column<int>(type: "int", nullable: true),
                    Feet40 = table.Column<int>(type: "int", nullable: true),
                    From24UpTo30Tons = table.Column<int>(type: "int", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Distance = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NearInCity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NearInCityNDS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Feet20 = table.Column<int>(type: "int", nullable: true),
                    Feet40 = table.Column<int>(type: "int", nullable: true),
                    From24UpTo30Tons = table.Column<int>(type: "int", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Distance = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NearInCityNDS", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InCity");

            migrationBuilder.DropTable(
                name: "InCityNDS");

            migrationBuilder.DropTable(
                name: "NearInCity");

            migrationBuilder.DropTable(
                name: "NearInCityNDS");
        }
    }
}
