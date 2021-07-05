using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using AdminPanel.Areas.Identity.Data;
using AdminPanel.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    public class ObradaTekstaPPController : Controller
    {
        AdminPanelContext _context = new AdminPanelContext();

        [HttpGet]
        public IActionResult Index(int id)
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            if (email != null)
            {

                ProsvetniPropis propis = _context.ProsvetnIPropis.Find(id);
                List<PodnaslovPP> podnaslovi = (from pod in _context.PodnaslovPP
                                                where pod.IdPropis == id
                                                select pod).ToList();
                List<ClanPP> clanovi = (from clan in _context.ClanPP
                                        where clan.IdPropis == id
                                        select clan).ToList();
                List<StavPP> stavovi = _context.StavPP.ToList();
                List<TackaPP> tacke = _context.TackaPP.ToList();
                List<AlinejaPP> alineje = _context.AlinejaPP.ToList();

                ViewBag.Podnaslovi = podnaslovi;
                ViewBag.Clanovi = clanovi;
                ViewBag.Stavovi = stavovi;
                ViewBag.Tacke = tacke;
                ViewBag.Alineje = alineje;
                ViewBag.IdPropisa = id;
                ViewBag.Propis = propis;

                return View();
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }

        [HttpPost]
        public IActionResult Index(int id, IFormCollection collection)
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            if (email != null)
            {
                ProsvetniPropis propis = (from p in _context.ProsvetnIPropis
                                 where p.Id == id
                                 select p).Single();
                string clanPatern = @"<p \s*(.+?)\s*>(<strong>)?<span \s*(.+?)\s*>(?!&nbsp;)\p{L}{1}\p{Ll}\s*(.+?)\s*</span>(\s*(.+?)\s*)?</p>(\s*(<p \s*(.+?)\s*><span \s*(.+?)\s*>[0-9]+(\))+\s+\p{Ll}\s*(.+?)\s*</span></p>)+)*";
                string patternStav = @"<p \s*(.+?)\s*>(<strong>)?(<em>)?<span \s*(.+?)\s*>(?!Члан)(\*\p{L}{1}\p{Ll}\s*(.+?)\s*)?\s*(.+?)\s*(?<!\@+)</span>(?<!<span \s*(.+?)\s*>\@+\s*(.+?)\s*</span>)(</em>)?(</strong>)?</p>";
                string tackaPatern = @"<p \s*(.+?)\s*><span \s*(.+?)\s*>[0-9]+\)*\.*\s*(.+?)\s*\p{Ll}\s*(.+?)\s*</span></p>";
                string alinejaPatern = @"<p \s*(.+?)\s*><span \s*(.+?)\s*>(&ndash;)+\s*\p{Ll}+\s*(.+?)\s*</span></p>";
                // string slikaPatern = @"<img .\s*(.+?)\s*>";
                string patern1Podnaslov = @"<p \s*(.+?)\s*>(<strong>)?(<em>)?<span \s*(.+?)\s*>(\*\p{Lu}+\s*\p{Ll}+\s*(.+?)\s*)?([0-9]+\)*\.*\s+\p{Lu}+\s*\p{L}*\s*\p{Ll}+\s*(.+?)\s*)?\s*(.+?)\s*\@*</span>(</em>)?(</strong>)?</p>(\s*(<p \s*(.+?)\s*><span \s*(.+?)\s*>)+[0-9]+(\)*\.*\s+\p{Lu}\s*\p{Ll}\s*(.+?)\s*(?<!\@+)</span></p>)+)*(<p \s*(.+?)\s*><span \s*(.+?)\s*>(&ndash;)+\s*p{Ll}+\s*(.+?)\s*\p{Ll}\s*(.+?)\s*</span></p>)*";

                string input2 = collection["TekstPropisa"]; //"I Mhave a dog Mand a Mcat.";
                List<string> _returnValue2 = new List<string>();
                MatchCollection _matchList2 = Regex.Matches(input2, patern1Podnaslov);
                var list2 = _matchList2.Cast<Match>().Select(match => match.Value).ToList();
                ViewBag.Lista = list2;
                int brojacClanova = 1;
                List<string> slova = new List<string>();
                slova.Add("а");
                slova.Add("б");
                slova.Add("в");
                slova.Add("г");
                slova.Add("д");
                slova.Add("ђ");
                slova.Add("е");
                slova.Add("ж");
                slova.Add("з");
                slova.Add("и");
                slova.Add("ј");
                slova.Add("к");
                slova.Add("л");
                slova.Add("љ");
                slova.Add("м");
                slova.Add("н");
                slova.Add("њ");
                slova.Add("о");

                foreach (string s in list2)
                {

                    //SUTRA UJUTUU OVO PRVA STVAR
                    int i = 0;
                    string slovoUClanu = slova[i];
                    List<ClanPP> clanovi = (from ck in _context.ClanPP
                                          where ck.IdPropis == id
                                          select ck).ToList();
                    string clan = "Члан " + brojacClanova;
                    if (s.Contains('@'))
                    {
                        UbaciPodnaslov(id, s);
                        continue;
                    }
                    if (s.Contains(clan))
                    {
                        UbaciClan(id, s, brojacClanova);
                        brojacClanova += 1;
                        continue;
                    }

                    int brojacClanova2 = brojacClanova - 1;
                    string clan2 = "Члан " + brojacClanova2 + slovoUClanu;

                    if (s.Contains(clan2))
                    {

                        UbaciClanSaSlovom(id, s, brojacClanova, slovoUClanu);
                        i += 1;
                        slovoUClanu = slova[i];
                        continue;
                    }

                    if (clanovi.Count > 0)
                    {
                        string input = s; //"I Mhave a dog Mand a Mcat.";
                        List<string> _returnValue = new List<string>();
                        MatchCollection _matchList = Regex.Matches(input, patternStav);
                        var list = _matchList.Cast<Match>().Select(match => match.Value).ToList();
                        if (list.Count > 0)
                        {
                            UbaciStav(id, s);
                            string inputtacka = s;
                            List<string> _returnValueTacka = new List<string>();
                            MatchCollection _matchListtacka = Regex.Matches(inputtacka, tackaPatern);
                            var listTacaka = _matchListtacka.Cast<Match>().Select(match => match.Value).ToList();
                            foreach (string tacka in listTacaka)
                            {


                                UbaciTacku(tacka);


                            }
                            string inputAlineja = s;
                            List<string> _returnValueAlineja = new List<string>();
                            MatchCollection _matchListAlineja = Regex.Matches(inputAlineja, alinejaPatern);
                            var listAlineja = _matchListAlineja.Cast<Match>().Select(match => match.Value).ToList();
                            foreach (string alineja in listAlineja)
                            {


                                UbaciAlineju(alineja);


                            }

                        }
                    }
                }
                List<PodnaslovPP> podnaslovi = (from pod in _context.PodnaslovPP
                                              where pod.IdPropis == id
                                              select pod).ToList();
                List<ClanPP> clanovi2 = (from clan in _context.ClanPP
                                       where clan.IdPropis == id
                                       select clan).ToList();
                List<StavPP> stavovi = _context.StavPP.ToList();
                List<TackaPP> tacke = _context.TackaPP.ToList();
                List<AlinejaPP> alineje = _context.AlinejaPP.ToList();

                ViewBag.Podnaslovi = podnaslovi;
                ViewBag.Clanovi = clanovi2;
                ViewBag.Stavovi = stavovi;
                ViewBag.Tacke = tacke;
                ViewBag.Alineje = alineje;
                ViewBag.IdPropisa = id;
                ViewBag.Propis = propis;
                return View();
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }

        //PODNASLOV UNOS
        public void UbaciPodnaslov(int id, string tekst)
        {
            ProsvetniPropis propis = (from p in _context.ProsvetnIPropis
                             where p.Id == id
                             select p).Single();

            string patern1 = @"<p \s*(.+?)\s*>(<strong>)?(<em>)?<span \s*(.+?)\s*>([0-9]+(\))+\s+)?\s*(.+?)\s*\@*</span>(</em>)?(</strong>)?</p>";

            string input = tekst; //"I Mhave a dog Mand a Mcat.";
            List<string> _returnValue = new List<string>();
            MatchCollection _matchList = Regex.Matches(input, patern1);
            var list = _matchList.Cast<Match>().Select(match => match.Value).ToList();

            PodnaslovPP podnaslov = new PodnaslovPP();

            NivoPodnaslova nivo1 = _context.NivoPodnaslova.Find(1);
            NivoPodnaslova nivo2 = _context.NivoPodnaslova.Find(2);
            NivoPodnaslova nivo3 = _context.NivoPodnaslova.Find(3);
            NivoPodnaslova nivo4 = _context.NivoPodnaslova.Find(4);

            int idPodnaslova = 0;

            List<PodnaslovPP> podnaslovi = (from pod in _context.PodnaslovPP
                                          select pod).ToList();
            if (podnaslovi.Count > 0)
            {
                idPodnaslova = (from idP in _context.PodnaslovPP
                                select idP.Id).Max();

            }

            int brojacEtova = 0;
            foreach (string c in list)
            {
                if (c.Contains("@@@@"))
                {
                    brojacEtova = 4;

                }
                else if (c.Contains("@@@"))
                {
                    brojacEtova = 3;

                }
                else if (c.Contains("@@"))
                {
                    brojacEtova = 2;
                }
                else
                {
                    brojacEtova = 1;
                }
            }
            string bezEta = tekst.Replace('@', ' ');
            if (brojacEtova == 1)
            {
                podnaslov.Id = idPodnaslova + 1;
                podnaslov.PodnaslovTekst = bezEta;
                podnaslov.IdNivoPodnaslova = nivo1.Id;
                podnaslov.IdPropis = id;
                try
                {
                    _context.PodnaslovPP.Add(podnaslov);
                    _context.SaveChanges();
                }
                catch
                {
                    throw;
                }

            }
            if (brojacEtova == 2)
            {
                podnaslov.Id = idPodnaslova + 1;
                podnaslov.PodnaslovTekst = bezEta;
                podnaslov.IdNivoPodnaslova = nivo2.Id;
                podnaslov.IdPropis = id;
                try
                {
                    _context.PodnaslovPP.Add(podnaslov);
                    _context.SaveChanges();
                }
                catch
                {
                    throw;
                }

            }
            if (brojacEtova == 3)
            {
                podnaslov.Id = idPodnaslova + 1;
                podnaslov.PodnaslovTekst = bezEta;
                podnaslov.IdNivoPodnaslova = nivo3.Id;
                podnaslov.IdPropis = id;
                try
                {
                    _context.PodnaslovPP.Add(podnaslov);
                    _context.SaveChanges();
                }
                catch
                {
                    throw;
                }
            }
            if (brojacEtova == 4)
            {
                podnaslov.Id = idPodnaslova + 1;
                podnaslov.PodnaslovTekst = bezEta;
                podnaslov.IdNivoPodnaslova = nivo4.Id;
                podnaslov.IdPropis = id;
                try
                {
                    _context.PodnaslovPP.Add(podnaslov);
                    _context.SaveChanges();
                }
                catch
                {
                    throw;
                }
            }
        }

        //UBACIVANJE CLANOVA
        //static int brojacClanova = 1;
        public void UbaciClan(int id, string s, int brojacClanova)
        {

            List<PodnaslovPP> podnaslovi = (from pod in _context.PodnaslovPP
                                          where pod.IdPropis == id
                                          select pod).ToList();
            int podnaslov = 0;
            if (podnaslovi.Count > 0)
            {
                podnaslov = (from pod in _context.PodnaslovPP
                             where pod.IdPropis == id
                             select pod.Id).Max();
            }
            else
            {
                podnaslov = Convert.ToInt32(null);
            }

            string clanPatern = "Члан " + brojacClanova;
            List<ClanPP> clanovi = (_context.ClanPP.ToList());
            int idClana = 0;
            if (clanovi.Count > 0)
            {
                idClana = (from c in _context.ClanPP
                           select c.Id).Max();
            }

            if (s.Contains(clanPatern))
            {
                ClanPP c = new ClanPP();
                c.Id = idClana + 1;
                c.IdPropis = id;
                c.Naziv = "Члан " + brojacClanova + ".";
                if (podnaslov != 0)
                {
                    c.IdPodnaslov = podnaslov;
                }
                else
                {
                    c.IdPodnaslov = null;
                }

                try
                {
                    _context.ClanPP.Add(c);
                    //   brojacClanova++;
                    _context.SaveChanges();
                }
                catch
                {
                    throw;
                }
            }
        }

        //CLAN SA SLOVOM UNOS
        //IDENRICNO KAO PRETHODNA S TIM STO BROJI I UBACUJE CLANOVE KOJI IMAJU I SLOVNU OZNAKU
        public void UbaciClanSaSlovom(int id, string s, int brojacClanova, string slovoUClanu)
        {
            List<PodnaslovPP> podnaslovi = (from pod in _context.PodnaslovPP
                                          where pod.IdPropis == id
                                          select pod).ToList();
            int podnaslov = 0;
            if (podnaslovi.Count > 0)
            {
                podnaslov = (from pod in _context.PodnaslovPP
                             where pod.IdPropis == id
                             select pod.Id).Max();
            }
            else
            {
                podnaslov = Convert.ToInt32(null);
            }
            brojacClanova--;
            string clanPatern = "Члан " + brojacClanova + slovoUClanu + "";
            List<ClanPP> clanovi = (_context.ClanPP.ToList());
            int idClana = 0;
            if (clanovi.Count > 0)
            {
                idClana = (from c in _context.ClanPP
                           select c.Id).Max();
            }

            if (s.Contains(clanPatern))
            {
                ClanPP c = new ClanPP();
                c.Id = idClana + 1;
                c.IdPropis = id;
                c.Naziv = "Члан " + brojacClanova + slovoUClanu;
                if (podnaslov != 0)
                {
                    c.IdPodnaslov = podnaslov;
                }
                else
                {
                    c.IdPodnaslov = null;
                }

                try
                {
                    _context.ClanPP.Add(c);
                    //   brojacClanova++;
                    _context.SaveChanges();
                }
                catch
                {
                    throw;
                }
            }
        }

        //UNOS STAVOVA U DB
        static int brojacStavova = 1;
        public void UbaciStav(int id, string s)
        {
            int idStava = 0;
            string pattern = @"<p \s*(.+?)\s*>(<strong>)?(<em>)?<span \s*(.+?)\s*>(?![0-9]+\)+)(\*\p{Lu}+\s*\p{Ll}+\s*(.+?)\s*)?\s*\p{L}+\s*\p{L}*\s*\p{Ll}+\s*(.+?)\s*(?<!\@+&nbsp;)(?<!\@+)</span>(?<!<span \s*(.+?)\s*>\@+\s*(.+?)\s*</span>)(</em>)?(</strong>)?</p>";
            List<StavPP> stavovi = (from str in _context.StavPP
                                  select str).ToList();
            StavPP stavClan = new StavPP();

            if (stavovi.Count > 0)
            {

                idStava = (from sta in _context.StavPP
                           select sta.Id).Max();
                stavClan = (from sc in _context.StavPP
                            where sc.Id == idStava
                            select sc).Single();
            }

            //pattern.Replace("\"", "\"");
            string input = s; //"I Mhave a dog Mand a Mcat.";
            List<string> _returnValue = new List<string>();
            MatchCollection _matchList = Regex.Matches(input, pattern);
            var list = _matchList.Cast<Match>().Select(match => match.Value).ToList();

            foreach (string st in list)
            {
                List<ClanPP> clanovi = (_context.ClanPP.ToList());

                int idClan = (from cl in _context.ClanPP
                              where cl.IdPropis == id
                              select cl.Id).Max();
                if (idClan != stavClan.IdClan)
                {
                    brojacStavova = 1;
                }
                else
                {
                    brojacStavova += 1;
                }

                StavPP stav = new StavPP();

                try
                {

                    stav.Id = ++idStava;
                    stav.Naziv = "Став " + brojacStavova;
                    stav.Tekst = s;
                    stav.IdClan = idClan;
                    _context.StavPP.Add(stav);

                    _context.SaveChanges();
                }
                catch
                {
                    throw;
                }
            }
        }

        //UNOS ALINEJA U DB
        static int brojacAlineja = 1;
        public void UbaciAlineju(string s)
        {
            string pattern = @"<p \s*(.+?)\s*>(<strong>)?(<em>)?<span \s*(.+?)\s*>(&ndash;)+\s*\p{Ll}+\s*(.+?)\s*(?<!\@+)</span>(</strong>)?</p>";
            List<AlinejaPP> alineja = (from str in _context.AlinejaPP
                                     select str).ToList();
            AlinejaPP alinejaStav = new AlinejaPP();

            if (alineja.Count > 0)
            {
                int idAlineje = (from a in _context.AlinejaPP
                                 select a.Id).Max();

                alinejaStav = (from sc in _context.AlinejaPP
                               where sc.Id == idAlineje
                               select sc).Single();
            }

            //pattern.Replace("\"", "\"");
            string input = s; //"I Mhave a dog Mand a Mcat.";
            List<string> _returnValue = new List<string>();
            MatchCollection _matchList = Regex.Matches(input, pattern);
            var list = _matchList.Cast<Match>().Select(match => match.Value).ToList();

            foreach (string st in list)
            {

                int idStav = (from stav in _context.StavPP
                              select stav.Id).Max();
                if (idStav != alinejaStav.IdStav)
                {
                    brojacAlineja = 1;
                }
                else
                {
                    brojacAlineja += 1;
                }

                AlinejaPP alineja1 = new AlinejaPP();

                alineja1.NazivAlineje = "Alineja " + brojacAlineja;
                alineja1.Tekst = s;
                alineja1.IdStav = idStav;
                try
                {
                    _context.AlinejaPP.Add(alineja1);

                    _context.SaveChanges();
                }
                catch
                {
                    throw;
                }
            }
        }

        //UNOS TACKE U DB
        static int brojacTacaka = 1;
        public void UbaciTacku(string s)
        {
            //string tackaPatern = "<p style=\"margin: 0in 0in 7.5pt; line - height: 115 %; font - size: 11pt; font - family: Verdana, sans - serif; \"><span style=\"color: black; \">1) ако код послодавца није основан синдикат или ниједан синдикат не испуњава услове репрезентативности или није закључен споразум о удруживању у складу са овим законом;</span></p>"

            string pattern = @"<p \s*(.+?)\s*>(<strong>)?(<em>)?<span \s*(.+?)\s*>[0-9]+\)*\.*(\s*<em>)?\s+\p{Ll}\s*(.+?)\s*(?<!\@+)</span>(</em>)?(</strong>)?</p>";
            List<TackaPP> tacke = (from str in _context.TackaPP
                                 select str).ToList();
            TackaPP tackaStav = new TackaPP();
            int idTacke = 0;
            if (tacke.Count > 0)
            {

                idTacke = (from tac in _context.TackaPP
                           select tac.Id).Max();
                tackaStav = (from sc in _context.TackaPP
                             where sc.Id == idTacke
                             select sc).Single();
            }

            //pattern.Replace("\"", "\"");
            string input = s; //"I Mhave a dog Mand a Mcat.";
            List<string> _returnValue = new List<string>();
            MatchCollection _matchList = Regex.Matches(input, pattern);
            var list = _matchList.Cast<Match>().Select(match => match.Value).ToList();

            foreach (string st in list)
            {

                int idStav = (from stav in _context.StavPP
                              select stav.Id).Max();
                if (idStav != tackaStav.IdStav)
                {
                    brojacTacaka = 1;
                }
                else
                {
                    brojacTacaka += 1;
                }

                TackaPP tacka = new TackaPP();
                tacka.Id = idTacke + 1;
                tacka.Naziv = "Taчка " + brojacTacaka;
                tacka.Tekst = s;
                tacka.IdStav = idStav;
                try
                {
                    _context.TackaPP.Add(tacka);

                    _context.SaveChanges();
                }
                catch
                {
                    throw;
                }
            }
        }


        //BRISANJE CELOG TEKSTA PROPISA IZ DB
        public IActionResult BrisiCeoTekst(int id)
        {
            List<PodnaslovPP> podnaslovi = (from pod in _context.PodnaslovPP
                                          where pod.IdPropis == id
                                          select pod).ToList();
            List<ClanPP> clanovi = (from cl in _context.ClanPP
                                  where cl.IdPropis == id
                                  select cl).ToList();
            List<StavPP> stavovi = new List<StavPP>();
            foreach (ClanPP c in clanovi)
            {
                List<StavPP> stav = (from st in _context.StavPP
                                   where st.IdClan == c.Id
                                   select st).ToList();
                stavovi.AddRange(stav);
            }

            List<TackaPP> tacke = new List<TackaPP>();
            List<AlinejaPP> alineje = new List<AlinejaPP>();
            if (stavovi != null)
            {
                foreach (StavPP s in stavovi)
                {
                    List<TackaPP> tacka = (from t in _context.TackaPP
                                         where t.IdStav == s.Id
                                         select t).ToList();
                    List<AlinejaPP> alineja = (from a in _context.AlinejaPP
                                             where a.IdStav == s.Id
                                             select a).ToList();
                    tacke.AddRange(tacka);
                    alineja.AddRange(alineja);
                }
            }


            try
            {
                if (tacke != null)
                {
                    foreach (TackaPP t in tacke)
                    {
                        _context.TackaPP.Remove(t);
                        _context.SaveChanges();
                    }

                }
                if (alineje != null)
                {
                    foreach (AlinejaPP a in alineje)
                    {
                        _context.AlinejaPP.Remove(a);
                        _context.SaveChanges();
                    }
                }
                if (stavovi != null)
                {
                    foreach (StavPP s in stavovi)
                    {
                        _context.StavPP.Remove(s);
                        _context.SaveChanges();
                    }
                }
                if (clanovi != null)
                {
                    foreach (ClanPP c in clanovi)
                    {
                        _context.ClanPP.Remove(c);
                        _context.SaveChanges();
                    }
                }

                if (podnaslovi != null)
                {
                    foreach (PodnaslovPP p in podnaslovi)
                    {
                        _context.PodnaslovPP.Remove(p);
                        _context.SaveChanges();
                    }
                }

                ViewBag.Uspeh = "Uspesno uklonjen tekst";
                return RedirectPermanent("~/ObradaTeksta/Index/" + id);
            }
            catch
            {
                throw;
            }
        }


        //TRENURNO ISKLJUCENA MOGUCNOST
        [HttpGet]
        public IActionResult DodajStav(int id)
        {
            ClanPP c = (from clan in _context.ClanPP
                      where clan.Id == id
                      select clan).Single();
            ViewBag.Clan = c;
            return View();
        }

        [HttpPost]
        public IActionResult DodajStav(StavPP s)
        {
            string email = HttpContext.Session.GetString("UserEmail");


            if (email != null)
            {
                int idMax = (from stav in _context.StavPP
                             select stav.Id).Max();
                s.Id = idMax + 1;
                try
                {
                    _context.StavPP.Add(s);
                    _context.SaveChanges();
                    ViewBag.Msg = "Став је успешно убачен";
                    return RedirectPermanent("~/ObradaTekstaPP/DodajStav/" + s.IdClan);
                }
                catch
                {

                    throw;
                }
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }

        public IActionResult PrikazCelogTeksta(int id)
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            if (email != null)
            {
                ProsvetniPropis propis = (from p in _context.ProsvetnIPropis
                                          where p.Id == id
                                          select p).Single();
                List<PodnaslovPP> podnaslovi = (from pod in _context.PodnaslovPP
                                                where pod.IdPropis == id
                                                select pod).ToList();
                List<ClanPP> clanovi = (from c in _context.ClanPP
                                        where c.IdPropis == id
                                        select c).ToList();
                List<StavPP> stavovi = new List<StavPP>();

                foreach (ClanPP c in clanovi)
                {
                    List<StavPP> stav = (from s in _context.StavPP
                                         where s.IdClan == c.Id
                                         select s).ToList();
                    stavovi.AddRange(stav);

                }

                List<TackaPP> tacke = new List<TackaPP>();

                foreach (StavPP s in stavovi)
                {
                    List<TackaPP> tacka = (from t in _context.TackaPP
                                           where t.IdStav == s.Id
                                           select t).ToList();
                    tacke.AddRange(tacka);
                }

                List<AlinejaPP> alineje = new List<AlinejaPP>();
                foreach (StavPP s in stavovi)
                {
                    List<AlinejaPP> alineja = (from a in _context.AlinejaPP
                                               where a.IdStav == s.Id
                                               select a).ToList();
                    alineje.AddRange(alineja);
                }

                List<PdfFajlProsvetniPropis> pdf = (from pd in _context.PdfFajlProsvetniPropis
                                                    where pd.IdProsvetniPropis == propis.Id
                                                    select new PdfFajlProsvetniPropis {Id = pd.Id, NaslovPdf = pd.NaslovPdf, PdfPath = pd.PdfPath }).ToList();
                ViewBag.PDF = pdf;

                ViewBag.Propis = propis;
                ViewBag.Podnaslovi = podnaslovi;
                ViewBag.Clanovi = clanovi;
                ViewBag.Stavovi = stavovi;
                ViewBag.Tacke = tacke;
                ViewBag.Alineje = alineje;
                return View();
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }



        public IActionResult CitajPdf(int id)
        {
            PdfFajlProsvetniPropis file = (from p in _context.PdfFajlProsvetniPropis
                                           where p.Id == id
                                           select p).Single();
            string path = file.PdfPath;
            return File(System.IO.File.ReadAllBytes(path), "application/pdf");

        }
    }
}