using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Alpid.Data;
using Alpid.Models;
using System.Collections.Generic;

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
        public async Task<IActionResult> Index(int? page, int valor)
        {
            ViewData["Message"] = valor;
            ViewData["Repetido"] = "Nombre";

            var evento = (from e in _context.EventoSolidarios orderby e.IdEvento select new EventoSolidarios { IdEvento = e.IdEvento, NombreEvento = e.NombreEvento, Fecha = e.Fecha }).Distinct();

            int pageSize = 15;
            return View(await Paginacion<EventoSolidarios>.CreateAsync(evento, page ?? 1, pageSize));
        }

        // GET: EventoSolidarios/Details/5
        public async Task<IActionResult> Details(int? id, int? page)
        {
            try
            {
                //obtiene el nombre
                var eventoSolidarios = await _context.EventoSolidarios.FirstOrDefaultAsync(m => m.IdEvento == id);
                ViewData["NombreEvento"] = eventoSolidarios.NombreEvento;
                ViewData["FechaEvento"] = eventoSolidarios.Fecha.ToShortDateString();

                //obtiene valor para mostran en pantalla
                var evento = (from e in _context.EventoSolidarios where e.IdEvento == id select e);

                int pageSize = 15;
                return View(await Paginacion<EventoSolidarios>.CreateAsync(evento.AsNoTracking(), page ?? 1, pageSize));

            }
            catch (Exception e)
            {
                Console.Write(e);
                var valor = 2;
                return RedirectToAction("Index", "EventoSolidarios", new { valor });
            }
        }

        // GET: EventoSolidario s/Create
        public async Task<IActionResult> Create(string Nombre, int idItem, int ID, DateTime Fecha, decimal ResultadoTotal)
        {
            try
            {
                ViewData["ValorID"] = idItem;
                ViewData["NombreEvento"] = Nombre;
                ViewData["IdEventoResultado"] = ID;
                ViewData["Fecha"] = Fecha.ToShortDateString();
                ViewData["ResultadoTotal"] = ResultadoTotal;
                return View(await _context.EventoSolidarios.ToListAsync());
            }
            catch (Exception e)
            {
                Console.Write(e);
                var valor = 2;
                return RedirectToAction("Index", "EventoSolidarios", new { valor });
            }
        }

        // POST: EventoSolidarios/Create
        [HttpPost]
        public async Task<IActionResult> Create(int IdEvento, string NombreEvento, int IdItemEvento, int Cantidad, string Concepto,
                                                decimal Ingreso, decimal Salida, DateTime Fecha, decimal Total)
        {
            try
            {
                //inicializa variables
                var evento = new EventoSolidarios();
                var ID = 0;
                decimal? ValorTotal;
                // busco si es el primer valor en la base
                var PrimerEvento = (from c in _context.EventoSolidarios select c).Count();

                //asigno variables
                evento.Fecha = Fecha;
                evento.Cantidad = Cantidad;
                evento.Concepto = Concepto;
                evento.Ingreso = Ingreso;
                evento.NombreEvento = NombreEvento;
                evento.Salida = Salida;
                evento.Total = Ingreso - Salida;

                //asigno el numero de item del evento
                if (IdItemEvento == 0)
                {
                    evento.IdItemEvento = 1;
                }
                else
                {
                    var UltIdItem = (from c in _context.EventoSolidarios where c.IdEvento == IdEvento select c.IdItemEvento).Count();
                    if (UltIdItem > 0)
                    {
                        var obetnerUltimoIdItem = (from c in _context.EventoSolidarios where c.IdEvento == IdEvento select c.IdItemEvento).Max();

                        if (obetnerUltimoIdItem == 0)
                        {
                            evento.IdItemEvento = 1;
                        }
                        else
                        {
                            evento.IdItemEvento = obetnerUltimoIdItem + 1;
                        }
                    }
                    else
                    {
                        evento.IdItemEvento = 1;
                    }
                }
                //si es el primer evento de la base 
                if (PrimerEvento == 0)
                {
                    evento.IdEvento = 1;
                    ID = 1;
                }
                //si no es el primer valor de la base
                else
                {
                    //busca valores en la base
                    var UltimoItemIdbase = (from c in _context.EventoSolidarios select c.IdEvento).Max();
                    var UltimoIdbase = (from c in _context.EventoSolidarios where c.IdEvento == UltimoItemIdbase select c.Id).FirstOrDefault();
                    var valoresBase = _context.EventoSolidarios.SingleOrDefault(x => x.Id == UltimoIdbase);

                    //asigno el item del id 
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
                }
                _context.Add(evento);
                await _context.SaveChangesAsync();

                var idItem = IdItemEvento + 1;
                var Nombre = NombreEvento;

                return RedirectToAction("Create", "EventoSolidarios", new { Nombre, idItem, ID, Fecha });
            }
            catch (Exception e)
            {
                Console.Write(e);
                var valor = 2;
                return RedirectToAction("Index", "EventoSolidarios", new { valor });
            }
        }

        public async Task<IActionResult> DeletePhysical(int id)
        {
            try
            {
                int idItem;
                string Nombre;
                int ID;
                //var ValorPrevio = Convert.ToDecimal(0);
                //string ValorAnteriorSuma;
                //string ValorSiguienteSuma;
                // busco en la base
                var Base = _context.EventoSolidarios.SingleOrDefault(x => x.Id == id);
                Nombre = Base.NombreEvento;
                if (Base.IdItemEvento == 0)
                {
                    idItem = 0;
                }
                else
                {
                    idItem = Base.IdEvento - 1;
                }
                ID = Base.IdEvento;

                

                //Deleteo fila base segun el id
                var evento = await _context.EventoSolidarios.FindAsync(id);
                _context.EventoSolidarios.Remove(evento);
                await _context.SaveChangesAsync();

                var ResultadoTotal = (from x in _context.EventoSolidarios where x.IdEvento == Base.IdEvento select x.Total).Sum();

                return RedirectToAction("Create", "EventoSolidarios", new { Nombre, idItem, ID, ResultadoTotal });
            }
            catch (Exception e)
            {
                Console.Write(e);
                var valor = 2;
                return RedirectToAction("Index", "EventoSolidarios", new { valor });
            }
        }

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

    }
}
