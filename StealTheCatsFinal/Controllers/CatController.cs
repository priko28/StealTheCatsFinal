using Microsoft.AspNetCore.Mvc;
using StealTheCats.Core.Services;

namespace StealTheCatsFinal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatsController(ICatService catService) : ControllerBase
    {
        private readonly ICatService _catService = catService;

        [HttpPost("fetch")]
        public async Task<IActionResult> FetchCats()
        {
            await _catService.FetchAndSaveCatsAsync();
            return Ok("Cats fetched and saved successfully.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCatById(string id)
        {
            var cat = await _catService.GetCatByIdAsync(id);
            if (cat == null)
                return NotFound();

            return Ok(cat);
        }

        [HttpGet]
        public async Task<IActionResult> GetCats(
            [FromQuery] int page,
            [FromQuery] int pageSize)
        {
            var cats = await _catService.GetCatsAsync(page, pageSize);
            return Ok(cats);
        }

        [HttpGet("by-tag")]
        public async Task<IActionResult> GetCatsByTag(
            [FromBody] string tag,
            [FromQuery] int page,
            [FromQuery] int pageSize)
        {
            var cats = await _catService.GetCatsByTagAsync(tag, page, pageSize);
            return Ok(cats);
        }
    }
}
