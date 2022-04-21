using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using mvcwebsitesi.Models;

namespace mvcwebsitesi.Controllers
{
    public class MüsteriController : Controller
    {
        CicekSatisContext db = new CicekSatisContext();

        // GET: Müsteri
        public ActionResult Index()
        {
            int Müsterid = (int)Session["Login"];
            List<Müsteri> Müsteri = db.Müsteris.Where(x => x.MüsteriId == Müsterid ).ToList();
            return View(Müsteri);
        }

        // GET: Müsteri/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Müsteri müsteri = db.Müsteris.Find(id);
            if (müsteri == null)
            {
                return HttpNotFound();
            }
            return View(müsteri);
        }

        // GET: Müsteri/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MüsteriId,AdSoyad,Email,Sifre")] Müsteri müsteri)
        {
            if (ModelState.IsValid)
            {
                db.Müsteris.Add(müsteri);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(müsteri);
        }

        // GET: Müsteri/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Müsteri müsteri = db.Müsteris.Find(id );
            if (müsteri == null)
            {
                return HttpNotFound();
            }
            return View(müsteri);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MüsteriId,AdSoyad,Email,Sifre")] Müsteri müsteri)
        {
            if (ModelState.IsValid)
            {
                db.Entry(müsteri).State = EntityState.Modified;
                db.SaveChanges();
                //return RedirectToAction("Index");
            }
            return View(müsteri);
        }

        // GET: Müsteri/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Müsteri müsteri = db.Müsteris.Find(id);
            if (müsteri == null)
            {
                return HttpNotFound();
            }
            return View(müsteri);
        }

        // POST: Müsteri/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Müsteri müsteri = db.Müsteris.Find(id);
            db.Müsteris.Remove(müsteri);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MüsteriLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult MüsteriLogin(Müsteri müsteri)
        {
            Müsteri Müsteri = db.Müsteris.Where(x => x.MüsteriId == müsteri.MüsteriId && x.Sifre == müsteri.Sifre).SingleOrDefault();
            if (Müsteri != null)
            {
                Session["Login"] = müsteri.MüsteriId;
                return RedirectToAction("AnaSayfa","CiceklerUrun");
            }
            else
            {
                ViewBag.msg = "GEÇERSİZ BİLGİ GİRDİNİZ!";
            }

            return View();
        }
        public ActionResult Cikis()
        {
            Session.Abandon();
            Session.RemoveAll();
            return RedirectToAction("AcilisSayfa", "Home");
        }

        public ActionResult SifreYenile()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SifreYenile(string email, string emailsifre)
        {
            var mail = db.Müsteris.Where(x => x.Email == email).SingleOrDefault();
            if (mail != null)
            {
                SmtpClient client = new SmtpClient();
                client.Credentials = new NetworkCredential(email, emailsifre);
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                MailMessage mailm = new MailMessage();
                mailm.To.Add(email);
                mailm.From = new MailAddress(email, "Şifre");
                mailm.IsBodyHtml = true;
                mailm.Subject = "Şifre Kurtarma";
                mailm.Body += "Merhaba " + mail.AdSoyad + "<br/> Şifre:" + mail.Sifre;
                try
                {
                    client.Send(mailm);
                    db.SaveChanges();
                    Response.Redirect("MüsteriLogin");
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
