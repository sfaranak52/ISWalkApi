using ISWalksApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalksApi.Data;
using NZWalksApi.Models.Domain;
using System.Diagnostics.SymbolStore;

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

        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null
            , string? sortBy = null, bool isAscending = true
            , int pagenumber = 1, int pageSize = 1000)
        {
            var walks = _iSWalksDb.Walks.Include(x => x.Difficulty).Include(x => x.Region).AsQueryable();
            
            //Filtering
            if(string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x=>x.Name.Contains(filterQuery));
                }
            }

            //Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name): walks.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("Lenght", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }

            //Pagination
            var skipResult = (pagenumber - 1) * pageSize;

            return await walks.Skip(skipResult).Take(pageSize).ToListAsync();
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
