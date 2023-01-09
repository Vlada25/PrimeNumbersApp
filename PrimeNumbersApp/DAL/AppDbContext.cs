using Microsoft.EntityFrameworkCore;
using PrimeNumbersApp.DAL.Models;

namespace PrimeNumbersApp.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options)
            : base(options) { }

        public DbSet<PrimeNumber> PrimeNumbers { get; set; }
    }
}
