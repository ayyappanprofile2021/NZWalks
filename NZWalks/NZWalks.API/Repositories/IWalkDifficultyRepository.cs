using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkDifficultyRepository
    {
        Task<List<Models.Domain.WalkDifficulty>> GetAllAsync();

        Task<Models.Domain.WalkDifficulty?> GetByIdAsync(Guid id);

        Task<Models.Domain.WalkDifficulty?> AddAsync(Models.Domain.WalkDifficulty walkDifficulty);

        Task<Models.Domain.WalkDifficulty?> DeleteAsync(Guid id);

        Task<Models.Domain.WalkDifficulty?> UpdateAsync(Models.Domain.WalkDifficulty walkDifficulty);
    }
}
