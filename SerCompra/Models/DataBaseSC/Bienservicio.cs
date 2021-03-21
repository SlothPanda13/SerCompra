using System;
using System.Collections.Generic;

#nullable disable

namespace SerCompra.Models.DataBaseSC
{
    public partial class Bienservicio
    {
        public Bienservicio()
        {
            BienservicoCompras = new HashSet<BienservicoCompra>();
            CotizacionBienservicios = new HashSet<CotizacionBienservicio>();
        }

        public int IdBienServicio { get; set; }
        public string Categoria { get; set; }
        public string Descripcion { get; set; }
        public int ProveedorIdProveedor { get; set; }

        public virtual Proveedor ProveedorIdProveedorNavigation { get; set; }
        public virtual ICollection<BienservicoCompra> BienservicoCompras { get; set; }
        public virtual ICollection<CotizacionBienservicio> CotizacionBienservicios { get; set; }
    }
}
