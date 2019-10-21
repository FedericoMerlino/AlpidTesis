using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Index(int? page)
        {
            var evento1 = (from e in _context.EventoSolidarios
                           orderby e.IdEvento, e.NombreEvento
                           select e);

            var evento = (from e in _context.EventoSolidarios select e);

            //var evento = _context.EventoSolidarios.SingleOrDefault(x => x.IdEvento == 1);
            int pageSize = 15;
            return View(await Paginacion<EventoSolidarios>.CreateAsync(evento.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: EventoSolidarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            var eventoSolidarios = await _context.EventoSolidarios
                .FirstOrDefaultAsync(m => m.Id == id);

            return View(eventoSolidarios);
        }

        // GET: EventoSolidario s/Create
        public async Task<IActionResult> Create(string Nombre, int idItem, int ID, decimal ValorTotal)
        {
            try
            {
                ViewData["ValorID"] = idItem;
                ViewData["NombreEvento"] = Nombre;
                ViewData["IdEventoResultado"] = ID;
                ViewData["ValorTotalResultado"] = ValorTotal;
                return View(await _context.EventoSolidarios.ToListAsync());
            }
            catch (Exception e)
            {
                Console.Write(e);
                return RedirectToAction("Index", "EventoSolidarios");
            }
        }

        // POST: EventoSolidarios/Create
        [HttpPost]
        public async Task<IActionResult> Create(int IdEvento, string NombreEvento, int IdItemEvento, int Cantidad, string Concepto,
                                                decimal Ingreso, decimal Salida, DateTime Fecha, decimal Total)
        {
            try
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
            catch (Exception e)
            {
                Console.Write(e);
                return RedirectToAction("Index", "EventoSolidarios");
            }
        }
        //[HttpPost]
        //public async Task<IActionResult> PhysicalDelete(int id)
        //{
        //    try
        //    {
        //        var productos = await _context.EventoSolidarios
        //            .FirstOrDefaultAsync(m => m.Id == id);

        //        return RedirectToAction("Create", "EventoSolidarios", new { Nombre, idItem, ID, ValorTotal });
        //    }
        //    catch (Exception e)
        //    {
        //        Console.Write(e);
        //        var valor = 2;
        //        return RedirectToAction("Index", "EventoSolidarios", new { valor });
        //    }
        //}


        // GET: EventoSolidarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            var eventoSolidarios = await _context.EventoSolidarios.FindAsync(id);

            return View(eventoSolidarios);
        }

        // POST: EventoSolidarios/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("EventoSolidarioID,Cantidad,Concepto,Ingreso,Salida,Total,Fecha")] EventoSolidarios eventoSolidarios)
        {
            _context.Update(eventoSolidarios);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: EventoSolidarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var eventoSolidarios = await _context.EventoSolidarios
                .FirstOrDefaultAsync(m => m.Id == id);

            return View(eventoSolidarios);
        }

        // POST: EventoSolidarios/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventoSolidarios = await _context.EventoSolidarios.FindAsync(id);
            _context.EventoSolidarios.Remove(eventoSolidarios);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
