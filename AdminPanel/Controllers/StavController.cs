using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminPanel.Areas.Identity.Data;
using AdminPanel.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    public class StavController : Controller
    {
        AdminPanelContext _context = new AdminPanelContext();
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DodajStav(int id)
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            if (email != null)
            {
                Propis propis = (from p in _context.Propis
                                 where p.Id == id
                                 select p).SingleOrDefault();
                ViewBag.Propis = propis;


                List<Propis> propisi = (from pr in _context.Propis
                                        where pr.Id == id
                                        select pr).ToList();
                ViewBag.Propisi = propisi;

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
                    return RedirectPermanent("~/Stav/DodajStav/" + s.IdClan);
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

        public IActionResult DeleteStav(int id)
        {
            Stav s = _context.Stav.Find(id);
            Clan c = (from cl in _context.Clan
                      where cl.Id == s.IdClan
                      select cl).Single();
            try
            {
                _context.Stav.Remove(s);
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
        public IActionResult EditStav(int id)
        {
            Stav s = _context.Stav.Find(id);
            Clan c = (from cl in _context.Clan
                      where cl.Id == s.IdClan
                      select cl).Single();
            ViewBag.Stav = s;
            ViewBag.Clan = c;

            Propis propis = (from p in _context.Propis
                where p.Id == c.IdPropis
                select p).SingleOrDefault();
            ViewBag.Propis = propis;

            return View();

        }

        [HttpPost]
        public IActionResult EditStav(int id,IFormCollection formCollection)
        {
            string email = HttpContext.Session.GetString("UserEmail");


            if (email != null)
            {
                Stav s = _context.Stav.Find(id);
                s.Tekst = formCollection["Tekst"];
                Clan c = (from cl in _context.Clan
                    where cl.Id == s.IdClan
                    select cl).Single();
                ViewBag.Clan = c;
                try
                {
                    _context.Stav.Update(s);
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