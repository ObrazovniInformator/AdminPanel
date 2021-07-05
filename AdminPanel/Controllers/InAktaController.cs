using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AdminPanel.Models;
using AdminPanel.Data;
using AdminPanel.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdminPanel.Controllers
{
    public class InAktaController : Controller
    {
        private AdminPanelContext _context = new AdminPanelContext();
        public IActionResult Index()
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            return View();
        }

        public IActionResult Prosveta()
        {
            string email = HttpContext.Session.GetString("UserEmail");

            if (email != null)
            {
                InAktaPodvrsta podvrsteInAkta = (from pi in _context.InAktaPodvrsta
                                                 where pi.Id == 1
                                                 select pi).Single();


                //IEnumerable<RubrikaInAkta> rubrikeInAkta = (from ri in _context.RubrikaInAkta
                //                                     where ri.IdPodvrsta == 1
                //                                     select ri).ToArray();

                //IEnumerable<InAkta> inAkta = (from ia in _context.InAkta
                //                              where ia.IdPodvrsta == 1
                //                              select ia);

                Dictionary<int, string> inAkta = _context.InAkta
                   .Where(x => x.IdPodvrsta == 1)
   .Select(x => new KeyValuePair<int, string>(x.Id, x.Naslov))
   .ToDictionary(x => x.Key, x => x.Value);

                ViewData["PodvrsteInAkta"] = podvrsteInAkta;
                //  ViewData["RubrikeInAkta"] = rubrikeInAkta;
                ViewData["InAkta"] = inAkta;
                ViewBag.Email = email;

                return View();
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }

        public IActionResult JavniSektor()
        {
            string email = HttpContext.Session.GetString("UserEmail");

            if (email != null)
            {
                InAktaPodvrsta podvrsteInAkta = (from p in _context.InAktaPodvrsta
                                                 where p.Id == 2
                                                 select p).Single();
                IEnumerable<RubrikaInAkta> rubrikeInAkta = (from rub in _context.RubrikaInAkta
                                                            where rub.IdPodvrsta == 2
                                                            select rub).ToArray();
                //List<int> idRubrike = (from rub in _context.RubrikaInAkta
                //                       where rub.IdPodvrsta == 2
                //                       select rub.Id).ToList();

                //List<PodrubrikaInAkta> podrubrikeInAkta = (from pi in _context.PodrubrikaInAkta
                //                                           where idRubrike.Contains((int)pi.IdRubrika)
                //                                           select pi).ToList();
                //List<int> idPodrubrike = (from pi in _context.PodrubrikaInAkta
                //                          where idRubrike.Contains((int)pi.IdRubrika)
                //                          select pi.Id).ToList();

                //List<PodpodrubrikaInAkta> podpodrubrikeInAkta = (from ppi in _context.PodpodrubrikaInAkta
                //                                                 where idPodrubrike.Contains((int)ppi.IdPodrubrika)
                //                                                 select ppi).ToList();
                //List<int> idPodpodrubrikeInAkta = (from ppi in _context.PodpodrubrikaInAkta
                //                                   where idPodrubrike.Contains((int)ppi.IdPodrubrika)
                //                                   select ppi.Id).ToList();
                //List<PodpodpodrubrikaInAkta> podpodpodrubrikeInAkta  = (from ppi in _context.PodpodpodrubrikaInAkta
                //                                                        where idPodpodrubrikeInAkta.Contains((int)ppi.IdPodpodrubrika)
                //                                                         select ppi).ToList();
                //List<int> idPodpodpodrubrikeInAkta = (from ppi in _context.PodpodpodrubrikaInAkta
                //                                      where idPodpodrubrikeInAkta.Contains((int)ppi.IdPodpodrubrika)
                //                                      select ppi.Id).ToList();

                //List<PodpodpodpodrubrikaInAkta> podpodpodpodrubrikeInAkta = (from ppi in _context.PodpodpodpodrubrikaInAkta
                //                                                             where idPodpodpodrubrikeInAkta.Contains((int)ppi.IdPodpodpodrubrika)
                //                                                             select ppi).ToList();
                Dictionary<int, string> inAkta = _context.InAkta
                   .Where(x => x.IdPodvrsta == 2)
   .Select(x => new KeyValuePair<int, string>(x.Id, x.Naslov))
   .ToDictionary(x => x.Key, x => x.Value);

                ViewData["PodvrsteInAkta"] = podvrsteInAkta;
                ViewData["RubrikeInAkta"] = rubrikeInAkta;
                //ViewBag.PodrubrikeInAkta = podrubrikeInAkta;
                //ViewBag.PodpodrubrikeInAkta = podpodrubrikeInAkta;
                //ViewBag.PodpodpodrubrikeInAkta = podpodpodrubrikeInAkta;
                //ViewBag.PodpodpodpodrubrikeInAkta = podpodpodpodrubrikeInAkta;
                ViewData["InAkta"] = inAkta;
                ViewBag.Email = email;
                return View();
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
                List<Propis> propisi = (from p in _context.Propis
                                        select p).ToList();
                List<Clan> clanovi = (from cl in _context.Clan
                                      select cl).ToList();
                List<Stav> stavovi = (from stav in _context.Stav
                                      select stav).ToList();
                List<Tacka> tacke = (from ta in _context.Tacka
                                     select ta).ToList();

                List<ProsvetniPropis> propisiPP = (from p in _context.ProsvetnIPropis
                                                   select p).ToList();
                List<ClanPP> clanoviPP = (from cl in _context.ClanPP
                                          select cl).ToList();
                List<StavPP> stavoviPP = (from stav in _context.StavPP
                                          select stav).ToList();
                List<TackaPP> tackePP = (from ta in _context.TackaPP
                                         select ta).ToList();

                List<InAktaPodvrsta> podvrsteInAkta = new List<InAktaPodvrsta>();
                podvrsteInAkta = (from r in _context.InAktaPodvrsta
                                  select r).ToList();
                ViewBag.PodvrsteIA = podvrsteInAkta;
                podvrsteInAkta.Insert(0, new InAktaPodvrsta { Id = 0, Naziv = "--Изабери ПОДВРСТУ--" });

                List<RubrikaInAkta> rubrikeInAkta = (from r in _context.RubrikaInAkta
                                                     select r).ToList();
                ViewBag.RubrikeIA = rubrikeInAkta;

                List<PodrubrikaInAkta> podrubrikeInAkta = (from r in _context.PodrubrikaInAkta
                                                           select r).ToList();
                ViewBag.PodrubrikeIA = podrubrikeInAkta;

                List<PodpodrubrikaInAkta> podpodrubrikeInAkta = (from r in _context.PodpodrubrikaInAkta
                                                                 select r).ToList();
                ViewBag.PodpodrubrikeIA = podpodrubrikeInAkta;

                List<PodpodpodrubrikaInAkta> podpodpodrubrikeInAkta = (from r in _context.PodpodpodrubrikaInAkta
                                                                       select r).ToList();
                ViewBag.PodpodrubrikeIA = podpodrubrikeInAkta;

                List<PodpodpodpodrubrikaInAkta> podpodpodpodrubrikeInAkta = (from r in _context.PodpodpodpodrubrikaInAkta
                                                                             select r).ToList();
                ViewBag.PodpodpodpodrubrikeIA = podpodpodpodrubrikeInAkta;

                ViewBag.Propisi = propisi;
                ViewBag.Clanovi = clanovi;
                ViewBag.Stavovi = stavovi;
                ViewBag.Tacke = tacke;

                ViewBag.ProsvetniPropisi = propisiPP;
                ViewBag.ClanoviPP = clanoviPP;
                ViewBag.StavoviPP = stavoviPP;
                ViewBag.TackePP = tackePP;

                return View();
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }
        [HttpPost]
        public IActionResult Create(IFormCollection ia, PropisInAkta propIA, ProsvetniPropisInAkta prosPropIA)
        {
            var IdRubrika = HttpContext.Request.Form["IdRubrika"].ToString();
            List<RubrikaInAkta> rubrikeInAkta = new List<RubrikaInAkta>();
            rubrikeInAkta = (from r in _context.RubrikaInAkta
                             select r).ToList();
            ViewBag.RubrikeIA = rubrikeInAkta;
            rubrikeInAkta.Insert(0, new RubrikaInAkta { Id = 0, Naziv = "--Изабери РУБРИКУ--" });

            List<PodrubrikaInAkta> podrubrikeInAkta = (from r in _context.PodrubrikaInAkta
                                                       select r).ToList();
            ViewBag.PodrubrikeIA = podrubrikeInAkta;

            List<PodpodrubrikaInAkta> podpodrubrikeInAkta = (from r in _context.PodpodrubrikaInAkta
                                                             select r).ToList();
            ViewBag.PodpodrubrikeIA = podpodrubrikeInAkta;

            List<PodpodpodrubrikaInAkta> podpodpodrubrikeInAkta = (from r in _context.PodpodpodrubrikaInAkta
                                                                   select r).ToList();
            ViewBag.PodpodrubrikeIA = podpodrubrikeInAkta;

            List<PodpodpodpodrubrikaInAkta> podpodpodpodrubrikeInAkta = (from r in _context.PodpodpodpodrubrikaInAkta
                                                                         select r).ToList();
            ViewBag.PodpodpodpodrubrikeIA = podpodpodpodrubrikeInAkta;

            List<Propis> propisi = (from p in _context.Propis
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

            List<ProsvetniPropis> propisiPP = (from p in _context.ProsvetnIPropis
                                               select p).ToList();
            List<ClanPP> clanoviPP = (from cl in _context.ClanPP
                                      select cl).ToList();
            List<StavPP> stavoviPP = (from stav in _context.StavPP
                                      select stav).ToList();
            List<TackaPP> tackePP = (from ta in _context.TackaPP
                                     select ta).ToList();

            ViewBag.ProsvetniPropisi = propisiPP;
            ViewBag.ClanoviPP = clanoviPP;
            ViewBag.StavoviPP = stavoviPP;
            ViewBag.TackePP = tackePP;

            InAkta i = new InAkta();
            i.Naslov = ia["Naslov"];
            i.Tekst = ia["Tekst"];
            i.Autor = ia["Autor"];
            i.DatumObjavljivanja = ia["DatumObjavljivanja"];
            i.Napomena = ia["Napomena"];
            i.IdPodvrsta = Convert.ToInt32(ia["IdPodvrsta"]);
            i.IdRubrika = Convert.ToInt32(ia["IdRubrika"]);
            i.IdPodrubrika = Convert.ToInt32(ia["IdPodrubrika"]);
            i.IdPodpodrubrika = Convert.ToInt32(ia["IdPodpodrubrika"]);
            i.IdPodpodpodrubrika = Convert.ToInt32(ia["IdPodpodpodrubrika"]);
            // i.IdPodpodpodpodrubrika = Convert.ToInt32(ia["IdPodpodpodpodrubrika"]);
            i.IdPodpodpodpodrubrika = null;

            try
            {
                InAkta.DodajInAkta(i);
                _context.SaveChanges();
                ViewBag.msg = "Успешно додата Ин Акта";
            }
            catch
            {
                throw;
            }

            int inAktaId = i.Id;

            PropisInAkta pia = new PropisInAkta();
            pia.IdPropis = propIA.IdPropis;
            pia.IdInAkta = inAktaId;
            pia.IdClan = Convert.ToInt32(ia["IdClan"]);
            pia.IdStav = Convert.ToInt32(ia["IdStav"]);
            pia.IdTacka = Convert.ToInt32(ia["IdTacka"]);
            pia.DatumUnosa = DateTime.Now;

            ProsvetniPropisInAkta ppia = new ProsvetniPropisInAkta();
            ppia.IdProsvetniPropis = prosPropIA.IdProsvetniPropis;
            ppia.IdInAkta = inAktaId;
            ppia.IdClanPP = Convert.ToInt32(ia["IdClanPP"]);
            ppia.IdStavPP = Convert.ToInt32(ia["IdStavPP"]);
            ppia.IdTackaPP = Convert.ToInt32(ia["IdTackaPP"]);
            ppia.DatumUnosa = DateTime.Now;

            try
            {
                PropisInAkta.DodajVezuPropisInAkta(pia);
                ProsvetniPropisInAkta.DodajVezuProsvetniPropisInAkta(ppia);
                _context.SaveChanges();
                ViewBag.Msg = "Успех";
                return RedirectPermanent("~/InAkta/Index");
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            List<InAkta> iAkta = (from iak in _context.InAkta
                                  where iak.Id == id
                                  select iak).ToList();
          

            ViewBag.InAkti = iAkta;
            

            //List<Propis> propisi = (from p in _context.Propis
                                       
            //                        select p).ToList();
            List<RubrikaInAkta> rubrikeInAkta = (from r in _context.RubrikaInAkta
                                                 select r).ToList();

            //List<Clan> clanovi = (from cl in _context.Clan
            //                      select cl).ToList();
            //List<Stav> stavovi = (from sta in _context.Stav
            //                      select sta).ToList();
            //List<Tacka> tacke = (from ta in _context.Tacka
            //                     select ta).ToList();

            //ViewBag.Propisi = propisi;
            ViewBag.Rubrike = rubrikeInAkta;
            //ViewBag.Clanovi = clanovi;
            //ViewBag.Stavovi = stavovi;
            //ViewBag.Tacke = tacke;

            //List<ProsvetniPropis> propisiPP = (from p in _context.ProsvetnIPropis
                                                   
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

            InAkta ia = (from iak in _context.InAkta
                         where iak.Id == id
                         select iak).Single();
            //PropisInAkta piakta = (from piak in _context.PropisInAkta
            //                       where piak.IdInAkta == ia.Id
            //                       select piak).SingleOrDefault();
            //ProsvetniPropisInAkta ppiakta = (from ppiak in _context.ProsvetniPropisInAkta
            //                                 where ppiak.IdInAkta == ia.Id
            //                                 select ppiak).FirstOrDefault();

            //if (piakta != null)
            //{
            //    Propis propis = (from p in _context.Propis
            //                     where p.Id == piakta.IdPropis
            //                     select p).SingleOrDefault();

            //    Clan clan = (from cl in _context.Clan
            //                 where cl.Id == piakta.IdClan
            //                 select cl).SingleOrDefault();

            //    Stav stav = (from st in _context.Stav
            //                 where st.Id == piakta.IdStav
            //                 select st).SingleOrDefault();
            //    Tacka tacka = (from p in _context.Tacka
            //                   where p.Id == piakta.IdTacka
            //                   select p).SingleOrDefault();

            //    ViewBag.Propis = propis;
            //    ViewBag.Clan = clan;
            //    ViewBag.Stav = stav;
            //    ViewBag.Tacka = tacka;
            //}

            //if (ppiakta != null)
            //{
            //    if (ppiakta.IdProsvetniPropis != null)
            //    {
            //        ProsvetniPropis prosvetniPropis = (from p in _context.ProsvetnIPropis
            //                                           where p.Id == ppiakta.IdProsvetniPropis
            //                                           select p).SingleOrDefault();
            //    }

            //    if (ppiakta.IdClanPP != null)
            //    {
            //        ClanPP clanPP = (from cl in _context.ClanPP
            //                         where cl.Id == ppiakta.IdClanPP
            //                         select cl).SingleOrDefault();
            //    }

            //    if (ppiakta.IdStavPP != null)
            //    {
            //        StavPP stavPP = (from st in _context.StavPP
            //                         where st.Id == ppiakta.IdStavPP
            //                         select st).SingleOrDefault();
            //    }
            //}

            InAktaPodvrsta iap = (from iapod in _context.InAktaPodvrsta
                                  where iapod.Id == ia.IdPodvrsta
                                  select iapod).SingleOrDefault();

            RubrikaInAkta ria = (from riak in _context.RubrikaInAkta
                                 where riak.Id == ia.IdRubrika
                                 select riak).SingleOrDefault();

            PodrubrikaInAkta pria = (from priak in _context.PodrubrikaInAkta
                                     where priak.Id == ia.IdPodrubrika
                                     select priak).SingleOrDefault();

            PodpodrubrikaInAkta ppria = (from ppriak in _context.PodpodrubrikaInAkta
                                         where ppriak.Id == ia.IdPodpodrubrika
                                         select ppriak).SingleOrDefault();

            PodpodpodrubrikaInAkta pppria = (from pppriak in _context.PodpodpodrubrikaInAkta
                                             where pppriak.Id == ia.IdPodpodpodrubrika
                                             select pppriak).SingleOrDefault();

            PodpodpodpodrubrikaInAkta ppppria = (from ppppriak in _context.PodpodpodpodrubrikaInAkta
                                                 where ppppriak.Id == ia.IdPodpodpodpodrubrika
                                                 select ppppriak).SingleOrDefault();

            ViewBag.Podvrsta = iap;
            ViewBag.Rubrika = ria;
            ViewBag.Podrubrika = pria;
            ViewBag.Podpodrubrika = ppria;
            ViewBag.Podpodpodrubrika = pppria;
            ViewBag.Podpodpodpodrubrika = ppppria;

            List<InAktaPodvrsta> iaPodvrste = (from iapodvrsta in _context.InAktaPodvrsta
                                               select iapodvrsta).ToList();

            List<PodrubrikaInAkta> podrubrikeInAkta = (from iapodrubrika in _context.PodrubrikaInAkta
                                                       select iapodrubrika).ToList();

            List<PodpodrubrikaInAkta> podpodrubrikeInAkta = (from iapodpodrubrika in _context.PodpodrubrikaInAkta
                                                             select iapodpodrubrika).ToList();

            List<PodpodpodrubrikaInAkta> podpodpodrubrikeInAkta = (from iapodpodpodrubrika in _context.PodpodpodrubrikaInAkta
                                                                   select iapodpodpodrubrika).ToList();

            List<PodpodpodpodrubrikaInAkta> podpodpodpodrubrikeInAkta = (from iapodpodpodpodrubrika in _context.PodpodpodpodrubrikaInAkta
                                                                         select iapodpodpodpodrubrika).ToList();

            ViewBag.Podvrste = iaPodvrste;
            ViewBag.Podrubrike = podrubrikeInAkta;
            ViewBag.Podpodrubrike = podpodrubrikeInAkta;
            ViewBag.Podpodpodrubrike = podpodpodrubrikeInAkta;
            ViewBag.Podpodpodpodrubrike = podpodpodpodrubrikeInAkta;

            int idIA = (from iak in _context.InAkta
                        where iak.Id == id
                        select iak.Id).SingleOrDefault();
            ViewBag.IdMaxIA = idIA;

            InAktaViewModel model = new InAktaViewModel();
            model.InAkta = ia;
            //model.PropisInAkta = piakta;
            //model.ProsvetniPropisInAkta = ppiakta;

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
        public IActionResult Edit(int id, InAkta inAkta, IFormCollection fcia)
        {
            InAkta ia = (from iak in _context.InAkta
                         where iak.Id == id
                         select iak).Single();

            //PropisInAkta pia = (from piak in _context.PropisInAkta
            //                    where piak.IdInAkta == ia.Id
            //                    select piak).FirstOrDefault();

            //ProsvetniPropisInAkta ppia = (from ppiak in _context.ProsvetniPropisInAkta
            //                              where ppiak.IdInAkta == ia.Id
            //                              select ppiak).SingleOrDefault();

            //if (pia != null)
            //{
            //    if (pia.IdPropis != null)
            //    {
            //        Propis propis = (from p in _context.Propis
            //                         where p.Id == pia.IdPropis
            //                         select p).SingleOrDefault();
            //    }

            //    if (pia.IdClan != null)
            //    {
            //        Clan clan = (from cl in _context.Clan
            //                     where cl.Id == pia.IdClan
            //                     select cl).SingleOrDefault();
            //    }

            //    if (pia.IdStav != null)
            //    {
            //        Stav stav = (from st in _context.Stav
            //                     where st.Id == pia.IdStav
            //                     select st).SingleOrDefault();
            //    }
            //}

            InAktaPodvrsta iap = (from iapod in _context.InAktaPodvrsta
                                  where iapod.Id == ia.IdPodvrsta
                                  select iapod).SingleOrDefault();

            RubrikaInAkta ria = (from riak in _context.RubrikaInAkta
                                 where riak.Id == ia.IdRubrika
                                 select riak).SingleOrDefault();

            PodrubrikaInAkta pria = (from priak in _context.PodrubrikaInAkta
                                     where priak.Id == ia.IdPodrubrika
                                     select priak).SingleOrDefault();

            PodpodrubrikaInAkta ppria = (from ppriak in _context.PodpodrubrikaInAkta
                                         where ppriak.Id == ia.IdPodpodrubrika
                                         select ppriak).SingleOrDefault();

            PodpodpodrubrikaInAkta pppria = (from pppriak in _context.PodpodpodrubrikaInAkta
                                             where pppriak.Id == ia.IdPodpodpodrubrika
                                             select pppriak).SingleOrDefault();

            PodpodpodpodrubrikaInAkta ppppria = (from ppppriak in _context.PodpodpodpodrubrikaInAkta
                                                 where ppppriak.Id == ia.IdPodpodpodpodrubrika
                                                 select ppppriak).SingleOrDefault();

            //if (ppia != null)
            //{
            //    if (ppia.IdProsvetniPropis != null)
            //    {
            //        ProsvetniPropis prosvetniPropis = (from p in _context.ProsvetnIPropis
            //                                           where p.Id == ppia.IdProsvetniPropis
            //                                           select p).SingleOrDefault();
            //    }

            //    if (ppia.IdClanPP != null)
            //    {
            //        ClanPP clanPP = (from cl in _context.ClanPP
            //                         where cl.Id == ppia.IdClanPP
            //                         select cl).SingleOrDefault();
            //    }

            //    if (ppia.IdStavPP != null)
            //    {
            //        StavPP stavPP = (from st in _context.StavPP
            //                         where st.Id == ppia.IdStavPP
            //                         select st).SingleOrDefault();
            //    }
            //}

            ViewBag.Podvrsta = iap;
            ViewBag.Rubrika = ria;
            ViewBag.Podrubrika = pria;
            ViewBag.Podpodrubrika = ppria;
            ViewBag.Podpodpodrubrika = pppria;
            ViewBag.Podpodpodpodrubrika = ppppria;

            ia.Naslov = inAkta.Naslov;
            ia.Tekst = fcia["Tekst"];
            ia.Autor = inAkta.Autor;
            ia.DatumObjavljivanja = inAkta.DatumObjavljivanja;
            ia.Napomena = inAkta.Napomena;
            if (!string.IsNullOrEmpty(fcia["IdPodvrsta"]))
            {
                ia.IdPodvrsta = Convert.ToInt32(fcia["IdPodvrsta"]);
            }
            if (!string.IsNullOrEmpty(fcia["IdRubrika"]))
            {
                ia.IdRubrika = Convert.ToInt32(fcia["IdRubrika"]);
            }
            if (!string.IsNullOrEmpty(fcia["IdPodrubrika"]))
            {
                ia.IdPodrubrika = Convert.ToInt32(fcia["IdPodrubrika"]);
            }
            if (!string.IsNullOrEmpty(fcia["IdPodpodrubrika"]))
            {
                ia.IdPodpodrubrika = Convert.ToInt32(fcia["IdPodpodrubrika"]);
            }
            if (!string.IsNullOrEmpty(fcia["IdPodpodpodrubrika"]))
            {
                ia.IdPodpodpodrubrika = Convert.ToInt32(fcia["IdPodpodpodrubrika"]);
            }
            if (!string.IsNullOrEmpty(fcia["IdPodpodpodpodrubrika"]))
            {
                ia.IdPodpodpodpodrubrika = Convert.ToInt32(fcia["IdPodpodpodpodrubrika"]);
            }
            //if (!string.IsNullOrEmpty(fcia["ListaPropisa"]))
            //{
            //    pia.IdPropis = Convert.ToInt32(fcia["ListaPropisa"]);
            //}
            //if (!string.IsNullOrEmpty(fcia["IdClan"]))
            //{
            //    pia.IdClan = Convert.ToInt32(fcia["IdClan"]);
            //}
            //if (!string.IsNullOrEmpty(fcia["IdStav"]))
            //{
            //    pia.IdStav = Convert.ToInt32(fcia["IdStav"]);
            //}
            //if (!string.IsNullOrEmpty(fcia["IdTacka"]))
            //{
            //    pia.IdTacka = Convert.ToInt32(fcia["IdTacka"]);
            //}
            //if (!string.IsNullOrEmpty(fcia["IdProsvetniPropis"]))
            //{
            //    ppia.IdProsvetniPropis = Convert.ToInt32(fcia["IdProsvetniPropis"]);
            //}
            //if (!string.IsNullOrEmpty(fcia["IdClanPP"]))
            //{
            //    ppia.IdClanPP = Convert.ToInt32(fcia["IdClanPP"]);
            //}
            //if (!string.IsNullOrEmpty(fcia["IdStavPP"]))
            //{
            //    ppia.IdStavPP = Convert.ToInt32(fcia["IdStavPP"]);
            //}
            //if (!string.IsNullOrEmpty(fcia["IdTackaPP"]))
            //{
            //    ppia.IdTackaPP = Convert.ToInt32(fcia["IdTackaPP"]);
            //}

            try
            {
                _context.InAkta.Update(ia);
                //if (pia != null)
                //{
                //    _context.PropisInAkta.Update(pia);
                //}
                //if (ppia != null)
                //{
                //    _context.ProsvetniPropisInAkta.Update(ppia);
                //}
                _context.SaveChanges();
                return RedirectPermanent("~/InAkta/Index/" + ia.IdRubrika);
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

            InAkta inAkta = _context.InAkta.Find(id);
            List<PropisInAkta> propIa = (from pia in _context.PropisInAkta
                                         where pia.IdInAkta == inAkta.Id
                                         select pia).ToList();

            List<int?> idProp = (from pia in _context.PropisInAkta
                                 where pia.IdInAkta == inAkta.Id
                                 select pia.IdPropis).ToList();

            List<ProsvetniPropisInAkta> prosPropIa = (from pia in _context.ProsvetniPropisInAkta
                                                      where pia.IdInAkta == inAkta.Id
                                                      select pia).ToList();
            List<int?> idProsPrp = (from pia in _context.ProsvetniPropisInAkta
                                    where pia.IdInAkta == inAkta.Id
                                    select pia.IdProsvetniPropis).ToList();

            List<Propis> propisi = (from p in _context.Propis
                                    where idProp.Contains(p.Id)
                                    select p).ToList();

            List<Clan> clanovi = (from c in _context.Clan
                                  where idProp.Contains(c.IdPropis)
                                  select c).ToList();
            List<int> idClan = (from c in _context.Clan
                                where idProp.Contains(c.IdPropis)
                                select c.Id).ToList();
            List<Stav> stavovi = (from s in _context.Stav
                                  where idClan.Contains(s.IdClan)
                                  select s).ToList();
            List<int> idStav = (from s in _context.Stav
                                where idClan.Contains(s.IdClan)
                                select s.Id).ToList();
            List<Tacka> tacke = (from t in _context.Tacka
                                 where idStav.Contains(t.IdStav)
                                 select t).ToList();
            List<ProsvetniPropis> propisiPP = (from pp in _context.ProsvetnIPropis
                                               where idProsPrp.Contains(pp.Id)
                                               select pp).ToList();

            List<ClanPP> clanoviPP = (from c in _context.ClanPP
                                      where idProsPrp.Contains(c.IdPropis)
                                      select c).ToList();
            List<int> idClanoviPP = (from c in _context.ClanPP
                                     where idProsPrp.Contains(c.IdPropis)
                                     select c.Id).ToList();
            List<StavPP> stavoviPP = (from s in _context.StavPP
                                      where idClanoviPP.Contains(s.IdClan)
                                      select s).ToList();
            List<int> idStava = (from s in _context.StavPP
                                 where idClanoviPP.Contains(s.IdClan)
                                 select s.Id).ToList();
            List<TackaPP> tackePP = (from i in _context.TackaPP
                                     where idStava.Contains(i.IdStav)
                                     select i).ToList();

            InAktaPodvrsta podvrstaInAkta = (from p in _context.InAktaPodvrsta
                                             where p.Id == inAkta.IdPodvrsta
                                             select p).SingleOrDefault();
            RubrikaInAkta rubrikaInAkta = (from p in _context.RubrikaInAkta
                                           where p.Id == inAkta.IdRubrika
                                           select p).SingleOrDefault();
            PodrubrikaInAkta podrubrikaInAkta = (from p in _context.PodrubrikaInAkta
                                                 where p.Id == inAkta.IdPodrubrika
                                                 select p).SingleOrDefault();
            PodpodrubrikaInAkta podpodrubrikaInAkta = (from p in _context.PodpodrubrikaInAkta
                                                       where p.Id == inAkta.IdPodpodrubrika
                                                       select p).SingleOrDefault();
            PodpodpodrubrikaInAkta podpodpodrubrikaInAkta = (from p in _context.PodpodpodrubrikaInAkta
                                                             where p.Id == inAkta.IdPodpodpodrubrika
                                                             select p).SingleOrDefault();
            PodpodpodpodrubrikaInAkta podpodpodpodrubrikaInAkta = (from p in _context.PodpodpodpodrubrikaInAkta
                                                                   where p.Id == inAkta.IdPodpodpodpodrubrika
                                                                   select p).SingleOrDefault();

            ViewBag.Podvrsta = podvrstaInAkta;
            ViewBag.Rubrika = rubrikaInAkta;
            ViewBag.Podrubrika = podrubrikaInAkta;
            ViewBag.Podpodrubrika = podpodrubrikaInAkta;
            ViewBag.Podpodpodrubrika = podpodpodrubrikaInAkta;
            ViewBag.Podpodpodpodrubrika = podpodpodpodrubrikaInAkta;

            ViewBag.Propis = propisi;
            ViewBag.Clan = clanovi;
            ViewBag.Stav = stavovi;
            ViewBag.Tacka = tacke;
            ViewBag.Veze = propIa;
            ViewBag.Veze1 = prosPropIa;
            ViewBag.ProsvetniPropisi = propisiPP;
            ViewBag.ClanoviPP = clanoviPP;
            ViewBag.StavoviPP = stavoviPP;
            ViewBag.TackePP = tackePP;

            if (email != null)
            {
                return View(inAkta);
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }
        //[HttpPost]
        //public IActionResult Details(InAkta inAkta)
        //{
        //    InAkta ia = (from sr in _context.InAkta
        //                 where sr.Id == inAkta.Id
        //                 select sr).SingleOrDefault();
        //    PropisInAkta pia = (from ps in _context.PropisInAkta
        //                        where ps.IdInAkta == ia.Id
        //                        select ps).SingleOrDefault();
        //    Propis propis = (from p in _context.Propis
        //                     where p.Id == pia.IdPropis
        //                     select p).SingleOrDefault();
        //    Clan clan = (from p in _context.Clan
        //                 where p.Id == pia.IdClan
        //                 select p).SingleOrDefault();
        //    Stav stav = (from p in _context.Stav
        //                 where p.Id == pia.IdStav
        //                 select p).SingleOrDefault();
        //    Tacka tacka = (from p in _context.Tacka
        //                   where p.Id == pia.IdTacka
        //                   select p).SingleOrDefault();
        //    InAktaPodvrsta podvrstaInAkta = (from p in _context.InAktaPodvrsta
        //                                     where p.Id == inAkta.IdPodvrsta
        //                                     select p).SingleOrDefault();
        //    RubrikaInAkta rubrikaInAkta = (from p in _context.RubrikaInAkta
        //                                   where p.Id == inAkta.IdRubrika
        //                                   select p).SingleOrDefault();
        //    PodrubrikaInAkta podrubrikaInAkta = (from p in _context.PodrubrikaInAkta
        //                                         where p.Id == inAkta.IdPodrubrika
        //                                         select p).SingleOrDefault();
        //    PodpodrubrikaInAkta podpodrubrikaInAkta = (from p in _context.PodpodrubrikaInAkta
        //                                               where p.Id == inAkta.IdPodpodrubrika
        //                                               select p).SingleOrDefault();
        //    PodpodpodrubrikaInAkta podpodpodrubrikaInAkta = (from p in _context.PodpodpodrubrikaInAkta
        //                                                     where p.Id == inAkta.IdPodpodpodrubrika
        //                                                     select p).SingleOrDefault();
        //    PodpodpodpodrubrikaInAkta podpodpodpodrubrikaInAkta = (from p in _context.PodpodpodpodrubrikaInAkta
        //                                                           where p.Id == inAkta.IdPodpodpodpodrubrika
        //                                                           select p).SingleOrDefault();

        //    ViewBag.Propis = propis;
        //    ViewBag.Clan = clan;
        //    ViewBag.Stav = stav;
        //    ViewBag.Tacka = tacka;
        //    ViewBag.PodvrstaInAkta = podvrstaInAkta;
        //    ViewBag.RubrikaInAkta = rubrikaInAkta;
        //    ViewBag.PodrubrikaInAkta = podrubrikaInAkta;
        //    ViewBag.PodpodrubrikaInAkta = podpodrubrikaInAkta;
        //    ViewBag.PodpodpodrubrikaInAkta = podpodpodrubrikaInAkta;
        //    ViewBag.PodpodpodpodrubrikaInAkta = podpodpodpodrubrikaInAkta;

        //    ia.Tekst = inAkta.Tekst;

        //    try
        //    {
        //        _context.SaveChanges();
        //        ViewBag.Msg = "Успех";
        //        return View(inAkta);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        [HttpGet]
        public IActionResult CreatePodvrsta()
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
        public IActionResult CreatePodvrsta(InAktaPodvrsta podvrstaIA)
        {
            try
            {
                _context.InAktaPodvrsta.Add(podvrstaIA);
                _context.SaveChanges();
                ViewBag.Msg = "Успешно сте додали подврсту за ин акта";
                return View();
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
                List<InAktaPodvrsta> podvrsteIA = _context.InAktaPodvrsta.ToList();
                ViewBag.PodvrsteIA = podvrsteIA;
                return View();
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }
        [HttpPost]
        public IActionResult CreateRubrika(RubrikaInAkta rubrikaIA)
        {
            List<InAktaPodvrsta> podvrsteIA = _context.InAktaPodvrsta.ToList();
            ViewBag.PodvrsteIA = podvrsteIA;
            try
            {
                _context.RubrikaInAkta.Add(rubrikaIA);
                _context.SaveChanges();
                ViewBag.Msg = "Успешно сте додали рубрику за ин акта";
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
                List<RubrikaInAkta> rubrikeIA = _context.RubrikaInAkta.ToList();
                ViewBag.RubrikeIA = rubrikeIA;
                return View();
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }
        [HttpPost]
        public IActionResult CreatePodrubrika(PodrubrikaInAkta podrubrikaIA)
        {
            List<RubrikaInAkta> rubrikeIA = _context.RubrikaInAkta.ToList();
            ViewBag.RubrikeIA = rubrikeIA;

            try
            {
                _context.PodrubrikaInAkta.Add(podrubrikaIA);
                _context.SaveChanges();
                ViewBag.Msg = "Успешно сте додали подрубрику за ин акта";
                return View();
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult CreatePodpodrubrika()
        {
            string email = HttpContext.Session.GetString("UserEmail");

            if (email != null)
            {
                List<PodrubrikaInAkta> podRubrikeIA = _context.PodrubrikaInAkta.ToList();
                ViewBag.PodrubrikeIA = podRubrikeIA;
                return View();
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }
        [HttpPost]
        public IActionResult CreatePodpodrubrika(PodpodrubrikaInAkta podpodrubrikaInA)
        {
            List<PodrubrikaInAkta> podRubrikeIA = _context.PodrubrikaInAkta.ToList();
            ViewBag.PodrubrikeIA = podRubrikeIA;

            try
            {
                _context.PodpodrubrikaInAkta.Add(podpodrubrikaInA);
                _context.SaveChanges();
                ViewBag.Msg = "Успешно сте додали подподрубрику за ин акта";
                return View();
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult CreatePodpodpodrubrika()
        {
            string email = HttpContext.Session.GetString("UserEmail");

            if (email != null)
            {
                List<PodpodrubrikaInAkta> podpodRubrikeIA = _context.PodpodrubrikaInAkta.ToList();
                ViewBag.PodpodrubrikeIA = podpodRubrikeIA;
                return View();
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }
        [HttpPost]
        public IActionResult CreatePodpodpodrubrika(PodpodpodrubrikaInAkta podpodpodrubrikaIA)
        {
            List<PodpodrubrikaInAkta> podpodRubrikeIA = _context.PodpodrubrikaInAkta.ToList();
            ViewBag.PodpodrubrikeIA = podpodRubrikeIA;

            try
            {
                _context.PodpodpodrubrikaInAkta.Add(podpodpodrubrikaIA);
                _context.SaveChanges();
                ViewBag.Msg = "Успешно сте додали подподподрубрику за ин акта";
                return View();
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult CreatePodpodpodpodrubrika()
        {
            string email = HttpContext.Session.GetString("UserEmail");

            if (email != null)
            {
                List<PodpodpodrubrikaInAkta> podpodpodRubrikeIA = _context.PodpodpodrubrikaInAkta.ToList();
                ViewBag.PodpodpodrubrikeIA = podpodpodRubrikeIA;
                return View();
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }
        [HttpPost]
        public IActionResult CreatePodpodpodpodrubrika(PodpodpodpodrubrikaInAkta podpodpodpodrubrikaInAkta)
        {
            List<PodpodpodrubrikaInAkta> podpodpodRubrikeIA = _context.PodpodpodrubrikaInAkta.ToList();
            ViewBag.PodpodpodrubrikeIA = podpodpodRubrikeIA;

            try
            {
                _context.PodpodpodpodrubrikaInAkta.Add(podpodpodpodrubrikaInAkta);
                _context.SaveChanges();
                ViewBag.Msg = "Успешно сте додали подподподподрубрику за ин акта";
                return View();
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

        public JsonResult GetRubrika(int IdPodvrsta)
        {
            List<RubrikaInAkta> listaRubrikaIA = new List<RubrikaInAkta>();
            listaRubrikaIA = (from lp in _context.RubrikaInAkta
                              where lp.IdPodvrsta == IdPodvrsta
                              select lp).ToList();
            listaRubrikaIA.Insert(0, new RubrikaInAkta { Id = 0, Naziv = "Изаберите рубрику" });
            return Json(new SelectList(listaRubrikaIA, "Id", "Naziv"));
        }

        public JsonResult GetPodrubrika(int IdRubrika)
        {
            List<PodrubrikaInAkta> listaPodrubrikaIA = new List<PodrubrikaInAkta>();
            listaPodrubrikaIA = (from lp in _context.PodrubrikaInAkta
                                 where lp.IdRubrika == IdRubrika
                                 select lp).ToList();
            listaPodrubrikaIA.Insert(0, new PodrubrikaInAkta { Id = 0, Naziv = "Изаберите подрубрику" });
            return Json(new SelectList(listaPodrubrikaIA, "Id", "Naziv"));
        }

        public JsonResult GetPodpodrubrika(int IdPodrubrika)
        {
            List<PodpodrubrikaInAkta> listaPodpodrubrikaIA = new List<PodpodrubrikaInAkta>();
            listaPodpodrubrikaIA = (from lp in _context.PodpodrubrikaInAkta
                                    where lp.IdPodrubrika == IdPodrubrika
                                    select lp).ToList();
            listaPodpodrubrikaIA.Insert(0, new PodpodrubrikaInAkta { Id = 0, Naziv = "Изаберите подподрубрику" });
            return Json(new SelectList(listaPodpodrubrikaIA, "Id", "Naziv"));
        }

        public JsonResult GetPodpodpodrubrika(int IdPodpodrubrika)
        {
            List<PodpodpodrubrikaInAkta> listaPodpodpodrubrikaIA = new List<PodpodpodrubrikaInAkta>();
            listaPodpodpodrubrikaIA = (from lp in _context.PodpodpodrubrikaInAkta
                                       where lp.IdPodpodrubrika == IdPodpodrubrika
                                       select lp).ToList();
            listaPodpodpodrubrikaIA.Insert(0, new PodpodpodrubrikaInAkta { Id = 0, Naziv = "Изаберите подподподрубрику" });
            return Json(new SelectList(listaPodpodpodrubrikaIA, "Id", "Naziv"));
        }

        public JsonResult GetPodpodpodpodrubrika(int IdPodpodpodrubrika)
        {
            List<PodpodpodpodrubrikaInAkta> listaPodpodpodpodrubrikaIA = new List<PodpodpodpodrubrikaInAkta>();
            listaPodpodpodpodrubrikaIA = (from lp in _context.PodpodpodpodrubrikaInAkta
                                          where lp.IdPodpodpodrubrika == IdPodpodpodrubrika
                                          select lp).ToList();
            listaPodpodpodpodrubrikaIA.Insert(0, new PodpodpodpodrubrikaInAkta { Id = 0, Naziv = "Изаберите подподподподрубрику" });
            return Json(new SelectList(listaPodpodpodpodrubrikaIA, "Id", "Naziv"));
        }

        [HttpGet]
        public IActionResult KreirajVezuInAktaPropis(int id, string idRubrika)
        {
            InAkta inAkta = (from ia in _context.InAkta
                             where ia.Id == id
                             select ia).SingleOrDefault();

            List<InAktaPodvrsta> podvrsteInAkta = _context.InAktaPodvrsta.ToList();
            List<RubrikaInAkta> rubrikeInAkta = _context.RubrikaInAkta.ToList();
            List<PodrubrikaInAkta> podrubrikeInAkta = _context.PodrubrikaInAkta.ToList();
            List<PodpodrubrikaInAkta> podpodrubrikeInAkta = _context.PodpodrubrikaInAkta.ToList();
            List<PodpodpodrubrikaInAkta> podpodpodrubrikeInAkta = _context.PodpodpodrubrikaInAkta.ToList();
            List<PodpodpodpodrubrikaInAkta> podpodpodpodrubrikeInAkta = _context.PodpodpodpodrubrikaInAkta.ToList();
            List<InAkta> inAktaLista = (from cn in _context.InAkta
                                        where cn.Id == id
                                        select cn).ToList();

            ViewBag.PodvrsteInAkta = podvrsteInAkta;
            ViewBag.RubrikeInAkta = rubrikeInAkta;
            ViewBag.PodrubrikeInAkta = podrubrikeInAkta;
            ViewBag.PodpodrubrikeInAkta = podpodrubrikeInAkta;
            ViewBag.PodpodpodrubrikeInAkta = podpodpodrubrikeInAkta;
            ViewBag.PodpodpodpodrubrikeInAkta = podpodpodpodrubrikeInAkta;
            ViewBag.InAktaLista = inAktaLista;

            List<Propis> propisi = (from p in _context.Propis
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
            if (idRubrika != null)
            {
                ViewBag.Msg = "Успешно успостављена веза";
            }

            ViewBag.InAkta = inAkta;

            return View();
        }
        [HttpPost]
        public IActionResult KreirajVezuInAktaPropis(int id, IFormCollection fc)
        {
            InAkta inAkta = (from ia in _context.InAkta
                             where ia.Id == id
                             select ia).FirstOrDefault();

            List<InAktaPodvrsta> podvrsteInAkta = _context.InAktaPodvrsta.ToList();
            List<RubrikaInAkta> rubrikeInAkta = _context.RubrikaInAkta.ToList();
            List<PodrubrikaInAkta> podrubrikeInAkta = _context.PodrubrikaInAkta.ToList();
            List<PodpodrubrikaInAkta> podpodrubrikeInAkta = _context.PodpodrubrikaInAkta.ToList();
            List<PodpodpodrubrikaInAkta> podpodpodrubrikeInAkta = _context.PodpodpodrubrikaInAkta.ToList();
            List<PodpodpodpodrubrikaInAkta> podpodpodpodrubrikeInAkta = _context.PodpodpodpodrubrikaInAkta.ToList();
            List<InAkta> inAktaLista = (from cn in _context.InAkta
                                        where cn.Id == id
                                        select cn).ToList();

            ViewBag.PodvrsteInAkta = podvrsteInAkta;
            ViewBag.RubrikeInAkta = rubrikeInAkta;
            ViewBag.PodrubrikeInAkta = podrubrikeInAkta;
            ViewBag.PodpodrubrikeInAkta = podpodrubrikeInAkta;
            ViewBag.PodpodpodrubrikeInAkta = podpodpodrubrikeInAkta;
            ViewBag.PodpodpodpodrubrikeInAkta = podpodpodpodrubrikeInAkta;
            ViewBag.InAktaLista = inAktaLista;

            List<Propis> propisi = (from p in _context.Propis
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


            PropisInAkta propisInAkta = new PropisInAkta();

            propisInAkta.IdInAkta = inAkta.Id;
            propisInAkta.IdPropis = Convert.ToInt32(fc["IdPropis"]);
            propisInAkta.IdClan = Convert.ToInt32(fc["IdClan"]);
            propisInAkta.IdStav = Convert.ToInt32(fc["IdStav"]);
            propisInAkta.IdTacka = Convert.ToInt32(fc["IdTacka"]);
            propisInAkta.DatumUnosa = DateTime.Now;

            try
            {
                _context.PropisInAkta.Add(propisInAkta);
                _context.SaveChanges();
                string poruka = "1";
                return RedirectPermanent("~/InAkta/KreirajVezuInAktaPropis/" + inAkta.Id + "/" + poruka);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult KreirajVezuInAktaProsvetniPropis(int id, string idRubrika)
        {
            InAkta inAkta = (from ia in _context.InAkta
                             where ia.Id == id
                             select ia).FirstOrDefault();

            List<InAkta> inAktaLista = (from cn in _context.InAkta
                                        where cn.Id == id
                                        select cn).ToList();

            ViewBag.InAktaLista = inAktaLista;

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
            if (idRubrika != null)
            {
                ViewBag.Msg = "Успешно успостављена веза";
            }

            ViewBag.InAkta = inAkta;

            return View();
        }

        [HttpPost]
        public IActionResult KreirajVezuInAktaProsvetniPropis(int id, IFormCollection fc)
        {
            InAkta inAkta = (from ia in _context.InAkta
                             where ia.Id == id
                             select ia).SingleOrDefault();

            List<InAkta> inAktaLista = (from cn in _context.InAkta
                                        where cn.Id == id
                                        select cn).ToList();

            ViewBag.InAktaLista = inAktaLista;

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

            ProsvetniPropisInAkta prosvetniPropisInAkta = new ProsvetniPropisInAkta();

            prosvetniPropisInAkta.IdInAkta = inAkta.Id;
            prosvetniPropisInAkta.IdProsvetniPropis = Convert.ToInt32(fc["IdProsvetniPropis"]);
            prosvetniPropisInAkta.IdClanPP = Convert.ToInt32(fc["IdClanPP"]);
            prosvetniPropisInAkta.IdStavPP = Convert.ToInt32(fc["IdStavPP"]);
            prosvetniPropisInAkta.IdTackaPP = Convert.ToInt32(fc["IdTackaPP"]);
            prosvetniPropisInAkta.DatumUnosa = DateTime.Now;

            try
            {
                _context.ProsvetniPropisInAkta.Add(prosvetniPropisInAkta);
                _context.SaveChanges();
                string poruka = "1";
                return RedirectPermanent("~/InAkta/KreirajVezuInAktaProsvetniPropis/" + inAkta.Id + "/" + poruka);
            }
            catch
            {
                throw;
            }
        }
    }
}