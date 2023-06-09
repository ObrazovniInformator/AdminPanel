﻿using AdminPanel.Data;
using System;

namespace AdminPanel.Areas.Identity.Data
{
    public class ProsvetniPropisSudskaPraksa
    {
        public int Id { get; set; }
        public int? IdSudskaPraksa { get; set; }
        public SudskaPraksa IdSudskaPraksaNavigation { get; set; }
        public int? IdProsvetniPropis { get; set; }
        public ProsvetniPropis IdProsvetniPropisNavigation { get; set; }
        public int? IdClanPP { get; set; }
        public ClanPP IdClanPPNavigation { get; set; }
        public int? IdStavPP { get; set; }
        public StavPP IdStavPPNavigation { get; set; }
        public int? IdTackaPP { get; set; }
        public TackaPP IdTackaPPNavigation { get; set; }
        public DateTime? DatumUnosa { get; set; }

        public static void DodajProsvetniPropisSudskaPraksa(ProsvetniPropisSudskaPraksa prosvetniPropisSudskaPraksa)
        {
            AdminPanelContext _context = new AdminPanelContext();
            _context.ProsvetniPropisSudskaPraksa.Add(prosvetniPropisSudskaPraksa);
            _context.SaveChanges();
        }
    }
}
