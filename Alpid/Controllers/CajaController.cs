using System;
using System.Linq;
using System.Threading.Tasks;
using Alpid.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Alpid.Models;
using Microsoft.AspNetCore.Authorization;
using Rotativa.AspNetCore;
using System.Collections.Generic;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace Alpid.Controllers
{
    //[Authorize]
    public class CajaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CajaController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: Caja
        public async Task<IActionResult> Index(DateTime fechaDesde, DateTime fechaHasta,
                        string FechaDesdeFilter, string FechaHastaFilter, int valor)
        {
            try
            {
                if (fechaDesde.Day == 1 && fechaDesde.Month == 1 && fechaDesde.Year == 1)
                {
                    fechaDesde = DateTime.Now;
                }
                if (fechaHasta.Day == 1 && fechaHasta.Month == 1 && fechaHasta.Year == 1)
                {
                    fechaHasta = DateTime.Now;
                }
                ViewData["FechaDesdeFilter"] = fechaDesde.ToShortDateString();
                ViewData["FechaHastaFilter"] = fechaHasta.ToShortDateString();

                var caja = from s in _context.Caja select s ;
                //where((s.FechaMovimiento >= fechaDesde) && (s.FechaMovimiento <= fechaHasta))
                //caja = caja.Where(s => s.FechaMovimiento >= fechaDesde);
                //caja = caja.Where(s => s.FechaMovimiento <= fechaHasta);
                caja = caja.OrderByDescending(s => s.FechaMovimiento);

                ViewData["Message"] = valor;

                var applicationDbContext = caja;
                return View(await applicationDbContext.ToListAsync());
            }
            catch (Exception e)
            {
                Console.Write(e);
                var PAsarvalor = 2;
                return RedirectToAction("Index", "Caja", new { PAsarvalor });
            }
        }

        public String ListaCaja(DateTime fechaDesde, DateTime fechaHasta)
        {
            String dataFilter = "";
            var caja = _context.Caja.OrderBy(p => p.AlquilerID).ToList();
            var query = caja.Where(c => c.Debe == 100);//(c.FechaMovimiento >= fechaDesde) && (c.FechaMovimiento <= fechaHasta));
            foreach (var item in query)
            {
                dataFilter += "<tr>" +
                   "<td>" + item.FechaMovimiento + "</td>" +
                   "<td>" + item.Observaciones + "</td>" +
                   "<td>" + item.Debe + "</td>" +
                    "<td>" + item.Haber + "</td>" +
                   "<td>" + item.Total + "</td>" +
               "</tr>";
            }
            return dataFilter;
        }

        internal List<Caja> getCaja(DateTime fechaDesde, DateTime fechaHasta)
        {
            return _context.Caja.Where(c => (c.FechaMovimiento >= fechaDesde) && (c.FechaMovimiento <= fechaHasta)).ToList();
        }

        public IActionResult ViewAsPDFCaja()
        {
            return new ViewAsPdf("Report");
        }

        // GET: Caja/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                var caja = await _context.Caja
                    .Include(c => c.Alquiler)
                    .FirstOrDefaultAsync(m => m.CajaId == id);
                if (caja == null)
                {
                    return NotFound();
                }
                return View(caja);
            }
            catch (Exception e)
            {
                Console.Write(e);
                var PAsarvalor = 2;
                return RedirectToAction("Index", "Caja", new { PAsarvalor });
            }
        }
        //Retiro de dinero
        public IActionResult CreateRetire(int valor)
        {
            try
            {
                var ultimoID = _context.Caja.Max(x => x.CajaId);
                var ultimoPrecio = _context.Caja.SingleOrDefault(x => x.CajaId == ultimoID);
                ViewData["TotalCaja"] = ultimoPrecio.Total;
                ViewData["Message"] = valor;
                return View();
            }
            catch (Exception e)
            {
                Console.Write(e);
                var PAsarvalor = 2;
                return RedirectToAction("Index", "Caja", new { PAsarvalor });
            }
        }
        //Retiro de dinero
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRetire([Bind("CajaId,Debe,Haber,TipoMovimiento,Observaciones,Estado,FechaMovimiento,Total,CuotaID,AlquilerID,Usuario")] Caja caja)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //busca el ultimo id 
                    var db = _context.Caja.Include(c => c.Alquiler)
                                      .Max(c => c.CajaId);
                    //busca el valor del total del ultimo id obtenido arriba
                    var UltimoValor = _context.Caja.SingleOrDefault(c => c.CajaId == db);
                    //Restar valor
                    caja.Total = UltimoValor.Total - caja.Haber;
                    //Obtener fecha y hora actual
                    caja.FechaMovimiento = DateTime.Now;
                    if (caja.Total < 0)
                    {
                        var valor = 1;
                        return RedirectToAction("CreateRetire", "Caja", new { valor });
                    }
                    else
                    {
                        _context.Add(caja);
                        //valor para mostarr mensaje
                        var valor = 1;
                        await _context.SaveChangesAsync();

                        return RedirectToAction("Index", "Caja", new { valor });
                    }
                }
                ViewData["AlquilerID"] = new SelectList(_context.Alquiler, "AlquilerID", "AlquilerID", caja.AlquilerID);
                return View(caja);
            }
            catch (Exception e)
            {
                Console.Write(e);
                var valor = 2;
                return RedirectToAction("Index", "Caja", new { valor });
            }
        }
        //ingreso de dinero
        public IActionResult CreateIngreso()
        {
            try
            {
                var ultimoID = _context.Caja.Max(x => x.CajaId);
                var ultimoPrecio = _context.Caja.SingleOrDefault(x => x.CajaId == ultimoID);
                ViewData["TotalCaja"] = ultimoPrecio.Total;
                return View();
            }
            catch (Exception e)
            {
                Console.Write(e);
                var PAsarvalor = 2;
                return RedirectToAction("Index", "Caja", new { PAsarvalor });
            }
        }
        //ingreso de dinero
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateIngreso([Bind("CajaId,Debe,Haber,TipoMovimiento,Observaciones,Estado,FechaMovimiento,Total,CuotaID,AlquilerID,Usuario")] Caja caja)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //busca el ultimo id 
                    var db = _context.Caja.Include(c => c.Alquiler)
                                  .Max(c => c.CajaId);
                    //busca el valor del total del ultimo id obtenido arriba
                    var UltimoValor = _context.Caja.SingleOrDefault(c => c.CajaId == db);
                    //sumar valor
                    caja.Total = UltimoValor.Total + caja.Debe;
                    //Obtener fecha y hora actual
                    caja.FechaMovimiento = DateTime.Now;
                    _context.Add(caja);
                    await _context.SaveChangesAsync();
                    var valor = 1;
                    return RedirectToAction("CreateRetire", "Caja", new { valor });
                }
                ViewData["AlquilerID"] = new SelectList(_context.Alquiler, "AlquilerID", "AlquilerID", caja.AlquilerID);
                return View(caja);
            }
            catch (Exception e)
            {
                Console.Write(e);

                var valor = 2;
                return RedirectToAction("Index", "Caja", new { valor });
            }
        }
    }
}
