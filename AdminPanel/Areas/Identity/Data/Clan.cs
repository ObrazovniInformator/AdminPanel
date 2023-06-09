using System.Collections.Generic;

namespace AdminPanel.Areas.Identity.Data
{
    public class Clan
    {
        public Clan()
        {
            Stav = new HashSet<Stav>();
            SudskaPraksa = new HashSet<SudskaPraksa>();
            SluzbenoMisljenje = new HashSet<SluzbenoMisljenje>();
            PropisCasopis = new HashSet<PropisCasopis>();
            PropisInAkta = new HashSet<PropisInAkta>();
            PrimeriKnjizenja = new HashSet<PrimeriKnjizenja>();
            Vest = new HashSet<Vest>();
            PropisPropis = new HashSet<PropisPropis>();
        }

        public int Id { get; set; }
        public string Naziv { get; set; }
        public int? IdPodnaslov { get; set; }
        public int IdPropis { get; set; }

        public Propis IdPropisNavigation { get; set; }
        public Podnaslov? IdPodnaslovNavigation { get; set; }
        public ICollection<Stav> Stav { get; set; }
        public ICollection<SudskaPraksa> SudskaPraksa { get; set; }
        public ICollection<SluzbenoMisljenje> SluzbenoMisljenje { get; set; }
        public ICollection<PropisCasopis> PropisCasopis { get; set; }
        public ICollection<PropisInAkta> PropisInAkta { get; set; }
        public ICollection<PrimeriKnjizenja> PrimeriKnjizenja { get; set; }
        public ICollection<Vest> Vest { get; set; }
        public ICollection<PropisPropis> PropisPropis { get; set; }
    }
}
