using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AnagramSolver.EF.CodeFirst.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CachedWords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CachedWords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SearchInformations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SearchTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SearchedWord = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Anagram = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchInformations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartOfSpeech = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderedValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WordCachedWordAdditionals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WordId = table.Column<int>(type: "int", nullable: false),
                    CachedWordId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordCachedWordAdditionals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WordCachedWordAdditionals_CachedWords_CachedWordId",
                        column: x => x.CachedWordId,
                        principalTable: "CachedWords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WordCachedWordAdditionals_Words_WordId",
                        column: x => x.WordId,
                        principalTable: "Words",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WordCachedWordAdditionals_CachedWordId",
                table: "WordCachedWordAdditionals",
                column: "CachedWordId");

            migrationBuilder.CreateIndex(
                name: "IX_WordCachedWordAdditionals_WordId",
                table: "WordCachedWordAdditionals",
                column: "WordId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SearchInformations");

            migrationBuilder.DropTable(
                name: "WordCachedWordAdditionals");

            migrationBuilder.DropTable(
                name: "CachedWords");

            migrationBuilder.DropTable(
                name: "Words");
        }
    }
}
