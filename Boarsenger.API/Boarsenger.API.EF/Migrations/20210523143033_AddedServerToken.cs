using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Boarsenger.API.EF.Migrations
{
    public partial class AddedServerToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAdultOnly",
                table: "Server",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ServerToken",
                columns: table => new
                {
                    ServerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerToken", x => x.ServerId);
                    table.ForeignKey(
                        name: "FK_ServerToken_Server_ServerId",
                        column: x => x.ServerId,
                        principalTable: "Server",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServerToken");

            migrationBuilder.DropColumn(
                name: "IsAdultOnly",
                table: "Server");
        }
    }
}
