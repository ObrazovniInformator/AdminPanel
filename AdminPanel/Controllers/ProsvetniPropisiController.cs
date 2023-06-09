using AdminPanel.Areas.Identity.Data;
using AdminPanel.Data;
using Microsoft.AspNetCore.Hosting;
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
    public class ProsvetniPropisiController : Controller
    {
        AdminPanelContext _context = new AdminPanelContext();
        
        IWebHostEnvironment _hostingEnvironment = null;

        public ProsvetniPropisiController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index(int id, string idRubrika)
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            if (email != null)
            {
                int idR = Convert.ToInt32(idRubrika);
                List<ProsvetniPropis> prosvetniPropisi = (from p in _context.ProsvetnIPropis
                                                          where p.IdPodrubrike == id
                                                          select p).ToList();
                List<PodnaslovPP> podnasloviPP = (from pod in _context.PodnaslovPP
                                              select pod).ToList();
                List<ClanPP> clanoviPP = (from pod in _context.ClanPP
                                      select pod).ToList();
                ViewBag.IdPodrubrike = id;
                ViewBag.Podnaslovi = podnasloviPP;
                ViewBag.Clanovi = clanoviPP;

                return View(prosvetniPropisi);
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

            if (email != null)
            {
                string trazeniPropis = collection["Naslov"];
                var propisi = from m in _context.ProsvetnIPropis
                              select m;
                List<PodnaslovPP> podnasloviPP = (from pod in _context.PodnaslovPP
                                              select pod).ToList();
                List<ClanPP> clanoviPP = (from pod in _context.ClanPP
                                      select pod).ToList();

                ViewBag.PodnasloviPP = podnasloviPP;
                ViewBag.ClanoviPP = clanoviPP;

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
                return View();
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }

        [HttpPost]
        public IActionResult Create(ProsvetniPropis propis)
        {
            int idRubrike = (from p in _context.PodrubrikaPP
                             where p.ID == propis.IdPodrubrike
                             select p.IdRubrika).SingleOrDefault();
            int idPropis = (from pr in _context.ProsvetnIPropis
                            select pr.Id).Max();
            propis.Id = idPropis + 1;
            propis.IdRubrike = idRubrike;      

            try
            {
                if (ModelState.IsValid)
                {
                    _context.ProsvetnIPropis.Add(propis);
                    _context.SaveChanges();
                    ViewBag.msg = "Успешно додат Просветни Пропис.";
                }
                else
                {
                    ViewBag.Msg = "Догодила се грешка код чувања у базу. Проверите унете податке и покушајте поново.";
                }
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

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            if (email != null)
            {
                ProsvetniPropis p = _context.ProsvetnIPropis.Find(id);

                RubrikaPP rubrika = (from r in _context.RubrikaPP
                                   where r.ID == p.IdRubrike
                                   select r).SingleOrDefault();

                PodrubrikaPP podrubrika = (from pod in _context.PodrubrikaPP
                                         where pod.ID == p.IdPodrubrike
                                         select pod).SingleOrDefault();

                List<RubrikaPP> rubrike = (from r in _context.RubrikaPP
                                         select r).ToList();
                List<PodrubrikaPP> podrubrike = (from po in _context.PodrubrikaPP
                                               select po).ToList();

                ViewBag.Rubrike = rubrike;
                ViewBag.Podrubrike = podrubrike;
                ViewBag.Rubrika = rubrika;
                ViewBag.Podrubrika = podrubrika;

                ViewBag.IdPodrubrike = id;
                return View(_context.ProsvetnIPropis.Find(id));
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }

        [HttpPost]
        public IActionResult Edit(int id,IFormCollection fcc)
        {
            ProsvetniPropis propis =(from pr in _context.ProsvetnIPropis
                                     where pr.Id == id
                                     select pr).Single();

            propis.Naslov = fcc["Naslov"];
            propis.IdPodrubrike = Convert.ToInt32(fcc["IdPodrubrike"]);
            propis.IdRubrike = Convert.ToInt32(fcc["IdRubrike"]);
            propis.GlasiloIDatumObjavljivanja = fcc["GlasiloIDatumObjavljivanja"];
            propis.VrstaPropisa = fcc["VrstaPropisa"];
            propis.Donosilac = fcc["Donosilac"];
            propis.NivoVazenja = fcc["NivoVazenja"];
            //as
            if (!String.IsNullOrEmpty(fcc["DatumStupanjaNaSnaguVerzijePropisa"])) 
            { 
            propis.DatumStupanjaNaSnaguVerzijePropisa = Convert.ToDateTime(fcc["DatumStupanjaNaSnaguVerzijePropisa"]);
            }
            if (!String.IsNullOrEmpty(fcc["DatumPrestankaVerzije"]))
            {
                propis.DatumPrestankaVerzije = Convert.ToDateTime(fcc["DatumPrestankaVerzije"]);
            }
            if (!String.IsNullOrEmpty(fcc["DatumObjavljivanjaVerzije"]))
            {
                propis.DatumObjavljivanjaVerzije = Convert.ToDateTime(fcc["DatumObjavljivanjaVerzije"]);
            }
            if (!String.IsNullOrEmpty(fcc["DatumObjavljivanjaOsnovnogTeksta"]))
            {
                propis.DatumObjavljivanjaOsnovnogTeksta = Convert.ToDateTime(fcc["DatumObjavljivanjaOsnovnogTeksta"]);
            }
            if (!String.IsNullOrEmpty(fcc["DatumStupanjaNaSnaguMeđunarodnogUgovora"]))
            {
                propis.DatumStupanjaNaSnaguMeđunarodnogUgovora = Convert.ToDateTime(fcc["DatumStupanjaNaSnaguMeđunarodnogUgovora"]);
            }
            if (!String.IsNullOrEmpty(fcc["DatumStupanjaNaSnaguOsnovnogTekstaPropisa"]))
            {
                propis.DatumStupanjaNaSnaguOsnovnogTekstaPropisa = Convert.ToDateTime(fcc["DatumStupanjaNaSnaguOsnovnogTekstaPropisa"]);
            }
            if (!String.IsNullOrEmpty(fcc["DatumPrestankaVazenjaPropisa"]))
            {
                propis.DatumPrestankaVazenjaPropisa = Convert.ToDateTime(fcc["DatumPrestankaVazenjaPropisa"]);
            }
            if (!String.IsNullOrEmpty(fcc["DatumPocetkaPrimene"]))
            {
                propis.DatumPocetkaPrimene = Convert.ToDateTime(fcc["DatumPocetkaPrimene"]);
            }
            string p = (from pr in _context.ProsvetnIPropis
                        where pr.Id == id
                        select pr.TekstPropisa).SingleOrDefault();

            propis.PravniOsnovZaDonosenjaPropisa = fcc["PravniOsnovZaDonosenjaPropisa"];
            propis.NormaOsnovaZaDonosenje = fcc["NormaOsnovaZaDonosenje"];
            propis.PropisKojiJePrestaoDaVazi = fcc["PropisKojiJePrestaoDaVazi"];
            propis.NormaOsnovaZaPrestanakVazenja = fcc["NormaOsnovaZaPrestanakVazenja"];
            propis.DatumPrestankaVazenjaPravnogPrethodnika = fcc["DatumPrestankaVazenjaPravnogPrethodnika"];
            propis.IstorijskaVerzijaPropisa = fcc["IstorijskaVerzijaPropisa"];
            propis.Napomena = fcc["Napomena"];
            propis.ReferencaNaPropis = fcc["ReferencaNaPropis"];
            propis.NapomeneGlasnika = fcc["NapomeneGlasnika"];
            try
            {
                propis.TekstPropisa = p + fcc["TekstPropisa"];
            }
            catch(Exception e)
            {
                PracenjeGresaka pg = new PracenjeGresaka();
                pg.Greska = e.InnerException.Message;
                pg.Datum = DateTime.Now;
                throw;
            }
            propis.Preambula = fcc["Preambula"];

            try
            {

                if (ModelState.IsValid)
                {
                    _context.ProsvetnIPropis.Update(propis);
                    _context.SaveChanges();
                }
                return RedirectPermanent("~/ProsvetniPropisi/Index/" + propis.IdPodrubrike);
            }
            catch(Exception e)
            {
                PracenjeGresaka pg = new PracenjeGresaka();
                pg.Greska = e.InnerException.Message;
                pg.Datum = DateTime.Now;
                _context.PracenjeGresaka.Add(pg);
                _context.SaveChanges();
                throw;
            }
        }

        public IActionResult Details(int id)
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            if (email != null)
            {
                ProsvetniPropis p = _context.ProsvetnIPropis.Find(id);

                RubrikaPP rubrika = (from r in _context.RubrikaPP
                                   where r.ID == p.IdRubrike
                                   select r).SingleOrDefault();

                PodrubrikaPP podrubrika = (from pod in _context.PodrubrikaPP
                                         where pod.ID == p.IdPodrubrike
                                         select pod).SingleOrDefault();

                ViewBag.Rubrika = rubrika;
                ViewBag.Podrubrika = podrubrika;
                ViewBag.IdPodrubrike = id;

                return View(_context.ProsvetnIPropis.Find(id));
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }

        [HttpGet]
        public IActionResult DodajPreambulu(int id)
        {
            string email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = email;

            if (email != null)
            {
                ProsvetniPropis propis = _context.ProsvetnIPropis.Find(id);
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
            ProsvetniPropis propis = (from p in _context.ProsvetnIPropis
                                      where p.Id == idPropisa
                             select p).Single();

            propis.Preambula = collection["TekstPreambule"];

            try
            {
                _context.ProsvetnIPropis.Update(propis);
                _context.SaveChanges();
                ViewBag.Msg = "Успешно додата преамбула";
                return RedirectPermanent("~/ProsvetniPropisi/DodajPreambulu/" + idPropisa);
            }
            catch
            {
                throw;
            }
        }

        public JsonResult GetPodrubrika(int IdRubrika)
        {
            List<PodrubrikaPP> podrubrike = new List<PodrubrikaPP>();

            podrubrike = (from prc in _context.PodrubrikaPP
                          where prc.IdRubrika == IdRubrika
                          select prc).ToList();

            podrubrike.Insert(0, new PodrubrikaPP { ID = 0, NazivPodrubrike = "Изаберите подрубрику" });
            return Json(new SelectList(podrubrike, "ID", "NazivPodrubrike"));
        }

        public JsonResult GetRubrika(int IdOblast)
        {
            List<RubrikaPP> rubrike = new List<RubrikaPP>();

            rubrike = (from rc in _context.RubrikaPP
                       select rc).ToList();

            rubrike.Insert(0, new RubrikaPP { ID = 0, NazivRubrike = "Изаберите рубрику" });
            return Json(new SelectList(rubrike, "ID", "NazivRubrike"));
        }

        public IActionResult Delete(int id)
        {
            ProsvetniPropis p =(from pp in _context.ProsvetnIPropis
                                where pp.Id == id
                                select pp).Single();
            try
            {
                _context.ProsvetnIPropis.Remove(p);
                _context.SaveChanges();
                return RedirectPermanent("~/ProsvetniPropisi/Index/" + p.IdPodrubrike + "/" + p.IdRubrike);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult Hijerarhija(int id)
        {
            List<ProsvetniPropis> prosvetniPropisi = (from p in _context.ProsvetnIPropis
                                             where p.IdPodrubrike == id
                                    select p).ToList();
            ViewBag.ProsvetniPropisi = prosvetniPropisi;
            return View();
        }

        [HttpPost]
        public IActionResult Hijerarhija(int id, IFormCollection fc)
        {
            List<ProsvetniPropis> prosvetniPropisi = (from p in _context.ProsvetnIPropis
                                                      where p.IdPodrubrike == id
                                                      select p).ToList();
            foreach (ProsvetniPropis pp in prosvetniPropisi)
            {
                pp.RedniBroj = Convert.ToInt32(fc["ProsvetniPropis " + pp.Id]);
            }
            try
            {
                _context.SaveChanges();
                return RedirectPermanent("~/ProsvetniPropisi/Index/" + id);
            }
            catch
            {
                return View();
            }

        }

        public IActionResult BrisiTekstNerazdeljen(int id)
        {
            ProsvetniPropis prosvetniPropis = (from pp in _context.ProsvetnIPropis
                                               where pp.Id == id
                                               select pp).Single();
            prosvetniPropis.TekstPropisa = "";
            try
            {
                _context.ProsvetnIPropis.Update(prosvetniPropis);
                _context.SaveChanges();
                return RedirectPermanent("~/ObradaTekstaPP/PrikazCelogTeksta/" + id);

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
                    PdfFajlProsvetniPropis pdfFajl = new PdfFajlProsvetniPropis();
                    try
                    {
                        pdfFajl.NaslovPdf = file.FileName;
                        pdfFajl.PdfPath = "wwwroot/UploadPdf/" + fileName;
                        pdfFajl.IdProsvetniPropis = id;
                        _context.PdfFajlProsvetniPropis.Add(pdfFajl);
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
            PdfFajlProsvetniPropis file = (from p in _context.PdfFajlProsvetniPropis
                                  where p.Id == id
                                  select p).Single();
            string path = file.PdfPath;
            return File(System.IO.File.ReadAllBytes(path), "application/pdf");
        }
    }
}