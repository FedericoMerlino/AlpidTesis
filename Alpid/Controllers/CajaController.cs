using System;
using System.Linq;
using System.Threading.Tasks;
using Alpid.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Alpid.Models;
using Microsoft.AspNetCore.Authorization;

namespace Alpid.Controllers
{
    [Authorize]
    public class CajaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CajaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Caja
        public async Task<IActionResult> Index(DateTime fechaDesde, DateTime fechaHasta,string FechaDesdeFilter, string FechaHastaFilter)
        {
            ViewData["FechaDesdeFilter"] = fechaDesde.ToString("dd/MM/yyyy");
            ViewData["FechaHastaFilter"] = fechaHasta.ToString("dd/MM/yyyy");

            var caja = from s in _context.Caja where ((s.FechaMovimiento >= fechaDesde) && (s.FechaMovimiento <= fechaHasta)) select s;

            //caja = caja.Where(s => s.FechaMovimiento >= fechaDesde);
            //caja = caja.Where(s => s.FechaMovimiento <= fechaHasta);

            var applicationDbContext = caja;
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Caja/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caja = await _context.Caja
                .Include(c => c.Alquiler)
                .FirstOrDefaultAsync(m => m.CajaId == id);
            if (caja == null)
            {
                return NotFound();
            }

            return View(caja);
        }

        //Retiro de dinero
        public IActionResult CreateRetire()
        {
            ViewData["AlquilerID"] = new SelectList(_context.Alquiler, "AlquilerID", "AlquilerID");
            return View();
        }

        //Retiro de dinero
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRetire([Bind("CajaId,Debe,Haber,TipoMovimiento,Observaciones,Estado,FechaMovimiento,Total,CuotaID,AlquilerID")] Caja caja)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //busca el ultimo id 
                    var db = _context.Caja.Include(c => c.Alquiler)
                                      .Max(c => c.CajaId);
                    //busca el valor del total del ultimo id obtenido arriba
                    var UltimoValor = _context.Caja.SingleOrDefault(c => c.CajaId == db);
                    //Restar valor
                    caja.Total = UltimoValor.Total - caja.Haber;
                    //Obtener fecha y hora actual
                    caja.FechaMovimiento = DateTime.Now;
                    _context.Add(caja);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["AlquilerID"] = new SelectList(_context.Alquiler, "AlquilerID", "AlquilerID", caja.AlquilerID);
                return View(caja);
            }
            catch (Exception e)
            {
                Console.Write(e);

                var applicationDbContext = _context.Caja.Include(c => c.Alquiler);
                return View(await applicationDbContext.ToListAsync());
            }
        }

        //ingreso de dinero
        public IActionResult CreateIngreso()
        {
            ViewData["AlquilerID"] = new SelectList(_context.Alquiler, "AlquilerID", "AlquilerID");
            return View();
        }

        //ingreso de dinero
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateIngreso([Bind("CajaId,Debe,Haber,TipoMovimiento,Observaciones,Estado,FechaMovimiento,Total,CuotaID,AlquilerID")] Caja caja)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_context.Caja.Select(x => x.CajaId) == null)
                    {
                        //busca el ultimo id 
                        var db = _context.Caja.Include(c => c.Alquiler)
                                      .Max(c => c.CajaId);
                        //busca el valor del total del ultimo id obtenido arriba
                        var UltimoValor = _context.Caja.SingleOrDefault(c => c.CajaId == db);
                        //sumar valor
                        caja.Total = UltimoValor.Total + caja.Debe;
                    }
                    //para cuando la base de datos esta vacia
                    else
                    {
                        var UltimoValor = 0;
                        caja.Total = UltimoValor + caja.Debe;
                    }
                    //Obtener fecha y hora actual
                    caja.FechaMovimiento = DateTime.Now;
                    _context.Add(caja);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["AlquilerID"] = new SelectList(_context.Alquiler, "AlquilerID", "AlquilerID", caja.AlquilerID);
                return View(caja);
            }
            catch (Exception e)
            {
                Console.Write(e);

                var applicationDbContext = _context.Caja.Include(c => c.Alquiler);
                return View(await applicationDbContext.ToListAsync());
            }
        }

        public async Task<IActionResult> PagoCuota(int ID, double debe)
        {
            try
            {
                var caja = new Caja();

                if (ModelState.IsValid)
                {
                    //busca el ultimo id 
                    var db = _context.Caja.Include(c => c.Alquiler)
                                      .Max(c => c.CajaId);
                    //busca el valor del total del ultimo id obtenido arriba
                    var UltimoValor = _context.Caja.SingleOrDefault(c => c.CajaId == db);
                    var imorte = debe;
                    //sumar valor
                    caja.Total = UltimoValor.Total + imorte;
                    caja.Debe = imorte;
                    caja.FechaMovimiento = DateTime.Now;
                    caja.CuotaID = ID;//"va el id de la cuota"
                    caja.TipoMovimiento = "Couta";
                    caja.Observaciones = "Pago cuota del socio";
                    _context.Add(caja);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction("Index", "Cuotas", new { FileUploadMsg = "File   uploaded successfully" });
            }
            catch (Exception e)
            {
                Console.Write(e);

                var applicationDbContext = _context.Caja.Include(c => c.Alquiler);
                return View(await applicationDbContext.ToListAsync());
            }
        }
    }
}
