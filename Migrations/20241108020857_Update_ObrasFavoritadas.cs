using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImpressioApi_.Migrations
{
    /// <inheritdoc />
    public partial class Update_ObrasFavoritadas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_obra_favoritada_t_obra_arte_ObraArteIdObraArte",
                table: "t_obra_favoritada");

            migrationBuilder.DropForeignKey(
                name: "fk_usuario_obras_favoritadas",
                table: "t_obra_favoritada");

            migrationBuilder.DropIndex(
                name: "IX_t_obra_favoritada_ObraArteIdObraArte",
                table: "t_obra_favoritada");

            migrationBuilder.DropColumn(
                name: "ObraArteIdObraArte",
                table: "t_obra_favoritada");

            migrationBuilder.CreateIndex(
                name: "IX_t_obra_favoritada_id_obra_arte",
                table: "t_obra_favoritada",
                column: "id_obra_arte");

            migrationBuilder.AddForeignKey(
                name: "fk_obra_favoritada_obra_arte",
                table: "t_obra_favoritada",
                column: "id_obra_arte",
                principalTable: "t_obra_arte",
                principalColumn: "id_obra_arte",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_obra_favoritada_usuario",
                table: "t_obra_favoritada",
                column: "id_usuario",
                principalTable: "t_usuario",
                principalColumn: "id_usuario",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_obra_favoritada_obra_arte",
                table: "t_obra_favoritada");

            migrationBuilder.DropForeignKey(
                name: "fk_obra_favoritada_usuario",
                table: "t_obra_favoritada");

            migrationBuilder.DropIndex(
                name: "IX_t_obra_favoritada_id_obra_arte",
                table: "t_obra_favoritada");

            migrationBuilder.AddColumn<int>(
                name: "ObraArteIdObraArte",
                table: "t_obra_favoritada",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_obra_favoritada_ObraArteIdObraArte",
                table: "t_obra_favoritada",
                column: "ObraArteIdObraArte");

            migrationBuilder.AddForeignKey(
                name: "FK_t_obra_favoritada_t_obra_arte_ObraArteIdObraArte",
                table: "t_obra_favoritada",
                column: "ObraArteIdObraArte",
                principalTable: "t_obra_arte",
                principalColumn: "id_obra_arte");

            migrationBuilder.AddForeignKey(
                name: "fk_usuario_obras_favoritadas",
                table: "t_obra_favoritada",
                column: "id_usuario",
                principalTable: "t_usuario",
                principalColumn: "id_usuario",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
