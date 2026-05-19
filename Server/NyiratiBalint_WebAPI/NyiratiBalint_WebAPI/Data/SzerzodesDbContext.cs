using Microsoft.EntityFrameworkCore;
using NyiratiBalint_WebAPI.Models;

namespace NyiratiBalint_WebAPI.Data
{
    public class SzerzodesDbContext : DbContext
    {
        public SzerzodesDbContext(DbContextOptions <SzerzodesDbContext> options) : base(options) { }
        public DbSet<Szerzodes> Szerzodesek { get; set; }
        public DbSet<Partner> Partnerek { get; set; }
    }
}
