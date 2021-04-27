using System;
using System.Collections.Generic;

#nullable disable

namespace vulns.Models.Db
{
    public partial class Vulnerabilidade
    {
        public int IdVulnerabilidades { get; set; }
        public DateTime FechaVulnerabilidades { get; set; }
        public int CriticasVulnerabilidades { get; set; }
        public int AltasVulnerabilidades { get; set; }
        public int MediasVulnerabilidades { get; set; }
        public int BajasVulnerabilidades { get; set; }
        public int IdClienteVulnerabilidades { get; set; }

        public virtual Cliente IdClienteVulnerabilidadesNavigation { get; set; }
    }
}
