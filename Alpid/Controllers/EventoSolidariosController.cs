using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Alpid.Data;
using Alpid.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Alpid.Controllers
{
    [Authorize]
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

            var evento = (from e in _context.EventoSolidarios orderby e.IdEvento
                          select new EventoSolidarios { IdEvento = e.IdEvento, NombreEvento = e.NombreEvento, Fecha = e.Fecha }).Distinct();

           int pageSize = 100;
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

                var maxvalor = (from s in _context.EventoSolidarios orderby s.IdItemEvento descending where s.IdEvento == id select s.ResultadoFinal).FirstOrDefaultAsync(); ;
                ViewData["ResultadoTotal"] = maxvalor.Result;
                //obtiene valor para mostran en pantalla
                var evento = (from e in _context.EventoSolidarios where e.IdEvento == id select e);

               int pageSize = 100;
                return View(await Paginacion<EventoSolidarios>.CreateAsync(evento.AsNoTracking(), page ?? 1, pageSize));

            }
            catch (Exception e)
            {
                Console.Write(e);
                var valor = 2;
                return RedirectToAction("Index", "EventoSolidarios", new { valor });
            }
        }

        public async Task<IActionResult> Report(int? page)
        {
           
           int pageSize = 100;

            var evento = (from e in _context.EventoSolidarios
                          orderby e.IdEvento
                          select new EventoSolidarios { IdEvento = e.IdEvento, NombreEvento = e.NombreEvento,Fecha = e.Fecha}).Distinct();

            return View(await Paginacion<EventoSolidarios>.CreateAsync(evento, page ?? 1, pageSize));

        }

        // GET: EventoSolidario s/Create
        public async Task<IActionResult> Create(int valor,string NombreRepetido, string Nombre, int idItem, int ID, DateTime Fecha, decimal ResultadoTotal)
        {
            try
            {
                ViewData["Message"] = valor;
                ViewData["ValorID"] = idItem;
                ViewData["NombreEvento"] = Nombre;
                ViewData["NombreRepetido"] = NombreRepetido; 
                ViewData["IdEventoResultado"] = ID;
                ViewData["Fecha"] = Fecha.ToShortDateString();
                ViewData["ResultadoTotal"] = ResultadoTotal;
                return View(await _context.EventoSolidarios.ToListAsync());
            }
            catch (Exception e)
            {
                Console.Write(e);
                valor = 2;
                return RedirectToAction("Index", "EventoSolidarios", new { valor });
            }
        }

        // POST: EventoSolidarios/Create
        [HttpPost]
        public async Task<IActionResult> Create(int IdEvento, string NombreEvento, int IdItemEvento, int Cantidad, string Concepto,
                                                decimal Ingreso, decimal Salida, DateTime Fecha)
        {
            try
            {
                var verificarNombre = (from c in _context.EventoSolidarios where NombreEvento == c.NombreEvento select c).Count();


                if (verificarNombre > 0 && IdEvento < 0)
                {
                    var valor = 3;
                    var NombreRepetido = NombreEvento;

                    return RedirectToAction("Create", "EventoSolidarios", new { valor, NombreRepetido });
                }
                else
                {
                    //inicializa variables
                    var evento = new EventoSolidarios();
                    var ID = 0;
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

                    var obtenreid = (from a in _context.EventoSolidarios
                                     where a.IdEvento == ID
                                     select a.Id).Max();

                    var ValorEvento = await _context.EventoSolidarios.FindAsync(obtenreid);

                    var ResultadoTotal = (from x in _context.EventoSolidarios where x.IdEvento == ID select x.Total).Sum();
                    ValorEvento.ResultadoFinal = ResultadoTotal;
                    ValorEvento.IdEvento = ValorEvento.IdEvento;

                    _context.EventoSolidarios.Update(ValorEvento);
                    await _context.SaveChangesAsync();


                    var idItem = IdItemEvento + 1;
                    var Nombre = NombreEvento;

                    return RedirectToAction("Create", "EventoSolidarios", new { Nombre, idItem, ID, Fecha, ResultadoTotal });
                }
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

                var Existe = (from s in _context.EventoSolidarios where s.IdEvento == Base.IdEvento select s).Count();

                decimal? ResultadoTotal = 0;

                if (Existe > 0)
                {
                    var obtenreid = (from a in _context.EventoSolidarios
                                     where a.IdEvento == ID
                                     select a.Id).Max();

                    var ValorEvento = await _context.EventoSolidarios.FindAsync(obtenreid);

                    ResultadoTotal = (from x in _context.EventoSolidarios where x.IdEvento == ID select x.Total).Sum();
                    
                    ValorEvento.ResultadoFinal = ResultadoTotal;
                    ValorEvento.IdEvento = Base.IdEvento;

                    _context.EventoSolidarios.Update(ValorEvento);
                    await _context.SaveChangesAsync();
                }

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

            try
            {
                var eventoSolidarios = await _context.EventoSolidarios.FirstOrDefaultAsync(m => m.IdEvento == id);
                var maxvalor = (from s in _context.EventoSolidarios orderby s.IdItemEvento descending where s.IdEvento == id select s.ResultadoFinal).FirstOrDefaultAsync(); ;
               
                //obtiene valor para mostran en pantalla
                ViewData["ValorID"] = eventoSolidarios.IdItemEvento;
                ViewData["NombreEvento"] = eventoSolidarios.NombreEvento;
                ViewData["IdEventoResultado"] = eventoSolidarios.IdEvento;
                ViewData["Fecha"] = eventoSolidarios.Fecha.ToShortDateString();
                ViewData["ResultadoTotal"] = maxvalor.Result;
                return View(await _context.EventoSolidarios.ToListAsync());
            }
            catch (Exception e)
            {
                Console.Write(e);
                var valor = 2;
                return RedirectToAction("Index", "EventoSolidarios", new { valor });
            }
        }

        // POST: EventoSolidarios/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int IdEvento, string NombreEvento, int IdItemEvento, int Cantidad, string Concepto,
                                                decimal Ingreso, decimal Salida, DateTime Fecha)
        { 
            try
            {
                //inicializa variables
                var evento = new EventoSolidarios();
                var ID = 0;
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

                var obtenreid = (from a in _context.EventoSolidarios
                                 where a.IdEvento == ID
                                 select a.Id).Max();

                var ValorEvento = await _context.EventoSolidarios.FindAsync(obtenreid);

                var ResultadoTotal = (from x in _context.EventoSolidarios where x.IdEvento == ID select x.Total).Sum();
                ValorEvento.ResultadoFinal = ResultadoTotal;
                ValorEvento.IdEvento = ValorEvento.IdEvento;

                _context.EventoSolidarios.Update(ValorEvento);
                await _context.SaveChangesAsync();


                var idItem = IdItemEvento + 1;
                var Nombre = NombreEvento;

                return RedirectToAction("Create", "EventoSolidarios", new { Nombre, idItem, ID, Fecha, ResultadoTotal });
            }
            catch (Exception e)
            {
                Console.Write(e);
                var valor = 2;
                return RedirectToAction("Index", "EventoSolidarios", new { valor });
            }
        }

    }
}
