using ProjeTakipWebApp.Models;
using ProjeTakipWebApp.Models.Personel;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ProjeTakipWebApp.Controllers
{
    public class PersonelBilgilerisController : Controller
    {
        public ProjeTakipDBContext db = new ProjeTakipDBContext();  //VERİ TABANI BAĞLANTISI


        public ActionResult Index() //verileri listeler
        {
            return View(db.PersonelBilgileris.ToList());
        }
        public ActionResult PersonelKart() //verileri listeler
        {
            return View(db.PersonelBilgileris.ToList());
        }


        // GET: PersonelBilgileris/Create
        public ActionResult Create() //EKLEME OLUŞTURMA
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PersonelBilgileri personelBilgileri)
        {
            if (ModelState.IsValid)
            {

                var personel = db.PersonelBilgileris.Where(emre => emre.TCNO == personelBilgileri.TCNO);

                if (personel == null)
                {

                }

                db.PersonelBilgileris.Add(personelBilgileri);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(personelBilgileri);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonelBilgileri personelBilgileri = db.PersonelBilgileris.Find(id);
            if (personelBilgileri == null)
            {
                return HttpNotFound();
            }
            return View(personelBilgileri);
        }

        // GET: PersonelBilgileris/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonelBilgileri personelBilgileri = db.PersonelBilgileris.Find(id);
            if (personelBilgileri == null)
            {
                return HttpNotFound();
            }
            return View(personelBilgileri);
        }

        // POST: PersonelBilgileris/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PersonelBilgileri personelBilgileri)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personelBilgileri).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(personelBilgileri);
        }

        //// GET: PersonelBilgileris/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    PersonelBilgileri personelBilgileri = db.PersonelBilgileris.Find(id);
        //    if (personelBilgileri == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(personelBilgileri);
        //}

        //// POST: PersonelBilgileris/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    PersonelBilgileri personelBilgileri = db.PersonelBilgileris.Find(id);
        //    db.PersonelBilgileris.Remove(personelBilgileri);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        public ActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return HttpNotFound();
            }
            var t = db.PersonelBilgileris.Find(Id);

            db.PersonelBilgileris.Remove(t);
            //TODO: personeli direkt olarak silmeyelim IsDeleted alanı ekleyip o alanı true olarak güncelleyelim.
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
    }
}
