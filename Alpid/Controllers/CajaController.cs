using System;
using System.Linq;
using System.Threading.Tasks;
using Alpid.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Alpid.Models;
using Rotativa.AspNetCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Alpid.Controllers
{
    [Authorize]
    public class CajaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CajaController(ApplicationDbContext context)
        {
            _context = context;
        }

        public DateTime FechaDesdeFilterGloval;
        public DateTime FechaHastaFilterGloval;

        // GET: Caja
        public async Task<IActionResult> Index(DateTime fechaDesde, DateTime fechaHasta,
                        string FechaDesdeFilter, string FechaHastaFilter, int valor)
        {
            try
            {
                string mesDesde;
                string mesHasta;
                string DiaDesde;
                string DiaHasta;

                if (fechaDesde.Day == 1 && fechaDesde.Month == 1 && fechaDesde.Year == 1)
                {
                    fechaDesde = DateTime.Now.AddDays(-30);
                };
                if (fechaHasta.Day == 1 && fechaHasta.Month == 1 && fechaHasta.Year == 1)
                {
                    fechaHasta = DateTime.Now;
                };

                if (fechaDesde.Month < 9)
                {
                    mesDesde = "-0" + fechaDesde.Month;
                }
                else
                {
                    mesDesde = "-" + fechaDesde.Month;
                }
                if (fechaHasta.Month < 9)
                {
                    mesHasta = "-0" + fechaHasta.Month;
                }
                else
                {
                    mesHasta = "-" + fechaHasta.Month;
                }

                if (fechaDesde.Day < 9)
                {
                    DiaDesde = "-0" + fechaDesde.Day;
                }
                else
                {
                    DiaDesde = "-" + fechaDesde.Day;
                }
                if (fechaHasta.Day < 9)
                {
                    DiaHasta = "-0" + fechaHasta.Day;
                }
                else
                {
                    DiaHasta = "-" + fechaHasta.Day;
                }
                ViewData["FechaDesdeFilter"] = fechaDesde.Year + mesDesde + DiaDesde;
                ViewData["FechaHastaFilter"] = fechaHasta.Year + mesHasta + DiaHasta; 

                HttpContext.Session.SetString("FechaDesdeFilterGloval", fechaDesde.ToShortDateString());
                HttpContext.Session.SetString("FechaHastaFilterGloval", fechaHasta.ToShortDateString());

                var caja = from s in _context.Caja select s;
                caja = caja.Where(s => Convert.ToDateTime(s.FechaMovimiento.ToShortDateString()) >= Convert.ToDateTime(fechaDesde.ToShortDateString()));
                caja = caja.Where(s => Convert.ToDateTime(s.FechaMovimiento.ToShortDateString()) <= Convert.ToDateTime(fechaHasta.ToShortDateString()));
                caja = caja.OrderByDescending(s => s.FechaMovimiento);

                var cajaVacia = (from c in _context.Caja select c).Count();
                if (cajaVacia == 0)
                {
                    ViewData["ValorParaBoton"] = cajaVacia;
                    ViewData["TotalCaja"] = 0;
                }
                else
                {
                    var Ultimacaja = (from s in _context.Caja orderby s.CajaId descending select s).FirstOrDefault().Total;
                    ViewData["ValorParaBoton"] = Convert.ToInt32(Ultimacaja);
                    var ultimoID = _context.Caja.Max(x => x.CajaId);
                    var ultimoPrecio = _context.Caja.SingleOrDefault(x => x.CajaId == ultimoID);
                    ViewData["TotalCaja"] = ultimoPrecio.Total;
                }
                ViewData["Message"] = valor;

                var applicationDbContext = caja;
                return View(await applicationDbContext.ToListAsync());
            }
            catch (Exception e)
            {
                Console.Write(e);
                valor = 2;
                return RedirectToAction("Index", "Caja", new { valor });
            }
        }
                     
        public async Task<IActionResult> Report(int? page)
        {
            var fechaDesde =  HttpContext.Session.GetString("FechaDesdeFilterGloval");
            var fechaHasta = HttpContext.Session.GetString("FechaHastaFilterGloval");
            ViewData["FechaDesdeFilter"] = fechaDesde;
            ViewData["FechaHastaFilter"] = fechaHasta;

            var resultado = (from e in _context.Caja where e.FechaMovimiento >= Convert.ToDateTime(fechaDesde) 
                             && e.FechaMovimiento <= Convert.ToDateTime(fechaHasta)  select e);


           int pageSize = 10000;
             //new ViewAsPdf("Report");
            return View(await Paginacion<Caja>.CreateAsync(resultado, page ?? 1, pageSize));
        }

        // GET: Caja/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                var caja = await _context.Caja
                    .Include(c => c.Alquiler)
                    .FirstOrDefaultAsync(m => m.CajaId == id);

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
                valor = 2;
                return RedirectToAction("Index", "Caja", new { valor });
            }
        }

        //Retiro de dinero
        [HttpPost]
        public async Task<IActionResult> CreateRetire([Bind("CajaId,Debe,Haber,TipoMovimiento,Observaciones,Estado,FechaMovimiento,Total,CuotaID,AlquilerID,Usuario")] Caja caja)
        {
            try
            {
                int valor;
                valor = 1;
                //busca el ultimo id 
                var db = _context.Caja.Include(c => c.Alquiler).Max(c => c.CajaId);
                //busca el valor del total del ultimo id obtenido arriba
                var UltimoValor = _context.Caja.SingleOrDefault(c => c.CajaId == db);
                //Restar valor
                caja.Total = UltimoValor.Total - caja.Haber;
                //Obtener fecha y hora actual
                caja.FechaMovimiento = DateTime.Now;
                if (caja.Total < 0)
                {
                    return RedirectToAction("CreateRetire", "Caja", new { valor });
                }
                else
                {
                    _context.Add(caja);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Caja", new { valor });
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
                var valor = 2;
                return RedirectToAction("Index", "Caja", new { valor });
            }
        }

        //ingreso de dinero
        public IActionResult CreateIngreso(int valor)
        {
            try
            {
                var cajaVacia = (from c in _context.Caja select c).Count();

                if (cajaVacia == 0)
                {
                    ViewData["TotalCaja"] = 0;
                    return View();
                }
                else
                {
                    var ultimoID = _context.Caja.Max(x => x.CajaId);
                    var ultimoPrecio = _context.Caja.SingleOrDefault(x => x.CajaId == ultimoID);
                    ViewData["TotalCaja"] = ultimoPrecio.Total;
                    ViewData["Message"] = valor;

                    return View();
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
                valor = 2;
                return RedirectToAction("Index", "Caja", new { valor });
            }
        }
        //ingreso de dinero
        [HttpPost]
        public async Task<IActionResult> CreateIngreso([Bind("CajaId,Debe,Haber,TipoMovimiento,Observaciones,Estado,FechaMovimiento,Total,CuotaID,AlquilerID,Usuario")] Caja caja)
        {
            try
            {
                var cajaVacia = (from c in _context.Caja select c).Count();

                if (cajaVacia != 0)
                {
                    int valor;

                    valor = 1;
                    //busca el ultimo id 
                    var db = _context.Caja.Include(c => c.Alquiler).Max(c => c.CajaId);
                    //busca el valor del total del ultimo id obtenido arriba
                    var UltimoValor = _context.Caja.SingleOrDefault(c => c.CajaId == db);
                    //sumar valor
                    caja.Total = UltimoValor.Total + caja.Debe;
                    //Obtener fecha y hora actual
                    caja.FechaMovimiento = DateTime.Now;
                    _context.Add(caja);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Caja", new { valor });
                }
                else
                {
                    caja.Total = caja.Debe;
                    caja.FechaMovimiento = DateTime.Now;
                    _context.Add(caja);
                    await _context.SaveChangesAsync();
                    var valor = 1;
                    return RedirectToAction("Index", "Caja", new { valor });
                }
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
