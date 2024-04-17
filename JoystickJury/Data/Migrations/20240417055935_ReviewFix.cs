using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoystickJury.Data.Migrations
{
    /// <inheritdoc />
    public partial class ReviewFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_Game_GameId",
                schema: "Identity",
                table: "Review");

            migrationBuilder.AlterColumn<int>(
                name: "Likes",
                schema: "Identity",
                table: "Review",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                schema: "Identity",
                table: "Review",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Dislikes",
                schema: "Identity",
                table: "Review",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Game_GameId",
                schema: "Identity",
                table: "Review",
                column: "GameId",
                principalSchema: "Identity",
                principalTable: "Game",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_Game_GameId",
                schema: "Identity",
                table: "Review");

            migrationBuilder.AlterColumn<int>(
                name: "Likes",
                schema: "Identity",
                table: "Review",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                schema: "Identity",
                table: "Review",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Dislikes",
                schema: "Identity",
                table: "Review",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Game_GameId",
                schema: "Identity",
                table: "Review",
                column: "GameId",
                principalSchema: "Identity",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
