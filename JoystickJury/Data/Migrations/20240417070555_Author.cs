using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoystickJury.Data.Migrations
{
    /// <inheritdoc />
    public partial class Author : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_AspNetUsers_UserId",
                schema: "Identity",
                table: "Review");

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "Identity",
                table: "Review",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Review_UserId",
                schema: "Identity",
                table: "Review",
                newName: "IX_Review_AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_AspNetUsers_AuthorId",
                schema: "Identity",
                table: "Review",
                column: "AuthorId",
                principalSchema: "Identity",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_AspNetUsers_AuthorId",
                schema: "Identity",
                table: "Review");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                schema: "Identity",
                table: "Review",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Review_AuthorId",
                schema: "Identity",
                table: "Review",
                newName: "IX_Review_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_AspNetUsers_UserId",
                schema: "Identity",
                table: "Review",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
