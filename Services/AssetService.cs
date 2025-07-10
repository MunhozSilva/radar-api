using Microsoft.EntityFrameworkCore;
using radar_api.Data;
using radar_api.Dtos;
using radar_api.Models;

namespace radar_api.Services
{
    public class AssetService : IAssetService
    {
        private readonly RadarDbContext _context;

        public AssetService(RadarDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Asset>> GetAllAsync()
        {
            return await _context.Assets.ToListAsync();
        }

        public async Task<Asset?> GetByIdAsync(int id)
        {
            return await _context.Assets.FindAsync(id);
        }

        public async Task<Asset> CreateAsync(AssetDto dto)
        {
            var asset = new Asset
            {
                Ticker = dto.Ticker,
                UrlCode = dto.UrlCode,
                TargetVariation = dto.TargetVariation
            };

            _context.Assets.Add(asset);
            await _context.SaveChangesAsync();
            return asset;
        }

        public async Task<bool> UpdateAsync(int id, AssetDto dto)
        {
            var asset = await _context.Assets.FindAsync(id);
            if (asset == null) return false;

            asset.Ticker = dto.Ticker;
            asset.UrlCode = dto.UrlCode;
            asset.TargetVariation = dto.TargetVariation;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var asset = await _context.Assets.FindAsync(id);
            if (asset == null) return false;

            _context.Assets.Remove(asset);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
