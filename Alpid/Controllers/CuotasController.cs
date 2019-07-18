using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Alpid.Data;
using Alpid.Models;

namespace Alpid.Controllers
{
    public class CuotasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CuotasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cuotas
        public async Task<IActionResult> Index(string currentFilter, string searchString, int? page, string filtroFecha, 
                                                string DateFilter)
        {

            var ultimoid = _context.CuotaPrecio.Max(c => c.CuotaPrecioID);
            var valor = _context.CuotaPrecio.SingleOrDefault(c => c.CuotaPrecioID == ultimoid);
            ViewData["precio"] = valor.Importe;

            //return View(await applicationDbContext.ToListAsync());

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;
            var cuota = from s in _context.Cuotas select s;

            if (!string.IsNullOrEmpty(searchString))
            {
                cuota = cuota.Where(s => s.Socios.RazonSocial.Contains(searchString));
            }
            int pageSize = 15;
            return View(await Paginacion<Cuotas>.CreateAsync(cuota.AsNoTracking(), page ?? 1, pageSize) );
        }

        // GET: Cuotas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuotas = await _context.Cuotas
                .Include(c => c.Socios)
                .FirstOrDefaultAsync(m => m.CuotasID == id);
            if (cuotas == null)
            {
                return NotFound();
            }

            return View(cuotas);
        }

        // GET: Cuotas/Create
        public IActionResult Create()
        {
            ViewData["SociosID"] = new SelectList(_context.Set<Socios>(), "SociosID", "RazonSocial");
            return View();
        }

        // POST: Cuotas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CuotasID,Estado,Observacion,FechaPago,FechaEmicion,SociosID")] Cuotas cuotas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cuotas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SociosID"] = new SelectList(_context.Set<Socios>(), "SociosID", "RazonSocial", cuotas.SociosID);
            return View(cuotas);
        }

        // GET: Cuotas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuotas = await _context.Cuotas.FindAsync(id);
            if (cuotas == null)
            {
                return NotFound();
            }
            ViewData["SociosID"] = new SelectList(_context.Set<Socios>(), "SociosID", "RazonSocial", cuotas.SociosID);
            return View(cuotas);
        }

        // POST: Cuotas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CuotasID,Estado,Observacion,FechaPago,FechaEmicion,SociosID")] Cuotas cuotas)
        {
            if (id != cuotas.CuotasID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cuotas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CuotasExists(cuotas.CuotasID))
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
            ViewData["SociosID"] = new SelectList(_context.Set<Socios>(), "SociosID", "RazonSocial", cuotas.SociosID);
            return View(cuotas);
        }

        // GET: Cuotas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuotas = await _context.Cuotas
                .Include(c => c.Socios)
                .FirstOrDefaultAsync(m => m.CuotasID == id);
            if (cuotas == null)
            {
                return NotFound();
            }

            return View(cuotas);
        }

        // POST: Cuotas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cuotas = await _context.Cuotas.FindAsync(id);
            _context.Cuotas.Remove(cuotas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CuotasExists(int id)
        {
            return _context.Cuotas.Any(e => e.CuotasID == id);
        }
    }
}
