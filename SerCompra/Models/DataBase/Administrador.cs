using System;
using System.Collections.Generic;

#nullable disable

namespace SerCompra.Models.DataBase
{
    public partial class Administrador
    {
        public Administrador()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public int IdAdministrador { get; set; }
        public string Nombre { get; set; }
        public string Notificaciones { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
