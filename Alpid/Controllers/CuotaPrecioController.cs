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
    public class CuotaPrecioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CuotaPrecioController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CuotaPrecio
        public async Task<IActionResult> Index()
        {
            return View(await _context.CuotaPrecio.ToListAsync());
        }

        // GET: CuotaPrecio/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuotaPrecio = await _context.CuotaPrecio
                .FirstOrDefaultAsync(m => m.CuotaPrecioID == id);
            if (cuotaPrecio == null)
            {
                return NotFound();
            }

            return View(cuotaPrecio);
        }

        // GET: CuotaPrecio/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CuotaPrecio/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CuotaPrecioID,Importe,FechaDesde,FechaHasta")] CuotaPrecio cuotaPrecio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cuotaPrecio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cuotaPrecio);
        }

        // GET: CuotaPrecio/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuotaPrecio = await _context.CuotaPrecio.FindAsync(id);
            if (cuotaPrecio == null)
            {
                return NotFound();
            }
            return View(cuotaPrecio);
        }

        // POST: CuotaPrecio/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CuotaPrecioID,Importe,FechaDesde,FechaHasta")] CuotaPrecio cuotaPrecio)
        {
            if (id != cuotaPrecio.CuotaPrecioID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cuotaPrecio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CuotaPrecioExists(cuotaPrecio.CuotaPrecioID))
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
            return View(cuotaPrecio);
        }

        // GET: CuotaPrecio/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuotaPrecio = await _context.CuotaPrecio
                .FirstOrDefaultAsync(m => m.CuotaPrecioID == id);
            if (cuotaPrecio == null)
            {
                return NotFound();
            }

            return View(cuotaPrecio);
        }

        // POST: CuotaPrecio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cuotaPrecio = await _context.CuotaPrecio.FindAsync(id);
            _context.CuotaPrecio.Remove(cuotaPrecio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CuotaPrecioExists(int id)
        {
            return _context.CuotaPrecio.Any(e => e.CuotaPrecioID == id);
        }
    }
}
