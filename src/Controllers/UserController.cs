using FeedsWebApi.Dtos;
using FeedsWebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FeedsWebApi.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponseDto>> Get(int id)
    {
        var user = await _userService.GetAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<UserResponseDto>> Create([FromForm] UserCreateDto dto)
    {
        try
        {
            var user = await _userService.CreateAsync(dto);

            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UserResponseDto>> Update(int id, [FromForm] UserUpdateDto dto)
    {
        try
        {
            var updatedUser = await _userService.UpdateAsync(id, dto);

            if (updatedUser == null)
                return NotFound();

            return Ok(updatedUser);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _userService.DeleteAsync(id);

        if (!success)
            return NotFound();

        return NoContent();
    }

    [HttpPost("{userId}/likes/{feedId}")]
    public async Task<IActionResult> LikeFeed(int userId, int feedId)
    {
        var success = await _userService.LikeFeedAsync(userId, feedId);

        if (!success)
            return Conflict("User already liked this feed.");

        return NoContent();
    }

    [HttpDelete("{userId}/likes/{feedId}")]
    public async Task<IActionResult> UnlikeFeed(int userId, int feedId)
    {
        var success = await _userService.UnlikeFeedAsync(userId, feedId);

        if (!success)
            return NotFound();

        return NoContent();
    }
}
