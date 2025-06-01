using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_safezone_cs.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alertas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Tipo = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    AreaAfetada = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Severidade = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Status = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DataHora = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alertas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ocorrencias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Local = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Tipo = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Status = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Prioridade = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DataHora = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ocorrencias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vitimas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Idade = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Condicao = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    OcorrenciaId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vitimas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vitimas_Ocorrencias_OcorrenciaId",
                        column: x => x.OcorrenciaId,
                        principalTable: "Ocorrencias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Localizacaos",
                columns: table => new
                {
                    VitimaId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Latitude = table.Column<string>(type: "NVARCHAR2(11)", maxLength: 11, nullable: false),
                    Longitude = table.Column<string>(type: "NVARCHAR2(12)", maxLength: 12, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localizacaos", x => x.VitimaId);
                    table.ForeignKey(
                        name: "FK_Localizacaos_Vitimas_VitimaId",
                        column: x => x.VitimaId,
                        principalTable: "Vitimas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vitimas_OcorrenciaId",
                table: "Vitimas",
                column: "OcorrenciaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alertas");

            migrationBuilder.DropTable(
                name: "Localizacaos");

            migrationBuilder.DropTable(
                name: "Vitimas");

            migrationBuilder.DropTable(
                name: "Ocorrencias");
        }
    }
}
