using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoCarnets.Migrations
{
    /// <inheritdoc />
    public partial class AnadirRestriccionesAProgramaEducativo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Clave",
                table: "ProgramasEducativos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Clave",
                table: "ProgramasEducativos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
