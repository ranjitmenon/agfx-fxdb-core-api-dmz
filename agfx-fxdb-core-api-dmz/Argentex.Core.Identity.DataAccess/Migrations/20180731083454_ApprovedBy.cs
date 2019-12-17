using Microsoft.EntityFrameworkCore.Migrations;

namespace Argentex.Core.Identity.DataAccess.Migrations
{
    public partial class ApprovedBy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_User_ClientCompanyContactId",
                table: "User");

            migrationBuilder.AddColumn<int>(
                name: "ApprovedByAuthUserId",
                table: "User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedByAuthUserId",
                table: "User");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_User_ClientCompanyContactId",
                table: "User",
                column: "ClientCompanyContactId");
        }
    }
}
