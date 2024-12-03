using Microsoft.AspNetCore.Mvc;
using ToDoWebApi.DTOs;
using ToDoWebApi.Models;
using ToDoWebApi.Services;

namespace ToDoWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController: ControllerBase
{
    private readonly IResourceService<UserModel> _userService;
    private readonly AuthService _authService;


    public UserController(IResourceService<UserModel> userService, AuthService authService)
    {
        _userService = userService;
        _authService = authService;
    }
    
    [HttpPost]
    public async Task<IActionResult> Register(UserDTO user)
    {
        try
        {
            var result = await _userService.Create(new UserModel(user));
            return Ok(_authService.GenerateToken(result));
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(UserDTO user)
    {
        try
        {
            var account = (await _userService.GetResources()).FirstOrDefault(x => x.Email == user.Email && x.Password == user.Password);
            if (account == null)
            {
                return Unauthorized("Invalid credentials");
            }
            return Ok(_authService.GenerateToken(account));
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}
