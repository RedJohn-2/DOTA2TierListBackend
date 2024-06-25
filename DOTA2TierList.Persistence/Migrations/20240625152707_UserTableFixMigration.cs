using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DOTA2TierList.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UserTableFixMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_Users_Email",
                table: "Users",
                column: "Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Users_Email",
                table: "Users");
        }
    }
}
