using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyLover.Models
{
    class MLContext : DbContext
    {
        public MLContext() : base("server=.; database=MoneyLover; trusted_connection=true;") { }

        public DbSet<KhachHang> KhachHangs { set; get; }
        public DbSet<KyHan> KyHans { get; set; }
        public DbSet<NganHang> NganHangs { get; set; }
        public DbSet<SoTietKiem> SoTietKiems { get; set; }
        public DbSet<ThamSo> ThamSos { get; set; }
    }
}
