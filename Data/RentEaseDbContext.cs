using Microsoft.EntityFrameworkCore;
using RentEase.API.Models.Domain;

namespace RentEase.API.Data
{
    public class RentEaseDbContext : DbContext
    {
        public RentEaseDbContext(DbContextOptions<RentEaseDbContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<Property> Properties { get; set; }
        public DbSet<Image> Images { get; set; }

    }
}
