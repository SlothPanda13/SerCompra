using System;
using System.Collections.Generic;

#nullable disable

namespace SerCompra.Models.DataBaseSC
{
    public partial class Administrador
    {
        public int IdAdministrador { get; set; }
        public string Nombre { get; set; }
        public string EmailAdmin { get; set; }
        public string ContraseñaAdmin { get; set; }
        public string Notificaciones { get; set; }
        public int RolesIdRol { get; set; }

        public virtual Role RolesIdRolNavigation { get; set; }
    }
}
