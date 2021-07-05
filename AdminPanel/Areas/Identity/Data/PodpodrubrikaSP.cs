using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Areas.Identity.Data
{
    public class PodpodrubrikaSP
    {
        public PodpodrubrikaSP()
        {
            SudskaPraksa = new HashSet<SudskaPraksa>();
        }
        public int Id { get; set; }
        public string Naziv { get; set; }
        public int IdPodrubrika { get; set; }
        public PodrubrikaSP IdPodrubrikaNavigation { get; set; }
        public ICollection<SudskaPraksa> SudskaPraksa { get; set; }
    }
}
