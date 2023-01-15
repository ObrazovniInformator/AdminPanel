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
    public class ClanPPController : Controller
    {
        AdminPanelContext _context = new AdminPanelContext();
        public IActionResult Index()
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
                ProsvetniPropis propis = (from p in _context.ProsvetnIPropis
                                 where p.Id == id
                                 select p).SingleOrDefault();
                ViewBag.Propis = propis;

                List<ProsvetniPropis> propisi = (from pr in _context.ProsvetnIPropis
                                                 where pr.Id == id
                                        select pr).ToList();
                ViewBag.Propisi = propisi;

                List<PodnaslovPP> podnaslovi = (from po in _context.PodnaslovPP
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
        public IActionResult DodajClan(ClanPP c)
        {
            string email = HttpContext.Session.GetString("UserEmail");


            if (email != null)
            {
                int idMax = (from clan in _context.ClanPP
                             select clan.Id).Max();
                c.Id = idMax + 1;
                try
                {
                    var replacementOne = c.Naziv.Replace("<p>", "");
                    var replacementTwo = c.Naziv.Replace("</p>", "");
                    var replacementТhree = replacementOne.Replace("</p>", "") + replacementTwo.Replace("<p>" + replacementOne, "");
                    var replacement = replacementТhree.Replace(replacementTwo, "");
                    c.Naziv = replacement;
                    _context.ClanPP.Add(c);
                    _context.SaveChanges();
                    ViewBag.Msg = "Члан је успешно убачен";
                    return RedirectPermanent("~/ClanPP/DodajClan/" + c.IdPropis);
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
            ClanPP c = _context.ClanPP.Find(id);
            ViewBag.Clan = c;

            ProsvetniPropis propis = (from p in _context.ProsvetnIPropis
                                      where p.Id == c.IdPropis
                select p).SingleOrDefault();
            ViewBag.Propis = propis;

            return View();
        }
        [HttpPost]
        public IActionResult IzmeniClan(int id, IFormCollection formCollection)
        {
            string email = HttpContext.Session.GetString("UserEmail");


            if (email != null)
            {
                ClanPP c = _context.ClanPP.Find(id);
                c.Naziv = formCollection["Naziv"];

                var replacementOne = c.Naziv.Replace("<p>", "");
                var replacementTwo = c.Naziv.Replace("</p>", "");
                var replacementТhree = replacementOne.Replace("</p>", "") +
                                       replacementTwo.Replace("<p>" + replacementOne, "");
                var replacement = replacementТhree.Replace(replacementTwo, "");

                c.Naziv = replacement;

                try
                {
                    _context.ClanPP.Update(c);
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