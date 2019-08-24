using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Alpid.Models
{
    public class ProductosAlquiler
    {
        [Key]
        public int ProductosAlquilerID { get; set; }


        public string Valor { get; set; }

        public string cantidad { get; set; }

        public int ProductosID { get; set; }

        public Productos Productos { get; set; }

        public Alquiler AlquilerID { get; set; }

        public ICollection<Alquiler> Alquiler { get; set; }

    }
}
