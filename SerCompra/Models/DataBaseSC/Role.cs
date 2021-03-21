using System;
using System.Collections.Generic;

#nullable disable

namespace SerCompra.Models.DataBaseSC
{
    public partial class Role
    {
        public Role()
        {
            Administradors = new HashSet<Administrador>();
            FuncionarioAreaCompras = new HashSet<FuncionarioAreaCompra>();
            Proveedors = new HashSet<Proveedor>();
        }

        public int IdRol { get; set; }
        public string Rol { get; set; }

        public virtual ICollection<Administrador> Administradors { get; set; }
        public virtual ICollection<FuncionarioAreaCompra> FuncionarioAreaCompras { get; set; }
        public virtual ICollection<Proveedor> Proveedors { get; set; }
    }
}
