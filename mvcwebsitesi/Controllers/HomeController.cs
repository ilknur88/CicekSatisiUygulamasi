using mvcwebsitesi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvcwebsitesi.Controllers
{
    public class HomeController : Controller
    {
        CicekSatisContext db = new CicekSatisContext();
        public ActionResult AcilisSayfa()
        {
            var urunler = db.CiceklerUruns.Where(i => i.OnayliMi == true && i.AnaSayfaMi == true);
            return View(urunler.ToList());
        }
        
    }
}