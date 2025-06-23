using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI_2025.Migrations
{
    /// <inheritdoc />
    public partial class notification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "notification",
                columns: table => new
                {
                    NotificationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    TargetRole = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notification", x => x.NotificationID);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEGYKHhDTxdesg+gHxO8k6VIeCojPpAHY9uz/njov9h7CeSFuixET3j2lDpAVIBIcRA==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEAa+Nh/76eCb7cg4Zubyfe4+hbCVDFiv1+K2HuaKVsv29xwJHAUO0pTerfmD0EriEg==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "notification");

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
    }
}
