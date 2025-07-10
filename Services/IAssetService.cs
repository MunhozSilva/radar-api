using radar_api.Dtos;
using radar_api.Models;

namespace radar_api.Services
{
    public interface IAssetService
    {
        Task<IEnumerable<Asset>> GetAllAsync();
        Task<Asset?> GetByIdAsync(int id);
        Task<Asset> CreateAsync(AssetDto dto);
        Task<bool> UpdateAsync(int id, AssetDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
