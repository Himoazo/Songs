using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Songs.Migrations
{
    /// <inheritdoc />
    public partial class ManyToMany3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artists_Songs_SongId",
                table: "Artists");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Songs_SongId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_SongId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Artists_SongId",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "SongId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "SongId",
                table: "Artists");

            migrationBuilder.CreateTable(
                name: "ArtistSong",
                columns: table => new
                {
                    ArtistId = table.Column<int>(type: "int", nullable: false),
                    SongsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistSong", x => new { x.ArtistId, x.SongsId });
                    table.ForeignKey(
                        name: "FK_ArtistSong_Artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtistSong_Songs_SongsId",
                        column: x => x.SongsId,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategorySong",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    SongsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategorySong", x => new { x.CategoryId, x.SongsId });
                    table.ForeignKey(
                        name: "FK_CategorySong_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategorySong_Songs_SongsId",
                        column: x => x.SongsId,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtistSong_SongsId",
                table: "ArtistSong",
                column: "SongsId");

            migrationBuilder.CreateIndex(
                name: "IX_CategorySong_SongsId",
                table: "CategorySong",
                column: "SongsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtistSong");

            migrationBuilder.DropTable(
                name: "CategorySong");

            migrationBuilder.AddColumn<int>(
                name: "SongId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SongId",
                table: "Artists",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_SongId",
                table: "Categories",
                column: "SongId");

            migrationBuilder.CreateIndex(
                name: "IX_Artists_SongId",
                table: "Artists",
                column: "SongId");

            migrationBuilder.AddForeignKey(
                name: "FK_Artists_Songs_SongId",
                table: "Artists",
                column: "SongId",
                principalTable: "Songs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Songs_SongId",
                table: "Categories",
                column: "SongId",
                principalTable: "Songs",
                principalColumn: "Id");
        }
    }
}
