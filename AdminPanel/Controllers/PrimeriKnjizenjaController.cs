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
                    //List<Propis> propisi = (from p in _context.Propis
                    //                        select p).ToList();
                    List<RubrikaPK> rubrikePK = (from rpk in _context2.RubrikaPK
                                                 select rpk).ToList();
                    //List<Clan> clanovi = (from cl in _context.Clan
                    //                      select cl).ToList();
                    //List<Stav> stavovi = (from stav in _context.Stav
                    //                      select stav).ToList();
                    //List<Tacka> tacke = (from ta in _context.Tacka
                    //                     select ta).ToList();

                    ViewBag.IdRubrika = idR;
                    ViewBag.Rubrike = rubrikePK;
                    //ViewBag.Propisi = propisi;
                    //ViewBag.Clanovi = clanovi;
                    //ViewBag.Stavovi = stavovi;
                    //ViewBag.Tacke = tacke;

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
                //List<Propis> propisi = (from p in _context.Propis
                //                            //where p.IdPodrubrike == id && p.IdRubrike == idR
                //                        select p).ToList();
                //List<Clan> clanovi = (from cl in _context.Clan
                //                      select cl).ToList();
                //List<Stav> stavovi = (from stav in _context.Stav
                //                      select stav).ToList();
                //List<Tacka> tacke = (from ta in _context.Tacka
                //                     select ta).ToList();

                
                List<RubrikaPK> rubrikePK = new List<RubrikaPK>();

                int idPK = (from pk in _context.PrimeriKnjizenja
                            select pk.Id).Max();

                ViewBag.IdMaxPK = idPK;

                rubrikePK = (from r in _context.RubrikaPK
                             select r).ToList();
                ViewBag.ListaRubrikePK = rubrikePK;

                rubrikePK.Insert(0, new RubrikaPK { Id = 0, Naziv = "--Изабери РУБРИКУ--" });

                //ViewBag.Propisi = propisi;
                //ViewBag.Clanovi = clanovi;
                //ViewBag.Stavovi = stavovi;
                //ViewBag.Tacke = tacke;

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


            //List<Propis> propisi = (from p in _context.Propis
            //                            //where p.IdPodrubrike == id && p.IdRubrike == idR
            //                        select p).ToList();
            //List<Clan> clanovi = (from cl in _context.Clan
            //                      select cl).ToList();
            //List<Stav> stavovi = (from stav in _context.Stav
            //                      select stav).ToList();
            //List<Tacka> tacke = (from ta in _context.Tacka
            //                     select ta).ToList();

            //ViewBag.Propisi = propisi;
            //ViewBag.Clanovi = clanovi;
            //ViewBag.Stavovi = stavovi;
            //ViewBag.Tacke = tacke;

            PrimeriKnjizenja primeriKnjizenja = new PrimeriKnjizenja();
            primeriKnjizenja.Naslov = pk.Naslov;
            primeriKnjizenja.Podnaslov = pk.Podnaslov;
            primeriKnjizenja.Napomena = pk.Napomena;
            primeriKnjizenja.Tekst = pk.Tekst;
            //primeriKnjizenja.IdPropis = pk.IdPropis;
            //primeriKnjizenja.IdClan = pk.IdClan;
            //primeriKnjizenja.IdStav = pk.IdStav;
            //primeriKnjizenja.IdTacka = pk.IdTacka;
            primeriKnjizenja.IdRubrikaPK = pk.IdRubrikaPK;

            try
            {
                PrimeriKnjizenja.DodajPrimerKnjizenja(primeriKnjizenja);
                ViewBag.msg = "Успешно додат Пример књижења";
            }
            catch
            {
                throw;
            }

            //int primerKnjizenjaId = (from prk in _context.PrimeriKnjizenja
            //                           select prk.Id).Max();

            //PropisPrimeriKnjizenja ppk = new PropisPrimeriKnjizenja();
            //ppk.IdPropis = primeriKnjizenja.IdPropis;
            //ppk.IdPrimeriKnjizenja = primerKnjizenjaId;
            //ppk.IdClan = primeriKnjizenja.IdClan;
            //ppk.IdStav = primeriKnjizenja.IdStav;
            //ppk.IdTacka = primeriKnjizenja.IdTacka;
            //ppk.DatumUnosa = DateTime.Now;


            //if (ppk != null)
            //{
            //    try
            //    {
            //        PropisPrimeriKnjizenja.DodajPropisPrimeriKnjizenja(ppk);
            //        ViewBag.Msg = "Успех";
            //    }
            //    catch
            //    {
            //        throw;
            //    }
            //}

            //return RedirectPermanent("~/SluzbenoMisljenje/Index/" + sm.IdPodrubrikaSM);
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

            //List<Propis> propisi = (from p in _context.Propis
            //                            //where p.IdPodrubrike == id && p.IdRubrike == idR
            //                        select p).ToList();
            List<RubrikaPK> rubrikePK = (from r in _context.RubrikaPK
                                         select r).ToList();
            //List<Clan> clanovi = (from cl in _context.Clan
            //                      select cl).ToList();
            //List<Stav> stavovi = (from sta in _context.Stav
            //                      select sta).ToList();
            //List<Tacka> tacke = (from ta in _context.Tacka
            //                     select ta).ToList();

            //ViewBag.Propisi = propisi;
            ViewBag.RubrikePK = rubrikePK;
            //ViewBag.Clanovi = clanovi;
            //ViewBag.Stavovi = stavovi;
            //ViewBag.Tacke = tacke;

            PrimeriKnjizenja pk = (from p in _context.PrimeriKnjizenja
                                   where p.Id == id
                                    select p).Single();
            PropisPrimeriKnjizenja ppk = (from pkn in _context.PropisPrimeriKnjizenja
                                          where pkn.IdPrimeriKnjizenja == pk.Id
                                           select pkn).FirstOrDefault();

            //if (ppk != null)
            //{
            //    Propis propis = (from p in _context.Propis
            //                     where p.Id == ppk.IdPropis
            //                     select p).SingleOrDefault();

            //    Clan clan = (from cl in _context.Clan
            //                 where cl.Id == ppk.IdClan
            //                 select cl).SingleOrDefault();

            //    Stav stav = (from st in _context.Stav
            //                 where st.Id == ppk.IdStav
            //                 select st).SingleOrDefault();
            //    Tacka tacka = (from p in _context.Tacka
            //                   where p.Id == ppk.IdTacka
            //                   select p).SingleOrDefault();

            //    ViewBag.Propis = propis;
            //    ViewBag.Clan = clan;
            //    ViewBag.Stav = stav;
            //    ViewBag.Tacka = tacka;
            //}

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
                                   select sr).Single();

            //PropisPrimeriKnjizenja propPk = (from ps in _context.PropisPrimeriKnjizenja
            //                                 where ps.IdPrimeriKnjizenja == pknj.Id
            //                                  select ps).SingleOrDefault();

            //if(propPk !=null)
            //{ 
            //    if (propPk.IdPropis != null || propPk.IdPropis == null)
            //    {
            //        Propis propis = (from p in _context.Propis
            //                         where p.Id == propPk.IdPropis
            //                         select p).SingleOrDefault();
            //    }

            //    if (propPk.IdClan != null)
            //    {
            //        Clan clan = (from cl in _context.Clan
            //                     where cl.Id == propPk.IdClan
            //                     select cl).SingleOrDefault();
            //    }

            //    if (propPk.IdStav != null)
            //    {
            //        Stav stav = (from st in _context.Stav
            //                     where st.Id == propPk.IdStav
            //                     select st).SingleOrDefault();
            //    }
            //}

            RubrikaPK rpk = (from r in _context.RubrikaPK
                             where r.Id == pknj.IdRubrikaPK
                             select r).SingleOrDefault();

            ViewBag.RubrikaPK = rpk;

            pknj.Naslov = pk["Naslov"];
            pknj.Podnaslov = pk["Podnaslov"];
            pknj.Napomena = pk["Napomena"];
            pknj.Tekst = pk["Tekst"];
            //if (!string.IsNullOrEmpty(pk["ListaPropisa"]))
            //{
            //    propPk.IdPropis = Convert.ToInt32(pk["ListaPropisa"]);
            //}
            //if (!string.IsNullOrEmpty(pk["IdClan"]))
            //{
            //    propPk.IdClan = Convert.ToInt32(pk["IdClan"]);
            //}
            //if (!string.IsNullOrEmpty(pk["IdStav"]))
            //{
            //    propPk.IdStav = Convert.ToInt32(pk["IdStav"]);
            //}
            //if (!string.IsNullOrEmpty(pk["IdTacka"]))
            //{
            //    propPk.IdTacka = Convert.ToInt32(pk["IdTacka"]);
            //}
            if (!string.IsNullOrEmpty(pk["ListaRubrikaPK"]))
            {
                pknj.IdRubrikaPK = Convert.ToInt32(pk["ListaRubrikaPK"]);
            }

            try
            {
                _context.PrimeriKnjizenja.Update(pknj);
                //if(propPk != null)
                //{ 
                //    _context.PropisPrimeriKnjizenja.Update(propPk);
                //}
                _context.SaveChanges();
                return RedirectPermanent("~/PrimeriKnjizenja/Index/" + pknj.IdRubrikaPK);
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