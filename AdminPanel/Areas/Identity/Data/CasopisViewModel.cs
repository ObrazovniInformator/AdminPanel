using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Areas.Identity.Data
{
    public class CasopisViewModel
    {
        public IEnumerable<CasopisNaslov> CasopisNaslovList { get; set; }
        public IEnumerable<PropisCasopis> PropisCasopisList { get; set; }
        public CasopisNaslov CasopisNaslov { get; set; }
        public  PropisCasopis PropisCasopis { get; set; }

        public CasopisViewModel()
        {
            CasopisNaslov = new CasopisNaslov();
            PropisCasopis = new PropisCasopis();
        }
    }
}
