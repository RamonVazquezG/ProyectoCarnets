using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoCarnets.Models;
using ProyectoCarnets.Services;

namespace ProyectoCarnets.Controllers
{
    public class CarnetsController : Controller
    {
        private readonly ApplicationDbContext context;

        public CarnetsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var carnets = context.Carnets.OrderByDescending(crnt => crnt.Id).ToList();

            return View(carnets);
        }

        public IActionResult Create()
        {
            // 1. Consultar todas las actividades de la base de datos
            var listaActividades = context.ActividadesComplementarias.ToList();

            // 2. Meterlas en el ViewBag. 
            // Parametros de SelectList: (La lista de datos, "CampoQueEsElValorInvisible", "CampoQueSeMuestraEnPantalla")
            // En tu caso: Valor = Clave (ID), Texto = NombreActividad
            ViewBag.Actividades = new SelectList(listaActividades, "Clave", "NombreActividad");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Carnet carnet)
        {
            // --- AGREGA ESTAS LÍNEAS AL INICIO ---
            // Le decimos al validador: "Ignora que el objeto Alumno y Programa vienen vacíos, 
            // yo sé que solo necesito sus IDs"
            ModelState.Remove("Alumno");
            ModelState.Remove("ProgramaEducativo");
            ModelState.Remove("ActividadComplementaria");
            // -------------------------------------

            if (ModelState.IsValid)
            {
                context.Add(carnet);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // DEBUG: Si llega aquí, quiero saber POR QUÉ falló.
            // Esto imprimirá los errores en la ventana de "Salida" (Output) de Visual Studio.
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    System.Diagnostics.Debug.WriteLine($"ERROR DE VALIDACIÓN: {error.ErrorMessage}");
                }
            }

            ViewBag.Actividades = new SelectList(context.ActividadesComplementarias, "Clave", "NombreActividad", carnet.ActividadComplementariaClave);
            return View(carnet);
        }

        [HttpGet]
        public IActionResult GetAlumnoInfo(int matricula)
        {
            // 1. Buscar al alumno
            var alumno = context.Alumnos.FirstOrDefault(a => a.Matricula == matricula);
            if (alumno == null) return NotFound();

            // 2. Buscar SOLO los programas asignados a ese alumno (Tu requisito de filtro)
            var programasDelAlumno = context.AlumnosProgramas
                .Where(ap => ap.AlumnoMatricula == matricula)
                .Select(ap => new {
                    id = ap.ProgramaEducativo.Id,
                    nombre = ap.ProgramaEducativo.NombrePrograma
                })
                .ToList();

            return Json(new { nombre = alumno.Nombre, programas = programasDelAlumno });
        }
    }
}
