using AdminPanel.Areas.Identity.Data;
using AdminPanel.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdminPanel.Controllers
{
    public class ClanController : Controller
    {
        AdminPanelContext _context = new AdminPanelContext();
        public IActionResult Index(int id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult DodajClan(int id)
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

                List<Podnaslov> podnaslovi = (from po in _context.Podnaslov
                    where po.IdPropis == id
                    select po).ToList();
                ViewBag.Podnaslovi = podnaslovi;

                return View();
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }

        [HttpPost]
        public IActionResult DodajClan(Clan c)
        {
            string email = HttpContext.Session.GetString("UserEmail");

            if (email != null)
            {
                int idMax = (from clan in _context.Clan
                    select clan.Id).Max();
                c.Id = idMax + 1;

                try
                {
                    var replacementOne = c.Naziv.Replace("<p>", "");
                    var replacementTwo = c.Naziv.Replace("</p>", "");
                    var replacementТhree = replacementOne.Replace("</p>", "") + replacementTwo.Replace("<p>" + replacementOne, "");
                    var replacement = replacementТhree.Replace(replacementTwo, "");
                    c.Naziv = replacement;
                    _context.Clan.Add(c);
                    _context.SaveChanges();
                    ViewBag.Msg = "Члан је успешно убачен";
                    return RedirectPermanent("~/Clan/DodajClan/" + c.IdPropis);
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

        [HttpGet]
        public IActionResult IzmeniClan(int id)
        {
            Clan c = _context.Clan.Find(id);
            ViewBag.Clan = c;

            Propis propis = (from p in _context.Propis
                where p.Id == c.IdPropis
                select p).SingleOrDefault();
            ViewBag.Propis = propis;

            return View();
        }
        [HttpPost]
        public IActionResult IzmeniClan(int id,IFormCollection formCollection)
        {
            string email = HttpContext.Session.GetString("UserEmail");


            if (email != null)
            {
                Clan c = _context.Clan.Find(id);
                c.Naziv = formCollection["Naziv"];

                var replacementOne = c.Naziv.Replace("<p>", "");
                var replacementTwo = c.Naziv.Replace("</p>", "");
                var replacementТhree = replacementOne.Replace("</p>", "") + replacementTwo.Replace("<p>" + replacementOne, "");
                var replacement = replacementТhree.Replace(replacementTwo, "");

                c.Naziv = replacement;

                try
                {
                    _context.Clan.Update(c);
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