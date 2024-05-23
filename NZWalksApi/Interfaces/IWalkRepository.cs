using Microsoft.AspNetCore.Mvc;
using NZWalksApi.Models.Domain;

namespace ISWalksApi.Interfaces
{
    public interface IWalkRepository
    {
        Task<Walk> AddAsync(Walk walk);
        Task<List<Walk>> GetAllAsync(string? filterOn = null ,string? filterQuery = null , 
            string? sortBy = null,bool isAscending = true,
            int pagenumber = 1, int pageSize = 1000);
        Task<Walk?> GetByIdAsync(Guid id);
        Task<Walk?> UpdateAsync(Guid id, Walk walk);
        Task<Walk?> DeleteAsync(Walk walk);
    }
}
