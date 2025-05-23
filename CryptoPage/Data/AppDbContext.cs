using Microsoft.EntityFrameworkCore;
using CryptoPage.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<CryptoCurrency> Cryptos { get; set; }
    public DbSet<BinanceTicker> binanceCryptos { get; set; }
}