using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityInfo.DBContexts.Migrations
{
    public partial class init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "pointOfInterests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cityid = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pointOfInterests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_pointOfInterests_cities_cityid",
                        column: x => x.cityid,
                        principalTable: "cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_pointOfInterests_cityid",
                table: "pointOfInterests",
                column: "cityid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pointOfInterests");

            migrationBuilder.DropTable(
                name: "cities");
        }
    }
}
