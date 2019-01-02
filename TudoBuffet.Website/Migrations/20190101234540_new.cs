using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TudoBuffet.Website.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    Profile = table.Column<string>(maxLength: 20, nullable: false),
                    Discriminator = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Budgets",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    UpdateAt = table.Column<DateTime>(nullable: true),
                    CustomerId = table.Column<Guid>(nullable: true),
                    EmailSender = table.Column<string>(maxLength: 256, nullable: true),
                    QuantityPartyGuests = table.Column<int>(nullable: false),
                    DayParty = table.Column<DateTime>(nullable: false),
                    Observation = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budgets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Budgets_Users_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    Zipcode = table.Column<string>(maxLength: 256, nullable: true),
                    Street = table.Column<string>(maxLength: 256, nullable: true),
                    Number = table.Column<string>(maxLength: 256, nullable: true),
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
                    Title = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buffets", x => x.Id);
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
                name: "BudgetBuffet",
                columns: table => new
                {
                    BudgetId = table.Column<Guid>(nullable: false),
                    BuffetId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetBuffet", x => new { x.BudgetId, x.BuffetId });
                    table.ForeignKey(
                        name: "FK_BudgetBuffet_Budgets_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "Budgets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BudgetBuffet_Buffets_BuffetId",
                        column: x => x.BuffetId,
                        principalTable: "Buffets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    SearchUrl = table.Column<string>(maxLength: 256, nullable: true),
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
                values: new object[] { new Guid("e5d61358-2a65-490f-81cd-c1d57ef78b28"), new DateTime(2019, 1, 1, 21, 45, 38, 767, DateTimeKind.Local), "O plano ouro favorece o aparecimento em mais vezes nas pesquisas e irá aparecer com mais frequencia no destaques do dia", "img/planouro.jpg", true, "Plano ouro", 1, 30.00m, null });

            migrationBuilder.InsertData(
                table: "Plans",
                columns: new[] { "Id", "CreateAt", "Description", "Image", "IsActive", "Name", "Order", "Price", "UpdateAt" },
                values: new object[] { new Guid("86a6e4e8-5de1-4ffc-8e68-8741843f50b8"), new DateTime(2019, 1, 1, 21, 45, 38, 814, DateTimeKind.Local), "O plano prata está a frente do plano bronze e também irá aparecer nas pesquisa com uma boa frequencia e também estará presente nos destaques do dia", "img/planprata.jpg", true, "Plano prata", 2, 20.00m, null });

            migrationBuilder.InsertData(
                table: "Plans",
                columns: new[] { "Id", "CreateAt", "Description", "Image", "IsActive", "Name", "Order", "Price", "UpdateAt" },
                values: new object[] { new Guid("8c25b3a5-9b4a-400e-a641-6c5af0a1c7bd"), new DateTime(2019, 1, 1, 21, 45, 38, 814, DateTimeKind.Local), "O plano bronze irá aparecer nas pesquisas, mas com menos frequencia na primeira página", "img/planbronze.jpg", true, "Plano bronze", 3, 10.00m, null });

            migrationBuilder.CreateIndex(
                name: "IX_BudgetBuffet_BuffetId",
                table: "BudgetBuffet",
                column: "BuffetId");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_CustomerId",
                table: "Budgets",
                column: "CustomerId");

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
                name: "BudgetBuffet");

            migrationBuilder.DropTable(
                name: "EmailsValidation");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "Budgets");

            migrationBuilder.DropTable(
                name: "Buffets");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Plans");
        }
    }
}
