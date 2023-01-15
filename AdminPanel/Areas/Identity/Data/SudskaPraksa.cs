using AdminPanel.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Areas.Identity.Data
{
    public class SudskaPraksa
    {
        public SudskaPraksa()
        {
            ProsvetniPropisSudskaPraksa = new HashSet<ProsvetniPropisSudskaPraksa>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Наслов је обавезан податак.")]
        public string Naslov { get; set; }
        public string Podnaslov { get; set; }
        public string Broj { get; set; }
        public string Datum { get; set; }
        public string Napomena { get; set; }
        public string Tekst { get; set; }

        public int IdPropis { get; set; }
        public Propis IdPropisNavigation { get; set; }
        public int IdClan { get; set; }
        public Clan IdClanNavigation { get; set; }
        public int IdStav { get; set; }
        public Stav IdStavNavigation { get; set; }
        public int? IdTacka { get; set; }
        public Tacka IdTackaNavigation { get; set; }
        public int IdRubrikaSP { get; set; }
        public RubrikaSP IdRubrikaSPNavigation { get; set; }
        public int IdPodrubrikaSP { get; set; }
        public PodrubrikaSP IdPodrubrikaSPNavigation { get; set; }
        public int IdDonosilacSP { get; set; }
        public DonosilacSP IdDonosilacSPNavigation { get; set; }

        public static void DodajSudskuPraksu(SudskaPraksa sudskaPraksa)
        {
            AdminPanelContext _context = new AdminPanelContext();
            _context.SudskaPraksa.Add(sudskaPraksa);
            _context.SaveChanges();
        }

        public ICollection<ProsvetniPropisSudskaPraksa> ProsvetniPropisSudskaPraksa { get; set; }
    }
}