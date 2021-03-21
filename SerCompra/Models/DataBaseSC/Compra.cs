using System;
using System.Collections.Generic;

#nullable disable

namespace SerCompra.Models.DataBaseSC
{
    public partial class Compra
    {
        public Compra()
        {
            BienservicoCompras = new HashSet<BienservicoCompra>();
        }

        public int IdCompra { get; set; }
        public int TotalPrecio { get; set; }
        public byte Calificacion { get; set; }
        public int FuncionarioAreaComprasIdFuncionarioAreaCompras { get; set; }

        public virtual FuncionarioAreaCompra FuncionarioAreaComprasIdFuncionarioAreaComprasNavigation { get; set; }
        public virtual ICollection<BienservicoCompra> BienservicoCompras { get; set; }
    }
}
