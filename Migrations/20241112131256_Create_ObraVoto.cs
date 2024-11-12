using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ImpressioApi_.Migrations
{
    /// <inheritdoc />
    public partial class Create_ObraVoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "downvote",
                table: "t_obra_arte");

            migrationBuilder.DropColumn(
                name: "upvote",
                table: "t_obra_arte");

            migrationBuilder.CreateTable(
                name: "t_obra_voto",
                columns: table => new
                {
                    id_obra_voto = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_obra_arte = table.Column<int>(type: "integer", nullable: false),
                    id_usuario = table.Column<int>(type: "integer", nullable: false),
                    voto = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_obra_voto", x => x.id_obra_voto);
                    table.ForeignKey(
                        name: "fk_obra_voto_obra_arte",
                        column: x => x.id_obra_arte,
                        principalTable: "t_obra_arte",
                        principalColumn: "id_obra_arte",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_obra_voto_usuario",
                        column: x => x.id_usuario,
                        principalTable: "t_usuario",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_obra_voto_id_obra_arte",
                table: "t_obra_voto",
                column: "id_obra_arte");

            migrationBuilder.CreateIndex(
                name: "IX_t_obra_voto_id_usuario",
                table: "t_obra_voto",
                column: "id_usuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_obra_voto");

            migrationBuilder.AddColumn<int>(
                name: "downvote",
                table: "t_obra_arte",
                type: "integer",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "upvote",
                table: "t_obra_arte",
                type: "integer",
                nullable: true,
                defaultValue: 0);
        }
    }
}
