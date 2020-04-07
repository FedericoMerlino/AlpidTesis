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
    public class CuotaPrecioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CuotaPrecioController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CuotaPrecio
        public async Task<IActionResult> Index()
        {
            return View(await _context.CuotaPrecio.ToListAsync());
        }

        // GET: CuotaPrecio/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            try
            {
                var cuotaPrecio = await _context.CuotaPrecio.FirstOrDefaultAsync(m => m.CuotaPrecioID == id);
                return View(cuotaPrecio);
            }
            catch (Exception e)
            {
                Console.Write(e);
                return RedirectToAction("Index", "CuotaPrecio", new { });
            }
        }

        // GET: CuotaPrecio/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CuotaPrecio/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CuotaPrecioID,Importe,FechaDesde,FechaHasta")] CuotaPrecio cuotaPrecio)
        {
            try
            {
                var PrecioVacio = (from c in _context.CuotaPrecio select c).Count();

                if (PrecioVacio == 0)
                {
                    //Crear registro nuevo
                    _context.Add(cuotaPrecio);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    //borra el registro viejo
                    var id = _context.CuotaPrecio.Max(x => x.CuotaPrecioID);
                    var cuotaPrecioViejo = await _context.CuotaPrecio.FindAsync(id);
                    _context.CuotaPrecio.Remove(cuotaPrecioViejo);
                    await _context.SaveChangesAsync();

                    //Crear registro nuevo
                    _context.Add(cuotaPrecio);
                    await _context.SaveChangesAsync();
                    //vuelve a pantalla de cuotas
                }
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
