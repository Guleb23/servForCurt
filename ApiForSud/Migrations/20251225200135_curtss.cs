using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ApiForSud.Migrations
{
    /// <inheritdoc />
    public partial class curtss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameOfCurt",
                table: "Cases");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Curts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Curts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Curts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Curts",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Curts",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Curts",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.AddColumn<string>(
                name: "NameOfCurt",
                table: "Cases",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
