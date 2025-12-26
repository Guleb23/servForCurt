using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ApiForSud.Migrations
{
    /// <inheritdoc />
    public partial class create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Curts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Login = table.Column<string>(type: "text", nullable: true),
                    FIO = table.Column<string>(type: "text", nullable: true),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    RefreshToken = table.Column<string>(type: "text", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NomerOfCase = table.Column<string>(type: "text", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: false),
                    Third = table.Column<string>(type: "text", nullable: false),
                    Applicant = table.Column<string>(type: "text", nullable: false),
                    Defendant = table.Column<string>(type: "text", nullable: false),
                    Reason = table.Column<string>(type: "text", nullable: false),
                    DateOfCurt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ResultOfCurt = table.Column<string>(type: "text", nullable: false),
                    DateOfResult = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsMarkeredByAdmin = table.Column<bool>(type: "boolean", nullable: false),
                    IsUnMarkeredByAdmin = table.Column<bool>(type: "boolean", nullable: false),
                    IsNotificated = table.Column<bool>(type: "boolean", nullable: false),
                    IsArhcived = table.Column<bool>(type: "boolean", nullable: false),
                    ArchivedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CurtId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cases_Curts_CurtId",
                        column: x => x.CurtId,
                        principalTable: "Curts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cases_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CurtInstances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    NameOfCurt = table.Column<string>(type: "text", nullable: false),
                    DateOfSession = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Link = table.Column<string>(type: "text", nullable: false),
                    Employee = table.Column<string>(type: "text", nullable: false),
                    ResultOfIstance = table.Column<string>(type: "text", nullable: false),
                    Report = table.Column<string>(type: "text", nullable: false),
                    DateOfResult = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CaseId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurtInstances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurtInstances_Cases_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Curts",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Железнодорожный районный суд" },
                    { 2, "Киевский районный суд" },
                    { 3, "Верховный суд РК" },
                    { 4, "Верховный суд РФ" },
                    { 5, "Симферопольский районный суд" },
                    { 6, "Арбитражный суд РК" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "User" },
                    { 2, "Admin" },
                    { 3, "Director" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cases_CurtId",
                table: "Cases",
                column: "CurtId");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_UserId",
                table: "Cases",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CurtInstances_CaseId",
                table: "CurtInstances",
                column: "CaseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurtInstances");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Cases");

            migrationBuilder.DropTable(
                name: "Curts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
