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
    
    public partial class ChoferDerLin
    {
        public int idChoDer { get; set; }
        public string Login { get; set; }
        public int idDerechoLinea { get; set; }
        public string Descripcion { get; set; }
    
        public virtual DerechoLinea DerechoLinea { get; set; }
        public virtual Personal Personal { get; set; }
    }
}
