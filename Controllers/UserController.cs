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
    public async Task<IActionResult> Get(int id)
    {
        var user = await _userService.GetAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] UserCreateDto dto)
    {
        try
        {
            var id = await _userService.CreateAsync(dto);

            return CreatedAtAction(nameof(Get), new { id }, null);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromForm] UserUpdateDto dto)
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
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var success = await _userService.DeleteAsync(id);

        if (!success)
            return NotFound();

        return NoContent();
    }
}
