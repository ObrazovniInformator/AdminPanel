using System.Collections.Generic;

namespace AdminPanel.Areas.Identity.Data
{
    public class SluzbenoMisljenjeViewModel
    {
        public IEnumerable<SluzbenoMisljenje> SluzbenoMisljenjeList { get; set; }
        public IEnumerable<PropisSluzbenoMisljenje> PropisSluzbenoMisljenjeList { get; set; }
        public IEnumerable<ProsvetniPropisSluzbenoMisljenje> ProsvetniPropisSluzbenoMisljenjeList { get; set; }
        public SluzbenoMisljenje SluzbenoMisljenje { get; set; }
        public PropisSluzbenoMisljenje PropisSluzbenoMisljenje { get; set; }
        public ProsvetniPropisSluzbenoMisljenje ProsvetniPropisSluzbenoMisljenje { get; set; }

        public SluzbenoMisljenjeViewModel()
        {
            SluzbenoMisljenje = new SluzbenoMisljenje();
            PropisSluzbenoMisljenje = new PropisSluzbenoMisljenje();
            ProsvetniPropisSluzbenoMisljenje = new ProsvetniPropisSluzbenoMisljenje();
        }
    }
}