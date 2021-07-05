using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Areas.Identity.Data
{
    public class CasopisBroj
    {
        public CasopisBroj()
        {
            RubrikaCasopis = new HashSet<RubrikaCasopis>();
            CasopisNaslov = new HashSet<CasopisNaslov>();
        }

        public int Id { get; set; }
        public string Naziv { get; set; }
        public int? IdGodina { get; set; }
        public CasopisGodina IdCasopisGodinaNavigation { get; set; }
        public ICollection<RubrikaCasopis> RubrikaCasopis { get; set; }
        public ICollection<CasopisNaslov> CasopisNaslov { get; set; }
    }
}

