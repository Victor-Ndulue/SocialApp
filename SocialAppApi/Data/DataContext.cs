using Microsoft.EntityFrameworkCore;
using SocialAppApi.DataSeeding;
using SocialAppApi.Entities;

namespace SocialAppApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppUserSeed());
        }

        public DbSet<AppUser> Users { get; set; }
    }
}
