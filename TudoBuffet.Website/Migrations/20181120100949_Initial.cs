using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TudoBuffet.Website.Migrations
{
    public partial class Initial : Migration
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
                name: "Plan",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    UpdateAt = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    QuantityDaysActive = table.Column<int>(nullable: false),
                    QuantityPhotosAllowed = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plan", x => x.Id);
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
                    ActivedAt = table.Column<DateTime>(nullable: true)
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
                    Address = table.Column<string>(maxLength: 256, nullable: true),
                    City = table.Column<string>(maxLength: 256, nullable: true),
                    State = table.Column<string>(maxLength: 256, nullable: true),
                    Facebook = table.Column<string>(maxLength: 256, nullable: true),
                    CelPhone = table.Column<string>(maxLength: 256, nullable: true),
                    PlanSelectedId = table.Column<Guid>(nullable: true),
                    Category = table.Column<string>(maxLength: 20, nullable: false),
                    PriceRange = table.Column<int>(nullable: false),
                    ActivedAt = table.Column<DateTime>(nullable: true),
                    ActiveUntil = table.Column<DateTime>(nullable: true)
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
                        name: "FK_Buffets_Plan_PlanSelectedId",
                        column: x => x.PlanSelectedId,
                        principalTable: "Plan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Photo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    UpdateAt = table.Column<DateTime>(nullable: true),
                    BuffetId = table.Column<Guid>(nullable: true),
                    Title = table.Column<string>(maxLength: 256, nullable: true),
                    Path = table.Column<string>(maxLength: 256, nullable: true),
                    IsMainPhoto = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photo_Buffets_BuffetId",
                        column: x => x.BuffetId,
                        principalTable: "Buffets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Buffets_OwnerId",
                table: "Buffets",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Buffets_PlanSelectedId",
                table: "Buffets",
                column: "PlanSelectedId");

            migrationBuilder.CreateIndex(
                name: "IX_Photo_BuffetId",
                table: "Photo",
                column: "BuffetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailsValidation");

            migrationBuilder.DropTable(
                name: "Photo");

            migrationBuilder.DropTable(
                name: "Buffets");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Plan");
        }
    }
}
