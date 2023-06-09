using System.Collections.Generic;

namespace AdminPanel.Areas.Identity.Data
{
    public class ClanPP
    {
        public ClanPP()
        {
            StavPP = new HashSet<StavPP>();
            ProsvetniPropisInAkta = new HashSet<ProsvetniPropisInAkta>();
            ProsvetniPropisSluzbenoMisljenje = new HashSet<ProsvetniPropisSluzbenoMisljenje>();
            ProsvetniPropisSudskaPraksa = new HashSet<ProsvetniPropisSudskaPraksa>();
        }
        public int Id { get; set; }
        public string Naziv { get; set; }
        public int? IdPodnaslov { get; set; }
        public int? IdPropis { get; set; }

        public ProsvetniPropis IdPropisNavigation { get; set; }
        public PodnaslovPP? IdPodnaslovNavigation { get; set; }
        public ICollection<StavPP> StavPP { get; set; }
        public ICollection<ProsvetniPropisInAkta> ProsvetniPropisInAkta { get; set; }
        public ICollection<ProsvetniPropisSluzbenoMisljenje> ProsvetniPropisSluzbenoMisljenje { get; set; }
        public ICollection<ProsvetniPropisSudskaPraksa> ProsvetniPropisSudskaPraksa { get; set; }
    }
}
