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
        public async Task<IActionResult> Index(int? page, int valor)
        {
            ViewData["Message"] = valor;

        //    var evento = _context.EventoSolidarios.GroupBy(c => new
        //{
        //    c.IdEvento,
        //    c.IdItemEvento,
        //})
        //.Select(gcs => new 
        //{
        //    School = gcs.Key.IdEvento,
        //    Friend = gcs.Key.IdItemEvento,
        //}).ToAsyncEnumerable();
            //var evento1 = (from e in _context.EventoSolidarios
            //               orderby e.IdEvento, e.NombreEvento
            //               select e);

            var evento = (from e in _context.EventoSolidarios select e);

            //var evento = _context.EventoSolidarios.SingleOrDefault(x => x.IdEvento == 1);
            int pageSize = 15;
            return View(await Paginacion<EventoSolidarios>.CreateAsync(evento.AsNoTracking(), page ?? 1, pageSize));
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
        public async Task<IActionResult> Create(string Nombre, int idItem, int ID, decimal? ValorTotal)
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

                    ValorTotal = 0;
                    ValorTotal = ValorTotal + Ingreso;
                    ValorTotal = ValorTotal - Salida;
                    evento.Total = ValorTotal;
                    ID = 1;
                }
                //si no es el primer valor de la base
                else
                {
                    //busca valores en la base
                    var UltimoIdbase = (from c in _context.EventoSolidarios select c.Id).Max();
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
                var ValorPrevio = Convert.ToDecimal(0);
                string ValorAnteriorSuma;
                string ValorSiguienteSuma;
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

                // obtiene el item del evento mas grande
                var UltimoItemId = (from s in _context.EventoSolidarios where s.IdEvento == Base.IdEvento select s.IdItemEvento).Max();
                // si el item del evento es el mas grande 
                // elimina el registro y busca el ultimo total del intem anterior
                if (UltimoItemId == Base.IdItemEvento && UltimoItemId != 1)
                {
                    //busco el evento con el que estamos trabajando
                    var valorItem = (from s in _context.EventoSolidarios orderby s.IdItemEvento descending where s.IdEvento == Base.IdEvento select s).FirstOrDefault();
                    // guardo el ultimo itemid 
                    var ultimoItemResto = valorItem.IdItemEvento - 1;
                    // busco el total del ultimo itemid
                    var ultimo = (from x in _context.EventoSolidarios where x.IdEvento == Base.IdEvento && x.IdItemEvento == ultimoItemResto select x.Total);
                    ValorPrevio = Convert.ToDecimal(ultimo);
                }
                // si el item del eventoesta en el medio
                if (UltimoItemId != Base.IdItemEvento && UltimoItemId != 1)
                {
                    //busco el evento con el que estamos trabajando
                    var valorItem = (from s in _context.EventoSolidarios orderby s.IdItemEvento descending where s.IdEvento == Base.IdEvento select s).FirstOrDefault();
                    // guardo el itemid anterior 
                    var ItemIdAnterior = valorItem.IdItemEvento - 1;
                    // guardo el itemid siguiente 
                    var ItemISiguiente = valorItem.IdItemEvento + 1;

                    // busco el total del ultimo itemid
                    var ValorAnterior = (from x in _context.EventoSolidarios where x.IdEvento == Base.IdEvento && x.IdItemEvento == ItemIdAnterior select x.Total).FirstOrDefault();
                    // busco el total del ultimo itemid
                    var ValorSiguiente = (from x in _context.EventoSolidarios where x.IdEvento == Base.IdEvento && x.IdItemEvento == ItemISiguiente select x.Total).FirstOrDefault();

                    if (ValorSiguiente == null)
                    {
                        ValorSiguienteSuma = (0).ToString();
                    }
                    else
                    {
                        ValorSiguienteSuma = ValorSiguiente.ToString();

                    }

                    if (ValorAnterior == null)
                    {
                        ValorAnteriorSuma = (0).ToString();
                    }
                    else
                    {
                        ValorAnteriorSuma = ValorAnterior.ToString();

                    }
                    ValorPrevio = Convert.ToDecimal(ValorAnteriorSuma) - Convert.ToDecimal(ValorSiguienteSuma);
                }
                // si solo existe un item
                if (UltimoItemId == 1)
                {
                    // paso el valor en  0 porque no tiene registros
                    ValorPrevio = 0;
                }

                var ValorTotal = ValorPrevio;
                //actualizpo el precion total en la ultima fila
                var IdActualizarValor = (from s in _context.EventoSolidarios where s.IdEvento == Base.IdEvento && s.IdItemEvento == (Base.IdItemEvento + 1) select s).SingleOrDefault();

                if (IdActualizarValor != null)
                {
                    IdActualizarValor.Total = ValorTotal;

                    _context.Update(IdActualizarValor);
                    await _context.SaveChangesAsync();
                }


                //Deleteo fila base segun el id
                var evento = await _context.EventoSolidarios.FindAsync(id);
                _context.EventoSolidarios.Remove(evento);
                await _context.SaveChangesAsync();

                return RedirectToAction("Create", "EventoSolidarios", new { Nombre, idItem, ID, ValorTotal });
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
