using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ImpressioApi_.Migrations
{
    /// <inheritdoc />
    public partial class Create_ObrasFavoritadas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "t_obra_favoritada",
                columns: table => new
                {
                    id_obra_favoritada = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_obra_arte = table.Column<int>(type: "integer", nullable: false),
                    id_usuario = table.Column<int>(type: "integer", nullable: false),
                    ObraArteIdObraArte = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_obra_favoritada", x => x.id_obra_favoritada);
                    table.ForeignKey(
                        name: "FK_t_obra_favoritada_t_obra_arte_ObraArteIdObraArte",
                        column: x => x.ObraArteIdObraArte,
                        principalTable: "t_obra_arte",
                        principalColumn: "id_obra_arte");
                    table.ForeignKey(
                        name: "fk_usuario_obras_favoritadas",
                        column: x => x.id_usuario,
                        principalTable: "t_usuario",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_obra_favoritada_id_usuario",
                table: "t_obra_favoritada",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_t_obra_favoritada_ObraArteIdObraArte",
                table: "t_obra_favoritada",
                column: "ObraArteIdObraArte");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_obra_favoritada");
        }
    }
}
