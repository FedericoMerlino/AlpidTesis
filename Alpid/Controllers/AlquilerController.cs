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


            var alquiler = (from e in _context.Alquiler
                            orderby e.AlquilerID
                            select new Alquiler
                            {
                                AlquilerID = e.AlquilerID,
                                FechaDesde = e.FechaDesde,
                                FechaHasta = e.FechaHasta,
                                Socios = e.Socios,
                                Observacion = e.Observacion
                            }).Distinct();


            int pageSize = 15;
            return View(await Paginacion<Alquiler>.CreateAsync(alquiler.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: Alquiler/Details/5
        public async Task<IActionResult> Details(int? id, int? page)
        {
            try
            {
                var DetalleAlquiler = await _context.Alquiler.FirstOrDefaultAsync(m => m.AlquilerID == id);

                ViewData["FechaDesde"] = DetalleAlquiler.FechaDesde.ToShortDateString();
                ViewData["FechaHasta"] = DetalleAlquiler.FechaHasta.ToShortDateString();
                ViewData["Observacion"] = DetalleAlquiler.Observacion;

                var socio = from s in _context.Socios select s;
                socio = socio.Where(s => s.SociosID == DetalleAlquiler.SociosID);
                ViewData["SociosId"] = new SelectList(socio, "SociosID", "RazonSocial");
                //obtiene valor para mostran en pantalla
                var alquiler = (from e in _context.Alquiler where e.AlquilerID == id select e);

                ViewData["ProductosID"] = new SelectList(alquiler, "ProductosID", "Nombre");

                int pageSize = 15;
                return View(await Paginacion<Alquiler>.CreateAsync(alquiler.Include(a => a.Productos).AsNoTracking(), page ?? 1, pageSize));

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
                                                int ID, int DeshabilitarCampos, string mensaje, int valorMensaje)
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
                ViewData["FechaDesde"] = FechaDesde.ToShortDateString();
                ViewData["FechaHasta"] = FechaHasta.ToShortDateString();
                ViewData["Observacion"] = Observacion;
                ViewData["DeshabilitarCampos"] = DeshabilitarCampos;
                ViewData["IdAlquiler"] = ID;
                ViewData["errorCantProductos"] = mensaje;
                ViewData["Message"] = valorMensaje;

                return View(await _context.Alquiler.Include(a => a.Productos).ToListAsync());
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
                var ID = 0;
                if (Base == 0)
                {
                    Add.AlquilerID = 1;
                    AlquilerID = 1;
                    ID = AlquilerID;
                }
                else
                {
                    if (AlquilerID == 0)
                    {
                        var UltimoIdbase = (from c in _context.Alquiler select c.AlquilerID).Max();
                        var valoresBase = _context.Alquiler.FirstOrDefault(x => x.AlquilerID == UltimoIdbase);

                        Add.AlquilerID = valoresBase.AlquilerID + 1;
                        ID = valoresBase.AlquilerID + 1;
                    }
                    else
                    {
                        Add.AlquilerID = AlquilerID;
                        ID = AlquilerID;
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

                var RestarProducto = _context.Productos.SingleOrDefault(x => x.ProductosID == ProductosID);

                var CantActual = RestarProducto.Cantidad;
                var cantGuardar = CantActual - cantidad;
                if (cantGuardar < 0)
                {
                    var mensaje = ("Solo posee " + CantActual + " " + RestarProducto.Nombre + " y quiere retirar " + cantidad).ToString();
                    var valorMensaje = 5;
                    return RedirectToAction("Create", "Alquiler", new { FechaDesde, FechaHasta, Observacion, SociosID, DeshabilitarCampos, ID, mensaje, valorMensaje });
                }
                else
                {
                    RestarProducto.Cantidad = cantGuardar;
                    _context.Update(RestarProducto);

                    _context.Add(Add);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Create", "Alquiler", new { FechaDesde, FechaHasta, Observacion, SociosID, DeshabilitarCampos, ID });
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
                return RedirectToAction("Index", "Alquiler");
            }
        }

        // GET: Alquiler/Edit/5
        public async Task<IActionResult> Edit(int? id, int? page)
        {
            try
            {
                var DetalleAlquiler = await _context.Alquiler.FirstOrDefaultAsync(m => m.AlquilerID == id);

                ViewData["FechaDesde"] = DetalleAlquiler.FechaDesde.ToShortDateString();
                ViewData["FechaHasta"] = DetalleAlquiler.FechaHasta.ToShortDateString();
                ViewData["Observacion"] = DetalleAlquiler.Observacion;
                ViewData["IdAlquiler"] = DetalleAlquiler.AlquilerID;


                var socio = from s in _context.Socios select s;
                socio = socio.Where(s => s.SociosID == DetalleAlquiler.SociosID);
                ViewData["SociosId"] = new SelectList(socio, "SociosID", "RazonSocial");
                //obtiene valor para mostran en pantalla
                var alquiler = (from e in _context.Alquiler where e.AlquilerID == id select e);

                //busca los productos que no esten dados de baja y solo sean de alquiler
                var producto = from s in _context.Productos select s;
                producto = producto.Where(s => s.FechaBaja == null && s.ProductosTipo == "DeAlquiler");
                ViewData["ProductosID"] = new SelectList(producto, "ProductosID", "Nombre");

                int pageSize = 15;
                return View(await Paginacion<Alquiler>.CreateAsync(alquiler.Include(a => a.Productos).AsNoTracking(), page ?? 1, pageSize));
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

        public async Task<IActionResult> DeletePhysical(int id)
        {
            try
            {

                // busco en la base
                var Base = _context.Alquiler.SingleOrDefault(x => x.ID == id);
                var producto = Base.ProductosID;
                var Cantidad = Base.cantidad;

                //lleno variables para pasar al front
                var FechaDesde = Base.FechaDesde;
                var FechaHasta = Base.FechaHasta;
                var Observacion = Base.Observacion;
                var SociosID = Base.SociosID;
                var DeshabilitarCampos = 1;
                var ID = Base.AlquilerID;

                //Deleteo fila base segun el id
                var evento = await _context.Alquiler.FindAsync(id);
                _context.Alquiler.Remove(evento);
                await _context.SaveChangesAsync();


                var ObtenerProducto = await _context.Productos.FindAsync(producto);

                ObtenerProducto.Cantidad = ObtenerProducto.Cantidad + Cantidad;

                _context.Productos.Update(ObtenerProducto);
                await _context.SaveChangesAsync();

                return RedirectToAction("Create", "Alquiler", new { FechaDesde, FechaHasta, Observacion, SociosID, DeshabilitarCampos, ID });
            }
            catch (Exception e)
            {
                Console.Write(e);
                var valor = 2;
                return RedirectToAction("Index", "EventoSolidarios", new { valor });
            }
        }

    }
}
