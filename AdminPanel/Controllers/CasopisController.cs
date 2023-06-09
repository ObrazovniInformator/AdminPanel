using AdminPanel.Areas.Identity.Data;
using AdminPanel.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Controllers
{
    public class CasopisController : Controller
    {
        private AdminPanelContext _context = new AdminPanelContext();

        public IActionResult IspisListe(int id)
        {
            Dictionary<int, string> casopisNaslovi = _context.CasopisNaslov
                                    .Where(x => x.IdRubrika == id)
                                    .Select(x => new KeyValuePair<int, string>(x.Id, x.Naslov))
                                    .ToDictionary(x => x.Key, x => x.Value);
            ViewBag.CasopisNaslovi = casopisNaslovi;
            return View();
        }

        public IActionResult Index(int id, string idBroj)
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            if (email != null)
            {
                List<CasopisBroj> casopisBrojevi = (from cb in _context.CasopisBroj
                                                    select cb).ToList();

                List<GlavneOblastiCasopis> glavneOblastiCasopis = _context.GlavneOblastiCasopis.ToList();
                List<RubrikaCasopis> rubrike = _context.RubrikaCasopis.ToList();

                Dictionary<int, string> casopisNaslovi = _context.CasopisNaslov
               .Where(x => x.IdRubrika == id)
               .Select(x => new KeyValuePair<int, string>(x.Id, x.Naslov))
               .ToDictionary(x => x.Key, x => x.Value);

                ViewBag.GlavneOblastiCasopis = glavneOblastiCasopis;
                ViewBag.Rubrike = rubrike;
        
                ViewBag.CasopisBrojevi = casopisBrojevi;
                ViewBag.CasopisNaslov = casopisNaslovi;
                ViewBag.Email = email;
                return View();
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }

        [HttpGet]
        [Route("Casopis/Tekst/{id?}")]
        public IActionResult Tekst(int id)
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            if (email != null)
            {
               
                return View(_context.CasopisNaslov.Find(id));
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }

        [HttpPost]
        public IActionResult Tekst(int id, CasopisNaslov casopisNaslov)
        {
            CasopisNaslov cn = (from c in _context.CasopisNaslov
                                where c.Id == id
                                select c).Single();

            cn.Tekst = casopisNaslov.Tekst;
            try
            {
                _context.CasopisNaslov.Update(cn);
                _context.SaveChanges();
                return RedirectPermanent("~/Casopis/Index/" + casopisNaslov.IdRubrika);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            if (email != null)
            {
                List<CasopisOznaka> casopisOznake = (from co in _context.CasopisOznaka
                                                     select co).ToList();
                List<CasopisBroj> casopisBrojevi = (from cb in _context.CasopisBroj
                                                    select cb).ToList();
                List<RubrikaCasopis> rubrike = (from r in _context.RubrikaCasopis
                                                select r).ToList();

                List<GlavneOblastiCasopis> glavneOblastiCasopis = (from goc in _context.GlavneOblastiCasopis
                                                                   select goc).ToList();

                List<CasopisGodina> casopisGodine = new List<CasopisGodina>();
                casopisGodine = (from cg in _context.CasopisGodina
                                 select cg).ToList();
                ViewBag.ListaCasopisGodine = casopisGodine;

                casopisGodine.Insert(0, new CasopisGodina { Id = 0, Naziv = "--Изабери ГОДИНУ--" });

                ViewBag.CasopisOznake = casopisOznake;
                ViewBag.CasopisBrojevi = casopisBrojevi;
                ViewBag.Rubrike = rubrike;
                ViewBag.GlavneOblastiCasopis = glavneOblastiCasopis;
                ViewBag.Email = email;

                return View();
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }

        [HttpPost]
        public IActionResult Create(IFormCollection cn, CasopisNaslov casopisN, PropisCasopis propCas)
        {
            var IdBroj = HttpContext.Request.Form["IdBroj"].ToString();

            List<CasopisGodina> casopisGodine = new List<CasopisGodina>();
            casopisGodine = (from cg in _context.CasopisGodina
                             select cg).ToList();
            ViewBag.ListaCasopisGodine = casopisGodine;

            casopisGodine.Insert(0, new CasopisGodina { Id = 0, Naziv = "--Изабери ГОДИНУ--" });

            List<CasopisOznaka> casopisOznake = (from co in _context.CasopisOznaka
                                                 select co).ToList();
            List<CasopisBroj> casopisBrojevi = (from cb in _context.CasopisBroj
                                                select cb).ToList();
            List<RubrikaCasopis> rubrike = (from r in _context.RubrikaCasopis
                                            select r).ToList();
            List<GlavneOblastiCasopis> glavneOblastiCasopis = (from goc in _context.GlavneOblastiCasopis
                                                               select goc).ToList();

            ViewBag.CasopisOznake = casopisOznake;
            ViewBag.CasopisBrojevi = casopisBrojevi;
            ViewBag.Rubrike = rubrike;
            ViewBag.GlavneOblastiCasopis = glavneOblastiCasopis;

            CasopisNaslov casopis = new CasopisNaslov();

            casopis.Naslov = cn["Naslov"];
            casopis.Tekst = cn["Tekst"];
            casopis.DatumObjavljivanja = cn["DatumObjavljivanja"];
            casopis.Autor = cn["Autor"];
            casopis.IdOznaka = casopisN.IdOznaka;
            casopis.IdGodina = Convert.ToInt32(cn["IdGodina"]);
            casopis.IdBroj = Convert.ToInt32(cn["IdBroj"]);
            casopis.IdRubrika = Convert.ToInt32(cn["IdRubrika"]);
            casopis.IdPodrubrika = Convert.ToInt32(cn["IdPodrubrika"]);
            casopis.IdOblast = Convert.ToInt32(cn["IdOblast"]);

            try
            {
                if (ModelState.IsValid)
                {
                    CasopisNaslov.DodajCasopis(casopis);
                    ViewBag.msg = "Успешно додат Часопис.";
                }
                else
                {
                    ViewBag.Msg = "Догодила се грешка код чувања у базу. Проверите унете податке и покушајте поново. Проверите да ли сте унели Наслов и/или Област који су обавезна поља.";
                }
                //return RedirectPermanent("~/Casopis/IspisListe/" + casopis.IdRubrika);
                return View();
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

            List<RubrikaCasopis> rubrikeCasopis = (from r in _context.RubrikaCasopis
                                                   select r).ToList();
            List<PodrubrikaCasopis> podrubrike = (from p in _context.PodrubrikaCasopis
                                                  select p).ToList();

            ViewBag.RubrikeCasopis = rubrikeCasopis;
            ViewBag.PodrubrikeCasopis = podrubrike;


            CasopisNaslov cn = (from s in _context.CasopisNaslov
                                where s.Id == id
                                select s).Single();

            RubrikaCasopis rc = (from r in _context.RubrikaCasopis
                                 where r.ID == cn.IdRubrika
                                 select r).SingleOrDefault();

            PodrubrikaCasopis prc = (from pr in _context.PodrubrikaCasopis
                                     where pr.Id == cn.IdPodrubrika
                                     select pr).SingleOrDefault();

            GlavneOblastiCasopis goca = (from gocas in _context.GlavneOblastiCasopis
                                        where gocas.ID == cn.IdOblast
                                        select gocas).SingleOrDefault();

            CasopisGodina cgo = (from cgod in _context.CasopisGodina
                                where cgod.Id == cn.IdGodina
                                 select cgod).SingleOrDefault();

            CasopisBroj cbr = (from cbro in _context.CasopisBroj
                               where cbro.Id == cn.IdBroj
                                 select cbro).SingleOrDefault();

            ViewBag.RubrikaCasopis = rc;
            ViewBag.PodrubrikaCasopis = prc;
            ViewBag.GlavnaOblastCasopis = goca;
            ViewBag.CasopisGodina = cgo;
            ViewBag.CasopisBroj = cbr;
            ViewBag.CasopisNaslov = cn;

            List<GlavneOblastiCasopis> goc = (from gocas in _context.GlavneOblastiCasopis
                                        select gocas).ToList();

            List<CasopisBroj> cb = (from cbroj in _context.CasopisBroj
                                              select cbroj).ToList();

            List<CasopisGodina> cg = (from cgod in _context.CasopisGodina
                                      select cgod).ToList();

            List<CasopisOznaka> co = (from coz in _context.CasopisOznaka
                                      select coz).ToList();

            ViewBag.GlavneOblastiCasopis = goc;
            ViewBag.CasopisBrojevi = cb;
            ViewBag.ListaCasopisGodine = cg;
            ViewBag.CasopisOznake = co;

            int idC = (from c in _context.CasopisNaslov
                       where c.Id == id
                       select c.Id).SingleOrDefault();
            ViewBag.IdMaxC = idC;

            CasopisViewModel model = new CasopisViewModel();
            model.CasopisNaslov = cn;

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
        public IActionResult Edit(int id, CasopisNaslov casopisNaslov, IFormCollection fcc)
        {
            CasopisNaslov cn = (from c in _context.CasopisNaslov
                                where c.Id == id
                                select c).SingleOrDefault();

            RubrikaCasopis rc = (from r in _context.RubrikaCasopis
                                 where r.ID == cn.IdRubrika
                                 select r).SingleOrDefault();

            PodrubrikaCasopis prc = (from pr in _context.PodrubrikaCasopis
                                     where pr.Id == cn.IdPodrubrika
                                     select pr).SingleOrDefault();

            GlavneOblastiCasopis goc = (from gocas in _context.GlavneOblastiCasopis
                                        where gocas.ID == cn.IdOblast
                                        select gocas).SingleOrDefault();

            CasopisGodina cgo = (from cgod in _context.CasopisGodina
                                 where cgod.Id == cn.IdGodina
                                 select cgod).SingleOrDefault();

            CasopisBroj cbr = (from cbro in _context.CasopisBroj
                               where cbro.Id == cn.IdBroj
                               select cbro).SingleOrDefault();

            ViewBag.RubrikaCasopis = rc;
            ViewBag.PodrubrikaCasopis = prc;
            ViewBag.GlavnaOblastCasopis = goc;
            ViewBag.CasopisGodina = cgo;
            ViewBag.CasopisBroj = cbr;

            cn.Naslov = casopisNaslov.Naslov;
            cn.Tekst = fcc["Tekst"];
            cn.DatumObjavljivanja = casopisNaslov.DatumObjavljivanja;
            cn.Autor = casopisNaslov.Autor;

            if (!string.IsNullOrEmpty(fcc["IdOznaka"]))
            {
                cn.IdOznaka = Convert.ToInt32(fcc["IdOznaka"]);
            }
            if (!string.IsNullOrEmpty(fcc["IdGodina"]))
            {
                cn.IdGodina = Convert.ToInt32(fcc["IdGodina"]);
            }
            if (!string.IsNullOrEmpty(fcc["IdBroj"]))
            {
                cn.IdBroj = Convert.ToInt32(fcc["IdBroj"]);
            }
            if (!string.IsNullOrEmpty(fcc["IdRubrika"]))
            {
                cn.IdRubrika = Convert.ToInt32(fcc["IdRubrika"]);
            }
            if (!string.IsNullOrEmpty(fcc["IdPodrubrika"]))
            {
                cn.IdPodrubrika = Convert.ToInt32(fcc["IdPodrubrika"]);
            }
            if (!string.IsNullOrEmpty(fcc["IdOblast"]))
            {
                cn.IdOblast = Convert.ToInt32(fcc["IdOblast"]);
            }           

            try
            {
                if (ModelState.IsValid)
                {
                    _context.CasopisNaslov.Update(cn);
                    _context.SaveChanges();
                }
                return RedirectPermanent("~/Casopis/Index/" + cn.IdRubrika);
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

        public IActionResult Delete(int id)
        {
            CasopisNaslov casopis = (from p in _context.CasopisNaslov
                                         where p.Id == id
                                         select p).Single();

            List<PropisCasopis> propisCasopis = (from pc in _context.PropisCasopis
                                                 where pc.IdCasopis== casopis.Id
                                                           select pc).ToList();

            try
            {
                _context.CasopisNaslov.Remove(casopis);
                _context.PropisCasopis.RemoveRange(propisCasopis);
                _context.SaveChanges();
                return RedirectPermanent("~/Casopis/Index/" + casopis.IdPodrubrika);
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

            CasopisNaslov casopisNaslov = _context.CasopisNaslov.Find(id);
            List<PropisCasopis> prc = (from psm in _context.PropisCasopis
                                                  where psm.IdCasopis == casopisNaslov.Id
                                                  select psm).ToList();

            List<int?> idPropis = (from n in _context.PropisCasopis
                            where n.IdCasopis == casopisNaslov.Id
                            select n.IdPropis).ToList();

            List<int?> idClan = (from n in _context.PropisCasopis
                                where n.IdCasopis == casopisNaslov.Id
                                select n.IdClan).ToList();

            List<int?> idStav = (from n in _context.PropisCasopis
                                 where n.IdCasopis == casopisNaslov.Id
                                 select n.IdStav).ToList();

            List<int?> idTacka = (from n in _context.PropisCasopis
                                  where n.IdCasopis == casopisNaslov.Id
                                  select n.IdTacka).ToList();


            List<Propis> propisi = (from n in _context.Propis
                                    where idPropis.Contains(n.Id)
                                    select n).ToList();
            List<Clan> clanovi = (from n in _context.Clan
                                  where idClan.Contains(n.Id)
                                  select n).ToList();
            List<Stav> stavovi = (from n in _context.Stav
                                  where idStav.Contains(n.Id)
                                  select n).ToList();
            List<Tacka> tacke = (from n in _context.Tacka
                                 where idTacka.Contains(n.Id)
                                 select n).ToList();

            CasopisOznaka casopisOznaka = (from coz in _context.CasopisOznaka
                                where coz.Id == casopisNaslov.IdOznaka
                                select coz).SingleOrDefault();

            CasopisGodina casopisGodina = (from cgod in _context.CasopisGodina
                                where cgod.Id == casopisNaslov.IdGodina
                                select cgod).SingleOrDefault();

            CasopisBroj casopisBroj = (from cbr in _context.CasopisBroj
                              where cbr.Id == casopisNaslov.IdBroj
                              select cbr).SingleOrDefault();

            RubrikaCasopis rubrika = (from r in _context.RubrikaCasopis
                                      where r.ID == casopisNaslov.IdRubrika
                                      select r).SingleOrDefault();

            PodrubrikaCasopis podrubrika = (from pr in _context.PodrubrikaCasopis
                                 where pr.Id == casopisNaslov.IdPodrubrika
                                 select pr).SingleOrDefault();

            GlavneOblastiCasopis glavneOblastiCasopis = (from go in _context.GlavneOblastiCasopis
                                        where go.ID == casopisNaslov.IdOblast
                                        select go).SingleOrDefault();

            List<PdfFajCasopis> pdf = (from p in _context.PdfFajlCasopis
                                       where p.IdCasopis == casopisNaslov.Id
                                       select p).ToList();
            ViewBag.Pdf = pdf;

            ViewBag.Propis = propisi;
            ViewBag.Clan = clanovi;
            ViewBag.Stav = stavovi;
            ViewBag.Tacka = tacke;
            ViewBag.Veze = prc;

            ViewBag.CasopisOznaka = casopisOznaka;
            ViewBag.CasopisGodina = casopisGodina;
            ViewBag.CasopisBroj = casopisBroj;
            ViewBag.Rubrika = rubrika;
            ViewBag.Podrubrika = podrubrika;
            ViewBag.GlavneOblastiCasopis = glavneOblastiCasopis;
            if (email != null)
            {
                return View(casopisNaslov);
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }

        [HttpGet]
        public IActionResult CreateGodina()
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            if (email != null)
            {
                List<GlavneOblastiCasopis> glavneOblastiCasopis = _context.GlavneOblastiCasopis.ToList();
                ViewBag.GlavneOblastiCasopis = glavneOblastiCasopis;
                return View();
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }

        [HttpPost]
        public IActionResult CreateGodina(CasopisGodina casopisGodina)
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            if (email != null)
            {
                List<GlavneOblastiCasopis> glavneOblastiCasopis = _context.GlavneOblastiCasopis.ToList();
                ViewBag.GlavneOblastiCasopis = glavneOblastiCasopis;

                try
                {
                    _context.CasopisGodina.Add(casopisGodina);
                    _context.SaveChanges();
                    ViewBag.Msg = "Успешно сте додали годину за Часопис";
                    return View();
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

        [HttpGet]
        public IActionResult CreateBroj()
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            if (email != null)
            {
                List<CasopisGodina> casopisGodine = _context.CasopisGodina.ToList();
                ViewBag.CasopisGodine = casopisGodine;
                List<GlavneOblastiCasopis> glavneOblastiCasopis = _context.GlavneOblastiCasopis.ToList();
                ViewBag.GlavneOblastiCasopis = glavneOblastiCasopis;
                return View();
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }

        [HttpPost]
        public IActionResult CreateBroj(CasopisBroj casopisBroj)
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            if (email != null)
            {
                List<CasopisGodina> casopisGodine = _context.CasopisGodina.ToList();
                ViewBag.CasopisGodine = casopisGodine;
                List<GlavneOblastiCasopis> glavneOblastiCasopis = _context.GlavneOblastiCasopis.ToList();
                ViewBag.GlavneOblastiCasopis = glavneOblastiCasopis;

                try
                {
                    _context.CasopisBroj.Add(casopisBroj);
                    _context.SaveChanges();
                    ViewBag.Msg = "Успешно сте додали број за годину Часописа";
                    return View();
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

        [HttpGet]
        public IActionResult CreateRubrika()
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            if (email != null)
            {
                List<GlavneOblastiCasopis> glavneOblastiCasopis = _context.GlavneOblastiCasopis.ToList();
                ViewBag.GlavneOblastiCasopis = glavneOblastiCasopis;

                return View();
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }

        [HttpPost]
        public IActionResult CreateRubrika(RubrikaCasopis rubrikaCasopis)
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            if (email != null)
            {

                List<GlavneOblastiCasopis> glavneOblastiCasopis = _context.GlavneOblastiCasopis.ToList();
                ViewBag.GlavneOblastiCasopis = glavneOblastiCasopis;
                List<CasopisBroj> casopisBrojevi = _context.CasopisBroj.ToList();
                ViewBag.CasopisBrojevi = casopisBrojevi;

                try
                {
                    _context.RubrikaCasopis.Add(rubrikaCasopis);
                    _context.SaveChanges();
                    ViewBag.Msg = "Успешно сте додали рубрику за број Часописа";
                    return View();
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

        [HttpGet]
        public IActionResult CreatePodrubrika()
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            if (email != null)
            {
                List<GlavneOblastiCasopis> glavneOblastiCasopis = _context.GlavneOblastiCasopis.ToList();
                ViewBag.GlavneOblastiCasopis = glavneOblastiCasopis;
                List<RubrikaCasopis> rubrikeCasopis = _context.RubrikaCasopis.ToList();
                ViewBag.RubrikeCasopis = rubrikeCasopis;

                return View();
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }
        [HttpPost]
        public IActionResult CreatePodrubrika(PodrubrikaCasopis podrubrikaCasopis)
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            if (email != null)
            {
                List<GlavneOblastiCasopis> glavneOblastiCasopis = _context.GlavneOblastiCasopis.ToList();
                ViewBag.GlavneOblastiCasopis = glavneOblastiCasopis;
                List<RubrikaCasopis> rubrikeCasopis = _context.RubrikaCasopis.ToList();
                ViewBag.RubrikeCasopis = rubrikeCasopis;

                try
                {
                    _context.PodrubrikaCasopis.Add(podrubrikaCasopis);
                    _context.SaveChanges();
                    ViewBag.Msg = "Успешно сте додали подрубрику за изабрану Рубрику";
                    return View();
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

        public JsonResult GetPodrubrika(int IdRubrika)
        {
            List<PodrubrikaCasopis> casopisPodrubrike = new List<PodrubrikaCasopis>();

            casopisPodrubrike = (from prc in _context.PodrubrikaCasopis
                              where prc.IdRubrika == IdRubrika
                                 select prc).ToList();

            casopisPodrubrike.Insert(0, new PodrubrikaCasopis { Id = 0, Naziv = "Изаберите подрубрику" });
            return Json(new SelectList(casopisPodrubrike, "Id", "Naziv"));
        }

        public JsonResult GetRubrika(int IdBroj)
        {
            List<RubrikaCasopis> casopisRubrike = new List<RubrikaCasopis>();

            casopisRubrike = (from rc in _context.RubrikaCasopis
                              where rc.IdBroj == IdBroj
                              select rc).ToList();

            casopisRubrike.Insert(0, new RubrikaCasopis { ID = 0, NazivRubrike = "Изаберите рубрику" });
            return Json(new SelectList(casopisRubrike, "ID", "NazivRubrike"));
        }

        public JsonResult GetBroj(int IdGodina)
        {
            List<CasopisBroj> casopisBrojevi = new List<CasopisBroj>();

            casopisBrojevi = (from cb in _context.CasopisBroj
                              where cb.IdGodina == IdGodina
                              select cb).ToList();

            casopisBrojevi.Insert(0, new CasopisBroj { Id = 0, Naziv = "Изаберите број" });
            return Json(new SelectList(casopisBrojevi, "Id", "Naziv"));
        }

        public JsonResult GetGodina(int IdOblast)
        {
            List<CasopisGodina> casopisGodine = new List<CasopisGodina>();

            casopisGodine = (from cb in _context.CasopisGodina
                             where cb.IdGlavneOblastiCasopis == IdOblast
                             select cb).ToList();

            casopisGodine.Insert(0, new CasopisGodina { Id = 0, Naziv = "Изаберите годину" });
            return Json(new SelectList(casopisGodine, "Id", "Naziv"));
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

        [HttpGet]
        public IActionResult KreirajVezuCasopisPropis(int id,string idRubrika)
        {
            CasopisNaslov casopis = (from c in _context.CasopisNaslov
                                     where c.Id == id
                                     select c).Single();
            List<CasopisBroj> casopisBrojevi = (from cb in _context.CasopisBroj
                                                select cb).ToList();

            List<GlavneOblastiCasopis> glavneOblastiCasopis = _context.GlavneOblastiCasopis.ToList();
            List<RubrikaCasopis> rubrike = _context.RubrikaCasopis.ToList();
            List<CasopisNaslov> casopisNaslovi = (from cn in _context.CasopisNaslov
                                                  where cn.Id == id
                                                  select cn).ToList();
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

            ViewBag.Casopis = casopis;

            return View();
        }

        [HttpPost]
        public IActionResult KreirajVezuCasopisPropis(int id,IFormCollection fc)
        {
            CasopisNaslov casopis = (from c in _context.CasopisNaslov
                                     where c.Id == id
                                     select c).Single();
            List<CasopisBroj> casopisBrojevi = (from cb in _context.CasopisBroj
                                                select cb).ToList();

            List<GlavneOblastiCasopis> glavneOblastiCasopis = _context.GlavneOblastiCasopis.ToList();
            List<RubrikaCasopis> rubrike = _context.RubrikaCasopis.ToList();
            List<CasopisNaslov> casopisNaslovi = (from cn in _context.CasopisNaslov
                                                  where cn.Id == id
                                                  select cn).ToList();
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


            PropisCasopis propisCasopis = new PropisCasopis();

            propisCasopis.IdCasopis = casopis.Id;
            propisCasopis.IdPropis = Convert.ToInt32(fc["IdPropis"]);
            propisCasopis.IdClan = Convert.ToInt32(fc["IdClan"]);
            propisCasopis.IdStav = Convert.ToInt32(fc["IdStav"]);
            propisCasopis.IdTacka = Convert.ToInt32(fc["IdTacka"]);
            propisCasopis.DatumUnosa = DateTime.Now;

            try
            {
                _context.PropisCasopis.Add(propisCasopis);
                _context.SaveChanges();
                string poruka = "1";
                return RedirectPermanent("~/Casopis/KreirajVezuCasopisPropis/" + casopis.Id+"/"+poruka);
            }
            catch
            {
                throw;
            }
        }

        public IActionResult IzmeniCasopis()
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            if (email != null)
            {
                Dictionary<int, string> casopisNaslov = _context.CasopisNaslov
                                                                    .Select(x => new KeyValuePair<int, string>(x.Id, x.Naslov))
                                                                    .ToDictionary(x => x.Key, x => x.Value);
                List<GlavneOblastiCasopis> glavneOblastiCasopis = _context.GlavneOblastiCasopis.ToList();
                List<RubrikaCasopis> casopisRubrike = _context.RubrikaCasopis.ToList();
                List<PodrubrikaCasopis> podrubrike = _context.PodrubrikaCasopis.ToList();
                List<CasopisGodina> casopisGodine = _context.CasopisGodina.ToList();
                List<CasopisBroj> casopisBrojevi = _context.CasopisBroj.ToList();

                ViewBag.GlavneOblastiCasopis = glavneOblastiCasopis;
                ViewBag.CasopisRubrike = casopisRubrike;
                ViewBag.Podrubrike = podrubrike;
                ViewBag.CasopisGodine = casopisGodine;
                ViewBag.CasopisBrojevi = casopisBrojevi;
                ViewBag.CasopisNaslovi = casopisNaslov;
                ViewBag.Email = email;
                return View();
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }

        [HttpGet]
        public IActionResult FileUpload(int id)
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> FileUpload(int id, IFormFile file)
        {
            await UploadFile(id, file);
            TempData["msg"] = "Uspesno ubacen fajl !";
            return View();
        }

        //Upload file on server
        public async Task<bool> UploadFile(int id, IFormFile file)
        {
            string path = "";
            bool iscopied = false;
            try
            {
                if (file.Length > 0)
                {
                    string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory() + "/wwwroot", "UploadPdf"));
                    using (var filestream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    {
                        await file.CopyToAsync(filestream);
                    }
                    PdfFajCasopis pdfFajl = new PdfFajCasopis();
                    try
                    {
                        pdfFajl.NaslovPdf = file.FileName;
                        pdfFajl.PdfPath = "wwwroot/UploadPdf/" + fileName;
                        pdfFajl.IdCasopis = id;
                        _context.PdfFajlCasopis.Add(pdfFajl);
                        _context.SaveChanges();
                    }
                    catch
                    {
                        throw;
                    }
                    iscopied = true;
                }
                else
                {
                    iscopied = false;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return iscopied;
        }

        public IActionResult CitajPdf(int id)
        {
            PdfFajCasopis file = (from p in _context.PdfFajlCasopis
                                           where p.Id == id
                                           select p).SingleOrDefault();
            string path = file.PdfPath;
            return File(System.IO.File.ReadAllBytes(path), "application/pdf");

        }
    }
}