using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoCarnets.Services;
// ... tus usings de modelos

public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdminController(ApplicationDbContext context)
    {
        _context = context;
    }

    // 1. La Vista Principal (El contenedor vacío)
    public IActionResult Index()
    {
        return View();
    }

    // 2. Acción para obtener la tabla de Alumnos (Partial View)
    [HttpGet]
    public IActionResult GetTablaAlumnos()
    {
        var alumnos = _context.Alumnos.ToList();
        return PartialView("_TablaAlumnos", alumnos);
    }

    // 3. Acción para obtener Programas
    [HttpGet]
    public IActionResult GetTablaProgramas()
    {
        var programas = _context.ProgramasEducativos.ToList();
        return PartialView("_TablaProgramas", programas);
    }

    // 4. Acción para obtener Carnets (Incluyendo datos relacionados con Include)
    [HttpGet]
    public IActionResult GetTablaCarnets()
    {
        var carnets = _context.Carnets
            .Include(c => c.Alumno)
            .Include(c => c.ProgramaEducativo)
            .Include(c => c.ActividadComplementaria)
            .OrderByDescending(c => c.FechaRegistro)
            .ToList();
        return PartialView("_TablaCarnets", carnets);
    }

    // 4. Acción para obtener actividades complementarias
    [HttpGet]
    public IActionResult GetActividadesComplementarias()
    {
        var actividades = _context.ActividadesComplementarias.ToList();
        return PartialView("_TablaActividades", actividades);
    }
}