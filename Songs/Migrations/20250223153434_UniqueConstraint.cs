using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Songs.Migrations
{
    /// <inheritdoc />
    public partial class UniqueConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Songs_Title",
                table: "Songs",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Genre",
                table: "Categories",
                column: "Genre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Artists_ArtistName",
                table: "Artists",
                column: "ArtistName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Songs_Title",
                table: "Songs");

            migrationBuilder.DropIndex(
                name: "IX_Categories_Genre",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Artists_ArtistName",
                table: "Artists");
        }
    }
}
