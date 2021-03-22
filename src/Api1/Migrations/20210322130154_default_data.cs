using Microsoft.EntityFrameworkCore.Migrations;

namespace Api1.Migrations
{
    public partial class default_data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Catalogs",
                columns: new[] { "Id", "Price", "Title" },
                values: new object[] { 1, 1000, "Catalog 1" });

            migrationBuilder.InsertData(
                table: "Catalogs",
                columns: new[] { "Id", "Price", "Title" },
                values: new object[] { 2, 2000, "Catalog 2" });

            migrationBuilder.InsertData(
                table: "Catalogs",
                columns: new[] { "Id", "Price", "Title" },
                values: new object[] { 3, 3000, "Catalog 3" });

            migrationBuilder.InsertData(
                table: "Catalogs",
                columns: new[] { "Id", "Price", "Title" },
                values: new object[] { 4, 4000, "Catalog 4" });

            migrationBuilder.InsertData(
                table: "Catalogs",
                columns: new[] { "Id", "Price", "Title" },
                values: new object[] { 5, 5000, "Catalog 5" });

            migrationBuilder.InsertData(
                table: "Catalogs",
                columns: new[] { "Id", "Price", "Title" },
                values: new object[] { 6, 6000, "Catalog 6" });

            migrationBuilder.InsertData(
                table: "Catalogs",
                columns: new[] { "Id", "Price", "Title" },
                values: new object[] { 7, 7000, "Catalog 7" });

            migrationBuilder.InsertData(
                table: "Catalogs",
                columns: new[] { "Id", "Price", "Title" },
                values: new object[] { 8, 8000, "Catalog 8" });

            migrationBuilder.InsertData(
                table: "Catalogs",
                columns: new[] { "Id", "Price", "Title" },
                values: new object[] { 9, 9000, "Catalog 9" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 9);
        }
    }
}
