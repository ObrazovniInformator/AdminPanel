using AdminPanel.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Areas.Identity.Data
{
    public class SluzbenoMisljenje
    {
        public SluzbenoMisljenje()
        {
            ProsvetniPropisSluzbenoMisljenje = new HashSet<ProsvetniPropisSluzbenoMisljenje>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Наслов је обавезан податак.")]
        public string Naslov { get; set; }
        public string Podnaslov { get; set; }
        public string Broj { get; set; }
        public string DatumDonosenja { get; set; }
        public string Napomena { get; set; }
        public string Tekst { get; set; }
        public int? RedniBroj { get; set; }
        public int IdPropis { get; set; }
        public Propis IdPropisNavigation { get; set; }
        public int IdClan { get; set; }
        public Clan IdClanNavigation { get; set; }
        public int IdStav { get; set; }
        public Stav IdStavNavigation { get; set; }
        public int? IdTacka { get; set; }
        public Tacka IdTackaNavigation { get; set; }
        public int IdRubrikaSM { get; set; }
        public RubrikaSM IdRubrikaSMNavigation { get; set; }
        public int IdPodrubrikaSM { get; set; }
        public PodrubrikaSM IdPodrubrikaSMNavigation { get; set; }
        //public int IdPodpodrubrikaSM { get; set; }
        //public PodpodrubrikaSM IdPodpodrubrikaSMNavigation { get; set; }
        public int IdDonosilacSM { get; set; }
        public DonosilacSM IdDonosilacSMNavigation { get; set; }
        public int? IdProsvetniPropis { get; set; }
        public ProsvetniPropis IdProsvetniPropisNavigation { get; set; }
        public int? IdClanPP { get; set; }
        public ClanPP IdClanPPNavigation { get; set; }
        public int? IdStavPP { get; set; }
        public StavPP IdStavPPNavigation { get; set; }
        public int? IdTackaPP { get; set; }
        public TackaPP IdTackaPPNavigation { get; set; }
        public ICollection<ProsvetniPropisSluzbenoMisljenje> ProsvetniPropisSluzbenoMisljenje { get; set; }

        public static void DodajSluzbenoMisljenje(SluzbenoMisljenje sluzbenoMisljenje)
        {
            AdminPanelContext _context = new AdminPanelContext();
            _context.SluzbenoMisljenje.Add(sluzbenoMisljenje);
            _context.SaveChanges();
        }
    }
}
