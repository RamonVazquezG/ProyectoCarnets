using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoCarnets.Models
{
    public class Alumno
    {
        [Key] // Importante: Indica que es la llave primaria
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // Porque tú escribes la matrícula, no es autoincremental
        public int Matricula { get; set; }
        public string Nombre { get; set; }

        // Relación: Un alumno tiene muchas asignaciones de carrera
        public List<AlumnoPrograma> AlumnoProgramas { get; set; }
    }
}
