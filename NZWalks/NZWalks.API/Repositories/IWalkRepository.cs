using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<List<Walk>> GetAllAsync();

        Task<Walk?> GetByIdAsync(Guid id);

        Task<Walk?> AddAsync(Walk walk);

        Task<Walk?> DeleteAsync(Guid id);

        Task<Walk?> UpdateAsync(Walk walk);
    }
}
