using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI_2025.Migrations
{
    /// <inheritdoc />
    public partial class check : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEN4hCNSHhAgdKMVT+960ybn0Btz6Fz02D7R+IfeB3PQEFnc9Ci1Iv64VhllhD0mWhg==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEEwdnRXAxtsTvQ7DCLDrUBaGnWf9lHPhOR7yJm/39Eqh156wtCS9b4pe9HkoGG143g==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEKw3Us5ILJCY43E6doh64QXyv7qvYLzbbSFRoBzBzvDA4p+IsjKRGX3p//7cRquWYA==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEEhXXzRBNW0LiTx+3nyVMY1J1guqumtMR8fQ6ibYW2YJp57Ukpr8/w1EpHO3H7ihPA==");
        }
    }
}
