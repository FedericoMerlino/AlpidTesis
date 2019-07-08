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
    public class ProductoTiposController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductoTiposController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProductoTipos
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProductoTipos.ToListAsync());
        }

        // GET: ProductoTipos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productoTipos = await _context.ProductoTipos
                .FirstOrDefaultAsync(m => m.ProductosTipoID == id);
            if (productoTipos == null)
            {
                return NotFound();
            }

            return View(productoTipos);
        }

        // GET: ProductoTipos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductoTipos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductosTipoID,Tipo,FechaBaja,FechaAlta")] ProductoTipos productoTipos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productoTipos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productoTipos);
        }

        // GET: ProductoTipos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productoTipos = await _context.ProductoTipos.FindAsync(id);
            if (productoTipos == null)
            {
                return NotFound();
            }
            return View(productoTipos);
        }

        // POST: ProductoTipos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductosTipoID,Tipo,FechaBaja,FechaAlta")] ProductoTipos productoTipos)
        {
            if (id != productoTipos.ProductosTipoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productoTipos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoTiposExists(productoTipos.ProductosTipoID))
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
            return View(productoTipos);
        }

        // GET: ProductoTipos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productoTipos = await _context.ProductoTipos
                .FirstOrDefaultAsync(m => m.ProductosTipoID == id);
            if (productoTipos == null)
            {
                return NotFound();
            }

            return View(productoTipos);
        }

        // POST: ProductoTipos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productoTipos = await _context.ProductoTipos.FindAsync(id);
            _context.ProductoTipos.Remove(productoTipos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoTiposExists(int id)
        {
            return _context.ProductoTipos.Any(e => e.ProductosTipoID == id);
        }
    }
}
