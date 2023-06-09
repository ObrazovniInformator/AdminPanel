using System.Collections.Generic;

namespace AdminPanel.Areas.Identity.Data
{
    public class PrimeriKnjizenjaViewModel
    {
        public IEnumerable<PrimeriKnjizenja> PrimeriKnjizenjaList { get; set; }
        public IEnumerable<PropisPrimeriKnjizenja> PropisPrimeriKnjizenjaList { get; set; }
        public PrimeriKnjizenja PrimeriKnjizenja { get; set; }
        public PropisPrimeriKnjizenja PropisPrimeriKnjizenja { get; set; }

        public PrimeriKnjizenjaViewModel()
        {
            PrimeriKnjizenja = new PrimeriKnjizenja();
            PropisPrimeriKnjizenja = new PropisPrimeriKnjizenja();
        }
    }
}
