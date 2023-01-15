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
    public class StavPPController : Controller
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
                ProsvetniPropis propis = (from p in _context.ProsvetnIPropis
                    where p.Id == id
                    select p).SingleOrDefault();
                ViewBag.Propis = propis;

                List<ProsvetniPropis> propisi = (from pr in _context.ProsvetnIPropis
                    where pr.Id == id
                    select pr).ToList();
                ViewBag.Propisi = propisi;

                List<ClanPP> clanovi = (from cl in _context.ClanPP
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
                    return RedirectPermanent("~/StavPP/DodajStav/" + s.IdClan);
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
            StavPP s = _context.StavPP.Find(id);
            ClanPP c = (from cl in _context.ClanPP
                      where cl.Id == s.IdClan
                      select cl).Single();
            try
            {
                _context.StavPP.Remove(s);
                _context.SaveChanges();
                return RedirectPermanent("~/ObradaTekstaPP/Index/" + c.IdPropis);
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
            StavPP s = _context.StavPP.Find(id);
            ClanPP c = (from cl in _context.ClanPP
                      where cl.Id == s.IdClan
                      select cl).Single();
            ViewBag.Stav = s;
            ViewBag.Clan = c;

            return View();

        }

        [HttpPost]
        public IActionResult EditStav(int id, IFormCollection formCollection)
        {
            StavPP s = _context.StavPP.Find(id);
            s.Tekst = formCollection["Tekst"];
            ClanPP c = (from cl in _context.ClanPP
                      where cl.Id == s.IdClan
                      select cl).Single();
            ViewBag.Clan = c;
            try
            {
                _context.StavPP.Update(s);
                _context.SaveChanges();
                return RedirectPermanent("~/ObradaTekstaPP/Index/" + c.IdPropis);
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
    }
}