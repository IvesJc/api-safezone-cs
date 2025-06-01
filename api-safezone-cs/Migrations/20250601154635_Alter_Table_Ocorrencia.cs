using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_safezone_cs.Migrations
{
    /// <inheritdoc />
    public partial class Alter_Table_Ocorrencia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Localizacaos");

            migrationBuilder.DropColumn(
                name: "Local",
                table: "Ocorrencias");

            migrationBuilder.AddColumn<string>(
                name: "Localizacao_Latitude",
                table: "Vitimas",
                type: "NVARCHAR2(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Localizacao_Longitude",
                table: "Vitimas",
                type: "NVARCHAR2(12)",
                maxLength: 12,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Localizacao_Latitude",
                table: "Ocorrencias",
                type: "NVARCHAR2(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Localizacao_Longitude",
                table: "Ocorrencias",
                type: "NVARCHAR2(12)",
                maxLength: 12,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Localizacao_Latitude",
                table: "Vitimas");

            migrationBuilder.DropColumn(
                name: "Localizacao_Longitude",
                table: "Vitimas");

            migrationBuilder.DropColumn(
                name: "Localizacao_Latitude",
                table: "Ocorrencias");

            migrationBuilder.DropColumn(
                name: "Localizacao_Longitude",
                table: "Ocorrencias");

            migrationBuilder.AddColumn<string>(
                name: "Local",
                table: "Ocorrencias",
                type: "NVARCHAR2(2000)",
                nullable: false,
                defaultValue: "");

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
        }
    }
}
