using ProjeTakipWebApp.Models.Personel;
using ProjeTakipWebApp.Models.ProjeTakip;
using System.Data.Entity;

namespace ProjeTakipWebApp.Models
{
    public class ProjeTakipDBContext : DbContext
    {
        public ProjeTakipDBContext() : base("ProjeTakipDB")
        {

        }
        public DbSet<PersonelBilgileri> PersonelBilgileris { get; set; }
        public DbSet<PersonelProjeleri> PersonelProjeleris { get; set; }
    }
}