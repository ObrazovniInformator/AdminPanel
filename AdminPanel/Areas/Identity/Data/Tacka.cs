using System.Collections.Generic;

namespace AdminPanel.Areas.Identity.Data
{
    public class Tacka
    {
        public Tacka()
        {
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
        public int IdStav { get; set; }
        public string Tekst { get; set; }

        public Stav IdStavNavigation { get; set; }
        public ICollection<SudskaPraksa> SudskaPraksa { get; set; }
        public ICollection<SluzbenoMisljenje> SluzbenoMisljenje { get; set; }
        public ICollection<PropisCasopis> PropisCasopis { get; set; }
        public ICollection<PropisInAkta> PropisInAkta { get; set; }
        public ICollection<PrimeriKnjizenja> PrimeriKnjizenja { get; set; }
        public ICollection<Vest> Vest { get; set; }
        public ICollection<PropisPropis> PropisPropis { get; set; }
    }
}
