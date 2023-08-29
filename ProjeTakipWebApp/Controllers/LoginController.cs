using ProjeTakipWebApp.Models;
using ProjeTakipWebApp.Models.Personel;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace ProjeTakipWebApp.Controllers
{
    public class LoginController : Controller
    {
        private ProjeTakipDBContext db = new ProjeTakipDBContext();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(PersonelBilgileri admin)
        {
            var bilgiler = db.PersonelBilgileris.FirstOrDefault(x => x.AdSoyad == admin.AdSoyad && x.Sifre == admin.Sifre);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.AdSoyad, false);
                Session["Kullanici"] = bilgiler.AdSoyad.ToString();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Logout()
        {
            Session["Kullanici"] = null;
            Session.Abandon();
            return RedirectToAction("Index");
        }

    }
}
