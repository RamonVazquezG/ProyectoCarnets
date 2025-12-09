using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoCarnets.Models
{
    public class ProgramaEducativo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "La clave no puede ser negativa")]
        public int? Clave { get; set; }
        [Required]
        public string NombrePrograma { get; set; }
    }
}
