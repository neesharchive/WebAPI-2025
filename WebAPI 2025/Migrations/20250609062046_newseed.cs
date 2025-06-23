using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI_2025.Migrations
{
    /// <inheritdoc />
    public partial class newseed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEF0z0c+yi+100q6/GfscHM3ldGr7q25qrTv8bHdB0/Qx9d29w5j2wJFkCc8YjCk93g==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEI1FyTqXZjtDpszd9fwV5PgukZ5PPTKyusa6NOMOkROqD+lOv1q8CKqekPbkzaGHYQ==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
