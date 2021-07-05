using AdminPanel.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Areas.Identity.Data
{
    public class PropisSluzbenoMisljenje
    {
        public int Id { get; set; }
        public int? IdPropis { get; set; }
        public int? IdSluzbenoMisljenje { get; set; }
        public int? IdClan { get; set; }
        public int? IdStav { get; set; }
        public int? IdTacka { get; set; }
        public DateTime? DatumUnosa { get; set; }

        public static void DodajPropisSluzbenoMisljenje(PropisSluzbenoMisljenje propisSluzbenoMisljenje)
        {
            AdminPanelContext _context = new AdminPanelContext();
            _context.PropisSluzbenoMisljenje.Add(propisSluzbenoMisljenje);
            _context.SaveChanges();
        }
    }
}