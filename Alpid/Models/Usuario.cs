using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alpid.Models
{
    public class Usuario
    {
        [Key]
        public int UsuarioID { get; set; }

        public string NombreUsuario { get; set; }

        public string Contraseña { get; set; }

    }
}
