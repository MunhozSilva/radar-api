using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using radar_api.Data;
using radar_api.Models;
using radar_api.Dtos;

namespace radar_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssetsController : ControllerBase
    {
        private readonly RadarDbContext _dbContext;

        public AssetsController(RadarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asset>>> GetAllAssets()
        {
            return await _dbContext.Assets.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Asset>> GetAssetById(int id)
        {
            var asset = await _dbContext.Assets.FindAsync(id);
            if (asset == null) return NotFound();
            return asset;
        }

        [HttpPost]
        public async Task<ActionResult<Asset>> CreateAsset([FromBody] AssetDto dto)
        {
            var asset = new Asset
            {
                Ticker = dto.Ticker,
                UrlCode = dto.UrlCode,
                TargetVariation = dto.TargetVariation
            };

            _dbContext.Assets.Add(asset);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAssetById), new { id = asset.Id }, asset);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsset(int id, [FromBody] AssetDto dto)
        {
            var asset = await _dbContext.Assets.FindAsync(id);
            if (asset == null) return NotFound();

            asset.Ticker = dto.Ticker;
            asset.UrlCode = dto.UrlCode;
            asset.TargetVariation = dto.TargetVariation;

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsset(int id)
        {
            var asset = await _dbContext.Assets.FindAsync(id);
            if (asset == null) return NotFound();

            _dbContext.Assets.Remove(asset);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
