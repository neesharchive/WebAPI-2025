using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI_2025.Migrations
{
    public partial class entity_seeded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Email", "Password", "UserName", "role" },
                values: new object[,]
                {
                    { 1, "nishantbhatt393@gmail.com", "AQAAAAIAAYagAAAAEA13CAUtTLZrhRw6nTmcp3LL4PHM0gp/LUcz/NyWr97c70sX9Gs+4ehwSOT6oQVE/g==", "admin", 1 },
                    { 2, "nab5996@psu.com", "AQAAAAIAAYagAAAAEKMPELeXa1nM00ByPNUuEjw3lkBAmoNDO8ybyh+H6xs7MB9/vrhSDyfNJ/rn/XBqjg==", "user", 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2);
        }
    }
}
