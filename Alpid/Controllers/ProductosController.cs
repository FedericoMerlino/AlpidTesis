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
    public class ProductosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Productos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Productos.Include(p => p.Proveedores)
                                                         .Include(p => p.ProductoTipos);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Productos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productos = await _context.Productos.Include(p => p.Proveedores)
                                                    .Include(p => p.ProductoTipos)
                                                    .FirstOrDefaultAsync(m => m.PoductosID == id);
            if (productos == null)
            {
                return NotFound();
            }

            return View(productos);
        }

        // GET: Productos/Create
        public IActionResult Create()
        {
            ViewData["ProveedoresID"] = new SelectList(_context.Proveedores, "ProveedoresId", "RazonSocial");
            ViewData["ProductosTipoID"] = new SelectList(_context.ProductoTipos, "ProductosTipoID", "Tipo");

            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PoductosID,Nombre,Cantidad,FechaCompra,PrecioCompra,FechaBaja,FechaAlta,MotivoBaja,PrecioAlquiler,ProductosTipoID,ProveedoresID")] Productos productos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProveedoresID"] = new SelectList(_context.Proveedores, "ProveedoresId", "RazonSocial", productos.ProveedoresID);
            ViewData["ProductosTipoID"] = new SelectList(_context.ProductoTipos, "ProductosTipoID", "Tipo", productos.ProductosTipoID);

            return View(productos);
        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productos = await _context.Productos.FindAsync(id);
            if (productos == null)
            {
                return NotFound();
            }
            ViewData["ProveedoresID"] = new SelectList(_context.Proveedores, "ProveedoresId", "RazonSocial", productos.ProveedoresID);
            ViewData["ProductosTipoID"] = new SelectList(_context.ProductoTipos, "ProductosTipoID", "Tipo", productos.ProductosTipoID);
            return View(productos);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PoductosID,Nombre,Cantidad,FechaCompra,PrecioCompra,FechaBaja,FechaAlta,MotivoBaja,PrecioAlquiler,ProductosTipoID,ProveedoresID")] Productos productos)
        {
            if (id != productos.PoductosID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductosExists(productos.PoductosID))
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
            ViewData["ProveedoresID"] = new SelectList(_context.Proveedores, "ProveedoresId", "RazonSocial", productos.ProveedoresID);
            ViewData["ProductosTipoID"] = new SelectList(_context.ProductoTipos, "ProductosTipoID", "Tipo", productos.ProductosTipoID);
            return View(productos);
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productos = await _context.Productos
                .Include(p => p.Proveedores)
                .Include(p => p.ProductoTipos)
                .FirstOrDefaultAsync(m => m.PoductosID == id);
            if (productos == null)
            {
                return NotFound();
            }

            return View(productos);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productos = await _context.Productos.FindAsync(id);
            _context.Productos.Remove(productos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductosExists(int id)
        {
            return _context.Productos.Any(e => e.PoductosID == id);
        }
    }
}
