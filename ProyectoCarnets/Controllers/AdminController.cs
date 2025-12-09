using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoCarnets.Services;
// ... tus usings de modelos
//prueba

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
    public IActionResult GetTablaAlumnos(int? matricula)
    {
        var query = _context.Alumnos.AsQueryable();

        if (matricula.HasValue)
        {
            query = query.Where(a => a.Matricula == matricula.Value);
        }

        var alumnos = query.ToList();

        ViewBag.Matricula = matricula;

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
    [HttpGet]
    public IActionResult GetTablaCarnets(string? search)
    {
        var carnets = _context.Carnets
            .Include(c => c.Alumno)
            .Include(c => c.ProgramaEducativo)
            .Include(c => c.ActividadComplementaria)
            .AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            carnets = carnets.Where(c =>
                c.Alumno.Nombre.Contains(search) ||
                c.Alumno.Matricula.ToString().Contains(search)
            );
        }

        var lista = carnets
            .OrderByDescending(c => c.FechaRegistro)
            .ToList();

        ViewBag.Search = search;

        return PartialView("_TablaCarnets", lista);
    }

    // 4. Acción para obtener actividades complementarias
    [HttpGet]
    public IActionResult GetActividadesComplementarias()
    {
        var actividades = _context.ActividadesComplementarias.ToList();
        return PartialView("_TablaActividades", actividades);
    }

    [HttpGet]
    public IActionResult BuscarAlumnos(string matricula)
    {
        var query = _context.Alumnos.AsQueryable();

        if (!string.IsNullOrWhiteSpace(matricula))
        {
            query = query.Where(a => a.Matricula.ToString().Contains(matricula));
        }

        var alumnos = query.ToList();

        return PartialView("_TablaAlumnos", alumnos);
    }

}