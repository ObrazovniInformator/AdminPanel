using AdminPanel.Data;
using System;

namespace AdminPanel.Areas.Identity.Data
{
    public class ProsvetniPropisInAkta
    {
        public int Id { get; set; }
        public int? IdInAkta { get; set; }
        public InAkta IdInAktaNavigation { get; set; }
        public int? IdProsvetniPropis { get; set; }
        public ProsvetniPropis IdProsvetniPropisNavigation { get; set; }
        public int? IdClanPP { get; set; }
        public ClanPP IdClanPPNavigation { get; set; }
        public int? IdStavPP { get; set; }
        public StavPP IdStavPPNavigation { get; set; }
        public int? IdTackaPP { get; set; }
        public TackaPP IdTackaPPNavigation { get; set; }
        public DateTime? DatumUnosa { get; set; }

        public static void DodajVezuProsvetniPropisInAkta(ProsvetniPropisInAkta prosvetniPropisInAkta)
        {
            AdminPanelContext _context = new AdminPanelContext();
            _context.ProsvetniPropisInAkta.Add(prosvetniPropisInAkta);
            _context.SaveChanges();
        }
    }
}