using NZWalksApi.Models.Domain;

namespace ISWalksApi.Interfaces
{
    public interface IWalkRepository
    {
        Task<Walk> AddAsync(Walk walk);
        Task<List<Walk>> GetAllAsync();
        Task<Walk?> GetByIdAsync(Guid id);
        Task<Walk?> UpdateAsync(Guid id, Walk walk);
        Task<Walk?> DeleteAsync(Walk walk);
    }
}
