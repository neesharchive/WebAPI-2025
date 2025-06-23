using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI_2025.Migrations
{
    /// <inheritdoc />
    public partial class users : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEF8rbWupa8JO53TdXUpUrv2seHDD5z5GqkOozNCjpeFiGmAoPI4EYnOfNiFIpAqmmA==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEKm0v3Arr0LeznvrXndm4CfpEvDAzsoHmjkMWUE/t+VC0ZFm18cvJ+RPQUww+irtBw==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
