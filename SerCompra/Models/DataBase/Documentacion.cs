using System;
using System.Collections.Generic;

#nullable disable

namespace SerCompra.Models.DataBase
{
    public partial class Documentacion
    {
        public Documentacion()
        {
            Proveedors = new HashSet<Proveedor>();
        }

        public int IdDocumentacion { get; set; }
        public string Rut { get; set; }
        public string FotocopiaCedula { get; set; }
        public string CamaraComercio { get; set; }
        public string EstadosFinancieros { get; set; }
        public string LicenciasPermisos { get; set; }
        public string OtrosDocumentos { get; set; }

        public virtual ICollection<Proveedor> Proveedors { get; set; }
    }
}
