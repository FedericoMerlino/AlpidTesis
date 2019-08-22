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

        [Required(ErrorMessage = "Debe de ingresar un Documento/cuit")]
        [MaxLength(11, ErrorMessage = "Debe ingresar como máximo 13 números")]
        [MinLength(7, ErrorMessage = "Debe ingresar al menos 7 dígitos")]
        [Range(0, 99999999999, ErrorMessage = "El valor debe ser numérico")]
        public string Cuit { get; set; }

        [Required(ErrorMessage = "Debe de ingresar una Razón Social")]
        public string RazonSocial { get; set; }

        public string Domicilio { get; set; }

        [Required(ErrorMessage = "Debe de ingresar un Teléfono")]
        [Phone]
        public string Telefono { get; set; }

        [EmailAddress(ErrorMessage = "Debe de ingresar un mail válido")]
        public string Mail { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? FechaBaja { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime FechaAlta { get; set; }

        public string MotivoBaja { get; set; }

        public ICollection<Productos> Productos { get; set; }
    }
}
