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
                                                string DateFilter)
        {

            var ultimoid = _context.CuotaPrecio.Max(c => c.CuotaPrecioID);
            var valor = _context.CuotaPrecio.SingleOrDefault(c => c.CuotaPrecioID == ultimoid);
            ViewData["precio"] = valor.Importe;

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
            int pageSize = 15;
            return View(await Paginacion<Cuotas>.CreateAsync(cuota.AsNoTracking().Include(c => c.Socios), page ?? 1, pageSize));
        }

        // GET: Cuotas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuotas = await _context.Cuotas
                .Include(c => c.Socios)
                .FirstOrDefaultAsync(m => m.CuotasID == id);
            if (cuotas == null)
            {
                return NotFound();
            }

            return View(cuotas);
        }

        // GET: Cuotas/Create
        public IActionResult Create()
        {
            var ultimoid = _context.CuotaPrecio.Max(c => c.CuotaPrecioID);
            var valor = _context.CuotaPrecio.SingleOrDefault(c => c.CuotaPrecioID == ultimoid);
            ViewData["precio"] = valor.Importe;

            ViewData["SociosID"] = new SelectList(_context.Set<Socios>(), "SociosID", "RazonSocial");
            return View();
        }

        // POST: Cuotas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CuotasID,Estado,Observacion,Importe,FechaDesde,FechaHasta,SociosID")] Cuotas cuotas)
        {
            if (ModelState.IsValid)
            {
                var importe = _context.Caja.Select(c => c.CajaId);
                
                _context.Add(cuotas);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("PagoCuota", "Caja", new { ID = cuotas.CuotasID, debe = cuotas.Importe});
        }
    }
}
