using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RentEase.API.Data
{
    public class RentEaseAuthDbContext : IdentityDbContext
    {
        public RentEaseAuthDbContext(DbContextOptions<RentEaseAuthDbContext> dbContextOptions):base(dbContextOptions) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var adminRoleId = "7F13CB20-8F1A-4CC2-A58D-A8C751555DA8";
            var ownerRoleId = "B333A249-D3DB-4DBE-B003-299305FFB181";
            var tenantRoleId = "E6E4797E-61D8-4978-8CA2-AF0AC4EAF487";

            var roles = new List<IdentityRole>()
            {

                new IdentityRole
                {
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper()

                },
                new IdentityRole
                {
                    Id = ownerRoleId,
                    ConcurrencyStamp = ownerRoleId,
                    Name = "Owner",
                    NormalizedName = "Owner".ToUpper()

                },
                new IdentityRole
                {
                    Id = tenantRoleId,
                    ConcurrencyStamp = tenantRoleId,
                    Name = "Tenant",
                    NormalizedName = "Tenant".ToUpper()

                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
