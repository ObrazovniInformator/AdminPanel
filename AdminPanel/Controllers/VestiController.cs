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
    public class VestiController : Controller
    {
        AdminPanelContext _context = new AdminPanelContext();
        public IActionResult Index()
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            if (email != null)
            {
                return View(_context.Vest.ToList());
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }

        public IActionResult Details(int id)
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            if (email != null)
            {
                Vest vest = _context.Vest.Find(id);
                VestiKategorija kategorja = _context.VestiKategorija.FirstOrDefault();
                if (vest.IdKategorija != null) { 
                 kategorja = (from k in _context.VestiKategorija
                                             where k.Id == vest.IdKategorija
                                             select k).Single();
                }
                ViewBag.Kategorija = kategorja;
                return View(vest);
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
                List<RubrikaVesti> rubrikeV = new List<RubrikaVesti>();

                int idV = (from v in _context.Vest
                            select v.Id).Max();

                ViewBag.IdMaxV = idV;

                rubrikeV = (from r in _context.RubrikaVesti
                             select r).ToList();
                ViewBag.ListaRubrikeV = rubrikeV;

                rubrikeV.Insert(0, new RubrikaVesti { Id = 0, NazivRubrike = "--Изабери РУБРИКУ--" });


                List<VestiKategorija> kategorije = _context.VestiKategorija.ToList();
                ViewBag.Kategorije = kategorije;

                return View();
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }

        [HttpPost]
        public IActionResult Create(IFormCollection fc, Vest vest)
        {
            List<RubrikaVesti> rubrikeV = new List<RubrikaVesti>();
            rubrikeV = (from r in _context.RubrikaVesti
                         select r).ToList();
            ViewBag.ListaRubrikeV = rubrikeV;

            rubrikeV.Insert(0, new RubrikaVesti { Id = 0, NazivRubrike = "--Изабери РУБРИКУ--" });

            List<VestiKategorija> kategorije = _context.VestiKategorija.ToList();
            ViewBag.Kategorije = kategorije;

            Vest v = new Vest();
            v.Naslov = fc["Naslov"];
            v.Sazetak = fc["Sazetak"];
            v.Tekst = fc["Tekst"];
            v.DanUNedelji = fc["DanUNedelji"];
            v.DanUMesecu = vest.DanUMesecu;
            v.Mesec = vest.Mesec;
            v.Godina = vest.Godina;
            v.IdRubrikaVesti = Convert.ToInt32(fc["IdRubrikaVesti"]);
            v.IdKategorija = Convert.ToInt32(fc["IdKategorija"]);
            try
            {
                if (ModelState.IsValid)
                {
                    Vest.DodajVest(v);
                    ViewBag.Msg = "Успешно убачена вест.";
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

            return View();
        }

        public IActionResult Delete(int id)
        {
            Vest vest = _context.Vest.Find(id);
            try
            {
                _context.Vest.Remove(vest);
                _context.SaveChanges();
                return RedirectToAction("Index");
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

        [HttpGet]
        public IActionResult Edit(int id)
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            if (email != null)
            {
                Vest v = _context.Vest.Find(id);
                List<VestiKategorija> kategorija = _context.VestiKategorija.ToList();
                VestiKategorija kv = _context.VestiKategorija.FirstOrDefault();
                if (v.IdKategorija != null) {
                 kv = (from k in _context.VestiKategorija
                                      where k.Id == v.IdKategorija
                                      select k).Single();
                }
                ViewBag.kv = kv;
                ViewBag.Kategorija = kategorija;
                return View(v);
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }

        [HttpPost]
        public IActionResult Edit(int id, Vest vest)
        {
            Vest v = vest;
            try
            {
                _context.Vest.Update(v);
                _context.SaveChanges();
                return RedirectToAction("Index");
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

        [HttpGet]
        public IActionResult KreirajVezuPropisVest(int id, string idRubrika)
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            if (email != null)
            {
                int idV = (from v in _context.Vest
                            select v.Id).Max();
                ViewBag.IdMaxV = idV;

                Vest vest = _context.Vest.Find(id);

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
                ViewBag.Vest = vest;

                if (idRubrika != null)
                {
                    ViewBag.Msg = "Успешно успостављена веза";
                }
            }
            return View();
        }
        [HttpPost]
        public IActionResult KreirajVezuPropisVest(int id, IFormCollection fc)
        {
            Vest vest = _context.Vest.Find(id);

            PropisVest propisVest = new PropisVest();

            propisVest.IdPropis = Convert.ToInt32(fc["IdPropis"]);
            propisVest.IdVest = vest.Id;
            propisVest.IdClan = Convert.ToInt32(fc["IdClan"]);
            propisVest.IdStav = Convert.ToInt32(fc["IdStav"]);
            propisVest.IdTacka = Convert.ToInt32(fc["IdTacka"]);
            propisVest.DatumUnosa = DateTime.Now;
            try
            {
                _context.PropisVest.Add(propisVest);
                _context.SaveChanges();

                string poruka = "1";
                return RedirectPermanent("~/Vesti/KreirajVezuPropisVest/" + vest.Id + "/" + poruka);
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
    }
}