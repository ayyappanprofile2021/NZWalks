using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly NZWalkDbContext nZWalkDbContext;

        public WalkDifficultyRepository(NZWalkDbContext nZWalkDbContext)
        {
            this.nZWalkDbContext = nZWalkDbContext;
        }
        public async Task<Models.Domain.WalkDifficulty?> AddAsync(Models.Domain.WalkDifficulty walkDifficulty)
        {
            await this.nZWalkDbContext.AddAsync(walkDifficulty);
            await this.nZWalkDbContext.SaveChangesAsync();
            return walkDifficulty;
        }

        public async Task<Models.Domain.WalkDifficulty?> DeleteAsync(Guid id)
        {
            var existingWalkDifficulty = await this.nZWalkDbContext.WalkDifficulty.FirstOrDefaultAsync(walkDiff => walkDiff.Id == id);
            if(existingWalkDifficulty == null) { return null; } 
            
            this.nZWalkDbContext.Remove(existingWalkDifficulty);
            await this.nZWalkDbContext.SaveChangesAsync();

            return existingWalkDifficulty;
        }

        public async Task<List<Models.Domain.WalkDifficulty>> GetAllAsync()
        {
            return await this.nZWalkDbContext.WalkDifficulty.ToListAsync();
        }

        public async Task<Models.Domain.WalkDifficulty?> GetByIdAsync(Guid id)
        {
            return await this.nZWalkDbContext.WalkDifficulty.FirstOrDefaultAsync(walkDiff => walkDiff.Id==id); 
        }

        public async Task<Models.Domain.WalkDifficulty?> UpdateAsync(Models.Domain.WalkDifficulty walkDifficulty)
        {
            var existingWalkDifficulty = await this.nZWalkDbContext.WalkDifficulty.FirstOrDefaultAsync(walkDiff => walkDiff.Id == walkDifficulty.Id);
            if (existingWalkDifficulty == null) { return null; }
            
            existingWalkDifficulty.Code = walkDifficulty.Code;
            this.nZWalkDbContext.Update(existingWalkDifficulty);
            await this.nZWalkDbContext.SaveChangesAsync();

            return existingWalkDifficulty;
        }
    }
}
