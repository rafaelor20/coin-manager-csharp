using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

using back_end.src.models;

[ApiController]
[Route("auth")]

public class AuthController : ControllerBase
{
    private readonly UserService _userService;

    public AuthController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> Authenticate([FromBody] User userDto)
    {
        try
        {
            var user = await _userService.AuthenticateAsync(userDto.Email, userDto.Password);
            return Ok(user);
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("InvalidCredentialsError"))
                return Unauthorized(ex.Message);
            return BadRequest(ex.Message);
        }
    }
}