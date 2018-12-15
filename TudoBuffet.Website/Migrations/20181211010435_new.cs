using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TudoBuffet.Website.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Budgets",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmailSender = table.Column<string>(maxLength: 256, nullable: true),
                    QuantityPartyGuests = table.Column<int>(nullable: false),
                    DayParty = table.Column<DateTime>(nullable: false),
                    Observation = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budgets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailsValidation",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    UpdateAt = table.Column<DateTime>(nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    Token = table.Column<string>(maxLength: 256, nullable: true),
                    ExpireAt = table.Column<DateTime>(nullable: false),
                    WasValidate = table.Column<bool>(nullable: false),
                    ValidateAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailsValidation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    UpdateAt = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Image = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    UpdateAt = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(maxLength: 256, nullable: true),
                    Salt = table.Column<string>(maxLength: 256, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    ActivedAt = table.Column<DateTime>(nullable: true),
                    Profile = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Buffets",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    UpdateAt = table.Column<DateTime>(nullable: true),
                    OwnerId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    Street = table.Column<string>(maxLength: 256, nullable: true),
                    Number = table.Column<int>(nullable: true),
                    District = table.Column<string>(maxLength: 256, nullable: true),
                    City = table.Column<string>(maxLength: 256, nullable: true),
                    State = table.Column<string>(maxLength: 256, nullable: true),
                    Cellphone = table.Column<string>(maxLength: 256, nullable: true),
                    Facebook = table.Column<string>(maxLength: 256, nullable: true),
                    Instagram = table.Column<string>(maxLength: 256, nullable: true),
                    PlanSelectedId = table.Column<Guid>(nullable: true),
                    Category = table.Column<string>(maxLength: 20, nullable: false),
                    Price = table.Column<string>(maxLength: 20, nullable: false),
                    ActivedAt = table.Column<DateTime>(nullable: true),
                    Environment = table.Column<string>(maxLength: 20, nullable: false),
                    BudgetId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buffets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Buffets_Budgets_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "Budgets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Buffets_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Buffets_Plans_PlanSelectedId",
                        column: x => x.PlanSelectedId,
                        principalTable: "Plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    UpdateAt = table.Column<DateTime>(nullable: true),
                    BuffetId = table.Column<Guid>(nullable: true),
                    ContainerName = table.Column<string>(maxLength: 256, nullable: true),
                    DetailFileName = table.Column<string>(maxLength: 256, nullable: true),
                    DetailUrl = table.Column<string>(maxLength: 256, nullable: true),
                    SearchFileName = table.Column<string>(maxLength: 256, nullable: true),
                    SearachUrl = table.Column<string>(maxLength: 256, nullable: true),
                    ThumbnailFileName = table.Column<string>(maxLength: 256, nullable: true),
                    ThumbnailUrl = table.Column<string>(maxLength: 256, nullable: true),
                    Size = table.Column<long>(nullable: false),
                    Type = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_Buffets_BuffetId",
                        column: x => x.BuffetId,
                        principalTable: "Buffets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Plans",
                columns: new[] { "Id", "CreateAt", "Description", "Image", "IsActive", "Name", "Order", "Price", "UpdateAt" },
                values: new object[] { new Guid("68dc7964-0385-4bee-95a4-17e23f00c57a"), new DateTime(2018, 12, 10, 23, 4, 34, 252, DateTimeKind.Local), "O plano ouro favorece o aparecimento em mais vezes nas pesquisas e irá aparecer com mais frequencia no destaques do dia", "img/planouro.jpg", true, "Plano ouro", 1, 30.00m, null });

            migrationBuilder.InsertData(
                table: "Plans",
                columns: new[] { "Id", "CreateAt", "Description", "Image", "IsActive", "Name", "Order", "Price", "UpdateAt" },
                values: new object[] { new Guid("58878d10-dd49-4422-bd0c-0eca5f33dffe"), new DateTime(2018, 12, 10, 23, 4, 34, 260, DateTimeKind.Local), "O plano prata está a frente do plano bronze e também irá aparecer nas pesquisa com uma boa frequencia e também estará presente nos destaques do dia", "img/planprata.jpg", true, "Plano prata", 2, 20.00m, null });

            migrationBuilder.InsertData(
                table: "Plans",
                columns: new[] { "Id", "CreateAt", "Description", "Image", "IsActive", "Name", "Order", "Price", "UpdateAt" },
                values: new object[] { new Guid("6d9c516c-4d8c-451c-a8df-ef7f5dd8c83d"), new DateTime(2018, 12, 10, 23, 4, 34, 260, DateTimeKind.Local), "O plano bronze irá aparecer nas pesquisas, mas com menos frequencia na primeira página", "img/planbronze.jpg", true, "Plano bronze", 3, 10.00m, null });

            migrationBuilder.CreateIndex(
                name: "IX_Buffets_BudgetId",
                table: "Buffets",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_Buffets_OwnerId",
                table: "Buffets",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Buffets_PlanSelectedId",
                table: "Buffets",
                column: "PlanSelectedId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_BuffetId",
                table: "Photos",
                column: "BuffetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailsValidation");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "Buffets");

            migrationBuilder.DropTable(
                name: "Budgets");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Plans");
        }
    }
}
