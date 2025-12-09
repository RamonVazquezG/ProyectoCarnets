using Microsoft.AspNetCore.Mvc;
using ProyectoCarnets.Models;
using ProyectoCarnets.Services;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoCarnets.Controllers
{
    public class ActividadComplementariaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActividadComplementariaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ActividadComplementaria
        public async Task<IActionResult> Index()
        {
            var actividades = _context.ActividadesComplementarias.ToList();
            return View(actividades);
        }

        // GET: ActividadComplementaria/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var actividad = await _context.ActividadesComplementarias.FindAsync(id);
            if (actividad == null) return NotFound();

            return View(actividad);
        }

        // GET: ActividadComplementaria/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ActividadComplementaria/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ActividadComplementaria actividad)
        {
            if (!ModelState.IsValid) return View(actividad);

            _context.Add(actividad);
            await _context.SaveChangesAsync();
            TempData["TablaParaCargar"] = "GetActividadesComplementarias";

            return RedirectToAction("Index", "Carnets");
        }

        // GET: ActividadComplementaria/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var actividad = await _context.ActividadesComplementarias.FindAsync(id);
            if (actividad == null) return NotFound();

            return View(actividad);
        }

        // POST: ActividadComplementaria/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ActividadComplementaria actividad)
        {
            if (id != actividad.Clave) return BadRequest();
            if (!ModelState.IsValid) return View(actividad);

            try
            {
                _context.Update(actividad);
                await _context.SaveChangesAsync();
            }
            catch
            {
                if (!_context.ActividadesComplementarias.Any(a => a.Clave == id))
                    return NotFound();
                throw;
            }

            TempData["TablaParaCargar"] = "GetActividadesComplementarias";

            return RedirectToAction("Index", "Carnets");
        }

        // GET: ActividadComplementaria/Delete/5 
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var actividad = await _context.ActividadesComplementarias.FindAsync(id);
            if (actividad == null) return NotFound();

            return View(actividad);
        }

        // POST: ActividadComplementaria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actividad = await _context.ActividadesComplementarias.FindAsync(id);
            if (actividad == null) return NotFound();

            _context.ActividadesComplementarias.Remove(actividad);
            await _context.SaveChangesAsync();
            TempData["TablaParaCargar"] = "GetActividadesComplementarias";

            return RedirectToAction("Index", "Carnets");
        }
    }
}
