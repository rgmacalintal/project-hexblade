using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forgeborn.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelsAndControllers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Rulesets_RulesetId",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_Lobbys_Players_PlayerId",
                table: "Lobbys");

            migrationBuilder.DropIndex(
                name: "IX_Lobbys_PlayerId",
                table: "Lobbys");

            migrationBuilder.DropIndex(
                name: "IX_Characters_RulesetId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Lobbys");

            migrationBuilder.DropColumn(
                name: "RulesetId",
                table: "Characters");

            migrationBuilder.AddColumn<int>(
                name: "Attack",
                table: "Weapons",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attack",
                table: "Weapons");

            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "Lobbys",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RulesetId",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Lobbys_PlayerId",
                table: "Lobbys",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_RulesetId",
                table: "Characters",
                column: "RulesetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Rulesets_RulesetId",
                table: "Characters",
                column: "RulesetId",
                principalTable: "Rulesets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lobbys_Players_PlayerId",
                table: "Lobbys",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
