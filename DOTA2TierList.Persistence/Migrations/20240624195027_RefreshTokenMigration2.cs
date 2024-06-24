using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DOTA2TierList.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RefreshTokenMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RefreshTokenExpire",
                table: "Users",
                newName: "RefreshTokenExpires");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RefreshTokenExpires",
                table: "Users",
                newName: "RefreshTokenExpire");
        }
    }
}
