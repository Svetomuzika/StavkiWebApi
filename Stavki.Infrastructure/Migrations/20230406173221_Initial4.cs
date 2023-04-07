using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stavki.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "UsersData");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UsersData",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserDataId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserDataId",
                table: "Users",
                column: "UserDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UsersData_UserDataId",
                table: "Users",
                column: "UserDataId",
                principalTable: "UsersData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UsersData_UserDataId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserDataId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UsersData");

            migrationBuilder.DropColumn(
                name: "UserDataId",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "UsersData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
