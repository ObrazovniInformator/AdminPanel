using AdminPanel.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Areas.Identity.Data
{
    public class Propis
    {
       
       public Propis()
       {
            Clan = new HashSet<Clan>();
            Podnaslov = new HashSet<Podnaslov>();
            SudskaPraksa = new HashSet<SudskaPraksa>();
            SluzbenoMisljenje = new HashSet<SluzbenoMisljenje>();
            PropisCasopis = new HashSet<PropisCasopis>();
            PropisInAkta = new HashSet<PropisInAkta>();
            PrimeriKnjizenja = new HashSet<PrimeriKnjizenja>();
            Vest = new HashSet<Vest>();
            PropisPropis = new HashSet<PropisPropis>();
        }
        
        public int Id { get; set; }
        [Required(ErrorMessage = "Наслов је обавезан податак.")]
        public string Naslov { get; set; }
        public int? IdRubrike { get; set; }
        public int? IdPodrubrike { get; set; }
        [Required(ErrorMessage = "Гласило и датум ибјављивања обавезан податак.")]
        public string GlasiloIDatumObjavljivanja { get; set; }
        [Required(ErrorMessage = "Врста прописа је обавезан податак.")]
        public string VrstaPropisa { get; set; }
        [Required(ErrorMessage = "Доносилац је обавезан податак.")]
        public string Donosilac { get; set; }
        [Required(ErrorMessage = "Ниво важења је обавезан податак.")]
        public string NivoVazenja { get; set;}
        public DateTime? DatumStupanjaNaSnaguVerzijePropisa { get; set; }
        public DateTime? DatumPrestankaVerzije { get; set; }
        public DateTime? DatumObjavljivanjaVerzije { get; set; }
        public DateTime? DatumObjavljivanjaOsnovnogTeksta { get; set; }
        public DateTime? DatumStupanjaNaSnaguMeđunarodnogUgovora { get; set; }
        public DateTime? DatumStupanjaNaSnaguOsnovnogTekstaPropisa { get; set; }
        public DateTime? DatumPrestankaVazenjaPropisa { get; set; }
        public DateTime? DatumPocetkaPrimene { get; set; }
        public string PravniOsnovZaDonosenjaPropisa { get; set; }
        public string NormaOsnovaZaDonosenje { get; set; }
        public string PropisKojiJePrestaoDaVazi { get; set; }
        public string NormaOsnovaZaPrestanakVazenja { get; set; }
        //public string NormaOsnovaZaPrestanakVazenjaPrethodnika { get; set; }
        public string DatumPrestankaVazenjaPravnogPrethodnika { get; set; }
        public string IstorijskaVerzijaPropisa { get; set; }
        public string Napomena { get; set; }
        public string ReferencaNaPropis { get; set; }
        public string NapomeneGlasnika { get; set; }
        public string TekstPropisa { get; set; }
        public string Preambula { get; set; }
        public int? RedniBroj { get; set; }

        public Podrubrika IdPodrubrikaNavigation { get; set; }
        public Rubrika IdRubrikaNavigation { get; set; }
        public ICollection<Clan> Clan { get; set; }
        public ICollection<Podnaslov> Podnaslov { get; set; }
        public ICollection<SudskaPraksa> SudskaPraksa { get; set; }
        public ICollection<SluzbenoMisljenje> SluzbenoMisljenje { get; set; }
        public ICollection<PropisCasopis> PropisCasopis { get; set; }
        public ICollection<PropisInAkta> PropisInAkta { get; set; }
        public ICollection<PrimeriKnjizenja> PrimeriKnjizenja { get; set; }
        public ICollection<Vest> Vest { get; set; }
        public ICollection<PropisPropis> PropisPropis { get; set; }
        public ICollection<PdfFajlPropis> PdfFajlPropis { get; set; }

        public static void DodajPropis(Propis propis)
        {
            AdminPanelContext _context = new AdminPanelContext();
            _context.Propis.Add(propis);
            _context.SaveChanges();
        }
    }
}
