using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ImpressioApi_.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate_Usuario_ObraArte : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "t_usuario",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    email_usuario = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    senha = table.Column<string>(type: "text", nullable: false),
                    data_nascimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    apelido = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    nome_usuario = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    biografia_usuario = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    imagem_usuario = table.Column<string>(type: "text", nullable: true),
                    publico = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_usuario", x => x.id_usuario);
                });

            migrationBuilder.CreateTable(
                name: "t_obra_arte",
                columns: table => new
                {
                    id_obra_arte = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    imagem_obra_arte = table.Column<string>(type: "text", nullable: false),
                    descricao_obra_arte = table.Column<string>(type: "character varying(170)", maxLength: 170, nullable: true),
                    publico = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    upvote = table.Column<int>(type: "integer", nullable: true, defaultValue: 0),
                    downvote = table.Column<int>(type: "integer", nullable: true, defaultValue: 0),
                    id_usuario = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_obra_arte", x => x.id_obra_arte);
                    table.ForeignKey(
                        name: "fk_usuario_obras_arte",
                        column: x => x.id_usuario,
                        principalTable: "t_usuario",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_obra_arte_id_usuario",
                table: "t_obra_arte",
                column: "id_usuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_obra_arte");

            migrationBuilder.DropTable(
                name: "t_usuario");
        }
    }
}
