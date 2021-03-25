using System;
using System.Collections.Generic;

#nullable disable

namespace SerCompra.Models.DataBase
{
    public partial class Proveedor
    {
        public Proveedor()
        {
            Bienservicios = new HashSet<Bienservicio>();
            Solicitudproveedors = new HashSet<Solicitudproveedor>();
            Usuarios = new HashSet<Usuario>();
        }

        public int IdProveedor { get; set; }
        public string NombreProveedor { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string NombreRepresentante { get; set; }
        public string CiudadMunicipio { get; set; }
        public string Notificaciones { get; set; }
        public int DocumentacionIdDocumentacion { get; set; }

        public virtual Documentacion DocumentacionIdDocumentacionNavigation { get; set; }
        public virtual ICollection<Bienservicio> Bienservicios { get; set; }
        public virtual ICollection<Solicitudproveedor> Solicitudproveedors { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
