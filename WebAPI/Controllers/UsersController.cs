using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Contexts;
using WebAPI.Helpers.Services;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly UsersService _usersService;

    public UsersController(UsersService usersService)
    {
        _usersService = usersService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllUsersAsync()
    {
        if(ModelState.IsValid)
        {
            var users = await _usersService.GetAllUsersAsync();
            if(users != null)
            {
                return Ok(users);
            }
            return Problem();
        }
        return BadRequest();
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserAsync(int id)
    {
        if (ModelState.IsValid)
        {
            var user = await _usersService.GetUserAsync(id);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }
        return BadRequest();
    }
}
