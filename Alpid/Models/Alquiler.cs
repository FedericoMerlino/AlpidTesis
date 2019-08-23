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
        public int AlquilerID { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [Required(ErrorMessage = "Debe ingresar una fecha")]

        public DateTime FechaDesde { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [Required(ErrorMessage ="Debe ingresar una fecha")]
        public DateTime FechaHasta { get; set; }

        public string Observacion { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Valor { get; set; }

        public int ProductosID { get; set; }

        public Productos Productos { get; set; }

        public int SociosId { get; set; }

        public Socios Socios { get; set; }

        public ICollection<Caja> Caja { get; set; }
    }
}
