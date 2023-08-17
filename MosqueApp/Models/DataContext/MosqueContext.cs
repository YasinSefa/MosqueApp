using Microsoft.EntityFrameworkCore;
using MosqueApp.Models.Model;

namespace MosqueApp.Models.DataContext
{
    public class MosqueContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=123;");
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Mosque> Mosques { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<LogMosque> LogMosques { get; set; }
        public DbSet<Photos> Photos { get; set; }
        public DbSet<LogPhotos> LogPhotos { get; set; }
    }

}
