using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI_2025.Migrations
{
    /// <inheritdoc />
    public partial class GHStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "guestHouses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "guestHouses");
        }
    }
}
