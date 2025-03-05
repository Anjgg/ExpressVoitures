using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpressVoitures.Migrations
{
    /// <inheritdoc />
    public partial class InitReparation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voitures_Reparations_ReparationId",
                table: "Voitures");

            migrationBuilder.AlterColumn<int>(
                name: "ReparationId",
                table: "Voitures",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Voitures_Reparations_ReparationId",
                table: "Voitures",
                column: "ReparationId",
                principalTable: "Reparations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voitures_Reparations_ReparationId",
                table: "Voitures");

            migrationBuilder.AlterColumn<int>(
                name: "ReparationId",
                table: "Voitures",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Voitures_Reparations_ReparationId",
                table: "Voitures",
                column: "ReparationId",
                principalTable: "Reparations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
