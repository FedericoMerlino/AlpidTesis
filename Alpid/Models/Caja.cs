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

        public decimal Debe { get; set; }

        public decimal Haber { get; set; }

        public string TipoMovimiento { get; set; }

        [Required]
        public string Observaciones { get; set; }

        public Boolean Estado { get; set; }

        [Required]
        public DateTime FechaMovimiento { get; set; }

        public decimal Total { get; set; }

        public int CuotaID { get; set; }

        public Cuotas Cuotas { get; set; }

        public int AlquilerID { get; set; }

        public Alquiler Alquiler { get; set; }
    }
}
