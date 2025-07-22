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
        private readonly ILogger _logger;

        public AssetsController(IAssetService service, ILogger<AssetsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Asset>>> GetAllAssets()
        {
            _logger.LogInformation("Getting all assets.");
            var assets = await _service.GetAllAsync();
            return Ok(assets);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Asset>> GetAssetById(int id)
        {
            _logger.LogInformation($"Getting asset with id: {id}.");
            var asset = await _service.GetByIdAsync(id);
            return asset == null ? NotFound() : Ok(asset);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Asset>> CreateAsset(AssetDto dto)
        {
            _logger.LogInformation($"Creating new asset: Name={dto.Ticker}.");
            var asset = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetAssetById), new { id = asset.Id }, asset);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsset(int id, AssetDto dto)
        {
            _logger.LogInformation($"Updating asset with id: {id}. New values: Ticker={dto.Ticker}, TargetVariation={dto.TargetVariation}");
            var success = await _service.UpdateAsync(id, dto);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsset(int id)
        {
            _logger.LogInformation($"Deleting asset with id: {id}");
            var success = await _service.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}
