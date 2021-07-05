using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Areas.Identity.Data
{
    public class Rubrika
    {
        public Rubrika()
        {
            Podrubrika = new HashSet<Podrubrika>();
            Propis = new HashSet<Propis>();
        }
        public int ID { get; set; }
        public string NazivRubrike { get; set; }
        public int IdOblast { get; set; }

        public ICollection<Podrubrika> Podrubrika { get; set; }
        public ICollection<Propis> Propis { get; set; }
    }
}
