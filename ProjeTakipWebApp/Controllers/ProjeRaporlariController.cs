using ProjeTakipWebApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ProjeTakipWebApp.Controllers
{
    public class ProjeRaporlariController : Controller
    {
        public ProjeTakipDBContext db = new ProjeTakipDBContext();  //VERİ TABANI BAĞLANTISI

        public ActionResult TamamalanmisOncelikGruplari()
        {
            return View();
        }
        public ActionResult VisualizeTamamlanmisDurumGruplari()
        {
            return Json(OncelikGrupTİpi(), JsonRequestBehavior.AllowGet);
        }
        public List<ClassOncelikDurumAnaliz> OncelikGrupTİpi()
        {

            List<ClassOncelikDurumAnaliz> snf = new List<ClassOncelikDurumAnaliz>();
            using (var c = new ProjeTakipDBContext())

                snf = c.PersonelProjeleris.Where(x => x.TamamlanmaDurumu == true).GroupBy(p => p.OncelikDurumu).Select(x => new ClassOncelikDurumAnaliz
                {

                    onceliktipi = x.Key,
                    oncelikadeti = x.Count(),
                }).ToList();

            return snf;
        }
        public ActionResult TamamalanmamisOncelikGruplari()
        {
            return View();
        }
        public ActionResult VisualizeTamamlanmayanDurumruplari()
        {
            return Json(OncelikTamamlanmisGrupTİpi(), JsonRequestBehavior.AllowGet);
        }
        public List<ClassOncelikDurumAnaliz> OncelikTamamlanmisGrupTİpi()
        {
            ;
            List<ClassOncelikDurumAnaliz> snf = new List<ClassOncelikDurumAnaliz>();
            using (var c = new ProjeTakipDBContext())

                snf = c.PersonelProjeleris.Where(x => x.TamamlanmaDurumu == false).GroupBy(p => p.OncelikDurumu).Select(x => new ClassOncelikDurumAnaliz
                {

                    onceliktipi = x.Key,
                    oncelikadeti = x.Count(),
                }).ToList();

            return snf;
        }
        public ActionResult GenelProjeRaporlari()
        {
            return View();
        }
        public ActionResult CanliDestek()
        {
            var destek = db.PersonelBilgileris.Where(x => x.Departman == "Yönetim");
            return View(destek.ToList());
        }
    }
}
