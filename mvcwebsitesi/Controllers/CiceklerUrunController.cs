using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mvcwebsitesi.Models;

namespace mvcwebsitesi.Controllers
{
    public class CiceklerUrunController : Controller
    {
         CicekSatisContext db = new CicekSatisContext();
        
        public ActionResult Index()
        {
            var ciceklerUruns = db.CiceklerUruns.Include(c => c.Kategori);
            return View(ciceklerUruns.ToList());
        }
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CiceklerUrun ürün = db.CiceklerUruns.Find(id);
            if (ürün == null)
            {
                return HttpNotFound();
            }
            return View(ürün);
        }
        public ActionResult Create()
        {
            ViewBag.KategoriId = new SelectList(db.Kategoris, "Id", "KategoriAlani");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UrunAd,Aciklama,Fiyat,Resim,OnayliMi,ÖncelikliMi,AnaSayfaMiKategoriId")] CiceklerUrun ciceklerUrun)
        {
            if (ModelState.IsValid)
            {
                db.CiceklerUruns.Add(ciceklerUrun);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KategoriId = new SelectList(db.Kategoris, "Id", "KategoriAlani", ciceklerUrun.KategoriId);
            return View(ciceklerUrun);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CiceklerUrun ciceklerUrun = db.CiceklerUruns.Find(id);
            if (ciceklerUrun == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategoriId = new SelectList(db.Kategoris, "Id", "KategoriAlani", ciceklerUrun.KategoriId);
            return View(ciceklerUrun);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UrunAd,Aciklama,Fiyat,Resim,OnayliMi,ÖncelikliMi,AnaSayfaMi,KategoriId")] CiceklerUrun ciceklerUrun)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ciceklerUrun).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KategoriId = new SelectList(db.Kategoris, "Id", "KategoriAlani", ciceklerUrun.KategoriId);
            return View(ciceklerUrun);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CiceklerUrun ciceklerUrun = db.CiceklerUruns.Find(id);
            if (ciceklerUrun == null)
            {
                return HttpNotFound();
            }
            return View(ciceklerUrun);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CiceklerUrun ciceklerUrun = db.CiceklerUruns.Find(id);
            db.CiceklerUruns.Remove(ciceklerUrun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult AnaSayfa(string aramakutusu)
        {
            //CicekSatisContext Veri = new CicekSatisContext();
            //Veri.Database.Create();

            var urunler = db.CiceklerUruns.Where(i => i.OnayliMi == true);
            if (!string.IsNullOrEmpty(aramakutusu))
            {
                aramakutusu = aramakutusu.ToLower();
                urunler = urunler.Where(i=>i.UrunAd.ToLower().Contains(aramakutusu));
            }

            return View(urunler.ToList());
        }
        public PartialViewResult OncelikliUrunList()
        {
            var urunler = db.CiceklerUruns.Where(i => i.OnayliMi == true && i.ÖncelikliMi==true).ToList();

            return PartialView(urunler);
        }
        public ActionResult UrunSatisDetay(int? id)
        {
           if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CiceklerUrun ciceklerUrun = db.CiceklerUruns.Find(id);
            if (ciceklerUrun == null)
            {
                return HttpNotFound();
            }
            return View(ciceklerUrun);
        }
        public ActionResult List()
        {
            int kategoriId = Convert.ToInt32(RouteData.Values["id"]);
            ViewBag.Kategoriid = kategoriId;

            var ciceklerUruns = db.CiceklerUruns.Where(c => c.KategoriId == kategoriId).ToList();
            return View(ciceklerUruns);
        }
    }
}
