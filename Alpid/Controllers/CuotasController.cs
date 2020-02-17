using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Alpid.Data;
using Alpid.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Alpid.Controllers
{
    //[Authorize]
    public class CuotasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CuotasController(ApplicationDbContext context)
        {
            _context = context;
        }
        public string FechaDesdeFilterGloval = null;
        public string FechaHastaFilterGloval = null;
        public string SocioFilterGloval = null;

        // GET: Cuotas
        public async Task<IActionResult> Index(DateTime fechaDesde, DateTime fechaHasta, string currentFilter, string searchString, int? page, string filtroFecha,
                                                string DateFilter, int valor)
        {
            var ValorParaCuota = (from s in _context.CuotaPrecio select s).Count();

            if (ValorParaCuota == 0)
            {
                ViewData["precio"] = "Debe cargar un valor";
            }
            else
            {
                var ultimoid = _context.CuotaPrecio.Max(c => c.CuotaPrecioID);
                var UltimoValor = _context.CuotaPrecio.SingleOrDefault(c => c.CuotaPrecioID == ultimoid);
                ViewData["precio"] = UltimoValor.Importe;
            }
            //return View(await applicationDbContext.ToListAsync());

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;
            var cuota = from s in _context.Cuotas select s;

            if (!string.IsNullOrEmpty(searchString))
            {
                cuota = cuota.Where(s => s.Socios.RazonSocial.Contains(searchString));
                HttpContext.Session.SetString("SocioFilterGloval", (from a in _context.Alquiler
                                                                    where a.Socios.RazonSocial.Contains(searchString)
                                                                     || a.Socios.Cuit.Contains(searchString)
                                                                    select a.SociosID).FirstOrDefault().ToString());
            }

            string mesDesde;
            string mesHasta;

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
            ViewData["FechaDesdeFilter"] = fechaDesde.Year + mesDesde + "-" + fechaDesde.Day;
            ViewData["FechaHastaFilter"] = fechaHasta.Year + mesHasta + "-" + fechaHasta.Day;

            HttpContext.Session.SetString("FechaDesdeFilterGloval", fechaDesde.ToShortDateString());
            HttpContext.Session.SetString("FechaHastaFilterGloval", fechaHasta.ToShortDateString());

            cuota = cuota.Where(s => Convert.ToDateTime(Convert.ToDateTime(s.FechaDesde).ToShortDateString()) >= Convert.ToDateTime(fechaDesde.ToShortDateString()));
            cuota = cuota.Where(s => Convert.ToDateTime(s.FechaHasta.ToShortDateString()) <= Convert.ToDateTime(fechaHasta.ToShortDateString()));

            int pageSize = 15;
            ViewData["Message"] = valor;

            return View(await Paginacion<Cuotas>.CreateAsync(cuota.AsNoTracking().Include(c => c.Socios).OrderByDescending(x => x.FechaHasta), page ?? 1, pageSize));
        }
        public async Task<IActionResult> Report(int? page)
        {
            var FechaDesde = HttpContext.Session.GetString("FechaDesdeFilterGloval");
            var FechaHasta = HttpContext.Session.GetString("FechaHastaFilterGloval");
            var SocioID = HttpContext.Session.GetString("SocioFilterGloval");
            ViewData["SocioFilter"] = 0;
            ViewData["FechaDesdeFilter"] = null;
            ViewData["FechaHastaFilter"] = null;

            int pageSize = 15;
            if (SocioID != null && SocioID != "vaciarActivos")
            {
                var resultado = (from e in _context.Cuotas.Include(x => x.Socios)
                                 where e.SociosID == Convert.ToInt32(SocioID)
                                 orderby e.FechaHasta, e.FechaDesde descending
                                 select e);
                ViewData["SocioFilter"] = (from e in _context.Socios where e.SociosID == Convert.ToInt32(SocioID) select e.RazonSocial).FirstOrDefault();

                HttpContext.Session.SetString("SocioID", "vaciarActivos");

                return View(await Paginacion<Cuotas>.CreateAsync(resultado, page ?? 1, pageSize));
            }
            if (FechaDesde != null && FechaDesde != "vaciarEliminados")
            {
                var resultado = (from e in _context.Cuotas
                                 where e.FechaDesde > Convert.ToDateTime(FechaDesde)
                                 select e.SociosID).ToList();
                resultado = (from e in _context.Cuotas.Include(x => x.Socios)
                             where e.FechaDesde < Convert.ToDateTime(FechaHasta)
                             select e.SociosID).ToList();

                //var resultadoFinal = (from e in _context.Socios
                //                      where resultado.Exists(e.SociosID)
                //                      select e);

                ViewData["FechaDesdeFilter"] = FechaDesde;
                ViewData["FechaHastaFilter"] = FechaHasta;

                HttpContext.Session.SetString("FechaDesde", "vaciarEliminados");

                return View(await Paginacion<Cuotas>.CreateAsync(resultado, page ?? 1, pageSize));
            }

            var valor = 3;
            return RedirectToAction("Index", "Socios", new { valor });
        }

        // GET: Cuotas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                var cuotas = await _context.Cuotas.Include(c => c.Socios)
                                                  .FirstOrDefaultAsync(m => m.CuotasID == id);
                return View(cuotas);
            }
            catch (Exception e)
            {
                Console.Write(e);
                var valor = 2;
                return RedirectToAction("Index", "Socios", new { valor });
            }
        }

        // GET: Cuotas/Create
        public IActionResult Create(int FechaValidacion, DateTime FechaHasta)
        {
            try
            {
                var ultimoid = _context.CuotaPrecio.Max(c => c.CuotaPrecioID);
                var valor = _context.CuotaPrecio.SingleOrDefault(c => c.CuotaPrecioID == ultimoid);
                ViewData["precio"] = valor.Importe;

                ViewData["FechaValidacion"] = FechaValidacion;
                ViewData["FechaHasta"] = FechaHasta;

                ViewData["SociosID"] = new SelectList(_context.Set<Socios>(), "SociosID", "RazonSocial");
                return View();
            }
            catch (Exception e)
            {
                Console.Write(e);
                var valor = 2;
                return RedirectToAction("Index", "Socios", new { valor });
            }
        }

        // POST: Cuotas/Create
        [HttpPost]
        public async Task<IActionResult> Create(string Estado, string Observacion, decimal Importe, DateTime FechaDesde, DateTime FechaHasta, int SociosID)
        {
            try
            {
                var add = new Cuotas();

                add.Estado = Estado;
                add.FechaDesde = FechaDesde;
                add.FechaHasta = FechaHasta;
                add.SociosID = SociosID;
                add.Observacion = Observacion;

                Importe = (from s in _context.CuotaPrecio select s.Importe).FirstOrDefault();

                add.Importe = Importe;

                var PrimerValor = (from c in _context.Cuotas where c.SociosID == SociosID select c).Count();
                if (PrimerValor != 0)
                {
                    var obtenerUltimaFechaSocio = (from c in _context.Cuotas where c.SociosID == SociosID select c.FechaHasta).Max();

                    if (obtenerUltimaFechaSocio >= FechaDesde)
                    {
                        var FechaValidacion = 4;
                        return RedirectToAction("Create", "Cuotas", new { FechaValidacion, FechaHasta });
                    }
                }


                _context.Add(add);
                await _context.SaveChangesAsync();
                var valor = 1;
                return RedirectToAction("Index", "Cuotas", new { valor });
            }
            catch (Exception e)
            {
                Console.Write(e);
                var valor = 2;
                return RedirectToAction("Index", "Cuotas", new { valor });
            }
        }
    }
}
