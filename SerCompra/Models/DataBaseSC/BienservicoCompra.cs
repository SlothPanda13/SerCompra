using System;
using System.Collections.Generic;

#nullable disable

namespace SerCompra.Models.DataBaseSC
{
    public partial class BienservicoCompra
    {
        public int IdBienServicoCompra { get; set; }
        public int CompraIdCompra { get; set; }
        public int CompraFuncionarioAreaComprasIdFuncionarioAreaCompras { get; set; }
        public int BienServicioIdBienServicio { get; set; }
        public int BienServicioProveedorIdProveedor { get; set; }

        public virtual Bienservicio BienServicio { get; set; }
        public virtual Compra Compra { get; set; }
    }
}
