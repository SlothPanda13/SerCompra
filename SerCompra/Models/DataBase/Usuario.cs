using System;
using System.Collections.Generic;

#nullable disable

namespace SerCompra.Models.DataBase
{
    public partial class Usuario
    {
        public int IdUsuario { get; set; }
        public string Rol { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public int ProveedorIdProveedor { get; set; }
        public int ProveedorDocumentacionIdDocumentacion { get; set; }
        public int FuncionarioAreaComprasIdFuncionarioAreaCompras { get; set; }
        public int AdministradorIdAdministrador { get; set; }

        public virtual Administrador AdministradorIdAdministradorNavigation { get; set; }
        public virtual FuncionarioAreaCompra FuncionarioAreaComprasIdFuncionarioAreaComprasNavigation { get; set; }
        public virtual Proveedor Proveedor { get; set; }
    }
}
