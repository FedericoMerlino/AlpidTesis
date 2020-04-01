using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Alpid.Data;
using Alpid.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Alpid.Controllers
{
    //[Authorize]
    public class SociosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SociosController(ApplicationDbContext context)
        {
            _context = context;
        }
        public string ActivosGloval = null;
        public string EliminadosGloval = null;

        // GET: Socios
        public async Task<IActionResult> Index(string currentFilter, string searchString, int? page, string filtroFecha,
                                                string DateFilter, int valor)
        {
            try
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
                var socio = from s in _context.Socios select s;

                if (!string.IsNullOrEmpty(searchString))
                {
                    socio = socio.Where(s => s.Cuit.Contains(searchString) || s.RazonSocial.Contains(searchString));
                }

                ViewData["DateFilter"] = filtroFecha;

                if (filtroFecha == null && bandera == true)
                {
                    //activos
                    socio = socio.Where(s => s.FechaBaja == null);
                    HttpContext.Session.SetString("ActivosGloval", "Activos");

                }
                if (filtroFecha != null && bandera == true)
                {
                    //eliminados
                    socio = socio.Where(s => s.FechaBaja != null);
                    HttpContext.Session.SetString("EliminadosGloval", "Eliminados");

                }
                ViewData["Message"] = valor;

               int pageSize = 100;
                return View(await Paginacion<Socios>.CreateAsync(socio.AsNoTracking().OrderByDescending(x => x.FechaAlta), page ?? 1, pageSize));
            }
            catch (Exception e)
            {
                Console.Write(e);
                valor = 2;
                return RedirectToAction("Index", "Socios", new { valor });
            }
        }
        public async Task<IActionResult> Report(int? page)
        {
            var Activos = HttpContext.Session.GetString("ActivosGloval");
            var Eliminados = HttpContext.Session.GetString("EliminadosGloval");
           int pageSize = 100;

            if (Eliminados != null && Eliminados != "vaciarEliminados")
            {
                var resultado = (from e in _context.Socios
                                 where e.FechaBaja != null
                                 select e);
                ViewData["SociosEliminados"] = 1;
                HttpContext.Session.SetString("EliminadosGloval", "vaciarEliminados");

                return View(await Paginacion<Socios>.CreateAsync(resultado, page ?? 1, pageSize));
            }
            if (Activos != null && Activos != "vaciarActivos")
            {
                var resultado = (from e in _context.Socios
                                 where e.FechaBaja == null
                                 select e);
                ViewData["SociosEliminados"] = 0;

                HttpContext.Session.SetString("ActivosGloval", "vaciarActivos");

                return View(await Paginacion<Socios>.CreateAsync(resultado, page ?? 1, pageSize));
            }
           
            var valor = 3;
            return RedirectToAction("Index", "Socios", new { valor });

        }

        // GET: Socios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                var socios = await _context.Socios.FirstOrDefaultAsync(m => m.SociosID == id);
                return View(socios);
            }
            catch (Exception e)
            {
                Console.Write(e);
                var valor = 2;
                return RedirectToAction("Index", "Socios", new { valor });
            }
        }

        // GET: Socios/Create
        public IActionResult Create(int valor, string cuitAlta)
        {
            ViewData["Message"] = valor;
            ViewData["CuitError"] = cuitAlta;

            return View();
        }

        // POST: Socios/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("SociosID,Cuit,RazonSocial,Domicilio,Telefono,Email,FechaAlta")] Socios socios)
        {
            try
            {
                int valor;
                if (ModelState.IsValid)
                {
                    var cuitAlta = socios.Cuit;
                    var Verificar = (from s in _context.Socios where s.Cuit == cuitAlta select s.Cuit).Count();

                    if (Verificar == 0)
                    {
                        _context.Add(socios);
                        await _context.SaveChangesAsync();
                        valor = 1;
                        return RedirectToAction("Index", "Socios", new { valor });
                    }
                    else
                    {
                        valor = 3;
                        return RedirectToAction("Create", "Socios", new { valor, cuitAlta });
                    }
                }
                valor = 2;
                return RedirectToAction("Index", "Socios", new { valor });
            }
            catch (Exception e)
            {
                Console.Write(e);
                var valor = 2;
                return RedirectToAction("Index", "Socios", new { valor });
            }
        }

        // GET: Socios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                var socios = await _context.Socios.FindAsync(id);
                return View(socios);
            }
            catch (Exception e)
            {
                Console.Write(e);
                var valor = 2;
                return RedirectToAction("Index", "Socios", new { valor });
            }
        }

        // POST: Socios/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("SociosID,Cuit,RazonSocial,Domicilio,Telefono,Email,FechaAlta")] Socios socios)
        {
            try
            {
                _context.Update(socios);
                await _context.SaveChangesAsync();
                var valor = 1;
                return RedirectToAction("Index", "Socios", new { valor });
            }
            catch (Exception e)
            {
                Console.Write(e);
                var valor = 2;
                return RedirectToAction("Index", "Socios", new { valor });
            }
        }
        // GET: Socios/Edit/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                var socios = await _context.Socios.FindAsync(id);
                return View(socios);
            }
            catch (Exception e)
            {
                Console.Write(e);
                var valor = 2;
                return RedirectToAction("Index", "Socios", new { valor });
            }
        }

        // POST: Socios/Edit/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int id, [Bind("SociosID,Cuit,RazonSocial,Domicilio,Telefono,Email,FechaBaja,FechaAlta,MotivoBaja")] Socios socios)
        {
            try
            {
                _context.Update(socios);
                await _context.SaveChangesAsync();
                var valor = 1;
                return RedirectToAction("Index", "Socios", new { valor });
            }
            catch (Exception e)
            {
                Console.Write(e);
                var valor = 2;
                return RedirectToAction("Index", "Socios", new { valor });
            }
        }

        public async Task<IActionResult> Active(int? id)
        {
            try
            {
                var socios = await _context.Socios.FindAsync(id);
                return View(socios);
            }
            catch (Exception e)
            {
                Console.Write(e);
                var valor = 2;
                return RedirectToAction("Index", "Socios", new { valor });
            }
        }

        // POST: Socios/Edit/5
        [HttpPost, ActionName("Active")]
        public async Task<IActionResult> Active(int id, [Bind("SociosID,Cuit,RazonSocial,Domicilio,Telefono,Email,FechaBaja,FechaAlta,MotivoBaja")] Socios socios)
        {
            try
            {
                _context.Update(socios);
                await _context.SaveChangesAsync();
                var valor = 1;
                return RedirectToAction("Index", "Socios", new { valor });
            }
            catch (Exception e)
            {
                Console.Write(e);
                var valor = 2;
                return RedirectToAction("Index", "Socios", new { valor });
            }
        }
    }
}
