using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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