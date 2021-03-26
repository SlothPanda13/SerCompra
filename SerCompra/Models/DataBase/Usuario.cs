using System;
using System.Collections.Generic;

#nullable disable

namespace SerCompra.Models.DataBase
{
    public partial class Usuario
    {
        public Usuario()
        {
            Administradors = new HashSet<Administrador>();
            FuncionarioAreaCompras = new HashSet<FuncionarioAreaCompra>();
            Proveedors = new HashSet<Proveedor>();
        }

        public int IdUsuario { get; set; }
        public string Rol { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }

        public virtual ICollection<Administrador> Administradors { get; set; }
        public virtual ICollection<FuncionarioAreaCompra> FuncionarioAreaCompras { get; set; }
        public virtual ICollection<Proveedor> Proveedors { get; set; }
    }
}
