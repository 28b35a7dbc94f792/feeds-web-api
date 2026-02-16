using FeedsWebApi.Dtos;
using FeedsWebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FeedsWebApi.Controllers;

[ApiController]
[Route("api/feeds")]
public class FeedController : ControllerBase
{
    private readonly IFeedService _feedService;

    public FeedController(IFeedService feedService)
    {
        _feedService = feedService;
    }

    [HttpGet]
    public async Task<ActionResult<List<FeedResponseDto>>> GetAll()
    {
        return await _feedService.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FeedResponseDto>> Get(int id)
    {
        var feed = await _feedService.GetAsync(id);

        if (feed == null)
        {
            return NotFound();
        }

        return Ok(feed);
    }

    [HttpGet("{id}/image")]
    public async Task<IActionResult> GetImage(int id)
    {
        var image = await _feedService.GetImageAsync(id);

        if (image == null)
            return NotFound();

        return File(image.Data, 
                    image.ContentType ?? "application/octet-stream");
    }

    [HttpPost]
    public async Task<ActionResult<FeedResponseDto>> Create([FromForm] FeedCreateDto dto)
    {
        try
        {
            var feed = await _feedService.CreateAsync(dto);

            return CreatedAtAction(nameof(Get), new { id = feed.Id }, feed);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<FeedResponseDto>> Update(int id, [FromForm] FeedUpdateDto dto)
    {
        try
        {
            var updatedFeed = await _feedService.UpdateAsync(id, dto);

            if (updatedFeed == null)
                return NotFound();

            return Ok(updatedFeed);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _feedService.DeleteAsync(id);

        if (!success)
            return NotFound();

        return NoContent();
    }
}
