using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shablon.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_AspNetUsers_creatorId",
                table: "Locations");

            migrationBuilder.RenameColumn(
                name: "creatorId",
                table: "Locations",
                newName: "ShablonUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Locations_creatorId",
                table: "Locations",
                newName: "IX_Locations_ShablonUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_AspNetUsers_ShablonUserId",
                table: "Locations",
                column: "ShablonUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_AspNetUsers_ShablonUserId",
                table: "Locations");

            migrationBuilder.RenameColumn(
                name: "ShablonUserId",
                table: "Locations",
                newName: "creatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Locations_ShablonUserId",
                table: "Locations",
                newName: "IX_Locations_creatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_AspNetUsers_creatorId",
                table: "Locations",
                column: "creatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
