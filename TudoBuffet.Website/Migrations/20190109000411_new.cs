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
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Image = table.Column<string>(maxLength: 256, nullable: false)
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
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    Email = table.Column<string>(maxLength: 256, nullable: false),
                    PasswordHash = table.Column<string>(maxLength: 256, nullable: false),
                    Salt = table.Column<string>(maxLength: 256, nullable: false),
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
                    PartyOwnerId = table.Column<Guid>(nullable: false),
                    QuantityPartyGuests = table.Column<int>(nullable: false),
                    PartyDay = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budgets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Budgets_Users_PartyOwnerId",
                        column: x => x.PartyOwnerId,
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
                    OwnerId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Zipcode = table.Column<string>(maxLength: 256, nullable: false),
                    Street = table.Column<string>(maxLength: 256, nullable: false),
                    Number = table.Column<string>(maxLength: 256, nullable: true),
                    District = table.Column<string>(maxLength: 256, nullable: false),
                    City = table.Column<string>(maxLength: 256, nullable: false),
                    State = table.Column<string>(maxLength: 256, nullable: false),
                    Cellphone = table.Column<string>(maxLength: 256, nullable: true),
                    Facebook = table.Column<string>(maxLength: 256, nullable: true),
                    Instagram = table.Column<string>(maxLength: 256, nullable: true),
                    PlanSelectedId = table.Column<Guid>(nullable: true),
                    Category = table.Column<string>(maxLength: 20, nullable: false),
                    Price = table.Column<string>(maxLength: 20, nullable: false),
                    ActivedAt = table.Column<DateTime>(nullable: true),
                    Environment = table.Column<string>(maxLength: 20, nullable: false),
                    Title = table.Column<string>(maxLength: 256, nullable: true),
                    UrlPage = table.Column<string>(maxLength: 256, nullable: true)
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
                name: "BudgetDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    UpdateAt = table.Column<DateTime>(nullable: true),
                    BuffetId = table.Column<Guid>(nullable: true),
                    IsDateAvaliable = table.Column<bool>(nullable: false),
                    ProposedDateFor = table.Column<DateTime>(nullable: true),
                    AnsweredAt = table.Column<DateTime>(nullable: true),
                    BudgetId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BudgetDetail_Budgets_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "Budgets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BudgetDetail_Buffets_BuffetId",
                        column: x => x.BuffetId,
                        principalTable: "Buffets",
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
                    BuffetId = table.Column<Guid>(nullable: false),
                    ContainerName = table.Column<string>(maxLength: 256, nullable: false),
                    DetailFileName = table.Column<string>(maxLength: 256, nullable: false),
                    DetailUrl = table.Column<string>(maxLength: 256, nullable: false),
                    SearchFileName = table.Column<string>(maxLength: 256, nullable: false),
                    SearchUrl = table.Column<string>(maxLength: 256, nullable: false),
                    ThumbnailFileName = table.Column<string>(maxLength: 256, nullable: false),
                    ThumbnailUrl = table.Column<string>(maxLength: 256, nullable: false),
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

            migrationBuilder.CreateTable(
                name: "BudgetQuestion",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    UpdateAt = table.Column<DateTime>(nullable: true),
                    Question = table.Column<string>(maxLength: 256, nullable: false),
                    Answer = table.Column<string>(maxLength: 256, nullable: true),
                    BudgetDetailId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BudgetQuestion_BudgetDetail_BudgetDetailId",
                        column: x => x.BudgetDetailId,
                        principalTable: "BudgetDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_BudgetDetail_BudgetId",
                table: "BudgetDetail",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetDetail_BuffetId",
                table: "BudgetDetail",
                column: "BuffetId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetQuestion_BudgetDetailId",
                table: "BudgetQuestion",
                column: "BudgetDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_PartyOwnerId",
                table: "Budgets",
                column: "PartyOwnerId");

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
                name: "BudgetQuestion");

            migrationBuilder.DropTable(
                name: "EmailsValidation");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "BudgetDetail");

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
