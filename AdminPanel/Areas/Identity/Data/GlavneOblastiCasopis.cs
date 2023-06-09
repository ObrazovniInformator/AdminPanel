using System.Collections.Generic;

namespace AdminPanel.Areas.Identity.Data
{
    public class GlavneOblastiCasopis
    {
        public GlavneOblastiCasopis()
        {
            CasopisGodina = new List<CasopisGodina>();
        }

        public int ID { get; set; }
        public string Naziv { get; set; }
        public List<CasopisGodina> CasopisGodina { get; set; }
    }
}