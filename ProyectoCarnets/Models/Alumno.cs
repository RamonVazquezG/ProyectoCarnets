using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoCarnets.Models
{
    public enum Etapa
    {
        BASICA,
        DISCIPLINARIA,
        TERMINAL
    }
    public class Alumno
    {
        [Key] // Importante: Indica que es la llave primaria
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // Porque tú escribes la matrícula, no es autoincremental
        public int Matricula { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public Etapa Etapa { get; set; }  // “DISCIPLINARIA”, etc.

        // Relación: Un alumno tiene muchas asignaciones de carrera
        public List<AlumnoPrograma> AlumnoProgramas { get; set; } = new List<AlumnoPrograma>();

    }
}
