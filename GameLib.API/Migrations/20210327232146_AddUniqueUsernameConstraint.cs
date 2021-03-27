using Microsoft.EntityFrameworkCore.Migrations;

namespace GameLib.API.Migrations
{
    public partial class AddUniqueUsernameConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_user_username",
                table: "user",
                column: "username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_user_username",
                table: "user");
        }
    }
}
