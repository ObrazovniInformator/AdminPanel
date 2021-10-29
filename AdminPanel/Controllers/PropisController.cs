using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AdminPanel.Areas.Identity.Data;
using AdminPanel.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdminPanel.Controllers
{
    public class PropisController : Controller
    {
        private AdminPanelContext _context = new AdminPanelContext();
        public IActionResult Index(int id,string idRubrika)
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            if (email != null)
            {
                int idR = Convert.ToInt32(idRubrika);
                List<Propis> propisi = (from p in _context.Propis
                                        where p.IdPodrubrike == id && p.IdRubrike == idR
                                        select p).ToList();
                List<Podnaslov> podnaslovi = (from pod in _context.Podnaslov
                                              select pod).ToList();
                List<Clan> clanovi = (from pod in _context.Clan
                                      select pod).ToList();
                ViewBag.IdPodrubrike = id;
                ViewBag.Podnaslovi = podnaslovi;
                ViewBag.Clanovi = clanovi;
                return View(propisi);
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }
        public IActionResult Search(IFormCollection collection)
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            if (email != null) { 
                string trazeniPropis = collection["Naslov"];
            var propisi = from m in _context.Propis
                         select m;
            List<Podnaslov> podnaslovi = (from pod in _context.Podnaslov
                                          select pod).ToList();
            List<Clan> clanovi = (from pod in _context.Clan
                                  select pod).ToList();
            
            ViewBag.Podnaslovi = podnaslovi;
            ViewBag.Clanovi = clanovi;

            if (!String.IsNullOrEmpty(trazeniPropis))
            {
                propisi = propisi.Where(s => s.Naslov.Contains(trazeniPropis));
            }
            return View(propisi);
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }

        [HttpGet]
        public IActionResult Create(int id)
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            if (email != null)
            {
                ViewBag.IdPodrubrike = id;
                //return View();
               // List<Propis> propisi = (from p in _context.Propis
                                            //where p.IdPodrubrike == id && p.IdRubrike == idR
              //                          select p).ToList();
                //List<Clan> clanovi = (from cl in _context.Clan
                //                      select cl).ToList();
                //List<Stav> stavovi = (from stav in _context.Stav
                //                      select stav).ToList();
                //List<Tacka> tacke = (from ta in _context.Tacka
                //                     select ta).ToList();

                List<Rubrika> rubrike = new List<Rubrika>();

                int idP = (from p in _context.Propis
                           select p.Id).Max();

                ViewBag.IdMaxP = idP;

                rubrike = (from r in _context.Rubrika
                            select r).ToList();
                ViewBag.ListaRubrike = rubrike;

                rubrike.Insert(0, new Rubrika { ID = 0, NazivRubrike = "--Изабери РУБРИКУ--" });

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
        public IActionResult Create(Propis propis, IFormCollection fc)
        {
            List<Rubrika> rubrike = new List<Rubrika>();
            rubrike = (from r in _context.Rubrika
                       select r).ToList();
            ViewBag.ListaRubrike = rubrike;

            rubrike.Insert(0, new Rubrika { ID = 0, NazivRubrike = "--Изабери РУБРИКУ--" });

            int idRubrike = (from pr in _context.Podrubrika
                         where pr.ID == propis.IdPodrubrike
                         select pr.IdRubrika).Single();
            int idPropis = (from pro in _context.Propis
                            select pro.Id).Max();

            Propis p = new Propis();
            propis.Id = idPropis + 1;
            propis.IdRubrike = idRubrike;
            p.Id = propis.Id;
            p.IdRubrike = propis.IdRubrike;
            p.IdPodrubrike = propis.IdPodrubrike;
            p.Naslov = fc["Naslov"];
            p.GlasiloIDatumObjavljivanja = fc["GlasiloIDatumObjavljivanja"];
            p.VrstaPropisa = fc["VrstaPropisa"];
            p.Donosilac = fc["Donosilac"];
            p.DatumStupanjaNaSnaguVerzijePropisa = propis.DatumStupanjaNaSnaguVerzijePropisa;
            p.DatumPrestankaVerzije = propis.DatumPrestankaVerzije;
            p.DatumObjavljivanjaVerzije = propis.DatumObjavljivanjaVerzije;
            p.DatumObjavljivanjaOsnovnogTeksta = propis.DatumObjavljivanjaOsnovnogTeksta;
            p.DatumStupanjaNaSnaguMeđunarodnogUgovora = propis.DatumStupanjaNaSnaguMeđunarodnogUgovora;
            p.DatumStupanjaNaSnaguOsnovnogTekstaPropisa = propis.DatumStupanjaNaSnaguOsnovnogTekstaPropisa;
            p.DatumPrestankaVazenjaPropisa = propis.DatumPrestankaVazenjaPropisa;
            p.DatumPocetkaPrimene = propis.DatumPocetkaPrimene;
            p.PravniOsnovZaDonosenjaPropisa = fc["PravniOsnovZaDonosenjaPropisa"];
            p.NormaOsnovaZaDonosenje = fc["NormaOsnovaZaDonosenje"];
            p.PropisKojiJePrestaoDaVazi = fc["PropisKojiJePrestaoDaVazi"];
            p.NormaOsnovaZaPrestanakVazenja = fc["NormaOsnovaZaPrestanakVazenja"];
            p.DatumPrestankaVazenjaPravnogPrethodnika = fc["DatumPrestankaVazenjaPravnogPrethodnika"];
            p.IstorijskaVerzijaPropisa = fc["IstorijskaVerzijaPropisa"];
            p.Napomena = fc["Napomena"];
            p.ReferencaNaPropis = fc["ReferencaNaPropis"];
            p.NapomeneGlasnika = fc["NapomeneGlasnika"];
            p.TekstPropisa = fc["TekstPropisa"];
            p.Preambula = fc["Preambula"];
            p.RedniBroj = propis.RedniBroj;

            try
            {
                //_context.Propis.Add(propis);
                //_context.SaveChanges();
                //return RedirectPermanent("~/Propis/Index/" + propis.IdPodrubrike);
                Propis.DodajPropis(p);
                ViewBag.Msg = "Успешно убачен пропис";
            }
            catch
            {
                throw;
            }

            //int propisId = (from prop in _context.Propis
            //              select prop.Id).Max();

            //PropisPropis pp = new PropisPropis();
            //pp.IdPropis = propisId;
            //pp.IdClan = Convert.ToInt32(fc["IdClan"]);
            //pp.IdStav = Convert.ToInt32(fc["IdStav"]);
            //pp.IdTacka = Convert.ToInt32(fc["IdTacka"]);
            //pp.DatumUnosa = DateTime.Now;


            //if (pp != null)
            //{
            //    try
            //    {
            //        PropisPropis.DodajVezuPropisPropis(pp);
            //        ViewBag.Msg = "Успех";
            //    }
            //    catch
            //    {
            //        throw;
            //    }
            //}
            ViewBag.IdPodrubrike = p.IdPodrubrike;
          //  List<Rubrika> rubrike = new List<Rubrika>();

            int idP = (from pr in _context.Propis
                       select pr.Id).Max();

            ViewBag.IdMaxP = idP;

            rubrike = (from r in _context.Rubrika
                       select r).ToList();
            ViewBag.ListaRubrike = rubrike;

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            if (email != null)
            {
                Propis p = _context.Propis.Find(id);

                Rubrika rubrika = (from r in _context.Rubrika
                                   where r.ID == p.IdRubrike
                                   select r).Single();

                Podrubrika podrubrika = (from pod in _context.Podrubrika
                                         where pod.ID == p.IdPodrubrike
                                         select pod).Single();

                List<Rubrika> rubrike = (from r in _context.Rubrika
                                         select r).ToList();
                List<Podrubrika> podrubrike = (from po in _context.Podrubrika
                                               select po).ToList();

                ViewBag.Rubrike = rubrike;
                ViewBag.Podrubrike = podrubrike;

                ViewBag.Rubrika = rubrika;
                ViewBag.Podrubrika = podrubrika;

                return View(_context.Propis.Find(id));
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }

        [HttpPost]
        public IActionResult Edit(Propis propis, IFormCollection fcc)
        {
            string p = (from pr in _context.Propis
                        where pr.Id == propis.Id
                        select pr.TekstPropisa).Single();
            if (!string.IsNullOrEmpty(fcc["IdRubrika"]))
            {
                propis.IdRubrike = Convert.ToInt32(fcc["IdRubrika"]);
            }
            if (!string.IsNullOrEmpty(fcc["IdPodrubrika"]))
            {
                propis.IdPodrubrike = Convert.ToInt32(fcc["IdPodrubrika"]);
            }
            if (!string.IsNullOrEmpty(fcc["TekstPropisa"]))
            {
                propis.TekstPropisa = p + fcc["TekstPropisa"];
            }
            else
            {
                propis.TekstPropisa = p;
            }

            try
            {
                _context.Propis.Update(propis);
                _context.SaveChanges();
                return RedirectPermanent("~/Propis/Index/" + propis.IdPodrubrike);
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

            if (email != null)
            {
                Propis p = _context.Propis.Find(id);

                Rubrika rubrika = (from r in _context.Rubrika
                                   where r.ID == p.IdRubrike
                                   select r).Single();

                Podrubrika podrubrika = (from pod in _context.Podrubrika
                                         where pod.ID == p.IdPodrubrike
                                         select pod).Single();

                ViewBag.Rubrika = rubrika;
                ViewBag.Podrubrika = podrubrika;

                return View(_context.Propis.Find(id));
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }

        public IActionResult Delete(int id)
        {
            Propis propis = _context.Propis.Find(id);
            try
            {
                _context.Propis.Remove(propis);
                _context.SaveChanges();
                return RedirectPermanent ("~/Propis/Index/" + propis.IdPodrubrike + "/"+propis.IdRubrike);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult DodajPreambulu(int id)
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            if (email != null)
            {
                Propis propis = _context.Propis.Find(id);
                ViewBag.Propis = propis;
                return View();
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }

        [HttpPost]
        public IActionResult DodajPreambulu(IFormCollection collection)
        {
            int idPropisa = Convert.ToInt32(collection["Id"]);
            Propis propis = (from p in _context.Propis
                             where p.Id == idPropisa
                             select p).Single();

            propis.Preambula = collection["TekstPreambule"];

            try
            {
                _context.Propis.Update(propis);
                _context.SaveChanges();
                ViewBag.Msg = "Успешно додата преамбула";
                return RedirectPermanent("~/Propis/DodajPreambulu/" + idPropisa);
            }
            catch
            {
                throw;
            }
        }

        public JsonResult GetPodrubrika(int IdRubrika)
        {
            List<Podrubrika> podrubrike = new List<Podrubrika>();

            podrubrike = (from prc in _context.Podrubrika
                                 where prc.IdRubrika == IdRubrika
                                 select prc).ToList();

            podrubrike.Insert(0, new Podrubrika { ID = 0, NazivPodrubrike = "Изаберите подрубрику" });
            return Json(new SelectList(podrubrike, "ID", "NazivPodrubrike"));
        }

        public JsonResult GetRubrika(int IdOblast)
        {
            List<Rubrika> rubrike = new List<Rubrika>();

            rubrike = (from rc in _context.Rubrika
                              where rc.IdOblast == IdOblast
                       select rc).ToList();

            rubrike.Insert(0, new Rubrika { ID = 0, NazivRubrike = "Изаберите рубрику" });
            return Json(new SelectList(rubrike, "ID", "NazivRubrike"));
        }


        [HttpGet]
        public IActionResult Hijararhija(int id)
        {
            List<Propis> propisi = (from p in _context.Propis
                                    where p.IdPodrubrike == id
                                    select p).ToList();
            ViewBag.Propisi = propisi;
            return View();
        }

        [HttpPost]
        public IActionResult Hijararhija(int id, IFormCollection fc)
        {
            List<Propis> propisi = (from p in _context.Propis
                                    where p.IdPodrubrike == id
                                    select p).ToList();
            foreach(Propis p in propisi)
            {
                p.RedniBroj =Convert.ToInt32(fc["Propis " + p.Id]);
            }
            try
            {
                _context.SaveChanges();
                return RedirectPermanent("~/Propis/Index/" + id);
            }
            catch
            {
                return View();
            }
           
        }

        [HttpGet]
        public IActionResult KreirajVezuPropisPropis(int id, string idRubrika)
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            if (email != null)
            {
                int idP = (from p in _context.Propis
                           select p.Id).Max();
                ViewBag.IdMaxP = idP;

                Propis propis = _context.Propis.Find(id);

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
                ViewBag.Propis = propis;

                if (idRubrika != null)
                {
                    ViewBag.Msg = "Успешно успостављена веза";
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult KreirajVezuPropisPropis(int id, IFormCollection fc)
        {
            Propis propis = _context.Propis.Find(id);

            PropisPropis propisPropis = new PropisPropis();

            propisPropis.IdPropis = propis.Id;
            propisPropis.IdClan = Convert.ToInt32(fc["IdClan"]);
            propisPropis.IdStav = Convert.ToInt32(fc["IdStav"]);
            propisPropis.IdTacka = Convert.ToInt32(fc["IdTacka"]);
            propisPropis.DatumUnosa = DateTime.Now;
            try
            {
                _context.PropisPropis.Add(propisPropis);
                _context.SaveChanges();

                string poruka = "1";
                return RedirectPermanent("~/Propis/KreirajVezuPropisPropis/" + propis.Id + "/" + poruka);
            }
            catch
            {
                throw;
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

        public IActionResult BrisiTekstNerazdeljen(int id)
        {
            Propis propis = (from p in _context.Propis
                             where p.Id == id
                             select p).Single();
            propis.TekstPropisa = "";

            try
            {
                _context.Propis.Update(propis);
                _context.SaveChanges();
                return RedirectPermanent("~/Propis/Edit/" + id);
            }
            catch
            {
                throw;
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
                    PdfFajlPropis pdfFajl = new PdfFajlPropis();
                    try
                    {
                        pdfFajl.NaslovPdf = file.FileName;
                        pdfFajl.PdfPath = "wwwroot/UploadPdf/" + fileName;
                        pdfFajl.IdPropis = id;
                        _context.PdfFajlPropis.Add(pdfFajl);
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
            PdfFajlPropis file = (from p in _context.PdfFajlPropis
                                           where p.Id == id
                                           select p).Single();
            string path = file.PdfPath;
            return File(System.IO.File.ReadAllBytes(path), "application/pdf");

        }
    }
}