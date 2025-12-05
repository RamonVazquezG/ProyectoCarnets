namespace ProyectoCarnets.Models
{
    public class AlumnoPrograma
    {
        public int Id { get; set; }

        // FK hacia Alumno
        public int AlumnoMatricula { get; set; }
        public Alumno Alumno { get; set; }

        // FK hacia Programa
        public int ProgramaEducativoId { get; set; }
        public ProgramaEducativo ProgramaEducativo { get; set; }
    }
}
