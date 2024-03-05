using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bajaj.Events.Dal.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserIdInEventRegistration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "EventRegistrations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EventRegistrations_UserId",
                table: "EventRegistrations",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EventRegistrations_User_UserId",
                table: "EventRegistrations",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventRegistrations_User_UserId",
                table: "EventRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_EventRegistrations_UserId",
                table: "EventRegistrations");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "EventRegistrations");
        }
    }
}
