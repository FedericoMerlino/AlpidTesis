using System;
using System.Linq;
using System.Threading.Tasks;
using Alpid.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Alpid.Models;

namespace Alpid.Controllers
{
    public class CajaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CajaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Caja
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Caja.Include(c => c.Alquiler);
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

        // GET: Caja/Create
        public IActionResult Create()
        {
            ViewData["AlquilerID"] = new SelectList(_context.Alquiler, "AlquilerID", "AlquilerID");
            return View();
        }

        // POST: Caja/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CajaId,Debe,Haber,TipoMovimiento,Observaciones,Estado,FechaMovimiento,Total,CuotaID,AlquilerID")] Caja caja)
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
                    // se usa solo para el ingreso de direno
                    if (caja.Debe != null)
                    {
                        caja.Total = UltimoValor.Total + caja.Debe;
                    }
                    //se usa solo para la salida de dinero
                    if (caja.Haber != null)
                    {
                        caja.Total = UltimoValor.Total - caja.Haber;
                    }
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

        // GET: Caja/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caja = await _context.Caja.FindAsync(id);
            if (caja == null)
            {
                return NotFound();
            }
            ViewData["AlquilerID"] = new SelectList(_context.Alquiler, "AlquilerID", "AlquilerID", caja.AlquilerID);
            return View(caja);
        }

        // POST: Caja/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CajaId,Debe,Haber,TipoMovimiento,Observaciones,Estado,FechaMovimiento,Total,CuotaID,AlquilerID")] Caja caja)
        {
            if (id != caja.CajaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(caja);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CajaExists(caja.CajaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlquilerID"] = new SelectList(_context.Alquiler, "AlquilerID", "AlquilerID", caja.AlquilerID);
            return View(caja);
        }

        // GET: Caja/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Caja/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var caja = await _context.Caja.FindAsync(id);
            _context.Caja.Remove(caja);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CajaExists(int id)
        {
            return _context.Caja.Any(e => e.CajaId == id);
        }
    }
}
