using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using System.Collections.Immutable;

namespace NZWalks.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLWalkRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var existingWalks = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalks == null)
            {
                return null;
            }
            else
            {
                dbContext.Walks.Remove(existingWalks);
                await dbContext.SaveChangesAsync();
                return existingWalks;
            }
        }

        public async Task<List<Walk>> GetAllAsync()
        {
            return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public Task<Walk?> GetByIdAsync(Guid id)
        {
            return dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk> updateAsync(Guid id, Walk walkdomainmodel)
        {
            //var existingWalk = dbContext.Walks.Find(id);
            var existingWalk = dbContext.Walks.FirstOrDefault(x => x.Id == id);
            if (existingWalk == null)
            {
                throw new InvalidOperationException("Walk not found");
            }

            // Update the existing walk with the new values from the domain model
            existingWalk.Name = walkdomainmodel.Name;
            existingWalk.Description = walkdomainmodel.Description;
            existingWalk.LengthInKm = walkdomainmodel.LengthInKm;
            existingWalk.WalkImageUrl = walkdomainmodel.WalkImageUrl;
            existingWalk.DifficultyId = walkdomainmodel.DifficultyId;
            existingWalk.RegionId = walkdomainmodel.RegionId;


            await dbContext.SaveChangesAsync();

            //dbContext.Walks.Update(existingWalk);
            //dbContext.SaveChanges();

            return existingWalk;
        }
    }
}
