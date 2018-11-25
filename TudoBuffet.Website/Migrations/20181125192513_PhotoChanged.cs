using Microsoft.EntityFrameworkCore.Migrations;

namespace TudoBuffet.Website.Migrations
{
    public partial class PhotoChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Path",
                table: "Photo",
                newName: "UrlThumbnail");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Photo",
                maxLength: 256,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Photo");

            migrationBuilder.RenameColumn(
                name: "UrlThumbnail",
                table: "Photo",
                newName: "Path");
        }
    }
}
