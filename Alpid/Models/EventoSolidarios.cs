﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Alpid.Models
{
    public class EventoSolidarios
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EventoSolidarioID { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ItemEventoSolidarioID { get; set; }

        public int? Cantidad { get; set; }

        [Required(ErrorMessage = "Favor de ingresar un concepto")]
        public string Concepto { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Ingreso { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Salida { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Total { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime Fecha { get; set; }

    }
}
