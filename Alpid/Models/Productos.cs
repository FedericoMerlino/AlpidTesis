using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alpid.Models
{
    public class Productos
    {
        [Key]
        public int PoductosID { get; set; }

        [Required(ErrorMessage ="Favor de ingresar un Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage ="Favor de ingresar una cantidad")]
        public int Cantidad { get; set; }

        [Required]
        public string ProductosTipo { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? FechaBaja { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime FechaAlta { get; set; }

        public string MotivoBaja { get; set; }

        public decimal PrecioAlquiler { get; set; }

        public int? ProveedoresID { get; set; }

        public Proveedores Proveedores { get; set; }

        public ICollection<Alquiler> Alquiler { get; set; }
    }
}
