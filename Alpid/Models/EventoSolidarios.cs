using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alpid.Models
{
    public class EventoSolidarios
    {
        [Key]
        public int EventoSolidarioID { get; set; }

        public int Cantidad { get; set; }

        [Required(ErrorMessage = "Favor de ingresar un concepto")]
        public int Concepto { get; set; }

        public double Ingreso { get; set; }

        public double Salida { get; set; }

        public double Total { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime Fecha { get; set; }

    }
}
