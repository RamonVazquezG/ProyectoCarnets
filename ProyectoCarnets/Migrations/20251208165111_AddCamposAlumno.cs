using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoCarnets.Migrations
{
    /// <inheritdoc />
    public partial class AddCamposAlumno : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApellidoMaterno",
                table: "Alumnos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApellidoPaterno",
                table: "Alumnos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Etapa",
                table: "Alumnos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApellidoMaterno",
                table: "Alumnos");

            migrationBuilder.DropColumn(
                name: "ApellidoPaterno",
                table: "Alumnos");

            migrationBuilder.DropColumn(
                name: "Etapa",
                table: "Alumnos");
        }
    }
}
