using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AnagramSolver.EF.DatabaseFirst.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cached_Words",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cached_Words", x => x.Id);
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
                name: "Word_CachedWord_Additionals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WordId = table.Column<int>(type: "int", nullable: true),
                    CachedWordId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Word_CachedWord_Additionals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Word_CachedWord_Additionals_Cached_Words_CachedWordId",
                        column: x => x.CachedWordId,
                        principalTable: "Cached_Words",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Word_CachedWord_Additionals_Words_WordId",
                        column: x => x.WordId,
                        principalTable: "Words",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Word_CachedWord_Additionals_CachedWordId",
                table: "Word_CachedWord_Additionals",
                column: "CachedWordId");

            migrationBuilder.CreateIndex(
                name: "IX_Word_CachedWord_Additionals_WordId",
                table: "Word_CachedWord_Additionals",
                column: "WordId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SearchInformations");

            migrationBuilder.DropTable(
                name: "Word_CachedWord_Additionals");

            migrationBuilder.DropTable(
                name: "Cached_Words");

            migrationBuilder.DropTable(
                name: "Words");
        }
    }
}
