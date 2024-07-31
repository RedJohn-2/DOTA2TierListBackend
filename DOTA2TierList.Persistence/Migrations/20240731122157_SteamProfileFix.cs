using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DOTA2TierList.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SteamProfileFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_SteamProfileEntity_SteamProfileId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SteamProfileEntity",
                table: "SteamProfileEntity");

            migrationBuilder.RenameTable(
                name: "SteamProfileEntity",
                newName: "SteamProfiles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SteamProfiles",
                table: "SteamProfiles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_SteamProfiles_SteamProfileId",
                table: "Users",
                column: "SteamProfileId",
                principalTable: "SteamProfiles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_SteamProfiles_SteamProfileId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SteamProfiles",
                table: "SteamProfiles");

            migrationBuilder.RenameTable(
                name: "SteamProfiles",
                newName: "SteamProfileEntity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SteamProfileEntity",
                table: "SteamProfileEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_SteamProfileEntity_SteamProfileId",
                table: "Users",
                column: "SteamProfileId",
                principalTable: "SteamProfileEntity",
                principalColumn: "Id");
        }
    }
}
