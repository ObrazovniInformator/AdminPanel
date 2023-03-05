using AdminPanel.Areas.Identity.Data;
using AdminPanel.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdminPanel.Controllers
{
    public class SudskaPraksaController : Controller
    {
        AdminPanelContext _context = new AdminPanelContext();

        public IActionResult Index(int id, string idRubrika, string idDonosilac)
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            if (email != null)
            {
                using(AdminPanelContext _context2 = new AdminPanelContext()) 
                {
                    int idR = Convert.ToInt32(idRubrika);
                    int idD = Convert.ToInt32(idDonosilac);
                  
                    Dictionary<int, string> sudskaPraksa = _context.SudskaPraksa
                                                                   .Select(x => new KeyValuePair<int, string>(x.Id, x.Naslov))
                                                                   .ToDictionary(x => x.Key, x => x.Value);

                    ViewBag.SudskaPraksa = sudskaPraksa;
                    List<RubrikaSP> rubrikeSP = (from r in _context2.RubrikaSP
                                                 select r).ToList();
                    List<PodrubrikaSP> podrubrikeSP = (from pod in _context2.PodrubrikaSP
                                                       select pod).ToList();
                    List<DonosilacSP> donosiociSP = (from ta in _context2.DonosilacSP
                                                     select ta).ToList();

                    ViewBag.IdPodrubrikaSP = id;
                    ViewBag.IdRubrikaSP = idR;
                    ViewBag.IdDonosilacSP = idD;
                    ViewBag.RubrikeSP = rubrikeSP;
                    ViewBag.PodrubrikeSP = podrubrikeSP;
                    ViewBag.Donosioci = donosiociSP;

                    return View(sudskaPraksa);
                }
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }

        public IActionResult Search(IFormCollection collection)
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            if (email != null)
            {
                string trazenaSudskaPraksa = collection["Naslov"];
                var sudskePrakse = from sp in _context.SudskaPraksa
                                   select sp;
                List<Propis> propisi = (from p in _context.Propis
                                            //where p.IdPodrubrike == id && p.IdRubrike == idR
                                        select p).ToList();
                List<Clan> clanovi = (from cl in _context.Clan
                                      select cl).ToList();
                List<Stav> stavovi = (from stav in _context.Stav
                                      select stav).ToList();
                List<Tacka> tacke = (from ta in _context.Tacka
                                     select ta).ToList();

                ViewBag.Propisi = propisi;
                ViewBag.Clanovi = clanovi;
                ViewBag.Stavovi = stavovi;
                ViewBag.Tacke = tacke;

                if (!String.IsNullOrEmpty(trazenaSudskaPraksa))
                {
                    sudskePrakse = sudskePrakse.Where(s => s.Naslov.Contains(trazenaSudskaPraksa));
                }
                return View(sudskePrakse);
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }

        public JsonResult GetsClanoviById(int IdPropis)
        {
            List<Clan> listaClanova = new List<Clan>();
            listaClanova = (from c in _context.Clan
                            where c.IdPropis == IdPropis
                            select c).ToList();
            listaClanova.Insert(0, new Clan { Id = 0, Naziv = "Изаберите члан" });
            return Json(new SelectList(listaClanova, "Id", "Naziv"));
        }

        public JsonResult GetsStavoviById(int IdClan)
        {
            List<Stav> listaStavova = new List<Stav>();
            listaStavova = (from s in _context.Stav
                            where s.IdClan == IdClan
                            select s).ToList();
            listaStavova.Insert(0, new Stav { Id = 0, Naziv = "Изаберите став" });
            return Json(new SelectList(listaStavova, "Id", "Naziv"));
        }

        public JsonResult GetsTackeById(int IdStav)
        {
            List<Tacka> listaTacaka = new List<Tacka>();
            listaTacaka = (from t in _context.Tacka
                           where t.IdStav == IdStav
                           select t).ToList();
            listaTacaka.Insert(0, new Tacka { Id = 0, Naziv = "Изаберите тачку" });
            return Json(new SelectList(listaTacaka, "Id", "Naziv"));
        }

        public JsonResult GetPodrubrika(int IdRubrikaSP)
        {
            List<PodrubrikaSP> listaPodrubrikaSP = new List<PodrubrikaSP>();
            listaPodrubrikaSP = (from lp in _context.PodrubrikaSP
                                 where lp.IdRubrika == IdRubrikaSP
                                 select lp).ToList();
            listaPodrubrikaSP.Insert(0, new PodrubrikaSP { Id = 0, Naziv = "Изаберите подрубрику" });
            return Json(new SelectList(listaPodrubrikaSP, "Id", "Naziv"));
        }

        [HttpGet]
        public IActionResult Create()
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            if (email != null)
            {
                List<PodrubrikaSP> podrubrikeSP = (from pod in _context.PodrubrikaSP
                                                   select pod).ToList();
                List<DonosilacSP> donosiociSP = (from ta in _context.DonosilacSP
                                                 select ta).ToList();

                List<RubrikaSP> rubrikeSP = new List<RubrikaSP>();
                rubrikeSP = (from r in _context.RubrikaSP
                             select r).ToList();
                ViewBag.ListaRubrikeSP = rubrikeSP;

                rubrikeSP.Insert(0, new RubrikaSP { Id = 0, Naziv = "--Изабери РУБРИКУ--" });


                int idSP = (from sm in _context.SudskaPraksa
                            select sm.Id).Max();
                ViewBag.IdMaxSP = idSP;
                ViewBag.PodrubrikeSP = podrubrikeSP;
                ViewBag.DonosiociSP = donosiociSP;

                return View();
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }

        [HttpPost]
        public IActionResult Create(SudskaPraksa sp, ProsvetniPropisSudskaPraksa pPropisSP)
        {
            var IdPodrubrikaSP = HttpContext.Request.Form["IdPodrubrikaSP"].ToString();
            List<RubrikaSP> rubrikeSP = new List<RubrikaSP>();
            rubrikeSP = (from r in _context.RubrikaSP
                         select r).ToList();
            ViewBag.ListaRubrikeSP = rubrikeSP;

            rubrikeSP.Insert(0, new RubrikaSP { Id = 0, Naziv = "--Изабери РУБРИКУ--" });

            List<PodrubrikaSP> podrubrikeSP = (from pod in _context.PodrubrikaSP
                                               select pod).ToList();
            List<DonosilacSP> donosiociSP = (from ta in _context.DonosilacSP
                                             select ta).ToList();

            ViewBag.PodrubrikeSP = podrubrikeSP;
            ViewBag.DonosiociSP = donosiociSP;

            SudskaPraksa s = new SudskaPraksa();
            s.Naslov = sp.Naslov;
            s.Podnaslov = sp.Podnaslov;
            s.Broj = sp.Broj;
            s.Datum = sp.Datum;
            s.Napomena = sp.Napomena;
            s.Tekst = sp.Tekst;
            s.IdRubrikaSP = sp.IdRubrikaSP;
            s.IdPodrubrikaSP = sp.IdPodrubrikaSP;
            s.IdDonosilacSP = sp.IdDonosilacSP;

            try
            {
                if (ModelState.IsValid)
                {
                    SudskaPraksa.DodajSudskuPraksu(s);
                    ViewBag.msg = "Успешно додата Судска пракса.";
                }
                else
                {
                    ViewBag.Msg = "Догодила се грешка код чувања у базу. Проверите унете податке и покушајте поново.";
                }
            }
            catch (Exception e)
            {
                PracenjeGresaka pg = new PracenjeGresaka();
                pg.Greska = e.InnerException.Message;
                pg.Datum = DateTime.Now;
                _context.PracenjeGresaka.Add(pg);
                _context.SaveChanges();
                throw;
            }

            //return RedirectToAction("Create", "SudskaPraksa");
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            //List<Propis> propisi = _context.Propis.ToList();
            List<SudskaPraksa> sudskaPraksa = (from spe in _context.SudskaPraksa
                                               where spe.Id == id
                                  select spe).ToList();

            ViewBag.SudskePrakse = sudskaPraksa;

            List<RubrikaSP> rubrikeSP = (from r in _context.RubrikaSP
                                         select r).ToList();
            List<PodrubrikaSP> podrubrikeSP = (from pod in _context.PodrubrikaSP
                                               select pod).ToList();
            List<DonosilacSP> donosiociSP = (from ta in _context.DonosilacSP
                                             select ta).ToList();

            ViewBag.RubrikeSP = rubrikeSP;
            ViewBag.PodrubrikeSP = podrubrikeSP;
            ViewBag.DonosiociSP = donosiociSP;

            SudskaPraksa sp = (from s in _context.SudskaPraksa
                               where s.Id == id
                               select s).Single();

            PropisSudskaPraksa psp = (from ps in _context.PropisSudskaPraksa
                                      where ps.IdSudskaPraksa == sp.Id 
                                      select ps).FirstOrDefault();

            ViewBag.PropisSudskaPraksa = psp;

            RubrikaSP rsp = (from r in _context.RubrikaSP
                             where r.Id == sp.IdRubrikaSP
                             select r).SingleOrDefault();

            ViewBag.RubrikaSP = rsp;
            ViewBag.SudskaPraksa = sp;

            SudskaPraksaViewModel model = new SudskaPraksaViewModel();
            model.SudskaPraksa = sp;
            model.PropisSudskaPraksa = psp;

            if (email != null)
            {
                return View(model);
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }
        [HttpPost]
        public IActionResult Edit(int id, SudskaPraksa sp, IFormCollection fcsp)
        {
            SudskaPraksa s = (from sr in _context.SudskaPraksa
                              where sr.Id == id
                              select sr).SingleOrDefault();

            RubrikaSP rsp = (from r in _context.RubrikaSP
                             where r.Id == s.IdRubrikaSP
                             select r).SingleOrDefault();

            PodrubrikaSP prsp = (from pr in _context.PodrubrikaSP
                                 where pr.Id == s.IdPodrubrikaSP
                                 select pr).SingleOrDefault();

            DonosilacSP donosilac = (from don in _context.DonosilacSP
                                     where don.Id == s.IdDonosilacSP
                                     select don).Single();

            ViewBag.RubrikaSP = rsp;
            ViewBag.PodrubrikaSP = prsp;
            ViewBag.Donosilac = donosilac;

            s.Naslov = fcsp["Naslov"];
            s.Podnaslov = fcsp["Podnaslov"];
            s.Broj = fcsp["Broj"];
            s.Datum = fcsp["Datum"];
            s.Napomena = fcsp["Napomena"];
            s.Tekst = fcsp["Tekst"];

            if (!string.IsNullOrEmpty(fcsp["ListaRubrikaSP"]))
            {
                s.IdRubrikaSP = Convert.ToInt32(fcsp["ListaRubrikaSP"]);
            }
            if (!string.IsNullOrEmpty(fcsp["ListaPodrubrikaSP"]))
            {
                s.IdPodrubrikaSP = Convert.ToInt32(fcsp["ListaPodrubrikaSP"]);
            }
            if (!string.IsNullOrEmpty(fcsp["IdDonosilacSP"]))
            {
                s.IdDonosilacSP = Convert.ToInt32(fcsp["IdDonosilacSP"]);
            }

            try
            {
                if (ModelState.IsValid)
                {
                    _context.SudskaPraksa.Update(s);
                    _context.SaveChanges();
                }
                return RedirectPermanent("~/SudskaPraksa/Index/" + s.IdRubrikaSP);
            }
            catch (Exception e)
            {
                PracenjeGresaka pg = new PracenjeGresaka();
                pg.Greska = e.InnerException.Message;
                pg.Datum = DateTime.Now;
                _context.PracenjeGresaka.Add(pg);
                _context.SaveChanges();
                throw;
            }
        }

        public IActionResult Details(int id)
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            SudskaPraksa sudskaPraksa = _context.SudskaPraksa.Find(id);

            List<int?> propisSP = (from psp in _context.PropisSudskaPraksa
                                                 where psp.IdSudskaPraksa == sudskaPraksa.Id
                                                 select psp.IdPropis).ToList();
            List<string> propisi = (from p in _context.Propis
                                    where propisSP.Contains(p.Id)
                                    select p.Naslov).ToList();
            List<int> idPropis = (from p in _context.Propis
                                  where propisSP.Contains(p.Id)
                                  select p.Id).ToList();
            List<string> clanovi = (from c in _context.Clan
                                  where idPropis.Contains(c.IdPropis)
                                  select c.Naziv).ToList();

            List<int> idClan = (from c in _context.Clan
                                where idPropis.Contains(c.IdPropis)
                                select c.Id).ToList();

            List<string> stavovi = (from s in _context.Stav
                                  where idClan.Contains(s.IdClan)
                                  select s.Naziv).ToList();

            List<int> idStava = (from s in _context.Stav
                                 where idClan.Contains(s.IdClan)
                                 select s.Id).ToList();

            List<string> tacke = (from t in _context.Tacka
                                  where idStava.Contains(t.IdStav)
                                  select t.Naziv).ToList();

            RubrikaSP rubrikaSp = (from r in _context.RubrikaSP
                                   where r.Id == sudskaPraksa.IdRubrikaSP
                                   select r).SingleOrDefault();

            PodrubrikaSP podrubrikaSp = (from p in _context.PodrubrikaSP
                                         where p.Id == sudskaPraksa.IdPodrubrikaSP
                                         select p).SingleOrDefault();

            DonosilacSP donosilac = (from d in _context.DonosilacSP
                                     where d.Id == sudskaPraksa.IdDonosilacSP
                                     select d).SingleOrDefault();

            ViewBag.Veze = propisSP;
            ViewBag.Propisi = propisi;
            ViewBag.Clanovi = clanovi;
            ViewBag.Stavovi = stavovi;
            ViewBag.Tacke = tacke;
            ViewBag.Rubrika = rubrikaSp;
            ViewBag.Podrubrika = podrubrikaSp;
            ViewBag.Donosilac = donosilac;

            if (email != null)
            {
                return View(sudskaPraksa);
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }

        public IActionResult Delete(int id)
        {
            SudskaPraksa sudskaPraksa = (from p in _context.SudskaPraksa
                                         where p.Id == id
                                         select p).Single();

            List<PropisSudskaPraksa> propisSudskaPraksa = (from psp in _context.PropisSudskaPraksa
                                                     where psp.IdSudskaPraksa == sudskaPraksa.Id
                                                     select psp).ToList();

            try
            {
                _context.SudskaPraksa.Remove(sudskaPraksa);
                _context.PropisSudskaPraksa.RemoveRange(propisSudskaPraksa);
                _context.SaveChanges();
                return RedirectPermanent("~/SudskaPraksa/Index/" + sudskaPraksa.IdPodrubrikaSP);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult CreateRubrika()
        {
            string email = HttpContext.Session.GetString("UserEmail");

            if (email != null)
            {
                return View();
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }
        [HttpPost]
        public IActionResult CreateRubrika(RubrikaSP rubrikaSP)
        {
            try
            {
                _context.RubrikaSP.Add(rubrikaSP);
                _context.SaveChanges();
                ViewBag.Msg = "Успешно сте додали рубрику за судску праксу";
                return View();
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult CreatePodrubrika()
        {
            string email = HttpContext.Session.GetString("UserEmail");

            if (email != null)
            {
                List<RubrikaSP> rubrikeSP = _context.RubrikaSP.ToList();
                ViewBag.RubrikeSP = rubrikeSP;
                return View();
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }
        [HttpPost]
        public IActionResult CreatePodrubrika(PodrubrikaSP podrubrikaSP)
        {
            List<RubrikaSP> rubrikeSP = _context.RubrikaSP.ToList();
            ViewBag.RubrikeSP = rubrikeSP;

            try
            {
                _context.PodrubrikaSP.Add(podrubrikaSP);
                _context.SaveChanges();
                ViewBag.Msg = "Успешно сте додали подрубрику за судску праксу";
                return View();
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult CreateDonosilac()
        {
            string email = HttpContext.Session.GetString("UserEmail");

            if (email != null)
            {
                return View();
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }
        [HttpPost]
        public IActionResult CreateDonosilac(DonosilacSP donosilacSP)
        {
            try
            {
                _context.DonosilacSP.Add(donosilacSP);
                _context.SaveChanges();
                ViewBag.Msg = "Успешно сте додали доносиоца за судску праксу";
                return View();
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult KreirajVezuSudskaPraksaPropis(int id,string idRubrika)
        {
            int sudskaPraksaId = (from sud in _context.SudskaPraksa
                                  select sud.Id).Max();

            ViewBag.IdMaxSP = sudskaPraksaId;

            SudskaPraksa sudskaPraksa = (from sp in _context.SudskaPraksa
                                         where sp.Id == id
                                         select sp).Single();

            Dictionary<int, string> propisi = _context.Propis
                                                      .Select(x => new KeyValuePair<int, string>(x.Id, x.Naslov))
                                                      .ToDictionary(x => x.Key, x => x.Value);
            Dictionary<int, string> clanovi = _context.Clan
                                                      .Select(x => new KeyValuePair<int, string>(x.Id, x.Naziv))
                                                      .ToDictionary(x => x.Key, x => x.Value);

            Dictionary<int, string> stavovi = _context.Stav
                                                      .Select(x => new KeyValuePair<int, string>(x.Id, x.Naziv))
                                                      .ToDictionary(x => x.Key, x => x.Value);

            Dictionary<int, string> tacke = _context.Tacka
                                                      .Select(x => new KeyValuePair<int, string>(x.Id, x.Naziv))
                                                      .ToDictionary(x => x.Key, x => x.Value);

            ViewBag.Propisi = propisi;
            ViewBag.Clanovi = clanovi;
            ViewBag.Stavovi = stavovi;
            ViewBag.Tacke = tacke;

            if (idRubrika != null)
            {
                ViewBag.Msg = "Успешно успостављена веза.";
            }
            ViewBag.SudskaPraksa = sudskaPraksa;

            return View();
        }

        [HttpPost]
        public IActionResult KreirajVezuSudskaPraksaPropis(int id,IFormCollection fc)
        {
            SudskaPraksa sudskaPraksa = _context.SudskaPraksa.Find(id);

            PropisSudskaPraksa propisSudskaPraksa = new PropisSudskaPraksa();

            propisSudskaPraksa.IdPropis = Convert.ToInt32(fc["IdPropis"]);
            propisSudskaPraksa.IdClan = Convert.ToInt32(fc["IdClan"]);
            propisSudskaPraksa.IdStav = Convert.ToInt32(fc["IdStav"]);
            propisSudskaPraksa.IdTacka = Convert.ToInt32(fc["IdTacka"]);
            propisSudskaPraksa.IdSudskaPraksa = sudskaPraksa.Id;
            propisSudskaPraksa.DatumUnosa = DateTime.Now;
            try
            {
                _context.PropisSudskaPraksa.Add(propisSudskaPraksa);
                _context.SaveChanges();
                string poruka = "1";
                return RedirectPermanent("~/SudskaPraksa/KreirajVezuSudskaPraksaPropis/" + sudskaPraksa.Id + "/" + poruka);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult KreirajVezuSudskaPraksaProsvetniPropis(int id)
        {
            SudskaPraksa sp = (from sps in _context.SudskaPraksa
                               where sps.Id == id
                               select new SudskaPraksa { Id = sps.Id, Naslov = sps.Naslov}).Single();
            List<ProsvetniPropis> prosvetniPropis = (from pp in _context.ProsvetnIPropis
                                                     select new ProsvetniPropis { Id = pp.Id, Naslov = pp.Naslov }).ToList();
            List<ClanPP> clanPP = (from cl in _context.ClanPP
                                   select new ClanPP { Id = cl.Id, Naziv = cl.Naziv }).ToList();
            List<StavPP> stavPP = (from st in _context.StavPP
                                   select new StavPP { Id = st.Id, Naziv = st.Naziv }).ToList();
            List<TackaPP> tackaPP = (from tac in _context.TackaPP
                                     select new TackaPP { Id = tac.Id, Naziv = tac.Naziv }).ToList();


            ViewBag.ProsvetniPropisi = prosvetniPropis;
            ViewBag.Clanovi = clanPP;
            ViewBag.Stavovi = stavPP;
            ViewBag.Tacke = tackaPP;
            ViewBag.SudskaPraksa = sp;
            return View();
        }

        [HttpPost]
        public IActionResult KreirajVezuSudskaPraksaProsvetniPropis(int id,IFormCollection fc)
        {
            ProsvetniPropisSudskaPraksa pps = new ProsvetniPropisSudskaPraksa();
            pps.IdSudskaPraksa = id;
            pps.IdProsvetniPropis = Convert.ToInt32(fc["IdProsvetniPropis"]);
            pps.IdClanPP = Convert.ToInt32(fc["IdClanPP"]);
            pps.IdStavPP = Convert.ToInt32(fc["IdStavPP"]);
            pps.IdTackaPP = Convert.ToInt32(fc["IdTackaPP"]);
            pps.DatumUnosa = DateTime.Now;
            ViewBag.Msg = "";
            try
            {
                _context.ProsvetniPropisSudskaPraksa.Add(pps);
                _context.SaveChanges();
                ViewBag.Msg = "Успешно убачено";
            }
            catch
            {
                throw;
            }
            SudskaPraksa sp = (from sps in _context.SudskaPraksa
                               where sps.Id == id
                               select new SudskaPraksa { Id = sps.Id, Naslov = sps.Naslov }).Single();
            List<ProsvetniPropis> prosvetniPropis = (from pp in _context.ProsvetnIPropis
                                                     select new ProsvetniPropis { Id = pp.Id, Naslov = pp.Naslov }).ToList();
            List<ClanPP> clanPP = (from cl in _context.ClanPP
                                   select new ClanPP { Id = cl.Id, Naziv = cl.Naziv }).ToList();
            List<StavPP> stavPP = (from st in _context.StavPP
                                   select new StavPP { Id = st.Id, Naziv = st.Naziv }).ToList();
            List<TackaPP> tackaPP = (from tac in _context.TackaPP
                                     select new TackaPP { Id = tac.Id, Naziv = tac.Naziv }).ToList();


            ViewBag.ProsvetniPropisi = prosvetniPropis;
            ViewBag.Clanovi = clanPP;
            ViewBag.Stavovi = stavPP;
            ViewBag.Tacke = tackaPP;
            ViewBag.SudskaPraksa = sp;
           
            return View();
        }
    }
}
