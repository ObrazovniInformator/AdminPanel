using System.Collections.Generic;

namespace AdminPanel.Areas.Identity.Data
{
    public class DonosilacSP
    {
        public DonosilacSP()
        {
            SudskaPraksa = new HashSet<SudskaPraksa>();
        }
        public int Id { get; set; }
        public string Naziv { get; set; }
        public ICollection<SudskaPraksa> SudskaPraksa { get; set; }
    }
}
