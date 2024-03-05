using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bajaj.Events.Dal.Migrations
{
    /// <inheritdoc />
    public partial class AddedRefreshTokenWithExpiry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTimeExpiry",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "User",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshTimeExpiry",
                table: "User");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "User");
        }
    }
}
