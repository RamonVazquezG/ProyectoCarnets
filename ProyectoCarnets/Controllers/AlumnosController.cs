using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoCarnets.Models;
using ProyectoCarnets.Services;
using System.Text;

namespace ProyectoCarnets.Controllers
{
    public class AlumnosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlumnosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Alumno
        public async Task<IActionResult> Index()
        {
            var alumnos = _context.Alumnos.ToList();
            return View(alumnos);
        }

        // GET: Alumno/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var alumno = await _context.Alumnos.FindAsync(id);
            if (alumno == null) return NotFound();

            return View(alumno);
        }

        // GET: Alumno/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Alumno/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Alumno alumno)
        {
            if (!ModelState.IsValid) return View(alumno);

            _context.Add(alumno);
            await _context.SaveChangesAsync();
            TempData["TablaParaCargar"] = "GetTablaAlumnos";

            return RedirectToAction("Index", "Carnets");
        }

        // GET: Alumno/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            Console.WriteLine("GET Edit llamado. id = " + id);

            if (id == null)
            {
                Console.WriteLine("id es null. Retornando NotFound");
                return NotFound();
            }

            var alumno = await _context.Alumnos.FindAsync(id);

            if (alumno == null)
            {
                Console.WriteLine("Alumno no encontrado para id = " + id);
                return NotFound();
            }

            Console.WriteLine($"Alumno encontrado: {alumno.Matricula} - {alumno.Nombre}");
            return View(alumno);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Alumno alumno)
        {
            Console.WriteLine("POST Edit llamado.");
            Console.WriteLine($"Datos recibidos: Matricula={alumno.Matricula}, Nombre={alumno.Nombre}, ApellidoP={alumno.ApellidoPaterno}, ApellidoM={alumno.ApellidoMaterno}, Etapa={alumno.Etapa}");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState inválido.");
                foreach (var state in ModelState)
                {
                    if (state.Value.Errors.Count > 0)
                    {
                        Console.WriteLine($"{state.Key} tiene errores:");
                        foreach (var err in state.Value.Errors)
                            Console.WriteLine("  " + err.ErrorMessage);
                    }
                }

                return View(alumno);
            }

            try
            {
                Console.WriteLine("Intentando actualizar alumno...");
                _context.Update(alumno);
                await _context.SaveChangesAsync();
                Console.WriteLine("Alumno actualizado correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar: " + ex.Message);
                if (!_context.Alumnos.Any(a => a.Matricula == alumno.Matricula))
                {
                    Console.WriteLine("Alumno no existe en DB. Retornando NotFound");
                    return NotFound();
                }
                throw;
            }

            TempData["TablaParaCargar"] = "GetTablaAlumnos";
            Console.WriteLine("Redirigiendo a Carnets/Index");
            return RedirectToAction("Index", "Carnets");
        }


        // ============================
        //  POST: /Alumnos/Upload
        // ============================
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile archivo)
        {
            if (archivo == null || archivo.Length == 0)
            {
                ViewBag.Errores = new List<string> { "No seleccionaste ningún archivo." };
                return View();
            }

            var errores = new List<string>();
            int guardados = 0;
            int actualizados = 0;

            using var reader = new StreamReader(archivo.OpenReadStream(), Encoding.UTF8);

            while (!reader.EndOfStream)
            {
                string? linea = await reader.ReadLineAsync();
                if (string.IsNullOrWhiteSpace(linea))
                    continue;

                var partes = linea.Split(',');
                if (partes.Length != 5)
                {
                    errores.Add($"Línea inválida (esperaba 5 columnas): {linea}");
                    continue;
                }

                string sMatricula = partes[0].Trim();
                string apellidoP = partes[1].Trim();
                string apellidoM = partes[2].Trim();
                string nombres = partes[3].Trim();
                string etapaStr = partes[4].Trim().ToUpper();

                // --- Validar matrícula ---
                if (!int.TryParse(sMatricula, out int matricula))
                {
                    errores.Add($"Matrícula inválida: {sMatricula}");
                    continue;
                }

                // --- Validar etapa ---
                if (!Enum.TryParse<Etapa>(etapaStr, true, out Etapa etapa))
                {
                    errores.Add($"Etapa inválida para matrícula {matricula}: {etapaStr}");
                    continue;
                }

                // --- Buscar si ya existe ---
                var alumnoExistente = await _context.Alumnos
                    .FirstOrDefaultAsync(a => a.Matricula == matricula);

                if (alumnoExistente == null)
                {
                    // Crear nuevo alumno
                    var nuevoAlumno = new Alumno
                    {
                        Matricula = matricula,
                        ApellidoPaterno = string.IsNullOrWhiteSpace(apellidoP) ? null : apellidoP,
                        ApellidoMaterno = string.IsNullOrWhiteSpace(apellidoM) ? null : apellidoM,
                        Nombre = nombres,
                        Etapa = etapa
                    };

                    _context.Alumnos.Add(nuevoAlumno);
                    guardados++;
                }
                else
                {
                    // Actualizar datos
                    alumnoExistente.ApellidoPaterno = string.IsNullOrWhiteSpace(apellidoP) ? alumnoExistente.ApellidoPaterno : apellidoP;
                    alumnoExistente.ApellidoMaterno = string.IsNullOrWhiteSpace(apellidoM) ? alumnoExistente.ApellidoMaterno : apellidoM;
                    alumnoExistente.Nombre = string.IsNullOrWhiteSpace(nombres) ? alumnoExistente.Nombre: nombres;
                    alumnoExistente.Etapa = etapa;

                    actualizados++;
                }
            }

            await _context.SaveChangesAsync();

            ViewBag.Guardados = guardados;
            ViewBag.Actualizados = actualizados;
            ViewBag.Errores = errores;

            return View();
        }
    }
}
