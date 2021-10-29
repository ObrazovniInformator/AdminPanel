using AdminPanel.Data;
using System;
using System.Collections.Generic;
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
        public string Naslov { get; set; }
        public int? IdRubrike { get; set; }
        public int? IdPodrubrike { get; set; }
        public string GlasiloIDatumObjavljivanja { get; set; }
        public string VrstaPropisa { get; set; }
        public string Donosilac { get; set; }
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
      //  public string NormaOsnovaZaPrestanakVazenjaPrethodnika { get; set; }
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
        //public int? IdPropis { get; set; }
        //public Propis IdPropisNavigation { get; set; }
        //public int? IdClan { get; set; }
        //public Clan IdClanNavigation { get; set; }
        //public int? IdStav { get; set; }
        //public Stav IdStavNavigation { get; set; }
        //public int? IdTacka { get; set; }
        //public Tacka IdTackaNavigation { get; set; }
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
