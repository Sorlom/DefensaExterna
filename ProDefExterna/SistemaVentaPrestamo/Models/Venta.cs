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
    
    public partial class Venta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Venta()
        {
            this.DetalleVenta = new HashSet<DetalleVenta>();
        }
    
        public int idVenta { get; set; }
        public string Descripcion { get; set; }
        public System.DateTime fechaInicio { get; set; }
        public System.DateTime fechaLimite { get; set; }
        public decimal Monto { get; set; }
        public int idDerechoLinea { get; set; }
        public string idChofer { get; set; }
        public string idEncargado { get; set; }
    
        public virtual DerechoLinea DerechoLinea { get; set; }
        public virtual Personal Personal { get; set; }
        public virtual Personal Personal1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetalleVenta> DetalleVenta { get; set; }
    }
}
