using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alpid.Models
{
    public class CuotaPrecio
    {
        [Key]
        public int CuotaPrecioID { get; set; }

        [Required(ErrorMessage ="Favor de ingresar un valor")]
        [Range(0, 99999999999, ErrorMessage = "El valor debe ser numérico")]
        public double Importe { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [Required(ErrorMessage ="Favor de ingresar una fecha")]
        public DateTime FechaDesde { get; set; }
    }
}
