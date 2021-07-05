using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Areas.Identity.Data
{
    public class CasopisGodina
    {
        public CasopisGodina()
        {
            CasopisNaslov = new HashSet<CasopisNaslov>();
            CasopisBroj = new HashSet<CasopisBroj>();
        }

        public int Id { get; set; }
        public string Naziv { get; set; }
        public int IdGlavneOblastiCasopis { get; set; }
        public GlavneOblastiCasopis IdGlavneOblastiCasopisNavigation { get; set; }
        public ICollection<CasopisNaslov> CasopisNaslov { get; set; }
        public ICollection<CasopisBroj> CasopisBroj { get; set; }
    }
}

