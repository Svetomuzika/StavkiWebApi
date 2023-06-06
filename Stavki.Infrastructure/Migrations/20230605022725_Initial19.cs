using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stavki.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial19 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RequestsHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DepartureCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArrivalCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    DepartureDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequestCreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResponsibleUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponsibleUserId = table.Column<int>(type: "int", nullable: true),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    UserDomainId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestsHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestsHistory_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestsHistory_Users_UserDomainId",
                        column: x => x.UserDomainId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestsHistory_RequestId",
                table: "RequestsHistory",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestsHistory_UserDomainId",
                table: "RequestsHistory",
                column: "UserDomainId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestsHistory");
        }
    }
}
