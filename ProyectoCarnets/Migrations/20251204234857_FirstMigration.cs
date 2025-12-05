using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoCarnets.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActividadesComplementarias",
                columns: table => new
                {
                    Clave = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreActividad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActividadesComplementarias", x => x.Clave);
                });

            migrationBuilder.CreateTable(
                name: "Alumnos",
                columns: table => new
                {
                    Matricula = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alumnos", x => x.Matricula);
                });

            migrationBuilder.CreateTable(
                name: "ProgramasEducativos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombrePrograma = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramasEducativos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AlumnosProgramas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlumnoMatricula = table.Column<int>(type: "int", nullable: false),
                    ProgramaEducativoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlumnosProgramas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlumnosProgramas_Alumnos_AlumnoMatricula",
                        column: x => x.AlumnoMatricula,
                        principalTable: "Alumnos",
                        principalColumn: "Matricula",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlumnosProgramas_ProgramasEducativos_ProgramaEducativoId",
                        column: x => x.ProgramaEducativoId,
                        principalTable: "ProgramasEducativos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carnets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlumnoMatricula = table.Column<int>(type: "int", nullable: false),
                    ProgramaEducativoId = table.Column<int>(type: "int", nullable: false),
                    ActividadComplementariaClave = table.Column<int>(type: "int", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carnets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carnets_ActividadesComplementarias_ActividadComplementariaClave",
                        column: x => x.ActividadComplementariaClave,
                        principalTable: "ActividadesComplementarias",
                        principalColumn: "Clave",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Carnets_Alumnos_AlumnoMatricula",
                        column: x => x.AlumnoMatricula,
                        principalTable: "Alumnos",
                        principalColumn: "Matricula",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Carnets_ProgramasEducativos_ProgramaEducativoId",
                        column: x => x.ProgramaEducativoId,
                        principalTable: "ProgramasEducativos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlumnosProgramas_AlumnoMatricula",
                table: "AlumnosProgramas",
                column: "AlumnoMatricula");

            migrationBuilder.CreateIndex(
                name: "IX_AlumnosProgramas_ProgramaEducativoId",
                table: "AlumnosProgramas",
                column: "ProgramaEducativoId");

            migrationBuilder.CreateIndex(
                name: "IX_Carnets_ActividadComplementariaClave",
                table: "Carnets",
                column: "ActividadComplementariaClave");

            migrationBuilder.CreateIndex(
                name: "IX_Carnets_AlumnoMatricula",
                table: "Carnets",
                column: "AlumnoMatricula");

            migrationBuilder.CreateIndex(
                name: "IX_Carnets_ProgramaEducativoId",
                table: "Carnets",
                column: "ProgramaEducativoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlumnosProgramas");

            migrationBuilder.DropTable(
                name: "Carnets");

            migrationBuilder.DropTable(
                name: "ActividadesComplementarias");

            migrationBuilder.DropTable(
                name: "Alumnos");

            migrationBuilder.DropTable(
                name: "ProgramasEducativos");
        }
    }
}
