using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for Difficulties
            // Easy, Medium, Hard
            var difficulties = new List<Difficulty>()
            {
                new Difficulty() { Id = Guid.Parse("a99aaeef-419c-45c0-ad1c-874869be0491"), Name = "Easy" },    //Guid.Parse which converts the string into a Guid object.
                new Difficulty() { Id = Guid.Parse("8c5a8a49-fc1f-46cd-ae4c-630db279c7cb"), Name = "Medium" },
                new Difficulty() { Id = Guid.Parse("f155af42-d7b4-4fc7-ada3-3b312e19b5a0"), Name = "Hard" }
            };

            // Seed difficulties to the database
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            // Seed data for Regions
            var regions = new List<Region>()
            {
                new Region() { Id = Guid.Parse("54b2e727-6470-47eb-826e-69b71d76e281"), Name = "Aukland", Code = "AKL", RegionImageUrl = "https://example.com/aukland.jpg" },
                new Region() { Id = Guid.Parse("49546d38-2cfa-4970-8f91-a44ec708133b"), Name = "Northland", Code = "NTL", RegionImageUrl = "https://example.com/region2.jpg" },
                new Region() { Id = Guid.Parse("9d006462-fe84-4157-8b82-02e9f0e06c8b"), Name = "Bay of Plenty", Code = "BOP", RegionImageUrl = "https://example.com/region3.jpg" },
                new Region() { Id = Guid.Parse("0c14408a-bc92-471f-8aad-92fb0a86f333"), Name = "Wellington", Code = "WLG", RegionImageUrl = "https://example.com/region4.jpg" }
            };

            // Seed regions to the database
            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
