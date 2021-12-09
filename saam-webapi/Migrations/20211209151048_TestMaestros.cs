using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace saam_webapi.Migrations
{
    public partial class TestMaestros : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CostoTextraordinario",
                table: "Especialidades",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CostoTordinario",
                table: "Especialidades",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FaenaId",
                table: "Especialidades",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "Especialidades",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Faenas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faenas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trabajadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rut = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Papellido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sapellido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Planta = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Pfono = table.Column<int>(type: "int", nullable: false),
                    Sfono = table.Column<int>(type: "int", nullable: false),
                    EspecialidadId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trabajadores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trabajadores_Especialidades_EspecialidadId",
                        column: x => x.EspecialidadId,
                        principalTable: "Especialidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inasistencias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Inicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Termino = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Dias = table.Column<int>(type: "int", nullable: false),
                    TrbajadorId = table.Column<int>(type: "int", nullable: false),
                    TrabajadorId = table.Column<int>(type: "int", nullable: true),
                    EspecialidadId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inasistencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inasistencias_Especialidades_EspecialidadId",
                        column: x => x.EspecialidadId,
                        principalTable: "Especialidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inasistencias_Trabajadores_TrabajadorId",
                        column: x => x.TrabajadorId,
                        principalTable: "Trabajadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Especialidades_FaenaId",
                table: "Especialidades",
                column: "FaenaId");

            migrationBuilder.CreateIndex(
                name: "IX_Inasistencias_EspecialidadId",
                table: "Inasistencias",
                column: "EspecialidadId");

            migrationBuilder.CreateIndex(
                name: "IX_Inasistencias_TrabajadorId",
                table: "Inasistencias",
                column: "TrabajadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Trabajadores_EspecialidadId",
                table: "Trabajadores",
                column: "EspecialidadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Especialidades_Faenas_FaenaId",
                table: "Especialidades",
                column: "FaenaId",
                principalTable: "Faenas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Especialidades_Faenas_FaenaId",
                table: "Especialidades");

            migrationBuilder.DropTable(
                name: "Faenas");

            migrationBuilder.DropTable(
                name: "Inasistencias");

            migrationBuilder.DropTable(
                name: "Trabajadores");

            migrationBuilder.DropIndex(
                name: "IX_Especialidades_FaenaId",
                table: "Especialidades");

            migrationBuilder.DropColumn(
                name: "CostoTextraordinario",
                table: "Especialidades");

            migrationBuilder.DropColumn(
                name: "CostoTordinario",
                table: "Especialidades");

            migrationBuilder.DropColumn(
                name: "FaenaId",
                table: "Especialidades");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Especialidades");
        }
    }
}
