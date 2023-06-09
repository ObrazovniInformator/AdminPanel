using System.Collections.Generic;

namespace AdminPanel.Areas.Identity.Data
{
    public class RubrikaPP
    {
        public RubrikaPP()
        {
            PodrubrikaPP = new HashSet<PodrubrikaPP>();
            ProsvetniPropis = new HashSet<ProsvetniPropis>();
        }

        public int ID { get; set; }
        public string NazivRubrike { get; set; }

        public ICollection<PodrubrikaPP> PodrubrikaPP { get; set; }
        public ICollection<ProsvetniPropis> ProsvetniPropis { get; set; }
    }
}
