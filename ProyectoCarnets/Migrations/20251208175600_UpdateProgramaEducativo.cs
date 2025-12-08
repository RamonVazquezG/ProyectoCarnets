using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoCarnets.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProgramaEducativo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Clave",
                table: "ProgramasEducativos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Clave",
                table: "ProgramasEducativos");
        }
    }
}
