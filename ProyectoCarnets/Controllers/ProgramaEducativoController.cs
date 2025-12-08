using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoCarnets.Models;
using ProyectoCarnets.Services;

namespace ProyectoCarnets.Controllers
{
    public class ProgramaEducativoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProgramaEducativoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // PANEL PRINCIPAL
        public IActionResult Index()
        {
            return View();
        }

        // ====== TABLAS PARA ADMIN PANEL ======

        public async Task<IActionResult> TablaProgramas()
        {
            var programas = await _context.ProgramasEducativos
                .OrderBy(p => p.Clave)
                .ToListAsync();

            return PartialView("_TablaProgramas", programas);
        }

        // ====== CREATE ======

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProgramaEducativo programa)
        {
            if (!ModelState.IsValid)
            {
                return View(programa);
            }

            // Validar clave duplicada
            if (_context.ProgramasEducativos.Any(p => p.Clave == programa.Clave))
            {
                ModelState.AddModelError("Clave", "Ya existe un programa con esa clave.");
                return View(programa);
            }
            try
            {
                _context.ProgramasEducativos.Add(programa);
                await _context.SaveChangesAsync();
                TempData["TablaParaCargar"] = "GetTablaProgramas";

                return RedirectToAction("Index", "Carnets");
            }
            catch
            {
                ModelState.AddModelError("", "Ocurrió un error al guardar el programa. Revisa los datos.");
                return View(programa);
            }
        }

        // ====== EDIT ======

        public async Task<IActionResult> Edit(int id)
        {
            var programa = await _context.ProgramasEducativos.FindAsync(id);
            if (programa == null) return NotFound();

            return View(programa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProgramaEducativo programa)
        {
            if (id != programa.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(programa);

            _context.Update(programa);
            await _context.SaveChangesAsync();
            TempData["TablaParaCargar"] = "GetTablaProgramas";

            return RedirectToAction("Index", "Carnets");
        }

        // ====== DELETE ======

        public async Task<IActionResult> Delete(int id)
        {
            var programa = await _context.ProgramasEducativos.FindAsync(id);
            if (programa == null) return NotFound();

            return View(programa);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int id)
        {
            var programa = await _context.ProgramasEducativos.FindAsync(id);
            if (programa == null) return NotFound();

            _context.ProgramasEducativos.Remove(programa);
            await _context.SaveChangesAsync();
            TempData["TablaParaCargar"] = "GetTablaProgramas";

            return RedirectToAction("Index", "Carnets");
        }
    }
}
