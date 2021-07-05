using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Areas.Identity.Data
{
    public class Stav
    {
        public Stav()
        {
            Tacka = new HashSet<Tacka>();
            Alineja = new HashSet<Alineja>();
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
        public string Tekst { get; set; }
        public int IdClan { get; set; }
            
        public Clan IdClanNavigation { get; set; }
        public ICollection<Tacka> Tacka { get; set; }
        public ICollection<Alineja> Alineja { get; set; }
        public ICollection<SudskaPraksa> SudskaPraksa { get; set; }
        public ICollection<SluzbenoMisljenje> SluzbenoMisljenje { get; set; }
        public ICollection<PropisCasopis> PropisCasopis { get; set; }
        public ICollection<PropisInAkta> PropisInAkta { get; set; }
        public ICollection<PrimeriKnjizenja> PrimeriKnjizenja { get; set; }
        public ICollection<Vest> Vest { get; set; }
        public ICollection<PropisPropis> PropisPropis { get; set; }
    }
}
