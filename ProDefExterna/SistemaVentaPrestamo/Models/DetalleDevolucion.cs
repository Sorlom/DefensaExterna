//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SistemaVentaPrestamo.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DetalleDevolucion
    {
        public int idDetDev { get; set; }
        public int idDevolucion { get; set; }
        public int idRepuesto { get; set; }
        public int Cantidad { get; set; }
    
        public virtual Devolucion Devolucion { get; set; }
        public virtual Repuesto Repuesto { get; set; }
    }
}
