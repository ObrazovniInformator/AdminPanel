using AdminPanel.Data;
using System;

namespace AdminPanel.Areas.Identity.Data
{
    public class PropisVest
    {
        public int Id { get; set; }
        public int? IdPropis { get; set; }
        public int? IdVest { get; set; }
        public int? IdClan { get; set; }
        public int? IdStav { get; set; }
        public int? IdTacka { get; set; }
        public DateTime? DatumUnosa { get; set; }

        public static void DodajPropisVest(PropisVest propisVest)
        {
            AdminPanelContext _context = new AdminPanelContext();
            _context.PropisVest.Add(propisVest);
            _context.SaveChanges();
        }
    }
}
