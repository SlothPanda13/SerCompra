using System;
using System.Collections.Generic;

#nullable disable

namespace SerCompra.Models.DataBase
{
    public partial class Solicitudproveedor
    {
        public int IdSolicitudProveedor { get; set; }
        public string Estado { get; set; }
        public int FuncionarioAreaComprasIdFuncionarioAreaCompras { get; set; }
        public int ProveedorIdProveedor { get; set; }
        public int ProveedorDocumentacionIdDocumentacion { get; set; }

        public virtual FuncionarioAreaCompra FuncionarioAreaComprasIdFuncionarioAreaComprasNavigation { get; set; }
        public virtual Proveedor Proveedor { get; set; }
    }
}
