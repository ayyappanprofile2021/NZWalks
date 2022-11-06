using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalkDbContext nZWalkDbContext;

        public RegionRepository(NZWalkDbContext nZWalkDbContext)
        {
            this.nZWalkDbContext = nZWalkDbContext;
        }

        public async Task<Region> AddAsync(Region region)
        {
            await this.nZWalkDbContext.Regions.AddAsync(region);
            await this.nZWalkDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var region = await this.nZWalkDbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);
            if (region == null) return null;
            this.nZWalkDbContext.Regions.Remove(region);
            await this.nZWalkDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
           return  await this.nZWalkDbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetAsync(Guid id)
        {
            return await this.nZWalkDbContext.Regions.FirstOrDefaultAsync(region => region.Id == id);
        }

        public async Task<Region?> UpdateAsync(Region region)
        {
            var existingRegion = await this.nZWalkDbContext.Regions.FirstOrDefaultAsync(r => r.Id == region.Id);
            if (existingRegion == null) return null;
            
            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.Area = region.Area;
            existingRegion.Latitude = region.Latitude;
            existingRegion.Longtitude = region.Longtitude;
            existingRegion.Population = region.Population;

            this.nZWalkDbContext.Regions.Update(existingRegion);
            await this.nZWalkDbContext.SaveChangesAsync();

            return existingRegion;
        }
    }
}
