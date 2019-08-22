using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Alpid.Data;
using Alpid.Models;
using Microsoft.AspNetCore.Authorization;

namespace Alpid.Controllers
{
    //[Authorize]
    public class ProveedoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProveedoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Proveedores
        public async Task<IActionResult> Index(string currentFilter, string searchString,
                                        int? page, string filtroFecha, string DateFilter, int valor)
        {
            var bandera = true;

            if (searchString != null)
            {
                page = 1;
                bandera = false;

            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;
            var proveedor = from s in _context.Proveedores select s;

            if (!string.IsNullOrEmpty(searchString))
            {
                proveedor = proveedor.Where(s => s.Cuit.Contains(searchString) || s.RazonSocial.Contains(searchString));
            }

            ViewData["DateFilter"] = filtroFecha;

            if (filtroFecha == null && bandera == true)
            {
                proveedor = proveedor.Where(s => s.FechaBaja == null);
            }
            if (filtroFecha != null && bandera == true)
            {
                proveedor = proveedor.Where(s => s.FechaBaja != null);
            }
            ViewData["Message"] = valor;

            int pageSize = 15;
            return View(await Paginacion<Proveedores>.CreateAsync(proveedor.AsNoTracking().OrderByDescending(x => x.FechaAlta), page ?? 1, pageSize));
        }

        // GET: Proveedores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                var proveedores = await _context.Proveedores
                .FirstOrDefaultAsync(m => m.ProveedoresId == id);
                if (proveedores == null)
                {
                    return NotFound();
                }
                return View(proveedores);
            }
            catch (Exception e)
            {
                Console.Write(e);
                var PAsarvalor = 2;
                return RedirectToAction("Index", "Proveedores", new { PAsarvalor });
            }
        }

        // GET: Proveedores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Proveedores/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProveedoresId,Cuit,RazonSocial,Domicilio,Telefono,Mail,FechaAlta")] Proveedores proveedores)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(proveedores);
                    await _context.SaveChangesAsync();

                    var valor = 1;
                    return RedirectToAction("Index", "Proveedores", new { valor });
                }
                var Pasarvalor = 2;
                return RedirectToAction("Index", "Proveedores", new { Pasarvalor });
            }
            catch (Exception e)
            {
                Console.Write(e);
                var PAsarvalor = 2;
                return RedirectToAction("Index", "Proveedores", new { PAsarvalor });
            }
        }
        // GET: Proveedores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                var proveedores = await _context.Proveedores.FindAsync(id);
                if (proveedores == null)
                {
                    return NotFound();
                }
                return View(proveedores);
            }
            catch (Exception e)
            {
                Console.Write(e);
                var PAsarvalor = 2;
                return RedirectToAction("Index", "Proveedores", new
                {
                    PAsarvalor
                });
            }
        }

        // POST: Proveedores/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("ProveedoresId,Cuit,RazonSocial,Domicilio,Telefono,Mail,FechaBaja,FechaAlta,MotivoBaja")] Proveedores proveedores)
        {
            try
            {
                _context.Update(proveedores);
                await _context.SaveChangesAsync();

                var valor = 1;
                return RedirectToAction("Index", "Proveedores", new { valor });
            }
            catch (Exception e)
            {
                Console.Write(e);
                var PAsarvalor = 2;
                return RedirectToAction("Index", "Proveedores", new { PAsarvalor });
            }
        }

        // GET: Proveedores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                var proveedores = await _context.Proveedores.FindAsync(id);
                if (proveedores == null)
                {
                    return NotFound();
                }
                return View(proveedores);
            }
            catch (Exception e)
            {
                Console.Write(e);
                var PAsarvalor = 2;
                return RedirectToAction("Index", "Proveedores", new { PAsarvalor });
            }
        }

        // POST: Proveedores/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int id, [Bind("ProveedoresId,Cuit,RazonSocial,Domicilio,Telefono,Mail,FechaBaja,FechaAlta,MotivoBaja")] Proveedores proveedores)
        {
            try
            {
                _context.Update(proveedores);
                await _context.SaveChangesAsync();

                var valor = 1;
                return RedirectToAction("Index", "Proveedores", new { valor });
            }
            catch (Exception e)
            {
                Console.Write(e);
                var PAsarvalor = 2;
                return RedirectToAction("Index", "Proveedores", new { PAsarvalor });
            }
        }

        // GET: Proveedores/Active/5
        public async Task<IActionResult> Active(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proveedores = await _context.Proveedores.FindAsync(id);
            if (proveedores == null)
            {
                return NotFound();
            }
            return View(proveedores);
        }

        // POST: Proveedores/Active/5
        [HttpPost]
        public async Task<IActionResult> Active(int id, [Bind("ProveedoresId,Cuit,RazonSocial,Domicilio,Telefono,Mail,FechaBaja,FechaAlta,MotivoBaja")] Proveedores proveedores)
        {
            try
            {
                _context.Update(proveedores);
                await _context.SaveChangesAsync();

                var valor = 1;
                return RedirectToAction("Index", "Proveedores", new { valor });
            }
            catch (Exception e)
            {
                Console.Write(e);
                var PAsarvalor = 2;
                return RedirectToAction("Index", "Proveedores", new { PAsarvalor });
            }
        }

        private bool ProveedoresExists(int id)
        {
            return _context.Proveedores.Any(e => e.ProveedoresId == id);
        }
    }
}
