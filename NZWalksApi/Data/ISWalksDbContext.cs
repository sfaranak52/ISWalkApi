using Microsoft.EntityFrameworkCore;
using NZWalksApi.Models.Domain;

namespace NZWalksApi.Data
{
    public class ISWalksDbContext : DbContext
    {
        public ISWalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed Data For difficulties
            var difficulties = new List<Difficulty>()
            {
                new Difficulty
                {
                    Id = Guid.Parse("3795b306-830b-4856-a201-88560e737912"),
                    Name = "Easy"
                },
                new Difficulty
                {
                    Id = Guid.Parse("37424cdc-ee37-4e18-8c6a-c3220a514d3c"),
                    Name = "Medium"
                },
                new Difficulty
                {
                    Id = Guid.Parse("3482f6ad-0168-47f9-ab8e-47bfe3b64caa"),
                    Name = "Hard"
                },
            };

            // Seed Data Difficulties to the database
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            //Seed Data For regions
            var regions = new List<Region>()
            {
                new Region
                {
                    Id = Guid.Parse("5e8d3572-6583-4483-93c3-f1b44dab0ec2"),
                    Code = "NZH",
                    Name = "Nazhvan",
                    RegionImgUrl = "nazhvanpic.jpg"
                },
                new Region
                {
                    Id = Guid.Parse("1348229d-e55f-42ba-baf8-bf325b0318ef"),
                    Code = "JLF",
                    Name = "Jolfa",
                    RegionImgUrl = "Jolfapic.jpg"
                },
                new Region
                {
                    Id = Guid.Parse("1590bc6d-357e-44a4-87b7-8a463661c9ad"),
                    Code = "HKM",
                    Name = "HAkim Nezami",
                    RegionImgUrl = "HakimNezamipic.jpg"
                },
            };

            // Seed Data Regions to the database
            modelBuilder.Entity<Region>().HasData(regions);

        }
    }
}
