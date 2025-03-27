using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

using back_end.src.models;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] User userDto)
    {
        try
        {
            var user = await _userService.CreateUserAsync(userDto.Email, userDto.Password);
            return CreatedAtAction(nameof(CreateUser), new { id = user.Id, Email = user.Email });
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("DuplicatedEmailError"))
                return Conflict(ex.Message);
            if (ex.Message.Contains("ValidationError"))
                return UnprocessableEntity(ex.Message);
            return BadRequest(ex.Message);
        }
    }
}