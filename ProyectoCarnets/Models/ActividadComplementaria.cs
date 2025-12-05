using System.ComponentModel.DataAnnotations;

namespace ProyectoCarnets.Models
{
    public class ActividadComplementaria
    {
        [Key]
        public int Clave { get; set; } // ID de la materia/actividad
        public string NombreActividad { get; set; }
    }
}
