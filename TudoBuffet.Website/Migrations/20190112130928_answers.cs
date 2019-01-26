using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TudoBuffet.Website.Migrations
{
    public partial class answers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Answer",
                table: "BudgetQuestion");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDateAvaliable",
                table: "BudgetDetail",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.CreateTable(
                name: "BudgetAnswer",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    UpdateAt = table.Column<DateTime>(nullable: true),
                    QuestionId = table.Column<Guid>(nullable: true),
                    Answer = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BudgetAnswer_BudgetQuestion_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "BudgetQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateIndex(
                name: "IX_BudgetAnswer_QuestionId",
                table: "BudgetAnswer",
                column: "QuestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BudgetAnswer");

            migrationBuilder.DeleteData(
                table: "Plans",
                keyColumn: "Id",
                keyValue: new Guid("0bb7a654-a069-4a7f-8017-9dbe30680709"));

            migrationBuilder.DeleteData(
                table: "Plans",
                keyColumn: "Id",
                keyValue: new Guid("d98c21c2-b944-4b50-9caa-e33294623b9e"));

            migrationBuilder.DeleteData(
                table: "Plans",
                keyColumn: "Id",
                keyValue: new Guid("e9dc20fd-36fb-4b5d-be32-3b16985aa3fe"));

            migrationBuilder.DropColumn(
                name: "UrlPage",
                table: "Buffets");

            migrationBuilder.AddColumn<string>(
                name: "Answer",
                table: "BudgetQuestion",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDateAvaliable",
                table: "BudgetDetail",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Plans",
                columns: new[] { "Id", "CreateAt", "Description", "Image", "IsActive", "Name", "Order", "Price", "UpdateAt" },
                values: new object[] { new Guid("50083a48-6734-4605-a816-862d6cacb68f"), new DateTime(2019, 1, 8, 22, 4, 9, 609, DateTimeKind.Local), "O plano ouro favorece o aparecimento em mais vezes nas pesquisas e irá aparecer com mais frequencia no destaques do dia", "img/planouro.jpg", true, "Plano ouro", 1, 30.00m, null });

            migrationBuilder.InsertData(
                table: "Plans",
                columns: new[] { "Id", "CreateAt", "Description", "Image", "IsActive", "Name", "Order", "Price", "UpdateAt" },
                values: new object[] { new Guid("3cbea676-a2cb-4d9e-8612-b37386ffdddf"), new DateTime(2019, 1, 8, 22, 4, 9, 698, DateTimeKind.Local), "O plano prata está a frente do plano bronze e também irá aparecer nas pesquisa com uma boa frequencia e também estará presente nos destaques do dia", "img/planprata.jpg", true, "Plano prata", 2, 20.00m, null });

            migrationBuilder.InsertData(
                table: "Plans",
                columns: new[] { "Id", "CreateAt", "Description", "Image", "IsActive", "Name", "Order", "Price", "UpdateAt" },
                values: new object[] { new Guid("d2421512-41ff-4f6c-9df8-a0607eec5c1b"), new DateTime(2019, 1, 8, 22, 4, 9, 698, DateTimeKind.Local), "O plano bronze irá aparecer nas pesquisas, mas com menos frequencia na primeira página", "img/planbronze.jpg", true, "Plano bronze", 3, 10.00m, null });
        }
    }
}
