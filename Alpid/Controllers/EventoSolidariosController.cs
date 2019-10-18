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

        // GET: EventoSolidario s/Create
        public async Task<IActionResult> Create(string Nombre, int idItem, int ID, decimal ValorTotal)
        {
            ViewData["ValorID"] = idItem;
            ViewData["NombreEvento"] = Nombre;
            ViewData["IdEventoResultado"] = ID;
            ViewData["ValorTotalResultado"] = ValorTotal;
            return View(await _context.EventoSolidarios.ToListAsync());
        }

        // POST: EventoSolidarios/Create
        [HttpPost]
        public async Task<IActionResult> Create(int IdEvento, string NombreEvento, int IdItemEvento, int Cantidad, string Concepto,
                                                decimal Ingreso, decimal Salida, DateTime Fecha, decimal Total)
        {
            var evento = new EventoSolidarios();
            var ID = 0;

            evento.Fecha = Fecha;
            evento.Cantidad = Cantidad;
            evento.Concepto = Concepto;
            evento.Ingreso = Ingreso;
            evento.NombreEvento = NombreEvento;
            evento.Salida = Salida;

            if (IdItemEvento == 0)
            {
                evento.IdItemEvento = 1;
            }
            else
            {
                evento.IdItemEvento = IdItemEvento + 1;
            }
            var PrimerEvento = (from c in _context.EventoSolidarios select c).Count();

            decimal? ValorTotal;

            if (PrimerEvento == 0)
            {
                evento.IdEvento = 1;

                ValorTotal = 0;
                ValorTotal = ValorTotal + Ingreso;
                ValorTotal = ValorTotal - Salida;
                evento.Total = ValorTotal;
            }
            else
            {
                var UltimoIdbase = (from c in _context.EventoSolidarios select c.Id).Max();
                var valoresBase = _context.EventoSolidarios.SingleOrDefault(x => x.Id == UltimoIdbase);

                if (valoresBase.NombreEvento == NombreEvento)
                {
                    evento.IdEvento = IdEvento;
                    ID = IdEvento;

                }
                else
                {
                    evento.IdEvento = valoresBase.IdEvento + 1;
                    ID = valoresBase.IdEvento + 1;

                }

                ValorTotal = Total;
                ValorTotal = ValorTotal + Ingreso;
                ValorTotal = ValorTotal - Salida;
                evento.Total = ValorTotal;
            }
            _context.Add(evento);
            await _context.SaveChangesAsync();

            var idItem = IdItemEvento + 1;
            var Nombre = NombreEvento;

            return RedirectToAction("Create", "EventoSolidarios", new { Nombre, idItem, ID, ValorTotal });
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
