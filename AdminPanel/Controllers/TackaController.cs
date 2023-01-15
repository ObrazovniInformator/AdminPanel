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
    public class TackaController : Controller
    {
        AdminPanelContext _context = new AdminPanelContext();
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetStav(int IdClan)
        {
            List<Stav> stavovi = new List<Stav>();

            stavovi = (from cb in _context.Stav
                where cb.IdClan == IdClan
                select cb).ToList();

            stavovi.Insert(0, new Stav { Id = 0, Naziv = "Изаберите став" });
            return Json(new SelectList(stavovi, "Id", "Naziv"));
        }

        [HttpGet]
        public IActionResult DodajTacku(int id)
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            if (email != null)
            {
                List<Stav> stavovi = _context.Stav.ToList();
                ViewBag.Stavovi = stavovi;
                List<Clan> clanovi = (from cl in _context.Clan
                        where cl.IdPropis == id
                        select cl).ToList();
                ViewBag.Clanovi = clanovi;
                return View();
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }

        [HttpPost]
        public IActionResult DodajTacku(Tacka t)
        {
            string email = HttpContext.Session.GetString("UserEmail");

            if (email != null)
            {
                int idMax = (from tacka in _context.Tacka
                             select tacka.Id).Max();
                t.Id = idMax + 1;
                try
                {
                    _context.Tacka.Add(t);
                    _context.SaveChanges();
                    ViewBag.Msg = "Тачка је успешно убачена";
                    return RedirectPermanent("~/Tacka/DodajTacku/" + t.IdStav);
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
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }

        public IActionResult DeleteTacku(int id)
        {
            Tacka t = _context.Tacka.Find(id);
            Stav s = (from st in _context.Stav
                      where st.Id == t.IdStav
                      select st).Single();
            Clan c = (from cl in _context.Clan
                      where cl.Id == s.IdClan
                      select cl).Single();
            try
            {
                _context.Tacka.Remove(t);
                _context.SaveChanges();
                return RedirectPermanent("~/ObradaTeksta/Index/" + c.IdPropis);
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
        public IActionResult EditTacka(int id)
        {
            Tacka t = _context.Tacka.Find(id);
            ViewBag.Tacka = t;
            Stav s = (from st in _context.Stav
                      where st.Id == t.IdStav
                      select st).Single();
            Clan c = (from cl in _context.Clan
                      where cl.Id == s.IdClan
                      select cl).Single();
            ViewBag.Clan = c;
            return View();
        }

        [HttpPost]
        public IActionResult EditTacka(int id,IFormCollection formCollection)
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            if (email != null)
            {
                Tacka t = _context.Tacka.Find(id);
                Stav s = (from st in _context.Stav
                    where st.Id == t.IdStav
                    select st).Single();
                Clan c = (from cl in _context.Clan
                    where cl.Id == s.IdClan
                    select cl).Single();
                t.Tekst = formCollection["Tekst"];
                ViewBag.Clan = c;
                try
                {
                    _context.Tacka.Update(t);
                    _context.SaveChanges();
                    return RedirectPermanent("~/ObradaTeksta/Index/" + c.IdPropis);
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
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }
    }
}