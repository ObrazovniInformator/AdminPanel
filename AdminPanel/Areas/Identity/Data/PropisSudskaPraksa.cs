using AdminPanel.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Areas.Identity.Data
{
    public class PropisSudskaPraksa
    {
        public int Id { get; set; }
        public int? IdPropis { get; set; }
        public int? IdSudskaPraksa { get; set; }
        public int? IdClan { get; set; }
        public int? IdStav { get; set; }
        public int? IdTacka { get; set; }
        public DateTime? DatumUnosa { get; set; }

        public static void DodajPropisSudskuPraksu(PropisSudskaPraksa propisSudskaPraksa)
        {
            AdminPanelContext _context = new AdminPanelContext();
            _context.PropisSudskaPraksa.Add(propisSudskaPraksa);
            _context.SaveChanges();
        }
    }
}