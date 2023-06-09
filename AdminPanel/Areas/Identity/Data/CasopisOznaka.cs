using System.Collections.Generic;

namespace AdminPanel.Areas.Identity.Data
{
    public class CasopisOznaka
    {
        public CasopisOznaka()
        {
            CasopisNaslov = new HashSet<CasopisNaslov>();
        }

        public int Id { get; set; }
        public string Naziv { get; set; }
        public ICollection<CasopisNaslov> CasopisNaslov { get; set; }
    }
}