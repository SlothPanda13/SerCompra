using System;
using System.Collections.Generic;

#nullable disable

namespace SerCompra.Models.DataBase
{
    public partial class Administrador
    {
        public int IdAdministrador { get; set; }
        public string Nombre { get; set; }
        public string Notificaciones { get; set; }
        public int UsuarioIdUsuario { get; set; }

        public virtual Usuario UsuarioIdUsuarioNavigation { get; set; }
    }
}
