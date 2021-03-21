using System;
using System.Collections.Generic;

#nullable disable

namespace SerCompra.Models.DataBaseSC
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
        public string Contraseña { get; set; }
        public string Notificaciones { get; set; }
        public string EmailFuncionario { get; set; }
        public int RolesIdRol { get; set; }

        public virtual Role RolesIdRolNavigation { get; set; }
        public virtual ICollection<Compra> Compras { get; set; }
        public virtual ICollection<Solicitudproveedor> Solicitudproveedors { get; set; }
    }
}
