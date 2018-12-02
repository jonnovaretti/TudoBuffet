using Microsoft.EntityFrameworkCore.Migrations;

namespace TudoBuffet.Website.Migrations
{
    public partial class thumbnail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ThumbprintName",
                table: "Photos",
                newName: "ThumbnailName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ThumbnailName",
                table: "Photos",
                newName: "ThumbprintName");
        }
    }
}
