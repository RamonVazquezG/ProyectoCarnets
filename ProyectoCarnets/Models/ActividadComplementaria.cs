using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoCarnets.Models
{
    public class ActividadComplementaria
    {
        [Key]
        [Range(0, int.MaxValue, ErrorMessage = "La clave no puede ser negativa")]

        public int Clave { get; set; } // ID de la materia/actividad
        [Required]

        public string NombreActividad { get; set; }
    }
}
