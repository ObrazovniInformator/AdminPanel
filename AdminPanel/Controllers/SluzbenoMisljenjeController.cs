using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminPanel.Areas.Identity.Data;
using AdminPanel.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdminPanel.Controllers
{
    public class SluzbenoMisljenjeController : Controller
    {
        AdminPanelContext _context = new AdminPanelContext();

        public IActionResult Index(int id, string idRubrika, string idDonosilac)
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            if (email != null)
            {
                int idR = Convert.ToInt32(idRubrika);
                int idD = Convert.ToInt32(idDonosilac);
                using (AdminPanelContext _context2 = new AdminPanelContext()) 
                {
                    //_context2.ExecuteCommand("SET TRANSACTION LEVEL READ UNCOMMITED");
                    List<SluzbenoMisljenje> sluzbenoMisljenje = (from sm in _context2.SluzbenoMisljenje
                                                                     //where sm.IdPodrubrikaSM == id && sm.IdRubrikaSM == idR
                                                                 select sm).ToList();
                    //List<Propis> propisi = (from p in _context2.Propis
                    //                            //where p.IdPodrubrike == id && p.IdRubrike == idR
                    //                        select p).ToList();
                    List<RubrikaSM> rubrikeSM = (from rsm in _context2.RubrikaSM
                                                 select rsm).ToList();
                    List<PodrubrikaSM> podrubrikeSM = (from pod in _context2.PodrubrikaSM
                                                       select pod).ToList();
                    //List<PodpodrubrikaSM> podpodrubrikeSM = (from podpod in _context.PodpodrubrikaSM
                    //                                         select podpod).ToList();
                    List<DonosilacSM> donosiociSM = (from ta in _context2.DonosilacSM
                                                     select ta).ToList();
                    //List<Clan> clanovi = (from cl in _context2.Clan
                    //                      select cl).ToList();
                    //List<Stav> stavovi = (from stav in _context2.Stav
                    //                      select stav).ToList();
                    //List<Tacka> tacke = (from ta in _context2.Tacka
                    //                     select ta).ToList();

                    ViewBag.IdPodrubrikaSM = id;
                    ViewBag.IdRubrikaSM = idR;
                    //ViewBag.IdPodpodubrikaSP = idPPR;
                    ViewBag.IdDonosilacSP = idD;
                    ViewBag.RubrikeSM = rubrikeSM;
                    ViewBag.PodrubrikeSM = podrubrikeSM;
                    ViewBag.Donosioci = donosiociSM;
                    //ViewBag.PodpodrubrikeSM = podpodrubrikeSM;
                    //ViewBag.Propisi = propisi;
                    //ViewBag.Clanovi = clanovi;
                    //ViewBag.Stavovi = stavovi;
                    //ViewBag.Tacke = tacke;

                    var model = new SluzbenoMisljenjeViewModel();
                    model.SluzbenoMisljenjeList = _context2.SluzbenoMisljenje.ToList();
                    model.PropisSluzbenoMisljenjeList = _context2.PropisSluzbenoMisljenje.ToList();

                    return View(model);
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
                string trazenoSluzbenoMisljenje = collection["Naslov"];
                var sluzbenaMisljenja = from sm in _context.SluzbenoMisljenje
                                        select sm;
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

                if (!String.IsNullOrEmpty(trazenoSluzbenoMisljenje))
                {
                    sluzbenaMisljenja = sluzbenaMisljenja.Where(s => s.Naslov.Contains(trazenoSluzbenoMisljenje));
                }
                return View(sluzbenaMisljenja);
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            if (email != null)
            {
                //List<Propis> propisi = (from p in _context.Propis
                //                            //where p.IdPodrubrike == id && p.IdRubrike == idR
                //                        select p).ToList();
                //List<RubrikaSM> rubrikeSM = (from r in _context.RubrikaSM
                //                             select r).ToList();
                List<PodrubrikaSM> podrubrikeSM = (from pod in _context.PodrubrikaSM
                                                   select pod).ToList();
                List<DonosilacSM> donosiociSM = (from ta in _context.DonosilacSM
                                                 select ta).ToList();
                //List<Clan> clanovi = (from cl in _context.Clan
                //                      select cl).ToList();
                //List<Stav> stavovi = (from stav in _context.Stav
                //                      select stav).ToList();
                //List<Tacka> tacke = (from ta in _context.Tacka
                //                     select ta).ToList();

                //List<ProsvetniPropis> propisiPP = (from p in _context.ProsvetnIPropis
                //                                   select p).ToList();
                //List<ClanPP> clanoviPP = (from cl in _context.ClanPP
                //                          select cl).ToList();
                //List<StavPP> stavoviPP = (from stav in _context.StavPP
                //                          select stav).ToList();
                //List<TackaPP> tackePP = (from ta in _context.TackaPP
                //                         select ta).ToList();

                List<RubrikaSM> rubrikeSM = new List<RubrikaSM>();

                int idSM = (from sm in _context.SluzbenoMisljenje
                            select sm.Id).Max();
                ViewBag.IdMaxSM = idSM;

                rubrikeSM = (from r in _context.RubrikaSM
                             select r).ToList();
                ViewBag.ListaRubrikeSM = rubrikeSM;

                rubrikeSM.Insert(0, new RubrikaSM { Id = 0, Naziv = "--Изабери РУБРИКУ--" });

                //ViewBag.Propisi = propisi;
                //ViewBag.Clanovi = clanovi;
                //ViewBag.Stavovi = stavovi;
                //ViewBag.Tacke = tacke;
                //ViewBag.ProsvetniPropisi = propisiPP;
                //ViewBag.ClanoviPP = clanoviPP;
                //ViewBag.StavoviPP = stavoviPP;
                //ViewBag.TackePP = tackePP;
                //ViewBag.RubrikeSM = rubrikeSM;
                ViewBag.PodrubrikeSM = podrubrikeSM;
                ViewBag.DonosiociSM = donosiociSM;

                return View();
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }
        [HttpPost]
        public IActionResult Create(SluzbenoMisljenje sm/*, ProsvetniPropisSluzbenoMisljenje pPropisSM*/)
        {
            var IdPodrubrikaSM = HttpContext.Request.Form["IdPodrubrikaSM"].ToString();
            List<RubrikaSM> rubrikeSM = new List<RubrikaSM>();
            rubrikeSM = (from r in _context.RubrikaSM
                         select r).ToList();
            ViewBag.ListaRubrikeSM = rubrikeSM;

            rubrikeSM.Insert(0, new RubrikaSM { Id = 0, Naziv = "--Изабери РУБРИКУ--" });


            //List<Propis> propisi = (from p in _context.Propis
            //                            //where p.IdPodrubrike == id && p.IdRubrike == idR
            //                        select p).ToList();
            //List<RubrikaSP> rubrikeSP = (from r in _context.RubrikaSP
            //                             select r).ToList();
            List<PodrubrikaSM> podrubrikeSM = (from pod in _context.PodrubrikaSM
                                               select pod).ToList();
            List<DonosilacSM> donosiociSM = (from ta in _context.DonosilacSM
                                             select ta).ToList();
            //List<Clan> clanovi = (from cl in _context.Clan
            //                      select cl).ToList();
            //List<Stav> stavovi = (from stav in _context.Stav
            //                      select stav).ToList();
            //List<Tacka> tacke = (from ta in _context.Tacka
            //                     select ta).ToList();

            //ViewBag.Propisi = propisi;
            //ViewBag.Clanovi = clanovi;
            //ViewBag.Stavovi = stavovi;
            //ViewBag.Tacke = tacke;
            //ViewBag.RubrikeSP = rubrikeSP;
            ViewBag.PodrubrikeSM = podrubrikeSM;
            ViewBag.DonosiociSM = donosiociSM;

            //List<ProsvetniPropis> propisiPP = (from p in _context.ProsvetnIPropis
            //                                   select p).ToList();
            //List<ClanPP> clanoviPP = (from cl in _context.ClanPP
            //                          select cl).ToList();
            //List<StavPP> stavoviPP = (from stav in _context.StavPP
            //                          select stav).ToList();
            //List<TackaPP> tackePP = (from ta in _context.TackaPP
            //                         select ta).ToList();

            //ViewBag.ProsvetniPropisi = propisiPP;
            //ViewBag.ClanoviPP = clanoviPP;
            //ViewBag.StavoviPP = stavoviPP;
            //ViewBag.TackePP = tackePP;

            SluzbenoMisljenje s = new SluzbenoMisljenje();
            s.Naslov = sm.Naslov;
            s.Podnaslov = sm.Podnaslov;
            s.Broj = sm.Broj;
            s.DatumDonosenja = sm.DatumDonosenja;
            s.Napomena = sm.Napomena;
            s.Tekst = sm.Tekst;
            //s.IdPropis = sm.IdPropis;
            //s.IdClan = sm.IdClan;
            //s.IdStav = sm.IdStav;
            //s.IdTacka = sm.IdTacka;
            s.IdRubrikaSM = sm.IdRubrikaSM;
            s.IdPodrubrikaSM = sm.IdPodrubrikaSM;
            s.IdDonosilacSM = sm.IdDonosilacSM;

            try
            {
                SluzbenoMisljenje.DodajSluzbenoMisljenje(s);
                ViewBag.msg = "Успешно додато Службено мишљење";
            }
            catch
            {
                throw;
            }

            int sluzbenoMisljenjeId = (from slm in _context.SluzbenoMisljenje
                                       select slm.Id).Max();

            //PropisSluzbenoMisljenje psm = new PropisSluzbenoMisljenje();
            //psm.IdPropis = s.IdPropis;
            //psm.IdSluzbenoMisljenje = sluzbenoMisljenjeId;
            //psm.IdClan = s.IdClan;
            //psm.IdStav = s.IdStav;
            //psm.IdTacka = s.IdTacka;
            //psm.DatumUnosa = DateTime.Now;

            //ProsvetniPropisSluzbenoMisljenje ppsm = new ProsvetniPropisSluzbenoMisljenje();
            //ppsm.IdProsvetniPropis = pPropisSM.IdProsvetniPropis;
            //ppsm.IdSluzbenoMisljenje = sluzbenoMisljenjeId;
            //ppsm.IdClanPP = pPropisSM.IdClanPP;
            //ppsm.IdStavPP = pPropisSM.IdStavPP;
            //ppsm.IdTackaPP = pPropisSM.IdTackaPP;
            //ppsm.DatumUnosa = DateTime.Now;


            //if (psm != null)
            //{
            //    try
            //    {
            //        PropisSluzbenoMisljenje.DodajPropisSluzbenoMisljenje(psm);
            //        ViewBag.Msg = "Успех";
            //    }
            //    catch
            //    {
            //        throw;
            //    }
            //}

            //if (ppsm != null)
            //{
            //    try
            //    {
            //        ProsvetniPropisSluzbenoMisljenje.DodajProsvetniPropisSluzbenoMisljenje(ppsm);
            //        ViewBag.Msg = "Успех";

            //    }
            //    catch
            //    {
            //        throw;
            //    }
            //}

            //return RedirectPermanent("~/SluzbenoMisljenje/Index/" + sm.IdPodrubrikaSM);
            return RedirectToAction("Create", "SluzbenoMisljenje");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            List<SluzbenoMisljenje> sMLista = (from smi in _context.SluzbenoMisljenje
                                                         where smi.Id == id
                                  select smi).ToList();
            List<PropisSluzbenoMisljenje> psmLista = (from psmi in _context.PropisSluzbenoMisljenje
                                                     //where piak.IdInAkta == ia.Id
                                                 select psmi).ToList();
            List<ProsvetniPropisSluzbenoMisljenje> ppsmLista = (from ppsmi in _context.ProsvetniPropisSluzbenoMisljenje
                                                                    //where ppiak.IdInAkta == ia.Id
                                                                select ppsmi).ToList();

            ViewBag.SluzbenaMisljenja = sMLista;
            ViewBag.PropisiSluzbenaMisljenja = psmLista;
            ViewBag.ProsvetniPropisiSluzbenaMisljenja = ppsmLista;

            //List<Propis> propisi = (from p in _context.Propis
            //                            //where p.IdPodrubrike == id && p.IdRubrike == idR
            //                        select p).ToList();
            List<RubrikaSM> rubrikeSM = (from r in _context.RubrikaSM
                                         select r).ToList();
            List<PodrubrikaSM> podrubrikeSM = (from pod in _context.PodrubrikaSM
                                               select pod).ToList();
            List<DonosilacSM> donosiociSM = (from ta in _context.DonosilacSM
                                             select ta).ToList();
            //List<Clan> clanovi = (from cl in _context.Clan
            //                      select cl).ToList();
            //List<Stav> stavovi = (from sta in _context.Stav
            //                      select sta).ToList();
            //List<Tacka> tacke = (from ta in _context.Tacka
            //                     select ta).ToList();

            //ViewBag.Propisi = propisi;
            ViewBag.RubrikeSM = rubrikeSM;
            ViewBag.PodrubrikeSM = podrubrikeSM;
            ViewBag.DonosiociSM = donosiociSM;
            //ViewBag.Clanovi = clanovi;
            //ViewBag.Stavovi = stavovi;
            //ViewBag.Tacke = tacke;

            //List<ProsvetniPropis> propisiPP = (from p in _context.ProsvetnIPropis
            //                                       //where p.IdPodrubrike == id && p.IdRubrike == idR
            //                                   select p).ToList();

            //List<ClanPP> clanoviPP = (from cl in _context.ClanPP
            //                          select cl).ToList();
            //List<StavPP> stavoviPP = (from sta in _context.StavPP
            //                          select sta).ToList();
            //List<TackaPP> tackePP = (from ta in _context.TackaPP
            //                         select ta).ToList();

            //ViewBag.ProsvetniPropisi = propisiPP;
            //ViewBag.Clanovipp = clanoviPP;
            //ViewBag.StavoviPP = stavoviPP;
            //ViewBag.TackePP = tackePP;

            SluzbenoMisljenje sm = (from s in _context.SluzbenoMisljenje
                                    where s.Id == id
                                    select s).Single();
            PropisSluzbenoMisljenje psm = (from ps in _context.PropisSluzbenoMisljenje
                                           where ps.IdSluzbenoMisljenje == sm.Id
                                           select ps).FirstOrDefault();

            ProsvetniPropisSluzbenoMisljenje ppsm = (from ppsmi in _context.ProsvetniPropisSluzbenoMisljenje
                                                     where ppsmi.IdSluzbenoMisljenje == sm.Id
                                                     select ppsmi).FirstOrDefault();

            //if (psm != null)
            //{
            //    Propis propis = (from p in _context.Propis
            //                     where p.Id == psm.IdPropis
            //                     select p).SingleOrDefault();

            //    Clan clan = (from cl in _context.Clan
            //                 where cl.Id == psm.IdClan
            //                 select cl).SingleOrDefault();

            //    Stav stav = (from st in _context.Stav
            //                 where st.Id == psm.IdStav
            //                 select st).SingleOrDefault();
            //    Tacka tacka = (from p in _context.Tacka
            //                   where p.Id == psm.IdTacka
            //                   select p).SingleOrDefault();

            //    ViewBag.Propis = propis;
            //    ViewBag.Clan = clan;
            //    ViewBag.Stav = stav;
            //    ViewBag.Tacka = tacka;
            //}

            //if (ppsm != null)
            //{
            //    ProsvetniPropis propisPP = (from p in _context.ProsvetnIPropis
            //                                where p.Id == ppsm.IdProsvetniPropis
            //                                select p).SingleOrDefault();

            //    ClanPP clanPP = (from cl in _context.ClanPP
            //                     where cl.Id == ppsm.IdClanPP
            //                     select cl).SingleOrDefault();

            //    StavPP stavPP = (from st in _context.StavPP
            //                     where st.Id == ppsm.IdStavPP
            //                     select st).SingleOrDefault();
            //    TackaPP tackaPP = (from p in _context.TackaPP
            //                       where p.Id == ppsm.IdTackaPP
            //                       select p).SingleOrDefault();

            //    ViewBag.PropisPP = propisPP;
            //    ViewBag.ClanPP = clanPP;
            //    ViewBag.StavPP = stavPP;
            //    ViewBag.TackaPP = tackaPP;
            //}

            RubrikaSM rsm = (from r in _context.RubrikaSM
                             where r.Id == sm.IdRubrikaSM
                             select r).SingleOrDefault();

            ViewBag.RubrikaSM = rsm;
            ViewBag.SluzbenoMisljenje = sm;

            int idSM = (from ism in _context.SluzbenoMisljenje
                        where ism.Id == id
                        select ism.Id).SingleOrDefault();
            ViewBag.IdMaxSM = idSM;

            SluzbenoMisljenjeViewModel model = new SluzbenoMisljenjeViewModel();
            model.SluzbenoMisljenje = sm;
            model.PropisSluzbenoMisljenje = psm;
            model.ProsvetniPropisSluzbenoMisljenje = ppsm;

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
        public IActionResult Edit(int id, IFormCollection sm)
        {
            SluzbenoMisljenje s = (from sr in _context.SluzbenoMisljenje
                                   where sr.Id == id
                                   select sr).Single();

            //PropisSluzbenoMisljenje propSm = (from ps in _context.PropisSluzbenoMisljenje
            //                                  where ps.IdSluzbenoMisljenje == s.Id
            //                                  select ps).SingleOrDefault();

            //ProsvetniPropisSluzbenoMisljenje prosPSm = (from pps in _context.ProsvetniPropisSluzbenoMisljenje
            //                                            where pps.IdSluzbenoMisljenje == s.Id
            //                                            select pps).FirstOrDefault();

            //if (propSm.IdPropis != null)
            //{
            //    Propis propis = (from p in _context.Propis
            //                     where p.Id == propSm.IdPropis
            //                     select p).SingleOrDefault();
            //}

            //if (propSm.IdClan != null)
            //{ 
            //    Clan clan = (from cl in _context.Clan
            //             where cl.Id == propSm.IdClan
            //             select cl).SingleOrDefault();
            //}

            //if (propSm.IdStav != null)
            //{
            //    Stav stav = (from st in _context.Stav
            //                 where st.Id == propSm.IdStav
            //                 select st).SingleOrDefault();
            //}

            RubrikaSM rsm = (from r in _context.RubrikaSM
                             where r.Id == s.IdRubrikaSM
                             select r).SingleOrDefault();

            PodrubrikaSM prsm = (from pr in _context.PodrubrikaSM
                                 where pr.Id == s.IdPodrubrikaSM
                                 select pr).Single();

            DonosilacSM donosilac = (from don in _context.DonosilacSM
                                     where don.Id == s.IdDonosilacSM
                                     select don).Single();

            //if (prosPSm.IdProsvetniPropis != null)
            //{
            //    ProsvetniPropis prosvetniPropis = (from p in _context.ProsvetnIPropis
            //                                       where p.Id == prosPSm.IdProsvetniPropis
            //                                       select p).SingleOrDefault();
            //}

            //if (prosPSm.IdClanPP != null)
            //{
            //    ClanPP clanPP = (from cl in _context.ClanPP
            //                     where cl.Id == prosPSm.IdClanPP
            //                     select cl).SingleOrDefault();
            //}

            //if (prosPSm.IdStavPP != null)
            //{
            //    StavPP stavPP = (from st in _context.StavPP
            //                     where st.Id == prosPSm.IdStavPP
            //                     select st).SingleOrDefault();
            //}

            ViewBag.RubrikaSM = rsm;
            ViewBag.PodrubrikaSM = prsm;
            ViewBag.Donosilac = donosilac;

            s.Naslov = sm["Naslov"];
            s.Podnaslov = sm["Podnaslov"];
            s.Broj = sm["Broj"];
            s.DatumDonosenja = sm["DatumDonosenja"];
            s.Napomena = sm["Napomena"];
            s.Tekst = sm["Tekst"];
            //if (!string.IsNullOrEmpty(sm["ListaPropisa"]))
            //{
            //    propSm.IdPropis = Convert.ToInt32(sm["ListaPropisa"]);
            //}
            //if (!string.IsNullOrEmpty(sm["IdClan"]))
            //{
            //    propSm.IdClan = Convert.ToInt32(sm["IdClan"]);
            //}
            //if (!string.IsNullOrEmpty(sm["IdStav"]))
            //{
            //    propSm.IdStav = Convert.ToInt32(sm["IdStav"]);
            //}
            //if (!string.IsNullOrEmpty(sm["IdTacka"]))
            //{
            //    propSm.IdTacka = Convert.ToInt32(sm["IdTacka"]);
            //}
            if (!string.IsNullOrEmpty(sm["ListaRubrikaSM"]))
            {
                s.IdRubrikaSM = Convert.ToInt32(sm["ListaRubrikaSM"]);
            }
            if (!string.IsNullOrEmpty(sm["ListaPodrubrikaSM"]))
            {
                s.IdPodrubrikaSM = Convert.ToInt32(sm["ListaPodrubrikaSM"]);
            }
            if (!string.IsNullOrEmpty(sm["IdDonosilacSM"]))
            {
                s.IdDonosilacSM = Convert.ToInt32(sm["IdDonosilacSM"]);
            }
            //if (!string.IsNullOrEmpty(sm["IdProsvetniPropis"]))
            //{
            //    prosPSm.IdProsvetniPropis = Convert.ToInt32(sm["IdProsvetniPropis"]);
            //}
            //if (!string.IsNullOrEmpty(sm["IdClanPP"]))
            //{
            //    prosPSm.IdClanPP = Convert.ToInt32(sm["IdClanPP"]);
            //}
            //if (!string.IsNullOrEmpty(sm["IdStavPP"]))
            //{
            //    prosPSm.IdStavPP = Convert.ToInt32(sm["IdStavPP"]);
            //}
            //if (!string.IsNullOrEmpty(sm["IdTackaPP"]))
            //{
            //    prosPSm.IdTackaPP = Convert.ToInt32(sm["IdTackaPP"]);
            //}

            try
            {
                _context.SluzbenoMisljenje.Update(s);
                //_context.PropisSluzbenoMisljenje.Update(propSm);
                //_context.ProsvetniPropisSluzbenoMisljenje.Update(prosPSm);
                _context.SaveChanges();
                return RedirectPermanent("~/SluzbenoMisljenje/Index/" + s.IdRubrikaSM);
            }
            catch
            {
                throw;
            }
        }

        public IActionResult Details(int id)
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            SluzbenoMisljenje sluzbenoMisljenje = _context.SluzbenoMisljenje.Find(id);
            List<PropisSluzbenoMisljenje> prsm = (from psm in _context.PropisSluzbenoMisljenje
                                                  where psm.IdSluzbenoMisljenje == sluzbenoMisljenje.Id
                                                  select psm).ToList();
            List<ProsvetniPropisSluzbenoMisljenje> ppsm = (from ppsmi in _context.ProsvetniPropisSluzbenoMisljenje
                                                           where ppsmi.IdSluzbenoMisljenje == sluzbenoMisljenje.Id
                                                           select ppsmi).ToList();
            List<Propis> propisi = _context.Propis.ToList();
            List<Clan> clanovi = _context.Clan.ToList();
            List<Stav> stavovi = _context.Stav.ToList();
            List<Tacka> tacke = _context.Tacka.ToList();
            List<ProsvetniPropis> propisiPP = _context.ProsvetnIPropis.ToList();
            List<ClanPP> clanoviPP = _context.ClanPP.ToList();
            List<StavPP> stavoviPP = _context.StavPP.ToList();
            List<TackaPP> tackePP = _context.TackaPP.ToList();
            RubrikaSM rubrika = (from r in _context.RubrikaSM
                                 where r.Id == sluzbenoMisljenje.IdRubrikaSM
                                 select r).SingleOrDefault();
            PodrubrikaSM podrubrika = (from p in _context.PodrubrikaSM
                                       where p.Id == sluzbenoMisljenje.IdPodrubrikaSM
                                       select p).SingleOrDefault();
            DonosilacSM donosilac = (from d in _context.DonosilacSM
                                     where d.Id == sluzbenoMisljenje.IdDonosilacSM
                                     select d).SingleOrDefault();


            ViewBag.Propis = propisi;
            ViewBag.Clan = clanovi;
            ViewBag.Stav = stavovi;
            ViewBag.Tacka = tacke;
            ViewBag.Veze = prsm;
            ViewBag.Veze1 = ppsm;
            ViewBag.ProsvetniPropisi = propisiPP;
            ViewBag.ClanoviPP = clanoviPP;
            ViewBag.StavoviPP = stavoviPP;
            ViewBag.TackePP = tackePP;

            ViewBag.Rubrika = rubrika;
            ViewBag.Podrubrika = podrubrika;
            ViewBag.Donosilac = donosilac;
            if (email != null)
            {
                return View(sluzbenoMisljenje);
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
           
        }

        public IActionResult Delete(int id)
        {
            SluzbenoMisljenje sluzbenoMisljenje = (from p in _context.SluzbenoMisljenje
                                                   where p.Id == id
                                                   select p).Single();

            List<PropisSluzbenoMisljenje> propisSluzbenoMisljenje = (from psm in _context.PropisSluzbenoMisljenje
                                                               where psm.IdSluzbenoMisljenje == sluzbenoMisljenje.Id
                                                               select psm).ToList();
            try
            {
                _context.SluzbenoMisljenje.Remove(sluzbenoMisljenje);
                _context.PropisSluzbenoMisljenje.RemoveRange(propisSluzbenoMisljenje);
                _context.SaveChanges();
                return RedirectPermanent("~/SluzbenoMisljenje/Index/" + sluzbenoMisljenje.IdPodrubrikaSM);
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
            ViewBag.Email = email;

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
        public IActionResult CreateRubrika(RubrikaSM rubrikaSM)
        {
            try
            {
                _context.RubrikaSM.Add(rubrikaSM);
                _context.SaveChanges();
                ViewBag.Msg = "Успешно сте додали рубрику за службено мишљење";
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
            ViewBag.Email = email;

            if (email != null)
            {
                List<RubrikaSM> rubrikeSМ = _context.RubrikaSM.ToList();
                ViewBag.RubrikeSM = rubrikeSМ;
                return View();
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }
        [HttpPost]
        public IActionResult CreatePodrubrika(PodrubrikaSM podrubrikaSM)
        {
            List<RubrikaSM> rubrikeSM = _context.RubrikaSM.ToList();
            ViewBag.RubrikeSM = rubrikeSM;

            try
            {
                _context.PodrubrikaSM.Add(podrubrikaSM);
                _context.SaveChanges();
                ViewBag.Msg = "Успешно сте додали подрубрику за службено мишљење";
                return View();
            }
            catch
            {
                throw;
            }
        }

        //[HttpGet]
        //public IActionResult CreatePodpodrubrika()
        //{
        //    string email = HttpContext.Session.GetString("UserEmail");

        //    if (email != null)
        //    {
        //        List<PodrubrikaSM> podrubrikeSM = _context.PodrubrikaSM.ToList();
        //        ViewBag.RubrikeSM = podrubrikeSM;
        //        return View();
        //    }
        //    else
        //    {
        //        return RedirectPermanent("~/Identity/Account/Login");
        //    }
        //}
        //[HttpPost]
        //public IActionResult CreatePodpodrubrika(PodpodrubrikaSM podpodrubrikaSM)
        //{
        //    List<PodrubrikaSM> podrubrikeSM = _context.PodrubrikaSM.ToList();
        //    ViewBag.RubrikeSM = podrubrikeSM;
        //    try
        //    {
        //        _context.PodpodrubrikaSM.Add(podpodrubrikaSM);
        //        _context.SaveChanges();
        //        ViewBag.Msg = "Успешно сте додали подподрубрику за службено мишљење";
        //        return View();
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

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
        public IActionResult CreateDonosilac(DonosilacSM donosilacSM)
        {
            try
            {
                _context.DonosilacSM.Add(donosilacSM);
                _context.SaveChanges();
                ViewBag.Msg = "Успешно сте додали доносиоца за службено мишљење";
                return View();
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult KreirajVezuPropisSluzbenoMisljenje(int id, string idRubrika)
        {
            int idSM = (from sm in _context.SluzbenoMisljenje
                        select sm.Id).Max();
            ViewBag.IdMaxSM = idSM;

            SluzbenoMisljenje sluzbenoMisljenje = _context.SluzbenoMisljenje.Find(id);

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


            //List<Propis> propisi = (from p in _context.Propis
            //                        select p).ToList();
            //List<Clan> clanovi = (from cl in _context.Clan
            //                      select cl).ToList();
            //List<Stav> stavovi = (from stav in _context.Stav
            //                      select stav).ToList();
            //List<Tacka> tacke = (from ta in _context.Tacka
            //                     select ta).ToList();

            ViewBag.Propisi = propisi;
            ViewBag.Clanovi = clanovi;
            ViewBag.Stavovi = stavovi;
            ViewBag.Tacke = tacke;
            ViewBag.SluzbenoMisljenje = sluzbenoMisljenje;

            if (idRubrika != null)
            {
                ViewBag.Msg = "Успешно успостављена веза";
            }

            return View();
        }
        [HttpPost]
        public IActionResult KreirajVezuPropisSluzbenoMisljenje(int id,IFormCollection fc)
        {
            SluzbenoMisljenje sluzbenoMisljenje = _context.SluzbenoMisljenje.Find(id);

            PropisSluzbenoMisljenje propisSluzbeno = new PropisSluzbenoMisljenje();

            propisSluzbeno.IdPropis = Convert.ToInt32(fc["IdPropis"]);
            propisSluzbeno.IdSluzbenoMisljenje = sluzbenoMisljenje.Id;
            propisSluzbeno.IdClan = Convert.ToInt32(fc["IdClan"]);
            propisSluzbeno.IdStav = Convert.ToInt32(fc["IdStav"]);
            propisSluzbeno.IdTacka = Convert.ToInt32(fc["IdTacka"]);
            propisSluzbeno.DatumUnosa = DateTime.Now;
            try
            {
                _context.PropisSluzbenoMisljenje.Add(propisSluzbeno);
                _context.SaveChanges();

                string poruka = "1";
                return RedirectPermanent("~/SluzbenoMisljenje/KreirajVezuPropisSluzbenoMisljenje/" + sluzbenoMisljenje.Id + "/" + poruka);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult KreirajVezuProsvetniPropisSluzbenoMisljenje(int id, string idRubrika)
        {
            SluzbenoMisljenje sluzbenoMisljenje = (from sm in _context.SluzbenoMisljenje
                                                   where sm.Id == id
                                                    select sm).FirstOrDefault();

            List<SluzbenoMisljenje> sluzbenaMisljenjaLista = (from smi in _context.SluzbenoMisljenje
                                                              where smi.Id == id
                                                                select smi).ToList();

            List<ProsvetniPropis> propisiPP = (from pr in _context.ProsvetnIPropis
                                               select new ProsvetniPropis { Id = pr.Id, Naslov = pr.Naslov }).ToList();

            List<ClanPP> clanoviPP = (from cl in _context.ClanPP
                                    select new ClanPP { Id = cl.Id, Naziv = cl.Naziv }).ToList();

            List<StavPP> stavoviPP = (from st in _context.StavPP
                                      select new StavPP { Id = st.Id, Naziv = st.Naziv }).ToList();

            List<TackaPP> tackePP = (from tac in _context.TackaPP
                                     select new TackaPP { Id = tac.Id, Naziv = tac.Naziv }).ToList();



            ViewBag.SluzbenaMisljenjaLista = sluzbenaMisljenjaLista;

            //OVDE

            //List<ProsvetniPropis> propisiPP = (from p in _context.ProsvetnIPropis
            //                                   select p).ToList();
            //List<ClanPP> clanoviPP = (from cl in _context.ClanPP
            //                          select cl).ToList();
            //List<StavPP> stavoviPP = (from stav in _context.StavPP
            //                          select stav).ToList();
            //List<TackaPP> tackePP = (from ta in _context.TackaPP
            //                         select ta).ToList();

            ViewBag.ProsvetniPropisi = propisiPP;
            ViewBag.Clanovi = clanoviPP;
            ViewBag.Stavovi = stavoviPP;
            ViewBag.Tacke = tackePP;
            if (idRubrika != null)
            {
                ViewBag.Msg = "Успешно успостављена веза";
            }

            ViewBag.SluzbenoMisljenje = sluzbenoMisljenje;

            return View();
        }
        [HttpPost]
        public IActionResult KreirajVezuProsvetniPropisSluzbenoMisljenje(int id, IFormCollection fc)
        {
            SluzbenoMisljenje sluzbenoMisljenje = (from sm in _context.SluzbenoMisljenje
                                                   where sm.Id == id
                                                   select sm).FirstOrDefault();

            List<SluzbenoMisljenje> sluzbenaMisljenjaLista = (from smi in _context.SluzbenoMisljenje
                                                              where smi.Id == id
                                                              select smi).ToList();

            ViewBag.SluzbenaMisljenjaLista = sluzbenaMisljenjaLista;

            List<ProsvetniPropis> propisiPP = (from p in _context.ProsvetnIPropis
                                               select p).ToList();
            List<ClanPP> clanoviPP = (from cl in _context.ClanPP
                                      select cl).ToList();
            List<StavPP> stavoviPP = (from stav in _context.StavPP
                                      select stav).ToList();
            List<TackaPP> tackePP = (from ta in _context.TackaPP
                                     select ta).ToList();

            ViewBag.ProsvetniPropisi = propisiPP;
            ViewBag.Clanovi = clanoviPP;
            ViewBag.Stavovi = stavoviPP;
            ViewBag.Tacke = tackePP;

            ProsvetniPropisSluzbenoMisljenje prosvetniPropisSluzbenoMisljenje = new ProsvetniPropisSluzbenoMisljenje();

            prosvetniPropisSluzbenoMisljenje.IdSluzbenoMisljenje = sluzbenoMisljenje.Id;
            prosvetniPropisSluzbenoMisljenje.IdProsvetniPropis = Convert.ToInt32(fc["IdProsvetniPropis"]);
            prosvetniPropisSluzbenoMisljenje.IdClanPP = Convert.ToInt32(fc["IdClanPP"]);
            prosvetniPropisSluzbenoMisljenje.IdStavPP = Convert.ToInt32(fc["IdStavPP"]);
            prosvetniPropisSluzbenoMisljenje.IdTackaPP = Convert.ToInt32(fc["IdTackaPP"]);
            prosvetniPropisSluzbenoMisljenje.DatumUnosa = DateTime.Now;

            try
            {
                _context.ProsvetniPropisSluzbenoMisljenje.Add(prosvetniPropisSluzbenoMisljenje);
                _context.SaveChanges();
                string poruka = "1";
                return RedirectPermanent("~/SluzbenoMisljenje/KreirajVezuProsvetniPropisSluzbenoMisljenje/" + sluzbenoMisljenje.Id + "/" + poruka);
            }
            catch
            {
                throw;
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

        public JsonResult GetPodrubrika(int IdRubrikaSM)
        {
            List<PodrubrikaSM> listaPodrubrikaSM = new List<PodrubrikaSM>();
            listaPodrubrikaSM = (from lp in _context.PodrubrikaSM
                                 where lp.IdRubrika == IdRubrikaSM
                                 select lp).ToList();
            listaPodrubrikaSM.Insert(0, new PodrubrikaSM { Id = 0, Naziv = "Изаберите подрубрику" });
            return Json(new SelectList(listaPodrubrikaSM, "Id", "Naziv"));
        }

        public JsonResult GetsClanoviPPById(int IdProsvetniPropis)
        {
            List<ClanPP> listaClanovaPP = new List<ClanPP>();
            listaClanovaPP = (from c in _context.ClanPP
                              where c.IdPropis == IdProsvetniPropis
                              select c).ToList();
            listaClanovaPP.Insert(0, new ClanPP { Id = 0, Naziv = "Изаберите члан" });
            return Json(new SelectList(listaClanovaPP, "Id", "Naziv"));
        }

        public JsonResult GetsStavoviPPById(int IdClanPP)
        {
            List<StavPP> listaStavovaPP = new List<StavPP>();
            listaStavovaPP = (from s in _context.StavPP
                              where s.IdClan == IdClanPP
                              select s).ToList();
            listaStavovaPP.Insert(0, new StavPP { Id = 0, Naziv = "Изаберите став" });
            return Json(new SelectList(listaStavovaPP, "Id", "Naziv"));
        }

        public JsonResult GetsTackePPById(int IdStavPP)
        {
            List<TackaPP> listaTacakaPP = new List<TackaPP>();
            listaTacakaPP = (from t in _context.TackaPP
                             where t.IdStav == IdStavPP
                             select t).ToList();
            listaTacakaPP.Insert(0, new TackaPP { Id = 0, Naziv = "Изаберите тачку" });
            return Json(new SelectList(listaTacakaPP, "Id", "Naziv"));
        }
    }
}