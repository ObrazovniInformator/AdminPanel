using System.Collections.Generic;

namespace AdminPanel.Areas.Identity.Data
{
    public class RubrikaSP
    {
        public RubrikaSP()
        {
            PodrubrikaSP = new HashSet<PodrubrikaSP>();
            SudskaPraksa = new HashSet<SudskaPraksa>();
        }
        public int Id { get; set; }
        public string Naziv { get; set; }
        public ICollection<PodrubrikaSP> PodrubrikaSP { get; set; }
        public ICollection<SudskaPraksa> SudskaPraksa { get; set; }
    }
}
