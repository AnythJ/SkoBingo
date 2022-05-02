using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkoBingo.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bingos",
                columns: table => new
                {
                    BingoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    UniqueLink = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bingos", x => x.BingoId);
                });

            migrationBuilder.CreateTable(
                name: "Scoreboards",
                columns: table => new
                {
                    ScoreboardId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BingoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scoreboards", x => x.ScoreboardId);
                    table.ForeignKey(
                        name: "FK_Scoreboards_Bingos_BingoId",
                        column: x => x.BingoId,
                        principalTable: "Bingos",
                        principalColumn: "BingoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sentences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BingoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sentences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sentences_Bingos_BingoId",
                        column: x => x.BingoId,
                        principalTable: "Bingos",
                        principalColumn: "BingoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WinDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScoreboardId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Scoreboards_ScoreboardId",
                        column: x => x.ScoreboardId,
                        principalTable: "Scoreboards",
                        principalColumn: "ScoreboardId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Players_ScoreboardId",
                table: "Players",
                column: "ScoreboardId");

            migrationBuilder.CreateIndex(
                name: "IX_Scoreboards_BingoId",
                table: "Scoreboards",
                column: "BingoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sentences_BingoId",
                table: "Sentences",
                column: "BingoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Sentences");

            migrationBuilder.DropTable(
                name: "Scoreboards");

            migrationBuilder.DropTable(
                name: "Bingos");
        }
    }
}
