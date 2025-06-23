using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI_2025.Migrations
{
    /// <inheritdoc />
    public partial class fixeses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEBMBNqm9i8Shkj3KNRqAoCj5fuHsxLWZQtPtWdOwB//EFd8NLUJVGg8wDgAV1Ir4rw==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEASITv9cawR2a59lPUHQVRChxUJvCz9qBYnuxjKmnUnRZqPJVsOfTyKV+WlWy9jqZA==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
