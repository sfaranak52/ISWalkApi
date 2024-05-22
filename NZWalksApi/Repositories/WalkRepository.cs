using ISWalksApi.Interfaces;
using Microsoft.EntityFrameworkCore;
using NZWalksApi.Data;
using NZWalksApi.Models.Domain;

namespace ISWalksApi.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly ISWalksDbContext _iSWalksDb;
        public WalkRepository(ISWalksDbContext iSWalksDbContext)
        {
            _iSWalksDb = iSWalksDbContext;
        }
        public async Task<Walk> AddAsync(Walk walk)
        {
            await _iSWalksDb.Walks.AddAsync(walk);
            await _iSWalksDb.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteAsync(Walk walk)
        {
            _iSWalksDb.Remove(walk);
            await _iSWalksDb.SaveChangesAsync();
            return walk;
        }

        public async Task<List<Walk>> GetAllAsync()
        {
            return await _iSWalksDb.Walks.ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await _iSWalksDb.Walks
                .Include("Difficulty")
                .Include("Region")
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var walkRegion = _iSWalksDb.Walks.FirstOrDefault(x => x.Id == id);
            if (walkRegion == null)
            {
                return null;
            }

            walkRegion.Name = walk.Name;
            walkRegion.Description = walk.Description;
            walkRegion.LengthInKm = walk.LengthInKm;
            walkRegion.WalkImgurl = walk.WalkImgurl;
            walkRegion.DifficultyId = walk.DifficultyId;
            walkRegion.RegionId = walk.RegionId;

            await _iSWalksDb.SaveChangesAsync();
            return walkRegion;
        }
    }
}
