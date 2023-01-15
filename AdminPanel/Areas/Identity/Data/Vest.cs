using AdminPanel.Data;
using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Areas.Identity.Data
{
    public class Vest
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Наслов је обавезан податак.")]
        public string Naslov { get; set; }
        public string Sazetak { get; set; }
        public string Tekst { get; set; }
        public string DanUNedelji { get; set; }
        public int DanUMesecu { get; set; }
        public int Mesec { get; set; }
        public int Godina { get; set; }
        public int IdRubrikaVesti { get; set; }
        public RubrikaVesti IdRubrikaVestiNavigation { get; set; }
        public int? IdPropis { get; set; }
        public Propis IdPropisNavigation { get; set; }
        public int? IdClan { get; set; }
        public Clan IdClanNavigation { get; set; }
        public int? IdStav { get; set; }
        public Stav IdStavNavigation { get; set; }
        public int? IdTacka { get; set; }
        public Tacka IdTackaNavigation { get; set; }
        public int? IdKategorija { get; set; }
        public VestiKategorija IdKategorijaNavigation { get; set; }

        public static void DodajVest(Vest vest)
        {
            AdminPanelContext _context = new AdminPanelContext();
            _context.Vest.Add(vest);
            _context.SaveChanges();
        }
    }
}
