using Microsoft.EntityFrameworkCore;
using ProyectoCarnets.Models;

namespace ProyectoCarnets.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<ProgramaEducativo> ProgramasEducativos { get; set; }
        public DbSet<ActividadComplementaria> ActividadesComplementarias { get; set; }
        public DbSet<AlumnoPrograma> AlumnosProgramas { get; set; }
        public DbSet<Carnet> Carnets { get; set; }
    }
}
