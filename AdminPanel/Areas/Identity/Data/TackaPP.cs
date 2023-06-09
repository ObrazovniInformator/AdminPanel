using System.Collections.Generic;

namespace AdminPanel.Areas.Identity.Data
{
    public class TackaPP
    {
        public TackaPP()
        {
            ProsvetniPropisInAkta = new HashSet<ProsvetniPropisInAkta>();
            ProsvetniPropisSluzbenoMisljenje = new HashSet<ProsvetniPropisSluzbenoMisljenje>();
            ProsvetniPropisSudskaPraksa = new HashSet<ProsvetniPropisSudskaPraksa>();
        }

        public int Id { get; set; }
        public string Naziv { get; set; }
        public int IdStav { get; set; }
        public string Tekst { get; set; }

        public StavPP IdStavNavigation { get; set; }
        public ICollection<ProsvetniPropisInAkta> ProsvetniPropisInAkta { get; set; }
        public ICollection<ProsvetniPropisSluzbenoMisljenje> ProsvetniPropisSluzbenoMisljenje { get; set; }
        public ICollection<ProsvetniPropisSudskaPraksa> ProsvetniPropisSudskaPraksa { get; set; }
    }
}
