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

namespace AdminPanel.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private AdminPanelContext _context = new AdminPanelContext();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

    
        public IActionResult Index()
        {
            string email = HttpContext.Session.GetString("UserEmail");


            if (email != null) { 
                List<GlavneOblasti> glavneOblasti = _context.GlavneOblasti.ToList();
                List<Rubrika> rubrike = _context.Rubrika.ToList();
                List<Podrubrika> podrubrike = _context.Podrubrika.ToList();

                ViewBag.GlavneOblasti = glavneOblasti;
                ViewBag.Rubrike = rubrike;
                ViewBag.Podrubrike = podrubrike;
                ViewBag.Email = email;
                return View();
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }

        public IActionResult Index2()
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            if (email != null)
            {
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
                ViewBag.Email = email;

                return View();
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }

        public IActionResult Index3()
        {
            string email = HttpContext.Session.GetString("UserEmail");

            if (email != null)
            {
                List<RubrikaPP> rubrikePP = _context.RubrikaPP.ToList();
                List<PodrubrikaPP> podrubrikePP = _context.PodrubrikaPP.ToList();

                ViewBag.RubrikePP = rubrikePP;
                ViewBag.PodrubrikePP = podrubrikePP;
                ViewBag.Email = email;
                return View();
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }

        [HttpGet]
        public IActionResult KreirajRubriku()
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            if (email != null)
            {
                List<GlavneOblasti> go = (from g in _context.GlavneOblasti
                                          select g).ToList();
                ViewBag.GlavneOblasti = go;
                return View();
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }
        [HttpPost]
        public IActionResult KreirajRubriku(Rubrika r)
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            List<GlavneOblasti> go = (from g in _context.GlavneOblasti
                                      select g).ToList();
            ViewBag.GlavneOblasti = go;

            if (email != null)
            {
                r.ID = (from rub in _context.Rubrika
                        select rub.ID).Max() + 1;
                try
                {
                    _context.Rubrika.Add(r);
                    _context.SaveChanges();
                    ViewBag.Msg = "Успешно сте додали рубрику";
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
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }

        [HttpGet]
        public IActionResult KreirajPodrubriku()
        {
            string email = HttpContext.Session.GetString("UserEmail");
            if (email != null)
            {
                List<Rubrika> rubrike = _context.Rubrika.ToList();
                ViewBag.Rubrike = rubrike;
                return View();
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }

        [HttpPost]
        public IActionResult KreirajPodrubriku(Podrubrika podrubrika)
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            if (email != null)
            {
                List<Rubrika> rubrike = _context.Rubrika.ToList();
                ViewBag.Rubrike = rubrike;
                podrubrika.ID = (from pod in _context.Podrubrika
                                 select pod.ID).Max()+1;
                try
                {
                    _context.Podrubrika.Add(podrubrika);
                    _context.SaveChanges();
                    ViewBag.Msg = "Подрубрика је успешно убачена";
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
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }


        [HttpGet]
        public IActionResult KreirajRubrikuPP()
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
        public IActionResult KreirajRubrikuPP(RubrikaPP r)
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            if (email != null)
            {
                r.ID = (from rub in _context.RubrikaPP
                        select rub.ID).Max() + 1;
                try
                {
                    _context.RubrikaPP.Add(r);
                    _context.SaveChanges();
                    ViewBag.Msg = "Успешно сте додали рубрику";
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
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }

        [HttpGet]
        public IActionResult KreirajPodrubrikuPP()
        {
            string email = HttpContext.Session.GetString("UserEmail");
            if (email != null)
            {
                List<RubrikaPP> rubrike = _context.RubrikaPP.ToList();
                ViewBag.Rubrike = rubrike;
                return View();
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }

        [HttpPost]
        public IActionResult KreirajPodrubrikuPP(PodrubrikaPP podrubrika)
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            if (email != null)
            {
                List<RubrikaPP> rubrike = _context.RubrikaPP.ToList();
                ViewBag.Rubrike = rubrike;
                podrubrika.ID = (from pod in _context.PodrubrikaPP
                                 select pod.ID).Max() + 1;
                try
                {
                    _context.PodrubrikaPP.Add(podrubrika);
                    _context.SaveChanges();
                    ViewBag.Msg = "Подрубрика је успешно убачена";
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
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
