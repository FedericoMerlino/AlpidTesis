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
    public class ProductosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Productos
        public async Task<IActionResult> Index(string currentFilter, string searchString, int? page,
                                               string filtroFecha, string DateFilter, int valor)
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
            var producto = from s in _context.Productos select s;

            if (!string.IsNullOrEmpty(searchString))
            {
                producto = producto.Where(s => s.Nombre.Contains(searchString));
            }

            ViewData["DateFilter"] = filtroFecha;

            if (filtroFecha == null && bandera == true)
            {
                producto = producto.Where(s => s.FechaBaja == null);
            }
            if (filtroFecha != null && bandera == true)
            {
                producto = producto.Where(s => s.FechaBaja != null);
            }
            ViewData["Message"] = valor;

            int pageSize = 15;
            return View(await Paginacion<Productos>.CreateAsync(producto.AsNoTracking().OrderByDescending(x => x.FechaAlta).Include(x => x.Proveedores), page ?? 1, pageSize));
        }

        public async Task<IActionResult> Report(int? page)
        {
            int pageSize = 15;

            var resultado = (from e in _context.Productos
                             where e.FechaBaja == null && e.ProductosTipo == "DeAlquiler"
                             select e) ;

                return View(await Paginacion<Productos>.CreateAsync(resultado.Include(x => x.Proveedores), page ?? 1, pageSize));
        }

        public async Task<IActionResult> ReportMobiliarios(int? page)
        {
            int pageSize = 15;

            var resultado = (from e in _context.Productos
                             where e.FechaBaja == null && e.ProductosTipo != "DeAlquiler"
                             select e);

            return View(await Paginacion<Productos>.CreateAsync(resultado, page ?? 1, pageSize));
        }
        // GET: Productos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                var productos = await _context.Productos.Include(p => p.Proveedores).FirstOrDefaultAsync(m => m.ProductosID == id);
                return View(productos);
            }
            catch (Exception e)
            {
                Console.Write(e);
                var valor = 2;
                return RedirectToAction("Index", "Productos", new { valor });
            }
        }

        // GET: Productos/Create
        public IActionResult Create(int valor, string NombreAlta)
        {
            try
            {
                ViewData["Message"] = valor;
                ViewData["NombreError"] = NombreAlta;

                //Busca los proveedores que no esten dados de baja
                var proveedor = from s in _context.Proveedores select s;
                proveedor = proveedor.Where(s => s.FechaBaja == null);
                ViewData["ProveedoresID"] = new SelectList(proveedor, "ProveedoresId", "RazonSocial");
                return View();
            }
            catch (Exception e)
            {
                Console.Write(e);
                valor = 2;
                return RedirectToAction("Index", "Productos", new { valor });
            }
        }

        // POST: Productos/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProductosID,Nombre,Cantidad,ProductosTipo,FechaAlta,ProveedoresID")] Productos productos)
        {
            try
            {
                int valor;
                var NombreAlta = productos.Nombre;
                var Verificar = (from s in _context.Productos where s.Nombre == NombreAlta select s.Nombre).Count();

                if (Verificar == 0)
                {
                    _context.Add(productos);
                    await _context.SaveChangesAsync();

                    ViewData["ProveedoresID"] = new SelectList(_context.Proveedores, "ProveedoresId", "RazonSocial", productos.ProveedoresID);
                     valor = 1;
                    return RedirectToAction("Index", "Productos", new { valor });
                }
                else
                {
                    valor = 3;
                    return RedirectToAction("Create", "Productos", new { valor, NombreAlta });
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
                var valor = 2;
                return RedirectToAction("Index", "Productos", new { valor });
            }
        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                var productos = await _context.Productos.FindAsync(id);
                ViewData["ProveedoresID"] = new SelectList(_context.Proveedores, "ProveedoresId", "RazonSocial", productos.ProveedoresID);
                return View(productos);
            }
            catch (Exception e)
            {
                Console.Write(e);
                var valor = 2;
                return RedirectToAction("Index", "Productos", new { valor });
            }
        }

        // POST: Productos/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("ProductosID,Nombre,Cantidad,ProductosTipo,FechaAlta,PrecioAlquiler,ProveedoresID")] Productos productos)
        {
            try
            {
                _context.Update(productos);
                await _context.SaveChangesAsync();
                ViewData["ProveedoresID"] = new SelectList(_context.Proveedores, "ProveedoresId", "RazonSocial", productos.ProveedoresID);

                var valor = 1;
                return RedirectToAction("Index", "Productos", new { valor });
            }
            catch (Exception e)
            {
                Console.Write(e);
                var valor = 2;
                return RedirectToAction("Index", "Productos", new { valor });
            }
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                var productos = await _context.Productos.FindAsync(id);

                ViewData["ProveedoresID"] = new SelectList(_context.Proveedores, "ProveedoresId", "RazonSocial", productos.ProveedoresID);
                return View(productos);
            }
            catch (Exception e)
            {
                Console.Write(e);
                var valor = 2;
                return RedirectToAction("Index", "Productos", new { valor });
            }
        }

        //// POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int id, [Bind("ProductosID,Nombre,Cantidad,ProductosTipo,FechaAlta,PrecioAlquiler,ProveedoresID,FechaBaja,MotivoBaja")] Productos productos)
        {
            try
            {
                productos.FechaBaja = DateTime.Now;

                _context.Update(productos);
                await _context.SaveChangesAsync();
                ViewData["ProveedoresID"] = new SelectList(_context.Proveedores, "ProveedoresId", "RazonSocial", productos.ProveedoresID);

                var valor = 1;
                return RedirectToAction("Index", "Productos", new { valor });
            }
            catch (Exception e)
            {
                Console.Write(e);
                var valor = 2;
                return RedirectToAction("Index", "Productos", new { valor });
            }
        }

        // GET: Productos/Active/5
        public async Task<IActionResult> Active(int? id)
        {
            try
            {
                var productos = await _context.Productos.FindAsync(id);

                ViewData["ProveedoresID"] = new SelectList(_context.Proveedores, "ProveedoresId", "RazonSocial", productos.ProveedoresID);
                return View(productos);
            }
            catch (Exception e)
            {
                Console.Write(e);
                var valor = 2;
                return RedirectToAction("Index", "Productos", new { valor });
            }
        }

        //// POST: Productos/Active/5
        public async Task<IActionResult> Active(int id, [Bind("ProductosID,Nombre,Cantidad,ProductosTipo,FechaAlta,PrecioAlquiler,ProveedoresID,FechaBaja,MotivoBaja")] Productos productos)
        {
            try
            {
                _context.Update(productos);
                await _context.SaveChangesAsync();
                ViewData["ProveedoresID"] = new SelectList(_context.Proveedores, "ProveedoresId", "RazonSocial", productos.ProveedoresID);

                var valor = 1;
                return RedirectToAction("Index", "Productos", new { valor });
            }
            catch (Exception e)
            {
                Console.Write(e);
                var valor = 2;
                return RedirectToAction("Index", "Productos", new { valor });
            }
        }

        // GET: Productos/DeletePhysical/5
        public async Task<IActionResult> DeletePhysical(int id)
        {
            try
            {
                var productos = await _context.Productos
                    .Include(p => p.Proveedores)
                    .FirstOrDefaultAsync(m => m.ProductosID == id);

                return View(productos);
            }
            catch (Exception e)
            {
                Console.Write(e);
                var valor = 2;
                return RedirectToAction("Index", "Productos", new { valor });
            }
        }

        // POST: Productos/DeletePhysical/5
        [HttpPost, ActionName("DeletePhysical")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var productos = await _context.Productos.FindAsync(id);
                _context.Productos.Remove(productos);
                await _context.SaveChangesAsync();
                var valor = 1;
                return RedirectToAction("Index", "Productos", new { valor });
            }
            catch (Exception e)
            {
                Console.Write(e);
                var valor = 2;
                return RedirectToAction("Index", "Productos", new { valor });
            }
        }
    }
}
