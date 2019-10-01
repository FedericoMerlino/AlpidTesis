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
    public class EventoSolidariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventoSolidariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EventoSolidarios
        public async Task<IActionResult> Index()
        {
            return View(await _context.EventoSolidarios.ToListAsync());
        }

        // GET: EventoSolidarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventoSolidarios = await _context.EventoSolidarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventoSolidarios == null)
            {
                return NotFound();
            }

            return View(eventoSolidarios);
        }

        // GET: EventoSolidarios/Create
        public IActionResult Create()
        {
            return View();
        }
        public ActionResult Crear(RowDtoEventosSolidarios[] filas)
        {
            if (ModelState.IsValid)
            {
                var ultimoId = _context.EventoSolidarios.Max(x => x.Id);
                var item = 1;

                while (item != 4)
                {
                    var valorID = ultimoId + 1;
                    //eventoSolidarios.ItemEventoSolidarioID = item;
                    _context.Add(filas);
                    _context.SaveChangesAsync();

                    item++;
                }

                return RedirectToAction(nameof(Index));
            }
            return View(filas);
        }
        // POST: EventoSolidarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("EventoSolidarioID,ItemEventoSolidarioID,Cantidad,Concepto,Ingreso,Salida,Total,Fecha")] EventoSolidarios[] eventoSolidarios)
        {
            if (ModelState.IsValid)
            {
                var ultimoId = _context.EventoSolidarios.Max(x => x.Id);
                var item = 1;

                while (item != 4)
                {
                    var valorID = ultimoId + 1;
                    //eventoSolidarios.ItemEventoSolidarioID = item;
                    _context.Add(eventoSolidarios);
                     _context.SaveChangesAsync();

                    item ++;
                }

                return RedirectToAction(nameof(Index));
            }
            return View(eventoSolidarios);
        }

        // GET: EventoSolidarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventoSolidarios = await _context.EventoSolidarios.FindAsync(id);
            if (eventoSolidarios == null)
            {
                return NotFound();
            }
            return View(eventoSolidarios);
        }

        // POST: EventoSolidarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventoSolidarioID,Cantidad,Concepto,Ingreso,Salida,Total,Fecha")] EventoSolidarios eventoSolidarios)
        {
            if (id != eventoSolidarios.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventoSolidarios);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventoSolidariosExists(eventoSolidarios.Id))
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
            return View(eventoSolidarios);
        }

        // GET: EventoSolidarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventoSolidarios = await _context.EventoSolidarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventoSolidarios == null)
            {
                return NotFound();
            }

            return View(eventoSolidarios);
        }

        // POST: EventoSolidarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventoSolidarios = await _context.EventoSolidarios.FindAsync(id);
            _context.EventoSolidarios.Remove(eventoSolidarios);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventoSolidariosExists(int id)
        {
            return _context.EventoSolidarios.Any(e => e.Id == id);
        }
    }
}
