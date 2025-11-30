using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forgeborn.Server.Migrations
{
    /// <inheritdoc />
    public partial class MakeCharacterNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Characters_CharacterId",
                table: "Players");

            migrationBuilder.AlterColumn<int>(
                name: "CharacterId",
                table: "Players",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CurrentHP",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxHP",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Characters_CharacterId",
                table: "Players",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Characters_CharacterId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "CurrentHP",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "MaxHP",
                table: "Characters");

            migrationBuilder.AlterColumn<int>(
                name: "CharacterId",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Characters_CharacterId",
                table: "Players",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
