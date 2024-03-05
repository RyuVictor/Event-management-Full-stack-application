using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bajaj.Events.Dal.Migrations
{
    /// <inheritdoc />
    public partial class AddedRefreshTokenWithExpiry2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RefreshTimeExpiry",
                table: "User",
                newName: "RefreshTokenExpiry");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RefreshTokenExpiry",
                table: "User",
                newName: "RefreshTimeExpiry");
        }
    }
}
