using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext _dbContext;

        public SQLRegionRepository(NZWalksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await Task.FromResult(_dbContext.Regions.ToList());
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await Task.FromResult(_dbContext.Regions.Find(id));
        }

        public async Task<Region> CreateAsync(Region region)
        {
            _dbContext.Regions.Add(region);
            await _dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = _dbContext.Regions.Find(id);

            if (existingRegion == null)
            {
                return null;
            }

            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;

            _dbContext.Regions.Update(existingRegion);
            await _dbContext.SaveChangesAsync();

            return existingRegion;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var region = _dbContext.Regions.Find(id);

            if (region == null)
            {
                return false;
            }

            _dbContext.Regions.Remove(region);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
