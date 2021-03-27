using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameLib.API.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "game",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(maxLength: 150, nullable: false),
                    year = table.Column<short>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_game", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    nickname = table.Column<string>(maxLength: 100, nullable: false),
                    username = table.Column<string>(maxLength: 100, nullable: false),
                    password = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_game",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    user_id = table.Column<Guid>(nullable: false),
                    game_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_game", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_game_game_game_id",
                        column: x => x.game_id,
                        principalTable: "game",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_game_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "game_borrowing",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    start_date = table.Column<DateTime>(nullable: false),
                    predicted_end_date = table.Column<DateTime>(nullable: false),
                    real_end_date = table.Column<DateTime>(nullable: true),
                    game_ownership_id = table.Column<Guid>(nullable: false),
                    game_borrower_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_game_borrowing", x => x.id);
                    table.ForeignKey(
                        name: "fk_game_borrowing_user_game_borrower_id",
                        column: x => x.game_borrower_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_game_borrowing_user_game_game_ownership_id",
                        column: x => x.game_ownership_id,
                        principalTable: "user_game",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_game_borrowing_game_borrower_id",
                table: "game_borrowing",
                column: "game_borrower_id");

            migrationBuilder.CreateIndex(
                name: "ix_game_borrowing_game_ownership_id",
                table: "game_borrowing",
                column: "game_ownership_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_game_game_id",
                table: "user_game",
                column: "game_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_game_user_id",
                table: "user_game",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "game_borrowing");

            migrationBuilder.DropTable(
                name: "user_game");

            migrationBuilder.DropTable(
                name: "game");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
