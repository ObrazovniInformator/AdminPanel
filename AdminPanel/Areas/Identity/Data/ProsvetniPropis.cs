using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Areas.Identity.Data
{
    public class ProsvetniPropis
    {
        public ProsvetniPropis()
        {
            ClanPP = new HashSet<ClanPP>();
            PodnaslovPP = new HashSet<PodnaslovPP>();
            ProsvetniPropisInAkta = new HashSet<ProsvetniPropisInAkta>();
            ProsvetniPropisSluzbenoMisljenje = new HashSet<ProsvetniPropisSluzbenoMisljenje>();
            ProsvetniPropisSudskaPraksa = new HashSet<ProsvetniPropisSudskaPraksa>();
            PdfFajlProsvetniPropis = new HashSet<PdfFajlProsvetniPropis>();
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
        public string NivoVazenja { get; set; }
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
        //  public string NormaOsnovaZaPrestanakVazenjaPrethodnika { get; set; }
        public string DatumPrestankaVazenjaPravnogPrethodnika { get; set; }
        public string IstorijskaVerzijaPropisa { get; set; }
        public string Napomena { get; set; }
        public string ReferencaNaPropis { get; set; }
        public string NapomeneGlasnika { get; set; }
        public string TekstPropisa { get; set; }
        public string Preambula { get; set; }
        public int? RedniBroj { get; set; }
       
       

        public PodrubrikaPP IdPodrubrikaNavigation { get; set; }
        public RubrikaPP IdRubrikaNavigation { get; set; }
        public ICollection<ClanPP> ClanPP { get; set; }
        public ICollection<PodnaslovPP> PodnaslovPP { get; set; }
        public ICollection<ProsvetniPropisInAkta> ProsvetniPropisInAkta { get; set; }
        public ICollection<ProsvetniPropisSluzbenoMisljenje> ProsvetniPropisSluzbenoMisljenje { get; set; }
        public ICollection<PdfFajlProsvetniPropis> PdfFajlProsvetniPropis { get; set; }
        public ICollection<ProsvetniPropisSudskaPraksa> ProsvetniPropisSudskaPraksa { get; set; }
    }
}
