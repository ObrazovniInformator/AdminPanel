using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Areas.Identity.Data
{
    public class PracenjeGresaka
    {
        public int Id { get; set; }
        public string Greska { get; set; }
        public DateTime Datum { get; set; }
    }
}
