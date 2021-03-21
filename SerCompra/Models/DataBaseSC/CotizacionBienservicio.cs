using System;
using System.Collections.Generic;

#nullable disable

namespace SerCompra.Models.DataBaseSC
{
    public partial class CotizacionBienservicio
    {
        public int IdCotizacionBienServicio { get; set; }
        public int BienServicioIdBienServicio { get; set; }
        public int BienServicioProveedorIdProveedor { get; set; }
        public int CotizacionIdCotizacion { get; set; }

        public virtual Bienservicio BienServicio { get; set; }
        public virtual Cotizacion CotizacionIdCotizacionNavigation { get; set; }
    }
}
