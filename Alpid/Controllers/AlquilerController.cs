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
    //[Authorize]
    public class AlquilerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlquilerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Alquiler
        public async Task<IActionResult> Index(int? page)
        {
            var applicationDbContext = _context.Alquiler.Include(a => a.Socios);

            //var evento = from e in _context.Alquiler group e by e.AlquilerID into g select g;

            var evento = (from e in _context.Alquiler select e);


            int pageSize = 15;
            return View(await Paginacion<Alquiler>.CreateAsync(evento.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: Alquiler/Details/5
        public async Task<IActionResult> Details(int? id, int? page)
        {
            try
            {
                var DetalleAlquiler = await _context.Alquiler.FirstOrDefaultAsync(m => m.AlquilerID == id);

                ViewData["FechaDesde"] = DetalleAlquiler.FechaDesde.Year + "-" + DetalleAlquiler.FechaDesde.Month + "-" + DetalleAlquiler.FechaDesde.Day;
                ViewData["FechaHasta"] = DetalleAlquiler.FechaHasta.Year + "-" + DetalleAlquiler.FechaHasta.Month + "-" + DetalleAlquiler.FechaHasta.Day;
                ViewData["Observacion"] = DetalleAlquiler.Observacion;

                var socio = from s in _context.Socios select s;
                socio = socio.Where(s => s.SociosID == DetalleAlquiler.SociosID);
                ViewData["SociosId"] = new SelectList(socio, "SociosID", "RazonSocial");
                //obtiene valor para mostran en pantalla
                var alquiler = (from e in _context.Alquiler where e.AlquilerID == id select e);

                ViewData["ProductosID"] = new SelectList(alquiler, "ProductosID", "Nombre");

                int pageSize = 15;
                return View(await Paginacion<Alquiler>.CreateAsync(alquiler.AsNoTracking(), page ?? 1, pageSize));

            }
            catch (Exception e)
            {
                Console.Write(e);
                var valor = 2;
                return RedirectToAction("Index", "EventoSolidarios", new { valor });
            }
        }

        // GET: Alquiler/Create
        public async Task<IActionResult> Create(DateTime FechaDesde, DateTime FechaHasta, string Observacion, int SociosID,
                                                int IdAlquiler, int DeshabilitarCampos)
        {
            try
            {
                //busca los productos que no esten dados de baja y solo sean de alquiler
                var producto = from s in _context.Productos select s;
                producto = producto.Where(s => s.FechaBaja == null && s.ProductosTipo == "DeAlquiler");
                ViewData["ProductosID"] = new SelectList(producto, "ProductosID", "Nombre");

                if (DeshabilitarCampos == 1)
                {
                    //Busca el socio que se cargo anteriormente
                    var socio = from s in _context.Socios select s;
                    socio = socio.Where(s => s.SociosID == SociosID);
                    ViewData["SociosId"] = new SelectList(socio, "SociosID", "RazonSocial");
                }
                else
                {
                    //Busca los socios que no esten dados de baja
                    var socio = from s in _context.Socios select s;
                    socio = socio.Where(s => s.FechaBaja == null);
                    ViewData["SociosId"] = new SelectList(socio, "SociosID", "RazonSocial");
                }
                ViewData["FechaDesde"] = FechaDesde.Year + "-" + FechaDesde.Month + "-" + FechaDesde.Day;
                ViewData["FechaHasta"] = FechaHasta.Year + "-" + FechaHasta.Month + "-" + FechaHasta.Day;
                ViewData["Observacion"] = Observacion;
                ViewData["DeshabilitarCampos"] = DeshabilitarCampos;
                ViewData["IdAlquiler"] = IdAlquiler;

                return View(await _context.Alquiler.ToListAsync());
            }
            catch (Exception e)
            {
                Console.Write(e);
                return RedirectToAction("Index", "Alquiler");
            }
        }

        // POST: Alquiler/Create
        [HttpPost]
        public async Task<IActionResult> Create(DateTime FechaDesde, DateTime FechaHasta, string Observacion, int SociosID, int ProductosID,
                                                int cantidad, decimal Valor, int AlquilerID)
        {
            try
            {
                var Add = new Alquiler();

                var Base = (from c in _context.Alquiler select c).Count();

                if (Base == 0)
                {
                    Add.AlquilerID = 1;
                    AlquilerID = 1;
                }
                else
                {
                    if (AlquilerID == 0)
                    {
                        var UltimoIdbase = (from c in _context.Alquiler select c.AlquilerID).Max();
                        var valoresBase = _context.Alquiler.SingleOrDefault(x => x.AlquilerID == UltimoIdbase);

                        Add.AlquilerID = valoresBase.AlquilerID + 1;
                    }
                    else
                    {
                        Add.AlquilerID = AlquilerID;
                    }
                }
                Add.FechaDesde = FechaDesde;
                Add.FechaHasta = FechaHasta;
                Add.Observacion = Observacion;
                Add.SociosID = SociosID;
                Add.cantidad = cantidad;
                Add.ProductosID = ProductosID;
                Add.Valor = Valor;

                var DeshabilitarCampos = 1;

                _context.Add(Add);
                await _context.SaveChangesAsync();

                return RedirectToAction("Create", "Alquiler", new { FechaDesde, FechaHasta, Observacion, SociosID, DeshabilitarCampos, AlquilerID });
            }
            catch (Exception e)
            {
                Console.Write(e);
                return RedirectToAction("Index", "Alquiler");
            }
        }

        // GET: Alquiler/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                var alquiler = await _context.Alquiler.FindAsync(id);

                ViewData["SociosId"] = new SelectList(_context.Socios, "SociosID", "RazonSocial", alquiler.SociosID);
                return View(alquiler);
            }
            catch (Exception e)
            {
                Console.Write(e);
                return RedirectToAction("Index", "Alquiler");
            }
        }

        // POST: Alquiler/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("AlquilerID,Fecha,Observacion,Valor,ProductosID,SociosId")] Alquiler alquiler)
        {

            try
            {
                _context.Update(alquiler);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.Write(e);
                return RedirectToAction("Index", "Alquiler");
            }

            ViewData["SociosId"] = new SelectList(_context.Socios, "SociosID", "RazonSocial", alquiler.SociosID);
            return View(alquiler);
        }

        // GET: Alquiler/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alquiler = await _context.Alquiler
                .Include(a => a.Socios)
                .FirstOrDefaultAsync(m => m.AlquilerID == id);
            if (alquiler == null)
            {
                return NotFound();
            }

            return View(alquiler);
        }

        // POST: Alquiler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var alquiler = await _context.Alquiler.FindAsync(id);
            _context.Alquiler.Remove(alquiler);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlquilerExists(int id)
        {
            return _context.Alquiler.Any(e => e.AlquilerID == id);
        }
    }
}
