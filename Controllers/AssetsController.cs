using Microsoft.AspNetCore.Mvc;
using radar_api.Dtos;
using radar_api.Models;
using radar_api.Services;

namespace radar_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssetsController : ControllerBase
    {
        private readonly IAssetService _service;

        public AssetsController(IAssetService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asset>>> GetAllAssets()
        {
            var assets = await _service.GetAllAsync();
            return Ok(assets);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Asset>> GetAssetById(int id)
        {
            var asset = await _service.GetByIdAsync(id);
            if (asset == null) return NotFound();
            return Ok(asset);
        }

        [HttpPost]
        public async Task<ActionResult<Asset>> CreateAsset(AssetDto dto)
        {
            var asset = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetAssetById), new { id = asset.Id }, asset);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsset(int id, AssetDto dto)
        {
            var success = await _service.UpdateAsync(id, dto);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsset(int id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}
