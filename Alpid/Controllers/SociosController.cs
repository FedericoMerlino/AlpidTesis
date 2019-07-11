using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Alpid.Data;
using Alpid.Models;

namespace Alpid.Controllers
{
    public class SociosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SociosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Socios
        public async Task<IActionResult> Index(string currentFilter, string searchString, int? page, string filtroFecha,string DateFilter)
        {
            if (searchString != null)
            {
                page = 1;
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

            if (filtroFecha == null)
            {
                socio = socio.Where(s => s.FechaBaja == null);
            }
            else
            {
                socio = socio.Where(s => s.FechaBaja != null);
            }

            int pageSize = 15;
            return View(await Paginacion<Socios>.CreateAsync(socio.AsNoTracking().OrderByDescending(x => x.FechaAlta), page ?? 1, pageSize));
        }

        // GET: Socios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var socios = await _context.Socios
                .FirstOrDefaultAsync(m => m.SociosID == id);
            if (socios == null)
            {
                return NotFound();
            }

            return View(socios);
        }

        // GET: Socios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Socios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SociosID,Cuit,RazonSocial,Domicilio,Telefono,Email,FechaAlta")] Socios socios)
        {
            if (ModelState.IsValid)
            {
                _context.Add(socios);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(socios);
        }

        // GET: Socios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var socios = await _context.Socios.FindAsync(id);
            if (socios == null)
            {
                return NotFound();
            }
            return View(socios);
        }

        // POST: Socios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SociosID,Cuit,RazonSocial,Domicilio,Telefono,Email,FechaAlta")] Socios socios)
        {
            if (id != socios.SociosID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(socios);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SociosExists(socios.SociosID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(socios);
        }

        // GET: Socios/Edit/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var socios = await _context.Socios.FindAsync(id);
            if (socios == null)
            {
                return NotFound();
            }
            return View(socios);
        }

        // POST: Socios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, [Bind("SociosID,Cuit,RazonSocial,Domicilio,Telefono,Email,FechaBaja,FechaAlta,MotivoBaja")] Socios socios)
        {
            if (id != socios.SociosID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(socios);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SociosExists(socios.SociosID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(socios);
        }

        public async Task<IActionResult> Active(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var socios = await _context.Socios.FindAsync(id);
            if (socios == null)
            {
                return NotFound();
            }
            return View(socios);
        }

        // POST: Socios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        [HttpPost, ActionName("Active")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Active(int id, [Bind("SociosID,Cuit,RazonSocial,Domicilio,Telefono,Email,FechaBaja,FechaAlta,MotivoBaja")] Socios socios)
        {
            if (id != socios.SociosID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(socios);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SociosExists(socios.SociosID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(socios);
        }

        // baja fisica
        //// GET: Socios/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var socios = await _context.Socios.FindAsync(id);
        //    if (socios == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(socios);
        //}

        //// POST: Socios/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id, [Bind("SociosID,Cuit,RazonSocial,Domicilio,Telefono,Email,FechaBaja,FechaAlta,MotivoBaja")] Socios socios)
        //{
        //    if (id != socios.SociosID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(socios);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!SociosExists(socios.SociosID))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(socios);
        //}

        private bool SociosExists(int id)
        {
            return _context.Socios.Any(e => e.SociosID == id);
        }
    }
}
