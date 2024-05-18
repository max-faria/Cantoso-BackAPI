using ContosoPizza.Data;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ContosoPizza.Controllers;

[ApiController]

[Route("[controller]")]
public class UserController: ControllerBase
{
    private readonly UserService _userService;
    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _userService.GetById(id);
        if(user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost("signup")]
    public async Task<IActionResult> Create([FromBody]User user)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await _userService.CreateUser(user);
        // return Ok();
        return CreatedAtAction(nameof(GetUserById), new {id = user.UserId}, user);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest();
        }

        var isValidUser = await _userService.VerifyUser(loginModel.Email, loginModel.Password);
        if(!isValidUser)
        {
            return Unauthorized(new { message = "Email or password invalid." });
        }

        var user = await _userService.GetUserByEmail(loginModel.Email);
        var token = _userService.GenerateJwtToken(user);

        return Ok(new { token });
    }

}