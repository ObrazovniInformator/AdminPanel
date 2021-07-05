using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Areas.Identity.Data
{
    public class PodpodrubrikaSM
    {
        public PodpodrubrikaSM()
        {
            SluzbenoMisljenje = new HashSet<SluzbenoMisljenje>();
        }
        public int Id { get; set; }
        public string Naziv { get; set; }
        public int IdPodrubrika { get; set; }
        public PodrubrikaSM IdPodrubrikaNavigation { get; set; }
        public ICollection<SluzbenoMisljenje> SluzbenoMisljenje { get; set; }
    }
}
