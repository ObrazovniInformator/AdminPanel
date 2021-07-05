using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AdminPanel.Areas.Identity.Data;
using AdminPanel.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    public class ObradaTekstaController : Controller
    {
        AdminPanelContext _context = new AdminPanelContext();

        //PRIKAZ STRANICE I ISPIS RAZDELJENOG TEKSTA 
        //treba jos doradjivati
        [HttpGet]
        public IActionResult Index(int id)
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            if (email != null)
            {
                Propis propis = _context.Propis.Find(id);

                List<Podnaslov> podnaslovi = (from pod in _context.Podnaslov
                                              where pod.IdPropis == id
                                              select pod).ToList();
                List<Clan> clanovi = (from clan in _context.Clan
                                      where clan.IdPropis == id
                                      select clan).ToList();
                List<int> idClanova = (from cl in _context.Clan
                                       where cl.IdPropis == id
                                       select cl.Id).ToList();
                List<Stav> stavovi = (from st in _context.Stav
                                      where idClanova.Contains(st.IdClan)
                                      select st).ToList();
                List<int> idStavova = (from st in _context.Stav
                                       where idClanova.Contains(st.IdClan)
                                       select st.Id).ToList();
                List<Tacka> tacke = (from tac in _context.Tacka
                                     where idStavova.Contains(tac.IdStav)
                                     select tac).ToList();
                List<Alineja> alineje = (from al in _context.Alineja
                                         where idStavova.Contains(al.IdStav)
                                         select al).ToList();


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

        //POCETNA LOGIKA UBACIVANJA. DELJENJE TEKSTOVA NA CERLINE, POZIV FUNKCIJA KOJE UBACUJU TEKST U DB.
        [HttpPost]
        public IActionResult Index(int id, IFormCollection collection)
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            if (email != null)
            {
                Propis propis = (from p in _context.Propis
                                 where p.Id == id
                                 select p).Single();
               // string clanPatern = @"<p \s*(.+?)\s*>(<strong>)?<span \s*(.+?)\s*>(?!&nbsp;)\p{L}{1}\p{Ll}\s*(.+?)\s*</span>(\s*(.+?)\s*)?</p>(\s*(<p \s*(.+?)\s*><span \s*(.+?)\s*>[0-9]+(\))+\s+\p{Ll}\s*(.+?)\s*</span></p>)+)*";
                string patternStav = @"<p \s*(.+?)\s*>(<strong>)?(<em>)?<span \s*(.+?)\s*>(?![0-9])(?!Члан)(\*\p{L}{1}\p{Ll}\s*(.+?)\s*)?\s*(.+?)\s*(?<!\@+)</span>(?<!<span \s*(.+?)\s*>\@+\s*(.+?)\s*</span>)(</em>)?(</strong>)?</p>";
                string tackaPatern = @"<p \s*(.+?)\s*><span \s*(.+?)\s*>\(?[0-9]+\)*\.*\s*(.+?)\s*(\p{L})*\p{Ll}\s*(.+?)\s*</span></p>";
                string alinejaPatern = @"<p \s*(.+?)\s*><span \s*(.+?)\s*>(&ndash;)+\s*\p{Ll}+\s*(.+?)\s*</span></p>";
                // string slikaPatern = @"<img .\s*(.+?)\s*>";
                string patern1Podnaslov = @"<p \s*(.+?)\s*>(<strong>)?(<em>)?<span \s*(.+?)\s*>(\*\p{Lu}+\s*\p{Ll}\s*(.+?)\s*)?([0-9]+\)*\.*\s+\p{Lu}+\s*\p{L}*\s*\p{Ll}+\s*(.+?)\s*)?\s*(.+?)\s*\@*</span>(</em>)?(</strong>)?</p>(\s*(<p \s*(.+?)\s*><span \s*(.+?)\s*>)+[0-9]+(\)*\.*\s+\p{Lu}\s*\p{Ll}\s*(.+?)\s*(?<!\@+)</span></p>)+)*(<p \s*(.+?)\s*><span \s*(.+?)\s*>(&ndash;)+\s*p{Ll}+\s*(.+?)\s*\p{Ll}\s*(.+?)\s*</span></p>)*";



                string input2 = collection["TekstPropisa"];
                MatchCollection _matchList2 = Regex.Matches(input2, patern1Podnaslov);
                var list2 = _matchList2.Cast<Match>().Select(match => match.Value).ToList();
                ViewBag.Lista = list2;
                int brojacClanova = 1;
                List<string> slova = new List<string>();
                slova.Add("а");
              //  slova.Add("б");
        

                foreach (string s in list2)
                {

                    //SUTRA UJUTUU OVO PRVA STVAR
                    int i = 0;
                    string slovoUClanu = slova[i];
                    List<Clan> clanovi = (from ck in _context.Clan
                                          where ck.IdPropis == id
                                          select ck).ToList();
                    string clan = "Члан " + brojacClanova;
                    int brojacClanova2 = brojacClanova - 1;
                    string clan2 = "Члан " + brojacClanova2 + slovoUClanu;
                    if (s.Contains('@'))
                    {
                        UbaciPodnaslov(id, s);
                        continue;
                    }
                    else if (s.Contains(clan))
                    {
                        UbaciClan(id, s, brojacClanova);
                        brojacClanova += 1;
                        continue;
                       

                    }
                    else if (s.Contains(clan2))
                    {

                        UbaciClanSaSlovom(id, s, brojacClanova, slovoUClanu);
                       
                        slovoUClanu = slova[i];
                        continue;
                    }
                    else if(clanovi.Count > 0)
                    {
                        string input = s; //"I Mhave a dog Mand a Mcat.";
                        List<string> _returnValue = new List<string>();
                        MatchCollection _matchList = Regex.Matches(input, patternStav);
                        var list = _matchList.Cast<Match>().Select(match => match.Value).ToList();
                        if (list.Count > 0)
                        {
                            UbaciStav(id, s);
                            

                        }
                        string inputtacka = s;
                        MatchCollection _matchListtacka = Regex.Matches(inputtacka, tackaPatern);
                        var listTacaka = _matchListtacka.Cast<Match>().Select(match => match.Value).ToList();
                        if (listTacaka.Count > 0) 
                        { 
                            foreach (string tacka in listTacaka)
                            {
                                UbaciTacku(tacka);
                            }
                        }

                        string inputAlineja = s;
                        List<string> _returnValueAlineja = new List<string>();
                        MatchCollection _matchListAlineja = Regex.Matches(inputAlineja, alinejaPatern);
                        var listAlineja = _matchListAlineja.Cast<Match>().Select(match => match.Value).ToList();
                        if(listAlineja.Count > 0) 
                        { 
                            foreach (string alineja in listAlineja)
                            {
                                UbaciAlineju(alineja);
                            }
                        }
                    }
                }
                List<Podnaslov> podnaslovi = (from pod in _context.Podnaslov
                                              where pod.IdPropis == id
                                              select pod).ToList();
                List<Clan> clanovi2 = (from clan in _context.Clan
                                       where clan.IdPropis == id
                                       select clan).ToList();
                List<int> idClanova = (from cl in _context.Clan
                                       where cl.IdPropis == id
                                       select cl.Id).ToList();
                List<Stav> stavovi = (from st in _context.Stav
                                      where idClanova.Contains(st.IdClan)
                                      select st).ToList();
                List<int> idStavova = (from st in _context.Stav
                                       where idClanova.Contains(st.IdClan)
                                       select st.Id).ToList();
                List<Tacka> tacke = (from tac in _context.Tacka
                                     where idStavova.Contains(tac.IdStav)
                                     select tac).ToList();
                List<Alineja> alineje = (from al in _context.Alineja
                                         where idStavova.Contains(al.IdStav)
                                         select al).ToList();


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

        //PROVERA DA LI PATERN KOJI JE PROSAO PRVU PROVERU PROLAZI I DRUGU I UNOS PODNASLOVA SA NIVOOM U DB

        public void UbaciPodnaslov(int id, string tekst)
        {
            Propis propis = (from p in _context.Propis
                             where p.Id == id
                             select p).Single();

            //string patern1 = @"<p \s*(.+?)\s*>(<strong>)?(<em>)?<span \s*(.+?)\s*>([0-9]+(\))+\s+)?\s*(.+?)\s*\@*</span>(</em>)?(</strong>)?</p>";

            //string input = tekst; //"I Mhave a dog Mand a Mcat.";
            //List<string> _returnValue = new List<string>();
            //MatchCollection _matchList = Regex.Matches(input, patern1);
            //var list = _matchList.Cast<Match>().Select(match => match.Value).ToList();

            Podnaslov podnaslov = new Podnaslov();

            NivoPodnaslova nivo1 = _context.NivoPodnaslova.Find(1);
            NivoPodnaslova nivo2 = _context.NivoPodnaslova.Find(2);
            NivoPodnaslova nivo3 = _context.NivoPodnaslova.Find(3);
            NivoPodnaslova nivo4 = _context.NivoPodnaslova.Find(4);
           
            int idPodnaslova = 0;

            List<Podnaslov> podnaslovi = (from pod in _context.Podnaslov
                                          select pod).ToList();
            
                //if (podnaslovi.Count > 0)
                //{
                //    idPodnaslova = (from idP in _context.Podnaslov
                //                    select idP.Id).Max();

                //}
            

            int brojacEtova = 0;
            //foreach (string c in list)
            //{
                if (tekst.Contains("@@@@"))
                {
                    brojacEtova = 4;

                }
                else if (tekst.Contains("@@@"))
                {
                    brojacEtova = 3;

                }
                else if (tekst.Contains("@@"))
                {
                    brojacEtova = 2;
                }
                else
                {
                    brojacEtova = 1;
                }
           // }
            string bezEta = tekst.Replace('@', ' ');
            if (brojacEtova == 1)
            {
                podnaslov.Id = (from idP in _context.Podnaslov
                                select idP.Id).Max() + 1;
                podnaslov.PodnaslovTekst = bezEta;
                podnaslov.IdNivoPodnaslova = nivo1.Id;
                podnaslov.IdPropis = id;
                try
                {
                    _context.Podnaslov.Add(podnaslov);
                    _context.SaveChanges();
                }
                catch
                {
                    throw;
                }

            }
            if (brojacEtova == 2)
            {
                podnaslov.Id = (from idP in _context.Podnaslov
                                select idP.Id).Max() + 1;
                podnaslov.PodnaslovTekst = bezEta;
                podnaslov.IdNivoPodnaslova = nivo2.Id;
                podnaslov.IdPropis = id;
                try
                {
                    _context.Podnaslov.Add(podnaslov);
                    _context.SaveChanges();
                }
                catch
                {
                    throw;
                }

            }
            if (brojacEtova == 3)
            {
                podnaslov.Id = (from idP in _context.Podnaslov
                                select idP.Id).Max() + 1;
                podnaslov.PodnaslovTekst = bezEta;
                podnaslov.IdNivoPodnaslova = nivo3.Id;
                podnaslov.IdPropis = id;
                try
                {
                    _context.Podnaslov.Add(podnaslov);
                    _context.SaveChanges();
                }
                catch
                {
                    throw;
                }
            }
            if (brojacEtova == 4)
            {
                podnaslov.Id = (from idP in _context.Podnaslov
                                select idP.Id).Max() + 1;
                podnaslov.PodnaslovTekst = bezEta;
                podnaslov.IdNivoPodnaslova = nivo4.Id;
                podnaslov.IdPropis = id;
                try
                {
                    _context.Podnaslov.Add(podnaslov);
                    _context.SaveChanges();
                }
                catch
                {
                    throw;
                }
            }

        }
        //UBACIVANJE CLANOVA U BP. KRECE REDOM OD CLANA 1, BROJI IH I UBACUJE U PRAVILNOM REDOSLEDU
        //static int brojacClanova = 1;
        public void UbaciClan(int id, string s, int brojacClanova)
        {

            List<Podnaslov> podnaslovi = (from pod in _context.Podnaslov
                                          where pod.IdPropis == id
                                          select pod).ToList();
            int podnaslov = 0;
            if (podnaslovi.Count > 0)
            {
                podnaslov = (from pod in _context.Podnaslov
                             where pod.IdPropis == id
                             select pod.Id).Max();
            }
            else
            {
                podnaslov = Convert.ToInt32(null);
            }

            string clanPatern = "Члан " + brojacClanova;
            //List<Clan> clanovi = (_context.Clan.ToList());
          //  int idClana = 0;
            //if (clanovi.Count > 0)
            //{
            //    idClana = (from c in _context.Clan
            //               select c.Id).Max();
            //}


            if (s.Contains(clanPatern))
            {
                Clan c = new Clan();
                c.Id = (from cl in _context.Clan
                                  select cl.Id).Max() + 1;
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
                    _context.Clan.Add(c);
                    //   brojacClanova++;
                    _context.SaveChanges();
                }
                catch
                {
                    throw;
                }
            }
        }

        //IDENRICNO KAO PRETHODNA S TIM STO BROJI I UBACUJE CLANOVE KOJI IMAJU I SLOVNU OZNAKU
        public void UbaciClanSaSlovom(int id, string s, int brojacClanova, string slovoUClanu)
        {
            List<Podnaslov> podnaslovi = (from pod in _context.Podnaslov
                                          where pod.IdPropis == id
                                          select pod).ToList();
            int podnaslov = 0;
            if (podnaslovi.Count > 0)
            {
                podnaslov = (from pod in _context.Podnaslov
                             where pod.IdPropis == id
                             select pod.Id).Max();
            }
            else
            {
                podnaslov = Convert.ToInt32(null);
            }
            brojacClanova--;
            string clanPatern = "Члан " + brojacClanova + slovoUClanu + "";
           // List<Clan> clanovi = (_context.Clan.ToList());
         //   int idClana = 0;
           // if (clanovi.Count > 0)
           // {
            //    idClana = (from c in _context.Clan
                       //    select c.Id).Max();
           // }

            if (s.Contains(clanPatern))
            {
                Clan c = new Clan();
                c.Id =  (from cl in _context.Clan
                                  select cl.Id).Max() + 1;
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
                    _context.Clan.Add(c);
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
            string pattern = @"<p \s*(.+?)\s*>(<strong>)?(<em>)?<span \s*(.+?)\s*>(\*\p{Lu}+\s*\p{Ll}+\s*(.+?)\s*)?\s*\p{L}+\s*\p{L}*\s*\p{Ll}+\s*(.+?)\s*(?<!\@+&nbsp;)(?<!\@+)(?<![0-9]+\)+)</span>(?<!<span \s*(.+?)\s*>\@+\s*(.+?)\s*</span>)(</em>)?(</strong>)?</p>";
            //List<Stav> stavovi = (from str in _context.Stav
            //                      select str).ToList();
            Stav stavClan = new Stav();

            //if (stavovi.Count > 0)
            //{

                idStava = (from sta in _context.Stav
                           select sta.Id).Max();
                stavClan = (from sc in _context.Stav
                            where sc.Id == idStava
                            select sc).Single();
          //  }

            //pattern.Replace("\"", "\"");
            string input = s; //"I Mhave a dog Mand a Mcat.";
            List<string> _returnValue = new List<string>();
            MatchCollection _matchList = Regex.Matches(input, pattern);
            var list = _matchList.Cast<Match>().Select(match => match.Value).ToList();

            foreach (string st in list)
            {
                List<Clan> clanovi = (_context.Clan.ToList());

                int idClan = (from cl in _context.Clan
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

                Stav stav = new Stav();

                try
                {

                    stav.Id =  (from sta in _context.Stav
                                         select sta.Id).Max()+1; 
                    stav.Naziv = "Став " + brojacStavova;
                    stav.Tekst = s;
                    stav.IdClan = idClan;
                    _context.Stav.Add(stav);

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
            //List<Alineja> alineja = (from str in _context.Alineja
            //                         select str).ToList();
            Alineja alinejaStav = new Alineja();

            //if (alineja.Count > 0)
            //{
                int idAlineje = (from a in _context.Alineja
                                 select a.Id).Max();

                alinejaStav = (from sc in _context.Alineja
                               where sc.Id == idAlineje
                               select sc).Single();
          //  }

            //pattern.Replace("\"", "\"");
            string input = s; //"I Mhave a dog Mand a Mcat.";
            List<string> _returnValue = new List<string>();
            MatchCollection _matchList = Regex.Matches(input, pattern);
            var list = _matchList.Cast<Match>().Select(match => match.Value).ToList();

            foreach (string st in list)
            {

                int idStav = (from stav in _context.Stav
                              select stav.Id).Max();
                if (idStav != alinejaStav.IdStav)
                {
                    brojacAlineja = 1;
                }
                else
                {
                    brojacAlineja += 1;
                }

                Alineja alineja1 = new Alineja();

                alineja1.NazivAlineje = "Alineja " + brojacAlineja;
                alineja1.Tekst = s;
                alineja1.IdStav = idStav;
                try
                {
                    _context.Alineja.Add(alineja1);

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

            string pattern = @"<p \s*(.+?)\s*>(<strong>)?(<em>)?<span \s*(.+?)\s*>\(?[0-9]+\)*\.*(\s*<em>)?\s+(\p{L})*(\p{Ll})+\s*(.+?)\s*(?<!\@+)</span>(</em>)?(</strong>)?</p>";
            //List<Tacka> tacke = (from str in _context.Tacka
            //                     select str).ToList();
            Tacka tackaStav = new Tacka();
            int idTacke = 0;
            //if (tacke.Count > 0)
            //{

                idTacke = (from tac in _context.Tacka
                           select tac.Id).Max();
                tackaStav = (from sc in _context.Tacka
                             where sc.Id == idTacke
                             select sc).Single();
           // }

           string input = s;
            MatchCollection _matchList = Regex.Matches(input, pattern);
            var list = _matchList.Cast<Match>().Select(match => match.Value).ToList();

            foreach (string st in list)
            {

                int idStav = (from stav in _context.Stav
                              select stav.Id).Max();
                if (idStav != tackaStav.IdStav)
                {
                    brojacTacaka = 1;
                }
                else
                {
                    brojacTacaka += 1;
                }

                Tacka tacka = new Tacka();
                tacka.Id = (from tac in _context.Tacka
                            select tac.Id).Max() + 1;
                tacka.Naziv = "Taчка " + brojacTacaka;
                tacka.Tekst = s;
                tacka.IdStav = idStav;
                try
                {
                    _context.Tacka.Add(tacka);

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
            List<Podnaslov> podnaslovi = (from pod in _context.Podnaslov
                                          where pod.IdPropis == id
                                          select pod).ToList();
            List<Clan> clanovi = (from cl in _context.Clan
                                  where cl.IdPropis == id
                                  select cl).ToList();
            List<Stav> stavovi = new List<Stav>();
            foreach (Clan c in clanovi)
            {
                List<Stav> stav = (from st in _context.Stav
                                   where st.IdClan == c.Id
                                   select st).ToList();
                stavovi.AddRange(stav);
            }

            List<Tacka> tacke = new List<Tacka>();
            List<Alineja> alineje = new List<Alineja>();
            if (stavovi != null)
            {
                foreach (Stav s in stavovi)
                {
                    List<Tacka> tacka = (from t in _context.Tacka
                                         where t.IdStav == s.Id
                                         select t).ToList();
                    List<Alineja> alineja = (from a in _context.Alineja
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
                    foreach (Tacka t in tacke)
                    {
                        _context.Tacka.Remove(t);
                        _context.SaveChanges();
                    }

                }
                if (alineje != null)
                {
                    foreach (Alineja a in alineje)
                    {
                        _context.Alineja.Remove(a);
                        _context.SaveChanges();
                    }
                }
                if (stavovi != null)
                {
                    foreach (Stav s in stavovi)
                    {
                        _context.Stav.Remove(s);
                        _context.SaveChanges();
                    }
                }
                if (clanovi != null)
                {
                    foreach (Clan c in clanovi)
                    {
                        _context.Clan.Remove(c);
                        _context.SaveChanges();
                    }
                }

                if (podnaslovi != null)
                {
                    foreach (Podnaslov p in podnaslovi)
                    {
                        _context.Podnaslov.Remove(p);
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
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            if (email != null)
            {
                Clan c = (from clan in _context.Clan
                          where clan.Id == id
                          select clan).SingleOrDefault();
                //List<Clan> clanovi = new List<Clan>();
                List<Clan> clanovi = (from cl in _context.Clan
                           where cl.IdPropis == c.IdPropis
                    select cl).ToList();
                ViewBag.Stavovi = clanovi;
                //Stav s = (from stav in _context.Stav
                //    where c.Id == 1
                //    select stav).SingleOrDefault();
                ViewBag.Clan = c;
                //ViewBag.IdStav = s;
                return View();
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }

        [HttpPost]
        public IActionResult DodajStav(Stav s)
        {
            string email = HttpContext.Session.GetString("UserEmail");


            if (email != null)
            {
                int idMax = (from stav in _context.Stav
                             select stav.Id).Max();
                s.Id = idMax + 1;
                try
                {
                    _context.Stav.Add(s);
                    _context.SaveChanges();
                    ViewBag.Msg = "Став је успешно убачен";
                    return RedirectPermanent("~/ObradaTeksta/DodajStav/" + s.IdClan);
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
                Propis propis = (from p in _context.Propis
                                 where p.Id == id
                                 select p).Single();
                List<Podnaslov> podnaslovi = (from pod in _context.Podnaslov
                                              where pod.IdPropis == id
                                              select pod).ToList();
                List<Clan> clanovi = (from c in _context.Clan
                                      where c.IdPropis == id
                                      select c).ToList();
                List<Stav> stavovi = new List<Stav>();

                foreach (Clan c in clanovi)
                {
                    List<Stav> stav = (from s in _context.Stav
                                       where s.IdClan == c.Id
                                       select s).ToList();
                    stavovi.AddRange(stav);

                }

                List<Tacka> tacke = new List<Tacka>();

                foreach (Stav s in stavovi)
                {
                    List<Tacka> tacka = (from t in _context.Tacka
                                         where t.IdStav == s.Id
                                         select t).ToList();
                    tacke.AddRange(tacka);
                }

                List<Alineja> alineje = new List<Alineja>();
                foreach (Stav s in stavovi)
                {
                    List<Alineja> alineja = (from a in _context.Alineja
                                             where a.IdStav == s.Id
                                             select a).ToList();
                    alineje.AddRange(alineja);
                }

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

    }
}