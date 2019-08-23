using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Alpid.Models
{
    public class Caja
    {
        [Key]
        public int CajaId { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Debe { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Haber { get; set; }

        public string TipoMovimiento { get; set; }

        [Required(ErrorMessage = "Debe ingresar un motivo")]
        public string Observaciones { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "Favor de ingresar una fecha")]
        public DateTime FechaMovimiento { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Total { get; set; }

        public int? CuotaID { get; set; }

        public Cuotas Cuotas { get; set; }

        public int? AlquilerID { get; set; }

        public Alquiler Alquiler { get; set; }

        public string Usuario { get; set; }
    }
}
