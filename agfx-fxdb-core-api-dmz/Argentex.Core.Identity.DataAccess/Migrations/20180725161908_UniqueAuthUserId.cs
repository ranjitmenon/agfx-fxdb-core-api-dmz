using Microsoft.EntityFrameworkCore.Migrations;

namespace Argentex.Core.Identity.DataAccess.Migrations
{
    public partial class UniqueAuthUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_User_AuthUserId",
                table: "User",
                column: "AuthUserId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_User_ClientCompanyContactId",
                table: "User",
                column: "ClientCompanyContactId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_User_AuthUserId",
                table: "User");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_User_ClientCompanyContactId",
                table: "User");
        }
    }
}
