using ISWalksApi.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using NZWalksApi.Data;
using NZWalksApi.Models.Domain;

namespace ISWalksApi.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly ISWalksDbContext _iSWalksDbContext;
        public RegionRepository(ISWalksDbContext iSWalksDbContext)
        {
            _iSWalksDbContext = iSWalksDbContext;
        }

        public async Task<Region> AddAsync(Region region)
        {
            await _iSWalksDbContext.Regions.AddAsync(region);
            await _iSWalksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteAsync(Region region)
        {
            _iSWalksDbContext.Remove(region);
            await _iSWalksDbContext.SaveChangesAsync() ;    
            return region;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await _iSWalksDbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await _iSWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region?> UpdateAsync(Guid id,Region region)
        {
            var existRegion = _iSWalksDbContext.Regions.FirstOrDefault(x => x.Id == id);
            if (existRegion == null)
            {
                return null;
            }

            existRegion.Code = region.Code;
            existRegion.Name = region.Name;
            existRegion.RegionImgUrl = region.RegionImgUrl;

            await _iSWalksDbContext.SaveChangesAsync();
            return existRegion;
        }
    }
}
