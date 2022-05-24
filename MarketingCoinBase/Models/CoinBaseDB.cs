using Microsoft.EntityFrameworkCore;

namespace MarketingCoinBase.Models
{
    public class CoinBaseDB : DbContext
    {
        public CoinBaseDB() { }
        public CoinBaseDB(DbContextOptions<CoinBaseDB> options): base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-HULF4UN;Initial Catalog=coinbase;User ID=sa;Password=***********;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }
     
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Commissions> Commissions { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<AccountBalances> AccountBalances { get; set; }
        public virtual DbSet<Partners> Partners { get; set; }
        public virtual DbSet<UserPartners> UserPartners { get; set; }
        public virtual DbSet<ServeProds> ServeProds { get; set; }

    }
}
