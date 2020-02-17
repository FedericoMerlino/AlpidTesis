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
using Microsoft.AspNetCore.Http;

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
        public string FechaDesdeFilterGloval = null;
        public string FechaHastaFilterGloval = null;
        public string SocioFilterGloval = null;

        // GET: Alquiler
        public async Task<IActionResult> Index(DateTime fechaDesde, DateTime fechaHasta, string currentFilter, string searchString,
                                                int? page, string filtroFecha, string DateFilter, int valor)
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
                var alquiler = from s in _context.Alquiler select s;

                if (!string.IsNullOrEmpty(searchString))
                {
                    alquiler = alquiler.Where(s => s.Socios.RazonSocial.Contains(searchString) || s.Socios.Cuit.Contains(searchString));
                    HttpContext.Session.SetString("SocioFilterGloval", (from a in _context.Alquiler where a.Socios.RazonSocial.Contains(searchString)
                                                  || a.Socios.Cuit.Contains(searchString) select a.SociosID).FirstOrDefault().ToString());
                }
                string mesDesde;
                string mesHasta;

                if (fechaDesde.Day == 1 && fechaDesde.Month == 1 && fechaDesde.Year == 1)
                {
                    fechaDesde = DateTime.Now.AddDays(-30);
                };
                if (fechaHasta.Day == 1 && fechaHasta.Month == 1 && fechaHasta.Year == 1)
                {
                    fechaHasta = DateTime.Now;
                };

                if (fechaDesde.Month < 9)
                {
                    mesDesde = "-0" + fechaDesde.Month;
                }
                else
                {
                    mesDesde = "-" + fechaDesde.Month;
                }
                if (fechaHasta.Month < 9)
                {
                    mesHasta = "-0" + fechaHasta.Month;
                }
                else
                {
                    mesHasta = "-" + fechaHasta.Month;
                }
                ViewData["FechaDesdeFilter"] = fechaDesde.Year + mesDesde + "-" + fechaDesde.Day;
                ViewData["FechaHastaFilter"] = fechaHasta.Year + mesHasta + "-" + fechaHasta.Day;

                HttpContext.Session.SetString("FechaDesdeFilterGloval", fechaDesde.ToShortDateString());
                HttpContext.Session.SetString("FechaHastaFilterGloval", fechaHasta.ToShortDateString());

                alquiler = alquiler.Where(s => Convert.ToDateTime(s.FechaDesde.ToShortDateString()) >= Convert.ToDateTime(fechaDesde.ToShortDateString()));
                alquiler = alquiler.Where(s => Convert.ToDateTime(s.FechaHasta.ToShortDateString()) <= Convert.ToDateTime(fechaHasta.ToShortDateString()));

                ViewData["Message"] = valor;

                int pageSize = 15;
                return View(await Paginacion<Alquiler>.CreateAsync(alquiler.AsNoTracking().Include(x => x.Socios), page ?? 1, pageSize));
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
            var FechaDesde = HttpContext.Session.GetString("FechaDesdeFilterGloval");
            var FechaHasta = HttpContext.Session.GetString("FechaHastaFilterGloval");
            var SocioID = HttpContext.Session.GetString("SocioFilterGloval");
            ViewData["SocioFilter"] = 0;
            ViewData["FechaDesdeFilter"] = null;
            ViewData["FechaHastaFilter"] = null;

            int pageSize = 15;
            if (SocioID != null && SocioID != "vaciarActivos")
            {
                var resultado = (from e in _context.Alquiler.Include(x => x.Socios)
                                 where e.SociosID == Convert.ToInt32(SocioID)
                                 select e);
                ViewData["SocioFilter"] = (from e in _context.Socios where e.SociosID == Convert.ToInt32(SocioID) select e.RazonSocial).FirstOrDefault();

                HttpContext.Session.SetString("SocioID", "vaciarActivos");

                return View(await Paginacion<Alquiler>.CreateAsync(resultado, page ?? 1, pageSize));
            }
            if (FechaDesde != null && FechaDesde != "vaciarEliminados")
            {
                var resultado = (from e in _context.Alquiler.Include(x => x.Socios)
                                 where e.FechaDesde > Convert.ToDateTime(FechaDesde)
                                 select e);
                resultado = (from e in _context.Alquiler.Include(x =>x.Socios)
                             where e.FechaDesde < Convert.ToDateTime(FechaHasta)
                             select e);

                ViewData["FechaDesdeFilter"] = FechaDesde;
                ViewData["FechaHastaFilter"] = FechaHasta;

                HttpContext.Session.SetString("FechaDesde", "vaciarEliminados");

                return View(await Paginacion<Alquiler>.CreateAsync(resultado, page ?? 1, pageSize));
            }

            var valor = 3;
            return RedirectToAction("Index", "Socios", new { valor });
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

                var maxvalor = (from s in _context.Alquiler
                                where s.AlquilerID == id && s.ValorTotal != 0
                                select s.ValorTotal).FirstOrDefaultAsync();
                ViewData["ResultadoTotal"] = maxvalor.Result;


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
                                                int ID, int DeshabilitarCampos, string mensaje, int valorMensaje, decimal ResultadoTotal)
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
                ViewData["ResultadoTotal"] = ResultadoTotal;

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
                //Busco si posee productos de lo que intenta alquilar
                if (cantGuardar < 0)
                {
                    var mensaje = ("Solo posee " + CantActual + " " + RestarProducto.Nombre + " y quiere retirar " + cantidad).ToString();
                    decimal ResultadoTotal = (from x in _context.Alquiler where x.AlquilerID == ID select x.Valor).Sum();
                    var valorMensaje = 5;
                    return RedirectToAction("Create", "Alquiler", new { FechaDesde, FechaHasta, Observacion, SociosID, DeshabilitarCampos, ID, mensaje, valorMensaje, ResultadoTotal });
                }
                else
                {
                    //Resta producto
                    RestarProducto.Cantidad = cantGuardar;
                    _context.Update(RestarProducto);

                    //Guarda alquiler
                    _context.Add(Add);
                    await _context.SaveChangesAsync();

                    //*actualiza alquiler para poner el valor total

                    var obtenreid = (from a in _context.Alquiler
                                     where a.AlquilerID == ID
                                     select a.ID).FirstOrDefault();

                    var Valoralquiler = await _context.Alquiler.FindAsync(obtenreid);

                    decimal ResultadoTotal = (from x in _context.Alquiler where x.AlquilerID == ID select x.Valor).Sum();
                    Valoralquiler.ValorTotal = ResultadoTotal;

                    _context.Update(Valoralquiler);
                    await _context.SaveChangesAsync();
                    //*

                    return RedirectToAction("Create", "Alquiler", new { FechaDesde, FechaHasta, Observacion, SociosID, DeshabilitarCampos, ID, ResultadoTotal });
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

                var maxvalor = (from s in _context.Alquiler
                                where s.AlquilerID == id && s.ValorTotal != 0
                                select s.ValorTotal).FirstOrDefaultAsync();
                ViewData["ResultadoTotal"] = maxvalor.Result;

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
        public async Task<IActionResult> Edit(DateTime FechaDesde, DateTime FechaHasta, string Observacion, int SociosID, int ProductosID,
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
                //Busco si posee productos de lo que intenta alquilar
                if (cantGuardar < 0)
                {
                    var mensaje = ("Solo posee " + CantActual + " " + RestarProducto.Nombre + " y quiere retirar " + cantidad).ToString();
                    decimal ResultadoTotal = (from x in _context.Alquiler where x.AlquilerID == ID select x.Valor).Sum();
                    var valorMensaje = 5;
                    return RedirectToAction("Create", "Alquiler", new { FechaDesde, FechaHasta, Observacion, SociosID, DeshabilitarCampos, ID, mensaje, valorMensaje, ResultadoTotal });
                }
                else
                {
                    //Resta producto
                    RestarProducto.Cantidad = cantGuardar;
                    _context.Update(RestarProducto);

                    //Guarda alquiler
                    _context.Add(Add);
                    await _context.SaveChangesAsync();

                    //*actualiza alquiler para poner el valor total

                    var obtenreid = (from a in _context.Alquiler
                                     where a.AlquilerID == ID
                                     select a.ID).FirstOrDefault();

                    var Valoralquiler = await _context.Alquiler.FindAsync(obtenreid);

                    decimal ResultadoTotal = (from x in _context.Alquiler where x.AlquilerID == ID select x.Valor).Sum();
                    Valoralquiler.ValorTotal = ResultadoTotal;

                    _context.Update(Valoralquiler);
                    await _context.SaveChangesAsync();
                    //*

                    return RedirectToAction("Create", "Alquiler", new { FechaDesde, FechaHasta, Observacion, SociosID, DeshabilitarCampos, ID, ResultadoTotal });
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
                return RedirectToAction("Index", "Alquiler");
            }
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

                decimal ResultadoTotal = (from x in _context.Alquiler where x.AlquilerID == ID select x.Valor).Sum();


                return RedirectToAction("Create", "Alquiler", new { FechaDesde, FechaHasta, Observacion, SociosID, DeshabilitarCampos, ID, ResultadoTotal });
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
