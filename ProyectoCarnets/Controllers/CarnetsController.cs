using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoCarnets.Models;
using ProyectoCarnets.Services;

namespace ProyectoCarnets.Controllers
{
    public class CarnetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarnetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Carnets
        public IActionResult Index()
        {
            var carnets = _context.Carnets
                .OrderByDescending(c => c.Id)
                .ToList();

            return View(carnets);
        }

        // GET: Carnets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var carnet = await _context.Carnets.FindAsync(id);
            if (carnet == null) return NotFound();

            ViewBag.Actividades = _context.ActividadesComplementarias.ToList();

            ViewBag.Programas = _context.ProgramasEducativos.ToList();

            return View(carnet);
        }

        // POST: Carnets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Carnet carnet)
        {
            if (id != carnet.Id)
                return BadRequest();

            // Quitar validaciones que no te interesan
            ModelState.Remove("Alumno");
            ModelState.Remove("ProgramaEducativo");
            ModelState.Remove("ActividadComplementaria");

            if (!ModelState.IsValid)
            {
                ViewBag.Actividades = _context.ActividadesComplementarias.ToList();

                ViewBag.Programas = _context.ProgramasEducativos.ToList();
                return View(carnet);
            }

            try
            {
                _context.Update(carnet);
                await _context.SaveChangesAsync();
            }
            catch
            {
                if (!_context.Carnets.Any(c => c.Id == id))
                    return NotFound();

                throw;
            }

            TempData["TablaParaCargar"] = "GetTablaCarnets";
            return RedirectToAction("Index", "Carnets");
        }


        // GET: Carnets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var carnet = await _context.Carnets
                .Include(c => c.Alumno)
                .Include(c => c.ProgramaEducativo)
                .Include(c => c.ActividadComplementaria)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (carnet == null) return NotFound();

            return View(carnet);
        }

        // POST: Carnets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carnet = await _context.Carnets.FindAsync(id);
            if (carnet == null) return NotFound();

            _context.Carnets.Remove(carnet);
            await _context.SaveChangesAsync();

            TempData["TablaParaCargar"] = "GetTablaCarnets";
            return RedirectToAction("Index", "Carnets");
        }

        public IActionResult Create()
        {
            // 1. Consultar todas las actividades de la base de datos
            var listaActividades = _context.ActividadesComplementarias.ToList();

            // 2. Meterlas en el ViewBag. 
            // Parametros de SelectList: (La lista de datos, "CampoQueEsElValorInvisible", "CampoQueSeMuestraEnPantalla")
            // En tu caso: Valor = Clave (ID), Texto = NombreActividad
            ViewBag.Actividades = new SelectList(listaActividades, "Clave", "NombreActividad");
            ViewBag.Programas = _context.ProgramasEducativos.ToList();

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
                _context.Add(carnet);
                await _context.SaveChangesAsync();
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

            ViewBag.Actividades = new SelectList(_context.ActividadesComplementarias, "Clave", "NombreActividad", carnet.ActividadComplementariaClave);
            return View(carnet);
        }

        [HttpGet]
        public IActionResult GetAlumnoInfo(int matricula)
        {
            // 1. Buscar al alumno
            var alumno = _context.Alumnos.FirstOrDefault(a => a.Matricula == matricula);
            if (alumno == null) return NotFound();

            // 2. Traer TODOS los programas educativos
            var todosProgramas = _context.ProgramasEducativos
                .Select(pe => new {
                    id = pe.Id,
                    nombre = pe.NombrePrograma
                })
                .ToList();

            return Json(new { nombre = alumno.Nombre, programas = todosProgramas });
        }

    }
}
