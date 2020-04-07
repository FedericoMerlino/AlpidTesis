using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Alpid.Models
{
    public class CuotaPrecio
    {
        [Key]
        public int CuotaPrecioID { get; set; }

        [Required(ErrorMessage ="Favor de ingresar un valor")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Importe { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [Required(ErrorMessage ="Favor de ingresar una fecha")]
        public DateTime FechaDesde { get; set; }
    }
}
