﻿using System;
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
    public class AlquilerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlquilerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Alquiler
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Alquiler.Include(a => a.Productos).Include(a => a.Socios);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Alquiler/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alquiler = await _context.Alquiler
                .Include(a => a.Productos)
                .Include(a => a.Socios)
                .FirstOrDefaultAsync(m => m.AlquilerID == id);
            if (alquiler == null)
            {
                return NotFound();
            }

            return View(alquiler);
        }

        // GET: Alquiler/Create
        public IActionResult Create()
        {
            ViewData["ProductosID"] = new SelectList(_context.Set<Productos>(), "PoductosID", "Nombre");
            ViewData["SociosId"] = new SelectList(_context.Socios, "SociosID", "Cuit");
            return View();
        }

        // POST: Alquiler/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AlquilerID,Fecha,Observacion,Valor,ProductosID,SociosId")] Alquiler alquiler)
        {
            if (ModelState.IsValid)
            {
                _context.Add(alquiler);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductosID"] = new SelectList(_context.Set<Productos>(), "PoductosID", "Nombre", alquiler.ProductosID);
            ViewData["SociosId"] = new SelectList(_context.Socios, "SociosID", "Cuit", alquiler.SociosId);
            return View(alquiler);
        }

        // GET: Alquiler/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alquiler = await _context.Alquiler.FindAsync(id);
            if (alquiler == null)
            {
                return NotFound();
            }
            ViewData["ProductosID"] = new SelectList(_context.Set<Productos>(), "PoductosID", "Nombre", alquiler.ProductosID);
            ViewData["SociosId"] = new SelectList(_context.Socios, "SociosID", "Cuit", alquiler.SociosId);
            return View(alquiler);
        }

        // POST: Alquiler/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AlquilerID,Fecha,Observacion,Valor,ProductosID,SociosId")] Alquiler alquiler)
        {
            if (id != alquiler.AlquilerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alquiler);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlquilerExists(alquiler.AlquilerID))
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
            ViewData["ProductosID"] = new SelectList(_context.Set<Productos>(), "PoductosID", "Nombre", alquiler.ProductosID);
            ViewData["SociosId"] = new SelectList(_context.Socios, "SociosID", "Cuit", alquiler.SociosId);
            return View(alquiler);
        }

        // GET: Alquiler/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alquiler = await _context.Alquiler
                .Include(a => a.Productos)
                .Include(a => a.Socios)
                .FirstOrDefaultAsync(m => m.AlquilerID == id);
            if (alquiler == null)
            {
                return NotFound();
            }

            return View(alquiler);
        }

        // POST: Alquiler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var alquiler = await _context.Alquiler.FindAsync(id);
            _context.Alquiler.Remove(alquiler);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlquilerExists(int id)
        {
            return _context.Alquiler.Any(e => e.AlquilerID == id);
        }
    }
}
