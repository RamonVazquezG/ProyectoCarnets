using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoCarnets.Models
{
    public class Carnet
    {
        public int Id { get; set; }

        // FK 1: Alumno
        public int AlumnoMatricula { get; set; }
        [ForeignKey("AlumnoMatricula")]
        public Alumno Alumno { get; set; } // Propiedad de navegación

        // FK 2: Programa (Se guarda el ID, se muestra el nombre)
        public int ProgramaEducativoId { get; set; }
        [ForeignKey("ProgramaEducativoId")]
        public ProgramaEducativo ProgramaEducativo { get; set; }

        // FK 3: Actividad
        public int ActividadComplementariaClave { get; set; }
        [ForeignKey("ActividadComplementariaClave")]
        public ActividadComplementaria ActividadComplementaria { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}
