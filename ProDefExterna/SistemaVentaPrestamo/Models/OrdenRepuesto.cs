using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaVentaPrestamo.Models
{
    public class OrdenRepuesto : Repuesto
    {
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        [Required(ErrorMessage = "Tienes que llenar el campo {0}")]
        public float MontoCantidad { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "${0:N2}", ApplyFormatInEditMode = false)]
        public decimal Valor { get { return Precio * (decimal)MontoCantidad; } }
    }
}