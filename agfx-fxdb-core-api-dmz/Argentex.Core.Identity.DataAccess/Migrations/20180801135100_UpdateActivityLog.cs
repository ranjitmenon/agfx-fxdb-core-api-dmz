using Microsoft.EntityFrameworkCore.Migrations;

namespace Argentex.Core.Identity.DataAccess.Migrations
{
    public partial class UpdateActivityLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthUserId",
                table: "ActivityLog",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "ActivityLog",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLog_Id",
                table: "ActivityLog",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityLog_User_Id",
                table: "ActivityLog",
                column: "Id",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityLog_User_Id",
                table: "ActivityLog");

            migrationBuilder.DropIndex(
                name: "IX_ActivityLog_Id",
                table: "ActivityLog");

            migrationBuilder.DropColumn(
                name: "AuthUserId",
                table: "ActivityLog");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ActivityLog");
        }
    }
}
