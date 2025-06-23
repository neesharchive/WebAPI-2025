using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI_2025.Migrations
{
    /// <inheritdoc />
    public partial class fixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEG+1fXirpooii4M4cQoJpbUjGo43wrCiS2PB2cZlK3LRU4Nq2uw4byZsWG3WPAk5uA==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEO8UjtiGjiHh0X/j9HuT5/XV0M0nThwrDRlQmk06MMCvbV0mJfVwTWSDhVSnHf2tNg==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                column: "Password",
                value: "admin123");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "Password",
                value: "user123");
        }
    }
}
