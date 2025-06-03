using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI_2025.Migrations
{
    /// <inheritdoc />
    public partial class passwords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordResetToken",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TokenExpiryTime",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "Password", "PasswordResetToken", "TokenExpiryTime" },
                values: new object[] { "AQAAAAIAAYagAAAAEKw3Us5ILJCY43E6doh64QXyv7qvYLzbbSFRoBzBzvDA4p+IsjKRGX3p//7cRquWYA==", null, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "Password", "PasswordResetToken", "TokenExpiryTime" },
                values: new object[] { "AQAAAAIAAYagAAAAEEhXXzRBNW0LiTx+3nyVMY1J1guqumtMR8fQ6ibYW2YJp57Ukpr8/w1EpHO3H7ihPA==", null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordResetToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TokenExpiryTime",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEA13CAUtTLZrhRw6nTmcp3LL4PHM0gp/LUcz/NyWr97c70sX9Gs+4ehwSOT6oQVE/g==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEKMPELeXa1nM00ByPNUuEjw3lkBAmoNDO8ybyh+H6xs7MB9/vrhSDyfNJ/rn/XBqjg==");
        }
    }
}
