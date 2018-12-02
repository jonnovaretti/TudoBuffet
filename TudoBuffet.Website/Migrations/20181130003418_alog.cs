using Microsoft.EntityFrameworkCore.Migrations;

namespace TudoBuffet.Website.Migrations
{
    public partial class alog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photo_Buffets_BuffetId",
                table: "Photo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Photo",
                table: "Photo");

            migrationBuilder.RenameTable(
                name: "Photo",
                newName: "Photos");

            migrationBuilder.RenameIndex(
                name: "IX_Photo_BuffetId",
                table: "Photos",
                newName: "IX_Photos_BuffetId");

            migrationBuilder.AddColumn<string>(
                name: "ContainerName",
                table: "Photos",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Photos",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThumbprintName",
                table: "Photos",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Photos",
                table: "Photos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Buffets_BuffetId",
                table: "Photos",
                column: "BuffetId",
                principalTable: "Buffets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Buffets_BuffetId",
                table: "Photos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Photos",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "ContainerName",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "ThumbprintName",
                table: "Photos");

            migrationBuilder.RenameTable(
                name: "Photos",
                newName: "Photo");

            migrationBuilder.RenameIndex(
                name: "IX_Photos_BuffetId",
                table: "Photo",
                newName: "IX_Photo_BuffetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Photo",
                table: "Photo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_Buffets_BuffetId",
                table: "Photo",
                column: "BuffetId",
                principalTable: "Buffets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
