using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace mvcwebsitesi.Models
{
    public class CicekSatisContext : DbContext
    {
        public CicekSatisContext() : base("cicekEntity")
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Müsteri> Müsteris { get; set; }
        public DbSet<Kategori> Kategoris { get; set; }
        public DbSet<CiceklerUrun> CiceklerUruns { get; set; }






    }
}