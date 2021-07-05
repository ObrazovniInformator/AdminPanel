using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Areas.Identity.Data
{
    public class StavPP
    {
        public StavPP()
        {
            TackaPP = new HashSet<TackaPP>();
            AlinejaPP = new HashSet<AlinejaPP>();
            ProsvetniPropisInAkta = new HashSet<ProsvetniPropisInAkta>();
            ProsvetniPropisSluzbenoMisljenje = new HashSet<ProsvetniPropisSluzbenoMisljenje>();
            ProsvetniPropisSudskaPraksa = new HashSet<ProsvetniPropisSudskaPraksa>();
        }
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Tekst { get; set; }
        public int IdClan { get; set; }

        public ClanPP IdClanNavigation { get; set; }
        public ICollection<TackaPP> TackaPP { get; set; }
        public ICollection<AlinejaPP> AlinejaPP { get; set; }
        public ICollection<ProsvetniPropisInAkta> ProsvetniPropisInAkta { get; set; }
        public ICollection<ProsvetniPropisSluzbenoMisljenje> ProsvetniPropisSluzbenoMisljenje { get; set; }
        public ICollection<ProsvetniPropisSudskaPraksa> ProsvetniPropisSudskaPraksa { get; set; }
    }
}
