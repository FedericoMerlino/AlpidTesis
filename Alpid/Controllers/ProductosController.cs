﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Alpid.Data;
using Alpid.Models;
using Microsoft.AspNetCore.Authorization;

namespace Alpid.Controllers
{
    [Authorize]
    public class ProductosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Productos
        public async Task<IActionResult> Index(string currentFilter, string searchString, int? page, string filtroFecha, string DateFilter)
        {
            var bandera = true;

            if (searchString != null)
            {
                page = 1;
                bandera = false;

            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;
            var producto = from s in _context.Productos select s;

            if (!string.IsNullOrEmpty(searchString))
            {
                producto = producto.Where(s => s.Nombre.Contains(searchString));
            }

            ViewData["DateFilter"] = filtroFecha;

            if (filtroFecha == null && bandera == true)
            {
                producto = producto.Where(s => s.FechaBaja == null);
            }
            if (filtroFecha != null && bandera == true)
            {
                producto = producto.Where(s => s.FechaBaja != null);
            }

            int pageSize = 15;
            return View(await Paginacion<Productos>.CreateAsync(producto.AsNoTracking().OrderByDescending(x => x.FechaAlta).Include(x => x.Proveedores), page ?? 1, pageSize));
        }

        // GET: Productos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productos = await _context.Productos.Include(p => p.Proveedores)
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

            return View();
        }

        // POST: Productos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PoductosID,Nombre,Cantidad,ProductosTipo,FechaAlta,PrecioAlquiler,ProveedoresID")] Productos productos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProveedoresID"] = new SelectList(_context.Proveedores, "ProveedoresId", "RazonSocial", productos.ProveedoresID);

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
            return View(productos);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PoductosID,Nombre,Cantidad,ProductosTipo,FechaAlta,PrecioAlquiler,ProveedoresID")] Productos productos)
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
            return View(productos);
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
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
            return View(productos);
        }

        //// POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, [Bind("PoductosID,Nombre,Cantidad,ProductosTipo,FechaAlta,PrecioAlquiler,ProveedoresID,FechaBaja,MotivoBaja")] Productos productos)
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
            return View(productos);
        }

        // GET: Productos/Active/5
        public async Task<IActionResult> Active(int? id)
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
            return View(productos);
        }

        //// POST: Productos/Active/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Active(int id, [Bind("PoductosID,Nombre,Cantidad,ProductosTipo,FechaAlta,PrecioAlquiler,ProveedoresID,FechaBaja,MotivoBaja")] Productos productos)
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
            return View(productos);
        }

        // GET: Productos/DeletePhysical/5
        public async Task<IActionResult> DeletePhysical(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productos = await _context.Productos
                .Include(p => p.Proveedores)
                .FirstOrDefaultAsync(m => m.PoductosID == id);
            if (productos == null)
            {
                return NotFound();
            }

            return View(productos);
        }

        // POST: Productos/DeletePhysical/5
        [HttpPost, ActionName("DeletePhysical")]
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
