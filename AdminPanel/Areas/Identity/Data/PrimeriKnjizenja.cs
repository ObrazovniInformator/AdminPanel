using AdminPanel.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Areas.Identity.Data
{
    public class PrimeriKnjizenja
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Наслов је обавезан податак.")]
        public string Naslov { get; set; }
        public string Podnaslov { get; set; }
        public string Napomena { get; set; }
        public string Tekst { get; set; }
        public int? IdPropis { get; set; }
        public Propis IdPropisNavigation { get; set; }
        public int? IdClan { get; set; }
        public Clan IdClanNavigation { get; set; }
        public int? IdStav { get; set; }
        public Stav IdStavNavigation { get; set; }
        public int? IdTacka { get; set; }
        public Tacka IdTackaNavigation { get; set; }
        public int? IdRubrikaPK { get; set; }
        public RubrikaPK IdRubrikaPKNavigation { get; set; }

        public static void DodajPrimerKnjizenja(PrimeriKnjizenja primeriKnjizenja)
        {
            AdminPanelContext _context = new AdminPanelContext();
            _context.PrimeriKnjizenja.Add(primeriKnjizenja);
            _context.SaveChanges();
        }
    }
}
