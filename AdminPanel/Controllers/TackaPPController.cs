using AdminPanel.Areas.Identity.Data;
using AdminPanel.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdminPanel.Controllers
{
    public class TackaPPController : Controller
    {
        AdminPanelContext _context = new AdminPanelContext();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DodajTacku(int id)
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            if (email != null)
            {
                ProsvetniPropis propis = _context.ProsvetnIPropis.Find(id);

                List<ProsvetniPropis> propisi = (from pr in _context.ProsvetnIPropis
                                        where pr.Id == id
                                        select pr).ToList();

                ViewBag.IdPropisa = id;
                ViewBag.Propis = propis;
                ViewBag.Propisi = propisi;

                List<ClanPP> clanovi = (from cl in _context.ClanPP
                                      where cl.IdPropis == id
                                      select cl).ToList();
                ViewBag.Clanovi = clanovi;

                var result = from clan in _context.ClanPP
                             where clan.IdPropis == id
                             select clan.Id;

                List<StavPP> stavovi = (from s in _context.StavPP
                                      where s.IdClan == result.First()
                                      select s).ToList();

                ViewBag.Stavovi = stavovi;

                if (clanovi.Count != 0)
                {
                    foreach (ProsvetniPropis p in propisi)
                    {
                        if (p.Id == id)
                        {
                            foreach (ClanPP c in clanovi)
                            {
                                if (c.IdPropis == id)
                                {
                                    foreach (StavPP s in stavovi)
                                    {
                                        if (s.IdClan == c.Id)
                                        {
                                            ViewBag.Stavovi = stavovi;
                                        }
                                    }

                                }
                            }
                        }
                    }
                }

                ViewBag.IdPropisa = id;

                return View();
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }

        [HttpPost]
        public IActionResult DodajTacku(TackaPP t)
        {
            string email = HttpContext.Session.GetString("UserEmail");

            if (email != null)
            {
                int idMax = (from tacka in _context.TackaPP
                             select tacka.Id).Max();
                t.Id = idMax + 1;
                try
                {
                    _context.TackaPP.Add(t);
                    _context.SaveChanges();
                    ViewBag.Msg = "Тачка је успешно убачена";
                    return RedirectPermanent("~/TackaPP/DodajTacku/" + t.IdStav);
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
            TackaPP t = _context.TackaPP.Find(id);
            StavPP s = (from st in _context.StavPP
                      where st.Id == t.IdStav
                      select st).Single();
            ClanPP c = (from cl in _context.ClanPP
                      where cl.Id == s.IdClan
                      select cl).Single();
            try
            {
                _context.TackaPP.Remove(t);
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
        public IActionResult EditTacka(int id)
        {
            TackaPP t = _context.TackaPP.Find(id);
            ViewBag.Tacka = t;
            StavPP s = (from st in _context.StavPP
                      where st.Id == t.IdStav
                      select st).Single();
            ClanPP c = (from cl in _context.ClanPP
                      where cl.Id == s.IdClan
                      select cl).Single();
            ViewBag.Clan = c;
            return View();
        }

        [HttpPost]
        public IActionResult EditTacka(int id, IFormCollection formCollection)
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            if (email != null)
            {
                TackaPP t = _context.TackaPP.Find(id);
                StavPP s = (from st in _context.StavPP
                    where st.Id == t.IdStav
                    select st).Single();
                ClanPP c = (from cl in _context.ClanPP
                    where cl.Id == s.IdClan
                    select cl).Single();
                t.Tekst = formCollection["Tekst"];
                ViewBag.Clan = c;
                try
                {
                    _context.TackaPP.Update(t);
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
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }
    }
}