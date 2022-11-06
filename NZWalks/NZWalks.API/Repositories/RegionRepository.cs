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
        public async Task<IEnumerable<Region>> GetAllAsync()
        {
           return  await this.nZWalkDbContext.Regions.ToListAsync();
        }
    }
}
