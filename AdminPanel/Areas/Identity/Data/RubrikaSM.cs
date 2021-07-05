using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Areas.Identity.Data
{
    public class RubrikaSM
    {
        public RubrikaSM()
        {
            PodrubrikaSM = new HashSet<PodrubrikaSM>();
            SluzbenoMisljenje = new HashSet<SluzbenoMisljenje>();
        }
        public int Id { get; set; }
        public string Naziv { get; set; }
        public ICollection<PodrubrikaSM> PodrubrikaSM { get; set; }
        public ICollection<SluzbenoMisljenje> SluzbenoMisljenje { get; set; }
    }
}
