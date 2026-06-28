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

        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
                                                  string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {
            var walks = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            // Filtering
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }

            // Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }

            // Pagination
            var skipResults = (pageNumber - 1) * pageSize;


            // return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
        }


        public Task<Walk?> GetByIdAsync(Guid id)
        {
            return dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk> updateAsync(Guid id, Walk walkdomainmodel)
        {
            //var existingWalk = dbContext.Walks.Find(id);
            var existingWalk = dbContext.Walks.FirstOrDefault(x => x.Id == id); // FirstOrDefault() is a LINQ query used to find the first entity that matches a specified condition.
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
