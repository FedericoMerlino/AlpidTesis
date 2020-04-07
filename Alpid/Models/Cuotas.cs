using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Alpid.Models
{
    public class Cuotas
    {
        [Key]
        public int CuotasID { get; set; }

        public string Estado { get; set; }

        public string Observacion { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Importe { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [Required(ErrorMessage ="Favor de ingresar una fecha")]
        public DateTime? FechaDesde { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [Required(ErrorMessage = "Favor de ingresar una fecha")]
        public DateTime FechaHasta { get; set; }

        public int SociosID { get; set; }

        public Socios Socios { get; set; }

        public ICollection<Caja> Caja { get; set; }
    }
}
