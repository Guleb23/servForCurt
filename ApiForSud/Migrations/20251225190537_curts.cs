using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ApiForSud.Migrations
{
    /// <inheritdoc />
    public partial class curts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurtId",
                table: "Cases",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Curt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CaseId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curt", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cases_CurtId",
                table: "Cases",
                column: "CurtId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Curt_CurtId",
                table: "Cases",
                column: "CurtId",
                principalTable: "Curt",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Curt_CurtId",
                table: "Cases");

            migrationBuilder.DropTable(
                name: "Curt");

            migrationBuilder.DropIndex(
                name: "IX_Cases_CurtId",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "CurtId",
                table: "Cases");
        }
    }
}
