using System;
using System.Collections.Generic;

#nullable disable

namespace vulns.Models.Db
{
    public partial class Cliente
    {
        public Cliente()
        {
            Vulnerabilidades = new HashSet<Vulnerabilidade>();
        }

        public int IdClientes { get; set; }
        public string NombreCliente { get; set; }

        public virtual ICollection<Vulnerabilidade> Vulnerabilidades { get; set; }
    }
}
