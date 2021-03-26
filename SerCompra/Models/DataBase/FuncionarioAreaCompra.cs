using System;
using System.Collections.Generic;

#nullable disable

namespace SerCompra.Models.DataBase
{
    public partial class FuncionarioAreaCompra
    {
        public FuncionarioAreaCompra()
        {
            Compras = new HashSet<Compra>();
            Solicitudproveedors = new HashSet<Solicitudproveedor>();
        }

        public int IdFuncionarioAreaCompras { get; set; }
        public string NombreTrabajador { get; set; }
        public string Cargo { get; set; }
        public string Notificaciones { get; set; }
        public int UsuarioIdUsuario { get; set; }

        public virtual Usuario UsuarioIdUsuarioNavigation { get; set; }
        public virtual ICollection<Compra> Compras { get; set; }
        public virtual ICollection<Solicitudproveedor> Solicitudproveedors { get; set; }
    }
}
