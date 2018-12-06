using Microsoft.EntityFrameworkCore.Migrations;

namespace TudoBuffet.Website.Migrations
{
    public partial class photosv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMainPhoto",
                table: "Photos");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Photos",
                newName: "ThumbnailFileName");

            migrationBuilder.RenameColumn(
                name: "ThumbnailName",
                table: "Photos",
                newName: "SearchFileName");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "Photos",
                newName: "SearachUrl");

            migrationBuilder.AddColumn<string>(
                name: "DetailFileName",
                table: "Photos",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DetailUrl",
                table: "Photos",
                maxLength: 256,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DetailFileName",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "DetailUrl",
                table: "Photos");

            migrationBuilder.RenameColumn(
                name: "ThumbnailFileName",
                table: "Photos",
                newName: "Url");

            migrationBuilder.RenameColumn(
                name: "SearchFileName",
                table: "Photos",
                newName: "ThumbnailName");

            migrationBuilder.RenameColumn(
                name: "SearachUrl",
                table: "Photos",
                newName: "FileName");

            migrationBuilder.AddColumn<bool>(
                name: "IsMainPhoto",
                table: "Photos",
                nullable: false,
                defaultValue: false);
        }
    }
}
