using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alpid.Models
{
    public class Caja
    {
        [Key]
        public int CajaId { get; set; }

        public double? Debe { get; set; }

        public double? Haber { get; set; }

        public string TipoMovimiento { get; set; }

        [Required(ErrorMessage ="Debe ingresar un motivo")]
        public string Observaciones { get; set; }

        [Required]
        public DateTime FechaMovimiento { get; set; }

        public double? Total { get; set; }

        public int? CuotaID { get; set; }

        public Cuotas Cuotas { get; set; }

        public int? AlquilerID { get; set; }

        public Alquiler Alquiler { get; set; }
    }
}
