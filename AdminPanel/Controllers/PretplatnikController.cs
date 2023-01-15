using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AdminPanel.Areas.Identity.Data;
using AdminPanel.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    public class PretplatnikController : Controller
    {
        AdminPanelContext _context = new AdminPanelContext();
        // GET: Pretplatnik
        public ActionResult Index()
        {
            string email = HttpContext.Session.GetString("UserEmail");


            if (email != null)
            {
                return View(_context.Pretplatnik.ToList());
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }

        public IActionResult Search(IFormCollection fc)
        {
            string pibCB = fc["pib"];
            string emaiCB = fc["email"];
            string trazeniPojam = fc["trazi"];

            List<Pretplatnik> pretplatnik = new List<Pretplatnik>();
            if(pibCB!=null && pibCB.Equals("on"))
            {
                pretplatnik = (from p in _context.Pretplatnik
                               where p.PIB.Equals(trazeniPojam)
                               select p).ToList();
            }

            if(emaiCB != null && emaiCB.Equals("on"))
            {
                pretplatnik = (from p in _context.Pretplatnik
                               where p.Email.Equals(trazeniPojam)
                               select p).ToList();
            }
            ViewBag.Pib = pibCB + "|"+trazeniPojam;
            return View(pretplatnik);
        }


        // GET: Pretplatnik/Details/5
        public ActionResult Details(int id)
        {
            string email = HttpContext.Session.GetString("UserEmail");

            Pretplatnik pretplatnik = (from p in _context.Pretplatnik
                                       where p.ID == id
                                       select p).SingleOrDefault();

            ViewBag.Pretplatnik = pretplatnik;

            Enkripcija enkripcija = new Enkripcija();
            pretplatnik.Lozinka = enkripcija.Decrypt(pretplatnik.Lozinka);



            if (email != null)
            {
                return View(_context.Pretplatnik.Find(id));
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }

        ////HESIRANJE LOZINKI  POMOCU MD5 ALGORITMA
        //public static string HashPasswordUsingMD5(string password)
        //{
        //    using (var md5 = MD5.Create())
        //    {
        //        byte[] passwordBytes = Encoding.ASCII.GetBytes(password);

        //        byte[] hash = md5.ComputeHash(passwordBytes);

        //        var stringBuilder = new StringBuilder();

        //        for (int i = 0; i < hash.Length; i++)
        //            stringBuilder.Append(hash[i].ToString("X2"));

        //        return stringBuilder.ToString();
        //    }
        //}

        // GET: Pretplatnik/Create
        public ActionResult Create()
        {
            string email = HttpContext.Session.GetString("UserEmail");


            if (email != null)
            {
                return View();
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }

        // POST: Pretplatnik/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pretplatnik pretplatnik)
        {
            Enkripcija enkripcija = new Enkripcija();
            pretplatnik.Lozinka = enkripcija.Encrypt(pretplatnik.Lozinka);

            try
            {
                _context.Pretplatnik.Add(pretplatnik);
                _context.SaveChanges();
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
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

        // GET: Pretplatnik/Edit/5
        public ActionResult Edit(int id)
        {
            string email = HttpContext.Session.GetString("UserEmail");


            if (email != null)
            {
                return View(_context.Pretplatnik.Find(id));
            }
            else
            {
                return RedirectPermanent("~/Identity/Account/Login");
            }
        }

        // POST: Pretplatnik/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Pretplatnik p)
        {
           
            try
            {
                _context.Pretplatnik.Update(p);
                _context.SaveChanges();
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
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

        public ActionResult Delete(int id)
        {
            Pretplatnik pretplatnik = _context.Pretplatnik.Find(id);
            try
            {
                _context.Pretplatnik.Remove(pretplatnik);
                _context.SaveChanges();
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }

    public class Enkripcija
    {
        public string Encrypt(string text)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes("Nesa"));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateEncryptor())
                    {
                        byte[] textBytes = UTF8Encoding.UTF8.GetBytes(text);
                        byte[] bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);
                        return Convert.ToBase64String(bytes, 0, bytes.Length);
                    }
                }
            }
        }

        public string Decrypt(string cipher)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes("Nesa"));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateDecryptor())
                    {
                        byte[] cipherBytes = Convert.FromBase64String(cipher);
                        byte[] bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                        return UTF8Encoding.UTF8.GetString(bytes);
                    }
                }
            }
        }
    }
}