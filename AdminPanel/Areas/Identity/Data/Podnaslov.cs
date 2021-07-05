using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Areas.Identity.Data
{
    public class Podnaslov
    {
        public Podnaslov()
        {
            Clan = new HashSet<Clan>();
        }

        public int Id { get; set; }
        public string PodnaslovTekst { get; set; }
        public int IdPropis { get; set; }
        public int IdNivoPodnaslova { get; set; }

        public Propis IdPropisNavigation { get; set; }
        public NivoPodnaslova IdNivoPodnaslovaNavigation { get; set; }
        public ICollection<Clan> Clan { get; set; }
    }
}
