using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alpid.Models
{
    public class Proveedores
    {
        [Key]
        public int ProveedoresId { get; set; }

        [Required]
        public string Cuit { get; set; }

        [Required]
        public string RazonSocial { get; set; }

        public string Domicilio { get; set; }

        [Required]
        [Phone]
        public string Telefono { get; set; }

        [EmailAddress]
        public string Mail { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? FechaBaja { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime FechaAlta { get; set; }

        public string MotivoBaja { get; set; }

        public ICollection<Productos> Productos { get; set; }
    }
}
