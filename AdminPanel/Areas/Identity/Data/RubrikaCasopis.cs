using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Areas.Identity.Data
{
    public class RubrikaCasopis
    {
        public RubrikaCasopis()
        {
            PodrubrikaCasopis = new HashSet<PodrubrikaCasopis>();
            CasopisNaslov = new HashSet<CasopisNaslov>();
        }
        public int ID { get; set; }
        public string NazivRubrike { get; set; }
        public int IdOblast { get; set; }
        public int? IdBroj { get; set; }
        public CasopisBroj IdBrojNavigation { get; set; }
        public ICollection<PodrubrikaCasopis> PodrubrikaCasopis { get; set; }
        public ICollection<CasopisNaslov> CasopisNaslov { get; set; }
    }
}