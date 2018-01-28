using SistemaVentaPrestamo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaVentaPrestamo.ViewModels
{
    public class VistaPedido
    {
        public string Descripcion { get; set; }
        public System.DateTime fechaInicio { get; set; }
        public System.DateTime fechaLimite { get; set; }
        public DerechoLinea DerechoLinea { get; set; }
        public Personal Encargado { get; set; }
        public Personal Chofer { get; set; }
        public OrdenRepuesto OrdenRepuesto { get; set; }
        public List<OrdenRepuesto> Repuestos { get; set; }

    }
}