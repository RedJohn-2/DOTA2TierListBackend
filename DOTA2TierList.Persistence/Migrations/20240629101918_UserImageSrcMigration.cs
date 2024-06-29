using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DOTA2TierList.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UserImageSrcMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "TierItems",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "TierItems");
        }
    }
}
