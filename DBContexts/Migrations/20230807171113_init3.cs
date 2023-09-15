using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityInfo.DBContexts.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "pointOfInterests",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "pointOfInterests",
                keyColumn: "Id",
                keyValue: 200);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "Id",
                keyValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "cities",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 1, "About first mohamed ibrahim fayad kandil", "Mohamed" });

            migrationBuilder.InsertData(
                table: "cities",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 2, "About Two mohamed ibrahim fayad kandil", "Ahmed" });

            migrationBuilder.InsertData(
                table: "cities",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 3, "About Thered mohamed ibrahim fayad kandil", "Wallid" });

            migrationBuilder.InsertData(
                table: "pointOfInterests",
                columns: new[] { "Id", "Description", "Name", "cityid" },
                values: new object[] { 100, "About Second mohamed ibrahim fayad kandil", "Loving", 1 });

            migrationBuilder.InsertData(
                table: "pointOfInterests",
                columns: new[] { "Id", "Description", "Name", "cityid" },
                values: new object[] { 200, "About first mohamed ibrahim fayad kandil", "Gamming", 1 });
        }
    }
}
