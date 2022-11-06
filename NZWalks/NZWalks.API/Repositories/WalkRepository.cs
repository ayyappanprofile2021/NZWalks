using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using System.ComponentModel.Design;

namespace NZWalks.API.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NZWalkDbContext nZWalkDbContext;

        public WalkRepository(NZWalkDbContext nZWalkDbContext)
        {
            this.nZWalkDbContext = nZWalkDbContext;
        }

        public async Task<Walk?> AddAsync(Walk walk)
        {
            await this.nZWalkDbContext.Walks.AddAsync(walk);
            await this.nZWalkDbContext.SaveChangesAsync();

            return walk;
        }

        public async Task<List<Walk>> GetAllAsync()
        {
            return await this.nZWalkDbContext.Walks
                .Include(walk=>walk.Region)
                .Include(walk=>walk.WalkDifficulty).                
                ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            var walk = await this.nZWalkDbContext.Walks.
                Include(walk => walk.Region)
                .Include(walk => walk.WalkDifficulty)
                .FirstOrDefaultAsync(walk => walk.Id == id);

            if(walk == null) { return null; }
            return walk;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var existingWalk = await this.nZWalkDbContext.Walks.FirstOrDefaultAsync(walk => walk.Id == id);
            if (existingWalk == null) { return null; }
            this.nZWalkDbContext.Walks.Remove(existingWalk);
            await this.nZWalkDbContext.SaveChangesAsync();

            return existingWalk;
        }

        public async Task<Walk?> UpdateAsync(Walk walk)
        {
            if (walk == null) { return null; }

            this.nZWalkDbContext.Walks.Update(walk);
            await this.nZWalkDbContext.SaveChangesAsync();

            return walk;
        }
    }
}
