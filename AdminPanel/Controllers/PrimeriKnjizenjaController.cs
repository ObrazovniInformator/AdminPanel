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
    public class PrimeriKnjizenjaController : Controller
    {
        AdminPanelContext _context = new AdminPanelContext();

        public IActionResult Index(int id, string idRubrika)
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            if (email != null)
            {
                using (AdminPanelContext _context2 = new AdminPanelContext())
                {
                    int idR = Convert.ToInt32(idRubrika);
                    List<PrimeriKnjizenja> primeriKnjizenja = (from pk in _context2.PrimeriKnjizenja
                                                               select pk).ToList();
                    List<RubrikaPK> rubrikePK = (from rpk in _context2.RubrikaPK
                                                 select rpk).ToList();

                    ViewBag.IdRubrika = idR;
                    ViewBag.Rubrike = rubrikePK;

                    var model = new PrimeriKnjizenjaViewModel();
                    model.PrimeriKnjizenjaList = _context2.PrimeriKnjizenja.ToList();
                    model.PropisPrimeriKnjizenjaList = _context2.PropisPrimeriKnjizenja.ToList();

                    return View(model);
                }
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
                List<RubrikaPK> rubrikePK = new List<RubrikaPK>();

                int idPK = (from pk in _context.PrimeriKnjizenja
                            select pk.Id).Max();

                ViewBag.IdMaxPK = idPK;

                rubrikePK = (from r in _context.RubrikaPK
                             select r).ToList();
                ViewBag.ListaRubrikePK = rubrikePK;

                rubrikePK.Insert(0, new RubrikaPK { Id = 0, Naziv = "--Изабери РУБРИКУ--" });

                return View();
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }
        [HttpPost]
        public IActionResult Create(PrimeriKnjizenja pk)
        {
            List<RubrikaPK> rubrikePK = new List<RubrikaPK>();
            rubrikePK = (from r in _context.RubrikaPK
                         select r).ToList();
            ViewBag.ListaRubrikePK = rubrikePK;

            rubrikePK.Insert(0, new RubrikaPK { Id = 0, Naziv = "--Изабери РУБРИКУ--" });

            PrimeriKnjizenja primeriKnjizenja = new PrimeriKnjizenja();
            primeriKnjizenja.Naslov = pk.Naslov;
            primeriKnjizenja.Podnaslov = pk.Podnaslov;
            primeriKnjizenja.Napomena = pk.Napomena;
            primeriKnjizenja.Tekst = pk.Tekst;
            primeriKnjizenja.IdRubrikaPK = pk.IdRubrikaPK;           

            try
            {
                if (ModelState.IsValid)
                {
                    PrimeriKnjizenja.DodajPrimerKnjizenja(primeriKnjizenja);
                    ViewBag.msg = "Успешно додат Пример књижења.";
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

            return RedirectToAction("Create", "PrimeriKnjizenja");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            List<PrimeriKnjizenja> pKLista = (from pknj in _context.PrimeriKnjizenja
                                              where pknj.Id == id
                                               select pknj).ToList();
            List<PropisPrimeriKnjizenja> ppkLista = (from ppknj in _context.PropisPrimeriKnjizenja
                                                         //where piak.IdInAkta == ia.Id
                                                     select ppknj).ToList();

            ViewBag.PrimeriKnjizenja = pKLista;
            ViewBag.PropisiPrimeriKnjizenja = ppkLista;

            List<RubrikaPK> rubrikePK = (from r in _context.RubrikaPK
                                         select r).ToList();

            ViewBag.RubrikePK = rubrikePK;

            PrimeriKnjizenja pk = (from p in _context.PrimeriKnjizenja
                                   where p.Id == id
                                    select p).Single();
            PropisPrimeriKnjizenja ppk = (from pkn in _context.PropisPrimeriKnjizenja
                                          where pkn.IdPrimeriKnjizenja == pk.Id
                                           select pkn).FirstOrDefault();

            RubrikaPK rpk = (from r in _context.RubrikaPK
                             where r.Id == pk.IdRubrikaPK
                             select r).SingleOrDefault();

            ViewBag.RubrikaPK = rpk;
            ViewBag.PrimeriKnjizenja = pk;

            int idPK = (from ism in _context.PrimeriKnjizenja
                        where ism.Id == id
                        select ism.Id).SingleOrDefault();
            ViewBag.IdMaxPK = idPK;

            PrimeriKnjizenjaViewModel model = new PrimeriKnjizenjaViewModel();
            model.PrimeriKnjizenja = pk;
            model.PropisPrimeriKnjizenja = ppk;

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
        public IActionResult Edit(int id, IFormCollection pk)
        {
            PrimeriKnjizenja pknj = (from sr in _context.PrimeriKnjizenja
                                  where sr.Id == id
                                   select sr).SingleOrDefault();

            RubrikaPK rpk = (from r in _context.RubrikaPK
                             where r.Id == pknj.IdRubrikaPK
                             select r).SingleOrDefault();

            ViewBag.RubrikaPK = rpk;

            pknj.Naslov = pk["Naslov"];
            pknj.Podnaslov = pk["Podnaslov"];
            pknj.Napomena = pk["Napomena"];
            pknj.Tekst = pk["Tekst"];

            if (!string.IsNullOrEmpty(pk["ListaRubrikaPK"]))
            {
                pknj.IdRubrikaPK = Convert.ToInt32(pk["ListaRubrikaPK"]);
            }

            try
            {
                if (ModelState.IsValid)
                {
                    _context.PrimeriKnjizenja.Update(pknj);
                    _context.SaveChanges();
                }
                return RedirectPermanent("~/PrimeriKnjizenja/Index/" + pknj.IdRubrikaPK);
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

            PrimeriKnjizenja primeriKnjizenja = _context.PrimeriKnjizenja.Find(id);
            List<PropisPrimeriKnjizenja> prpk = (from ppk in _context.PropisPrimeriKnjizenja
                                                 where ppk.IdPrimeriKnjizenja == primeriKnjizenja.Id
                                                  select ppk).ToList();
            
            List<Propis> propisi = _context.Propis.ToList();
            List<Clan> clanovi = _context.Clan.ToList();
            List<Stav> stavovi = _context.Stav.ToList();
            List<Tacka> tacke = _context.Tacka.ToList();
           
            RubrikaPK rubrika = (from r in _context.RubrikaPK
                                 where r.Id == primeriKnjizenja.IdRubrikaPK
                                 select r).SingleOrDefault();

            ViewBag.Propis = propisi;
            ViewBag.Clan = clanovi;
            ViewBag.Stav = stavovi;
            ViewBag.Tacka = tacke;
            ViewBag.Veze = prpk;
            ViewBag.Rubrika = rubrika;

            if (email != null)
            {
                return View(primeriKnjizenja);
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }

        public IActionResult Delete(int id)
        {
            PrimeriKnjizenja primeriKnjizenja = (from pk in _context.PrimeriKnjizenja
                                                 where pk.Id == id
                                                   select pk).Single();

            List<PropisPrimeriKnjizenja> propisPrimeriKnjizenja = (from ppk in _context.PropisPrimeriKnjizenja
                                                                   where ppk.IdPrimeriKnjizenja == primeriKnjizenja.Id
                                                                     select ppk).ToList();
            try
            {
                _context.PrimeriKnjizenja.Remove(primeriKnjizenja);
                _context.PropisPrimeriKnjizenja.RemoveRange(propisPrimeriKnjizenja);
                _context.SaveChanges();
                return RedirectPermanent("~/PrimeriKnjizenja/Index/" + primeriKnjizenja.IdRubrikaPK);
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
        public IActionResult CreateRubrika(RubrikaPK rubrikaPK)
        {
            try
            {
                _context.RubrikaPK.Add(rubrikaPK);
                _context.SaveChanges();
                ViewBag.Msg = "Успешно сте додали рубрику за пример књижења";
                return View();
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult KreirajVezuPropisPrimeriKnjizenja(int id, string idRubrika)
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            if (email != null)
            {
                int idPK = (from pk in _context.PrimeriKnjizenja
                            select pk.Id).Max();
                ViewBag.IdMaxPK = idPK;

                PrimeriKnjizenja primeriKnjizenja = _context.PrimeriKnjizenja.Find(id);

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
                ViewBag.PrimeriKnjizenja = primeriKnjizenja;

                if (idRubrika != null)
                {
                    ViewBag.Msg = "Успешно успостављена веза";
                }
            }
            return View();
        }
        [HttpPost]
        public IActionResult KreirajVezuPropisPrimeriKnjizenja(int id, IFormCollection fc)
        {
            PrimeriKnjizenja primeriKnjizenja = _context.PrimeriKnjizenja.Find(id);

            PropisPrimeriKnjizenja propisKnjizenja = new PropisPrimeriKnjizenja();

            propisKnjizenja.IdPropis = Convert.ToInt32(fc["IdPropis"]);
            propisKnjizenja.IdPrimeriKnjizenja = primeriKnjizenja.Id;
            propisKnjizenja.IdClan = Convert.ToInt32(fc["IdClan"]);
            propisKnjizenja.IdStav = Convert.ToInt32(fc["IdStav"]);
            propisKnjizenja.IdTacka = Convert.ToInt32(fc["IdTacka"]);
            propisKnjizenja.DatumUnosa = DateTime.Now;
            try
            {
                _context.PropisPrimeriKnjizenja.Add(propisKnjizenja);
                _context.SaveChanges();

                string poruka = "1";
                return RedirectPermanent("~/PrimeriKnjizenja/KreirajVezuPropisPrimeriKnjizenja/" + primeriKnjizenja.Id + "/" + poruka);
            }
            catch
            {
                throw;
            }
        }

        public IActionResult Search(IFormCollection collection)
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            if (email != null)
            {
                string trazenPrimerKnjizenja = collection["Naslov"];
                var primeriKnjizenja = from sm in _context.PrimeriKnjizenja
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

                if (!String.IsNullOrEmpty(trazenPrimerKnjizenja))
                {
                    primeriKnjizenja = primeriKnjizenja.Where(s => s.Naslov.Contains(trazenPrimerKnjizenja));
                }
                return View(primeriKnjizenja);
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
    }
}