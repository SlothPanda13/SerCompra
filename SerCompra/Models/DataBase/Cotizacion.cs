using System;
using System.Collections.Generic;

#nullable disable

namespace SerCompra.Models.DataBase
{
    public partial class Cotizacion
    {
        public Cotizacion()
        {
            CotizacionBienservicios = new HashSet<CotizacionBienservicio>();
        }

        public int IdCotizacion { get; set; }
        public int Precio { get; set; }
        public string Estado { get; set; }
        public DateTime Fecha { get; set; }

        public virtual ICollection<CotizacionBienservicio> CotizacionBienservicios { get; set; }
    }
}
