using Microsoft.EntityFrameworkCore.Migrations;

namespace Argentex.Core.Identity.DataAccess.Migrations
{
    public partial class Signatory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAuthorisedSignatory",
                table: "User",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSignatory",
                table: "User",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAuthorisedSignatory",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IsSignatory",
                table: "User");
        }
    }
}
