using System.Collections.Generic;

namespace AdminPanel.Areas.Identity.Data
{
    public class PodnaslovPP
    {
        public PodnaslovPP()
        {
            ClanPP = new HashSet<ClanPP>();
        }

        public int Id { get; set; }
        public string PodnaslovTekst { get; set; }
        public int IdPropis { get; set; }
        public int IdNivoPodnaslova { get; set; }

        public ProsvetniPropis IdPropisNavigation { get; set; }
        public NivoPodnaslova IdNivoPodnaslovaNavigation { get; set; }
        public ICollection<ClanPP> ClanPP { get; set; }
    }
}
