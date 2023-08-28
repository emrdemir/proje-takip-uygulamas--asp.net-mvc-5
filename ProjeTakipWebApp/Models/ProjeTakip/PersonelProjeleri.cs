using ProjeTakipWebApp.Models.Personel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjeTakipWebApp.Models.ProjeTakip
{
    public class PersonelProjeleri
    {
        public PersonelProjeleri()
        {
            this.PersonelBilgileris = new HashSet<PersonelBilgileri>();
        }
        [Key]
        public int PersonelProjeId { get; set; }
        [DisplayName("PROJE BAŞLIĞI")]
        [StringLength(50, ErrorMessage = "Maksimum Uzunluk 25 Karakterden Fazla Olamaz!")]
        public string PersonelProjeBaslik { get; set; }
        public string PersonelProjeAciklama { get; set; }
        public DateTime OlusturulmaTarihi { get; set; }
        [DisplayName("ÖNCELİK DURUMU")]
        [StringLength(25, ErrorMessage = "Maksimum Uzunluk 25 Karakterden Fazla Olamaz!")]
        public string OncelikDurumu { get; set; }
        public int TamamlanmaOrani { get; set; }
        public DateTime? TamamlanmaTarihi { get; set; }
        public bool TamamlanmaDurumu { get; set; }

        public virtual ICollection<PersonelBilgileri> PersonelBilgileris { get; set; }
    }
}