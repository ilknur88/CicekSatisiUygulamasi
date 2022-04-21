using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using mvcwebsitesi.Models;

namespace mvcwebsitesi.Controllers
{
    public class AdminController : Controller
    {
        CicekSatisContext db = new CicekSatisContext();

        // GET: Admin
        public ActionResult Index()
        {
            return View(db.Admins.ToList());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AdminId,AdSoyad,Email,Sifre")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Admins.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(admin);
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AdminId,AdSoyad,Email,Sifre")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin);
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Admin admin = db.Admins.Find(id);
            db.Admins.Remove(admin);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Cikis()
        {
            Session.Abandon();
            Session.RemoveAll();
            return RedirectToAction("AcilisSayfa","Home");
        } 
        public ActionResult AdminGiris()
        {
            return View();
        }
        public ActionResult SifreYenile()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SifreYenile(string email, string emailsifre)
        {
            var mail = db.Admins.Where(x => x.Email == email).SingleOrDefault();
            if (mail != null)
            {
                SmtpClient client = new SmtpClient();
                client.Credentials = new NetworkCredential(email, emailsifre);
                client.Port = 587;
                client.Host= "smtp.gmail.com";
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
                    Response.Redirect("AdminLogin");
                }
                catch(Exception)
                {
                    throw;
                }
            }
            return View();
        }
        public ActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminLogin(Admin admin)
        {
            Admin Admin = db.Admins.Where(x => x.AdminId == admin.AdminId && x.Sifre == admin.Sifre).SingleOrDefault();
            if (Admin != null)
            {
                Session["Login"] = admin.AdminId;
                return RedirectToAction("AdminGiris");
            }
            else
            {
                ViewBag.msg = "GEÇERSİZ BİLGİ GİRDİNİZ!";
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
