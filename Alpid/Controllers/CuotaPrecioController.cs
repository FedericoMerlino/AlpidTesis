using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Alpid.Data;
using Alpid.Models;

namespace Alpid.Controllers
{
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
            if (id == null)
            {
                return NotFound();
            }

            var cuotaPrecio = await _context.CuotaPrecio
                .FirstOrDefaultAsync(m => m.CuotaPrecioID == id);
            if (cuotaPrecio == null)
            {
                return NotFound();
            }

            return View(cuotaPrecio);
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
            if (ModelState.IsValid)
            {
                //borra el registro viejo
                var id = _context.CuotaPrecio.Max(x => x.CuotaPrecioID);
                var cuotaPrecioViejo = await _context.CuotaPrecio.FindAsync(id);
                _context.CuotaPrecio.Remove(cuotaPrecioViejo);
                await _context.SaveChangesAsync();

                //Crear registro nuevo
                _context.Add(cuotaPrecio);
                await _context.SaveChangesAsync();
            }
            //vuelve a pantalla de cuotas
            return RedirectToAction("Index", "Cuotas", new { FileUploadMsg = "File   uploaded successfully" });
        }
    }
}
