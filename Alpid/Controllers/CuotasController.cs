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

namespace Alpid.Controllers
{
    [Authorize]
    public class CuotasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CuotasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cuotas
        public async Task<IActionResult> Index(string currentFilter, string searchString, int? page, string filtroFecha,
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
            }
            //else
            //{
            //     cuota = (from s in _context.Cuotas group s by s.SociosID into g select g).ToArrayAsync();
            //}

            int pageSize = 15;
            ViewData["Message"] = valor;

            return View(await Paginacion<Cuotas>.CreateAsync(cuota.AsNoTracking().Include(c => c.Socios).OrderByDescending(x => x.FechaHasta), page ?? 1, pageSize));
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
