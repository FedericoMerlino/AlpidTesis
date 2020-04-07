using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Alpid.Models
{
    public class Alquiler
    {
        [Key]
        public int ID { get; set; }

        public int AlquilerID { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [Required(ErrorMessage = "Debe ingresar una fecha")]
        public DateTime FechaDesde { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [Required(ErrorMessage ="Debe ingresar una fecha")]
        public DateTime FechaHasta { get; set; }

        [Required(ErrorMessage = "Debe ingresar un valor en el campo")]
        public string Observacion { get; set; }

        [Required(ErrorMessage = "Debe ingresar un Socio")]
        public int SociosID { get; set; }

        public Socios Socios { get; set; }

        [Required(ErrorMessage = "Debe ingresar un Precio")]
        public decimal Valor { get; set; }

        public decimal ValorTotal { get; set; }

        public decimal ValorPagado { get; set; }

        public string Estado { get; set; }

        [Required(ErrorMessage = "Debe ingresar una cantidad")]
        public int cantidad { get; set; }

        [Required(ErrorMessage = "Debe ingresar un Producto")]
        public int ProductosID { get; set; }

        public Productos Productos { get; set; }

        public ICollection<Caja> Caja { get; set; }
    }
}
