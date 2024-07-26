using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DOTA2TierList.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SteamProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<long>(
                name: "SteamProfileId",
                table: "Users",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SteamProfileEntity",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MatchMakingRating = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SteamProfileEntity", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_SteamProfileId",
                table: "Users",
                column: "SteamProfileId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_SteamProfileEntity_SteamProfileId",
                table: "Users",
                column: "SteamProfileId",
                principalTable: "SteamProfileEntity",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_SteamProfileEntity_SteamProfileId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "SteamProfileEntity");

            migrationBuilder.DropIndex(
                name: "IX_Users_SteamProfileId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SteamProfileId",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
