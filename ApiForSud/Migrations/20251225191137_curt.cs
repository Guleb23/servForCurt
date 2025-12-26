using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiForSud.Migrations
{
    /// <inheritdoc />
    public partial class curt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Curt_CurtId",
                table: "Cases");

            migrationBuilder.DropIndex(
                name: "IX_Cases_CurtId",
                table: "Cases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Curt",
                table: "Curt");

            migrationBuilder.DropColumn(
                name: "CaseId",
                table: "Curt");

            migrationBuilder.RenameTable(
                name: "Curt",
                newName: "Curts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Curts",
                table: "Curts",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_CurtId",
                table: "Cases",
                column: "CurtId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Curts_CurtId",
                table: "Cases",
                column: "CurtId",
                principalTable: "Curts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Curts_CurtId",
                table: "Cases");

            migrationBuilder.DropIndex(
                name: "IX_Cases_CurtId",
                table: "Cases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Curts",
                table: "Curts");

            migrationBuilder.RenameTable(
                name: "Curts",
                newName: "Curt");

            migrationBuilder.AddColumn<int>(
                name: "CaseId",
                table: "Curt",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Curt",
                table: "Curt",
                column: "Id");

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
    }
}
