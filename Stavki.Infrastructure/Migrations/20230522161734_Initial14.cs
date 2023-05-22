using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stavki.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResponsibleUser",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResponsibleUserId",
                table: "Requests",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResponsibleUser",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "ResponsibleUserId",
                table: "Requests");
        }
    }
}
