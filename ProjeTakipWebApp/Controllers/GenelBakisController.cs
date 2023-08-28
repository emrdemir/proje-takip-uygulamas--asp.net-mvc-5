using ProjeTakipWebApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ProjeTakipWebApp.Controllers
{
    public class GenelBakisController : Controller
    {
        public ProjeTakipDBContext db = new ProjeTakipDBContext();  //VERİ TABANI BAĞLANTISI

        // GET: GenelBakis
        public ActionResult Index()
        {
            int projeSayisi = db.PersonelProjeleris.Count();
            ViewBag.ProjeSayisi = projeSayisi;



            int tamamlanmisProje = db.PersonelProjeleris.Where(p => p.TamamlanmaDurumu == true).Count();
            ViewBag.TamamlanmisProje = tamamlanmisProje;


            var yuksekOncelikliProjeler = db.PersonelProjeleris.Where(p => p.OncelikDurumu == "Yüksek Öncelikli").Count();
            ViewBag.YuksekOncelikli = yuksekOncelikliProjeler;

            var dusukOncelikliProjeler = db.PersonelProjeleris.Where(p => p.OncelikDurumu == "Düşük Öncelikli").Count();
            ViewBag.DusukOncelikli = dusukOncelikliProjeler;


            var ortaOncelikliProjeler = db.PersonelProjeleris.Where(p => p.OncelikDurumu == "Orta Öncelikli").Count();
            ViewBag.OrtaOncelikli = ortaOncelikliProjeler;

            var basariliveyuksek = db.PersonelProjeleris.Where(p => p.TamamlanmaDurumu == true && p.OncelikDurumu == "Yüksek Öncelikli").Count();
            ViewBag.YuksekVeBasarili = basariliveyuksek;

            var basariliveorta = db.PersonelProjeleris.Where(p => p.TamamlanmaDurumu == true && p.OncelikDurumu == "Orta Öncelikli").Count();
            ViewBag.OrtaVeBasarili = basariliveorta;


            var basarilivedusuk = db.PersonelProjeleris.Where(p => p.TamamlanmaDurumu == true && p.OncelikDurumu == "Düşük Öncelikli").Count();
            ViewBag.DusukVeBasarili = basarilivedusuk;



            int tamamlanmamisProje = db.PersonelProjeleris.Where(p => p.TamamlanmaDurumu == false).Count();
            ViewBag.TamamlanmamisProje = tamamlanmamisProje;


            var personelProjeListesi = db.PersonelProjeleris.ToList();
            var personelTamamlanmisProjeSayisi = new Dictionary<int, int>(); //personelid- tamamlanmışproje sayisi çiftlerini tutmak için
            foreach (var personel in db.PersonelBilgileris.ToList())
            {
                int tamamlanmisProjeSayisi = 0;
                foreach (var proje in personel.PersonelProjeleris)
                {
                    if (proje.TamamlanmaDurumu == true)
                    {
                        tamamlanmisProjeSayisi++;
                    }
                }
                personelTamamlanmisProjeSayisi[personel.PersonelBilgileriId] = tamamlanmisProjeSayisi;
            }

            var siraliPersoneListesi = personelTamamlanmisProjeSayisi.OrderByDescending(x => x.Value); //tamamlanmış proje sayısına göre personelleri sırala
            var enCokTamamlananPersonelId = siraliPersoneListesi.First().Key; //en çok tamamlama sayısına sahip personeli al
            var enCokTamamlananPersonel = db.PersonelBilgileris.FirstOrDefault(p => p.PersonelBilgileriId == enCokTamamlananPersonelId);
            ViewBag.EnCokTamamlayanPersonelBilgisi = enCokTamamlananPersonel.AdSoyad;



            int enCokProjeTamamlayanPersonelProjeSayisi = personelTamamlanmisProjeSayisi[enCokTamamlananPersonelId];
            ViewBag.EnCokProjeTamamlayanPersonelinProjeSayisi = enCokProjeTamamlayanPersonelProjeSayisi;
            return View();
        }
        public ActionResult Genelİstatistik()
        {

            var personeller = db.PersonelBilgileris.ToList();
            var personelProjeleri = db.PersonelProjeleris.ToList();
            var tamamlananProjeSayisi = new Dictionary<int, int>();
            var tamamlanmayanProjeSayisi = new Dictionary<int, int>();
            var toplamProjeSayisi = new Dictionary<int, int>();
            foreach (var personel in personeller)
            {
                int tamamlananProje = 0;
                int tamamlanmayanProje = 0;
                int toplamProje = 0;
                foreach (var proje in personelProjeleri)
                {
                    if (proje.PersonelBilgileris.Contains(personel))
                    {

                        toplamProje++;
                        if (proje.TamamlanmaDurumu)
                        {
                            tamamlananProje++;
                        }
                        else
                        {
                            tamamlanmayanProje++;
                        }
                    }
                }
                tamamlananProjeSayisi[personel.PersonelBilgileriId] = tamamlananProje;
                tamamlanmayanProjeSayisi[personel.PersonelBilgileriId] = tamamlanmayanProje;
                toplamProjeSayisi[personel.PersonelBilgileriId] = toplamProje;


            }

            ViewBag.TamamlananProjeSayisi = tamamlananProjeSayisi;
            ViewBag.TamamlanmayanProjeSayisi = tamamlanmayanProjeSayisi;
            ViewBag.ToplamProjeSayisi = toplamProjeSayisi;



            int projeSayisi = db.PersonelProjeleris.Count();
            ViewBag.ProjeSayisi = projeSayisi;



            int personelSayisi = db.PersonelBilgileris.Count();
            ViewBag.PersonelSayisi = personelSayisi;


            int tamamlanmisProje = db.PersonelProjeleris.Where(p => p.TamamlanmaDurumu == true).Count();
            ViewBag.TamamlanmisProje = tamamlanmisProje;

            int tamamlanmamisProje = db.PersonelProjeleris.Where(p => p.TamamlanmaDurumu == false).Count();
            ViewBag.TamamlanmamisProje = tamamlanmamisProje;



            var basarisizveyuksek = db.PersonelProjeleris.Where(p => p.TamamlanmaDurumu == false && p.OncelikDurumu == "Yüksek Öncelikli").Count();
            ViewBag.YuksekVeBasarisiz = basarisizveyuksek;

            var basarisizveorta = db.PersonelProjeleris.Where(p => p.TamamlanmaDurumu == false && p.OncelikDurumu == "Orta Öncelikli").Count();
            ViewBag.OrtaVeBasarisiz = basarisizveorta;


            return View(personeller);
        }
    }
}