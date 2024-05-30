using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NZWalksApi.Models.Domain;

namespace NZWalksApi.Data
{
    public class ISWalksAuthDbContext : IdentityDbContext
    {
        public ISWalksAuthDbContext(DbContextOptions<ISWalksAuthDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var readerRoleId = "5ecfe980-f18e-4ebb-86b1-9686b788df85";
            var writerRoleId = "2a18e8c0-ea43-411f-a1ec-d2709111a0a1";

            var roles = new List<IdentityRole>()
            {
                new IdentityRole
                {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper(),
                },
                new IdentityRole
                {
                    Id = writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name = "Wirter",
                    NormalizedName = "Wirter".ToUpper(),
                }
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);

        }
    }
}
