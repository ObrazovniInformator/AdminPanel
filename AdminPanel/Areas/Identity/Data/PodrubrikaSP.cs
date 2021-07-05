using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Areas.Identity.Data
{
    public class PodrubrikaSP
    {
        public PodrubrikaSP()
        {
            //PodpodrubrikaSP = new HashSet<PodpodrubrikaSP>();
            SudskaPraksa = new HashSet<SudskaPraksa>();
        }
        public int Id { get; set; }
        public string Naziv { get; set; }
        public int IdRubrika { get; set; }
        public RubrikaSP IdRubrikaNavigation { get; set; }
        //public ICollection<PodpodrubrikaSP> PodpodrubrikaSP { get; set; }
        public ICollection<SudskaPraksa> SudskaPraksa { get; set; }
    }
}
