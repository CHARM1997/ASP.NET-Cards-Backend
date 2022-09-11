using Cards.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Cards.API.Data
{
    public class CardsDbContext : DbContext //CardsDbContext class inherit from DbContext class
    {
        public CardsDbContext(DbContextOptions options) : base(options)
        {
        }
        // Dbset
        public DbSet<Card> Cards { get; set; } //This Cards represent Sql server card table
    }
}
