/*using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAn_FW.Models
{
    public class StoreContext : DbContext
    {
        private const string connectionString = "Data Source=localhost;Initial Catalog=website_petShop;Integrated Security=True";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseMySQL(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SanPham>().HasKey(key => new { key.Masp });
        }
        public DbSet<SanPham> SanPham { set; get; }
    }
}
*/